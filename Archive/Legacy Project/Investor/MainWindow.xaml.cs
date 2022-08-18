using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using MyLibrary;
using System.IO;
using System.Security.AccessControl;

namespace Investor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //---------------------------------------------------------------------------------+
        //  Fields ...                                                                     |
        //---------------------------------------------------------------------------------+

        // Settings ...
        public static string Currency = "Dollars";

        // InterFace Fields ..
        public static double TotalVal;
        int OpNumber;

        // Dynamic Fields ..
        double Balance, GProfit;
        List<BType> BTypes = new List<BType>();
        List<Operation> Operations = new List<Operation>();
        List<Transfer> Transfers = new List<Transfer>();
        List<GTransfer> GTransfers = new List<GTransfer>();

        bool IsFresh;
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Data Read ...                                                                  |
        //---------------------------------------------------------------------------------+
        public void ReadData()
        {
            if (!Directory.Exists(@AppDomain.CurrentDomain.BaseDirectory + "\\Data"))
                Directory.CreateDirectory(@AppDomain.CurrentDomain.BaseDirectory + "\\Data");

            ReadData("BTypes.clg");
            ReadData("Brands.clg");
            ReadData("Operations.clg");
            ReadData("Transfers.clg");
            ReadData("GTransfers.clg");
            ReadData("Settings.clg");
            ReadData("Customers.clg");
        }

        public void ReadData(string Data)
        {
            string Path = @AppDomain.CurrentDomain.BaseDirectory + "\\Data\\" + Data;

            if (!File.Exists(Path)) using (StreamWriter sw = new StreamWriter(File.Create(Path))) { };

            FileInfo fInfo = new FileInfo(Path);
            fInfo.IsReadOnly = false;

            string[] Get = File.ReadAllLines(Path, Encoding.UTF8);
            int Len = 0;

            switch (Data)
            {
                case "BTypes.clg":
                    foreach (string Line in Get) BTypes.Add(new BType(Line));
                    break;
                case "Brands.clg":
                    Len = 9;
                    foreach (string Line in Get) if (Line.Split('$').Length == 9)
                            BTypes.Find(TTF => TTF.Name == Line.Split('$')[8]).Brands.Add(new Brand(Line));
                    break;
                case "Operations.clg":
                    Len = 14;
                    foreach (string Line in Get) if (Line.Split('$').Length == 14) Operations.Add(new Operation(Line));
                    break;
                case "Transfers.clg":
                    Len = 7;
                    foreach (string Line in Get) if (Line.Split('$').Length == 7) Transfers.Add(new Transfer(Line));
                    break;
                case "GTransfers.clg":
                    Len = 6;
                    foreach (string Line in Get) if (Line.Split('$').Length == 6) GTransfers.Add(new GTransfer(Line));
                    break;
                case "Settings.clg":
                    double.TryParse(Get[0], out Balance);
                    int.TryParse(Get[1], out OpNumber);
                    double.TryParse(Get[2], out GProfit);
                    break;
                case "Customers.clg":
                    foreach (string Line in Get) TCustomer.Items.Add(Line);
                    break;
            }
            if (Len != 0) foreach (string Line in Get) if (Line.Split('$').Length != Len) Get = Get.Where(LE => LE != Line).ToArray();

            File.WriteAllLines(Path, Get, Encoding.UTF8);

            fInfo.IsReadOnly = true;

        }

        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Data Write ...                                                                 |
        //---------------------------------------------------------------------------------+

        // Writes New Line ..
        private void WriteData(string Path, string Input)
        {
            FileInfo fInfo = new FileInfo(Path);
            fInfo.IsReadOnly = false;

            using (StreamWriter file = new StreamWriter(Path, true, Encoding.UTF8))
            { file.WriteLine(Input); }

            fInfo.IsReadOnly = true;
        }

        // Edits a Line ..
        private void EditData(string Path, string Old, string Input)
        {
            FileInfo fInfo = new FileInfo(Path);
            fInfo.IsReadOnly = false;

            string[] Get = File.ReadAllLines(Path, Encoding.UTF8);
            Get = Get.Where(LE => LE != Old).ToArray();
            if (Input != "")
            {
                List<string> Lst = Get.ToList();
                Lst.Add(Input);
                Get = Lst.ToArray();
            }
            File.WriteAllLines(Path, Get, Encoding.UTF8);

            fInfo.IsReadOnly = true;
        }

        enum Stings { Balance, OPID, GProfit }

        // Edit Settings ..
        private void EditSettings(Stings line, string Input)
        {
            string Path = @AppDomain.CurrentDomain.BaseDirectory + "\\Data\\Settings.clg";

            FileInfo fInfo = new FileInfo(Path);
            fInfo.IsReadOnly = false;

            string[] Get = File.ReadAllLines(Path, Encoding.UTF8);
            Get[(int)line] = Input;

            File.WriteAllLines(Path, Get, Encoding.UTF8);

            fInfo.IsReadOnly = true;
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Main Entry ...                                                                 |
        //---------------------------------------------------------------------------------+
        public MainWindow()
        {
            InitializeComponent();

            TimerEvents.SetTimerEvents();
            
            MyInterfaces.InterfaceSet(InvoPan, InvoRemove, InvoReturn);

            // Getting Start Values -----------------------------------------------------------+
            ReadData();

            // Setting Start Values -----------------------------------------------------------+
            foreach (BType T in BTypes)
            {
                BTypeCmb.Items.Add(T.Name);
                BTypeView.Items.Add(T.Name);
                TType.Items.Add(T.Name);
                SType.Items.Add(T.Name);

                StackPanel NewSP = new StackPanel();
                NewSP.Name = T.Name;
                NewSP.Visibility = Visibility.Hidden;
                NewSP.Height = 0;
                foreach (Brand B in T.Brands)
                {
                    if (B.Amount != 0)
                    {
                        NewSP.Children.Add(MyInterfaces.StoreGB(false, Currency, B));
                    }
                }

                StoreG.Children.Add(NewSP);
            }

            // Setting data Tabels ...
            foreach (Operation Op in Operations) InvoPan.Children.Add(MyInterfaces.InvoB(Op, Currency));

            int res;
            for (int i = 0; i < GTransfers.Count(); i++)
            {
                Math.DivRem(i, 2, out res);
                GTransPan.Children.Add(MyInterfaces.GTransG(GTransfers[i], res == 1));
            }

            for (int i = 0; i < Transfers.Count(); i++)
            {
                Math.DivRem(i, 2, out res);
                TransPan.Children.Add(MyInterfaces.TransG(Transfers[i], Currency, res == 1));
            }

            // Setting Start textes ...
            TrType.Items.Add("Bank");
            TrType.Items.Add("Add Deposit type ...");

            STotal.Text = SSTotal.Text = string.Format("{0:N2}", TotalVal);
            BalanceLbl.Text = TrBalanceLbl.Text = TBalanceLbl.Text = SBalanceLbl.Text = string.Format("{0:N2}", Balance);

            SCapital.Text = string.Format("{0:N2}", TotalVal + Balance);
            OpNum.Text = Operations.Count.ToString();

            SGProfit.Text = string.Format("{0:N2}", GProfit);

            ShowBData(false, false);

            // Setting Time ...
            TimerEvents.SetTime(TDateY, TDateMo, TDateD, TDateH, TDateM, TDateC);

            for (int i = 1; i < 32; i++)
            {
                TDDComb.Items.Add(i.ToString());
                if (i < 13) TDMComb.Items.Add(i.ToString());
            }
            TDDComb.SelectedIndex = TDMComb.SelectedIndex = BTypeView.SelectedIndex = 0;

            TDDComb.SelectedIndex = DateTime.Today.Day - 1;
            TDMComb.SelectedIndex = DateTime.Today.Month - 1;
            TDYTxt.Text = BYear.Text = DateTime.Today.Year.ToString();

            // Organizing Grids ...
            foreach (Grid InG in InPanel.Children) { InG.Visibility = Visibility.Hidden; InG.Opacity = 0; }
            StatGrid.Visibility = Visibility.Visible;
            StatGrid.Opacity = 1;
            IsFresh = true;
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Check For Nulls ...                                                            |
        //---------------------------------------------------------------------------------+
        public bool CheckNulls(object[] Controls)
        {
            bool Checking = true;
            foreach (object c in Controls) if (c == null) { Checking = false; break; }
            return Checking;
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Tool Bar Options ...                                                           |
        //---------------------------------------------------------------------------------+

        // Dragging Window
        private void DragWindow(object sender, MouseButtonEventArgs e)
        { DragMove(); }

        // Tool Bar Buttons
        private void ToolBtn_Click(object sender, RoutedEventArgs e)
        {
            Border CBtn = (Border)sender;
            switch (CBtn.Name)
            {
                case "ExitBtn": Application.Current.Shutdown(); break;
                case "MiniBtn": WindowState = WindowState.Minimized; break;
            }
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Texting Options ...                                                            |
        //---------------------------------------------------------------------------------+

        // Setting Scale Unit
        private void BScale_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (BScale.Text != "")
            {
                UPricelbl.Text = UPricelbl2.Text = BScale.Text + " :";
                UAmount.Text = BScale.Text;
            }
            else
            {
                UPricelbl.Text = UPricelbl2.Text = "Units :";
                UAmount.Text = "Units";
            }
        }

        // Limiting Numeric texts
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Checking for a Double value
        private void TextBox_LfDouble(object sender, RoutedEventArgs e)
        {
            TextBox Myone = (TextBox)sender;
            double MyTxt;
            if (!double.TryParse(Myone.Text, out MyTxt))
            {
                Myone.Text = "0.00";
                SystemSounds.Beep.Play();
            }
            else Myone.Text = string.Format("{0:N2}", MyTxt);
        }

        // Checking for a Int value
        private void TextBox_LfInt(object sender, RoutedEventArgs e)
        {
            TextBox Myone = (TextBox)sender;

            int MyTxt;
            if (!int.TryParse(Myone.Text, out MyTxt))
            {
                Myone.Text = "0";
                SystemSounds.Beep.Play();
            }
            else Myone.Text = string.Format("{0:N0}", MyTxt);
        }

        // Checking for a Double value isn't money.
        private void TextBox_LfNCurren(object sender, RoutedEventArgs e)
        {
            TextBox Myone = (TextBox)sender;
            double MyTxt;
            if (!double.TryParse(Myone.Text, out MyTxt))
            {
                Myone.Text = "0";
                SystemSounds.Beep.Play();
            }
        }

        // Lost Focus in Trading Panel.
        private void TradingLF(object sender, RoutedEventArgs e)
        {
            if (!CheckNulls(new object[] { TPriceA, TTotalLbl, TAmount, TPriceV })) return;

            TextBox Myone = (TextBox)sender;
            string V;
            bool Edit;

            if (Myone.Name == "TAmount" || Myone.Name == "TPriceA") { V = "0"; Edit = false; }
            else { V = "0.00"; Edit = true; }

            double MyTxt;
            if (!double.TryParse(Myone.Text, out MyTxt))
            {
                Myone.Text = V;
                SystemSounds.Beep.Play();
            }
            else if (Edit) Myone.Text = string.Format("{0:N2}", MyTxt);
        }

        // Change Text in Trading Panel.
        private void TradingCT(object sender, TextChangedEventArgs e)
        {
            if (!CheckNulls(new object[] { TPriceA, TTotalLbl, TAmount, TPriceV })) return;

            if (double.Parse(TPriceA.Text) != 0)
                TTotalLbl.Text = string.Format("{0:N2}", ((double.Parse(TAmount.Text) / double.Parse(TPriceA.Text)) * double.Parse(TPriceV.Text)));
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Special Interface Options ...                                                  |
        //---------------------------------------------------------------------------------+

        // Check Box Options
        private void ChkBx_Click(object sender, RoutedEventArgs e)
        {
            CheckBox MyOne = sender as CheckBox;
            switch (MyOne.Name)
            {
                case "IsSetted":
                    TPriceGrid.IsEnabled = IsAmountSetted.IsEnabled = MyOne.IsChecked == true;
                    if (MyOne.IsChecked == false) { IsAmountSetted.IsChecked = false; BAmount.IsEnabled = false; BAmount.Text = "0"; }
                    break;
                case "IsSellSetted": SPriceGrid.IsEnabled = MyOne.IsChecked == true; break;
                case "IsAmountSetted": BAmount.IsEnabled = MyOne.IsChecked == true; BAmount.Text = "0"; break;
                case "IsNow":
                    DT1.IsEnabled = DT2.IsEnabled = !(MyOne.IsChecked == true);
                    if (MyOne.IsChecked == true) TimerEvents.SetTime(TDateY, TDateMo, TDateD, TDateH, TDateM, TDateC);
                    else TimerEvents.EndTimeSetting();
                    break;
                case "IsNtype":
                    if (MyOne.IsChecked == true)
                    { BTypeTxt.Visibility = Visibility.Visible; BTypeCmb.Visibility = Visibility.Hidden; }
                    else
                    { BTypeTxt.Visibility = Visibility.Hidden; BTypeCmb.Visibility = Visibility.Visible; }
                    break;
                case "IsScaleSetted":
                    BScale.IsEnabled = MyOne.IsChecked == true;
                    if (MyOne.IsChecked == true)
                    {
                        BTPriceA.Text = BSPriceA.Text = "0";
                        BTPriceA.Visibility = BSPriceA.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        BScale.Text = "Unit";
                        BTPriceA.Text = BSPriceA.Text = "1";
                        BTPriceA.Visibility = BSPriceA.Visibility = Visibility.Collapsed;
                    }
                    break;
                case "IsPCustom":
                    TCustomer.IsEnabled = MyOne.IsChecked == true;
                    if (MyOne.IsChecked == true) TCustomer.Text = "";
                    else TCustomer.Text = "-";
                    break;
                case "IsTrNow":
                    TDDComb.IsEnabled = TDMComb.IsEnabled = TDYTxt.IsEnabled = MyOne.IsChecked == false;
                    if (MyOne.IsChecked == true)
                    {
                        TDDComb.SelectedIndex = DateTime.Today.Day - 1;
                        TDMComb.SelectedIndex = DateTime.Today.Month - 1;
                        TDYTxt.Text = DateTime.Today.Year.ToString();
                    }
                    break;
                case "IsAllTime":
                    BalanceTime.IsEnabled = MyOne.IsChecked == false;
                    if (MyOne.IsChecked != true) BalanceTime.Background = Brushes.White;
                    else
                    {
                        BalanceTime.Background = Brushes.WhiteSmoke;
                        ShowBData(false, false);
                    }
                    break;
            }
        }

        // Switched Buttons Visibility.
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpGroup.Visibility = AcGroup.Visibility = InventGroup.Visibility = Visibility.Hidden;
        }

        // Store Type Selected.
        private void BTypeView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BTypeView.SelectedItem != null)
            {
                foreach (StackPanel S in StoreG.Children) { S.Visibility = Visibility.Hidden; S.Height = 0; }
                foreach (StackPanel SP in StoreG.Children)
                    if (SP.Name == BTypeView.SelectedItem.ToString()) { SP.Height = double.NaN; TimerEvents.ChangeView(SP); break; }
            }
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Active / Deactive Buttons ...                                                  |
        //---------------------------------------------------------------------------------+

        // Active
        private void InterBtn_MouseEnter(object sender, MouseEventArgs e)
        { TimerEvents.ActiveBtn(sender as Border); }

        // Deactive
        private void InterBtn_MouseLeave(object sender, MouseEventArgs e)
        { TimerEvents.DeActiveBtn(sender as Border); }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Switching Grids ...                                                            |
        //---------------------------------------------------------------------------------+

        // Switching Primary Grids
        private void InBtn_Click(object sender, RoutedEventArgs e)
        {
            if (IsFresh) { StatGrid.Opacity = 0; IsFresh = false; }

            Border CBtn = (Border)sender;
            OpGroup.Visibility = AcGroup.Visibility = InventGroup.Visibility = Visibility.Hidden;

            switch (CBtn.Name)
            {
                case "TradeBtn":
                    if (TradeGrid.Visibility == Visibility.Visible) TimerEvents.ChangeView(TradeGrid, true);
                    else TimerEvents.ChangeView(TradeGrid);
                    TOrder.Items.Clear();
                    TOrder.Items.Add("Purchase");
                    TOrder.Items.Add("Sell");
                    TOrder.SelectedIndex = 0;
                    IsNewPrice.Visibility = Visibility.Visible;
                    IsNow.Content = "Trade Now";
                    ConOBtn.Content = "Confirm Order";
                    break;
                case "AddBtn": TimerEvents.ChangeView(AddGrid); break;
                case "ViewBtn": TimerEvents.ChangeView(BrandGrid); break;
                case "TransBtn":
                    TimerEvents.ChangeView(TransGrid);
                    TDDComb.SelectedIndex = DateTime.Today.Day - 1;
                    TDMComb.SelectedIndex = DateTime.Today.Month - 1;
                    TDYTxt.Text = DateTime.Today.Year.ToString();
                    break;
                case "BalanBtn":
                    ShowBData(IsAllTime.IsChecked == false, (IsAllTime.IsChecked == false && BMonth.SelectedIndex != 0));
                    TimerEvents.ChangeView(BalanGrid);
                    break;
                case "ReturnBtn":
                    if (TradeGrid.Visibility == Visibility.Visible) TimerEvents.ChangeView(TradeGrid, true);
                    else TimerEvents.ChangeView(TradeGrid);
                    TOrder.Items.Clear();
                    TOrder.Items.Add("Return Purchases");
                    TOrder.Items.Add("Return Sales");
                    TOrder.SelectedIndex = 0;
                    IsNewPrice.Visibility = Visibility.Collapsed;
                    IsNow.Content = "Return Now";
                    ConOBtn.Content = "Confirm Return";
                    break;
                case "StatBtn":
                    TimerEvents.ChangeView(StatGrid);
                    break;
                case "InvoBtn":
                    TimerEvents.ChangeView(InvoGrid);
                    break;
                case "TrnBtn":
                    TimerEvents.ChangeView(GTransGrid);
                    break;
            }
        }

        // Switching Secondary Grids
        private void OutBtn_Click(object sender, RoutedEventArgs e)
        {
            Border CBtn = (Border)sender;
            bool[] IsV = new bool[3];

            IsV[0] = OpGroup.Visibility == Visibility.Visible;
            IsV[1] = AcGroup.Visibility == Visibility.Visible;
            IsV[2] = InventGroup.Visibility == Visibility.Visible;

            OpGroup.Visibility = AcGroup.Visibility = InventGroup.Visibility = Visibility.Hidden;
            switch (CBtn.Name)
            {
                case "OpBtn": if (!IsV[0]) TimerEvents.StretchGrid(OpGroup); break;
                case "AcBtn": if (!IsV[1]) TimerEvents.StretchGrid(AcGroup); break;
                case "InventBtn": if (!IsV[2]) TimerEvents.StretchGrid(InventGroup); break;
            }
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Goods Transfer Options ...                                                     |
        //---------------------------------------------------------------------------------+

        // Change Selected Brand Type ...
        private void SType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SType.SelectedItem != null)
            {
                SBrand.Items.Clear();
                BType FType = BTypes.Find(TTF => TTF.Name == SType.SelectedItem.ToString());
                foreach (Brand B in FType.Brands) SBrand.Items.Add(B.Name);
            }
        }

        // Change Selected Brand ...
        private void SBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CheckNulls(new object[] { SType, SBrand })) return;
            if (!CheckNulls(new object[] { SType.SelectedItem, SBrand.SelectedItem })) return;

            BType FType = BTypes.Find(TTF => TTF.Name == SType.SelectedItem.ToString());
            Brand FBrand = FType.Brands.Find(BTF => BTF.Name == SBrand.SelectedItem.ToString());

            SScale0.Text = SScale1.Text = FBrand.ScaleUnit;

            SStore.Text = FBrand.Amount.ToString();
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Trade Options ...                                                              |
        //---------------------------------------------------------------------------------+

        // Trade Type Selected.
        private void TType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TType.SelectedItem != null)
            {
                TBrand.Items.Clear();
                BType FType = BTypes.Find(TTF => TTF.Name == TType.SelectedItem.ToString());
                foreach (Brand B in FType.Brands) TBrand.Items.Add(B.Name);
            }
        }

        // Trade Brand Selected.
        private void TBrand_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CheckNulls(new object[] { TType, TBrand })) return;
            if (!CheckNulls(new object[] { TType.SelectedItem, TBrand.SelectedItem })) return;

            BType FType = BTypes.Find(TTF => TTF.Name == TType.SelectedItem.ToString());
            Brand FBrand = FType.Brands.Find(BTF => BTF.Name == TBrand.SelectedItem.ToString());

            TScale0.Text = TScale1.Text = FBrand.ScaleUnit;
            TScale2.Text = FBrand.ScaleUnit + " : ";

            TStore.Text = FBrand.Amount.ToString();

            if (TOrder.SelectedIndex == 0)
            {
                if (FBrand.TradePrice != null)
                {
                    TPriceA.Text = FBrand.TradePrice.Amount.ToString();
                    TPriceV.Text = string.Format("{0:N2}", FBrand.TradePrice.Value);
                }
            }
            else
            {
                if (FBrand.SellPrice != null)
                {
                    TPriceA.Text = FBrand.SellPrice.Amount.ToString();
                    TPriceV.Text = string.Format("{0:N2}", FBrand.SellPrice.Value);
                }
            }
        }

        // Trade Order Selected.
        private void TOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsNewPrice != null)
            {
                if (TOrder.SelectedIndex == 0) IsNewPrice.Content = "Set as a Trade price";
                else IsNewPrice.Content = "Set as a Sell price";
            }

            if (!CheckNulls(new object[] { TType, TBrand, TPriceA, TPriceV })) return;
            if (!CheckNulls(new object[] { TType.SelectedItem, TBrand.SelectedItem })) return;

            BType FType = BTypes.Find(TTF => TTF.Name == TType.SelectedItem.ToString());
            Brand FBrand = FType.Brands.Find(BTF => BTF.Name == TBrand.SelectedItem.ToString());

            if (TOrder.SelectedIndex == 0)
            {
                if (FBrand.TradePrice != null)
                {
                    TPriceA.Text = FBrand.TradePrice.Amount.ToString();
                    TPriceV.Text = string.Format("{0:N2}", FBrand.TradePrice.Value);
                }
            }
            else
            {
                if (FBrand.SellPrice != null)
                {
                    TPriceA.Text = FBrand.SellPrice.Amount.ToString();
                    TPriceV.Text = string.Format("{0:N2}", FBrand.SellPrice.Value);
                }
            }
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Confirm Button ...                                                             |
        //---------------------------------------------------------------------------------+
        private void ConTOrder_Click(object sender, RoutedEventArgs e)
        {
            // Check Inputs -------------------------------------------------------------------+
            if (TType.SelectedItem == null || TBrand.SelectedItem == null || TAmount.Text == "" || TPriceA.Text == "" || TPriceV.Text == "") goto Invailed;
            if (TDateD.Text == "0" || TDateMo.Text == "0" || TDateY.Text == "0") goto Invailed;

            bool IsRtrn;
            Trading Type;

            if (TOrder.Text == "Purchase") { Type = Trading.Purchase; IsRtrn = false; }
            else if (TOrder.Text == "Sell") { Type = Trading.Sell; IsRtrn = false; }
            else if (TOrder.Text == "Return Purchases") { Type = Trading.RtrnPurch; IsRtrn = true; }
            else { Type = Trading.RtrnSales; IsRtrn = true; }

            Operation NewOp = new Operation();

            BType FBType = BTypes.Find(TTF => TTF.Name == TType.SelectedItem.ToString());
            if (FBType == null) goto Invailed;
            Brand FBrand = FBType.Brands.Find(BTF => BTF.Name == TBrand.SelectedItem.ToString());
            if (FBrand == null) goto Invailed;
            //---------------------------------------------------------------------------------+


            // Read Inputs --------------------------------------------------------------------+
            NewOp.Customer = TCustomer.Text;

            NewOp.Brand.Name = FBrand.Name;
            NewOp.Brand.TName = FBType.Name;
            NewOp.Brand.Scale = FBrand.ScaleUnit;

            NewOp.Amount = double.Parse(TAmount.Text);
            NewOp.price = new Price(double.Parse(TPriceA.Text), double.Parse(TPriceV.Text));

            int Y = int.Parse(TDateY.Text);
            int Mo = int.Parse(TDateMo.Text);
            int D = int.Parse(TDateD.Text);
            int M = int.Parse(TDateM.Text);
            int H;
            if (TDateC.SelectedItem.ToString() == "Am") H = int.Parse(TDateH.Text);
            else H = int.Parse(TDateH.Text) + 12;

            NewOp.Time = new DateTime(Y, Mo, D, H, M, 0);
            NewOp.Type = Type;

            if (!IsRtrn)
            {
                if (Type == Trading.Purchase && IsNewPrice.IsChecked == true) FBrand.TradePrice = new Price(double.Parse(TPriceA.Text), double.Parse(TPriceV.Text));
                else if (Type == Trading.Sell && IsNewPrice.IsChecked == true) FBrand.SellPrice = new Price(double.Parse(TPriceA.Text), double.Parse(TPriceV.Text));
            }

            List<string> Itms = new List<string>();
            foreach (var item in TCustomer.Items) Itms.Add(item.ToString());
            if (TCustomer.SelectedItem == null && Itms.Find(ITF => ITF == NewOp.Customer) == null)
            {
                TCustomer.Items.Add(NewOp.Customer);
                WriteData(@AppDomain.CurrentDomain.BaseDirectory + "\\Data\\Customers.clg", NewOp.Customer);
            }
            //---------------------------------------------------------------------------------+


            // Write Inputs -------------------------------------------------------------------+
            bool TradeT;
            if (IsRtrn) TradeT = NewOp.Type == Trading.RtrnSales;
            else TradeT = NewOp.Type == Trading.Purchase;

            if (TradeT) if (NewOp.TotalPrice > Balance) { MessageBox.Show("Not enough Balance !!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }
                else if (NewOp.Amount > FBrand.Amount) { MessageBox.Show("Not enough Goods !!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }

            OpNumber++;
            NewOp.Number = OpNumber;
            Operations.Add(NewOp);
            InvoPan.Children.Add(MyInterfaces.InvoB(NewOp, Currency));

            WriteData(@AppDomain.CurrentDomain.BaseDirectory + "\\Data\\Operations.clg", NewOp.ToString());

            string Old = FBrand.ToString();

            if (TradeT)
            {
                Balance -= NewOp.TotalPrice;

                FBrand.Value += NewOp.TotalPrice;
                FBrand.Amount += NewOp.Amount;
            }
            else
            {
                Balance += NewOp.TotalPrice;

                FBrand.Value -= (FBrand.Value / FBrand.Amount) * NewOp.Amount;
                FBrand.Amount -= NewOp.Amount;
            }

            if (NewOp.Type == Trading.Sell)
                GProfit += ((NewOp.price.Value / NewOp.price.Amount) - (FBrand.TradePrice.Value / FBrand.TradePrice.Amount)) * NewOp.Amount;
            else if (NewOp.Type == Trading.RtrnSales)
                GProfit -= ((NewOp.price.Value / NewOp.price.Amount) - (FBrand.TradePrice.Value / FBrand.TradePrice.Amount)) * NewOp.Amount;
            

            MyInterfaces.EditStoreGB(StoreG, FBrand);
            EditData(@AppDomain.CurrentDomain.BaseDirectory + "\\Data\\Brands.clg", Old, FBrand.ToString());
            EditSettings(Stings.Balance, Balance.ToString());
            EditSettings(Stings.OPID, OpNumber.ToString());
            EditSettings(Stings.GProfit, GProfit.ToString());
            //---------------------------------------------------------------------------------+


            // Clear --------------------------------------------------------------------------+
            SGProfit.Text = string.Format("{0:N2}", GProfit);
            STotal.Text = SSTotal.Text = string.Format("{0:N2}", TotalVal);
            BalanceLbl.Text = TrBalanceLbl.Text = TBalanceLbl.Text = SBalanceLbl.Text = string.Format("{0:N2}", Balance);

            SCapital.Text = string.Format("{0:N2}", TotalVal + Balance);
            OpNum.Text = Operations.Count.ToString();

            TCustomer.Text = TType.Text = TBrand.Text = "";
            TAmount.Text = TPriceA.Text = TStore.Text = "0";
            TPriceV.Text = TTotalLbl.Text = "0.00";
            //---------------------------------------------------------------------------------+


            return;
            Invailed:
            MessageBox.Show("Invalid inputs", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Balance Show Void ...                                                          |
        //---------------------------------------------------------------------------------+
        private void ShowBData(bool Isyear, bool Ismonth)
        {
            bool IsTime = false;

            double TPursh, TSales, TProf, TRSales, TRPursh, TExpen;
            TPursh = TSales = TProf = TRSales = TRPursh = TExpen = 0;

            foreach (Operation MyOP in Operations)
            {
                if (!Isyear) IsTime = true;
                else if (!Ismonth) IsTime = MyOP.Time.Year == double.Parse(BYear.Text);
                else IsTime = (MyOP.Time.Year == double.Parse(BYear.Text) && MyOP.Time.Month == (BMonth.SelectedIndex));

                if (!IsTime) continue;
                else IsTime = false;

                if (MyOP.Type == Trading.Purchase) TPursh += MyOP.TotalPrice;
                else if (MyOP.Type == Trading.RtrnPurch) TRPursh += MyOP.TotalPrice;
                else if (MyOP.Type == Trading.RtrnSales)
                {
                    TRSales += MyOP.TotalPrice;

                    Brand FBrand = BTypes.Find(TTF => TTF.Name == MyOP.Brand.TName).Brands.Find(BTF => BTF.Name == MyOP.Brand.Name);
                    TProf -= ((MyOP.price.Value / MyOP.price.Amount) - (FBrand.TradePrice.Value / FBrand.TradePrice.Amount)) * MyOP.Amount;
                }
                else
                {
                    TSales += MyOP.TotalPrice;

                    Brand FBrand = BTypes.Find(TTF => TTF.Name == MyOP.Brand.TName).Brands.Find(BTF => BTF.Name == MyOP.Brand.Name);
                    TProf += ((MyOP.price.Value / MyOP.price.Amount) - (FBrand.TradePrice.Value / FBrand.TradePrice.Amount)) * MyOP.Amount;
                }

            }

            foreach (Transfer MyTr in Transfers)
            {
                if (!Isyear) IsTime = true;
                else if (!Ismonth) IsTime = MyTr.Date.Year == double.Parse(BYear.Text);
                else IsTime = (MyTr.Date.Year == double.Parse(BYear.Text) && MyTr.Date.Month == (BMonth.SelectedIndex + 1));

                if (!IsTime) continue;
                else IsTime = false;

                if (MyTr.order == Order.Expenses) TExpen += MyTr.Amount;
            }

            TPurshLbl.Text = string.Format("{0:N2}", TPursh);
            TSalesLbl.Text = string.Format("{0:N2}", TSales);
            TProfLbl.Text = string.Format("{0:N2}", TProf);
            TRSalesLbl.Text = string.Format("{0:N2}", TRSales);
            TRPurshLbl.Text = string.Format("{0:N2}", TRPursh);
            TExpenLbl.Text = string.Format("{0:N2}", TExpen);
            GProfLbl.Text = string.Format("{0:N2}", (TProf - TExpen));
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Balance Show Button ...                                                        |
        //---------------------------------------------------------------------------------+
        private void BShowBTn_Click(object sender, RoutedEventArgs e)
        {
            ShowBData(true, BMonth.SelectedIndex != 0);
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Transfer Options ...                                                           |
        //---------------------------------------------------------------------------------+
        private void TrOrder_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CheckNulls(new object[] { TrType, TrOrder })) return;

            TrType.Items.Clear();
            if (TrOrder.SelectedIndex == 0) { TrType.Items.Add("Bank"); TrType.Items.Add("Add Deposit type ..."); }
            else if (TrOrder.SelectedIndex == 1) { TrType.Items.Add("Bank"); TrType.Items.Add("Add Withdraw type ..."); }
            else TrType.Items.Add("Add Expenses type ...");
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Transfer Button ...                                                            |
        //---------------------------------------------------------------------------------+
        private void ConOrder_Click(object sender, RoutedEventArgs e)
        {
            // Check Inputs -------------------------------------------------------------------+
            if (TrType.SelectedItem == null || TrAmount.Text == "0" || TDYTxt.Text == "0") goto Invailed;

            int outval;
            if (!int.TryParse(TDYTxt.Text, out outval)) goto Invailed;

            if (STrOrder.SelectedIndex != 0) if (double.Parse(TrAmount.Text) > double.Parse(TrBalanceLbl.Text))
                { MessageBox.Show("Not enough Balance !!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }
            //---------------------------------------------------------------------------------+


            // Read Inputs --------------------------------------------------------------------+
            Transfer NewT = new Transfer
            {
                Type = TrType.SelectedItem.ToString(),
                Note = TrNote.Text,
                Amount = double.Parse(TrAmount.Text),

                Date = new DateTime(int.Parse(TDYTxt.Text), int.Parse(TDMComb.Text), int.Parse(TDDComb.Text))
            };

            if (TrOrder.SelectedIndex == 0) NewT.order = Order.Deposit;
            else if (TrOrder.SelectedIndex == 1) NewT.order = Order.Withdraw;
            else NewT.order = Order.Expenses;
            //---------------------------------------------------------------------------------+


            // Write Inputs -------------------------------------------------------------------+
            int res;
            Math.DivRem(Transfers.Count(), 2, out res);
            bool IsGray = res == 1;

            Transfers.Add(NewT);
            TransPan.Children.Add(MyInterfaces.TransG(NewT, Currency, IsGray));

            WriteData(@AppDomain.CurrentDomain.BaseDirectory + "\\Data\\Transfers.clg", NewT.ToString());

            if (NewT.order == Order.Deposit) Balance += double.Parse(TrAmount.Text);
            else Balance -= double.Parse(TrAmount.Text);

            if (NewT.order == Order.Expenses) GProfit -= NewT.Amount;

            EditSettings(Stings.Balance, Balance.ToString());
            EditSettings(Stings.GProfit, GProfit.ToString());
            //---------------------------------------------------------------------------------+


            // Clear --------------------------------------------------------------------------+
            SGProfit.Text = string.Format("{0:N2}", GProfit);
            BalanceLbl.Text = TrBalanceLbl.Text = TBalanceLbl.Text = SBalanceLbl.Text = string.Format("{0:N2}", Balance);

            SCapital.Text = string.Format("{0:N2}", TotalVal + Balance);

            TrType.Text = TrNote.Text = "";
            TrAmount.Text =  "0.00";
            //---------------------------------------------------------------------------------+

        
            return;
            Invailed:
            MessageBox.Show("Invalid inputs", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Add Brand Button ...                                                           |
        //---------------------------------------------------------------------------------+
        private void AddBrand_Click(object sender, RoutedEventArgs e)
        {
            bool IsValid = true;

            if (IsNtype.IsChecked == true)
            {
                if (BTypeTxt.Text == "") IsValid = false;
                else if (BTypes.Find(TTF => TTF.Name.ToLower() == BTypeTxt.Text.ToLower()) != null)
                { MessageBox.Show("There is a type has the same name you entered", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }
            }
            else { if (BTypeCmb.SelectedItem == null) IsValid = false; }
            if (BName.Text == "" || BScale.Text == "") IsValid = false;
            if (IsSetted.IsChecked == true) if (BTPriceA.Text == "0" || BTPriceA.Text == "0.00" || BTPriceV.Text == "0.00") IsValid = false;
            if (IsSellSetted.IsChecked == true) if (BSPriceA.Text == "0" || BSPriceA.Text == "0.00" || BSPriceV.Text == "0.00") IsValid = false;
            if (IsAmountSetted.IsChecked == true) if (BAmount.Text == "") IsValid = false;

            foreach (BType T in BTypes)
                if (T.Brands.Find(BTF => BTF.Name.ToLower() == BName.Text.ToLower()) != null)
                { MessageBox.Show("There is a brand has the same name you entered", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }

            if (IsValid)
            {
                Brand NewB = new Brand();

                NewB.Name = BName.Text;
                NewB.ScaleUnit = BScale.Text;

                if (IsSetted.IsChecked == true) NewB.TradePrice = new Price(double.Parse(BTPriceA.Text), double.Parse(BTPriceV.Text));
                else NewB.TradePrice = new Price(0, 0);

                if (IsSellSetted.IsChecked == true) NewB.SellPrice = new Price(double.Parse(BSPriceA.Text), double.Parse(BSPriceV.Text));
                else NewB.SellPrice = new Price(0, 0);

                if (IsNtype.IsChecked == true)
                {
                    List<Brand> NB = new List<Brand>();
                    NB.Add(NewB);
                    BTypes.Add(new BType(BTypeTxt.Text, NB));
                    NewB.Type = BTypes.Last();

                    StackPanel NewSP = new StackPanel();
                    NewSP.Name = BTypeTxt.Text;
                    NewSP.Visibility = Visibility.Hidden;
                    NewSP.Height = 0;
                    StoreG.Children.Add(NewSP);

                    BTypeCmb.Items.Add(BTypeTxt.Text);
                    BTypeView.Items.Add(BTypeTxt.Text);
                    TType.Items.Add(BTypeTxt.Text);
                    SType.Items.Add(BTypeTxt.Text);
                }
                else
                {
                    BType BT = BTypes.Find(BTF => BTF.Name == BTypeCmb.SelectedItem.ToString());
                    NewB.Type = BT;
                    BT.Brands.Add(NewB);
                }

                WriteData(@AppDomain.CurrentDomain.BaseDirectory + "\\Data\\BTypes.clg", NewB.Type.Name);
                WriteData(@AppDomain.CurrentDomain.BaseDirectory + "\\Data\\Brands.clg", NewB.ToString());

                if (IsAmountSetted.IsChecked == true)
                {
                    NewB.Amount = double.Parse(BAmount.Text);
                    NewB.Value = NewB.TradePrice.Value * (NewB.Amount / NewB.TradePrice.Amount);

                    foreach (StackPanel SP in StoreG.Children)
                        if (SP.Name == NewB.Type.Name)
                        {
                            SP.Children.Add(MyInterfaces.StoreGB(false, Currency, NewB));
                            STotal.Text = SSTotal.Text = string.Format("{0:N2}", TotalVal);
                            SCapital.Text = string.Format("{0:N2}", TotalVal + Balance);
                            break;
                        }
                }

                BTypeTxt.Text = BTypeCmb.Text = BName.Text = BScale.Text = "";
                BTPriceA.Text = BSPriceA.Text = BAmount.Text = "0";
                BTPriceV.Text = BSPriceV.Text = "0.00";
            }
            else MessageBox.Show("Invalid inputs", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Goods Transfer Buton ...                                                       |
        //---------------------------------------------------------------------------------+
        private void SOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            // Check Inputs -------------------------------------------------------------------+
            if (SType.SelectedItem == null || SBrand.SelectedItem == null || SAmount.Text == "0") goto Invailed;
            if (STrOrder.SelectedIndex == 1) if (double.Parse(SAmount.Text) > double.Parse(SStore.Text))
                { MessageBox.Show("Not enough Goods !!", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation); return; }

            BType FBType = BTypes.Find(TTF => TTF.Name == SType.SelectedItem.ToString());
            if (FBType == null) goto Invailed;
            Brand FBrand = FBType.Brands.Find(BTF => BTF.Name == SBrand.SelectedItem.ToString());
            if (FBrand == null) goto Invailed;
            //---------------------------------------------------------------------------------+


            // Read Inputs --------------------------------------------------------------------+
            GTransfer NewGT = new GTransfer();

            NewGT.Brand.TName = SType.Text;
            NewGT.Brand.Name = SBrand.Text;
            NewGT.Brand.Scale = SScale0.Text;
            NewGT.Amount = double.Parse(SAmount.Text);

            if (STrOrder.SelectedIndex == 0) NewGT.order = GOrder.MoveIn;
            else NewGT.order = GOrder.MoveOut;

            NewGT.Note = STrNote.Text;
            //---------------------------------------------------------------------------------+


            // Write Inputs -------------------------------------------------------------------+
            int res;
            Math.DivRem(GTransfers.Count(), 2, out res);
            bool IsGray = res == 1;

            GTransfers.Add(NewGT);
            GTransPan.Children.Add(MyInterfaces.GTransG(NewGT, IsGray));

            WriteData(@AppDomain.CurrentDomain.BaseDirectory + "\\Data\\GTransfers.clg", NewGT.ToString());

            string Old = FBrand.ToString();

            if (NewGT.order == GOrder.MoveIn)
            {
                FBrand.Amount += NewGT.Amount;
                FBrand.Value += NewGT.Amount * (FBrand.TradePrice.Value / FBrand.TradePrice.Amount);
            }
            else
            {
                FBrand.Amount -= NewGT.Amount;
                FBrand.Value -= NewGT.Amount * (FBrand.TradePrice.Value / FBrand.TradePrice.Amount);
            }
            MyInterfaces.EditStoreGB(StoreG, FBrand);
            EditData(@AppDomain.CurrentDomain.BaseDirectory + "\\Data\\Brands.clg", Old, FBrand.ToString());
            //---------------------------------------------------------------------------------+


            // Clear --------------------------------------------------------------------------+
            STotal.Text = SSTotal.Text = string.Format("{0:N2}", TotalVal);

            SCapital.Text = string.Format("{0:N2}", TotalVal + Balance);

            SScale0.Text = SScale1.Text = "Units";

            SType.Text = SBrand.Text = STrNote.Text = "";
            SAmount.Text = SStore.Text = "0";
            //---------------------------------------------------------------------------------+

            return;
            Invailed:
            MessageBox.Show("Invalid inputs", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Invoices Search Options ...                                                    |
        //---------------------------------------------------------------------------------+
        private void SearchText_Changed(object sender, TextChangedEventArgs e)
        {
            TextBlock TB = new TextBlock();
            TB.Text = "No Items Match your search.";
            TB.FontSize = 16;
            TB.HorizontalAlignment = HorizontalAlignment.Center;

            if (SSrchTxt.Text != "" && SComb.SelectedIndex != 3)
            {
                StackPanel SrchSP = new StackPanel();

                foreach (Border BTF in InvoPan.Children)
                {
                    Grid BG = (BTF.Child as StackPanel).Children[0] as Grid;

                    string SearchType = "";

                    switch (SComb.SelectedIndex)
                    {
                        case 0: SearchType = ((TextBlock)((Border)BG.Children[3]).Child).Text; break;
                        case 1: SearchType = ((TextBlock)((Border)BG.Children[5]).Child).Text; break;
                        case 2: SearchType = ((TextBlock)((Border)BG.Children[4]).Child).Text; break;
                    }

                    int Index = ((TextBlock)((Border)BG.Children[0]).Child).Text.IndexOf('-');
                    string Num = ((TextBlock)((Border)BG.Children[0]).Child).Text.Remove(Index - 1);

                    Operation Op = Operations.Find(OTF => OTF.Number == int.Parse(Num));

                    if (SearchType.ToLower().Contains(SSrchTxt.Text.ToLower()) == true) SrchSP.Children.Add(MyInterfaces.InvoB(Op, Currency));
                }

                if (SrchSP.Children.Count != 0) InvoScroll.Content = SrchSP;
                else InvoScroll.Content = TB;
            }
            else if ((SSDay.Text != "" || SSMonth.Text != "" || SSyear.Text != "") && SComb.SelectedIndex == 3)
            {
                StackPanel SrchSP = new StackPanel();

                foreach (Border BTF in InvoPan.Children)
                {
                    Grid BG = (BTF.Child as StackPanel).Children[0] as Grid;

                    int Index = ((TextBlock)((Border)BG.Children[0]).Child).Text.IndexOf('-');
                    string Num = ((TextBlock)((Border)BG.Children[0]).Child).Text.Remove(Index - 1);

                    Operation Op = Operations.Find(OTF => OTF.Number == int.Parse(Num));

                    string[] Date = ((TextBlock)((Border)BG.Children[1]).Child).Text.Split('/');
                    bool[] Function = new bool[3];

                    if (SSDay.Text == "") Function[0] = true;
                    else if (Date[0].ToLower().Contains(SSDay.Text.ToLower()) == true) Function[0] = true;
                    if (SSMonth.Text == "") Function[1] = true;
                    else if (Date[1].ToLower().Contains(SSMonth.Text.ToLower()) == true) Function[1] = true;
                    if (SSyear.Text == "") Function[2] = true;
                    else if (Date[2].ToLower().Contains(SSyear.Text.ToLower()) == true) Function[2] = true;

                    if (Function[0] && Function[1] && Function[2]) SrchSP.Children.Add(MyInterfaces.InvoB(Op, Currency));
                }

                if (SrchSP.Children.Count != 0) InvoScroll.Content = SrchSP;
                else InvoScroll.Content = TB;
            }
            else InvoScroll.Content = InvoPan;
        }

        private void SComb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!CheckNulls(new object[] { SComb, SSrchTxt, SDate })) return;
            if (SComb.SelectedIndex == 3) { SSrchTxt.Visibility = Visibility.Hidden; SDate.Visibility = Visibility.Visible; }
            else { SSrchTxt.Visibility = Visibility.Visible; SDate.Visibility = Visibility.Hidden; }
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Invoices Buttons ...                                                           |
        //---------------------------------------------------------------------------------+

        // Return Button ...
        private void InvoReturn_Click(object sender, RoutedEventArgs e)
        {
            int num = 0;
            foreach (Border Bord in InvoPan.Children)
            {
                StackPanel SP = Bord.Child as StackPanel;
                if (SP.Height != 25)
                {
                    Grid G = SP.Children[0] as Grid;
                    Border B = G.Children[0] as Border;
                    TextBlock TB = B.Child as TextBlock;

                    num = int.Parse(TB.Text.Remove(TB.Text.Length - 2));
                    break;
                }
            }
            if (num == 0) return;

            Operation FOperation = Operations.Find(OTF => OTF.Number == num);

            TCustomer.Text = FOperation.Customer;
            TType.Text = FOperation.Brand.TName;
            TBrand.Text = FOperation.Brand.Name;

            TStore.Text = BTypes.Find(TTF => TTF.Name == TType.Text).Brands.Find(BTF => BTF.Name == TBrand.Text).Amount.ToString();

            TPriceA.Text = FOperation.price.Amount.ToString();
            TPriceV.Text = FOperation.price.Value.ToString();
            TScale0.Text = TScale1.Text = TScale2.Text = FOperation.Brand.Scale;

            TOrder.Items.Clear();
            TOrder.Items.Add("Return Purchases");
            TOrder.Items.Add("Return Sales");
            if (FOperation.Type == Trading.Purchase) TOrder.SelectedIndex = 0;
            else TOrder.SelectedIndex = 1;
            IsNewPrice.Visibility = Visibility.Collapsed;
            IsNow.Content = "Return Now";
            ConOBtn.Content = "Confirm Return";
            TimerEvents.ChangeView(TradeGrid);
        }

        // Remove Button ...
        private void InvoRemove_Click(object sender, RoutedEventArgs e)
        {
            foreach (Border Bord in InvoPan.Children)
            {
                StackPanel SP = Bord.Child as StackPanel;
                if (SP.Height != 25)
                {
                    Grid G = SP.Children[0] as Grid;
                    Border B = G.Children[0] as Border;
                    TextBlock TB = B.Child as TextBlock;

                    int num = int.Parse(TB.Text.Remove(TB.Text.Length - 2));
                    Operation FOperation = Operations.Find(OTF => OTF.Number == num);
                    if (FOperation == null) return;

                    EditData(@AppDomain.CurrentDomain.BaseDirectory + "\\Data\\Operations.clg", FOperation.ToString(), "");
                    Operations.Remove(FOperation);
                    InvoPan.Children.Remove(Bord);

                    InvoRemove.IsEnabled = InvoReturn.IsEnabled = false;
                    break;
                }
            }             
        }
        //---------------------------------------------------------------------------------+

    }
}