﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Threading.Tasks;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RobotCode
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Loaded += InitRobot;
        }

        private async void InitRobot(object sender, RoutedEventArgs e)
        {
            if(!Hardware.InitGPIO())
            {
                //https://stackoverflow.com/questions/32677597/how-to-exit-or-close-an-uwp-app-programmatically-windows-10
                Application.Current.Exit();
                return;
            }

            //init the robot networking
            if(!NetworkUtils.InitComms())
            {
                RobotController.SetRobotStatus(RobotStatus.Error);
                Application.Current.Exit();
                return;
            }

            //DEBUG: wait for dashboard logging connection
            if(NetworkUtils.DEBUG_FORCE_DASHBOARD_CONNECT)
            {
                while(!NetworkUtils.ConnectionLive)
                {
                    System.Threading.Thread.Sleep(100);
                }
                NetworkUtils.LogNetwork("dashboard connected via force wait", MessageType.Debug);
            }

            //encoders
            NetworkUtils.LogNetwork("Initializing Encoders", MessageType.Info);
            if(!Hardware.InitEncoders())
            {
                NetworkUtils.LogNetwork("Encoders Failed to initialize", MessageType.Error);
                RobotController.SetRobotStatus(RobotStatus.Error);
                Application.Current.Exit();
            }

            //proximity
            /*
            NetworkUtils.LogNetwork("Encoders initialized, loading proximity sensor", MessageType.Info);
            if(!Hardware.InitDistanceSensor())
            {
                NetworkUtils.LogNetwork("Proximity Failed to initialize", MessageType.Error);
                RobotController.SetRobotStatus(RobotStatus.Error);
                Application.Current.Exit();
            }*/

            //SPI/ADC
            NetworkUtils.LogNetwork("Proximity initialized, initializing SPI interface", MessageType.Info);
            //http://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
            //https://docs.microsoft.com/en-us/uwp/api/windows.devices.enumeration.deviceinformation.findallasync
            //https://stackoverflow.com/questions/33587832/prevent-winforms-ui-block-when-using-async-await
            if (! await Hardware.InitSPI())
            {
                NetworkUtils.LogNetwork("SPI failed to intialize", MessageType.Error);
                RobotController.SetRobotStatus(RobotStatus.Error);
                Application.Current.Exit();
                return;
            }

            //pwm
            NetworkUtils.LogNetwork("SPI Interface initialization complete, loading PWM", MessageType.Info);
            if (! await Hardware.InitPWM())
            {
                NetworkUtils.LogNetwork("PWM failed to intialize", MessageType.Error);
                RobotController.SetRobotStatus(RobotStatus.Error);
                Application.Current.Exit();
                return;
            }

            //I2c
            NetworkUtils.LogNetwork("PWM loading complete, I2C", MessageType.Info);
            if(! await Hardware.InitI2C())
            {
                NetworkUtils.LogNetwork("I2C Failed to Initialize", MessageType.Error);
                RobotController.SetRobotStatus(RobotStatus.Error);
                Application.Current.Exit();
            }

            //main control system
            NetworkUtils.LogNetwork("I2C loading complete, Initializing Main Robot controller", MessageType.Info);
            if (!RobotController.InitController(this.Dispatcher))
            {
                NetworkUtils.LogNetwork("Controller failed to intialize", MessageType.Error);
                RobotController.SetRobotStatus(RobotStatus.Error);
                Application.Current.Exit();
                return;
            }
            NetworkUtils.LogNetwork("Controller initialized, system online", MessageType.Info);
            RobotController.SystemOnline = true;
        }
    }
}
