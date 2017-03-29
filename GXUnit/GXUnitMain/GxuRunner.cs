using System;
using System.Collections.Generic;

using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System.IO;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class GxuRunner
    {
        private static GxuRunner instance = new GxuRunner();
   
        private GxuRunner()
        {
        }

        public static GxuRunner GetInstance()
        {
            return instance;
        }

        //public bool RegenerateAndBuildRunner(LinkedList<DTTestCase> lista)
        //{
        //    bool ret = false;
        //    if (!Artech.Genexus.Common.Services.GenexusUIServices.Build.IsBuilding)
        //    {
        //        string XMLPath;
        //        ret = RegenerateTestLoaderProcedure(lista, out XMLPath);
        //        ret = ret && RebuildRunner();
        //    }
        //    else
        //    {
        //        GxHelper.WriteOutput("Can´t Test while building");
        //    }
        //    return ret;
        //}

       
        public bool RegenerateTestLoaderProcedure(LinkedList<GxuTestItem> selectedTestCases, out string XMLName)
        {

            string resultPath = GxuHelper.GetResultsPath();
            XMLName = resultPath + "GXUnitR_" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml";

            //Check if the file already exists and delete it
            try
            {
                string targetPath = KBManager.GetTargetPath();
                targetPath = Path.Combine(targetPath, XMLName);
                if (File.Exists(targetPath) )
                {
                    File.Delete(targetPath);
                }
            }
            catch (Exception e)
            {
                GxHelper.WriteOutput("Exception: " + e.Message);
            }

            //Regenerate Procedure
            String procName = "GXUnit_LoadTests";
            String procRules = "parm(out:&GXUnitSuiteCollection, out:&ResultFileName);";

            LinkedList<GxuVariable> variables = new LinkedList<GxuVariable>();
            GxuVariable v = new GxuVariable("GXUnitSuite", "GXUnitSuite");
            variables.AddFirst(v);
            v = new GxuVariable("GXUnitSuiteCollection", "GXUnitSuite", true);
            variables.AddFirst(v);
            v = new GxuVariable("GXUnitTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            v = new GxuVariable("ResultFileName", Constants.GxuDataType.VARCHAR, 512, 0);
            variables.AddFirst(v);

            LinkedList<GxuProperty> propiedades = new LinkedList<GxuProperty>();

            String procSource = "";
            procSource += "/*\r\n";
            procSource += "Loads the tests to be executed.\r\n";
            procSource += "This program is overwitten every time GXTest executes\r\n";
            procSource += "\r\n";
            procSource += "Created Automatically by GXUnit\r\n";
            procSource += "*/\r\n";
            procSource += "\r\n";
            procSource += "//Output File\r\n";
            procSource += "&ResultFileName='" + XMLName.Trim() + "'\r\n";
            procSource += "\r\n";
            procSource += "&GXUnitSuiteCollection = new()\r\n";

            string CurrentSuiteName = "";
            string PreviousSuiteName = "";
            bool firstFlag = true;
            bool hasContentFlag = false;

            foreach (GxuTestItem testcase1 in selectedTestCases)
            {

                if (testcase1.IsSuite)
                {
                    foreach (GxuTestItem testcase in selectedTestCases)
                    {
                        //if it is a suite, it goes through all children test-cases
                        if ((testcase.IsSuite == false) && (testcase.Folder.Equals(testcase1.Name)))
                        {

                            CurrentSuiteName = testcase.Folder;
                            if (CurrentSuiteName != PreviousSuiteName)
                            {
                                if (firstFlag)
                                    firstFlag = false;
                                else
                                    procSource += "&GXUnitSuiteCollection.Add(&GXUnitSuite)\r\n";

                                procSource += "\r\n";
                                procSource += "&GXUnitSuite = new()\r\n";
                                procSource += "&GXUnitSuite.SuiteName = '" + CurrentSuiteName + "'\r\n";
                                procSource += "\r\n";
                                PreviousSuiteName = CurrentSuiteName;
                            }

                            hasContentFlag = true;
                            procSource += "&GXUnitTestCase = new()\r\n";
                            procSource += "&GXUnitTestCase.TestName = '" + testcase.Name + "'\r\n";
                            procSource += "&GXUnitSuite.TestCases.Add(&GXUnitTestCase)\r\n";
                            procSource += "\r\n";

                        }

                    }
                }
            }

            if (hasContentFlag)
            {
                procSource += "&GXUnitSuiteCollection.Add(&GXUnitSuite)\r\n";
                procSource += "\r\n";
            }

            //Now Create an entry for static-link
            foreach (GxuTestItem testcase1 in selectedTestCases)
            {
                if (testcase1.IsSuite)
                {
                    procSource += "\r\n";
                    procSource += "//Add Calls for Static-Link\r\n";
                    procSource += "if false\r\n";
                    foreach (GxuTestItem testcase in selectedTestCases)
                    {
                        //si es un suite, recorro todos los test case hijos
                        if ((testcase.IsSuite == false) && (testcase.Folder.Equals(testcase1.Name)))
                        {

                            procSource += '\t' + testcase.Name + "()\r\n";
                        }

                    }
                    procSource += "endif\r\n";
                }
            }

            procSource += "";

            GxuProcedure proc = new GxuProcedure(procName, procSource, procRules, Constants.GXUNIT_FOLDER, variables, propiedades);
            GxuProcedureHandler procHandler = new GxuProcedureHandler();
            procHandler.CreateProcedure(proc, true);

            return true;

        }

        public bool ExecuteRunnerProc()
        {
            try
            {
                GxuProcedureHandler m = new GxuProcedureHandler();
                m.Run(Constants.RUNNER_PROC);
            }
            catch (Exception e)
            {
                GxHelper.WriteOutput(e.Message);
            }
            return true;
        }

        public bool RebuildRunner()
        {
            GxuProcedureHandler procHandler = new GxuProcedureHandler();
            procHandler.Build(Constants.RUNNER_PROC.Trim());
            return true;
        }

    }
}

