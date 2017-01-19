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
                LinkedList<DTTestCase> testCaseList = new LinkedList<DTTestCase>();
                Folder GXUnitSuitesFolder = KBFolderHandler.GetFolderObject(KB.DesignModel, "GXUnitSuites");
                foreach (KBObject suite in GXUnitSuitesFolder.Objects)
                {
                    if (suite is Folder)
                    {
                        testCaseList.AddLast(new DTTestCase(suite.Name, "GXUnitSuites", true));
                        foreach (KBObject testCase in (suite as Folder).Objects)
                        {
                            if (testCase is Procedure)
                            {
                                testCaseList.AddLast(new DTTestCase(testCase.Name, suite.Name, false));
                            }
                        }
                    }
                }

                ContextHandler.Model = KB.DesignModel;

                ManejadorRunner mr = ManejadorRunner.GetInstance();
                if (testsSelected(testCaseList))
                {
                    mr.CrearRunner(testCaseList, out xmlName);

                    Procedure runner = KBProcedureHandler.GetProcedureObject(KB.DesignModel, Constantes.RUNNER_PROC);

                    if (runner == null)
                    {
                        CommonServices.Output.AddErrorLine(Constantes.RUNNER_PROC + " cannot be created");
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

        private bool testsSelected(LinkedList<DTTestCase> tests)
        {
            foreach (DTTestCase item in tests)
            {
                // No es una Suite, es un Test Case
                if (!item.GetSuite())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
