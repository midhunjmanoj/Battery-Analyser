using BatteryMonitor.Model;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Windows.Devices.Power;



namespace BatteryMonitor
{
    public sealed partial class BatteryCycleWindow : Window
    {

        ObservableCollection<BatteryCycle> batteryCycles = new ObservableCollection<BatteryCycle>();
        public BatteryCycleWindow()
        {
            this.InitializeComponent();
            Fun1();
        }

        public void Fun1()
        {
            var aggBattery = Battery.AggregateBattery;
            var report = aggBattery.GetReport();
            int maxCapacity = (int)report.DesignCapacityInMilliwattHours;
            const string connectionString = "server=INL385\\SQLEXPRESS;database=BatteryDb;Trusted_connection=yes";
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "select * from batteryCycle order by [date] desc";
            comm.Connection = conn;
            conn.Open();
            List<List<string>> cycle = new List<List<string>>();
            SqlDataReader reader = comm.ExecuteReader();
            while (reader.Read())
            {
                string date = reader["date"].ToString();
                double discharge = Convert.ToInt32(reader["discharge"])*100.00 / maxCapacity;
                double dischargeTime = Convert.ToDouble(reader["dischargeTime"]);
                batteryCycles.Add(new BatteryCycle(date, discharge, dischargeTime));


            }
            conn.Close();
        }
    }
}
