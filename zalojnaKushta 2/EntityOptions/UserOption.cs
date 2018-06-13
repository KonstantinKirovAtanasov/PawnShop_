using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zalojnaKushta_2.DBConnection;
using zalojnaKushta_2.Entity;

namespace zalojnaKushta_2.EntityOptions
{
    class UserOption
    {   
        public static UserEntity UserLoginValidation(string UserName,string Password)
        {
            using (var dbConnection = new DatabaseContext())
            {
                UserEntity user = null;

                if ((user = (from u in dbConnection.Users
                             where u.UserName.Equals(UserName) &&
                             u.Password.Equals(Password)
                             select u).FirstOrDefault()) != null)
                {
                    user.IsLogged = true;
                    updateUser(ref user);
                    return user;
                }
                else
                    return null;
            }
        }
        public static string AddNewUser(string Username, string Password, Role userRole)
        {
            using (var dbConnection = new DatabaseContext())
            {
                if (Username.Length < 5)
                    return "Потребителското име е късо";
                if (Password.Length < 5)
                    return "Паролата е къса";
                if ((from user in dbConnection.Users
                    where user.UserName.Equals(Username)
                    select user).FirstOrDefault() == null)
                {
                    dbConnection.Users.Add(new UserEntity(Username, Password, userRole));
                    dbConnection.SaveChanges();
                    return "Готово";
                }
                else
                    return "Вече съществува такъв потребител";
            }
        }
        public static string DeleteUser(string Username)
        {
            using (var dbConnection = new DatabaseContext())
            {
                var userToDelete = new UserEntity();
                if ((userToDelete = (from user in dbConnection.Users
                                     where user.UserName.Equals(Username)
                                     select user).FirstOrDefault()) == null)
                    return "Не е намерен такъв потребител";
                else
                {
                    dbConnection.Users.Remove(userToDelete);
                    dbConnection.SaveChanges();
                    return "Готово";
                }
            }
        }
        public static void updateUser(ref UserEntity ModifiedUser)
        {
            using (var dbConnection = new DatabaseContext())
            {
                var MFUserID = ModifiedUser.ID;
                var temp = dbConnection.Users.SingleOrDefault(x => x.ID == MFUserID);
                if (temp != null)
                {
                    dbConnection.Entry(temp).CurrentValues.SetValues(ModifiedUser);
                    dbConnection.SaveChanges();
                }
            }
        }
        public static void GetUsersNames(ref ObservableCollection<string> users)
        {
            using (var dbConnection = new DatabaseContext())
            {
                var usersSet = (from user in dbConnection.Users
                             select user.UserName).ToList();
                foreach (var u in usersSet)
                    if (!users.Contains(u))
                        users.Add(u);
            }
        }
        public static UserEntity FindUser(string username)
        {
            using (var dbConnection = new DatabaseContext())
            {
                var user = (from u in dbConnection.Users
                            where u.UserName.Equals(username)
                            select u).FirstOrDefault();
                return user;
            }
        }
    }
}