using System;
using System.Collections.Generic;

namespace IFN564_Assignment1
{
    public class Person
    {
        private string _fullName;
        private string _phoneNumber;
        private LinkedList<Tool> _tools;
        public Person(string fullName, string phoneNumber)
        {
            if (fullName.Length <= 0)
            {
                throw new Exception("person name cannot be empty");
            }

            if (phoneNumber.Length <= 0)
            {
                throw new Exception("person phone number cannot be empty");
            }

            this._fullName = fullName;
            this._phoneNumber = phoneNumber;
            this._tools = new LinkedList<Tool>();
        }

        public string FullName()
        {
            return this._fullName;
        }

        public string PhoneNumber()
        {
            return this._phoneNumber;
        }

        public ref LinkedList<Tool> Tools()
        {
            return ref this._tools;
        }

        public void RentATool(Tool tool)
        {
            if (tool == null)
            {
                throw new Exception("tool is required to perform rent operation");
            }

            this._tools.AddLast(tool);
        }

        public bool ToolExists(Tool tool)
        {
            foreach (var t in this._tools)
            {
                if (tool.Name().ToUpper() == t.Name().ToUpper() && tool.ToolType() == t.ToolType())
                {
                    return true;
                }
            }

            return false;
        }

        public void IncrementToolQuantity(Tool tool)
        {
            foreach (var t in this._tools)
            {
                if (tool.Name().ToUpper() == t.Name().ToUpper() && tool.ToolType() == t.ToolType())
                {
                    t.IncrementQuantity();
                }
            }
        }
    }
}
