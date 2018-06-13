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
    /// Interaction logic for SetAsSelled.xaml
    /// </summary>
    public partial class SetAsSelled : Window
    {
        public bool FlagIsClosed = false;

        public DealEntity deal { get; set; }
        public string DealInFo { get; set; }

        public SetAsSelled(DealEntity deal)
        {
            this.deal = deal;
            SetDealInfo();
            InitializeComponent();
            DataContext = this;
        }
        private void SetDealInfo()
        {
            var sb = new StringBuilder();
            sb.Append("Клиент:" + deal.ClientName+ "\n");
            sb.Append("Дата на покупка:" + deal.DateOfBuy+ "\n");
            sb.Append("Вещ:" + deal.ItemDesc+ "\n");
            sb.Append("Цена на покупка:" + deal.Price+ "\n");
            sb.Append("Категория:" + deal.ItemType+ "\n");
            DealInFo = sb.ToString();
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            double price;
            if (double.TryParse(PriceTextBox.Text, out price))
            {
                deal.SelledFor = price;
                deal.IsClosed = true;
                deal.DealType = DealType.Sell;
                deal.ItemDesc += "/"+price;
                FlagIsClosed = true;
                Close();
            }
            else
                MessageBox.Show("Невалидни данни за цена!");
        }
    }
}
