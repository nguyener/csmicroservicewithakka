using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharpMicroServiceWithAkka
{
    class Product
    {
        private string name;
        private int price;

        public String Name
        {
            get { return name; }
            set { name = value; }

        }

        public int Price
        {
            get { return price; }
            set { price = value; }

        }
    }
}
