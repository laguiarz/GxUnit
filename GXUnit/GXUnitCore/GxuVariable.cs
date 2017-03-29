/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 * */

using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class GxuVariable
    {

        private String name;
        private Constants.GxuDataType dataType;
        private String complexTypeName = null;
        private int length;
        private int decimals;
        private bool isCollection;

        public GxuVariable()
        {
        }

        public GxuVariable(String name, Constants.GxuDataType dataType)
        {
            this.name = name;
            this.dataType = dataType;
        }


        public GxuVariable(String name, Constants.GxuDataType dataType, int length, int decimals)
        {
            this.name       = name;
            this.dataType   = dataType;
            this.length     = length;
            this.decimals   = decimals;
            
        }

        public GxuVariable(String name, Constants.GxuDataType dataType, String complexTypeName)
        {
            this.name = name;
            this.dataType = dataType;
            this.complexTypeName = complexTypeName;

        }

        public GxuVariable(String name, String complexTypeName)
        {
            this.name = name;
            this.complexTypeName = complexTypeName;

        }

        public GxuVariable(String name, String complexTypeName, bool isCollection)
        {
            this.name = name;
            this.complexTypeName = complexTypeName;
            this.isCollection = isCollection;

        }

        public GxuVariable(String name, Constants.GxuDataType dataType, String complexTypeName, int length, int decimals)
        {
            this.name = name;
            this.dataType = dataType;
            this.complexTypeName = complexTypeName;
            this.length = length;
            this.decimals = decimals;

        }

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public Constants.GxuDataType DataType
        { 
           get { return this.dataType; }
            set { this.dataType = value; }
        }

        public String  ComplexTypeName
        {
           get { return this.complexTypeName; }
           set { this.complexTypeName = value;  }
        }

        public int Length
        {
           get { return this.length; }
            set { this.length = value; }
        }

        public int Decimals
        {
           get { return this.decimals; }
            set { this.decimals = value; }
        }

        public bool IsCollection
        {
           get { return this.isCollection; }
            set { this.isCollection = value; }
        }
    }
}
