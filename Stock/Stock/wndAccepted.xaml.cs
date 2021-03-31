using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;
using Stock.Classes;

namespace Stock
{
    public partial class WndAccepted : Window
    {
        public WndAccepted()
        {
            InitializeComponent();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {

            if (tbArticul.Text == "" ||
                tbGood.Text == "" ||
                tbQuantity.Text == "" ||
                tbUnit.Text == "" ||
                tbPrice.Text == "" ||
                tbCost.Text == "")
            {
                MessageBox.Show("Не все поля заполнены!");
            }
            else
            {
                DBClass.OpenConnection();

                string[] str = {tbArticul.Text, tbGood.Text, tbQuantity.Text, tbUnit.Text, tbPrice.Text, tbCost.Text};
                DBClass.AddRow(str);

                DBClass.CloseConnection();

                this.DialogResult = true;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
