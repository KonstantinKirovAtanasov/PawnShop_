using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using zalojnaKushta_2.Entity;

namespace zalojnaKushta_2
{
    /// <summary>
    /// Interaction logic for DealForm.xaml
    /// </summary>
    public partial class DealForm : Window
    {
        public bool FlagIsClosed = false;
        private int userID;
        private DealEntity newDeal { get; set; }
        public DealForm(DealEntity newDeal, int userID)
        {
            this.userID = userID;
            this.newDeal = newDeal;
            InitializeComponent();
            fillType();
            DataContext = this;
        }

        private void CreateDealButton_Click(object sender, RoutedEventArgs e)
        {
            double price;
            if(double.TryParse(PriceTextBox.Text, out price))
            {
                newDeal.ItemType = (ItemType)TypeComboBox.SelectedItem;
                newDeal.DealType = DealType.Buy;
                newDeal.ItemDesc = ItemDescTextBox.Text;
                newDeal.UserID = userID;
                newDeal.ClientName = ClientNameTextBox.Text;
                newDeal.DateOfBuy = (DateTime?)DateTime.Now;
                newDeal.Price = price;
                FlagIsClosed = true;
                Close();
            }
            else
            {
                MessageBox.Show("Невалиден формат за цената");
            }
            
        }
        private void fillType()
        {
            TypeComboBox.Items.Add(ItemType.Antique);
            TypeComboBox.Items.Add(ItemType.Technique);
            TypeComboBox.Items.Add(ItemType.Gold);
            TypeComboBox.Items.Add(ItemType.Silver);
            TypeComboBox.Items.Add(ItemType.Trash);
            TypeComboBox.Items.Add(ItemType.Other);
        }
    }
}
