using System;
using System.Collections.Generic;

using PGGXUnit.Packages.GXUnit.GeneXusAPI;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class GXUnitInicializador
    {

        private static GXUnitInicializador instance = new GXUnitInicializador();

        private GXUnitInicializador()
        {
        }

        public static GXUnitInicializador GetInstance()
        {
            return instance;
        }

        private bool CrearAssertNumericEquals()
        {
            String nombre = "AssertNumericEquals";
            
            String procRules = "Parm(in:&ObtainedValue, in:&ExpectedValue);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("ObtainedValue", Constantes.Tipo.NUMERIC, 18, 4);
            variables.AddFirst(v);
            v = new DTVariable("ExpectedValue", Constantes.Tipo.NUMERIC, 18, 4);
            variables.AddFirst(v);
            v = new DTVariable("SDTAssert", Constantes.Tipo.SDT, "GXUnitAssert");
            variables.AddFirst(v);
            v = new DTVariable("SDTAssertItem", Constantes.Tipo.SDT, "GXUnitAssert.GXUnitAssertInfo");
            variables.AddFirst(v);
            v = new DTVariable("SDTSuite", Constantes.Tipo.SDT, "GXUnitSuite");
            variables.AddFirst(v);
            v = new DTVariable("SDTSuiteTestcase", Constantes.Tipo.SDT, "GXUnitSuite.TestCase");
            variables.AddFirst(v);
            v = new DTVariable("SDTTestCase", Constantes.Tipo.SDT, "GXUnitTestCase");
            variables.AddFirst(v);
            v = new DTVariable("SDTTestCaseAssert", Constantes.Tipo.SDT, "GXUnitTestCase.Assert");
            variables.AddFirst(v);
            v = new DTVariable("Parent", Constantes.Tipo.VARCHAR,40,0);
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
            procSource += "\t&SDTAssertItem.Expected = trim(&ExpectedValue.ToString())\r\n";
            procSource += "\t&SDTAssertItem.Obtained = trim(&ObtainedValue.ToString())\r\n";
            procSource += "\t&SDTAssert.GXUnitAssertInfos.Add(&SDTAssertItem)\r\n";
            procSource += "endif\r\n";
            procSource += "\r\n";

            procSource += "&Parent = GetGXUnitSession('SuiteParent')\r\n";
            procSource += "&SDTSuite.FromXml(GetGXUnitSession(&Parent))\r\n";
            procSource += "&SDTSuiteTestcase = &SDTSuite.TestCases.Item(&SDTSuite.TestCases.Count)\r\n";
            procSource += "&SDTTestCase = &SDTSuiteTestcase.TestCase\r\n";
            procSource += "&SDTTestCaseAssert.Assert = &SDTAssert\r\n";
            procSource += "&SDTTestCase.Asserts.Add(&SDTTestCaseAssert)\r\n";
            procSource += "SetGXUnitSession(&Parent,&SDTSuite.ToXml())\r\n";
            procSource += "\r\n";
            
            Procedimiento p = new Procedimiento(nombre,procSource,procRules,"GXUnit",variables,propiedades);
            ManejadorProcedimiento m = new ManejadorProcedimiento();
            m.CrearProcedimiento(p,true);

            return true;
        }

        private bool CrearAssertStringEquals()
        {
            String nombre = "AssertStringEquals";
            String procRules = "Parm(in:&ObtainedValue, in:&ExpectedValue);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("ObtainedValue", Constantes.Tipo.VARCHAR, 9999, 0);
            variables.AddFirst(v);
            v = new DTVariable("ExpectedValue", Constantes.Tipo.VARCHAR, 9999, 0);
            variables.AddFirst(v);    
            v = new DTVariable("SDTAssert", "GXUnitAssert");
            variables.AddFirst(v); 
            v = new DTVariable("SDTAssertItem", "GXUnitAssert.GXUnitAssertInfo");
            variables.AddFirst(v); 
            v = new DTVariable( "SDTSuite", "GXUnitSuite");
            variables.AddFirst(v);    
            v = new DTVariable("SDTSuiteTestcase", "GXUnitSuite.TestCase");
            variables.AddFirst(v); 
            v = new DTVariable( "SDTTestCase", "GXUnitTestCase");
            variables.AddFirst(v); 
            v = new DTVariable("SDTTestCaseAssert", "GXUnitTestCase.Assert");
            variables.AddFirst(v);
            v = new DTVariable("Parent", Constantes.Tipo.VARCHAR, 40, 0);
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
            procSource += "\t&SDTAssertItem.Expected = &ExpectedValue\r\n";
            procSource += "\t&SDTAssertItem.Obtained = &ObtainedValue\r\n";
            procSource += "\t&SDTAssert.GXUnitAssertInfos.Add(&SDTAssertItem)\r\n";
            procSource += "endif\r\n";
            procSource += "\r\n";

            procSource += "&Parent = GetGXUnitSession('SuiteParent')\r\n";
            procSource += "&SDTSuite.FromXml(GetGXUnitSession(&Parent))\r\n";  
            procSource += "&SDTSuiteTestcase = &SDTSuite.TestCases.Item(&SDTSuite.TestCases.Count)\r\n";
            procSource += "&SDTTestCase = &SDTSuiteTestcase.TestCase\r\n";
            procSource += "&SDTTestCaseAssert.Assert = &SDTAssert\r\n";
            procSource += "&SDTTestCase.Asserts.Add(&SDTTestCaseAssert)\r\n";
            procSource += "SetGXUnitSession(&Parent,&SDTSuite.ToXml())\r\n";
            procSource += "\r\n";

            Procedimiento p = new Procedimiento(nombre, procSource, procRules, "GXUnit", variables, propiedades);
            
            ManejadorProcedimiento m = new ManejadorProcedimiento();
            m.CrearProcedimiento(p,true);
            
            return true;
        }

        private bool CrearSetGXUnitSession()
        {
            String nombre = "SetGXUnitSession";
            String procRules = "parm(in:&Key, in:&Value);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("Key", Constantes.Tipo.VARCHAR, 99999, 0);
            variables.AddFirst(v);
            v = new DTVariable("Value", Constantes.Tipo.VARCHAR, 99999, 0);
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

            ManejadorProcedimiento m = new ManejadorProcedimiento();
            m.CrearProcedimiento(p, true);

            return true;
        }

        private bool CrearGetGXUnitSession()
        {
            String nombre = "GetGXUnitSession";
            String procRules = "parm(in:&Key, out:&Value);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("Key", Constantes.Tipo.VARCHAR, 99999, 0);
            variables.AddFirst(v);
            v = new DTVariable("Value", Constantes.Tipo.VARCHAR, 99999, 0);
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

            ManejadorProcedimiento m = new ManejadorProcedimiento();
            m.CrearProcedimiento(p, true);

            return true;
        }

        private bool CrearSDTGXUnitAssert()
        {
            SDTipoNivelItem item = new SDTipoNivelItem("ObtainedValue", Constantes.Tipo.VARCHAR, 4);
            LinkedList<SDTipoNivelItem> listaitem = new LinkedList<SDTipoNivelItem>();
            listaitem.AddFirst(item);
            SDTipoNivel root = new SDTipoNivel("ROOT", listaitem, false, new LinkedList<SDTipoNivel>());
            item = new SDTipoNivelItem("Obtained", Constantes.Tipo.VARCHAR, 9999);
            listaitem = new LinkedList<SDTipoNivelItem>();
            listaitem.AddFirst(item);
            item = new SDTipoNivelItem("Expected", Constantes.Tipo.VARCHAR, 9999);
            listaitem.AddFirst(item);
            LinkedList<SDTipoNivel> niveles = new LinkedList<SDTipoNivel>();
            niveles.AddFirst(new SDTipoNivel("GXUnitAssertInfo", listaitem, true, new LinkedList<SDTipoNivel>()));
            LinkedList<DTPropiedad> prop = new LinkedList<DTPropiedad>();
            DTPropiedad p = new DTPropiedad("ExternalNamespace", "");
            prop.AddFirst(p);
            SDTipo sdt = new SDTipo("GXUnitAssert", "GXUnit", niveles, root,prop);
            
            ManejadorSDT m = new ManejadorSDT();
            m.CrearSDT(sdt, true);
            return true;
        }

        private bool CrearSDTGXUnitTestCase()
        {
            SDTipoNivelItem item = new SDTipoNivelItem("TestName", Constantes.Tipo.VARCHAR, 40);
            LinkedList<SDTipoNivelItem> listaitem = new LinkedList<SDTipoNivelItem>();
            listaitem.AddFirst(item);
            item = new SDTipoNivelItem("TestTimeExecution", Constantes.Tipo.NUMERIC,16);
            listaitem.AddLast(item);

            SDTipoNivel root = new SDTipoNivel("ROOT", listaitem, false, new LinkedList<SDTipoNivel>());

            item = new SDTipoNivelItem("Assert", Constantes.Tipo.SDT, "GXUnitAssert");
            listaitem = new LinkedList<SDTipoNivelItem>();
            listaitem.AddFirst(item);

            LinkedList<SDTipoNivel> niveles = new LinkedList<SDTipoNivel>();
            niveles.AddFirst(new SDTipoNivel("Assert", listaitem, true, new LinkedList<SDTipoNivel>()));
            LinkedList<DTPropiedad> prop = new LinkedList<DTPropiedad>();
            DTPropiedad p = new DTPropiedad("ExternalNamespace", "");
            prop.AddFirst(p);
            SDTipo sdt = new SDTipo("GXUnitTestCase", "GXUnit", niveles, root,prop);

            ManejadorSDT m = new ManejadorSDT();
            m.CrearSDT(sdt, true);

            return true;
        }

        private bool CrearSDTGXUnitSuite()
        {
            SDTipoNivelItem item = new SDTipoNivelItem("SuiteName", Constantes.Tipo.VARCHAR, 40);
            LinkedList<SDTipoNivelItem> listaitem = new LinkedList<SDTipoNivelItem>();
            listaitem.AddFirst(item);
            SDTipoNivel root = new SDTipoNivel("ROOT", listaitem, false, new LinkedList<SDTipoNivel>());

            item = new SDTipoNivelItem("TestCase", Constantes.Tipo.SDT, "GXUnitTestCase");
            listaitem = new LinkedList<SDTipoNivelItem>();
            listaitem.AddFirst(item);

            LinkedList<SDTipoNivel> niveles = new LinkedList<SDTipoNivel>();
            niveles.AddFirst(new SDTipoNivel("TestCase", listaitem, true, new LinkedList<SDTipoNivel>()));

            item = new SDTipoNivelItem("Suite", Constantes.Tipo.SDT, "GXUnitSuite");
            listaitem = new LinkedList<SDTipoNivelItem>();
            listaitem.AddFirst(item);

            niveles.AddLast(new SDTipoNivel("Suite", listaitem, true, new LinkedList<SDTipoNivel>()));
            LinkedList<DTPropiedad> prop = new LinkedList<DTPropiedad>();
            DTPropiedad p = new DTPropiedad("ExternalNamespace", "");
            
            prop.AddFirst(p);
            SDTipo sdt = new SDTipo("GXUnitSuite", "GXUnit", niveles, root,prop);

            ManejadorSDT m = new ManejadorSDT();
            m.CrearSDT(sdt, true);

            return true;
        }

        private bool CrearCarpetasGXUnit()
        {
            DTFolder folder1 = new DTFolder(Constantes.carpetaGXUnit, "");
            ManejadorFolder mf = new ManejadorFolder();
            mf.CrearFolder(folder1, true);

            DTFolder GXUnitSuites = new DTFolder(Constantes.carpetaSuites, Constantes.carpetaGXUnit);
            mf.CrearFolder(GXUnitSuites, true);

            DTFolder GXUnitResults = new DTFolder(Constantes.carpetaResults, Constantes.carpetaGXUnit);
            mf.CrearFolder(GXUnitResults, true);
            return true;
        }

        private bool CrearRESTInvoker()
        {
            String nombre = "RESTInvoker";

            String procRules = "parm(in:&Server, in:&Port, in:&UrlBase, in:&GetString, in:&Method, in:&AddString, in:&HeaderName, in:&HeaderValue, out:&HttpStatus, out:&ObtainedValue);";

            LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
            DTVariable v = new DTVariable("AddString", Constantes.Tipo.LONGVARCHAR, 2097152, 0);
            variables.AddFirst(v);
            v = new DTVariable("GetString", Constantes.Tipo.CHARACTER, 200, 0);
            variables.AddFirst(v);
            v = new DTVariable("HeaderName", Constantes.Tipo.CHARACTER, 80, 0);
            variables.AddFirst(v);
            v = new DTVariable("HeaderValue", Constantes.Tipo.CHARACTER, 80, 0);
            variables.AddFirst(v);
            v = new DTVariable("HttpClient", Constantes.Tipo.SDT, "HttpClient");
            variables.AddFirst(v);
            v = new DTVariable("HttpStatus", Constantes.Tipo.NUMERIC, 6, 0);
            variables.AddFirst(v);
            v = new DTVariable("Method", Constantes.Tipo.CHARACTER, 15, 0);
            variables.AddFirst(v);
            v = new DTVariable("Port", Constantes.Tipo.NUMERIC, 6, 0);
            variables.AddFirst(v);
            v = new DTVariable("ObtainedValue", Constantes.Tipo.LONGVARCHAR, 2097152, 0);
            variables.AddFirst(v);
            v = new DTVariable("Server", Constantes.Tipo.CHARACTER, 50, 0);
            variables.AddFirst(v);
            v = new DTVariable("Text", Constantes.Tipo.CHARACTER, 500, 0);
            variables.AddFirst(v);
            v = new DTVariable("UrlBase", Constantes.Tipo.CHARACTER, 80, 0);
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
            ManejadorProcedimiento m = new ManejadorProcedimiento();
            m.CrearProcedimiento(p, true);

            return true;
        }

        public bool InicializarGXUnit() 
        {
            CrearCarpetasGXUnit();
            CrearSDTGXUnitAssert();
            CrearSDTGXUnitTestCase();
            CrearSDTGXUnitSuite();
            CrearGetGXUnitSession();
            CrearSetGXUnitSession();
            CrearAssertStringEquals();
            CrearAssertNumericEquals();
            
            CrearRESTInvoker();
            GXUnit.GXUnitUI.GXUnitMainWindow.getInstance().cargarNodosTest();
            return true;
        }

    }
}
