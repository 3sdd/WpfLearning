using Microsoft.Win32;
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
            
            downloadPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), defaultDownloadDir, "file.mp4");
            PathTextBlock.Text = downloadPath;

        }

        static readonly HttpClient client = new HttpClient();
        CancellationTokenSource tokenSource = new CancellationTokenSource();
        bool isDownloading = false;
        static string defaultDownloadDir = "Downloads";
        string downloadPath = "";
        Progress<float> progress = new Progress<float>();

        private async void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            isDownloading = true;
            StatusLabel.Content = "ダウンロード中";
            //using (this.tokenSource = new CancellationTokenSource())
            //{
            if (!Directory.Exists(System.IO.Path.GetDirectoryName(downloadPath)))
            {
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(downloadPath));
            }
            await DownloadFileAsync();
            //}
            StatusLabel.Content = "何もしてない";
            isDownloading = false;
        }


        async Task DownloadFileAsync()
        {

            try
            {

                var url = "https://video.kurashiru.com/production/videos/6e7eaa48-4ead-4c15-9dfa-472652d44497/original.mp4";
                //var filePath = Path.Combine(downloadDir, "./test1.mp4");
                using (tokenSource = new CancellationTokenSource())
                using (var file = new FileStream(this.downloadPath, FileMode.Create, FileAccess.Write, FileShare.None))
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

        private void RefButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Title = "ファイルを開く";
            dialog.Filter = "動画ファイル|*.mp4";
            dialog.InitialDirectory = System.IO.Path.GetDirectoryName(downloadPath);

            if (dialog.ShowDialog() == true)
            {
                PathTextBlock.Text = dialog.FileName;
                downloadPath = dialog.FileName;
            }
        }
    }
}
