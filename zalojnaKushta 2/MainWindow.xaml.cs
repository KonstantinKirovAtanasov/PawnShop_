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
using zalojnaKushta_2.DBConnection;
using zalojnaKushta_2.Entity;
using zalojnaKushta_2.EntityOptions;

namespace zalojnaKushta_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            UserOption.AddNewUser("admin", "admin", Role.Admin);
            UserOption.AddNewUser("employee", "employee", Role.Employee);
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var user = UserOption.UserLoginValidation(UserNameTextBox.Text,
                UserPasswordTextBox.Password);
            if (user == null)
            {
                MessageBox.Show("Невалидно потребителско име или патола");
            }
            else if (user.UserRole.Equals(Role.Admin))
            {
                //Opens Admin Window with adminUser
                new AdminWindow(user).Show();
                Close();
            }
            else if(user.UserRole.Equals(Role.Owner))
            {
                //Opens Owner Window with ownerUser
                Close();
            }
            else if(user.UserRole.Equals(Role.Employee))
            {
                //Opens Employee Window with employeeUser
                new EmployeeWindow(user).Show();
                Close();
            }
                
        }

        private void QuitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
