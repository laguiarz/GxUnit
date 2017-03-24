using System;
using System.Collections.Generic;

using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System.IO;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class RunnerHandler
    {
        private static RunnerHandler instance = new RunnerHandler();
        private const String Folder = Constants.GXUNIT_FOLDER;

        private RunnerHandler()
        {
        }

        public static RunnerHandler GetInstance()
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

       
        public bool RegenerateTestLoaderProcedure(LinkedList<DTTestCase> lista, out string XMLName)
        {

            string kbPath = KBManager.getTargetPath();
            string resultPath = kbPath.Trim() + Constants.RESULT_PATH;
            XMLName = resultPath + System.DateTime.Now.ToString("GXUnitR_yyyyMMdd_HHmmss") + ".xml";

            //Check if the file already exists and delete it
            try
            {
                string targetPath = KBManager.getTargetPath();
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

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("GXUnitSuite", "GXUnitSuite");
            variables.AddFirst(v);
            v = new DTVariable("GXUnitSuiteCollection", "GXUnitSuite", true);
            variables.AddFirst(v);
            v = new DTVariable("GXUnitTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            v = new DTVariable("ResultFileName", Constants.Tipo.VARCHAR, 512, 0);
            variables.AddFirst(v);

            LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();

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
            bool firstFlag = false;
            bool hasContentFlag = false;

            foreach (DTTestCase testcase1 in lista)
            {

                if (testcase1.GetSuite())
                {
                    foreach (DTTestCase testcase in lista)
                    {
                        //si es un suite, recorro todos los test case hijos
                        if ((testcase.GetSuite() == false) && (testcase.GetFolder().Equals(testcase1.GetNombre())))
                        {

                            CurrentSuiteName = testcase.GetFolder();
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
                            procSource += "&GXUnitTestCase.TestName = '" + testcase.GetNombre() + "'\r\n";
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
            foreach (DTTestCase testcase1 in lista)
            {
                if (testcase1.GetSuite())
                {
                    procSource += "\r\n";
                    procSource += "//Add Calls for Static-Link\r\n";
                    procSource += "if false\r\n";
                    foreach (DTTestCase testcase in lista)
                    {
                        //si es un suite, recorro todos los test case hijos
                        if ((testcase.GetSuite() == false) && (testcase.GetFolder().Equals(testcase1.GetNombre())))
                        {

                            procSource += '\t' + testcase.GetNombre() + "()\r\n";
                        }

                    }
                    procSource += "endif\r\n";
                }
            }

            procSource += "";

            Procedimiento proc = new Procedimiento(procName, procSource, procRules, Folder, variables, propiedades);
            KBProcedureHandler m = new KBProcedureHandler();
            m.CrearProcedimiento(proc, true);

            return true;

        }

        public bool ExecuteRunnerProc()
        {
            try
            {
                KBProcedureHandler m = new KBProcedureHandler();
                m.RunProcedimiento(Constants.RUNNER_PROC);
                ContextHandler.Execute = true;
            }
            catch (Exception e)
            {
                String msgoutput = e.Message;
                GxHelper.WriteOutput(msgoutput);
            }
            return true;
        }

        public bool RebuildRunner()
        {
            KBProcedureHandler procHandler = new KBProcedureHandler();
            procHandler.GenerarProcedimiento(Constants.RUNNER_PROC.Trim());
            ContextHandler.Execute = true;
            return true;
        }

    }
}

