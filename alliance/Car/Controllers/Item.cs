using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Car.Controllers
{
    [Serializable]
    public class Item
    {
        private Remnants pr = new Remnants();
        private int quantity;

        public Item() {
        }

        public Item(Remnants product, int quantity) {
            this.pr = product;
            this.Quantity = quantity;

        }

        public Remnants Pr
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













