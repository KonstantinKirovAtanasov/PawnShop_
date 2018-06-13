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
    /// Interaction logic for SetPladged.xaml
    /// </summary>
    public partial class SetPladged : Window
    {
        public bool FlagIsClosed = false;

        public PladgedEntity pladged { get; set; }
        public string PladgedInFo { get; set; }
        public double earned=0;

        public SetPladged(PladgedEntity pladged)
        {
            this.pladged = pladged;
            SetPladgedInfo();
            InitializeComponent();
            DataContext = this;
        }

        private void SetPladgedInfo()
        {
            var sb = new StringBuilder();
            sb.Append("Клиент: " + pladged.ClientName + "\n");
            sb.Append("Дата на залог: " + pladged.DayOfCome + "\n");
            sb.Append("Вещ: " + pladged.ItemDesc + "\n");
            sb.Append("Цена на покупка: " + pladged.Price + "\n");
            sb.Append("Категория: " + pladged.ItemType + "\n");
            sb.Append("Лихва: " + pladged.increaseForMonth);
            PladgedInFo = sb.ToString();
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(EarnedMoney.Text, out earned))
            {
                if (DeadLineNewCalendar.SelectedDate.HasValue)
                {
                    pladged.DeadLine = DeadLineNewCalendar.SelectedDate;
                    pladged.returned += earned;
                    pladged.IsClosed = (bool)isClosed.IsChecked;
                    FlagIsClosed = true;
                    Close();
                }
                else
                    MessageBox.Show("Моля отбележе дата");
            }
            else
                MessageBox.Show("Невалидни данни за внесени пари");
        }
    }
}
