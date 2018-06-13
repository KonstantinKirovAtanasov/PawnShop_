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
using System.Windows.Shapes;
using zalojnaKushta_2.Entity;

namespace zalojnaKushta_2
{
    /// <summary>
    /// Interaction logic for PladgedForm.xaml
    /// </summary>
    public partial class PladgedForm : Window
    {
        public bool FlagIsClosed = false;
        private int userID;
        private PladgedEntity newPladged { get; set; }
        public PladgedForm(PladgedEntity newPladged, int UserID)
        {
            this.userID = UserID;
            this.newPladged = newPladged;
            InitializeComponent();
            fillType();
            DataContext = this;
        }

        private void CreatePladgedButton_Click(object sender, RoutedEventArgs e)
        {
            double price, increase, period;
            if (double.TryParse(PriceTextBox.Text, out price) &&
                double.TryParse(IncreaseTextBox.Text, out increase) &&
                double.TryParse(IncreaseTextBox.Text, out period))
            {
                newPladged.ClientName = ClientNameTextBox.Text;
                newPladged.ItemDesc = ItemDescTextBox.Text;
                newPladged.UserID = userID;
                newPladged.Price = price;
                newPladged.increaseForMonth = increase;
                newPladged.DayOfCome = (DateTime?)DateTime.Now;
                newPladged.DeadLine = (DateTime?)DateTime.Now.AddDays(period);
                newPladged.ItemType = (ItemType)ItemTypeComboBox.SelectedValue;
                FlagIsClosed = true;
                Close();
            }
            else
            {
                MessageBox.Show("Невалиден формат за цена/лихва");
            }
        }

        private void fillType()
        {
            ItemTypeComboBox.Items.Add(ItemType.Antique);
            ItemTypeComboBox.Items.Add(ItemType.Gold);
            ItemTypeComboBox.Items.Add(ItemType.Silver);
            ItemTypeComboBox.Items.Add(ItemType.Technique);
            ItemTypeComboBox.Items.Add(ItemType.Trash);
            ItemTypeComboBox.Items.Add(ItemType.Other);
        }

    }
}
