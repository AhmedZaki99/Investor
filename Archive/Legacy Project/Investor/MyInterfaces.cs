using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using MyLibrary;

namespace Investor
{
    static class MyInterfaces
    {
        //---------------------------------------------------------------------------------+
        //  Main Entry ...                                                                 |
        //---------------------------------------------------------------------------------+

        // Fields ..
        static StackPanel InvoPan;
        static Button InvoRemove, InvoReturn;

        // Colors Settings ...
        static Brush StoreLow_Brush = Brushes.LightPink;
        static Brush StoreDef_Brush = Brushes.LightBlue;

        static Brush InvoPrch_Brush = Brushes.Teal;
        static Brush InvoSale_Brush = Brushes.DarkSeaGreen;
        static Brush InvoRtrn_Brush = Brushes.LightSteelBlue;

        static Brush InvoMouseIn_Brush = Brushes.AliceBlue;
        static Brush InvoMouseOut_Brush = Brushes.White;

        static Brush TransferList_Brush = Brushes.LightGray;
        static Brush NumbersBorder_Brush = Brushes.Gray;

        // More Sittings ...
        const byte Size_Font = 16;

        /// <summary>
        /// Setting Interface Options.
        /// </summary>
        public static void InterfaceSet(StackPanel invoPan, Button invoRemove, Button invoReturn)
        {
            InvoPan = invoPan;
            InvoRemove = invoRemove;
            InvoReturn = invoReturn;
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Store GroupBox ...                                                             |
        //---------------------------------------------------------------------------------+
        /// <summary>
        /// The GroupBox of new store's brands.
        /// </summary>
        /// <param name="IsLow">Asks if the brand's amount is low.</param>
        /// <param name="Currency">The used currency.</param>
        /// <param name="NBrand">The new brand.</param>
        /// <returns></returns>
        public static GroupBox StoreGB(bool IsLow, string Currency, Brand NBrand)
        {
            GroupBox NewGB = new GroupBox
            {
                FontSize = Size_Font,
                Header = NBrand.Name,
                BorderThickness = new Thickness(3)
            };

            if (!IsLow) NewGB.BorderBrush = StoreDef_Brush;
            else NewGB.BorderBrush = StoreLow_Brush;

            StackPanel NewSP = new StackPanel();

            string FirstInput = $"Trade Price Per {NBrand.TradePrice.Amount} {NBrand.ScaleUnit} : ";

            NewSP.Children.Add(StoreG(FirstInput, string.Format("{0:N2}", NBrand.TradePrice.Value), Currency));
            NewSP.Children.Add(StoreG("Amount : ", NBrand.Amount.ToString(), NBrand.ScaleUnit));
            NewSP.Children.Add(StoreG("Value : ", string.Format("{0:N2}", NBrand.Value), Currency));

            NewGB.Content = NewSP;

            MainWindow.TotalVal += NBrand.Value;

            return NewGB;
        }
        /// <summary>
        /// GroupBox's Grid in the Store.
        /// </summary>
        /// <param name="Tb1">TextBlock 1 Text</param>
        /// <param name="Tb2">TextBlock 2 Text</param>
        /// <param name="Tb3">TextBlock 3 Text</param>
        /// <returns></returns>
        private static Grid StoreG(string Tb1, string Tb2, string Tb3)
        {
            Grid NewG = new Grid();
            
            NewG.Margin = new Thickness(10, 5, 10, 5);

            ColumnDefinition[] CD = new ColumnDefinition[3];
            for (int i = 0; i < CD.Length; i++) CD[i] = new ColumnDefinition();
            CD[0].Width = new GridLength(6, GridUnitType.Star);
            CD[1].Width = new GridLength(4, GridUnitType.Star);
            CD[2].Width = new GridLength(3, GridUnitType.Star);
            foreach (ColumnDefinition C in CD) NewG.ColumnDefinitions.Add(C);

            TextBlock[] TB = new TextBlock[3];
            for (int i = 0; i < TB.Length; i++) TB[i] = new TextBlock();

            TB[0].Text = Tb1;
            TB[1].Text = Tb2;
            TB[2].Text = Tb3;
            TB[0].FontSize = TB[1].FontSize = TB[2].FontSize = Size_Font;
            TB[1].Padding = new Thickness(5, 0, 5, 0);

            Border NewB = new Border();
            NewB.Margin = new Thickness(5, 0, 5, 0);
            NewB.BorderBrush = NumbersBorder_Brush;
            NewB.BorderThickness = new Thickness(1);

            NewB.Child = TB[1];

            NewG.Children.Add(TB[0]);
            NewG.Children.Add(NewB);
            NewG.Children.Add(TB[2]);

            TB[0].SetValue(Grid.ColumnProperty, 0);
            NewB.SetValue(Grid.ColumnProperty, 1);
            TB[2].SetValue(Grid.ColumnProperty, 2);

            return NewG;
        }
        /// <summary>
        /// Edit's a GroupBox in the store.
        /// </summary>
        /// <param name="SG">The Parent grid.</param>
        /// <param name="FBrand">The edited brand.</param>
        public static void EditStoreGB(Grid SG, Brand FBrand)
        {
            bool IsExist = false;

            foreach (StackPanel SP in SG.Children)
                if (SP.Name == FBrand.Type.Name)
                {
                    foreach (GroupBox GB in SP.Children)
                        if ((string)GB.Header == FBrand.Name)
                        {
                            IsExist = true;

                            StackPanel FSP = GB.Content as StackPanel;
                            Grid FG1 = FSP.Children[0] as Grid;
                            Grid FG2 = FSP.Children[1] as Grid;
                            Grid FG3 = FSP.Children[2] as Grid;

                            Border FB1 = FG1.Children[1] as Border;
                            Border FB2 = FG2.Children[1] as Border;
                            Border FB3 = FG3.Children[1] as Border;

                            TextBlock FTB = FG1.Children[0] as TextBlock;
                            TextBlock FTB1 = FB1.Child as TextBlock;
                            TextBlock FTB2 = FB2.Child as TextBlock;
                            TextBlock FTB3 = FB3.Child as TextBlock;

                            MainWindow.TotalVal -= double.Parse(FTB3.Text);

                            FTB.Text = string.Format("Trade Price Per {0} {1} : ", FBrand.TradePrice.Amount, FBrand.ScaleUnit);
                            FTB1.Text = string.Format("{0:N2}", FBrand.TradePrice.Value);
                            FTB2.Text = FBrand.Amount.ToString();
                            FTB3.Text = string.Format("{0:N2}", FBrand.Value);

                            MainWindow.TotalVal += double.Parse(FTB3.Text);

                            break;
                        }
                    if (!IsExist) SP.Children.Add(StoreGB(false, MainWindow.Currency, FBrand));

                    break;
                }
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Invoices Border ...                                                            |
        //---------------------------------------------------------------------------------+
        /// <summary>
        /// The Border of The new Invoce.
        /// </summary>
        /// <param name="NewOp">The New Operation.</param>
        /// <param name="Currency">The used currency.</param>
        /// <param name="Num">The invoice number.</param>
        /// <returns></returns>
        public static Border InvoB(Operation NewOp, string Currency)
        {
            Border NewBo = new Border();

            NewBo.Margin = new Thickness(3, 1, 3, 1);
            NewBo.Padding = new Thickness(3, 1, 3, 1);
            NewBo.BorderThickness = new Thickness(1, 0, 0, 1);

            if (NewOp.Type == Trading.Purchase) NewBo.BorderBrush = InvoPrch_Brush;
            else if (NewOp.Type == Trading.Sell) NewBo.BorderBrush = InvoSale_Brush;
            else NewBo.BorderBrush = InvoRtrn_Brush;

            StackPanel NewSP = new StackPanel();

            NewSP.Height = 25;

            NewSP.MouseUp += InvoPanel_MouseUp;
            NewSP.MouseLeave += InvoPanel_MouseLeave;
            NewSP.MouseEnter += InvoPanel_MouseEnter;

            Grid NewG = new Grid();

            NewG.Margin = new Thickness(5, 3, 10, 3);

            ColumnDefinition[] CD = new ColumnDefinition[6];
            for (int i = 0; i < CD.Length; i++) CD[i] = new ColumnDefinition();
            CD[0].Width = new GridLength(1, GridUnitType.Star);
            CD[1].Width = new GridLength(3, GridUnitType.Star);
            CD[2].Width = new GridLength(2.2, GridUnitType.Star);
            CD[3].Width = new GridLength(5.5, GridUnitType.Star);
            CD[4].Width = new GridLength(4.5, GridUnitType.Star);
            CD[5].Width = new GridLength(5, GridUnitType.Star);
            foreach (ColumnDefinition C in CD) NewG.ColumnDefinitions.Add(C);

            Border[] B = new Border[6];
            for (int i = 0; i < B.Length; i++)
            {
                B[i] = new Border();
                if (NewOp.Type == Trading.Purchase) B[i].BorderBrush = InvoPrch_Brush;
                else if (NewOp.Type == Trading.Sell) B[i].BorderBrush = InvoSale_Brush;
                else B[i].BorderBrush = InvoRtrn_Brush;

                if (i != 5) B[i].BorderThickness = new Thickness(0, 0, 1, 0);
            }

            TextBlock[] TB = new TextBlock[6];
            for (int i = 0; i < TB.Length; i++) TB[i] = new TextBlock();

            TB[2].Padding = TB[3].Padding = TB[4].Padding = TB[5].Padding = new Thickness(5, 0, 5, 0);

            for (int i = 0; i < TB.Length; i++)
            {
                TB[i].FontSize = 14;
                B[i].Child = TB[i];
                NewG.Children.Add(B[i]);
            }

            TB[0].Text = NewOp.Number.ToString() + " - ";
            TB[1].Text = NewOp.Time.Date.ToString(" dd / MM / yyyy ");
            TB[2].Text = NewOp.Type.ToString();
            TB[3].Text = NewOp.Customer;
            TB[4].Text = NewOp.Brand.TName;
            TB[5].Text = NewOp.Brand.Name;

            for (int i = 0; i < 6; i++) B[i].SetValue(Grid.ColumnProperty, i);

            string PString = string.Format("Price Per {0} {1} : ", NewOp.price.Amount, NewOp.Brand.Scale);

            NewSP.Children.Add(NewG);
            NewSP.Children.Add(InvoG(NewOp.Time.ToString(" hh : mm  tt"), "Amount : ", NewOp.Amount.ToString(), NewOp.Brand.Scale));
            NewSP.Children.Add(InvoG("", PString, string.Format("{0:N2}", NewOp.price.Value), Currency));
            NewSP.Children.Add(InvoG("", "Total Price : ", string.Format("{0:N2}", (NewOp.Amount / NewOp.price.Amount) * NewOp.price.Value), Currency));

            NewBo.Child = NewSP;

            return NewBo;
        }
        /// <summary>
        /// Border's Grid in Invoices.
        /// </summary>
        /// <param name="Tb1">TextBlock 1 Text</param>
        /// <param name="Tb2">TextBlock 2 Text</param>
        /// <param name="Tb3">TextBlock 3 Text</param>
        /// <returns></returns>
        private static Grid InvoG(string Tb0, string Tb1, string Tb2, string Tb3)
        {
            Grid NewG = new Grid();

            NewG.Margin = new Thickness(10, 3, 10, 3);

            ColumnDefinition[] CD = new ColumnDefinition[4];
            for (int i = 0; i < CD.Length; i++) CD[i] = new ColumnDefinition();
            CD[0].Width = new GridLength(3.7, GridUnitType.Star);
            CD[1].Width = new GridLength(6, GridUnitType.Star);
            CD[2].Width = new GridLength(6, GridUnitType.Star);
            CD[3].Width = new GridLength(5, GridUnitType.Star);
            foreach (ColumnDefinition C in CD) NewG.ColumnDefinitions.Add(C);

            int n;
            if (Tb0 != "") n = 4;
            else n = 3;

            TextBlock[] TB = new TextBlock[n];
            for (int i = 0; i < TB.Length; i++) TB[i] = new TextBlock();

            TB[0].Text = Tb1;
            TB[1].Text = Tb2;
            TB[2].Text = Tb3;
            TB[0].FontSize = TB[1].FontSize = TB[2].FontSize = 14;
            TB[1].Padding = new Thickness(5, 0, 5, 0);

            if (n == 4) { TB[3].Text = Tb0; TB[3].FontSize = 14; TB[3].Margin = new Thickness(30, 0, 0, 0); }

            foreach (TextBlock T in TB) NewG.Children.Add(T);

            TB[0].SetValue(Grid.ColumnProperty, 1);
            TB[1].SetValue(Grid.ColumnProperty, 2);
            TB[2].SetValue(Grid.ColumnProperty, 3);

            if (n == 4) TB[3].SetValue(Grid.ColumnProperty, 0);

            return NewG;
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Invoices Properities ...                                                       |
        //---------------------------------------------------------------------------------+

        // Controling Invoice Colors
        private static void InvoPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            StackPanel MyBo = sender as StackPanel;
            MyBo.Background = InvoMouseIn_Brush;
        }
        private static void InvoPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            StackPanel MyBo = sender as StackPanel;
            MyBo.Background = InvoMouseOut_Brush;
        }

        // Showing The Full Invoice.
        private static void InvoPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StackPanel MyBo = sender as StackPanel;
            if (MyBo.Height == 25)
            {
                foreach (Border Bord in InvoPan.Children)
                {
                    StackPanel SP = Bord.Child as StackPanel;
                    if (SP.Height != 25) SP.Height = 25;
                }
                TimerEvents.StretchPanel(MyBo);
                InvoRemove.IsEnabled = true;

                Border B = MyBo.Parent as Border;
                if (B.BorderBrush == InvoPrch_Brush || B.BorderBrush == InvoSale_Brush) InvoReturn.IsEnabled = true;
            }
            else
            {
                MyBo.Height = 25;
                InvoRemove.IsEnabled = InvoReturn.IsEnabled = false;
            }
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Goods Transfer Grid ...                                                        |
        //---------------------------------------------------------------------------------+
        /// <summary>
        /// The Grid of The new Goods Transfer.
        /// </summary>
        /// <param name="NewGT">The New Goods Transfer.</param>
        /// <param name="IsGry">The Grid Color.</param>
        /// <returns></returns>
        public static Grid GTransG(GTransfer NewGT, bool IsGry)
        {
            Grid NewG = new Grid();

            NewG.Margin = new Thickness(5);
            if (IsGry) NewG.Background = TransferList_Brush;

            ColumnDefinition[] CD = new ColumnDefinition[5];
            for (int i = 0; i < CD.Length; i++) CD[i] = new ColumnDefinition();
            CD[0].Width = new GridLength(1.2, GridUnitType.Star);
            CD[1].Width = new GridLength(2, GridUnitType.Star);
            CD[2].Width = new GridLength(3, GridUnitType.Star);
            CD[3].Width = new GridLength(1.5, GridUnitType.Star);
            CD[4].Width = new GridLength(1.5, GridUnitType.Star);
            foreach (ColumnDefinition C in CD) NewG.ColumnDefinitions.Add(C);

            int n;

            if (NewGT.Note != "")
            {
                n = 7;
                RowDefinition[] RD = new RowDefinition[2];
                for (int i = 0; i < RD.Length; i++)
                {
                    RD[i] = new RowDefinition();
                    RD[i].Height = new GridLength(1, GridUnitType.Star);
                    NewG.RowDefinitions.Add(RD[i]);
                }
            }
            else n = 5;

            TextBlock[] TB = new TextBlock[n];
            for (int i = 0; i < TB.Length; i++)
            {
                TB[i] = new TextBlock();
                TB[i].FontSize = Size_Font;
                if (i != 3) TB[i].Margin = new Thickness(5, 0, 5, 0);
            }

            TB[0].Text = NewGT.order.ToString();
            TB[1].Text = NewGT.Brand.TName;
            TB[2].Text = NewGT.Brand.Name;
            TB[3].Text = NewGT.Amount.ToString();
            TB[4].Text = NewGT.Brand.Scale;

            if (NewGT.Note != "")
            {
                TB[5].Text = "Note : ";
                TB[6].Text = NewGT.Note;
            }

            Border B = new Border();
            B.BorderBrush = NumbersBorder_Brush;
            B.BorderThickness = new Thickness(1);
            B.Margin = new Thickness(5, 0, 5, 0);
            B.Child = TB[3];

            for (int i = 0; i < TB.Length; i++) if (i != 3) NewG.Children.Add(TB[i]);
            NewG.Children.Add(B);

            TB[0].SetValue(Grid.ColumnProperty, 0);
            TB[1].SetValue(Grid.ColumnProperty, 1);
            TB[2].SetValue(Grid.ColumnProperty, 2);
            TB[4].SetValue(Grid.ColumnProperty, 4);
            B.SetValue(Grid.ColumnProperty, 3);

            if (NewGT.Note != "")
            {
                TB[5].SetValue(Grid.ColumnProperty, 1);
                TB[6].SetValue(Grid.ColumnProperty, 2);
                TB[5].SetValue(Grid.RowProperty, 1);
                TB[6].SetValue(Grid.RowProperty, 1);
            }

            return NewG;
        }
        //---------------------------------------------------------------------------------+


        //---------------------------------------------------------------------------------+
        //  Transfer Grid ...                                                              |
        //---------------------------------------------------------------------------------+
        /// <summary>
        /// The Grid of The new Transfer.
        /// </summary>
        /// <param name="NewT">The New Transfer.</param>
        /// <param name="IsGry">The Grid Color.</param>
        /// <returns></returns>
        public static Grid TransG(Transfer NewT, string Currency, bool IsGry)
        {
            Grid NewG = new Grid();
            
            NewG.Margin = new Thickness(5);
            if (IsGry) NewG.Background = TransferList_Brush;

            ColumnDefinition[] CD = new ColumnDefinition[5];
            for (int i = 0; i < CD.Length; i++) CD[i] = new ColumnDefinition();
            CD[0].Width = new GridLength(2, GridUnitType.Star);
            CD[1].Width = new GridLength(1.5, GridUnitType.Star);
            CD[2].Width = new GridLength(3, GridUnitType.Star);
            CD[3].Width = new GridLength(3, GridUnitType.Star);
            CD[4].Width = new GridLength(1.5, GridUnitType.Star);
            foreach (ColumnDefinition C in CD) NewG.ColumnDefinitions.Add(C);

            int n;

            if (NewT.Note != "")
            {
                n = 7;
                RowDefinition[] RD = new RowDefinition[2];
                for (int i = 0; i < RD.Length; i++)
                {
                    RD[i] = new RowDefinition();
                    RD[i].Height = new GridLength(1, GridUnitType.Star);
                    NewG.RowDefinitions.Add(RD[i]);
                }
            }
            else n = 5;

            TextBlock[] TB = new TextBlock[n];
            for (int i = 0; i < TB.Length; i++)
            {
                TB[i] = new TextBlock();
                TB[i].FontSize = Size_Font;
                if (i != 3) TB[i].Margin = new Thickness(5, 0, 5, 0);
            }

            TB[0].Text = NewT.Date.ToString(" dd / mm / yyyy ");
            TB[1].Text = NewT.order.ToString() + " : ";
            TB[2].Text = NewT.Type;
            TB[3].Text = string.Format("{0:N2}", NewT.Amount);
            TB[4].Text = Currency;

            if (NewT.Note != "")
            {
                TB[5].Text = "Note : ";
                TB[6].Text = NewT.Note;
            }

            Border B = new Border();
            B.BorderBrush = NumbersBorder_Brush;
            B.BorderThickness = new Thickness(1);
            B.Margin = new Thickness(5, 0, 5, 0);
            B.Child = TB[3];

            for (int i = 0; i < TB.Length; i++) if (i != 3) NewG.Children.Add(TB[i]);
            NewG.Children.Add(B);

            TB[0].SetValue(Grid.ColumnProperty, 0);
            TB[1].SetValue(Grid.ColumnProperty, 1);
            TB[2].SetValue(Grid.ColumnProperty, 2);
            TB[4].SetValue(Grid.ColumnProperty, 4);
            B.SetValue(Grid.ColumnProperty, 3);

            if (NewT.Note != "")
            {
                TB[5].SetValue(Grid.ColumnProperty, 1);
                TB[6].SetValue(Grid.ColumnProperty, 2);
                TB[5].SetValue(Grid.RowProperty, 1);
                TB[6].SetValue(Grid.RowProperty, 1);
            }

            return NewG;
        }
        //---------------------------------------------------------------------------------+


    }
}
