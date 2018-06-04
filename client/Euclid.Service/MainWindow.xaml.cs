using Euclid.CacheManager;
using Euclid.CacheManager.Pof;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Euclid.Service
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel viewModel;
        ICacheManager<PositionKey, Position> _positionCache;
        Stopwatch _stopWatch;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _positionCache = CacheFactory<PositionKey, Position>.GetCacheManager();
            viewModel = new ViewModel();
            this.DataContext = viewModel;            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Time = "0 ms";
            _stopWatch = Stopwatch.StartNew();

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;            
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
            viewModel.Time = "Running";

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _stopWatch.Stop();
            viewModel.Time = _stopWatch.Elapsed.TotalSeconds + " s";
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < 1000; i++)
            {
                String key = i.ToString();

                _positionCache.Save(
                    new PositionKey(key),
                    new Position
                    {
                        PositionId = i.ToString(),
                        Field1 = key + "_Data1",
                        Field2 = key + "_Data2",
                        Field3 = key + "_Data3",
                        Field4 = key + "_Data4",
                        Field5 = key + "_Data5",
                        Field6 = key + "_Data6",
                        Field7 = key + "_Data7",
                        Field8 = key + "_Data8",
                        Field9 = key + "_Data9",
                    });
            }            
        }
    }
}
