using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment1
{
    class Program
    {
        // Create all of the neccessary classes that I need.
        static Armor[] armors = new Armor[4];
        static Weapon[] weapons = new Weapon[4];
        static Character[] enemies = new Enemy[4];
        static Character slime = new Slime();
        static Character player = new Player();
        static StreamReader reader = new StreamReader("GameEvents.txt");
        static StreamWriter writer = new StreamWriter("Statistics.txt");
        // Create all of the public arrays I need for user input.
        static string[] weaponNames = new string[4] { "Fists", "Dagger", "Sword", "Trident" };
        static string[] enemyNames = new string[5] { "no one" , "1" , "2" , "3" , "4" };
        static bool weaponsNamed = false;
        static int enemyNameNum = 1;
        // This is a void to type out the start of the encounters with some story and descriptions.
        static void SetUpText()
        {
            string[] weaponTypes = new string[4] {"","piercing","slashing","blunt"};
            if (enemyNameNum < 5)
            {
                Console.WriteLine("What is the name of your opponent?");
                enemyNames[enemyNameNum] = Console.ReadLine();
                Console.Clear();
            }
            // The player creates the names of their weapons and will print them.
            if (weaponsNamed == false)
            {
                for (int i = 1; i < 4; i++)
                {
                    Console.WriteLine($"What would you like to name your {weaponTypes[i]} weapon?");
                    weaponNames[i] = Console.ReadLine();
                    Console.Clear();
                }
                weaponsNamed = true;
            }
            for (int i = 0; i < 4; i++)
            {
                string readLine = reader.ReadLine();
                Console.WriteLine(readLine);
            }
            Console.WriteLine($"Press 1 for the {weaponNames[1]} | Press 2 for the {weaponNames[2]} | Press 3 for the {weaponNames[3]}");
            enemyNameNum++;
        }
        // The losing game over.
        static void GameOver()
        {
            StreamReader statisticReader = new StreamReader("Statistics.txt");
            for (int i = 0; i < 3; i++)
            {
                string line = statisticReader.ReadLine();
                Console.WriteLine(line);
            }
            Console.WriteLine("Thank you for playing!");
            Console.ReadLine();
            statisticReader.Close();
        }
        // The winning game over.
        static void GameOver(int pHealth)
        {
            StreamReader statisticReader = new StreamReader("Statistics.txt");
            string line = statisticReader.ReadLine();
            Console.WriteLine($"You defeated the monstrosity and managed to defeat a total of {line} opponents and still have {pHealth} HP left!");
            Console.WriteLine("Thank you for playing!");
            Console.ReadLine();
            statisticReader.Close();
        }
        // Final gameplay loop that makes the player fight a slime and will keep looping until one of them dies.
        static void CombatTurns()
        {
            Random rd = new Random();
            while ((slime.healthTotal > 0) && (player.healthTotal > 0))
            {
                int playerAttackChoice = 0;
                int slimeAttackChoice = rd.Next(1, 3);
                // Asking the player what action they want to take, and then executing that action.
                Console.WriteLine($"The slime has {slime.healthTotal} health points remaining!\r\nYou have {player.healthTotal} health points remaining!\r\nYou see an opening in his defences!\r\nPress 1 to Attack || Press 2 to Pummel || Press 3 to Throw Sand!");
                Int32.TryParse(Console.ReadLine(), out playerAttackChoice);
                if ((playerAttackChoice == 1) || (playerAttackChoice == 2) || (playerAttackChoice == 3))
                {
                    // Basic attack that deals the most damage, but has a chance to miss depending on the accuracy of the damage.
                    if (playerAttackChoice == 1)
                    {
                        int hitDetection = rd.Next(1, player.accuracyTotal);
                        if (hitDetection != 5)
                        {
                            Console.WriteLine($"You hit the monstrosity for {player.damageTotal * (slime.sandEaten + 1)} DMG");
                            slime.healthTotal -= player.damageTotal * (slime.sandEaten + 1);
                            if (slime.sandEaten == 1)
                            {
                                Console.WriteLine("You start to deal much more damage than you did against the gladiators");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You missed your attack");
                        }
                    }
                    // A pummel attack that avoids the armor of the opponent.
                    if (playerAttackChoice == 2)
                    {
                        Console.WriteLine("You hit the behemoth for 20 DMG");
                        slime.healthTotal -= 20;
                    }
                    // A throwing sand attack that reduces their accuracy with a special property.
                    if (playerAttackChoice == 3)
                    {
                        Console.WriteLine("Throwing sand has thickened it self!");
                        slime.sandEaten++;
                    }
                }
                else
                {
                    Console.WriteLine("You fall flat on your face in front of everyone! Press Enter to continue");
                    player.healthTotal -= 5;
                    Console.ReadLine();
                }
                // An instant kill for the player, if the enemy has had sand thrown at him too many times.
                if (slime.sandEaten == 10)
                {
                    Console.WriteLine("The excessive amounts of sand has caused the slime to turn into a type of concrete! \r\n YOU HAVE DEFEATED IT!");
                    slime.healthTotal = 0;
                }
                // If the player doesnt choose a valid option, then they will fail and take damage as a punishment.
                
                // If the slime is alive then have them attack the player with a random attack.
                if (slime.healthTotal > 0)
                {
                    // The slime makes a basic attack against the player.
                    if (slimeAttackChoice == 1)
                    {
                        int hitDetection = rd.Next(1, slime.accuracyTotal);
                        if (hitDetection != 2)
                        {
                            Console.WriteLine($"It starts to devour you, dealing {slime.damageTotal} DMG!");
                            player.healthTotal -= slime.damageTotal;
                        }
                        else
                        {
                            Console.WriteLine("They missed their attack");
                        }
                    }
                    // The slime deals damage past your defence.
                    if (slimeAttackChoice == 2)
                    {
                        player.healthTotal -= 20;
                        Console.WriteLine("They try to suffocate you, dealing 20 DMG!");
                    }
                    // The slime sprays you reducing your accuracy.
                    if (slimeAttackChoice == 3)
                    {
                        Console.WriteLine("It shoots a volley of slime, impairing your eyesight!");
                    }
                }
            }
            Console.ReadLine();
        }
        //Base gameplay loop for the first 4 enemies with each one having different gear.
        static void CombatTurns(int enemyNum)
        {

            Random rd = new Random();

            while ((enemies[enemyNum].healthTotal > 0) && (player.healthTotal > 0))
            {
                int playerAttackChoice = 0;
                int enemyAttackChoice = rd.Next(1, 3);
                // Asking the player what action they want to take, and then executing that action.
                Console.WriteLine($"The enemy has {enemies[enemyNum].healthTotal} health points remaining!\r\nYou have {player.healthTotal} health points remaining!\r\nYou see an opening in his defences!\r\nPress 1 to Attack || Press 2 to Pummel || Press 3 to Throw Sand!");
                Int32.TryParse(Console.ReadLine(), out playerAttackChoice);
                if ((playerAttackChoice == 1) || (playerAttackChoice == 2) || (playerAttackChoice == 3))
                {
                    // Basic attack that deals the most damage, but has a chance to miss depending on the accuracy of the damage.
                    if (playerAttackChoice == 1)
                    {
                        int hitDetection = rd.Next(1, player.accuracyTotal);
                        if (hitDetection != 4)
                        {
                            Console.WriteLine($"You Hit them for {player.damageTotal} DMG");
                            enemies[enemyNum].healthTotal -= player.damageTotal;
                        }
                        else
                        {
                            Console.WriteLine("You missed your attack");
                        }
                    }
                    // A pummel attack that avoids the armor of the opponent.
                    if (playerAttackChoice == 2)
                    {
                        Console.WriteLine("You hit them for 20 DMG");
                        enemies[enemyNum].healthTotal -= 20;
                    }
                    // A throwing sand attack that reduces their accuracy with a special property.
                    if (playerAttackChoice == 3)
                    {
                        Console.WriteLine("Throwing sand was super effective");
                        enemies[enemyNum].sandEaten++;
                    }
                }
                else
                {
                    Console.WriteLine("You fall flat on your face in front of everyone! Press Enter to continue");
                    player.healthTotal -= 5;
                    Console.ReadLine();
                }
                // An instant kill for the player, if the enemy has had sand thrown at him too many times.
                if (enemies[enemyNum].sandEaten == 5)
                {
                    Console.WriteLine("You shove the sand so far down his throat, that they suffocate!");
                    enemies[enemyNum].healthTotal = 0;
                }
                // If the player doesnt choose a valid option, then they will fail and take damage as a punishment.

                // If the enemy is alive then have them attack the player with a random attack.
                if (enemies[enemyNum].healthTotal > 0)
                {
                    // The enemy makes a basic attack.
                    if (enemyAttackChoice == 1)
                    {

                        int hitDetection = rd.Next(1, enemies[enemyNum].accuracyTotal);
                        if (hitDetection != 4)
                        {
                            Console.WriteLine($"They hit you for {enemies[enemyNum].damageTotal} DMG!");
                            player.healthTotal -= enemies[enemyNum].damageTotal;
                        }
                        else
                        {
                            Console.WriteLine("They missed their attack");
                        }
                    }
                    // The enemy pummels past your defence.
                    if (enemyAttackChoice == 2)
                    {
                        player.healthTotal -= 20;
                        Console.WriteLine("They pummel you for 20 damage");
                    }
                    // The enemy reduces your accuracy.
                    if (enemyAttackChoice == 3)
                    {
                        Console.WriteLine("They throw sand in your face");
                    }
                }
            }
            if (player.healthTotal > 0)
            {
                Console.WriteLine("THE BATTLE HAS BEEN DECIDED. \r\nYOU ARE THE WINNER");
            }
            Console.ReadLine();
        }
        // This method assigns all of the arrays when the game starts up.
        static void arrayAssigning()
        {
            for (int i = 0; i < armors.Length; i++)
            {
                if (i == 0)
                {
                    armors[i] = new Flesh();
                }
                if (i == 1)
                {
                    armors[i] = new Leather();
                }
                if (i == 2)
                {
                    armors[i] = new Padded();
                }
                if (i == 3)
                {
                    armors[i] = new Chain();
                }
                Console.WriteLine(armors[i]);
            }
            for (int i = 0; i < weapons.Length; i++)
            {
                if (i == 0)
                {
                    weapons[i] = new Fists();
                }
                if (i == 1)
                {
                    weapons[i] = new Dagger();
                }
                if (i == 2)
                {
                    weapons[i] = new Sword();
                }
                if (i == 3)
                {
                    weapons[i] = new Trident();
                }
                Console.WriteLine(weapons[i]);
            }
            for (int i = 0; i < enemies.Length; i++)
            {
                int damageMod = 0;
                enemies[i] = new Enemy();
                enemies[i].damageTotal = weapons[i].damage + damageMod;
                damageMod += 2;
            }
        }
        // The main void and it contains the starting variable values for.
        static void Main(string[] args)
        {
            bool gameOver = false;
            bool newGamePlus = false;
            arrayAssigning();
            while (gameOver == false)
            {
                int weaponChoice = 0;
                int enemiesDefeated = 0;
                int playerDamageModifier = 0;             
                // Loop for the first 4 enemies that.
                while (gameOver == false)
                {
                    while (enemiesDefeated < 4 && player.healthTotal > 0)
                    {
                        Console.Clear();
                        SetUpText();
                        player.healthTotal = 100;
                        enemies[enemiesDefeated].armorTotal = armors[enemiesDefeated].armorValue;
                        Int32.TryParse(Console.ReadLine(), out weaponChoice);
                        // If the player does not choose a valid option, then punish them by not having a weapon.
                        if (weaponChoice != 1 && weaponChoice != 2 && weaponChoice != 3)
                        {
                            Console.WriteLine("Being a complete utter fool, you drop all of the weapons given to you and must fight with your hands now!");
                        }
                        try
                        {
                            weapons[weaponChoice] = weapons[weaponChoice];
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            weaponChoice = 0;
                        }
                        finally
                        {
                            // Assgns all of the current damages and also accuracy for each charactern that is in the arena.
                            player.damageTotal = weapons[weaponChoice].damage + (weapons[weaponChoice].fleshDamageBonus / enemies[enemiesDefeated].armorTotal) + playerDamageModifier;
                            enemies[enemiesDefeated].damageTotal = enemies[enemiesDefeated].damageTotal + (weapons[enemiesDefeated].fleshDamageBonus / player.armorTotal);
                            player.accuracyTotal = weapons[weaponChoice].accuracy;
                            enemies[enemiesDefeated].accuracyTotal = weapons[enemiesDefeated].accuracy;
                            playerDamageModifier += 2;
                            Console.WriteLine("You have chosen: " + weaponNames[weaponChoice] + "\r\nPress ENTER to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        CombatTurns(enemiesDefeated);
                        player.armorTotal += 2;
                        enemiesDefeated++;
                    }
                    // Check to see if the player is dead, if so then exit the game.
                    if ((player.healthTotal <= 0) && (gameOver == false))
                    {
                        Console.Clear();
                        writer.WriteLine($"You have died a tragic death at the hands of another gladiator. To be fair, you did take out {enemyNames[enemiesDefeated - 1]}.");
                        writer.WriteLine($"GAME OVER");
                        writer.Close();
                        GameOver();
                        gameOver = true;
                        break;
                    }
                    else
                    // The final fight against the boss.
                    {
                        Console.Clear();
                        SetUpText();
                        player.healthTotal = 100;
                        Int32.TryParse(Console.ReadLine(), out weaponChoice);
                        // If the player does not choose a valid option, then punish them by not having a weapon.
                        if ((weaponChoice) != 1 && (weaponChoice != 2) && (weaponChoice != 3))
                        {
                            Console.WriteLine("Being a complete utter fool, you drop all of the weapons given to you and must fight with your hands now!");
                        }
                        try
                        {
                            weapons[weaponChoice] = weapons[weaponChoice];
                        }
                        catch (IndexOutOfRangeException e)
                        {
                            weaponChoice = 0;
                        }
                        finally
                        {
                            // Assign the players damage and their weapon accuracy along with choice.
                            player.damageTotal = weapons[weaponChoice].damage + (weapons[weaponChoice].fleshDamageBonus / slime.armorTotal) + playerDamageModifier;
                            player.accuracyTotal = weapons[weaponChoice].accuracy;
                            Console.WriteLine(player.damageTotal);
                            Console.WriteLine("You have chosen: " + weaponNames[weaponChoice] + "\r\nPress ENTER to continue");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        CombatTurns();
                    }
                    // Check to see if the player is dead, if so then exit the game and have boss text.
                    if ((player.healthTotal <= 0) && (gameOver == false))
                    {
                        Console.Clear();
                        writer.WriteLine($"You have died at the hands of the colossal slime. You were the best fighter the world had seen.");
                        writer.WriteLine($"GAME OVER");
                        writer.WriteLine($"PRESS ENTER TO EXIT");
                        writer.Close();
                        GameOver();
                        gameOver = true;
                    }
                    // Check to see if the player is still alive and give them a special ending if they have beaten it once.
                    else if (player.healthTotal >= 0)
                    {
                        if (newGamePlus == true)
                        {
                            Console.Clear();
                            writer.WriteLine($"You have finally received vengance for all of your dead comrades and saved them as well.  You are now the most unstoppable being on the planet now");
                            writer.WriteLine($"GAME OVER");
                            writer.WriteLine($"PRESS ENTER TO EXIT");
                            writer.Close();
                            GameOver();
                            gameOver = true;
                        }
                        else
                        // If its their first time beating the boss, then reset it with a few changes.
                        {
                            Console.Clear();
                            writer.WriteLine($"You have defeated the slime and in the mass of the slime, you notice a stop watch.");
                            writer.WriteLine($"GAME OVER");
                            writer.WriteLine($"PRESS ENTER TO START NG+");
                            GameOver();
                            weapons[0].damage = 1000;
                            enemiesDefeated = 0;
                            for (int i = 0; i < 4; i++)
                            {
                                int damageMod = 0;
                                enemies[i] = new Enemy();
                                enemies[i].damageTotal = weapons[i].damage + damageMod;
                                damageMod += 2;
                            }
                            newGamePlus = true;
                            slime = new Slime();
                            break;
                        }
                    }
                }
            }
        }
    }
}