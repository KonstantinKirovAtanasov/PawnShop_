using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zalojnaKushta_2.Entity
{
    public class LogEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }

        public DateTime? SingedIn { get; set; }
        public DateTime? SingedOut { get; set; }

        public double Earned { get; set; }
        public double Spent { get; set; }

        public int UserID { get; set; }
        public UserEntity Users;

        public LogEntity() { }
        public LogEntity(DateTime SingedIn, int userID )
        {
            this.SingedIn = SingedIn;
            this.UserID = userID;
        }
    }
}
