using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zalojnaKushta_2.Entity
{
    public enum DealType { Buy,Sell}
    public enum ItemType { Gold, Silver, Technique, Antique, Trash, Other}
    public class DealEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get;private set; }

        public DealType DealType { get; set; }
        public ItemType ItemType { get; set; }
        public string ItemDesc { get; set; }
        public DateTime? DateOfBuy { get; set; }
        public double Price { get; set; }
        public double SelledFor { get; set; }

        public bool IsClosed { get; set; }

        public int UserID { get; set; }
        public UserEntity User { get; set; }

        public string ClientName { get; set; }

        public DealEntity() { }
        public DealEntity(ItemType itemType,DealType dealType,string ItemDesc, int UserID, string ClientName,double Price)
        {
            this.DealType = dealType;
            this.ItemType = itemType;
            this.ItemDesc = ItemDesc;
            this.UserID = UserID;
            this.ClientName = ClientName;
            this.DateOfBuy = (DateTime?)DateTime.Now;
            this.Price = Price;
        }
    }
}
