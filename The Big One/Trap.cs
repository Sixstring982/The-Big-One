using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Big_One
{
    class Trap
    {
        enum TrapType
        {
            Laser = 0,
            Bars,
            PressurePad
        }

        private TrapType type;
        public Trap()
        {
            type = (TrapType)(Program.RNG().Next() % 3);
        }
    }
}
