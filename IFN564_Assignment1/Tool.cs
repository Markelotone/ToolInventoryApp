using System;
namespace IFN564_Assignment1
{
    // Tool class will hold 4 attributes as requested per the assessement requirements
    public class Tool
    {
        private string _name;
        private string _description;
        private int _qty;
        private TToolType _toolType;

        public Tool(string name, TToolType toolType, int qty, string description)
        {
            if (name.Length <= 0)
            {
                throw new Exception("tool name cannot be empty");
            }
            if (qty < 0)
            {
                throw new Exception("tool qty cannot be negative");
            }
            if (description.Length <= 0)
            {
                throw new Exception("tool description cannot be empty");
            }

            this._name = name;
            this._toolType = toolType;
            this._qty = qty;
            this._description = description;
        }

        public string Name()
        {
            return this._name;
        }

        public TToolType ToolType()
        {
            return this._toolType;
        }

        public string Description()
        {
            return this._description;
        }

        public int Quantity()
        {
            return this._qty;
        }

        // Staff will be able to this method in order to add a new tool to the inventory management system
        public int IncrementQuantity()
        {
            this._qty += 1;
            return this._qty;
        }

        // Staff will use this method in order to rent out a tool to a person
        public int DecrementQuantity()
        {
            if (this._qty - 1 < 0)
            {
                throw new Exception("not enough tools");
            }

            this._qty -= 1;

            return this._qty;
        }

        public string Stringify()
        {
            return String.Format("Name: {0}\n" +
                                 "Type: {1}\n" +
                                 "Available Quantity: {2}\n" +
                                 "Description: {3}", this._name, this._toolType, this._qty, this._description);
        }
    }
}
