/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 */


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
        private LinkedList<GxuSDTCollectionItem> collectionItems;      //the items of the level (it should have either items or sublevels otherwise it is an empty SDT)
        private LinkedList<GxuSDTItem> items;

        public GxuSDTLevel()
        { 
        }

        public GxuSDTLevel(string name, bool isCollection)
        {
            this.name           = name;
            this.isCollection   = isCollection;
            this.subLevels      = new LinkedList<GxuSDTLevel>();
            this.items          = new LinkedList<GxuSDTItem>();
            this.collectionItems = new LinkedList<GxuSDTCollectionItem>();    
        }

        public GxuSDTLevel( String name, LinkedList<GxuSDTItem> items, bool isCollection, LinkedList<GxuSDTLevel> subLevels)
        {
            this.name           = name;
            this.items          = items;
            this.isCollection   = isCollection;
            this.subLevels       = subLevels;
            this.collectionItems = new LinkedList<GxuSDTCollectionItem>();
        }

        public String Name
        {
            get { return name; }
            set { name = value; }

        }

        public bool IsCollection
        {
            get { return isCollection; }
            set { isCollection = value; }
        }

        public LinkedList<GxuSDTItem> GetItems()
        {
            return this.items;
        }

        public LinkedList<GxuSDTCollectionItem> GetCollectionItems()
        {
            return this.collectionItems;
        }

        public LinkedList<GxuSDTLevel> GetSubLevels()
        {
            return this.subLevels;
        }

        public void AddItem(GxuSDTItem item)
        {
            this.items.AddLast(item);
        }

        public void AddLevel(GxuSDTLevel subLevel)
        {
            this.subLevels.AddLast(subLevel);
        }

        public void AddCollectionItem(GxuSDTCollectionItem item)
        {
            this.collectionItems.AddLast(item);
        }
    }
}
