using System.Collections.Generic;
using Artech.Architecture.Common.Objects;
using Artech.Architecture.Common.Services;
using Artech.Genexus.Common.Objects;
using Artech.MsBuild.Common;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using PGGXUnit.Packages.GXUnit.GXUnitCore;

namespace PGGXUnit.Packages.GXUnit.MSBuildAPI
{
    public class GenerateGXUnitTests : ArtechTask
    {
        bool isSuccsess = true;
        private string xmlName;

        [Output]
        public string XMLName
        {
            get { return xmlName; }
            set { xmlName = value; }
        }

        public override bool Execute()
        {
            OutputSubscribe();
            CommonServices.Output.StartSection("Generate GXUnit Tests Task");

            if (KB == null)
            {
                CommonServices.Output.AddErrorLine("No KB opened");
                isSuccsess = false;
            }
            else
            {
                LinkedList<GxuTestItem> testCaseList = new LinkedList<GxuTestItem>();
                Folder GXUnitSuitesFolder = GxuFolderHandler.GetFolderObject(KB.DesignModel, "GXUnitSuites");
                foreach (KBObject suite in GXUnitSuitesFolder.Objects)
                {
                    if (suite is Folder)
                    {
                        testCaseList.AddLast(new GxuTestItem(suite.Name, "GXUnitSuites", true));
                        foreach (KBObject testCase in (suite as Folder).Objects)
                        {
                            if (testCase is Procedure)
                            {
                                testCaseList.AddLast(new GxuTestItem(testCase.Name, suite.Name, false));
                            }
                        }
                    }
                }

                ContextHandler.Model = KB.DesignModel;

                GxuRunner mr = GxuRunner.GetInstance();
                if (testsSelected(testCaseList))
                {
                    mr.RegenerateTestLoaderProcedure(testCaseList, out xmlName);

                    Procedure runner = GxuProcedureHandler.GetProcedureObject(KB.DesignModel, Constants.RUNNER_PROC);

                    if (runner == null)
                    {
                        CommonServices.Output.AddErrorLine(Constants.RUNNER_PROC + " cannot be created");
                        isSuccsess = false;
                    }
                }
                else
                {
                    CommonServices.Output.AddErrorLine("Select at least one test case");
                    isSuccsess = false;
                }
            }

            CommonServices.Output.EndSection("Generate GXUnit Tests Task", true);
            OutputUnsubscribe();
            return isSuccsess;
        }

        private bool testsSelected(LinkedList<GxuTestItem> tests)
        {
            foreach (GxuTestItem item in tests)
            {
                // No es una Suite, es un Test Case
                if (!item.IsSuite)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
