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
using System.Diagnostics;
using System.IO;

namespace HoursTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Stopwatch stopwatch = new Stopwatch();
        DateTime startDate;
        DateTime endDate;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_ToggleTimer_Click(object sender, RoutedEventArgs e)
        {
            if (stopwatch.IsRunning)
            {
                StopTimer();
            }
            else
            {
                StartTimer();
            }
        }

        private void StartTimer()
        {
            stopwatch.Start();
            startDate = DateTime.Now;
            lblMessage.Content = "Running... started at " + GetCurrentTimeString() + ".";
            Button_ToggleTimer.Content = "Clock-out";
        }

        private string GetCurrentTimeString()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        private void StopTimer()
        {
            stopwatch.Stop();
            endDate = DateTime.Now;
            lblMessage.Content = "Stopped... stopped at " + GetCurrentTimeString() + ".";
            Button_ToggleTimer.Content = "Clock-in";

            SaveToFile();

            stopwatch.Reset();
        }

        private void SaveToFile()
        {
            StringBuilder entry = new StringBuilder();
            double totalHours = Math.Round(stopwatch.Elapsed.TotalHours, 2);

            entry.AppendLine(GetExcelFriendlyDateTime(startDate) + "," +
                                GetExcelFriendlyDateTime(endDate) + "," +
                                totalHours);

            if (File.Exists("hours.csv"))
            {
                File.AppendAllText("hours.csv", entry.ToString());
            }
            else
            {
                File.AppendAllText("hours.csv", "Start Date,End Date,Hours" + Environment.NewLine + entry.ToString());
            }
        }

        private string GetExcelFriendlyDateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
