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
using System.Windows.Threading;

namespace Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            this.Closing += (sender, e) => timer.Stop();
        }

        DispatcherTimer timer;
        TimeSpan timeSpan;

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (HasFinished())
                return;
            var (s0, s1, m0, m1) = GetTimerNumbers();
            timeSpan = new TimeSpan(0, m1 * 10 + m0, s1 * 10 + s0);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Tick();
            //timeSpan.Subtract(TimeSpan.FromSeconds(1));
            //var minutes = timeSpan.Minutes;
            //var seconds = timeSpan.Seconds;
            //UpdateText(minutes, seconds);


            if (HasFinished())
            {
                System.Media.SystemSounds.Beep.Play();
                timer.Stop();
                //MessageBox.Show("end");
            }


        }

        void UpdateText(int minutes,int seconds)
        {
            var s0 = seconds % 10;
            var s1 = seconds / 10;

            var m0 = minutes % 10;
            var m1 = minutes / 10;

            S0Text.Text = s0.ToString();
            S1Text.Text = s1.ToString();
            M0Text.Text = m0.ToString();
            M1Text.Text = m1.ToString();

        }

        void Tick()
        {
            var (s0, s1, m0, m1) = GetTimerNumbers();
            s0--;
            if (s0 >= 0)
            {
                S0Text.Text = s0.ToString();

                return;
            }
            //s1から

            s1--;
            s0 = 9;
            if (s1 >= 0)
            {
                S0Text.Text = s0.ToString();
                S1Text.Text = s1.ToString();
                return;
            }

            m0--;
            s1 = 5;
            if (m0 >= 0)
            {
                S0Text.Text = s0.ToString();
                S1Text.Text = s1.ToString();
                M0Text.Text = m0.ToString();
                return;
            }

            m1--;
            m0 = 9;
            S0Text.Text = s0.ToString();
            S1Text.Text = s1.ToString();
            M0Text.Text = m0.ToString();
            M1Text.Text = m1.ToString();
        }

        (int s0, int s1, int m0, int m1) GetTimerNumbers()
        {
            int s0 = int.Parse(S0Text.Text);
            int s1 = int.Parse(S1Text.Text);
            int m0 = int.Parse(M0Text.Text);
            int m1 = int.Parse(M1Text.Text);

            return (s0, s1, m0, m1);
        }
        bool HasFinished()
            => S0Text.Text == "0" && S1Text.Text == "0" && M0Text.Text == "0" && M1Text.Text == "0";
        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            var textBlock = GetTextBlock((Button)sender);
            var num = int.Parse(textBlock.Text) - 1;
            num = num < 0 ? 0 : num;
            textBlock.Text = num.ToString();
        }

        private void IncraeseButton_Click(object sender, RoutedEventArgs e)
        {
            var textBlock = GetTextBlock((Button)sender);
            var num = int.Parse(textBlock.Text) + 1;
            num = num > 9 ? 9 : num;
            textBlock.Text = num.ToString();
        }



        TextBlock GetTextBlock(Button sender)
        {
            TextBlock block = null;
            switch (sender.Tag.ToString())
            {
                case "M1":
                    block = M1Text;
                    break;
                case "M0":
                    block = M0Text;
                    break;
                case "S1":
                    block = S1Text;
                    break;
                case "S0":
                    block = S0Text;
                    break;
                default:
                    break;
            }
            return block;
        }
    }
}
