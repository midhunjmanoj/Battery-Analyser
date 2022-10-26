using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Threading;
using Windows.Devices.Power;

namespace BatteryMonitor.Model
{
    class BatteryController
    {
        public int charge { get; set; } = 0;
        public DateTime time { get; set; }
        public double dischargeTime { get; set; } = 0.0;
        public string status { get; set; }
        public int discharge { get; set; } = 0;
        public int badCount { get; set; } = 0;
        public int optimalCount { get; set; } = 0;
        public int spotCount { get; set; } = 0;
        public BatteryController()
        {
            var aggBattery = Battery.AggregateBattery;
            var report = aggBattery.GetReport();
            charge = (int)report.RemainingCapacityInMilliwattHours;
            status = "" + report.Status;
            time = DateTime.Now;
            Thread thr2 = new Thread(DataUpdater);
            thr2.Start();
            Battery.AggregateBattery.ReportUpdated += AggregateBattery_ReportUpdated;

        }

        public void DataCooker()
        {
            var aggBattery = Battery.AggregateBattery;
            var report = aggBattery.GetReport();
            DateTime currentTime = DateTime.Now;
            int newCharge = (int)report.RemainingCapacityInMilliwattHours;
            string newStatus = "" + report.Status;
            if (charge > newCharge)
            {
                discharge = discharge + (charge - newCharge);
                dischargeTime += (Convert.ToDouble((currentTime.Subtract(time).TotalMinutes).ToString()));
                charge = newCharge;
                status = newStatus;
                time = currentTime;
            }
            else
            {
                charge = newCharge;
                status = newStatus;
                time = currentTime;
            }
            
        }


        public void CookedDataDispatcher()
        {
            const string connectionString = "server=INL385\\SQLEXPRESS;database=BatteryDb;Trusted_connection=yes";
            var aggBattery = Battery.AggregateBattery;
            var report = aggBattery.GetReport();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "Insert into BatteryCycle values(getdate()," + discharge + "," + dischargeTime + ");";
            comm.Connection = conn;
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            discharge = 0;
            dischargeTime = 0.0;
        }
        public void DataUpdater()
        {
            while (true)
            {
                string currentTime = string.Format("{0:HH:mm:ss}", DateTime.Now);
                string pattern = @"..:.6:00";
                Match match = Regex.Match(currentTime, pattern);
                if (match.Success)
                {
                    DataCooker();
                    UpdateBatteryStatDatabase();
                    CookedDataDispatcher();
                }
                System.Threading.Thread.Sleep(1000);
            }

        }
        private void UpdateBatteryStatDatabase()
        {

            const string connectionString = "server=INL385\\SQLEXPRESS;database=BatteryDb;Trusted_connection=yes";
            var aggBattery = Battery.AggregateBattery;
            var report = aggBattery.GetReport();
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand comm = new SqlCommand();
            comm.CommandText = "Insert into BatteryStat values(getdate()," + report.RemainingCapacityInMilliwattHours + ",'" + report.Status + "');";
            comm.Connection = conn;
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();

        }
        public void BatterOverchargeDetector()
        {
            const string connectionString = "server=INL385\\SQLEXPRESS;database=BatteryDb;Trusted_connection=yes";
            var aggBattery = Battery.AggregateBattery;
            var report = aggBattery.GetReport();
            int maxCharge = (int)report.DesignCapacityInMilliwattHours;
            int currentCharge = (int)report.RemainingCapacityInMilliwattHours;
            string newStatus = "" + report.Status;
            if (currentCharge >= maxCharge )
            {
                Thread th3 = new Thread(OverChargeCounter);
                th3.Start();
            }
            else if (status == "Charging")
            {
                if (newStatus != "Charging")
                {
                    SqlConnection conn = new SqlConnection(connectionString);
                    SqlCommand comm = new SqlCommand();
                    comm.CommandText = "Insert into BatteryOvercharge values(getdate(),'spotCount');";
                    comm.Connection = conn;
                    conn.Open();
                    comm.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        public void OverChargeCounter()
        {
            const string connectionString = "server=INL385\\SQLEXPRESS;database=BatteryDb;Trusted_connection=yes";
            DateTime currentTime = DateTime.Now;
            var aggBattery = Battery.AggregateBattery;
            var report = aggBattery.GetReport();
            DateTime oldTime = DateTime.Now;
            int maxCharge = (int)report.DesignCapacityInMilliwattHours;
            int currentCharge = (int)report.RemainingCapacityInMilliwattHours;
            int oldCharge = currentCharge;
            double overChargeTime = 0.0;
            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand comm = new SqlCommand();
            string newStatus = "" + report.Status;
            comm.CommandText = "Insert into BatteryOvercharge values(getdate(),'AtCounter');";
            badCount = 0;
            while (currentCharge >= maxCharge && newStatus!="Discharging")
            {
                oldCharge = currentCharge;
                currentTime = DateTime.Now;
                currentCharge = (int)report.RemainingCapacityInMilliwattHours;
                overChargeTime = (Convert.ToDouble((currentTime.Subtract(oldTime).TotalMinutes).ToString()));
                if (overChargeTime >= 0)
                {
                    badCount += 1;
                    overChargeTime = 0;
                    oldTime = currentTime;
                    comm.CommandText = "Insert into BatteryOvercharge values(getdate(),'badCount');";
                    break;
                }
                newStatus = "" + report.Status;
            }
            if (badCount == 0)
            {
                optimalCount = 1;
                comm.CommandText = "Insert into BatteryOvercharge values(getdate(),'optimalCount');";
            }
            badCount = 0;
            comm.Connection = conn;
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();

        }

        async private void AggregateBattery_ReportUpdated(Battery sender, object args)
        {
            if (true)
            {

                BatterOverchargeDetector();
                DataCooker();
                UpdateBatteryStatDatabase();
            }
        }

    }
}
