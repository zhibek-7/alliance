using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Car.Controllers
{
    [Serializable]
    public class ItemRemoteStore
    {
        private RemoteStore pr = new RemoteStore();
      
        private int quantity;

        public ItemRemoteStore()
        {
        }

        public ItemRemoteStore(RemoteStore product, int quantity)
        {
            this.pr = product;
            this.Quantity = quantity;
        }

        public RemoteStore Pr
        {
            get
            {
                return pr;
            }

            set
            {
                pr = value;
            }
        }

        public int Quantity
        {
            get
            {
                return quantity;
            }

            set
            {
                quantity = value;
            }
        }
    }
}
