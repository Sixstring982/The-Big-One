using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Big_One
{
    class Room
    {
        public Treasure treasure;
        public Trap trap;

        public bool IsJanitor = false;
        public bool IsElevator = false;

        public Room()
        {
            trap = null;
            treasure = null;
        }

        public void MakeJanitor()
        {
            IsJanitor = true;
        }

        public void MakeElevator()
        {
            IsElevator = true;
        }
    }
}
