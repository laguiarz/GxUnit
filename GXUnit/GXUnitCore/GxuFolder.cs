/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 * */
using System;


namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class GxuFolder : GxuObject
    {
        private String parentName;


        public GxuFolder(String name, String parentName)
            : base(name)
        {
            this.parentName = parentName;
        }

        public String ParentName
        {

            get
            {
                return parentName;
            }
            set
            {
                parentName = value;
            }
        }

    }
}
