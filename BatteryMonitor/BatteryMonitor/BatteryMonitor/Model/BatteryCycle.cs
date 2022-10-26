using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Text;

namespace BatteryMonitor.Model
{
    public class BatteryCycle
    {
        public string Date { get; set; }
        public double Discharge { get; set; }
        public double DischargeTime { get; set; }

        public BatteryCycle( string date,double discharge,double dischargeTime)
        {
            Date = date;
            Discharge = discharge;
            DischargeTime = dischargeTime;
        }
    }

    

}
