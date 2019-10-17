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

namespace NewWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            UpdateWindowCountLabel();
        }

        private void Window1Button_Click(object sender, RoutedEventArgs e)
        {
            var win1 = new Window1();
            win1.Owner = this;
            win1.Closed += (sender, e) => UpdateWindowCountLabel();
            win1.Show();
            UpdateWindowCountLabel();
        }

        Window2 win2 = null;
        private void Window2Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (Window window in OwnedWindows)
            {
                if (window is Window2)
                {
                    return;
                }
            }

            var win2 = new Window2();
            win2.Closed += (sender, e) => UpdateWindowCountLabel();
            win2.Owner = this;
            win2.Show();
            UpdateWindowCountLabel();
        }

        void UpdateWindowCountLabel()
        {
            WindowCountLabel.Content = OwnedWindows.Count;
        }
    }
}
