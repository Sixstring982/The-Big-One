using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace The_Big_One
{
    class Treasure
    {
        public string name;
        public int value;
        public Treasure()
        {
            int objName = (int)Math.Log10(Math.Pow((Program.RNG().Next() % ObjectNames.Count), 10.0));
            if (objName < 0) objName = 0;
            name = "The " + ObjectNames[objName].name + 
                " of " + ObjectDescriptions[Program.RNG().Next() % ObjectDescriptions.Count];
            value = ObjectNames[objName].value;
        }

        class ObjectName
        {
            public int value;
            public string name;

            public ObjectName(string name, int value)
            {
                this.name = name;
                this.value = value;
            }
        }

        private static List<ObjectName> ObjectNames;
        private static List<string> ObjectDescriptions;

        public static void LoadObjectNames()
        {
            ObjectNames = new List<ObjectName>();
            ObjectNames.Add(new ObjectName("helmet", 20));
            ObjectNames.Add(new ObjectName("mask", 50));
            ObjectNames.Add(new ObjectName("idol", 200));
            ObjectNames.Add(new ObjectName("statue", 500));
            ObjectNames.Add(new ObjectName("amulet", 1000));
            ObjectNames.Add(new ObjectName("bracelet", 1200));
            ObjectNames.Add(new ObjectName("revolver", 2000));
            ObjectNames.Add(new ObjectName("rifle", 2500));
            ObjectNames.Add(new ObjectName("cape", 3000));
            ObjectNames.Add(new ObjectName("emerald", 4000));
            ObjectNames.Add(new ObjectName("amethyst", 6000));
            ObjectNames.Add(new ObjectName("diamond", 10000));
            ObjectNames.Add(new ObjectName("scepter", 15000));
            ObjectNames.Add(new ObjectName("crown", 20000));
            ObjectNames.Add(new ObjectName("throne", 30000));

            ObjectDescriptions = new List<string>();
            ObjectDescriptions.Add("destiny");
            ObjectDescriptions.Add("glory");
            ObjectDescriptions.Add("happiness");
            ObjectDescriptions.Add("destruction");
            ObjectDescriptions.Add("mordor");
            ObjectDescriptions.Add("elves");
            ObjectDescriptions.Add("dwarves");
            ObjectDescriptions.Add("satan");
            ObjectDescriptions.Add("god");
            ObjectDescriptions.Add("power");
            ObjectDescriptions.Add("wonder");
            ObjectDescriptions.Add("blasphemy");
        }
    }
}
