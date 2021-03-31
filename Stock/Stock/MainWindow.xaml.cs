using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Threading;

namespace Stock
{
    public partial class MainWindow : Window
    {
        public int index;
        public int selectedCMitem;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void RebindData()
        {
            DBClass.OpenConnection();

            switch (selectedCMitem)
            {
                case 0:
                    DBClass.OpenConnection();
                    DBClass.ExecuteFillingCommand("SELECT * FROM Goods;", dgListGoods);
                    DBClass.CloseConnection();
                    break;
                case 1:
                    DBClass.OpenConnection();
                    DBClass.ExecuteFillingCommand("SELECT * FROM Goods WHERE [Перемещение] = 'Принят';", dgListGoods);
                    DBClass.CloseConnection();
                    break;
                case 2:
                    DBClass.OpenConnection();
                    DBClass.ExecuteFillingCommand("SELECT * FROM Goods WHERE [Перемещение] = 'Склад';", dgListGoods);
                    DBClass.CloseConnection();
                    break;
                case 3:
                    DBClass.OpenConnection();
                    DBClass.ExecuteFillingCommand("SELECT * FROM Goods WHERE [Перемещение] = 'Продан';", dgListGoods);
                    DBClass.CloseConnection();
                    break;
                default:
                    DBClass.OpenConnection();
                    DBClass.ExecuteFillingCommand("SELECT * FROM Goods;", dgListGoods);
                    DBClass.CloseConnection();
                    break;
            }

            DBClass.ExecuteFillingCommand("SELECT * FROM Goods WHERE [Перемещение] = 'Принят';", dgAcceptedListGoods);
            DBClass.ExecuteFillingCommand("SELECT * FROM Goods WHERE [Перемещение] = 'Склад';", dgStockListGoods);
            DBClass.ExecuteFillingCommand("SELECT * FROM Goods WHERE [Перемещение] = 'Продан';", dgSoldOutListGoods);

            DBClass.CloseConnection();
        }
        protected void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            RebindData();
        }
        private void SetTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 60);
            dispatcherTimer.Start();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RebindData();
            SetTimer();
        }
        private void BtnAccepted_Click(object sender, RoutedEventArgs e)
        {
            WndAccepted wndAccepted = new WndAccepted();
            wndAccepted.ShowDialog();
        }
        private void DgStockListGoods_ClickRightMouse(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var row = DataGridRow.GetRowContainingElement(e.OriginalSource as FrameworkElement);

                if (row != null)
                {
                    DataRowView dataRow = (DataRowView)dgStockListGoods.SelectedItem;
                    index = Convert.ToInt32(dataRow.Row.ItemArray[0]);

                    ContextMenu cm = this.FindResource("cmButton") as ContextMenu;
                    cm.PlacementTarget = sender as Button;
                    cm.IsOpen = true;
                }
            }
        }

        private void CmButton_Click(object sender, RoutedEventArgs e)
        {
            DBClass.OpenConnection();
            DBClass.UpdateRow(index);
            DBClass.CloseConnection();
        }

        private void ComboBox_Selected(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cbSelectedFilter = (ComboBox)sender;
            selectedCMitem = cbSelectedFilter.SelectedIndex;     
        }
    }
}
