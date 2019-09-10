using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    /// <summary>
    /// This is a class for all of the armor types that the gladiators equip in game.
    /// </summary>
    public class Armor
    {
        public int armorValue;
    }
    public class Flesh : Armor
    {
        public Flesh()
        {
            armorValue = 1;
        }
    }
    public class Leather : Armor
    {
        public Leather()
        {
            armorValue = 2;
        }
    }
    public class Padded : Armor
    {
        public Padded()
        {
            armorValue = 3;
        }
    }
    
    public class Chain : Armor
    {
        public Chain()
        {
            armorValue = 5;
        }
    }
}
