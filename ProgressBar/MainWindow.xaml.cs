using System;
using System.Collections.Generic;
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

namespace ProgressBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as ViewModel;

            await Task.Run(async () =>
            {
                while (vm.Progress < 100)
                {
                    vm.Progress += 1;
                    await Task.Delay(10);
                }
            });

            MessageBox.Show("タスク完了");
            vm.Progress = 0;
        }
    }
}
