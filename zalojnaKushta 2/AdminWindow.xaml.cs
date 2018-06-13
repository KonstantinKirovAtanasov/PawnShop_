using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using zalojnaKushta_2.EntityOptions;

namespace zalojnaKushta_2
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private UserEntity selectedUser = new UserEntity();
        public UserEntity SelectedUser { get { return selectedUser; } set { selectedUser = value; } }

        private ObservableCollection<string> usersList = new ObservableCollection<string>();
        public ObservableCollection<string> UsersList { get { return usersList; } set {; } }
        
        private UserEntity user = new UserEntity();
        public AdminWindow(UserEntity user)
        {
            this.user = user;
            InitializeComponent();
            FillBoxes();
            DataContext = this;
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            user.IsLogged = false;
            UserOption.updateUser(ref user);
            base.OnClosing(e);
        }

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(UserOption.DeleteUser(selectedUser.UserName));
            usersList.Remove(selectedUser.UserName);
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(UserOption.AddNewUser(UserNameTextBox.Text, UserPasswordTextBox.Text,
                (Role)Enum.Parse(typeof(Role), Roles.Text)));
            UserOption.GetUsersNames(ref usersList);
        }
        private void FillBoxes()
        {
            Roles.Items.Add(Role.Admin);
            Roles.Items.Add(Role.Owner);
            Roles.Items.Add(Role.Employee);

            UserOption.GetUsersNames(ref usersList);
        }

        private void UserNamesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                selectedUser = UserOption.FindUser(UserNamesListBox.SelectedValue.ToString());
                UserNameTextBox.Text = selectedUser.UserName;
                UserPasswordTextBox.Text = selectedUser.Password;
                Roles.SelectedValue = selectedUser.UserRole;
            }catch (Exception)
            {
            }
        }

        private void ChangeUser_Click(object sender, RoutedEventArgs e)
        {
            selectedUser.UserName = UserNameTextBox.Text;
            selectedUser.Password = UserPasswordTextBox.Text;
            selectedUser.UserRole = (Role)Roles.SelectedValue;
            UserOption.updateUser(ref selectedUser);
            MessageBox.Show("Готово!");
        }
    }
}
