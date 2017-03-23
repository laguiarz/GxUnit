using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    //Represents a level in an SDT (i.e. the root level)
    class GxuSDTLevel
    {
        private String name;                            //the level name
        private bool isCollection;                      //the level represents a collection?
        private LinkedList<GxuSDTLevel> subLevels;         //the sublevels of the level..if any
        private LinkedList<gxuSDTColItem> collectionItems;      //the items of the level (it should have either items or sublevels otherwise it is an empty SDT)
        private LinkedList<gxuSDTItem> items;

        public GxuSDTLevel()
        { 
        }

        public GxuSDTLevel(string name, bool isCollection)
        {
            this.name           = name;
            this.isCollection   = isCollection;
            this.subLevels      = new LinkedList<GxuSDTLevel>();
            this.items          = new LinkedList<gxuSDTItem>();
            this.collectionItems = new LinkedList<gxuSDTColItem>();    
        }

        public GxuSDTLevel( String name, LinkedList<gxuSDTItem> items, bool isCollection, LinkedList<GxuSDTLevel> subLevels)
        {
            this.name           = name;
            this.items          = items;
            this.isCollection   = isCollection;
            this.subLevels       = subLevels;
            this.collectionItems = new LinkedList<gxuSDTColItem>();
        }

        public String GetName()
        {
            return this.name;
        }

        public LinkedList<gxuSDTItem> GetItems()
        {
            return this.items;
        }

        public LinkedList<gxuSDTColItem> GetCollectionItems()
        {
            return this.collectionItems;
        }

        public LinkedList<GxuSDTLevel> GetSubLevels()
        {
            return this.subLevels;
        }

        public bool GetIsCollection()
        {
            return this.isCollection;
        }

        public void AddItem(gxuSDTItem item)
        {
            this.items.AddLast(item);
        }

        public void AddLevel(GxuSDTLevel subLevel)
        {
            this.subLevels.AddLast(subLevel);
        }

        public void AddCollectionItem(gxuSDTColItem item)
        {
            this.collectionItems.AddLast(item);
        }
    }
}
