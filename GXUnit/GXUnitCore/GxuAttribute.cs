/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 * */
using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class GxuAttribute
    {
        private String name;
        private Constants.GxuDataType dataType;
        private bool isKey;
        private bool isReadOnly;

        public GxuAttribute(String name, Constants.GxuDataType type, bool isKey, bool isReadOnly)
        {
            this.name = name;
            this.dataType = type;
            this.isKey = isKey;
            this.isReadOnly = isReadOnly;
        }

        public String Name
        {
            get { return this.name;  }
            set {this.name = value;  }
        }

        public bool IsKey
        {
            get { return this.isKey; }
            set { this.isKey = value; }

        }

        public bool IsReadOnly
        {
            get { return this.isReadOnly; }
            set { this.isReadOnly = value; }

        }

        public Constants.GxuDataType DataType
        {
            get { return this.dataType; }
            set { this.dataType = value; }
        }

    }
}
