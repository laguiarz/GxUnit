/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed GetParentName function to get/set properties
 * */
 using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public abstract class GxuObject
    {
        private String name;

        public GxuObject(String name)
        {
            this.name = name;
        }

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }


    }
}
