using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zalojnaKushta_2.Entity
{
    public enum Role {Admin, Owner, Employee}
    public class UserEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; private set; }

        public Role UserRole { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool IsLogged { get; set; }

        public double spent { get; set; }
        public double earned { get; set; }

        [ForeignKey("UserID")]
        public ICollection<DealEntity> Deals { get; set; }

        [ForeignKey("UserID")]
        public ICollection<PladgedEntity> Pladged { get; set; }

        [ForeignKey("UserID")]
        public ICollection<LogEntity> Logs { get; set; }

        public UserEntity() { }
        public UserEntity(string Username, string Password, Role UserRole)
        {
            this.UserName = Username;
            this.Password = Password;
            this.UserRole = UserRole;
        }
    }
}
