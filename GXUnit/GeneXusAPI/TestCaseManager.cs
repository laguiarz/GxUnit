using System;
using System.Collections.Generic;

using Artech.Architecture.UI.Framework.Services;
using Artech.Architecture.Common.Objects;
using Artech.Genexus.Common.Objects;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Types;

using PGGXUnit.Packages.GXUnit.GXUnitCore;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class TestCaseManager
    {
        private KBModel model;
        private static TestCaseManager instance = new TestCaseManager();

        private TestCaseManager()
        {
            model = ManejadorContexto.Model;
        }

        public static TestCaseManager GetInstance()
        {
            return instance;
        }

        public LinkedList<DTTestCase> ListarTestCases()
        {
            LinkedList<DTTestCase> Tests = new LinkedList<DTTestCase>();
            IKBService kbserv = UIServices.KB;
            model = ManejadorContexto.Model;
            try
            {
                foreach (KBObject pr in model.Objects.GetAll())
                {
                    if (pr.GetPropertyValue("TestCase") != null)
                    {
                        if ((bool)pr.GetPropertyValue("TestCase"))
                        {
                            Tests.AddLast(new DTTestCase(pr.Name));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
            return Tests;
        }

        public DTTestCase GetTestCase(String nombre)
        {
            DTTestCase test = null;
            Procedure tc = ManejadorProcedimiento.GetProcedureObject(model, nombre);
            if (tc != null)
            {
                String objectToTest = tc.GetPropertyValueString("ObjectToTest");
                LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
                DTVariable variable;
                foreach (Variable var in tc.Variables.Variables)
                {
                    Constantes.Tipo tipo = FuncionesAuxiliares.GetTipoInterno(var.Type);
                    if (tipo != Constantes.Tipo.NUMERIC && tipo != Constantes.Tipo.CHARACTER && tipo != Constantes.Tipo.VARCHAR && tipo != Constantes.Tipo.LONGVARCHAR)
                        variable = new DTVariable(var.Name, tipo);
                    else
                        variable = new DTVariable(var.Name, tipo, var.Length, var.Decimals);
                    variables.AddFirst(variable);
                }
                test = new DTTestCase(nombre, tc.ProcedurePart.Source, tc.Parent.Name, variables, tc.GetType(), objectToTest);
            }
            return test;
        }

        public bool ModificarTestCase(DTTestCase test, LinkedList<Parametro> vars)
        {
            String msgoutput;

            Procedure tc = ManejadorProcedimiento.GetProcedureObject(model, test.GetNombre());
            if (tc != null)
            {
                tc.ProcedurePart.Source = test.GetSource();
            }
            else
            {
                msgoutput = "Procedure Object " + test.GetNombre() + " does not exists!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
                return false;
            }

            //Agrego Variables
            LinkedList<String> lista = new LinkedList<String>();
            foreach (Variable v in tc.Variables.Variables)
            {
                lista.AddFirst(v.Name);
            }

            foreach (Parametro var in vars)
            {
                if (!lista.Contains(var.GetVariable().Name))
                    AgregarVariable(tc, var.GetVariable());
            }

            tc.Dirty = true;
            tc.Save();

            msgoutput = "Procedure Object " + test.GetNombre() + " modified!";
            FuncionesAuxiliares.EscribirOutput(msgoutput);
            return true;
        }

        public bool EliminarTestCase(DTTestCase testCase)
        {
            String msgoutput;

            Procedure test = ManejadorProcedimiento.GetProcedureObject(model, testCase.GetNombre());
            if (test != null)
            {
                test.Delete();
                msgoutput = "TestCase Object " + testCase.GetNombre() + " deleted!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
                return true;
            }
            else
            {
                msgoutput = "TestCase Object " + testCase.GetNombre() + " does not exists!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
                return false;
            }
        }

        private void AgregarVariable(Procedure test, Variable var)
        {
            test.Variables.Variables.Add(var);
        }

        private void AgregarVariable(Procedure test, DTVariable var1, KBModel model)
        {
            Variable var = new Variable(test.Variables);
            var.Name = var1.GetNombre();
            if (var1.GetTipo() == Constantes.Tipo.SDT)
            {
                DataType.ParseInto(model, var1.GetNombreTipoCompuesto(), var);
                var.IsCollection = false;
                test.Variables.Variables.Add(var);
            }
            else
            {
                if (var1.GetNombreTipoCompuesto() != null)
                {
                    DataType.ParseInto(model, var1.GetNombreTipoCompuesto(), var);
                    var.IsCollection = false;
                    test.Variables.Variables.Add(var);
                }
                else
                {
                    var.Type = FuncionesAuxiliares.GetTipoGX(var1.GetTipo());
                    var.Length = var1.GetLongitud();
                    var.Decimals = var1.GetDecimales();
                    test.Variables.Variables.Add(var);
                }

            }
        }

    }
}
