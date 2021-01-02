using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewShoeStore.Models
{
    public class OrderShoe
    {
        public int IdOrder { get; set; }
        public Order Order { get; set; }
        public int IdShose { get; set; }
        public Shoe Shoe { get; set; }
    }
}
