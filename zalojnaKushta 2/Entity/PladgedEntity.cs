using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zalojnaKushta_2.Entity
{
    public class PladgedEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get;private set; }

        public ItemType ItemType { get; set; }
        public string ItemDesc { get; set; }
        public DateTime? DayOfCome { get; set; }
        public DateTime? DeadLine { get; set; }
        public double Price { get; set; }
        public double increaseForMonth { get; set; }
        public double returned { get; set; }

        public bool IsClosed { get; set; }

        public int UserID { get;set; }
        public UserEntity User { get; set; }

        public string ClientName { get; set; }

        public PladgedEntity() { }
        public PladgedEntity(double Price, double Increase, ItemType itemType,
            string ItemDesc, int UserID, string ClientName, double Days = 31)
        {
            this.ItemType = ItemType;
            this.ItemDesc = ItemDesc;
            this.DayOfCome = DateTime.Now;
            this.DeadLine = DateTime.Now.AddDays(Days);
            this.Price = Price;
            this.increaseForMonth = Increase;
            this.UserID = UserID;
            this.returned = 0;
            this.ClientName = ClientName;
        }
    }
}
