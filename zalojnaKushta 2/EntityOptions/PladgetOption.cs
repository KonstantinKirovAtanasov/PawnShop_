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
    class PladgetOption
    {
        public static string AddnewItem(PladgedEntity pladged)
        {
            using(var dbConnection = new DatabaseContext())
            {
                try
                {
                    dbConnection.Pladgeds.Add(pladged);
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
        public static void updatePladged(PladgedEntity Modifiedpladged)
        {
            using(var dbConnection = new DatabaseContext())
            {
                int pladgedID = Modifiedpladged.ID;
                var pladged = dbConnection.Pladgeds.SingleOrDefault(x => x.ID == pladgedID);

                if (pladged != null)
                {
                    pladged.DeadLine = Modifiedpladged.DeadLine;
                    pladged.IsClosed = Modifiedpladged.IsClosed;
                    pladged.returned = Modifiedpladged.returned;

                    dbConnection.SaveChanges();
                }
                else
                {
                    dbConnection.Pladgeds.Add(Modifiedpladged);
                    dbConnection.SaveChanges();
                }
            }
        }

        public static void UpdatePladgeds(ref ObservableCollection<PladgedEntity> pladgeds)
        {
            using(var dbConnection = new DatabaseContext())
            {
                pladgeds.Clear();
                var pls = (from pladged in dbConnection.Pladgeds
                           select pladged).ToList();
                foreach (var pl in pls)
                {
                        if (pl.IsClosed == false)
                        {
                            pladgeds.Add(pl);
                        }
                }            
            }
        }

        public static void FindPladget(ref PladgedEntity pladged, int ID)
        {
            using (var dbConnection = new DatabaseContext())
            {
                pladged = (from pl in dbConnection.Pladgeds
                           where pl.ID == ID
                           select pl).FirstOrDefault();
            }
        }
        public static void UpdatePladgedsByFilter(ref ObservableCollection<PladgedEntity> pladgeds,
            string nameFilter, string itemType)
        {
            UpdatePladgeds(ref pladgeds);
            if (nameFilter.Length<1 && itemType.Length<1)
            {
                return;
            }
            if (nameFilter.Length>=1 && itemType.Length<1)
            {
                var allPladgeds = pladgeds.ToList();
                foreach (var pl in allPladgeds)
                    if (!pl.ClientName.Contains(nameFilter))
                        pladgeds.Remove(pl);
                return;
            }

            // the itemType Selection is Legit so let cast it 
            // And look only for the name filter
            // In all cases itemtype is selected

            ItemType item;
            Enum.TryParse(itemType, out item);

            if (nameFilter.Length<1)
            {
                var allPladgeds = pladgeds.ToList();
                foreach (var pl in allPladgeds)
                    if (!pl.ItemType.Equals(item))
                        pladgeds.Remove(pl);
                return;
            }
            if(nameFilter.Length>=1)
            {
                var allPladgeds = pladgeds.ToList();
                foreach (var pl in allPladgeds)
                    if (!(pl.ItemType.Equals(item) && pl.ClientName.Contains(nameFilter)))
                        pladgeds.Remove(pl);
                return;
            }
        }
    }
}
