using System;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyLibrary
{
    /// <summary>
    /// Presents Processes wich related to a timer.
    /// </summary>
    public static class TimerEvents
    {
        static int Counter, SCounter;
        static TextBox[] DTexts = new TextBox[5];
        static ComboBox DCombo;

        static bool OutPStretch, InvoPStretch, InPView, InSPView, Timing, SameView;

        static DispatcherTimer MainTimer = new DispatcherTimer();

        static Border Active;
        static List<Border> DeActive = new List<Border>();

        static Border OutPanel;
        static Grid AcGrid, DeAcGrid, SameGrid;
        static StackPanel StoreSP, InvoPanel;

        /// <summary>
        /// The main entry to Timer Events.
        /// </summary>
        public static void SetTimerEvents()
        {                   
            MainTimer.Tick += MainTimer_Tick;
            MainTimer.Interval = new TimeSpan(10000);
            MainTimer.Start();
        }

        /// <summary>
        /// Stretching a Border.
        /// </summary>
        /// <param name="Grid">The Grid To Stretch</param>
        public static void StretchGrid(Border ToStretch)
        {
            OutPanel = ToStretch;
            OutPanel.Visibility = Visibility.Visible;
            OutPanel.Width = 0;
            OutPStretch = true;
        }

        /// <summary>
        /// Stretching an Invo Panel.
        /// </summary>
        /// <param name="Grid">The Grid To Stretch</param>
        public static void StretchPanel(StackPanel ToStretch)
        {
            InvoPanel = ToStretch;
            InvoPStretch = true;
        }

        /// <summary>
        /// Brighten the focused button.
        /// </summary>
        /// <param name="Button">The Button To Activate</param>
        public static void ActiveBtn(Border ToAct)
        {
            Active = ToAct;
            DeActive.RemoveAll(RAct => RAct == Active);
        }

        /// <summary>
        /// Return the button state.
        /// </summary>
        /// <param name="Button">The Button To Deactivate</param>
        public static void DeActiveBtn(Border ToDeAct)
        {
            if (ToDeAct == Active) Active = null;
            DeActive.Add(ToDeAct);
        }

        /// <summary>
        /// Changing Active Grid.
        /// </summary>
        /// <param name="Grid">The Active Grid</param>
        public static void ChangeView(Grid AcG)
        {
            AcGrid = AcG;
            AcGrid.Visibility = Visibility.Visible;
            InPView = true;
        }

        /// <summary>
        /// Flashing Active Grid.
        /// </summary>
        /// <param name="Grid">The Active Grid</param>
        public static void ChangeView(Grid AcG, bool Same)
        {
            SameGrid = AcG;
            SameView = true;
        }

        /// <summary>
        /// Changing Active StackPanel.
        /// </summary>
        /// <param name="ASP">The Active StackPanel</param>
        public static void ChangeView(StackPanel ASP)
        {
            StoreSP = ASP;
            StoreSP.Opacity = 0;
            StoreSP.Visibility = Visibility.Visible;
            InSPView = true;
        }

        /// <summary>
        /// Setting the time of the day.
        /// </summary>
        public static void SetTime(TextBox Year, TextBox Mon, TextBox Day, TextBox Hour, TextBox Min, ComboBox Comb)
        {
            Counter = 1800;
            DTexts[0] = Year; DTexts[1] = Mon; DTexts[2] = Day; DTexts[3] = Hour; DTexts[4] = Min; DCombo = Comb;
            Timing = true;
        }


        public static void EndTimeSetting()
        {
            Timing = false;
        }

        //---------------------------------------------------------------------------------+
        //  Timer Event ...                                                                |
        //---------------------------------------------------------------------------------+
        private static void MainTimer_Tick(object sender, EventArgs e)
        {
            //---------------------------------------------------------------------------------+
            //  Setting today's time ...                                                       |
            //---------------------------------------------------------------------------------+
            if (Timing)
            {
                Counter++;
                if (Counter > 180)
                {
                    DTexts[0].Text = DateTime.Now.Year.ToString();
                    DTexts[1].Text = DateTime.Now.Month.ToString();
                    DTexts[2].Text = DateTime.Now.Day.ToString();
                    DTexts[4].Text = DateTime.Now.Minute.ToString();

                    int H = DateTime.Now.Hour;
                    if (H >= 12)
                    {
                        H -= 12;
                        DTexts[3].Text = H.ToString();
                        DCombo.Text = "Pm";
                    }
                    else
                    {
                        DTexts[3].Text = H.ToString();
                        DCombo.Text = "Am";
                    }

                    Counter = 0;
                }
            }

            //---------------------------------------------------------------------------------+
            //  Brighten Buttons ...                                                           |
            //---------------------------------------------------------------------------------+
            if (Active != null)
            {
                BtnBlow(true, Active);

                byte Num3;
                if (Active.Name == "ExitBtn") Num3 = 75;
                else Num3 = 195;
                if (!(((SolidColorBrush)Active.Background).Color.B > Num3)) Active = null;
            }
            if (DeActive.Count != 0)
            {
                foreach (Border ActB in DeActive) BtnBlow(false, ActB);
                DeActive.RemoveAll(RAct => ((SolidColorBrush)(RAct.Background)).Color.B == 255);
            }


            //---------------------------------------------------------------------------------+
            //  Stretching Border ...                                                          |
            //---------------------------------------------------------------------------------+
            if (OutPStretch)
            {
                OutPanel.Width += 10;
                if (OutPanel.Width == 110) OutPStretch = false;
            }

            //---------------------------------------------------------------------------------+
            //  Stretching Invo Border ...                                                     |
            //---------------------------------------------------------------------------------+
            if (InvoPStretch)
            {
                InvoPanel.Height += 7.5;
                if (InvoPanel.Height == 100) InvoPStretch = false;
            }

            //---------------------------------------------------------------------------------+
            //  Changing Panels ...                                                            |
            //---------------------------------------------------------------------------------+
            if (InPView)
            {
                if (AcGrid != DeAcGrid)
                {
                    AcGrid.Opacity += 0.1;
                    if (DeAcGrid != null) DeAcGrid.Opacity -= 0.1;
                    if (AcGrid.Opacity > 1)
                    {
                        InPView = false;
                        if (DeAcGrid != null) DeAcGrid.Visibility = Visibility.Hidden;
                        DeAcGrid = AcGrid;
                    }
                }
            }

            //---------------------------------------------------------------------------------+
            //  Flashing Panels ...                                                            |
            //---------------------------------------------------------------------------------+
            if (SameView)
            {
                if (SCounter < 10) SameGrid.Opacity -= 0.1;
                else
                {
                    if (SameGrid.Opacity < 1) SameGrid.Opacity += 0.1;
                    else { SCounter = 0; SameView = false; }
                }
                SCounter++;
            }

            //---------------------------------------------------------------------------------+
            //  Changing SPanels ...                                                           |
            //---------------------------------------------------------------------------------+
            if (InSPView)
            {
                StoreSP.Opacity += 0.1;
                if (StoreSP.Opacity > 1) InSPView = false;
            }
        }

        //---------------------------------------------------------------------------------+
        //  Brighten Buttons method ...                                                    |
        //---------------------------------------------------------------------------------+
        private static void BtnBlow(bool Blowing, Border OnBlowing)
        {
            byte CNum, InNum, DeNum, Num1, Num2;

            if (OnBlowing.Name == "ExitBtn") Num1 = 15;
            else Num1 = 5;

            if (OnBlowing.Background != null)
            {
                InNum = (byte)(((SolidColorBrush)OnBlowing.Background).Color.B - Num1);
                DeNum = ((SolidColorBrush)OnBlowing.Background).Color.B;
                if (DeNum < 245) DeNum += Num1;
                else DeNum = 255;
            }
            else InNum = DeNum = 255;

            if (Blowing) CNum = InNum;
            else CNum = DeNum;

            if (OnBlowing.Name == "ExitBtn") Num2 = 255;
            else Num2 = CNum;

            OnBlowing.Background = new SolidColorBrush(Color.FromRgb(Num2, CNum, CNum));
        }

    }
}
