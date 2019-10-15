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

namespace HelloWorld2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HelloWorldButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello world");
            var result = MessageBox.Show("hello", "hello-world", MessageBoxButton.YesNoCancel, MessageBoxImage.Error);

            switch (result)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    MessageBox.Show("Cancel");
                    break;
                case MessageBoxResult.Yes:
                    MessageBox.Show("YES");
                    break;
                case MessageBoxResult.No:
                    MessageBox.Show("NO");
                    break;
                default:
                    break;
            }
        }
    }
}
