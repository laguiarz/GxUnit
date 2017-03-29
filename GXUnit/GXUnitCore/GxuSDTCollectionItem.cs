/*
 * 2017-03-27   LAZ Created to support a complex-type collection-item in an SDT 
 * 
 */

 using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class GxuSDTCollectionItem
    {
        private String name;
        private Constants.GxuDataType dataType;
        private String complexTypeName;
        private int length;

        public GxuSDTCollectionItem()
        { 
        }

        public GxuSDTCollectionItem(String name, Constants.GxuDataType dataType, int length)
        {
            this.name     = name;
            this.dataType = dataType;
            this.length   = length;
        }

        public GxuSDTCollectionItem(String name, Constants.GxuDataType dataType, String complexDataType)
        {
            this.name = name;
            this.dataType = dataType;
            this.complexTypeName = complexDataType;
        }

        public String Name
        {
            get { return name; }
            set { name = value;}
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

        public String ComplexDataType
        {
            get { return complexTypeName; }
            set { complexTypeName = value; }
        }

    }
}
