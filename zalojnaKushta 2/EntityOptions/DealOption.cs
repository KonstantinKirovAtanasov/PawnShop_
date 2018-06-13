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
    class DealOption
    {
        public static string AddnewItem(DealEntity pladged)
        {
            using (var dbConnection = new DatabaseContext())
            {
                try
                {
                    dbConnection.Deals.Add(pladged);
                    dbConnection.SaveChanges();

                    //user.spent += pladged.Price;
                    //UserOption.updateUser(ref user);

                    return "Готово";
                }
                catch (Exception)
                {
                    return "Something went wrong";
                }
            }
        }
        
        public static void updateDeal(DealEntity ModifiedDeal)
        {
            using (var dbConnection = new DatabaseContext())
            {
                int pladgedID = ModifiedDeal.ID;
                var deal = dbConnection.Deals.SingleOrDefault(x => x.ID == pladgedID);

                if (deal != null)
                {                  
                    deal.DealType = ModifiedDeal.DealType;
                    deal.IsClosed = ModifiedDeal.IsClosed;
                    deal.ItemDesc = ModifiedDeal.ItemDesc;
                    deal.SelledFor = ModifiedDeal.SelledFor;
                    deal.ClientName = ModifiedDeal.ClientName;

                    dbConnection.SaveChanges();
                }
                else
                {
                    dbConnection.Deals.Add(ModifiedDeal);
                    dbConnection.SaveChanges();
                }
            }
        }

        public static void UpdateDeals(ref ObservableCollection<DealEntity> deals)
        {
            deals.Clear();
            using (var dbConnection = new DatabaseContext())
            {
                var dls = (from deal in dbConnection.Deals
                           select deal).ToList();
                foreach (var dl in dls)
                    if (dl.IsClosed == false)
                        deals.Add(dl);
            }
        }

        public static void UpdateDealsbyFilte(ref ObservableCollection<DealEntity> deals,
            string ClientNameFilter, string ItemTypeFilter)
        {
            UpdateDeals(ref deals);
            if (ClientNameFilter.Length < 1 && ItemTypeFilter.Length < 1)
            {
                return;
            }
            if (ClientNameFilter.Length >= 1 && ItemTypeFilter.Length < 1)
            {
                var allDeals = deals.ToList();
                foreach (var d in allDeals)
                    if (!d.ClientName.Contains(ClientNameFilter))
                        deals.Remove(d);
                return;
            }

            // the itemType Selection is Legit so let cast it 
            // And look only for the name filter In all cases 
            // itemtype is selected the validation is realised by adding
            // ItemTypes to combobox and the combobox can not be edited

            ItemType type;
            Enum.TryParse(ItemTypeFilter, out type);

            if (ClientNameFilter.Length < 1)
            {
                var allDeals = deals.ToList();
                foreach (var d in allDeals)
                    if (!d.ItemType.Equals(type))
                        deals.Remove(d);
                return;
            }
            if (ClientNameFilter.Length >= 1)
            {
                var allDeals = deals.ToList();
                foreach (var d in allDeals)
                    if (!(d.ItemType.Equals(type) && d.ClientName.Contains(ClientNameFilter)))
                        deals.Remove(d);
                return;
            }
        }       
    }
}
