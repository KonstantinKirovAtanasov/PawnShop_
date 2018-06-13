using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using zalojnaKushta_2.EntityOptions;

namespace zalojnaKushta_2
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>

    public partial class EmployeeWindow : Window
    {
        public string date { get { return DateTime.Now.ToShortDateString(); } private set {; } }

        private bool ReadyToRefresh = true;
        private LogEntity currentLog;
        private UserEntity user = new UserEntity();
        public UserEntity User { get { return user; } set {; } }

        private DealEntity SelectedDeal = new DealEntity();
        public DealEntity selectedDeal { get { return SelectedDeal; } set { SelectedDeal = value; } }

        private PladgedEntity SelectedPladged = new PladgedEntity();
        public PladgedEntity selectedPladged { get { return SelectedPladged; } set { SelectedPladged = value; } }

        // Lists For DataGrid

        private ObservableCollection<DealEntity> Deals = new ObservableCollection<DealEntity>();
        public ObservableCollection<DealEntity> deals { get { return Deals; } set {; } }
        private ObservableCollection<PladgedEntity> Pladgeds = new ObservableCollection<PladgedEntity>();
        public ObservableCollection<PladgedEntity> pladgeds { get { return Pladgeds; } set {; } }

        //
        public double dayEarned { get; set; }
        public double daySpent { get; set; }

        public EmployeeWindow(UserEntity u)
        {
            try
            {
                this.user = u;
                this.currentLog = new LogEntity(DateTime.Now, u.ID);
                updateLists();
                InitializeComponent();
                fillBoxes();
                DataContext = this;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString() + "\n"
                    + e.Source.ToString() + "\n" + e.StackTrace.ToString());
            }
        }

        private void PladgedDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedPladged = (PladgedEntity)PladgedDataGrid.SelectedValue;
            }
            catch (NullReferenceException) { }
        }
        private void updateLists()
        {
            DealOption.UpdateDeals(ref Deals);
            PladgetOption.UpdatePladgeds(ref Pladgeds);
            ReadyToRefresh = false;
        }

        private void AddPladgetButton_Click(object sender, RoutedEventArgs e)
        {
            PladgedEntity newPladged = new PladgedEntity();
            PladgedForm pf = new PladgedForm(newPladged, this.user.ID);
            new Thread(x =>
            {
                while (true)
                {
                    if (pf.FlagIsClosed)
                    {
                        PladgetOption.AddnewItem(newPladged);
                        daySpent += newPladged.Price;
                        ReadyToRefresh = true;
                        break;
                    }
                }
            }).Start();
            pf.Show();
        }

        private void AddDealButton_Click(object sender, RoutedEventArgs e)
        {
            DealEntity newDeal = new DealEntity();
            DealForm df = new DealForm(newDeal, this.user.ID);
            new Thread(x =>
            {
                while (true)
                {
                    if (df.FlagIsClosed)
                    {
                        DealOption.AddnewItem(newDeal);
                        daySpent += newDeal.Price;
                        ReadyToRefresh = true;
                        break;
                    }
                }
            }).Start();
            df.Show();
        }

        private void DealsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedDeal = (DealEntity)DealsDataGrid.SelectedValue;
            }
            catch (NullReferenceException) { }
            catch (InvalidCastException) { }
        }

        private void W_MouseMove(object sender, MouseEventArgs e)
        {
            // when you can't write your own event :D
            if (ReadyToRefresh)
                updateLists();
        }

        private void MarkAsSelled_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedDeal.DateOfBuy != null && SelectedDeal.DealType != DealType.Sell)
                {
                        SetAsSelled sas = new SetAsSelled(SelectedDeal);
                        new Thread(x =>
                        {
                            while (true)
                            {
                                if (sas.FlagIsClosed)
                                {
                                    dayEarned += selectedDeal.SelledFor;
                                    DealOption.updateDeal(selectedDeal);
                                    ReadyToRefresh = true;
                                    break;
                                }
                            }
                        }).Start();
                        sas.Show();
                    }
                    else
                        MessageBox.Show("Тази вещ е отбелязана като продадена моля селектирайте друга");
            }catch(NullReferenceException)
            {
                MessageBox.Show("Не е селектирана заложена вещ");
            }
        }

        private void IncomeForPladged_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedPladged.DeadLine != null)
                {
                    SetPladged setPl = new SetPladged(SelectedPladged);
                    new Thread(x =>
                    {
                        while (true)
                        {
                            if (setPl.FlagIsClosed)
                            {
                                PladgetOption.updatePladged(SelectedPladged);
                                dayEarned += setPl.earned;
                                ReadyToRefresh = true;
                                break;
                            }
                        }
                    }).Start();
                    setPl.Show();
                }
                else
                    MessageBox.Show("Не е селектирана заложена вещ");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Не е селектирана заложена вещ");
            }
        }

        private void filterListPladged_Click(object sender, RoutedEventArgs e)
        {
            PladgetOption.UpdatePladgedsByFilter(ref Pladgeds,
                ClientNamePladgedsFilterTextBox.Text, FilterItemTypeComboBoxPladged.Text);
        }
        private void filterListDeal_Click(object sender, RoutedEventArgs e)
        {
                DealOption.UpdateDealsbyFilte(ref Deals,
                    ClientNameDealFilterDeal.Text, FilterItemTypeComboBoxDeal.Text);
        }
        private void fillBoxes()
        {
            FilterItemTypeComboBoxPladged.Items.Add("");
            FilterItemTypeComboBoxPladged.Items.Add(ItemType.Antique);
            FilterItemTypeComboBoxPladged.Items.Add(ItemType.Technique);
            FilterItemTypeComboBoxPladged.Items.Add(ItemType.Gold);
            FilterItemTypeComboBoxPladged.Items.Add(ItemType.Silver);
            FilterItemTypeComboBoxPladged.Items.Add(ItemType.Trash);
            FilterItemTypeComboBoxPladged.Items.Add(ItemType.Other);

            FilterItemTypeComboBoxDeal.Items.Add("");
            FilterItemTypeComboBoxDeal.Items.Add(ItemType.Antique);
            FilterItemTypeComboBoxDeal.Items.Add(ItemType.Technique);
            FilterItemTypeComboBoxDeal.Items.Add(ItemType.Gold);
            FilterItemTypeComboBoxDeal.Items.Add(ItemType.Silver);
            FilterItemTypeComboBoxDeal.Items.Add(ItemType.Trash);
            FilterItemTypeComboBoxDeal.Items.Add(ItemType.Other);
        }

        private void DeletePladged_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPladged.DeadLine != null)
            {
                var resultFromMessageBox = MessageBox.Show(
                    "Сигурни ли сте че искате да изтриете съдържанието",
                    "Внимание!!", MessageBoxButton.YesNo);
                if (resultFromMessageBox == MessageBoxResult.Yes)
                {
                    SelectedPladged.IsClosed = true;
                    PladgetOption.updatePladged(SelectedPladged);
                    PladgetOption.UpdatePladgeds(ref Pladgeds);
                }
            }
            else
                MessageBox.Show("Не е селектирана заложена вещ");
        }
        private void DeleteDeal_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedDeal.DateOfBuy != null)
            {
                var result = MessageBox.Show(
                    "Сигурни ли сте че искате да изтриете съдържанието",
                    "Внимание!!!", MessageBoxButton.YesNo);
                if(result == MessageBoxResult.Yes)
                {
                    SelectedDeal.IsClosed = true;
                    DealOption.updateDeal(SelectedDeal);
                    DealOption.UpdateDeals(ref Deals);
                }
            }
            else
                MessageBox.Show("Моля селектирайте сделка");
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            user.earned += dayEarned;
            user.spent += daySpent;
            user.IsLogged = false;
            currentLog.SingedOut = DateTime.Now;
            currentLog.Earned = dayEarned;
            currentLog.Spent = daySpent;
            LogOption.AddLogEntity(ref currentLog);
            UserOption.updateUser(ref user);
            base.OnClosing(e);
        }
    }
}
