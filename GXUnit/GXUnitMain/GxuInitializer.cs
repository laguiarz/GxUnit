using System;
using System.Collections.Generic;
using PGGXUnit.Packages.GXUnit.GeneXusAPI;


namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class GxuInitializer
    {

        private static GxuInitializer instance = new GxuInitializer();

        private GxuInitializer()
        {
        }

        public static GxuInitializer GetInstance()
        {
            return instance;
        }


        private string AssertProcSource()
        {
            String procSource = "";

            procSource += "/*\r\n";
            procSource += "Compares obtained and expected results\r\n";
            procSource += "\r\n";
            procSource += "Created Automatically by GXUnit\r\n";
            procSource += "*/\r\n";
            procSource += "\r\n";
            procSource += "&SDTAssert = new()\r\n";
            procSource += "&SDTAssert.Variable = &VariableName\r\n";
            procSource += "&SDTAssert.Expected = trim(&ExpectedValue.ToString())\r\n";
            procSource += "&SDTAssert.Obtained = trim(&ObtainedValue.ToString())\r\n";
            procSource += "&SDTAssert.Result   = iif(&ExpectedValue = &ObtainedValue,'OK','FAIL')\r\n";
            procSource += "\r\n";

            procSource += "//Retrieve the Current-Test Information\r\n";
            procSource += "GXUnit_GetSession('CurrentTest', &SessionValue)\r\n";
            procSource += "&GXUnitTestCase.FromXml(&SessionValue)\r\n";
            procSource += "\r\n";

            procSource += "&GXUnitTestCase.Asserts.Add(&SDTAssert)\r\n";
            procSource += "GXUnit_SetSession('CurrentTest', &GXUnitTestCase.ToXml() )\r\n";
            procSource += "\r\n";

            return procSource;
        }
        private bool CreateAssertNumericEquals()
        {
            String nombre = "GXUnit_AssertNumericEquals";
            
            String procRules = "Parm(in:&VariableName, in:&ObtainedValue, in:&ExpectedValue);";

            LinkedList<GxuVariable> variables = new LinkedList<GxuVariable>();
            GxuVariable v = new GxuVariable("ObtainedValue", Constants.GxuDataType.NUMERIC, 18, 4);
            variables.AddFirst(v);
            v = new GxuVariable("ExpectedValue", Constants.GxuDataType.NUMERIC, 18, 4);
            variables.AddFirst(v);
            v = new GxuVariable("GXUnitTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            v = new GxuVariable("SDTAssert", Constants.GxuDataType.SDT, "GXUnitAssert");
            variables.AddFirst(v);
            v = new GxuVariable("SDTSuite", Constants.GxuDataType.SDT, "GXUnitSuite");
            variables.AddFirst(v);
            v = new GxuVariable("SessionValue", Constants.GxuDataType.VARCHAR, 9999, 0);
            variables.AddFirst(v);
            v = new GxuVariable("VariableName", Constants.GxuDataType.VARCHAR, 80, 0);
            variables.AddFirst(v);

            LinkedList<GxuProperty> propiedades = new LinkedList<GxuProperty>();

            String procSource = AssertProcSource();
           
            GxuProcedure p = new GxuProcedure(nombre,procSource,procRules,"GXUnit",variables,propiedades);
            GxuProcedureHandler m = new GxuProcedureHandler();
            m.CreateProcedure(p,true);

            return true;
        }

        private bool CreateAssertStringEquals()
        {
            String nombre = "GXUnit_AssertStringEquals";
            String procRules = "Parm(in:&VariableName, in:&ObtainedValue, in:&ExpectedValue);";

            LinkedList<GxuVariable> variables = new LinkedList<GxuVariable>();
            GxuVariable v = new GxuVariable("ObtainedValue", Constants.GxuDataType.VARCHAR, 1024, 4);
            variables.AddFirst(v);
            v = new GxuVariable("ExpectedValue", Constants.GxuDataType.VARCHAR, 1024, 4);
            variables.AddFirst(v);
            v = new GxuVariable("GXUnitTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            v = new GxuVariable("SDTAssert", Constants.GxuDataType.SDT, "GXUnitAssert");
            variables.AddFirst(v);
            v = new GxuVariable("SDTSuite", Constants.GxuDataType.SDT, "GXUnitSuite");
            variables.AddFirst(v);
            v = new GxuVariable("SessionValue", Constants.GxuDataType.VARCHAR, 9999, 0);
            variables.AddFirst(v);
            v = new GxuVariable("VariableName", Constants.GxuDataType.VARCHAR, 80, 0);
            variables.AddFirst(v);

            LinkedList<GxuProperty> propiedades = new LinkedList<GxuProperty>();

            String procSource = AssertProcSource();

            GxuProcedure p = new GxuProcedure(nombre, procSource, procRules, "GXUnit", variables, propiedades);
            
            GxuProcedureHandler m = new GxuProcedureHandler();
            m.CreateProcedure(p,true);
            
            return true;
        }

        private bool CreateToolsGetCurrentMillisecs()
        {
            String nombre = "GXUnit_GetCurrentMillisecs";
            String procRules = "Parm(out:&DateTime, out:&Milliseconds);";

            LinkedList<GxuVariable> variables = new LinkedList<GxuVariable>();
            GxuVariable v = new GxuVariable("DateTime", Constants.GxuDataType.DATETIME);
            variables.AddFirst(v);
            v = new GxuVariable("Milliseconds", Constants.GxuDataType.NUMERIC, 16, 0);
            variables.AddFirst(v);

            LinkedList<GxuProperty> propiedades = new LinkedList<GxuProperty>();
            GxuProperty property = new GxuProperty("SPC_WARNINGS_DISABLED", "spc0096 spc0107 spc0142 spc0087");
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

            GxuProcedure p = new GxuProcedure(nombre, procSource, procRules, "GXUnit", variables, propiedades);

            GxuProcedureHandler m = new GxuProcedureHandler();
            m.CreateProcedure(p, true);

            return true;
        }

        private bool CreateToolsGetEllapsedMillisecs()
        {
            String nombre = "GXUnit_GetElapsedMilliseconds";
            String procRules = "parm(in:&StartDateTime, in:&StartMilliseconds, 	out:&ElapsedMilliseconds);";

            LinkedList<GxuVariable> variables = new LinkedList<GxuVariable>();
            GxuVariable v = new GxuVariable("StartDateTime", Constants.GxuDataType.DATETIME);
            variables.AddFirst(v);
            v = new GxuVariable("StartMilliseconds", Constants.GxuDataType.NUMERIC, 16, 0);
            variables.AddFirst(v);
            v = new GxuVariable("ElapsedMilliseconds", Constants.GxuDataType.NUMERIC, 16, 0);
            variables.AddFirst(v);

            LinkedList<GxuProperty> propiedades = new LinkedList<GxuProperty>();
            GxuProperty property = new GxuProperty("SPC_WARNINGS_DISABLED", "spc0096 spc0107 spc0142 spc0087");
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

            GxuProcedure p = new GxuProcedure(nombre, procSource, procRules, "GXUnit", variables, propiedades);

            GxuProcedureHandler m = new GxuProcedureHandler();
            m.CreateProcedure(p, true);

            return true;
        }

        public bool CreateToolsLoadTests()
        {
            String nombre = "GXUnit_LoadTests";
            String procRules = "parm(out:&GXUnitSuiteCollection, out:&ResultFileName);";

            LinkedList<GxuVariable> variables = new LinkedList<GxuVariable>();
            GxuVariable v = new GxuVariable("GXUnitSuite", "GXUnitSuite");
            variables.AddFirst(v);
            v = new GxuVariable("GXUnitSuiteCollection", "GXUnitSuite", true); 
            variables.AddFirst(v);
            v = new GxuVariable("GXUnitSuiteTestCase", "GXUnitSuite.TestCase"); 
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

            GxuProcedure p = new GxuProcedure(nombre, procSource, procRules, "GXUnit", variables, propiedades);

            GxuProcedureHandler m = new GxuProcedureHandler();
            m.CreateProcedure(p, true);

            return true;
        }


        private bool CreateToolsRunner()
        {
            String nombre = Constants.RUNNER_PROC; 
            String procRules = "";

            LinkedList<GxuVariable> variables = new LinkedList<GxuVariable>();
            GxuVariable v = new GxuVariable("ExecutedGXUnitTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            v = new GxuVariable("FileName", Constants.GxuDataType.VARCHAR, 512, 0);
            variables.AddFirst(v);
            v = new GxuVariable("GXUnitAssert", "GXUnitAssert");
            variables.AddFirst(v);
            v = new GxuVariable("GXUnitSuite", "GXUnitSuite");
            variables.AddFirst(v);
            v = new GxuVariable("GXUnitSuiteCollection", "GXUnitSuite", true); 
            variables.AddFirst(v);
            v = new GxuVariable("GXUnitTestCase", "GXUnitTestCase");
            variables.AddFirst(v);
            v = new GxuVariable("Milliseconds", Constants.GxuDataType.NUMERIC, 16, 0);
            variables.AddFirst(v);
            v = new GxuVariable("OutputGXUnitSuite", "GXUnitSuite");
            variables.AddFirst(v);
            v = new GxuVariable("SessionValue", Constants.GxuDataType.VARCHAR, 9999,0);
            variables.AddFirst(v);
            v = new GxuVariable("StartDateTime", Constants.GxuDataType.DATETIME);
            variables.AddFirst(v);
            v = new GxuVariable("TestPgmName", Constants.GxuDataType.VARCHAR, 128, 0);
            variables.AddFirst(v);
            v = new GxuVariable("xmlWriter", "XMLWriter");
            variables.AddFirst(v);
            v = new GxuVariable("FoundFailFlag", Constants.GxuDataType.Boolean);
            variables.AddFirst(v);
            

            LinkedList<GxuProperty> properties = new LinkedList<GxuProperty>();
            GxuProperty property = new GxuProperty("IsMain", true);
            properties.AddFirst(property);

            property = new GxuProperty("CALL_PROTOCOL", Artech.Genexus.Common.Properties.PRC.CallProtocol_Values.CommandLine);
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
            procSource += "\t\tGXUnit_SetSession('CurrentTest',&GXUnitTestCase.ToXml())\r\n";
            procSource += "\t\tGXUnit_GetCurrentMillisecs(&StartDateTime, &Milliseconds)\r\n";
            procSource += "\r\n";
            procSource += "\t\t&testPgmName = &GXUnitTestCase.TestName.Trim()\r\n";
            procSource += "\t\tcall(&testPgmName)\r\n";
            procSource += "\r\n";
            procSource += "\t\t//At This point in the Session we have the &GXUnitSuiteTestCase Assert Portion Saved\r\n";
            procSource += "\t\t//So let's retrieve it...\r\n";
            procSource += "\t\tGXUnit_GetSession('CurrentTest', &SessionValue)\r\n";
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
            procSource += "\t\t\t&GXUnitAssert.Result = 'EXCEPTION'\r\n";
            procSource += "\t\t\t&GXUnitTestCase.Asserts.Add(&GXUnitAssert)\r\n";
            procSource += "\t\tendif\r\n";
            procSource += "//Now Evaluate the test-result based on its asserts\r\n";
            procSource += "\t\t&FoundFailFlag = false\r\n";
            procSource += "\t\tfor &GXUnitAssert in &GXUnitTestCase.Asserts\r\n";
            procSource += "\t\t\tif &GXUnitAssert.Result = 'EXCEPTION' or &GXUnitAssert.Result = 'FAIL'\r\n";
            procSource += "\t\t\t\t&GXUnitTestCase.TestResult = 'FAIL'\r\n";
            procSource += "\t\t\t\t&FoundFailFlag = true\r\n";
            procSource += "\t\t\t\texit\r\n";
            procSource += "\t\t\tendif\r\n";
            procSource += "\t\tendfor\r\n";
            procSource += "\r\n";
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
            procSource += "&xmlWriter.Open(&FileName)\r\n";
            procSource += "&xmlWriter.WriteRawText(&OutputGXUnitSuite.ToXml())\r\n";
            procSource += "&xmlWriter.Close()\r\n";
            procSource += "//";

            GxuProcedure p = new GxuProcedure(nombre, procSource, procRules, "GXUnit", variables, properties);

            GxuProcedureHandler m = new GxuProcedureHandler();
            m.CreateProcedure(p, true);

            return true;
        }

        private bool CreateGXUnit_SetSession()
        {
            String nombre = "GXUnit_SetSession";
            String procRules = "parm(in:&Key, in:&Value);";

            LinkedList<GxuVariable> variables = new LinkedList<GxuVariable>();
            GxuVariable v = new GxuVariable("Key", Constants.GxuDataType.VARCHAR, 99999, 0);
            variables.AddFirst(v);
            v = new GxuVariable("Value", Constants.GxuDataType.VARCHAR, 99999, 0);
            variables.AddFirst(v);
            v = new GxuVariable("XmlWriter", "XmlWriter");
            variables.AddFirst(v);
            
            LinkedList<GxuProperty> propiedades = new LinkedList<GxuProperty>();

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

            GxuProcedure p = new GxuProcedure(nombre, procSource, procRules, "GXUnit", variables, propiedades);

            GxuProcedureHandler m = new GxuProcedureHandler();
            m.CreateProcedure(p, true);

            return true;
        }

        private bool CreateGXUnit_GetSession()
        {
            String nombre = "GXUnit_GetSession";
            String procRules = "parm(in:&Key, out:&Value);";

            LinkedList<GxuVariable> variables = new LinkedList<GxuVariable>();
            GxuVariable v = new GxuVariable("Key", Constants.GxuDataType.VARCHAR, 99999, 0);
            variables.AddFirst(v);
            v = new GxuVariable("Value", Constants.GxuDataType.VARCHAR, 99999, 0);
            variables.AddFirst(v);
            v = new GxuVariable("XmlReader", "XmlReader");
            variables.AddFirst(v);

            LinkedList<GxuProperty> propiedades = new LinkedList<GxuProperty>();

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

            GxuProcedure p = new GxuProcedure(nombre, procSource, procRules, Constants.GXUNIT_FOLDER, variables, propiedades);

            GxuProcedureHandler m = new GxuProcedureHandler();
            m.CreateProcedure(p, true);

            return true;
        }

        private bool CreateSDTGXUnitAssert()
        { 
           GxuSDTLevel root = new GxuSDTLevel("ROOT", false);

            //Add Items
            GxuSDTItem item = new GxuSDTItem("Result", Constants.GxuDataType.VARCHAR, 40);
            root.AddItem(item);
            
            item = new GxuSDTItem("Variable", Constants.GxuDataType.VARCHAR, 80);
            root.AddItem(item);

            item = new GxuSDTItem("Obtained", Constants.GxuDataType.VARCHAR, 9999);
            root.AddItem(item);
            
            item = new GxuSDTItem("Expected", Constants.GxuDataType.VARCHAR, 9999);
            root.AddItem(item);

            LinkedList<GxuProperty> prop = new LinkedList<GxuProperty>();
            GxuProperty p = new GxuProperty("ExternalNamespace", "");
            prop.AddFirst(p);
            GxuSDT sdt = new GxuSDT("GXUnitAssert", Constants.GXUNIT_FOLDER, null, root,prop);
            
            GxuSDTHandler m = new GxuSDTHandler();
            m.CreateSDT(sdt, true);
            return true;
        }

        private bool CreateSDTGXUnitTestCase()
        {

            GxuSDTLevel root = new GxuSDTLevel("ROOT", false);

            //Add Items
            GxuSDTItem item = new GxuSDTItem("TestName", Constants.GxuDataType.VARCHAR, 40);
            root.AddItem(item);
            item = new GxuSDTItem("TestTimeExecution", Constants.GxuDataType.NUMERIC, 16);
            root.AddItem(item);
            item = new GxuSDTItem("TestResult", Constants.GxuDataType.VARCHAR, 40);
            root.AddItem(item);

            //Add Test-Case Level
            GxuSDTCollectionItem colItem = new GxuSDTCollectionItem("Assert", Constants.GxuDataType.SDT, "GXUnitAssert");
            root.AddCollectionItem(colItem);

            LinkedList<GxuProperty> prop = new LinkedList<GxuProperty>();
            GxuProperty p = new GxuProperty("ExternalNamespace", "");
            prop.AddFirst(p);
            GxuSDT sdt = new GxuSDT("GXUnitTestCase", Constants.GXUNIT_FOLDER, null, root,prop);

            GxuSDTHandler m = new GxuSDTHandler();
            m.CreateSDT(sdt, true);

            return true;
        }

        private bool CreateSDTGXUnitSuite()
        {


            GxuSDTLevel root = new GxuSDTLevel("ROOT", false);

            //Add Items
            GxuSDTItem item = new GxuSDTItem("SuiteName", Constants.GxuDataType.VARCHAR, 40);
            root.AddItem(item);

            //Add Test-Case Level
            GxuSDTCollectionItem colItem = new GxuSDTCollectionItem("TestCase", Constants.GxuDataType.SDT, "GXUnitTestCase");
            root.AddCollectionItem(colItem);

            //Add Suites-Level
            colItem = new GxuSDTCollectionItem("Suite", Constants.GxuDataType.SDT, "GXUnitSuite");
            root.AddCollectionItem(colItem);

            LinkedList<GxuProperty> prop = new LinkedList<GxuProperty>();
            GxuProperty p = new GxuProperty("ExternalNamespace", "");
            
            prop.AddFirst(p);
            GxuSDT sdt = new GxuSDT("GXUnitSuite", Constants.GXUNIT_FOLDER, null, root, prop);

            GxuSDTHandler m = new GxuSDTHandler();
            m.CreateSDT(sdt, true);

            return true;
        }

        private bool CreateFoldersGXUnit()
        {
            GxuFolder folder1 = new GxuFolder(Constants.GXUNIT_FOLDER, "");
            GxuFolderHandler mf = new GxuFolderHandler();
            mf.CreateFolder(folder1, true);

            GxuFolder GXUnitSuites = new GxuFolder(Constants.SUITES_FOLDER, Constants.GXUNIT_FOLDER);
            mf.CreateFolder(GXUnitSuites, true);

            return true;
        }

        private bool CreateRESTInvoker()
        {
            String nombre = "RESTInvoker";

            String procRules = "parm(in:&Server, in:&Port, in:&UrlBase, in:&GetString, in:&Method, in:&AddString, in:&HeaderName, in:&HeaderValue, out:&HttpStatus, out:&ObtainedValue);";

            LinkedList<GxuVariable> variables = new LinkedList<GxuVariable>();
            GxuVariable v = new GxuVariable("AddString", Constants.GxuDataType.LONGVARCHAR, 2097152, 0);
            variables.AddFirst(v);
            v = new GxuVariable("GetString", Constants.GxuDataType.CHARACTER, 200, 0);
            variables.AddFirst(v);
            v = new GxuVariable("HeaderName", Constants.GxuDataType.CHARACTER, 80, 0);
            variables.AddFirst(v);
            v = new GxuVariable("HeaderValue", Constants.GxuDataType.CHARACTER, 80, 0);
            variables.AddFirst(v);
            v = new GxuVariable("HttpClient", Constants.GxuDataType.SDT, "HttpClient");
            variables.AddFirst(v);
            v = new GxuVariable("HttpStatus", Constants.GxuDataType.NUMERIC, 6, 0);
            variables.AddFirst(v);
            v = new GxuVariable("Method", Constants.GxuDataType.CHARACTER, 15, 0);
            variables.AddFirst(v);
            v = new GxuVariable("Port", Constants.GxuDataType.NUMERIC, 6, 0);
            variables.AddFirst(v);
            v = new GxuVariable("ObtainedValue", Constants.GxuDataType.LONGVARCHAR, 2097152, 0);
            variables.AddFirst(v);
            v = new GxuVariable("Server", Constants.GxuDataType.CHARACTER, 50, 0);
            variables.AddFirst(v);
            v = new GxuVariable("Text", Constants.GxuDataType.CHARACTER, 500, 0);
            variables.AddFirst(v);
            v = new GxuVariable("UrlBase", Constants.GxuDataType.CHARACTER, 80, 0);
            variables.AddFirst(v);

            LinkedList<GxuProperty> propiedades = new LinkedList<GxuProperty>();

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

            GxuProcedure p = new GxuProcedure(nombre, procSource, procRules, "GXUnit", variables, propiedades);
            GxuProcedureHandler m = new GxuProcedureHandler();
            m.CreateProcedure(p, true);

            return true;
        }

        public bool InitializeGXUnit() 
        {
            CreateFoldersGXUnit();

            CreateSDTGXUnitAssert();
            CreateSDTGXUnitTestCase();
            CreateSDTGXUnitSuite();

            CreateGXUnit_GetSession();
            CreateGXUnit_SetSession();

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
