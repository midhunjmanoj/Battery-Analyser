﻿#pragma checksum "C:\Users\mmanoj\Desktop\soti\BatteryApplication\BatteryMonitor\BatteryMonitor\BatteryMonitor\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "5E6EF699295D452578D28C00681ACADDC00E433701F9690D034BD1AB08D32A62"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BatteryMonitor
{
    partial class MainWindow : 
        global::Microsoft.UI.Xaml.Window, 
        global::Microsoft.UI.Xaml.Markup.IComponentConnector
    {

        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainWindow.xaml line 21
                {
                    this.BatteryReportPanel = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.StackPanel>(target);
                }
                break;
            case 3: // MainWindow.xaml line 16
                {
                    this.GetBatteryReportButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.Button>(target);
                    ((global::Microsoft.UI.Xaml.Controls.Button)this.GetBatteryReportButton).Click += this.GetBatteryReport;
                }
                break;
            case 4: // MainWindow.xaml line 12
                {
                    this.CycleButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.RadioButton>(target);
                }
                break;
            case 5: // MainWindow.xaml line 13
                {
                    this.RechargeButton = global::WinRT.CastExtensions.As<global::Microsoft.UI.Xaml.Controls.RadioButton>(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.UI.Xaml.Markup.Compiler"," 1.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Microsoft.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Microsoft.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

