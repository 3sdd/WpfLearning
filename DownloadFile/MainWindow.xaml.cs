using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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

namespace DownloadFile
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StatusLabel.Content = "何もしてない";
            progress.ProgressChanged += (sender, e) =>
            {
                DownloadProgressBar.Value = e;
            };
        }

        static readonly HttpClient client = new HttpClient();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        bool isDownloading = false;
        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            isDownloading = true;
            StatusLabel.Content = "ダウンロード中";
            //using (this.tokenSource = new CancellationTokenSource())
            //{
            await DownloadFileAsync();
            //}
            StatusLabel.Content = "何もしてない";
            isDownloading = false;
        }

        Progress<float> progress = new Progress<float>();

        async Task DownloadFileAsync()
        {
            
            try
            {
                
                var url = "https://video.kurashiru.com/production/videos/6e7eaa48-4ead-4c15-9dfa-472652d44497/original.mp4";

                using (tokenSource=new CancellationTokenSource())
                using (var file = new FileStream("./test1.mp4", FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await client.DownloadAsync(url, file, progress, tokenSource.Token);
                }             
            }
            catch (TaskCanceledException tcx)
            {
                if (tcx.CancellationToken.IsCancellationRequested)
                {
                    MessageBox.Show("canceled");
                    var iprogress = progress as IProgress<float>;
                    iprogress.Report(0);
                }
                tokenSource.Dispose();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"exception caught {ex}");
            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (tokenSource == null)
                return;
            if (!isDownloading)
                return;
            tokenSource.Cancel();
        }
    }
}
