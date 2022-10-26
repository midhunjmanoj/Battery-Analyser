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
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Devices.Power;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using Windows.ApplicationModel.Contacts;

namespace BatteryMonitor
{
 
    public sealed partial class MainWindow : Window
    {
 
        ObservableCollection<BatteryCycle> batteryCycles = new ObservableCollection<BatteryCycle>();
        public MainWindow()
        {
            this.InitializeComponent();
            BatteryController bc = new BatteryController();
  
        }
        private void GetBatteryReport(object sender, RoutedEventArgs e)
        {
            if (CycleButton.IsChecked == true)
            {
                BatteryCycleWindow w1 = new BatteryCycleWindow();
                w1.Activate();
            }
            else
            {
                BatteryPatternWindow w2 = new BatteryPatternWindow();
                w2.Activate();
            }
        }

       
    }
}
