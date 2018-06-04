using Euclid.CacheManager;
using Euclid.CacheManager.Pof;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using Tangosol.Net.Cache;

namespace Euclid.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<String> Events { get; set; }
        ICacheManager<PositionKey, Position> _positionCache;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _positionCache = CacheFactory<PositionKey, Position>.GetCacheManager();
            MyListViewBinding = new ObservableCollection<string>();
        }

        public ObservableCollection<string> MyListViewBinding { get; set; }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            _positionCache.Subscribe(new CacheChangeListener(MyListViewBinding));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var position = _positionCache.GetByKey(new PositionKey("0"));
            Console.WriteLine(position);

        }
    }

    public class CacheChangeListener : ICacheListener
    {

        private ObservableCollection<string> MyListViewBinding;

        public CacheChangeListener(ObservableCollection<string> list)
        {
            MyListViewBinding = list;
        }

        public virtual void EntryInserted(CacheEventArgs eventArg)
        {
            App.Current.Dispatcher.Invoke((System.Action)delegate
            {
                MyListViewBinding.Add("Inserted: " + eventArg.Key);
            });
            

        }

        public virtual void EntryUpdated(CacheEventArgs eventArg)
        {
            App.Current.Dispatcher.Invoke((System.Action)delegate
            {
                MyListViewBinding.Add("Updated: " + eventArg.Key);
            });
        }

        public virtual void EntryDeleted(CacheEventArgs eventArg)
        {
            App.Current.Dispatcher.Invoke((System.Action)delegate
            {
                MyListViewBinding.Add("Deleted: " + eventArg.Key);
            });
        }

    }
}
