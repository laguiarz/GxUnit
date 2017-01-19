using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class ManejadorDTTestCase
    {
        private static ManejadorDTTestCase instance = new ManejadorDTTestCase();


        private ManejadorDTTestCase()
        {
        }

        public static ManejadorDTTestCase GetInstance()
        {
            return instance;
        }

        public Procedimiento GetDTTestCase(String objectToTest)
        {
            TestSourceGenerator tsg = TestSourceGenerator.GetInstance();
            string source = tsg.GenerateSourceFromObjectToTest(objectToTest);

            DTObjeto oToTest = ManejadorObjeto.GetInstance().GetDTObjeto(objectToTest);
            LinkedList<Parametro> parametros = new LinkedList<Parametro>();

            if (oToTest != null)
            {
                if (oToTest is Procedimiento)
                    parametros = ((Procedimiento)oToTest).GetVariablesRules();
                else if (oToTest is DTTransaccion)
                    parametros = ((DTTransaccion)oToTest).GetVariablesTrn();
                else if (oToTest is DTDataProvider)
                    parametros = ((DTDataProvider)oToTest).GetParametros();
            }
            else
            {
                string msg = ManejadorContexto.Message;
                if (msg != "")
                {
                    MessageBox.Show(msg);
                }
                return null;
            }

            Procedimiento proc = new Procedimiento(objectToTest, source, parametros);
            return proc;
        }
    }
}
