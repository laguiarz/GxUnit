using Artech.Architecture.Common.Objects;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class TestSuite
    {
        private String TestSuiteName = "";


        public TestSuite(String suiteName, string parentFolder)
        {
            TestSuiteName = suiteName;

            // Creating Suite Directory
            ManejadorFolder mf = new ManejadorFolder();
            Folder f = mf.GetFolder(suiteName);
            if (f != null)
                throw new Exception("Unable to create the suite '" + suiteName + "', directory already exists.");

            DTFolder SuiteFolder = new DTFolder(suiteName, parentFolder);
            mf.CrearFolder(SuiteFolder, false);
        }

        public String getSuiteName()
        {
            return this.TestSuiteName;
        }
    }
}

