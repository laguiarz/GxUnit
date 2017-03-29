/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 * */

using Artech.Architecture.Common.Objects;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class GxuTestSuite
    {
        private String name = "";


        public GxuTestSuite(String suiteName, string parentFolder)
        {
            name = suiteName;

            // Creating Suite Directory
            GxuFolderHandler mf = new GxuFolderHandler();
            Folder f = mf.GetFolder(suiteName);
            if (f != null)
                throw new Exception("Unable to create the suite '" + suiteName + "', directory already exists.");

            GxuFolder SuiteFolder = new GxuFolder(suiteName, parentFolder);
            mf.CreateFolder(SuiteFolder, false);
        }

        public String Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
    }
}

