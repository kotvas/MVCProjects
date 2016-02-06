using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServices.Models
{
    public class ReservationRepository
    {
        private static ReservationRepository repo = new ReservationRepository();
        public static ReservationRepository Current
        {
            get
            {
                return repo;
            }
        }

        private List<Reservation> data = new List<Reservation>
        {
            new Reservation { ReservationId = 1, ClientName = "Adam", Location = "Board Room"},
            new Reservation { ReservationId = 2, ClientName = "Jacqui", Location = "Lecture Hall"},
            new Reservation { ReservationId = 3, ClientName = "Russell", Location = "Meeting Room 1"}
        };

        public IEnumerable<Reservation> GetAll()
        {
            return data;
        }

        public Reservation Get(Int32 id)
        {
            return data.Where(r => r.ReservationId == id).FirstOrDefault();
        }

        public Reservation Add(Reservation item)
        {
            item.ReservationId = data.Count + 1;
            data.Add(item);
            return item;
        }

        public void Remove(Int32 id)
        {
            Reservation item = Get(id);
            if (item != null)
            {
                data.Remove(item);
            }
        }

        public Boolean Update(Reservation item)
        {
            Reservation storedItem = Get(item.ReservationId);
            if (storedItem != null)
            {
                storedItem.ClientName = item.ClientName;
                storedItem.Location = item.Location;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}