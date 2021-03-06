﻿using System;
using System.Windows;
using System.Reflection;
using System.Windows.Controls;
using Dashboard.Resources;
using System.Xml;

namespace Dashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        ///  Gets the version of the Assembely information for the Appliation
        /// </summary>
        private string GetApplicationVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
        /// <summary>
        /// Gets the time of which the application was compiled
        /// </summary>
        /// <returns></returns>
        private string GetCompileTime()
        {
            return CiInfo.BuildTag;
        }
        /// <summary>
        /// When the application is loaded for the first time
        /// </summary>
        /// <param name="sender">Stuff</param>
        /// <param name="e">More stuff</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Logging.InitLogging(this);
            Logging.LogConsole("/----------------------------------------------------------------------------------------------------------------------------------\\");
            Logging.LogConsole(string.Format("Dashboard Version {0}", GetApplicationVersion()));
            Logging.LogConsole("Built on " + GetCompileTime());
            Logging.LogConsole("Initializing network connections");
            Logging.LogRobot("/----------------------------------------------------------------------------------------------------------------------------------\\");
            NetworkUtils.InitComms(this);
        }
        /// <summary>
        /// When The application is closed
        /// </summary>
        /// <param name="sender">Stuff</param>
        /// <param name="e">Even more stuff</param>
        private void Window_Closed(object sender, EventArgs e)
        {
            Logging.LogConsole("Application Closing");
            Logging.LogConsole("\\----------------------------------------------------------------------------------------------------------------------------------/");
            Logging.LogRobot("\\----------------------------------------------------------------------------------------------------------------------------------/");
        }
        /// <summary>
        /// When the Robot Log output text is changed. Event used to scrolling to see the last entry in the log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RobotLogOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            RobotLogOutput.ScrollToEnd();
        }
        /// <summary>
        /// When the Robot Log output text is changed. Event used to scrolling to see the last entry in the log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConsoleLogOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConsoleLogOutput.ScrollToEnd();
        }
        /// <summary>
        /// On click of the button to clear the dashboard log output
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearDashboardLogOutput_Click(object sender, RoutedEventArgs e)
        {
            ConsoleLogOutput.Clear();
            Logging.LogConsole("Console log cleared");
        }
        /// <summary>
        /// On lcick of button to clear dashboard log text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteDashboardLogFile_Click(object sender, RoutedEventArgs e)
        {
            Logging.ClearConsoleLogFile();
        }
        /// <summary>
        /// On click of button to clear the robot log output
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearRobotLogOutput_Click(object sender, RoutedEventArgs e)
        {
            RobotLogOutput.Clear();
            Logging.LogRobot("Robot log cleared");
        }
        /// <summary>
        /// On click of button to clear the robot log text file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteRobotLogFile_Click(object sender, RoutedEventArgs e)
        {
            Logging.ClearRobotLogFile();
        }
        /// <summary>
        /// On click of button to start the process of resetting the network communication
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetNetworkConnection_Click(object sender, RoutedEventArgs e)
        {
            Logging.LogConsole("Resetting network connections...");
            if (NetworkUtils.ConnectionManager == null)
            {
                NetworkUtils.Disconnect();
                NetworkUtils.InitComms(null);
            }
            else
            { 
                NetworkUtils.ConnectionManager.CancelAsync();
            }
        }
        /// <summary>
        /// On putton press to acivate the button press events of clearing both log displays
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBothlogDisplays_Click(object sender, RoutedEventArgs e)
        {
            ClearRobotLogOutput_Click(null, null);
            ClearDashboardLogOutput_Click(null, null);
        }
        /// <summary>
        /// Used for when the network system has parsed diagnostic data from the robot
        /// </summary>
        /// <param name="data">The diagnostic data send from the robot</param>
        public void OnDiagnosticData(string[] data)
        {
            int i = 0;
            ControlStatus.Text = data[i++];
            RobotStatus.Text = data[i++];
            RobotAutoStatus.Text = data[i++];
            Channel0Data.Text = data[i++];
            Channel1Data.Text = data[i++];
            Channel2Data.Text = data[i++];
            Channel3Data.Text = data[i++];
            Channel4Data.Text = data[i++];
            Channel5Data.Text = data[i++];
            Channel6Data.Text = data[i++];
            Channel7Data.Text = data[i++];
            LeftDriveSign.Text = data[i++];
            LeftDriveMag.Text = data[i++];
            LeftDriveEncoder.Text = data[i++];
            RightDriveSign.Text = data[i++];
            RightDriveMag.Text = data[i++];
            RightDriveEncoder.Text = data[i++];
            Battery1Volts.Text = data[i++];
            Battery1Amps.Text = data[i++];
            Battery2Volts.Text = data[i++];
            Battery2Amps.Text = data[i++];
            AccelX.Text = data[i++];
            AccelY.Text = data[i++];
            AccelZ.Text = data[i++];
            GyroX.Text = data[i++];
            GyroY.Text = data[i++];
            GyroZ.Text = data[i++];
            AugerRelay.Text = data[i++];
            ImpellerRelay.Text = data[i++];
            VelocityX.Text = data[i++];
            VelocityY.Text = data[i++];
            VelocityZ.Text = data[i++];
            RotationX.Text = data[i++];
            RotationY.Text = data[i++];
            RotationZ.Text = data[i++];
            Tempature_MPU.Text = data[i++];
            SideWallDetect.Text = data[i++];
            FrontWallDetect.Text = data[i++];
            ProximityDistance.Text = data[i++];
            PositionX.Text = data[i++];
            PositionY.Text = data[i++];
            PositionZ.Text = data[i++];
            //and log it to disk
            Logging.WriteDataLogEntry(data);
        }
        /// <summary>
        /// When xml mapping data is sent from the robot. Is parsed in the UI
        /// </summary>
        /// <param name="xmlData">The string of direct XML data</param>
        public void OnXMLMapData(string xmlData)
        {
            XmlDocument doc = new XmlDocument();
            bool parseXmlPass = false;
            try
            {
                doc.LoadXml(xmlData);
                parseXmlPass = true;
            }
            catch(XmlException xmle)
            {
                Logging.LogConsole("ERROR: failed to parse xml document: \n" + xmle.ToString());
            }
            if (!parseXmlPass)
                return;
            Logging.LogConsole("Parsing xml document");
            if(WorkAreaTangle.Visibility != Visibility.Visible)
            {
                WorkAreaTangle.Visibility = Visibility.Visible;
            }
            //get the attributes
            XmlNode map = doc.SelectSingleNode("//Map");
            foreach(XmlAttribute attribute in map.Attributes)
            {
                switch(attribute.Name)
                {
                    case "Height":
                        if(!string.IsNullOrWhiteSpace(attribute.Value))
                            WorkAreaTangle.Height = float.Parse(attribute.Value);
                        break;
                    case "Width":
                        if (!string.IsNullOrWhiteSpace(attribute.Value))
                            WorkAreaTangle.Height = float.Parse(attribute.Value);
                        break;
                    case "RobotPositionX":
                        if (!string.IsNullOrWhiteSpace(attribute.Value))
                        {
                            //TODO
                        }
                        break;
                    case "RobotPositionY":
                        if (!string.IsNullOrWhiteSpace(attribute.Value))
                        {
                            //TODO
                        }
                        break;
                    default:
                        Logging.LogConsole("ERROR: unknown xml attribute: " + attribute.Name);
                        break;
                }
            }
            XmlNode obstructions = doc.SelectSingleNode("//Map/Obstructions");
            foreach(XmlElement element in obstructions.ChildNodes)
            {
                //TODO
            }
            Logging.LogConsole("Parsing xml document complete");
        }
        /// <summary>
        /// On click on checkbox when the user wants to request manual debug control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManualControlToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (!NetworkUtils.ConnectionLive)
                return;
            Logging.LogConsole("Starting Manual Control");
            ControlSystem.StartControl(this);
        }
        /// <summary>
        /// On click on checkbox when user wants to release manual debug contorl, back to automated system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManualControlToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!NetworkUtils.ConnectionLive)
                return;
            //send stop of manual control
            ControlSystem.StopControl();
            ControlSystem.FirstJoystickMoveMent = true;
            OutputLeft.Text = "";
            OutputRight.Text = "";
            OutputMotor.Text = "";
        }
        /// <summary>
        /// On selection change from the combox for selecting which joystick to use
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Joysticks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!NetworkUtils.ConnectionLive)
                return;
            if (Joysticks.SelectedIndex == -1)
                return;
            Logging.LogConsole("Init joystick index " + Joysticks.SelectedIndex);
            ControlSystem.EnableManualJoystickControl(Joysticks.SelectedIndex);
        }
        /// <summary>
        /// On enable click (check) of the checkbox to enable joystick control (over-ride the button or keyboard contorls)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JoystickToggle_Checked(object sender, RoutedEventArgs e)
        {
            if (!NetworkUtils.ConnectionLive)
                return;
            //clear the list
            Joysticks.Items.Clear();
            ControlSystem.joystickDriveneable = true;
            ControlSystem.InitManualJoystickControl(this);
        }
        /// <summary>
        /// On disable click (uncheck) of the checkbox to disable jostick control (back to keyboard or buttons)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JoystickToggle_Unchecked(object sender, RoutedEventArgs e)
        {
            if (!NetworkUtils.ConnectionLive)
                return;
            Joysticks.SelectedIndex = -1;
            ControlSystem.joystickDriveneable = false;
            ControlSystem.StopJoystickControl();
        }
        /// <summary>
        /// Sends a request message to the robot to shut down after a specified timeout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RequestShutdownButton_Click(object sender, RoutedEventArgs e)
        {
            if(!NetworkUtils.ConnectionLive)
            {
                Logging.LogConsole("ERROR: cannont shutdown request when no connection");
            }
            if(string.IsNullOrWhiteSpace(SecondsDelay.Text))
            {
                Logging.LogConsole("ERROR: cannot start shutdown when no timeout given");
                return;
            }
            if (int.TryParse(SecondsDelay.Text, out int timeout))
            {
                if(timeout < 10)
                {
                    Logging.LogConsole(string.Format("ERROR: invalid shutdown timeout: {0}. Timeout must be positive number greator than 10", timeout.ToString()));
                    return;
                }
                NetworkUtils.SendRobotMesage(NetworkUtils.MessageType.Control, "Shutdown," + timeout.ToString());
                Logging.LogConsole(string.Format("Sent Shutdown command for {0} seconds delay",timeout.ToString()));
            }
            else
            {
                Logging.LogConsole("ERROR: failed to parse shutdown timout value: " + SecondsDelay.Text);
            }
        }
        /// <summary>
        /// Sends a request message to the robot to reboot after a specified timeout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RequestRebootButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NetworkUtils.ConnectionLive)
            {
                Logging.LogConsole("ERROR: cannont reboot request when no connection");
            }
            if (string.IsNullOrWhiteSpace(SecondsDelay.Text))
            {
                Logging.LogConsole("ERROR: cannot start reboot when no timeout given");
                return;
            }
            if (int.TryParse(SecondsDelay.Text, out int timeout))
            {
                if (timeout < 10)
                {
                    Logging.LogConsole(string.Format("ERROR: invalid reboot timeout: {0}. Timeout must be positive number greator than 10", timeout.ToString()));
                    return;
                }
                NetworkUtils.SendRobotMesage(NetworkUtils.MessageType.Control, "Reboot," + timeout.ToString());
                Logging.LogConsole(string.Format("Sent Reboot command for {0} seconds delay", timeout.ToString()));
            }
            else
            {
                Logging.LogConsole("ERROR: failed to parse reboot timout value: " + SecondsDelay.Text);
            }
        }
        /// <summary>
        /// Sends a request message to cancel a pending shutdown, if one currently exists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelShutdownRestartButton_Click(object sender, RoutedEventArgs e)
        {
            if (!NetworkUtils.ConnectionLive)
            {
                Logging.LogConsole("ERROR: cannont shutdown request when no connection");
            }
            NetworkUtils.SendRobotMesage(NetworkUtils.MessageType.Control, "Cancel_Shutdown");
        }
    }
}
