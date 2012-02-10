using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Big_One
{
    class Item
    {
        public enum ItemType
        {
            Deadweight,
            SprayCan,
            Jack
        }

        private ItemType type;
        public Item(ItemType type)
        {
            this.type = type;
        }
    }
}
