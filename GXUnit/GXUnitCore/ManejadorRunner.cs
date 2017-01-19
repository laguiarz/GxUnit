using System;
using System.Collections.Generic;

using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System.IO;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class ManejadorRunner
    {
        private static ManejadorRunner instance = new ManejadorRunner();
        private const String Nombre = "RunnerProcedure";
        private const String Folder = "GXUnit";

        private ManejadorRunner()
        {
        }

        public static ManejadorRunner GetInstance()
        {
            return instance;
        }

        public String GetNombre()
        {
            return Nombre;
        }

        public bool CrearGenerarRunner(LinkedList<DTTestCase> lista)
        {
            bool ret = false;
            if (!Artech.Genexus.Common.Services.GenexusUIServices.Build.IsBuilding)
            {
                string XMLPath;
                ret = CrearRunner(lista, out XMLPath);
                ret = ret && GeneraRunner();
            }
            else
            {
                FuncionesAuxiliares.EscribirOutput("Can´t Test while building");
            }
            return ret;
        }

       
        public bool CrearRunner(LinkedList<DTTestCase> lista, out string XMLName)
        {


            XMLName = System.DateTime.Now.ToString("R_yyyyMMdd_HHmmss") + ".xml";
            ManejadorContexto.LastXMLName = XMLName;

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
                FuncionesAuxiliares.EscribirOutput("Exception: " + e.Message);
            }


            //XMLName = XMLName.Replace(":", "-");
            //XMLName = XMLName.Replace("/", "-");
            //XMLName = XMLName.Replace(" ", "_") + ".xml";


            String procRules = "";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("xmlWriter", "XMLWriter");
            variables.AddFirst(v);
            v = new DTVariable("SDTSuite", "GXUnitSuite");
            variables.AddFirst(v);
            v = new DTVariable("SDTSuiteTestcase", "GXUnitSuite.TestCase");
            variables.AddFirst(v);
            v = new DTVariable("SDTSuiteSuite", "GXUnitSuite.Suite");
            variables.AddFirst(v);
            v = new DTVariable("SDTTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            //variables utilizadas para calcular la duracion de la ejecucion de un test case
            v = new DTVariable("tiempoIniciodatetime", Constantes.Tipo.DATETIME);
            variables.AddFirst(v);
            v = new DTVariable("tiempoInicio", Constantes.Tipo.NUMERIC, 16, 0);
            variables.AddFirst(v);
            v = new DTVariable("totaltiempo", Constantes.Tipo.NUMERIC, 16, 0);
            variables.AddFirst(v);


            foreach (DTTestCase testcase in lista)
            {

                if (testcase.GetSuite())
                {
                    v = new DTVariable(testcase.GetNombre(), "GXUnitSuite");
                    variables.AddFirst(v);
                }
                else
                {
                    v = new DTVariable(testcase.GetNombre(), "GXUnitTestCase");
                    variables.AddFirst(v);
                }
            }


            LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();
            DTPropiedad p = new DTPropiedad("IsMain", true);
            propiedades.AddFirst(p);

            p = new DTPropiedad("CALL_PROTOCOL", Artech.Genexus.Common.Properties.PRC.CallProtocol_Values.CommandLine);
            propiedades.AddFirst(p);

            p = new DTPropiedad("SPC_WARNINGS_DISABLED", "spc0096 spc0107 spc0142 spc0087");
            propiedades.AddFirst(p);


            String procSource = "//GXUnit Runner\r\n";
            foreach (DTTestCase testcase1 in lista)
            {

                if (testcase1.GetSuite())
                {
                    foreach (DTTestCase testcase in lista)
                    {
                        //si es un suite, recorro todos los test case hijos
                        if ((testcase.GetSuite() == false) && (testcase.GetFolder().Equals(testcase1.GetNombre())))
                        {
                            procSource += "&" + testcase.GetFolder() + ".SuiteName =" + "'" + testcase.GetFolder() + "'\r\n";

                            procSource += "&" + testcase.GetNombre() + ".TestName = '" + testcase.GetNombre() + "'\r\n";
                            procSource += "&SDTSuiteTestcase.TestCase = &" + testcase.GetNombre() + "\r\n";
                            procSource += "&" + testcase.GetFolder() + ".TestCases.Add(&SDTSuiteTestcase.Clone())" + "\r\n";
                            //Cambio para utilizar un proc que encapsule el acceso a session
                            procSource += "SetGXUnitSession.Call('SuiteParent','" + testcase.GetFolder() + "')\r\n";
                            procSource += "SetGXUnitSession.Call('" + testcase.GetFolder() + "',&" + testcase.GetFolder() + ".ToXml()" + ")\r\n";                          
                            //calculo del tiempo
                            procSource += "CSHARP  [!&tiempoIniciodatetime!] = DateTime.UtcNow;\r\n";
                            procSource += "JAVA [!&tiempoInicio!] = System.currentTimeMillis();\r\n";
                            //llamada al test case
                            procSource += "Call(" + testcase.GetNombre().Trim() + ")\r\n";
                            //calculo del tiempo
                            procSource += "JAVA [!&totalTiempo!] = System.currentTimeMillis() - [!&tiempoInicio!];\r\n";
                            procSource += "CSHARP  [!&totalTiempo!] = (long) ((DateTime.UtcNow - [!&tiempoIniciodatetime!]).TotalMilliseconds);\r\n";
                            //Cambio para utilizar un proc que encapsule el acceso a session
                            procSource += "&" + testcase.GetFolder() + ".FromXml(GetGXUnitSession.Udp('" + testcase.GetFolder() + "'))\r\n";
                            procSource += "&tiempoInicio = &" + testcase.GetFolder() + ".TestCases.Count\r\n";
                            procSource += "&" + testcase.GetFolder() + ".TestCases.Item(&tiempoInicio).TestCase.TestTimeExecution = &totalTiempo\r\n";

                        }

                    }
                    if (testcase1.GetFolder().Equals("GXUnitSuites"))
                    {
                        procSource += "&" + testcase1.GetNombre() + ".SuiteName = '" + testcase1.GetNombre() + "'\r\n";
                        procSource += "&SDTSuiteSuite.Suite = &" + testcase1.GetNombre() + "\r\n";
                        procSource += "&SDTSuite.Suites.Add(&SDTSuiteSuite.Clone())\r\n";
                        procSource += "&SDTSuite.SuiteName = 'GXUnitSuites'\r\n";
                        procSource += "////////////////////////////////////////////////////////\r\n";
                    }
                    else
                    {
                        procSource += "&SDTSuiteSuite.Suite = &" + testcase1.GetNombre() + "\r\n";
                        procSource += "&" + testcase1.GetNombre() + ".SuiteName = '" + testcase1.GetNombre() + "'\r\n";
                        procSource += "&" + testcase1.GetFolder() + ".Suites.Add(&SDTSuiteSuite.Clone())\r\n";
                        procSource += "/////////////////////////////////////////////\r\n";
                    }
                }
            }

            procSource += "&xmlWriter.Open('" + XMLName + "')\r\n";
            procSource += "&xmlWriter.WriteRawText(&SDTSuite.ToXml())\r\n";
            procSource += "&xmlWriter.Close()\r\n";

            Procedimiento proc = new Procedimiento(Nombre, procSource, procRules, Folder, variables, propiedades);
            ManejadorProcedimiento m = new ManejadorProcedimiento();
            m.CrearProcedimiento(proc, true);

            return true;


        }

        public bool EliminarRunner()
        {
            ManejadorProcedimiento m = new ManejadorProcedimiento();
            return m.EliminarProcedimiento(Nombre);

        }
        public bool EjecutarRunner()
        {
            ManejadorProcedimiento m = new ManejadorProcedimiento();
            m.RunProcedimiento(Nombre);
            ManejadorContexto.Ejecutar = true;
            return true;
        }

        public bool GeneraRunner()
        {
            ManejadorProcedimiento m = new ManejadorProcedimiento();
            m.GenerarProcedimiento(Nombre);
            ManejadorContexto.Ejecutar = true;
            return true;
        }

    }
}

