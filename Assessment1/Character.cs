using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    /// <summary>
    /// This is a class for all of the "Characters" that have health and are the gladiators in the game as well as the final boss.
    /// </summary>
    class Character
    {
        public int healthTotal = 0;
        public int accuracyTotal = 0;
        public int armorTotal = 0;
        public int damageTotal = 0;
        public int sandEaten = 0;
    }
    class Enemy : Character
    {
        public Enemy()
        {
            healthTotal = 85;
            accuracyTotal = 0;
            armorTotal = 0;
            damageTotal = 0;
            sandEaten = 0;
        }
    }
    class Player : Character
    {
        public Player()
        {
            healthTotal = 100;
            accuracyTotal = 0;
            armorTotal = 2;
            damageTotal = 0;
            sandEaten = 0;
        }
    }
    class Slime : Character
    {
        public Slime()
        {
            healthTotal = 200;
            accuracyTotal = 2;
            armorTotal = 10;
            damageTotal = 30;
            sandEaten = 0;
        }
    }
}
