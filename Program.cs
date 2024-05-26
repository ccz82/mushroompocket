using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MushroomPocket
{
    class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        static void Main(string[] args)
        {
            // MushroomMaster criteria list for checking character transformation availability.
            List<MushroomMaster> mushroomMasters = new List<MushroomMaster>() {
                new MushroomMaster("Daisy", 2, "Peach"),
                new MushroomMaster("Wario", 3, "Mario"),
                new MushroomMaster("Waluigi", 1, "Luigi")
            };

            // Assignment 1 Requirements
            using (GameContext context = new GameContext())
            {
                // Ensure the database is created.
                context.Database.EnsureCreated();

                // Initialize game loop.
                while (true)
                {
                    // Clear the console first.
                    Console.Clear();

                    // Print out the main menu.
                    PrintMainMenu();

                    // Get the user's choice in the main menu.
                    if (!TryReadChoice(7, "Please only enter ", out int choice))
                    {
                        return;
                    }
                    else
                    {
                        // Clear the main menu.
                        Console.Clear();

                        switch (choice)
                        {
                            case 1:
                                AddCharacter(context);
                                break;
                            case 2:
                                PrintCharacters(context, false); // Don't display the index to follow requirements
                                break;
                            case 3:
                                CheckTransformations(mushroomMasters, context);
                                break;
                            case 4:
                                TransformCharacters(mushroomMasters, context);
                                break;
                            case 5:
                                BattleAgainstCPU(context);
                                break;
                            case 6:
                                HealCharacters(context);
                                break;
                            case 7:
                                RemoveCharacters(context);
                                break;
                        }

                    }

                    // Wait for a keypress before going back to the main menu.
                    WaitForKeypress();
                }
            }
        }

        /// <summary>
        /// Waits for the user to press any key.
        /// Once any key is pressed, the console is cleared.
        /// </summary>
        /// <param name="message">
        /// The message to display when waiting for user to press the key.
        /// </param>
        private static void WaitForKeypress(string message = "Press any key to return back to menu...")
        {
            Console.WriteLine(message);
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Print the title screen to standard output.
        /// </summary>
        private static void PrintTitle()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            // A verbatim string is used here to tell the compiler
            // to ignore escape characters and line breaks.
            Console.WriteLine(@"
  __  __           _                               _____           _        _   
 |  \/  |         | |                             |  __ \         | |      | |  
 | \  / |_   _ ___| |__  _ __ ___   ___  _ __ ___ | |__) |__   ___| | _____| |_ 
 | |\/| | | | / __| '_ \| '__/ _ \ / _ \| '_ ` _ \|  ___/ _ \ / __| |/ / _ \ __|
 | |  | | |_| \__ \ | | | | | (_) | (_) | | | | | | |  | (_) | (__|   <  __/ |_ 
 |_|  |_|\__,_|___/_| |_|_|  \___/ \___/|_| |_| |_|_|   \___/ \___|_|\_\___|\__|
                                                                                ");
            Console.ResetColor();
        }

        /// <summary>
        /// Print the main menu to standard output.
        /// </summary>
        private static void PrintMainMenu()
        {
            PrintTitle();
            Console.WriteLine(new string('*', 32));
            Console.WriteLine("Welcome to Mushroom Pocket App");
            Console.WriteLine(new string('*', 32));
            Console.WriteLine("(1). Add Mushroom's character to my Pocket");
            Console.WriteLine("(2). List character(s) in my Pocket");
            Console.WriteLine("(3). Check if I can transform my characters");
            Console.WriteLine("(4). Transform character(s)");
            Console.WriteLine("(5). Battle against CPU");
            Console.WriteLine("(6). Heal character(s) in my Pocket");
            Console.WriteLine("(7). Remove character(s) in my Pocket");
        }

        /// <summary>
        /// Print the battle selection menu to standard output.
        /// </summary>
        private static void PrintBattleSelectionMenu()
        {
            Console.WriteLine(new string('*', 32));
            Console.WriteLine("BATTLE: Select a move!");
            Console.WriteLine(new string('*', 32));
            Console.WriteLine("(1). Attack");
            Console.WriteLine("(2). Defend");
            Console.WriteLine("(q/Q). Run away");
        }

        /// <summary>
        /// Print the character healing menu to standard output.
        /// </summary>
        private static void PrintCharacterHealingMenu()
        {
            Console.WriteLine(new string('*', 32));
            Console.WriteLine("Heal Character in Pocket");
            Console.WriteLine(new string('*', 32));
            Console.WriteLine("(1). Heal a character in my Pocket");
            Console.WriteLine("(2). Heal ALL characters in my Pocket");
        }

        /// <summary>
        /// Print the character removal menu to standard output.
        /// </summary>
        private static void PrintCharacterRemovalMenu()
        {
            Console.WriteLine(new string('*', 32));
            Console.WriteLine("Remove Character from Pocket");
            Console.WriteLine(new string('*', 32));
            Console.WriteLine("(1). Remove a character in my Pocket");
            Console.WriteLine("(2). Remove ALL characters in my Pocket");
        }

        /// <summary>
        /// Generate a List<char> from a given integer. Use the list as valid
        /// selections to get a choice from the user, with q/Q being stated as a choice to exit.
        /// </summary>
        /// <param name="length">
        /// The maximum number.
        /// </param>
        /// <param name="selectionMessage">
        /// The message to display when asking the user to select a choice.
        /// </param>
        /// <param name="choice">
        /// Returns the choice from 1 to length as an integer.
        /// If the user entered q/Q to exit, choice is set to 0.
        /// </param>
        /// <returns>
        /// True, if the user entered a valid choice, otherwise
        /// false, if the user entered q or Q.
        /// </returns>
        private static bool TryReadChoice(int length, string selectionMessage, out int choice)
        {
            // Generate a List<char> from integer that
            // represents the maximum number.
            List<char> allowedChars = new List<char>();

            for (int i = 1; i <= length; i++)
            {
                allowedChars.Add(i.ToString()[0]);
            }

            while (true)
            {
                Console.Write($"{selectionMessage}[");
                for (int i = 0; i < allowedChars.Count; i++)
                {
                    if (i == (allowedChars.Count - 1))
                    {
                        Console.Write($"{allowedChars[i]}");
                    }
                    else
                    {
                        Console.Write($"{allowedChars[i]},");
                    }
                }
                Console.Write("] or q/Q to quit: ");

                if (!char.TryParse(Console.ReadLine(), out char input))
                {
                    Console.WriteLine("You didn't enter an option. Please try again!");
                    continue;
                }

                if (!allowedChars.Contains(input) && char.ToLower(input) != 'q')
                {
                    Console.Write("The option you entered was not [");
                    for (int i = 0; i < allowedChars.Count; i++)
                    {
                        if (i == (allowedChars.Count - 1))
                        {
                            Console.Write($"{allowedChars[i]}");
                        }
                        else
                        {
                            Console.Write($"{allowedChars[i]},");
                        }
                    }
                    Console.Write("] or q/Q. Please try again!\n");
                    continue;
                }

                if (input == 'q')
                {
                    choice = 0;
                    return false;
                }
                else
                {
                    choice = int.Parse(input.ToString());
                    return true;
                }
            }
        }

        /// <summary>
        /// Prompt user for creating a character, adding to the database after successful creation.
        /// </summary>
        /// <param name="context">
        /// The database context instance.
        /// </param>
        private static void AddCharacter(GameContext context)
        {
            // Get the character username, HP and EXP.
            CharacterName name = Character.ReadCharacterName();
            int hp = Character.ReadCharacterHp();
            int exp = Character.ReadCharacterExp();

            // Initialize the character and add it to the database.
            Type type = Type.GetType($"MushroomPocket.{name.ToString()}");
            Character character = (Character)Activator.CreateInstance(type, hp, exp);
            context.Characters.Add(character);

            // Print success message.
            Console.WriteLine($"{name.ToString()} has been added.");

            // Save changes to the database.
            context.SaveChanges();
        }

        /// <summary>
        /// Sorts all the characters in the database by HP in descending
        /// order and returns a sorted List<Character>.
        /// </summary>
        /// <param name="context">
        /// The database context instance.
        /// </param>
        /// <returns>
        /// A sorted List<Character> by HP in descending order, otherwise
        /// an empty List<Character> if no characters are found.
        /// </returns>
        private static List<Character> SortCharactersByHp(GameContext context)
        {
            if (context.Characters.Any())
            {
                // Sort by HP in descending order.
                List<Character> sorted = context.Characters
                    .OrderByDescending(character => character.Hp)
                    .ToList();

                return sorted;
            }
            else
            {
                return new List<Character>();
            }
        }

        /// <summary>
        /// Print out a list of all the characters and their information,
        /// in descending order sorted by HP.
        /// </summary>
        /// <param name="context">
        /// The database context instance.
        /// </param>
        /// <param name="displayIndex">
        /// Whether or not to display the index of the character.
        /// </param>
        private static void PrintCharacters(GameContext context, bool displayIndex)
        {
            List<Character> sorted = SortCharactersByHp(context);

            if (sorted.Any())
            {
                if (displayIndex)
                {
                    for (int i = 0; i < sorted.Count; i++)
                    {
                        Console.WriteLine($"Character {i + 1}:");
                        sorted[i].PrintInfo();
                        Console.WriteLine();
                    }
                }
                else
                {
                    foreach (Character character in sorted)
                    {
                        character.PrintInfo();
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no characters available!");
            }
        }

        /// <summary>
        /// Check if there are any available transformation of characters possible.
        /// Prints out the available transformations to standard output, if there
        /// are no transformations available, prints out a message.
        /// </summary>
        /// <param username="mushroomMasters">
        /// The List containing character transformation rules.
        /// </param>
        /// <param username="context">
        /// The database context instance.
        /// </param>
        private static void CheckTransformations(List<MushroomMaster> mushroomMasters, GameContext context)
        {
            if (context.Characters.Any())
            {
                bool transformationFound = false;

                foreach (MushroomMaster mushroomMaster in mushroomMasters)
                {
                    List<Character> initialCharacters = context.Characters
                        .ToList() // Client-side evaluation, hence double call to ToList()
                        .Where(character => character.Name.ToString() == mushroomMaster.Name)
                        .ToList();

                    // Print out every available transformation until the number of occurrences is less than the NoToTransform.
                    for (int i = initialCharacters.Count; i >= mushroomMaster.NoToTransform; i -= mushroomMaster.NoToTransform)
                    {
                        Console.WriteLine($"{mushroomMaster.Name} --> {mushroomMaster.TransformTo}");
                        transformationFound = true;
                    }
                }

                if (!transformationFound)
                {
                    Console.WriteLine("There is currently no transformation available.");
                }
            }
            else
            {
                Console.WriteLine("There are no characters available!");
            }
        }

        /// <summary>
        /// Transform all transformable characters.
        /// If there are no transformable characters, prints out a message
        /// to tell the user there is no transformation available.
        /// </summary>
        /// <param username="mushroomMasters">
        /// The List containing character transformation rules.
        /// </param>
        /// <param username="context">
        /// The database context instance.
        /// </param>
        private static void TransformCharacters(List<MushroomMaster> mushroomMasters, GameContext context)
        {
            if (context.Characters.Any())
            {
                bool transformationFound = false;

                foreach (MushroomMaster mushroomMaster in mushroomMasters)
                {
                    List<Character> initialCharacters = context.Characters
                        .ToList() // Client-side evaluation, hence double call to ToList()
                        .Where(character => character.Name.ToString() == mushroomMaster.Name)
                        .ToList();

                    // Iterate out every available transformation until the number of occurrences is less than the NoToTransform.
                    for (int i = initialCharacters.Count; i >= mushroomMaster.NoToTransform; i -= mushroomMaster.NoToTransform)
                    {
                        // Perform transformation by removing the initial characters.
                        context.Characters.RemoveRange(initialCharacters.Take(mushroomMaster.NoToTransform));

                        // Finally, create the newly transformed character and add it to the database.
                        // The newly transformed character has 100 HP and 0 EXP.
                        Type type = Type.GetType($"MushroomPocket.{mushroomMaster.TransformTo}");
                        Character character = (Character)Activator.CreateInstance(type, 100, 0);
                        context.Characters.Add(character);

                        // Save changes to the database.
                        context.SaveChanges();

                        Console.WriteLine($"Transformed {mushroomMaster.Name} --> {mushroomMaster.TransformTo}");

                        transformationFound = true;
                    }
                }

                if (!transformationFound)
                {
                    Console.WriteLine("There is currently no transformation available.");
                }
            }
            else
            {
                Console.WriteLine("There are no characters available!");
            }
        }

        /// <summary>
        /// Start a new battle against the CPU.
        /// </summary>
        /// <param username="context">
        /// The database context instance.
        /// </param>
        private static void BattleAgainstCPU(GameContext context)
        {
            if (context.Characters.Any())
            {
                PrintCharacters(context, true); // Display the index of the characters

                if (!TryReadChoice(context.Characters.Count(), "Please select a character from ", out int selection))
                {
                    Console.WriteLine("Quitting battle!");
                }
                else
                {
                    Character player = context.Characters.ToList()[selection - 1];

                    if (!player.IsAlive())
                    {
                        Console.WriteLine($"Your {player.Name.ToString()} is not alive! Go heal it first before battling.");
                    }
                    else
                    {
                        Console.WriteLine($"You selected {player.Name.ToString()}!");

                        Character cpu = Character.CpuCharacter();
                        Console.WriteLine($"You are battling the CPU's {cpu.Name.ToString()}!");

                        WaitForKeypress("Press any key to continue...");
                        Console.Clear();

                        while (player.IsAlive() && cpu.IsAlive())
                        {
                            int playerDamage = player.Attack;
                            bool playerDefend = false;

                            int cpuDamage = (int)(cpu.Attack * 0.6);
                            bool cpuDefend = false;

                            Console.WriteLine($"Your {player.Name.ToString()}");
                            Console.WriteLine($"HP: {player.Hp}/100\n");

                            Console.WriteLine($"CPU's {cpu.Name.ToString()}");
                            Console.WriteLine($"HP: {cpu.Hp}/100\n");

                            PrintBattleSelectionMenu();

                            if (!TryReadChoice(2, "Choose an option from ", out int option))
                            {
                                Console.WriteLine("Running away from this battle!");
                                break;
                            }
                            else
                            {
                                switch (option)
                                {
                                    case 1:
                                        playerDamage = player.CalculateDamage();
                                        break;
                                    case 2:
                                        playerDefend = true;
                                        break;
                                }
                            }

                            Random random = new Random();

                            if (random.NextDouble() < 0.75)
                            {
                                cpuDamage = cpu.CalculateDamage();
                            }
                            else
                            {
                                cpuDefend = true;
                            }

                            int cpuPureDamageTaken = cpu.TakeDamage(playerDamage, cpuDefend);
                            if (cpuPureDamageTaken > 0)
                            {
                                Console.WriteLine($"The CPU's {cpu.Name.ToString()} took {cpuPureDamageTaken} damage from your {player.Name.ToString()}!");
                            }
                            else
                            {
                                Console.WriteLine($"The CPU's {cpu.Name.ToString()} just stood there like a sigma.");
                            }

                            int playerPureDamageTaken = player.TakeDamage(cpuDamage, playerDefend);
                            if (playerPureDamageTaken > 0)
                            {
                                Console.WriteLine($"Your {player.Name.ToString()} took {playerPureDamageTaken} damage from the CPU's {cpu.Name.ToString()}!");
                            }
                            else
                            {
                                Console.WriteLine($"Your {player.Name.ToString()} just stood there like a sigma.");
                            }

                            WaitForKeypress("Press any key to continue...");
                            Console.Clear();
                        }

                        if (player.IsAlive() && cpu.IsAlive())
                        {
                            return;
                        }
                        else if (player.IsAlive())
                        {
                            Console.WriteLine("You won the battle.");

                            player.Exp += new Random().Next(100, 250);
                            if (player.TryLevelUp(out int previousLevel))
                            {
                                Console.WriteLine($"Your {player.Name.ToString()} levelled up from level {previousLevel} to {player.Level}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("You lost the battle.");

                            player.Exp += new Random().Next(10, 75);
                            if (player.TryLevelUp(out int previousLevel))
                            {
                                Console.WriteLine($"Your {player.Name.ToString()} levelled up from level {previousLevel} to {player.Level}");
                            }
                        }
                    }

                    // Update the database with the newly updated character.
                    context.Characters.Update(player);
                    context.SaveChanges();
                }
            }
            else
            {
                Console.WriteLine("There are no characters available!");
            }
        }

        /// <summary>
        /// Heal a character or all characters from the database.
        /// </summary>
        /// <param username="context">
        /// The database context instance.
        /// </param>
        private static void HealCharacters(GameContext context)
        {
            if (context.Characters.Any())
            {
                PrintCharacterHealingMenu();

                if (!TryReadChoice(2, "Please only enter ", out int choice))
                {
                    Console.WriteLine("Quitting character healing menu!");
                }
                else
                {
                    switch (choice)
                    {
                        case 1:
                            PrintCharacters(context, true); // Display the index of the characters

                            if (!TryReadChoice(context.Characters.Count(), "Please select a character from ", out int selection))
                            {
                                Console.WriteLine("Quitting character healing menu!");
                            }
                            else
                            {
                                Character selectedCharacter = context.Characters.ToList()[selection - 1];
                                selectedCharacter.HealToFullHp();

                                context.Characters.Update(selectedCharacter);

                                Console.WriteLine($"Successfully healed {selectedCharacter.Name}.");

                                context.SaveChanges();
                            }

                            break;
                        case 2:
                            foreach (Character character in context.Characters)
                            {
                                character.HealToFullHp();
                            }

                            Console.WriteLine("Successfully healed all characters!");

                            context.SaveChanges();

                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no characters available!");
            }
        }

        /// <summary>
        /// Remove a character or all characters from the database.
        /// </summary>
        /// <param username="context">
        /// The database context instance.
        /// </param>
        private static void RemoveCharacters(GameContext context)
        {
            if (context.Characters.Any())
            {
                PrintCharacterRemovalMenu();

                if (!TryReadChoice(2, "Please only enter ", out int choice))
                {
                    Console.WriteLine("Quitting character removal menu!");
                }
                else
                {
                    switch (choice)
                    {
                        case 1:
                            PrintCharacters(context, true); // Display the index of the characters

                            if (!TryReadChoice(context.Characters.Count(), "Please select a character from ", out int selection))
                            {
                                Console.WriteLine("Quitting character removal menu!");
                            }
                            else
                            {
                                context.Characters.Remove(context.Characters.ToList()[selection - 1]);
                                Console.WriteLine($"Successfully removed {context.Characters.ToList()[selection - 1].Name.ToString()}.");
                                context.SaveChanges();
                            }

                            break;
                        case 2:
                            context.Characters.ExecuteDelete(); // Executes immediately against the database, SavesChanges() not required.
                            Console.WriteLine("Successfully removed all characters!");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no characters available!");
            }
        }
    }
}
