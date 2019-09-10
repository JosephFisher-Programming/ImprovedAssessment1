using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    /// <summary>
    /// This is a class for all of the weapons that are used by the characters.
    /// </summary>
    class Weapon
    {
        public int damage = 0;
        public int armorPenetration = 0;
        public int fleshDamageBonus = 0;
        public int accuracy = 0;
    }
    class Fists : Weapon
    {
        public Fists()
        {
            damage = 10;
            armorPenetration = 1;
            fleshDamageBonus = 5;
            accuracy = 95;
        }

    }
    class Dagger : Weapon
    {
        public Dagger()
        {
            damage = 20;
            armorPenetration = 1;
            fleshDamageBonus = 25;
            accuracy = 6;
        }
        
    }
    class Sword : Weapon
    {
        public Sword()
        {
            damage = 25;
            armorPenetration = 2;
            fleshDamageBonus = 15;
            accuracy = 5;
        }
    }
    class Trident : Weapon
    {
        public Trident()
        {
            damage = 30;
            armorPenetration = 4;
            fleshDamageBonus = 5;
            accuracy = 4;
        }
    }
}
