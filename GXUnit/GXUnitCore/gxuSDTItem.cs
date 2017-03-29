/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 */

using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class GxuSDTItem
    {
        private String name;
        private Constants.GxuDataType dataType;
        private String complexTypeName;
        private int length;
        private bool isCollection;

        public GxuSDTItem()
        { 
        }

        public GxuSDTItem(String name, Constants.GxuDataType dataType, int length)
        {
            this.name     = name;
            this.dataType = dataType;
            this.length   = length;
        }

        public GxuSDTItem(String name, Constants.GxuDataType dataType, String complexTypeName)
        {
            this.name           = name;
            this.dataType       = dataType;
            this.complexTypeName = complexTypeName;
        }

        public GxuSDTItem(String name, Constants.GxuDataType dataType, String complexTypeName, bool isCollection)
        {
            this.name = name;
            this.dataType = dataType;
            this.complexTypeName = complexTypeName;
            this.isCollection = isCollection;
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public Constants.GxuDataType DataType
        {
            get { return dataType; }
            set { dataType = value; }
        }

       public int Length
        {
            get { return length; }
            set { length = value; }

        }

        public String ComplexTypeName
        {
            get { return complexTypeName; }
            set { complexTypeName = value; }
        }

       public bool IsCollection
        {
            get { return isCollection; }
            set { isCollection = value; }

        }

  
    }
}
