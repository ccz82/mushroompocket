using System;

namespace MushroomPocket
{
    /// <summary>
    /// A list of valid Mushroom character names.
    /// </summary>
    public enum CharacterName
    {
        Waluigi,
        Daisy,
        Wario,
        Luigi,
        Peach,
        Mario
    }

    /// <summary>
    /// Base class for a Mushroom character.
    /// </summary>
    public class Character
    {
        /// <summary>
        /// The character's primary key.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The character's name, can only be one of the few
        /// defined in the CharacterName enum.
        /// </summary>
        public CharacterName Name { get; set; }

        /// <summary>
        /// The character's health, ranges from 0 to 100 inclusive.
        /// </summary>
        public int Hp { get; set; }

        /// <summary>
        /// The character's experience, ranges from 0 to 99 inclusive.
        /// A character with >= 100 EXP will level up and have its EXP
        /// reset back to 0.
        /// </summary>
        public int Exp { get; set; }

        /// <summary>
        /// A string that describes the character's special skill.
        /// </summary>
        public string Skill { get; set; }

        /// <summary>
        /// The character's level, increments by 1 when EXP rolls over 100.
        /// Starts at 1 when the character is created.
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// The character's attack damage in battles.
        /// </summary>
        public int Attack { get; set; }

        /// <summary>
        /// The character's defense against damage in battles.
        /// </summary>
        public int Defense { get; set; }

        /// <summary>
        /// The percentage damage multiplier when dealing critical
        /// damage in battle.
        /// </summary>
        public double CriticalMultiplier { get; set; }

        /// <summary>
        /// The percentage chance that the character deals critical
        /// damage in battle.
        /// </summary>
        public double CriticalChance { get; set; }

        /// <summary>
        /// Constructor for Character.
        /// </summary>
        /// <param name="name">
        /// The character's name, represented as a CharacterName enum.
        /// </param>
        /// <param name="hp">
        /// The character's HP.
        /// </param>
        /// <param name="exp">
        /// The character's EXP.
        /// </param>
        public Character(CharacterName name, int hp, int exp)
        {
            // Generate a GUID for primary key.
            Id = Guid.NewGuid().ToString();

            Name = name;
            Hp = hp;
            Exp = exp;

            // Every character starts at level 1.
            Level = 1;
        }

        /// <summary>
        /// Generate a random enemy character with 100 HP and 0 EXP.
        /// </summary>
        /// <returns>
        /// A randomly generated Character.
        /// </returns>
        public static Character CpuCharacter()
        {
            Random random = new Random();
            Array values = Enum.GetValues(typeof(CharacterName));
            CharacterName randomName = (CharacterName)values.GetValue(random.Next(values.Length));
            Type type = Type.GetType($"MushroomPocket.{randomName.ToString()}");
            return (Character)Activator.CreateInstance(type, 100, 0);
        }

        /// <summary>
        /// Print out the full information of a character.
        /// </summary>
        public void PrintInfo()
        {
            Console.WriteLine(new string('-', 25));
            Console.WriteLine($"Name: {Name.ToString()}");
            Console.WriteLine($"HP: {Hp}");
            Console.WriteLine($"EXP: {Exp}");
            Console.WriteLine($"Skill: {Skill}");
            Console.WriteLine($"Level: {Level}");
            Console.WriteLine($"Attack: {Attack}");
            Console.WriteLine($"Defense: {Defense}");
            Console.WriteLine($"Critical Multiplier: {CriticalMultiplier:P2}");
            Console.WriteLine($"Critical Chance: {CriticalChance:P2}");
            Console.WriteLine(new string('-', 25));
        }

        /// <summary>
        /// Read the character's username from standard input, ensuring it exists as a member of enum CharacterName.
        /// </summary>
        /// <returns>
        /// A valid character name of type CharacterName.
        /// </returns>
        public static CharacterName ReadCharacterName()
        {
            while (true)
            {
                Console.Write("Enter Character's Name: ");
                string name = Console.ReadLine();

                // We're working with enums, which are just symbolic constants (int) so reject integer input.
                if (int.TryParse(name, out int choice))
                {
                    Console.WriteLine("You did not enter a valid character's username. Please try again!");
                    continue;
                }

                // Check if the string input, can be parsed to a member of CharacterName.
                // Second argument in Enum.TryParse determines case sensitivity when comparing.
                if (!Enum.TryParse<CharacterName>(name.Trim(), true, out CharacterName characterName))
                {
                    Console.WriteLine("You did not enter a valid character's username. Please try again!");
                    continue;
                }

                return characterName;
            }
        }

        /// <summary>
        /// Read the character's HP from standard input, ensuring it is between 0-100 inclusive.
        /// </summary>
        /// <returns>
        /// The HP of the character to be added.
        /// </returns>
        public static int ReadCharacterHp()
        {
            while (true)
            {
                Console.Write("Enter Character's HP: ");

                if (!int.TryParse(Console.ReadLine().Trim(), out int hp))
                {
                    Console.WriteLine("Enter an integer. Please try again!");
                    continue;
                }

                if (hp < 0)
                {
                    Console.WriteLine("You did not enter a positive integer. Please try again!");
                    continue;
                }
                else if (hp > 100)
                {
                    Console.WriteLine("Maximum allowed HP is 100. Please try again!");
                    continue;
                }

                return hp;
            }
        }

        /// <summary>
        /// Read the character's EXP from standard input, ensuring it is between 0-99 inclusive.
        /// </summary>
        /// <returns>
        /// The EXP of the character to be added.
        /// </returns>
        public static int ReadCharacterExp()
        {
            while (true)
            {
                Console.Write("Enter Character's EXP: ");

                if (!int.TryParse(Console.ReadLine().Trim(), out int exp))
                {
                    Console.WriteLine("Enter an integer. Please try again!");
                    continue;
                }

                if (exp < 0)
                {
                    Console.WriteLine("You did not enter a positive integer. Please try again!");
                    continue;
                }
                else if (exp > 99)
                {
                    Console.WriteLine("Maximum allowed EXP is 99. Please try again!");
                    continue;
                }

                return exp;
            }
        }

        /// <summary>
        /// Check if the character is still alive.
        /// </summary>
        /// <returns>
        /// True if the character is alive,
        /// otherwise false.
        /// </returns>
        public bool IsAlive()
        {
            return Hp > 0;
        }

        /// <summary>
        /// Heal the character to full HP.
        /// </summary>
        public void HealToFullHp()
        {
            Hp = 100;
        }

        /// <summary>
        /// Calculate the amount of damage the character
        /// deals to the enemy in battle.
        /// </summary>
        /// <returns>
        /// The amount of damage to deal to enemy character.
        /// </returns>
        public int CalculateDamage()
        {
            int totalDamage = Attack;

            Random random = new Random();
            if (random.NextDouble() <= CriticalChance)
            {
                totalDamage += (int)(totalDamage * CriticalMultiplier);
            }

            return totalDamage;
        }

        /// <summary>
        /// Calculate the amount of defense the character
        /// has against damage from the enemy in battle.
        /// </summary>
        /// <param name="defend">
        /// If true, double the defense.
        /// Otherwise, do not double the defense.
        /// </param>
        /// <returns>
        /// The amount of defense against damage dealt for this character.
        /// </returns>
        private int CalculateDefense(bool defend)
        {
            int totalDefense = Defense;

            if (defend)
            {
                totalDefense *= 2;
            }

            return totalDefense;
        }

        /// <summary>
        /// Take damage from an attack.
        /// </summary>
        /// <param name="damage">
        /// The amount of damage to take.
        /// </param>
        /// <param name="defend">
        /// If true, double the defense.
        /// Otherwise, do not double the defense.
        /// </param>
        /// <returns>
        /// The calculated pure (actual) damage taken.
        /// </returns>
        public int TakeDamage(int damage, bool defend)
        {
            int pureDamageTaken = Math.Max(0, damage - CalculateDefense(defend));

            Hp -= pureDamageTaken;

            if (Hp < 0)
            {
                Hp = 0;
            }

            return pureDamageTaken;
        }

        /// <summary>
        /// Check if a character can be levelled up (EXP >= 100)
        /// and levels it up if it is allowed to do so.
        /// </summary>
        /// <returns>
        /// True, if the character successfully levelled up, otherwise
        /// false if the character did not level up.
        /// </returns>
        public bool TryLevelUp(out int previousLevel)
        {
            previousLevel = Level;

            bool levelledUp = false;

            while (Exp >= 100)
            {
                Exp -= 100;
                Level++;

                levelledUp = true;
            }

            return levelledUp;
        }
    }
}
