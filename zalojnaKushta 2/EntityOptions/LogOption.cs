using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using zalojnaKushta_2.DBConnection;
using zalojnaKushta_2.Entity;

namespace zalojnaKushta_2.EntityOptions
{
    class LogOption
    {
        public static void AddLogEntity(ref LogEntity log)
        {
            using (var dbConnection  = new DatabaseContext())
            {
                try
                {
                    dbConnection.Logs.Add(log);
                    dbConnection.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Something went Wrong call your Admin!");
                }
            }
        }
        public static void getLogsOfUser(int UserID, ref ICollection<LogEntity> logs)
        {
            // this is for owner role .. to be continued
            // in next version of the program
            using(var dbConnection = new DatabaseContext())
            {
                ICollection<LogEntity> logsTemp = (from log in dbConnection.Logs
                                               where log.UserID == UserID
                                               select log).ToList();
                logs = logsTemp;
            }
        }
    }
}
