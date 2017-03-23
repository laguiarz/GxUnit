using System;
using System.Collections.Generic;

using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using Artech.Genexus.Common.Objects;
using Artech.Genexus.Common.Parts.SDT;
using Artech.Architecture.Common.Objects;
using Artech.Architecture.UI.Framework.Services;
using Artech.Genexus.Common;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class GXUnitInialize
    {

        private static GXUnitInialize instance = new GXUnitInialize();

        private GXUnitInialize()
        {
        }

        public static GXUnitInialize GetInstance()
        {
            return instance;
        }

        private bool CreateAssertNumericEquals()
        {
            String nombre = "AssertNumericEquals";
            
            String procRules = "Parm(in:&ObtainedValue, in:&ExpectedValue);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("ObtainedValue", Constants.Tipo.NUMERIC, 18, 4);
            variables.AddFirst(v);
            v = new DTVariable("ExpectedValue", Constants.Tipo.NUMERIC, 18, 4);
            variables.AddFirst(v);
            v = new DTVariable("GXUnitTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            v = new DTVariable("SDTAssert", Constants.Tipo.SDT, "GXUnitAssert");
            variables.AddFirst(v);
            v = new DTVariable("SDTAssertItem", Constants.Tipo.SDT, "GXUnitAssert.GXUnitAssertInfo");
            variables.AddFirst(v);
            v = new DTVariable("SDTSuite", Constants.Tipo.SDT, "GXUnitSuite");
            variables.AddFirst(v);
            v = new DTVariable("SessionValue", Constants.Tipo.VARCHAR, 9999, 0);
            variables.AddFirst(v);

            LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();

            String procSource = "";

            procSource += "/*\r\n";
            procSource += "Compares obtained and expected results\r\n";
            procSource += "\r\n";
            procSource += "Created Automatically by GXUnit\r\n";
            procSource += "*/\r\n";
            procSource += "\r\n";
            procSource += "&SDTAssert = new()\r\n";
            procSource += "if &ExpectedValue = &ObtainedValue\r\n";
            procSource += "\t&SDTAssert.ObtainedValue = 'OK'\r\n";
            procSource += "else\r\n";
            procSource += "\t&SDTAssert.ObtainedValue = 'FAIL'\r\n";
            procSource += "\r\n";
            procSource += "\t&SDTAssertItem = new()\r\n";
            procSource += "\t&SDTAssertItem.Expected = trim(&ExpectedValue.ToString())\r\n";
            procSource += "\t&SDTAssertItem.Obtained = trim(&ObtainedValue.ToString())\r\n";
            procSource += "\t&SDTAssert.GXUnitAssertInfos.Add(&SDTAssertItem)\r\n";
            procSource += "endif\r\n";
            procSource += "\r\n";

            procSource += "//Retrieve the Current-Test Information\r\n";
            procSource += "GetGXUnitSession('CurrentTest', &SessionValue)\r\n";
            procSource += "&GXUnitTestCase.FromXml(&SessionValue)\r\n";
            procSource += "\r\n";

            procSource += "&GXUnitTestCase.Asserts.Add(&SDTAssert)\r\n";
            procSource += "SetGXUnitSession('CurrentTest', &GXUnitTestCase.ToXml() )\r\n";
            procSource += "\r\n";

            
            Procedimiento p = new Procedimiento(nombre,procSource,procRules,"GXUnit",variables,propiedades);
            KBProcedureHandler m = new KBProcedureHandler();
            m.CrearProcedimiento(p,true);

            return true;
        }

        private bool CreateAssertStringEquals()
        {
            String nombre = "AssertStringEquals";
            String procRules = "Parm(in:&ObtainedValue, in:&ExpectedValue);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("ObtainedValue", Constants.Tipo.VARCHAR, 1024, 4);
            variables.AddFirst(v);
            v = new DTVariable("ExpectedValue", Constants.Tipo.VARCHAR, 1024, 4);
            variables.AddFirst(v);
            v = new DTVariable("GXUnitTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            v = new DTVariable("SDTAssert", Constants.Tipo.SDT, "GXUnitAssert");
            variables.AddFirst(v);
            v = new DTVariable("SDTAssertItem", Constants.Tipo.SDT, "GXUnitAssert.GXUnitAssertInfo");
            variables.AddFirst(v);
            v = new DTVariable("SDTSuite", Constants.Tipo.SDT, "GXUnitSuite");
            variables.AddFirst(v);
            v = new DTVariable("SessionValue", Constants.Tipo.VARCHAR, 9999, 0);
            variables.AddFirst(v);

            LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();

            String procSource = "";
            procSource += "/*\r\n";
            procSource += "Compares obtained and expected results\r\n";
            procSource += "\r\n";
            procSource += "Created Automatically by GXUnit\r\n";
            procSource += "*/\r\n";
            procSource += "\r\n";

            procSource += "if &ExpectedValue = &ObtainedValue\r\n";
            procSource += "\t&SDTAssert.ObtainedValue = 'OK'\r\n";
            procSource += "else\r\n";
            procSource += "\t&SDTAssert.ObtainedValue = 'FAIL'\r\n";
            procSource += "\t&SDTAssertItem=new()\r\n";
            procSource += "\t&SDTAssertItem.Expected = &ExpectedValue\r\n";
            procSource += "\t&SDTAssertItem.Obtained = &ObtainedValue\r\n";
            procSource += "\t&SDTAssert.GXUnitAssertInfos.Add(&SDTAssertItem)\r\n";
            procSource += "endif\r\n";
            procSource += "\r\n";

            procSource += "//Retrieve the Current-Test Information\r\n";
            procSource += "GetGXUnitSession('CurrentTest', &SessionValue)\r\n";
            procSource += "&GXUnitTestCase.FromXml(&SessionValue)\r\n";
            procSource += "\r\n";

            procSource += "&GXUnitTestCase.Asserts.Add(&SDTAssert)\r\n";
            procSource += "SetGXUnitSession('CurrentTest', &GXUnitTestCase.ToXml() )\r\n";
            procSource += "\r\n";


            Procedimiento p = new Procedimiento(nombre, procSource, procRules, "GXUnit", variables, propiedades);
            
            KBProcedureHandler m = new KBProcedureHandler();
            m.CrearProcedimiento(p,true);
            
            return true;
        }

        private bool CreateToolsGetCurrentMillisecs()
        {
            String nombre = "GXUnit_GetCurrentMillisecs";
            String procRules = "Parm(out:&DateTime, out:&Milliseconds);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("DateTime", Constants.Tipo.DATETIME);
            variables.AddFirst(v);
            v = new DTVariable("Milliseconds", Constants.Tipo.NUMERIC, 16, 0);
            variables.AddFirst(v);

            LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();
            DTPropiedad property = new DTPropiedad("SPC_WARNINGS_DISABLED", "spc0096 spc0107 spc0142 spc0087");
            propiedades.AddFirst(property);

            String procSource = "";
            procSource += "/*\r\n";
            procSource += "Returns a number a datetime/number with current time\r\n";
            procSource += "\r\n";
            procSource += "Created Automatically by GXUnit\r\n";
            procSource += "*/\r\n";
            procSource += "\r\n";

            procSource += "csharp  [!&DateTime!] = DateTime.UtcNow;\r\n";
            procSource += "\r\n";
            procSource += "java [!&milliseconds!] = System.currentTimeMillis();\r\n";
            procSource += "\r\n";

            Procedimiento p = new Procedimiento(nombre, procSource, procRules, "GXUnit", variables, propiedades);

            KBProcedureHandler m = new KBProcedureHandler();
            m.CrearProcedimiento(p, true);

            return true;
        }

        private bool CreateToolsGetEllapsedMillisecs()
        {
            String nombre = "GXUnit_GetElapsedMilliseconds";
            String procRules = "parm(in:&StartDateTime, in:&StartMilliseconds, 	out:&ElapsedMilliseconds);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("StartDateTime", Constants.Tipo.DATETIME);
            variables.AddFirst(v);
            v = new DTVariable("StartMilliseconds", Constants.Tipo.NUMERIC, 16, 0);
            variables.AddFirst(v);
            v = new DTVariable("ElapsedMilliseconds", Constants.Tipo.NUMERIC, 16, 0);
            variables.AddFirst(v);

            LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();
            DTPropiedad property = new DTPropiedad("SPC_WARNINGS_DISABLED", "spc0096 spc0107 spc0142 spc0087");
            propiedades.AddFirst(property);

            String procSource = "";
            procSource += "/*\r\n";
            procSource += "Returns ellapsed time in milliseconds\r\n";
            procSource += "\r\n";
            procSource += "Created Automatically by GXUnit\r\n";
            procSource += "*/\r\n";
            procSource += "\r\n";

            procSource += "java [!&ElapsedMilliseconds!] = System.currentTimeMillis() - [!&StartMilliseconds!];\r\n";
            procSource += "\r\n";
            procSource += "csharp  [!&ElapsedMilliseconds!] = (long) ((DateTime.UtcNow - [!&StartDateTime!]).TotalMilliseconds);\r\n";
            procSource += "\r\n";

            Procedimiento p = new Procedimiento(nombre, procSource, procRules, "GXUnit", variables, propiedades);

            KBProcedureHandler m = new KBProcedureHandler();
            m.CrearProcedimiento(p, true);

            return true;
        }

        public bool CreateToolsLoadTests()
        {
            String nombre = "GXUnit_LoadTests";
            String procRules = "parm(out:&GXUnitSuiteCollection, out:&ResultFileName);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("GXUnitSuite", "GXUnitSuite");
            variables.AddFirst(v);
            v = new DTVariable("GXUnitSuiteCollection", "GXUnitSuite", true); 
            variables.AddFirst(v);
            v = new DTVariable("GXUnitSuiteTestCase", "GXUnitSuite.TestCase"); 
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
            procSource += "&ResultFileName='outputfilename.xml'";
            procSource += "\r\n";
            procSource += "&GXUnitSuiteCollection = new()\r\n";
            procSource += "\r\n";
            procSource += "&GXUnitSuite = new()\r\n";
            procSource += "&GXUnitSuite.SuiteName = 'TestSuiteNameHere'\r\n";
            procSource += "\r\n";
            procSource += "&GXUnitTestCase = new()\r\n";
            procSource += "&GXUnitTestCase.TestName = 'testprogramnamehere'\r\n";
            procSource += "&GXUnitSuite.TestCases.Add(&GXUnitTestCase)\r\n";
            procSource += "\r\n";
            procSource += "&GXUnitSuiteCollection.Add(&GXUnitSuite)";
            procSource += "";

            Procedimiento p = new Procedimiento(nombre, procSource, procRules, "GXUnit", variables, propiedades);

            KBProcedureHandler m = new KBProcedureHandler();
            m.CrearProcedimiento(p, true);

            return true;
        }


        private bool CreateToolsRunner()
        {
            String nombre = Constants.RUNNER_PROC; 
            String procRules = "";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("ExecutedGXUnitTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            v = new DTVariable("FileName", Constants.Tipo.VARCHAR, 512, 0);
            variables.AddFirst(v);
            v = new DTVariable("GXUnitAssert", "GXUnitAssert");
            variables.AddFirst(v);
            v = new DTVariable("GXUnitSuite", "GXUnitSuite");
            variables.AddFirst(v);
            v = new DTVariable("GXUnitSuiteCollection", "GXUnitSuite", true); 
            variables.AddFirst(v);
            v = new DTVariable("GXUnitTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            v = new DTVariable("Milliseconds", Constants.Tipo.NUMERIC, 16, 0);
            variables.AddFirst(v);
            v = new DTVariable("OutputGXUnitSuite", "GXUnitSuite");
            variables.AddFirst(v);
            v = new DTVariable("SessionValue", Constants.Tipo.VARCHAR, 9999,0);
            variables.AddFirst(v);
            v = new DTVariable("StartDateTime", Constants.Tipo.DATETIME);
            variables.AddFirst(v);
            v = new DTVariable("TestPgmName", Constants.Tipo.VARCHAR, 128, 0);
            variables.AddFirst(v);
            v = new DTVariable("xmlWriter", "XMLWriter");
            variables.AddFirst(v);

            LinkedList<DTPropiedad> properties = new LinkedList<DTPropiedad>();
            DTPropiedad property = new DTPropiedad("IsMain", true);
            properties.AddFirst(property);

            property = new DTPropiedad("CALL_PROTOCOL", Artech.Genexus.Common.Properties.PRC.CallProtocol_Values.CommandLine);
            properties.AddFirst(property);

            String procSource = "";
            procSource += "/*\r\n";
            procSource += "Runs every test in the list\r\n";
            procSource += "\r\n";
            procSource += "Created Automatically by GXUnit\r\n";
            procSource += "*/\r\n";
            procSource += "\r\n";

            procSource += "//Load Tests to Run\r\n";
            procSource += "GXUnit_LoadTests(&GXUnitSuiteCollection, &FileName)\r\n";
            procSource += "\r\n";

            procSource += "//Run Tests\r\n";
            procSource += "for &GXUnitSuite in &GXUnitSuiteCollection\r\n";
            procSource += "\r\n";
            procSource += "\tfor &GXUnitTestCase in &GXUnitSuite.TestCases\r\n";
            procSource += "\r\n";
            procSource += "\t\tSetGXUnitSession('CurrentTest',&GXUnitTestCase.ToXml())\r\n";
            procSource += "\t\tGXUnit_GetCurrentMillisecs(&StartDateTime, &Milliseconds)\r\n";
            procSource += "\r\n";
            procSource += "\t\t&testPgmName = &GXUnitTestCase.TestName.Trim()\r\n";
            procSource += "\t\tcall(&testPgmName)\r\n";
            procSource += "\r\n";
            procSource += "\t\t//At This point in the Session we have the &GXUnitSuiteTestCase Assert Portion Saved\r\n";
            procSource += "\t\t//So let's retrieve it...\r\n";
            procSource += "\t\tGetGXUnitSession('CurrentTest', &SessionValue)\r\n";
            procSource += "\t\t&ExecutedGXUnitTestCase.FromXml(&SessionValue)\r\n";
            procSource += "\r\n";
            procSource += "\t\tif &ExecutedGXUnitTestCase.TestName = &testPgmName\r\n";
            procSource += "\r\n";
            procSource += "\t\t\t&GXUnitTestCase.Asserts = &ExecutedGXUnitTestCase.Asserts.Clone()\r\n";
            procSource += "\t\t\t&GXUnitTestCase.TestTimeExecution = GXUnit_GetElapsedMilliseconds(&StartDateTime, &Milliseconds)\r\n";
            procSource += "\r\n";
            procSource += "\t\telse\r\n";
            procSource += "\r\n";
            procSource += "\t\t\t//Exeption\r\n";
            procSource += "\t\t\t&GXUnitAssert = new()\r\n";
            procSource += "\t\t\t&GXUnitAssert.ObtainedValue = 'EXCEPTION'\r\n";
            procSource += "\t\t\t&GXUnitTestCase.Asserts.Add(&GXUnitAssert)\r\n";
            procSource += "\t\tendif\r\n";
            procSource += "\r\n";
            procSource += "\tendfor\r\n";
            procSource += "\r\n";
            procSource += "endfor\r\n";
            procSource += "\r\n";
            procSource += "\r\n";
            procSource += "//Now load the SDT for Output\r\n";
            procSource += "&OutputGXUnitSuite = new()\r\n";
            procSource += "&OutputGXUnitSuite.SuiteName = 'GXUnitSuites'\r\n";
            procSource += "for &GXUnitSuite in &GXUnitSuiteCollection\r\n";
            procSource += "\t&OutputGXUnitSuite.Suites.Add( &GXUnitSuite.Clone() )\r\n";
            procSource += "endfor\r\n";
            procSource += "\r\n";
            procSource += "\r\n";
            procSource += "//Generate Output in root KB folder\r\n";
            procSource += "&xmlWriter.Open(&FileName)";
            procSource += "&xmlWriter.WriteRawText(&OutputGXUnitSuite.ToXml())";
            procSource += "&xmlWriter.Close()";


            Procedimiento p = new Procedimiento(nombre, procSource, procRules, "GXUnit", variables, properties);

            KBProcedureHandler m = new KBProcedureHandler();
            m.CrearProcedimiento(p, true);

            return true;
        }

        private bool CreateSetGXUnitSession()
        {
            String nombre = "SetGXUnitSession";
            String procRules = "parm(in:&Key, in:&Value);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("Key", Constants.Tipo.VARCHAR, 99999, 0);
            variables.AddFirst(v);
            v = new DTVariable("Value", Constants.Tipo.VARCHAR, 99999, 0);
            variables.AddFirst(v);
            v = new DTVariable("XmlWriter", "XmlWriter");
            variables.AddFirst(v);
            
            LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();

            String procSource = "";
            procSource += "/*\r\n";
            procSource += "Sets a Session-Value\r\n";
            procSource += "\r\n";
            procSource += "Created Automatically by GXUnit\r\n";
            procSource += "*/\r\n";
            procSource += "\r\n";
            procSource += "&XmlWriter.Open(&Key)\r\n";
	        procSource += "&XmlWriter.WriteElement(&Key, &Value)\r\n";
            procSource += "&XmlWriter.Close()\r\n";

            Procedimiento p = new Procedimiento(nombre, procSource, procRules, "GXUnit", variables, propiedades);

            KBProcedureHandler m = new KBProcedureHandler();
            m.CrearProcedimiento(p, true);

            return true;
        }

        private bool CreateGetGXUnitSession()
        {
            String nombre = "GetGXUnitSession";
            String procRules = "parm(in:&Key, out:&Value);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("Key", Constants.Tipo.VARCHAR, 99999, 0);
            variables.AddFirst(v);
            v = new DTVariable("Value", Constants.Tipo.VARCHAR, 99999, 0);
            variables.AddFirst(v);
            v = new DTVariable("XmlReader", "XmlReader");
            variables.AddFirst(v);

            LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();

            String procSource = "";

            procSource += "/*\r\n";
            procSource += "Retrieves a session value for a given session key\r\n";
            procSource += "\r\n";
            procSource += "Created Automatically by GXUnit\r\n";
            procSource += "*/\r\n";
            procSource += "\r\n";

            procSource += "&XmlReader.Open(&Key)\r\n";
            procSource += "&XmlReader.Read()\r\n";
            procSource += "&Value = &XmlReader.Value\r\n";
            procSource += "&XmlReader.Close()\r\n";

            Procedimiento p = new Procedimiento(nombre, procSource, procRules, "GXUnit", variables, propiedades);

            KBProcedureHandler m = new KBProcedureHandler();
            m.CrearProcedimiento(p, true);

            return true;
        }

        private bool CreateSDTGXUnitAssert()
        {
            gxuSDTItem item = new gxuSDTItem("ObtainedValue", Constants.Tipo.VARCHAR, 4);
            LinkedList<gxuSDTItem> listaitem = new LinkedList<gxuSDTItem>();
            listaitem.AddFirst(item);
            GxuSDTLevel root = new GxuSDTLevel("ROOT", listaitem, false, new LinkedList<GxuSDTLevel>());
            item = new gxuSDTItem("Obtained", Constants.Tipo.VARCHAR, 9999);
            listaitem = new LinkedList<gxuSDTItem>();
            listaitem.AddFirst(item);
            item = new gxuSDTItem("Expected", Constants.Tipo.VARCHAR, 9999);
            listaitem.AddFirst(item);
            LinkedList<GxuSDTLevel> niveles = new LinkedList<GxuSDTLevel>();
            niveles.AddFirst(new GxuSDTLevel("GXUnitAssertInfo", listaitem, true, new LinkedList<GxuSDTLevel>()));
            LinkedList<DTPropiedad> prop = new LinkedList<DTPropiedad>();
            DTPropiedad p = new DTPropiedad("ExternalNamespace", "");
            prop.AddFirst(p);
            SDTipo sdt = new SDTipo("GXUnitAssert", "GXUnit", niveles, root,prop);
            
            KBSDTHandler m = new KBSDTHandler();
            m.CrearSDT(sdt, true);
            return true;
        }

        private bool CreateSDTGXUnitTestCase()
        {

            GxuSDTLevel root = new GxuSDTLevel("ROOT", false);

            //Add Items
            gxuSDTItem item = new gxuSDTItem("TestName", Constants.Tipo.VARCHAR, 40);
            root.AddItem(item);
            item = new gxuSDTItem("TestTimeExecution", Constants.Tipo.NUMERIC, 16);
            root.AddItem(item);

            //Add Test-Case Level
            gxuSDTColItem colItem = new gxuSDTColItem("Assert", Constants.Tipo.SDT, "GXUnitAssert");
            root.AddCollectionItem(colItem);

            LinkedList<DTPropiedad> prop = new LinkedList<DTPropiedad>();
            DTPropiedad p = new DTPropiedad("ExternalNamespace", "");
            prop.AddFirst(p);
            SDTipo sdt = new SDTipo("GXUnitTestCase", Constants.GXUNIT_FOLDER, null, root,prop);

            KBSDTHandler m = new KBSDTHandler();
            m.CrearSDT(sdt, true);

            return true;
        }

        private bool CreateSDTGXUnitSuite()
        {


            GxuSDTLevel root = new GxuSDTLevel("ROOT", false);

            //Add Items
            gxuSDTItem item = new gxuSDTItem("SuiteName", Constants.Tipo.VARCHAR, 40);
            root.AddItem(item);

            //Add Test-Case Level
            gxuSDTColItem colItem = new gxuSDTColItem("TestCase", Constants.Tipo.SDT, "GXUnitTestCase");
            root.AddCollectionItem(colItem);

            //Add Suites-Level
            colItem = new gxuSDTColItem("Suite", Constants.Tipo.SDT, "GXUnitSuite");
            root.AddCollectionItem(colItem);

            LinkedList<DTPropiedad> prop = new LinkedList<DTPropiedad>();
            DTPropiedad p = new DTPropiedad("ExternalNamespace", "");
            
            prop.AddFirst(p);
            SDTipo sdt = new SDTipo("GXUnitSuite", Constants.GXUNIT_FOLDER, null, root, prop);

            KBSDTHandler m = new KBSDTHandler();
            m.CrearSDT(sdt, true);

            return true;
        }

        private bool CreateFoldersGXUnit()
        {
            DTFolder folder1 = new DTFolder(Constants.GXUNIT_FOLDER, "");
            KBFolderHandler mf = new KBFolderHandler();
            mf.CrearFolder(folder1, true);

            DTFolder GXUnitSuites = new DTFolder(Constants.SUITES_FOLDER, Constants.GXUNIT_FOLDER);
            mf.CrearFolder(GXUnitSuites, true);

            return true;
        }

        private bool CreateRESTInvoker()
        {
            String nombre = "RESTInvoker";

            String procRules = "parm(in:&Server, in:&Port, in:&UrlBase, in:&GetString, in:&Method, in:&AddString, in:&HeaderName, in:&HeaderValue, out:&HttpStatus, out:&ObtainedValue);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("AddString", Constants.Tipo.LONGVARCHAR, 2097152, 0);
            variables.AddFirst(v);
            v = new DTVariable("GetString", Constants.Tipo.CHARACTER, 200, 0);
            variables.AddFirst(v);
            v = new DTVariable("HeaderName", Constants.Tipo.CHARACTER, 80, 0);
            variables.AddFirst(v);
            v = new DTVariable("HeaderValue", Constants.Tipo.CHARACTER, 80, 0);
            variables.AddFirst(v);
            v = new DTVariable("HttpClient", Constants.Tipo.SDT, "HttpClient");
            variables.AddFirst(v);
            v = new DTVariable("HttpStatus", Constants.Tipo.NUMERIC, 6, 0);
            variables.AddFirst(v);
            v = new DTVariable("Method", Constants.Tipo.CHARACTER, 15, 0);
            variables.AddFirst(v);
            v = new DTVariable("Port", Constants.Tipo.NUMERIC, 6, 0);
            variables.AddFirst(v);
            v = new DTVariable("ObtainedValue", Constants.Tipo.LONGVARCHAR, 2097152, 0);
            variables.AddFirst(v);
            v = new DTVariable("Server", Constants.Tipo.CHARACTER, 50, 0);
            variables.AddFirst(v);
            v = new DTVariable("Text", Constants.Tipo.CHARACTER, 500, 0);
            variables.AddFirst(v);
            v = new DTVariable("UrlBase", Constants.Tipo.CHARACTER, 80, 0);
            variables.AddFirst(v);

            LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();

            String procSource = "";
            procSource += "/*\r\n";
            procSource += "Rest Invoker\r\n";
            procSource += "\r\n";
            procSource += "Created Automatically by GXUnit\r\n";
            procSource += "*/\r\n";
            procSource += "\r\n";

            procSource += "&HttpClient.Host\t= &Server\r\n";
            procSource += "&HttpClient.Port\t= &port\r\n";
            procSource += "&HttpClient.BaseUrl\t= &UrlBase\r\n";
            procSource += "msg('Server:'\t+ &Server, status)\r\n";
            procSource += "msg('Port:'\t\t+ &Port.ToString(), status)\r\n";
            procSource += "msg('Base url:'\t+ &UrlBase, status)\r\n";
            procSource += "msg('GetString:'+ &GetString, status)\r\n";
            procSource += "msg('Method:'\t+ &Method, status)\r\n";
            procSource += "\r\n";
            procSource += "if not &AddString.IsEmpty()\r\n";
            procSource += "\t&HttpClient.AddString(&AddString)\r\n";
            procSource += "endif\r\n";
            procSource += "\r\n";
            procSource += "if not &HeaderName.IsEmpty()\r\n";
            procSource += "\t&HttpClient.AddHeader(&HeaderName, &HeaderValue)\r\n";
            procSource += "endif\r\n";
            procSource += "\r\n";
            procSource += "&HttpClient.Execute(&Method, &GetString)\r\n";
            procSource += "&HttpStatus\t\t= &HttpClient.StatusCode\r\n";
            procSource += "&ObtainedValue\t= &HttpClient.ToString()\r\n";
            procSource += "\r\n";
            procSource += "msg('Http status: ' + &HttpStatus, status)\r\n";
            procSource += "msg('Output:      ', status)\r\n";
            procSource += "msg('=============', status)\r\n";
            procSource += "msg(&ObtainedValue, status)\r\n";
            procSource += "msg('=============', status)\r\n";
            procSource += "\r\n";

            Procedimiento p = new Procedimiento(nombre, procSource, procRules, "GXUnit", variables, propiedades);
            KBProcedureHandler m = new KBProcedureHandler();
            m.CrearProcedimiento(p, true);

            return true;
        }

        public bool InitializeGXUnit() 
        {
            CreateFoldersGXUnit();

            CreateSDTGXUnitAssert();
            CreateSDTGXUnitTestCase();
            CreateSDTGXUnitSuite();

            CreateGetGXUnitSession();
            CreateSetGXUnitSession();

            CreateAssertStringEquals();
            CreateAssertNumericEquals();

            CreateToolsGetCurrentMillisecs();
            CreateToolsGetEllapsedMillisecs();
            CreateToolsLoadTests();
            CreateToolsRunner();

            CreateRESTInvoker();
            GXUnit.GXUnitUI.GXUnitMainWindow.getInstance().LoadTestTrees();
            return true;
        }

    }
}
