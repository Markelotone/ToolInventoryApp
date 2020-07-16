using System;
using System.Linq;
using System.Collections.Generic;

namespace IFN564_Assignment1
{
    public class InventorySystem
    {
        LinkedList<Tool> _tools;
        LinkedList<Person> _people;

        public InventorySystem()
        {
            _tools = new LinkedList<Tool>();
            _people = new LinkedList<Person>();
        }

        // This function has been added in the main class, so that the sysem has some data before running.
        // 2 lawn mowers, 2 ladders in garden tool type and 2 shovels will be in flooring type just FYI
        // You may decide to comment out this function to have an EMPTY data set for the system. 
        public void AddFewTools()
        {
            _tools.AddLast(new Tool("lawn mower", TToolType.Gardening, 2, "This category is used for lawn mowers"));
            _tools.AddLast(new Tool("ladder", TToolType.Gardening, 2, "This category is used for ladders"));
            _tools.AddLast(new Tool("shovel", TToolType.Flooring, 2, "This category is used for shovels"));
        }

        public void MakeAChoice(int choice)
        {
            // error handling for choices other than 1-5

            if (choice < 1 || choice > 5)
            {
                throw new Exception("[ Invalid choice ]");
            }

            switch (choice)
            {
                //add a new tool
                case 1:
                    AddANewTool();
                    break;
                //rent a tool
                case 2:
                    RentANewTool();
                    break;
                //return a tool
                case 3:
                    ReturnARentedTool();
                    break;
                //display tool status
                case 4:
                    DisplayToolStatus();
                    break;
            }
        }

        /* This function adds a new tool in the inventory system */
        private void AddANewTool()
        {
            Console.WriteLine("\nAdd a new tool\n" +
                              "------------------");

            //read tool type
            string toolTypePrompt = "Select the tool type:\n";
            foreach (var v in Enum.GetValues(typeof(TToolType)))
            {
                toolTypePrompt += String.Format("\t{0}: {1}\n", (int)v, v);
            }
            toolTypePrompt += "Enter the tool type: ";
            int toolType = Helper.ReadIntegerFromConsole(toolTypePrompt, " [ Wrong choice ]\nEnter the tool type: ", new int[]{0,1,2,3,4,5,6,7,8});

            //read tool name
            string toolNamePromt = String.Format("Enter the {0} tool name: ", (TToolType)toolType);
            string toolName = Helper.ReadStringFromConsole(toolNamePromt, "[ Invalid name ]\n");

            //read tool description
            string toolDescriptionPromt = String.Format("Enter the {0} tool description: ", (TToolType)toolType);
            string toolDecription = Helper.ReadStringFromConsole(toolDescriptionPromt, "[ Invalid description ]\n");

            
            bool toolExists = false;
            //increate qty if tool already exists
            var currentNode = _tools.First; //gets the head of linkedlist
            while (currentNode != null)
            {
                var tool = currentNode.Value;
                if (tool.Name().ToUpper() == toolName.ToUpper() && tool.ToolType() == (TToolType)toolType)
                {
                    tool.IncrementQuantity();
                    toolExists = true;
                }
                currentNode = currentNode.Next;
            }
            //otherwise add the tool
            if (!toolExists)
            {
                this._tools.AddLast(new Tool(toolName, (TToolType)toolType, 1, toolDecription));
            }

            Console.WriteLine("\n[ A {0} has been added ]", toolName);
        }

        /* This function shows the available tool's status in the inventory system */
        private void DisplayToolStatus()
        {
            Console.WriteLine("\nTools staus\n" +
                              "---------------");

            Tool[] tools = new Tool[_tools.Count];

            //ask for tool type
            string toolTypePrompt = "Select the tool type:\n";
            foreach (var v in Enum.GetValues(typeof(TToolType)))
            {
                toolTypePrompt += String.Format("\t{0}: {1}\n", (int)v, v);
            }
            toolTypePrompt += "Enter the tool type: ";
            int toolType = Helper.ReadIntegerFromConsole(toolTypePrompt, " [ Wrong choice ]\nEnter the tool type: ", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8 });

            //search for the tool type details in linked list and add in the array
            var currentNode = _tools.First;
            int index = 0;
            while (currentNode != null)
            {
                var tool = currentNode.Value;
                if (tool.ToolType() == (TToolType)toolType)
                {
                    tools[index] = tool;
                    index += 1;
                }
                currentNode = currentNode.Next;
            }

            //remove the empty array space
            Array.Resize(ref tools, index);

            //sort the tools with tool name using bubble sort
            for (int i = 0; i < tools.Length - 1; i++)
            {
                for (int j = 0; j < (tools.Length - i - 1); j++)
                {
                    if (tools[j].Name().CompareTo(tools[j + 1].Name()) >= 1)
                    {
                        var temp = tools[j];
                        tools[j] = tools[j + 1];
                        tools[j + 1] = temp;
                    }
                }
            }

            Console.WriteLine("-------------------");
            if (tools.Length <= 0) // if no tool exists of provided type
            {
                Console.WriteLine("EMPTY");
            }
            else //otherise show tools details
            {
                for (int i = 0; i < tools.Length; i++)
                {
                    Console.WriteLine(tools[i].Stringify());
                    if (i < tools.Length - 1)
                    {
                        Console.WriteLine();
                    }
                }
            }
            Console.WriteLine("-------------------");
        }

        /* This function enables tool renting feature in the inventory system */
        private void RentANewTool()
        {
            Console.WriteLine("\nRent a tool\n" +
                              "------------------");

            //check if any tool exists
            string toolTypePrompt = "Select the tool type:\n";
            LinkedList<TToolType> availableToolTypeForRent = new LinkedList<TToolType>();
            var currentNode = _tools.First; //gets the head of linkedlist
            while (currentNode != null)
            {
                var t = currentNode.Value;
                if (t.Quantity() > 0 && !availableToolTypeForRent.Contains(t.ToolType()))
                {
                    toolTypePrompt += String.Format("\t{0}: {1}\n", (int)t.ToolType(), t.ToolType());
                    availableToolTypeForRent.AddLast(t.ToolType());
                }
                currentNode = currentNode.Next;
            }
            if (availableToolTypeForRent.Count <= 0)
            {
                Console.WriteLine("[ Tools not available for renting ]");
                return;
            }

            //ask for renters name
            string personFullName = Helper.ReadStringFromConsole("Enter the person's name: ", "[ Invalid name ]\n");

            //ask for renters phone number
            string personPhone = Helper.ReadStringFromConsole("Enter the person's phone number: ", "[ Invalid phone number ]");

            //ask for tool type
            toolTypePrompt += "Enter the tool type number: ";
            int toolType = Helper.ReadIntegerFromConsole(toolTypePrompt, " [ Wrong choice ]\nEnter the tool type number: ", getIntegerArray(availableToolTypeForRent));

            //ask for tool
            string toolNamePrompt = String.Format("Select the {0} tool:\n", (TToolType)toolType);
            int count = 0;
            LinkedList<Tool> availableToolsForRent = new LinkedList<Tool>();
            currentNode = _tools.First; //gets the head of linkedlist
            while (currentNode != null)
            {
                var t = currentNode.Value;
                if (t.ToolType() == (TToolType)toolType && t.Quantity() > 0)
                {
                    toolNamePrompt += String.Format("\t{0}: {1}\n", count, t.Name());
                    availableToolsForRent.AddLast(t);
                    count += 1;
                }
                currentNode = currentNode.Next;
            }
            toolNamePrompt += "Enter the tool name number: ";
            int toolIndex = Helper.ReadIntegerFromConsole(toolNamePrompt, " [ Wrong choice ]\nEnter the tool name number: ", constructIntegerArray(count));


            //complete renting process
            var tool = availableToolsForRent.ElementAt(toolIndex);
            bool personExists = false;
            //increase the rented tool quntity if the person has rent the same tool before

            var currentNodeP = _people.First;
            while(currentNodeP != null)
            {
                var p = currentNodeP.Value;
                if (p.FullName().ToUpper() == personFullName.ToUpper())
                {
                    personExists = true;

                    //increment the rented tool qty
                    if (p.ToolExists(tool))
                    {
                        p.IncrementToolQuantity(tool);
                        currentNode = _tools.First; //gets the head of linkedlist
                        while (currentNode != null)
                        {
                            var tool2 = currentNode.Value;
                            if (tool.ToolType() == tool2.ToolType() && tool.Name() == tool2.Name())
                            {
                                tool2.DecrementQuantity();
                            }
                            currentNode = currentNode.Next;
                        }
                    }
                    else
                    {
                        //rent the new tool
                        p.RentATool(new Tool(tool.Name(), tool.ToolType(), 1, tool.Description()));
                        currentNode = _tools.First; //gets the head of linkedlist
                        while (currentNode != null)
                        {
                            var tool2 = currentNode.Value;
                            if (tool.ToolType() == tool2.ToolType() && tool.Name() == tool2.Name())
                            {
                                tool2.DecrementQuantity();
                            }
                            currentNode = currentNode.Next;
                        }
                    }
                }
                currentNodeP = currentNodeP.Next;
            }

            //otherwise add the tool
            if (!personExists)
            {
                var person = new Person(personFullName, personPhone);
                person.RentATool(new Tool(tool.Name(), tool.ToolType(), 1, tool.Description()));
                this._people.AddLast(person);
                currentNode = _tools.First; //gets the head of linkedlist
                while (currentNode != null)
                {
                    var tool2 = currentNode.Value;
                    if (tool.ToolType() == tool2.ToolType() && tool.Name() == tool2.Name())
                    {
                        tool2.DecrementQuantity();
                    }
                    currentNode = currentNode.Next;
                }
            }

            Console.WriteLine("\n[ A {0} has been rented to {1} ] ", tool.Name(), personFullName);
        }


        public void ReturnARentedTool()
        {
            Console.WriteLine("\nReturn a tool\n" +
                              "------------------");

            //ask for renters name
            string personName = Helper.ReadStringFromConsole("Enter the person's name: ", "[ Invalid name ]\n");

            //check if this person exists
            var personExists = false;
            var personindex = -1;
            var currentNodeP = _people.First; //gets the head of linkedlist
            while (currentNodeP != null)
            {
                personindex += 1;
                var person = currentNodeP.Value;
                if (person.FullName().ToUpper() == personName.ToUpper())
                {
                    personExists = true;
                    break;
                }
                currentNodeP = currentNodeP.Next;
            }

            if (!personExists)
            {
                Console.WriteLine("\n[ {0} has not rented any tool ]", personName);
                return;
            }
            //otherwise proceed to returing process
            //ask for tool
            var renter = _people.ElementAt(personindex);
            var currentNodePT = renter.Tools().First; //gets the head of linkedlist
            int count = 0;
            string toolNamePrompt = "Select the tool that is returning: \n";
            while (currentNodePT != null)
            {
                var t = currentNodePT.Value;
                toolNamePrompt += String.Format("\t{0}: {1}\n", count, t.Name());
                currentNodePT = currentNodePT.Next;
                count += 1;
            }
            toolNamePrompt += "Enter the tool name number: ";
            int toolIndex = Helper.ReadIntegerFromConsole(toolNamePrompt, " [ Wrong choice ]\nEnter the tool name number: ", constructIntegerArray(count));

            //if qty is more than 1 then decrease the qty by 1 otherwise remove the renter record
            var tool = renter.Tools().ElementAt(toolIndex);
            
            //add the qty back into the system
            var currentNodeT = _tools.First; //gets the head of linkedlist
            while (currentNodeT != null)
            {
                var t = currentNodeT.Value;
                if (t.Name() == tool.Name() && t.ToolType() == tool.ToolType())
                {
                    t.IncrementQuantity();
                }
                currentNodeT = currentNodeT.Next;
            }

            //subtract the tool qty if there were more than 1 else remove from person
            if (tool.Quantity() > 1)
            {
                tool.DecrementQuantity();
                //check 
            } else if (renter.Tools().Count > 1)//there is another tool)
            {
                renter.Tools().Remove(tool);
            } else
            {
                _people.Remove(renter);
            }

            Console.WriteLine("\n[ The {0} has been returned to the system ] ", tool.Name());
        }

        private int[] getIntegerArray(LinkedList<TToolType> toolTypes)
        {
            int[] valueList = new int[toolTypes.Count];
            var currentNode = toolTypes.First; //gets the head of linkedlist
            int index = 0;
            while (currentNode != null)
            {
                valueList[index] = (int)currentNode.Value;
                currentNode = currentNode.Next;
            }

            return valueList;
        }

        private int[] constructIntegerArray(int Length)
        {
            int[] valueList = new int[Length];
            for (int i=0; i < Length; i++)
            {
                valueList[i] = i;
            }

            return valueList;
        }
    }
}
