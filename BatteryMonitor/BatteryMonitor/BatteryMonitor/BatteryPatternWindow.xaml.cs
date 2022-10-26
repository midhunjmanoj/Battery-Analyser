using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks.Dataflow;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Data.SqlClient;


namespace BatteryMonitor
{

    public sealed partial class BatteryPatternWindow : Window
    {
        public BatteryPatternWindow()
        {
            this.InitializeComponent();
            batteryPatternUI();
        }
        public void batteryPatternUI()
        {
            int badCount = 0;
            int spotCount = 0;
            int optimalCount = 0;
            const string connectionString = "server=INL385\\SQLEXPRESS;database=BatteryDb;Trusted_connection=yes";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand comm = new SqlCommand();
            string command1 = "Select count(*) as spotCount from BatteryOverCharge where Condition = 'spotCount'";
            string command2 = "Select count(*) as badCount from BatteryOverCharge where Condition = 'badCount'";
            string command3 = "Select count(*) as optimalCount from BatteryOverCharge where Condition = 'optimalCount'";
            comm.Connection = conn;
            conn.Open();
            comm.CommandText = command1;
            SqlDataReader reader1 = comm.ExecuteReader();
            while (reader1.Read())
            {
                spotCount = Convert.ToInt32(reader1["spotCount"]);
            }
            conn.Close();
            conn.Open();
            comm.CommandText = command2;
            SqlDataReader  reader2= comm.ExecuteReader();
            while (reader2.Read())
            {
                badCount = Convert.ToInt32(reader2["badCount"]);
            }
            comm.CommandText = command3;
            conn.Close();
            conn.Open();
             SqlDataReader reader3 = comm.ExecuteReader();
            while (reader3.Read())
            {
                optimalCount = Convert.ToInt32(reader3["optimalCount"]);
            }

            conn.Close();

            TextBlock txt1 = new TextBlock { Text = "Spot Count: " + spotCount };
            txt1.FontSize = 15;
            txt1.Margin = new Thickness(0, 15, 0, 0);
            txt1.TextWrapping = TextWrapping.WrapWholeWords;
            TextBlock txt2 = new TextBlock { Text = "Optimal Count: " + optimalCount };
            txt1.FontSize = 15;
            txt1.Margin = new Thickness(0, 15, 0, 0);
            txt1.TextWrapping = TextWrapping.WrapWholeWords;
            TextBlock txt3 = new TextBlock { Text = "Bad Count: " + badCount };
            txt1.FontSize = 15;
            txt1.Margin = new Thickness(0, 15, 0, 0);
            txt1.TextWrapping = TextWrapping.WrapWholeWords;
            BatteryReportPanel.Children.Add(txt1);
            BatteryReportPanel.Children.Add(txt2);
            BatteryReportPanel.Children.Add(txt3);
        }
    }
}
