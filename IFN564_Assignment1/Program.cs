using System;

namespace IFN564_Assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice = 5;
            InventorySystem inventorySystem = new InventorySystem();

            // This AddFewTools() function has been added in the main class, so that the sysem has some data before running.
            // 2 lawn mowers, 2 ladders in garden tool type and 2 shovels will be in flooring type just FYI.
            // You may decide to comment out this function to have an EMPTY data set for the system. 
            inventorySystem.AddFewTools();

            // Following code is to catch any errors, if the user enters a number less than 1 or more than 5
            // or for any character that is not 1-5

            do {
                Console.Write("\n\n+------------------------------------------------+\n" +
                          "| INVENTORY MANAGEMENT SYSTEM FOR A TOOL LIBRARY |\n" +
                          "+------------------------------------------------+\n" +
                                  " 1. Add a new tool\n" +
                                  " 2. Rent a tool\n" +
                                  " 3. Return a tool\n" +
                                  " 4. Show tools status\n" +
                                  " 5. Exit\n" +
                                  " Make a choice(1-5): ");
                string choiceInput = Console.ReadLine();
                if (!Int32.TryParse(choiceInput, out choice))
                {
                    Console.Write("\n [ Non numeric choice ]\n\n");
                }
                else
                {
                    try
                    {
                        inventorySystem.MakeAChoice(choice);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n "+e.Message+"\n\n");
                        choice = 0;
                    }
                }
            } while (choice != 5);
            Console.WriteLine("\nThank you for using our system...");
            
        }
    }
}
