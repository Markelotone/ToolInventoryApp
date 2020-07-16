using System;
namespace IFN564_Assignment1
{
    // This class was made to clean up the code in the InventorySystem class, this class will repeat prompts and check errors of user input values
    public static class Helper
    {
        public static int ReadIntegerFromConsole(string prompt, string errorPrompt, int[] acceptableValues)
        {
            int value;
            Console.Write(prompt);
            do
            {
                string inputValue = Console.ReadLine();
                if (!Int32.TryParse(inputValue, out value))
                {
                    value = getLargestValue(acceptableValues) + 1;
                }
                if (!valueExists(acceptableValues, value))
                {
                    Console.Write(errorPrompt);
                }
            } while (!valueExists(acceptableValues, value));

            return value;
        }


        public static string ReadStringFromConsole(string prompt, string errorPrompt)
        {
            string value;
            do
            {
                Console.Write(prompt);
                value = Console.ReadLine();
                value = value.Trim();
                if (value.Length == 0)
                {
                    Console.Write(errorPrompt);

                }
            } while (value.Length == 0);

            return value;
        }

        private static bool valueExists(int[] valueList, int value)
        {
            foreach (var v in valueList)
            {
                if (v == value)
                {
                    return true;
                }
            }

            return false;
        }

        private static int getLargestValue(int[] valueList)
        {
            int largest = valueList[0];
            for (int i = 1; i < valueList.Length; i++)
            {
                if (valueList[i] > largest)
                {
                    largest = valueList[i];
                }
            }

            return largest;
        }
    }
}
