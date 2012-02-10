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
            if ((Program.RNG().Next() % 10) > 2)
                treasure = new Treasure();
            else
                treasure = null;

            if ((Program.RNG().Next() % 10) > 2)
                trap = new Trap();
            else
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
