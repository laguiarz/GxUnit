/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 * */
using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class GxuProperty
    {
        private String name;
        private object propertyValue;

        public GxuProperty()
        { 
        }

        public GxuProperty(String name, Object value)
        {
            this.name = name;
            this.propertyValue = value;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public object PropertyValue
        {
            get { return this.propertyValue; }
            set { this.propertyValue = value; }
        }

    }
}
