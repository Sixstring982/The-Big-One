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

        public override string ToString()
        {
            return name;
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
            ObjectNames.Add(new ObjectName("helmet", 10));
            ObjectNames.Add(new ObjectName("mask", 20));
            ObjectNames.Add(new ObjectName("idol", 35));
            ObjectNames.Add(new ObjectName("statue", 50));
            ObjectNames.Add(new ObjectName("amulet", 75));
            ObjectNames.Add(new ObjectName("bracelet", 90));
            ObjectNames.Add(new ObjectName("revolver", 130));
            ObjectNames.Add(new ObjectName("rifle", 180));
            ObjectNames.Add(new ObjectName("cape", 250));
            ObjectNames.Add(new ObjectName("emerald", 300));
            ObjectNames.Add(new ObjectName("amethyst", 400));
            ObjectNames.Add(new ObjectName("diamond", 600));
            ObjectNames.Add(new ObjectName("scepter", 1000));
            ObjectNames.Add(new ObjectName("crown", 2000));
            ObjectNames.Add(new ObjectName("throne", 5000));

            ObjectDescriptions = new List<string>();
            ObjectDescriptions.Add("destiny");
            ObjectDescriptions.Add("marijauna");
            ObjectDescriptions.Add("happiness");
            ObjectDescriptions.Add("time");
            ObjectDescriptions.Add("the heavens");
            ObjectDescriptions.Add("Aslan");
            ObjectDescriptions.Add("time");
            ObjectDescriptions.Add("destruction");
            ObjectDescriptions.Add("mordor");
            ObjectDescriptions.Add("elves");
            ObjectDescriptions.Add("dwarves");
            ObjectDescriptions.Add("satan");
            ObjectDescriptions.Add("god");
            ObjectDescriptions.Add("power");
            ObjectDescriptions.Add("wonder");
            ObjectDescriptions.Add("blasphemy");
            ObjectDescriptions.Add("ganja");
            ObjectDescriptions.Add("Far-Mer");
            ObjectDescriptions.Add("the dreaded Kyle");
            ObjectDescriptions.Add("the Farmer");
            ObjectDescriptions.Add("the Theif");
            ObjectDescriptions.Add("the Knight");
            ObjectDescriptions.Add("kings");
            ObjectDescriptions.Add("queens");
            ObjectDescriptions.Add("beer");
        }
    }
}
