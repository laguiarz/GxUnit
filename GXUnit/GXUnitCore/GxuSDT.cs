/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 */

using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class GxuSDT
    {

        String name;
        String parentName;

        GxuSDTLevel root;
        LinkedList<GxuSDTLevel> levels;
        private LinkedList<GxuProperty> properties;

        public GxuSDT(String name)
        {
            this.name = name;
        }

        public GxuSDT(String name, String parentName, LinkedList<GxuSDTLevel> levels, GxuSDTLevel root, LinkedList<GxuProperty> properties)
        {
            this.name           = name;
            this.parentName     = parentName;
            this.levels         = levels;
            this.root           = root;
            this.properties     = properties;
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String ParentName
        {
            get { return parentName; }
            set { parentName = value; }
        }

        public LinkedList<GxuSDTLevel> GetNiveles()
        {
            if (this.levels == null)
                return this.root.GetSubLevels();
             else
            return this.levels;
        }

        public GxuSDTLevel Root
        {
            get { return this.root; }
            set { root = value; }
        }

        public LinkedList<GxuProperty> Propiedades
        {
           get{ return this.properties; }
           set { properties = value; }
        }

    }
}
