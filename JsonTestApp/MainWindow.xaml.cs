using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
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

namespace JsonTestApp
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

        private async void WriteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(BookPriceText.Text, out var price))
            {
                MessageBox.Show("price は整数");
                return;
            }

            string[] authors = new string[]
            {
                "a",
                "b",
            };

            var testDic = new Dictionary<string, string>()
            {
                ["hi"] = "hello",
                ["aa"] = "bb",

            };

            var book = new Book()
            {
                Title = BookTitleText.Text,
                Price = price,
                Authors = authors,
                TestDic = testDic,

            };
            using (var file = File.Open("./test.json", FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true,
                };
                await JsonSerializer.SerializeAsync<Book>(file, book, options);
            }
            MessageBox.Show("Json書き込み完了");
        }

        private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            var book = new Book();
            using (var file = File.OpenText("./test.json"))
            {
                var jsonString = file.ReadToEnd();
                var json = JsonSerializer.Deserialize<Book>(jsonString);
                BookPriceText.Text = json.Price.ToString();
            }
        }
    }
}
