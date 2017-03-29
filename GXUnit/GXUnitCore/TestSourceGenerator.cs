﻿using System;
using PGGXUnit.Packages.GXUnit.GeneXusAPI;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class TestSourceGenerator
    {
        private static TestSourceGenerator instance = new TestSourceGenerator();

        private TestSourceGenerator()
        {
        }

        public static TestSourceGenerator GetInstance()
        {
            return instance;
        }

        public String GenerateSourceFromObjectToTest(String nombre)
        {
            KBObjectHandler mo = KBObjectHandler.GetInstance();
            DTObjeto objectToTest = mo.GetDTObjeto(nombre);
            
            String source = "";
            
            if (objectToTest != null)
            {
                source = GetSource(objectToTest);
            }
            else
                GxHelper.WriteOutput("Object to test does not exists!");
            return source;
        }

        private String GetSource(DTObjeto objectToTest)
        {
            String source = "";
            if (objectToTest is Procedimiento)
            {
                source = GetSourceForProcedure((Procedimiento)objectToTest);
            }
            else if (objectToTest is DTTransaccion)
            {
                source = GetSourceForTransaction((DTTransaccion)objectToTest);
            }
            //else if (objectToTest is DTDataProvider)
            //{
            //    source = GetSourceForDataProvider((DTDataProvider)objectToTest);
            //}
            return source;
        }

        private String GetSourceForProcedure(Procedimiento objectToTest)
        {
            String source = "/* Setup */\r\n\r\n";
            source += "/* Parameters definition */\r\n";
            foreach (KBParameterHandler parm in objectToTest.GetVariablesRules())
            {
                if (parm.GetTipo() != Constants.PARM_OUT) // Si no es out
                {
                    if (parm.isSimple())
                        source += "&" + parm.GetVariable().Name + " = " + parm.defaultValue() + "\r\n";
                    else if (parm.isSDT())
                    {
                        KBSDTHandler mSDT = new KBSDTHandler();
                        source += mSDT.getSDTTSourceForProcedure(parm.GetVariable(), "");
                    }
                    else
                        source += "//&" + parm.GetVariable().Name + " = \r\n";
                }
            }
            source += "\r\n";
            source += "/* Object call */\r\n";
            source += objectToTest.GetNombre() + ".Call(";
            bool tieneParm = false;
            foreach (KBParameterHandler parm in objectToTest.GetVariablesRules())
            {
                source += "&" + parm.GetVariable().Name + ",";
                tieneParm = true;
            }
            if (tieneParm)
                source = source.Substring(0, source.Length - 1);
            source += ")\r\n";
            source += "\r\n";
            source += "/* Expected values definition */\r\n";
            foreach (KBParameterHandler parm in objectToTest.GetVariablesRules())
            {
                if (parm.isSDT() && parm.GetTipo() == Constants.PARM_OUT)
                {
                    KBSDTHandler mSDT = new KBSDTHandler();
                    source += mSDT.getSDTTSourceForProcedure(parm.GetVariable(), Constants.VALOR_ESPERADO);
                }
            }
            source += "\r\n";
            source += "/* Assertions */\r\n";
            foreach (KBParameterHandler parm in objectToTest.GetVariablesRules())
            {
                if (parm.GetTipo() != Constants.PARM_IN)
                {
                    if (parm.isNumeric())
                    {
                        source += "AssertNumericEquals.Call(&" + parm.GetVariable().Name + ", 0)\r\n\r\n";
                    }
                    else if (parm.isString())
                    {
                        source += "AssertStringEquals.Call(&" + parm.GetVariable().Name + ", '')\r\n\r\n";
                    }
                    else if (parm.isSDT())
                    {
                        source += "&" + Constants.RESULTADO + " = &" + parm.GetVariable().Name + ".ToXML()\r\n";
                        source += "&" + Constants.VALOR_ESPERADO + " = &" + parm.GetVariable().Name + Constants.VALOR_ESPERADO + ".ToXML()\r\n";
                        source += "AssertStringEquals.Call(&" + Constants.RESULTADO + ", &" + Constants.VALOR_ESPERADO + ")\r\n";
                    }

                }
            }
            source += "/* Teardown */\r\n";
            return source;
        }

        private String GetSourceForTransaction(DTTransaccion objectToTest)
        {
            String source = "/* Setup */\r\n\r\n";
            source += "/* Attributes definition */\r\n";
            foreach (KBParameterHandler parm in objectToTest.GetAttTrn())
            {
                if (!parm.GetEsSoloLectura())
                {
                    source += "&" + objectToTest.GetNombre() + ".";
                    source += parm.GetVariable().Name + " = " + parm.defaultValue() + "\r\n";
                }
            }
            source += "&" + objectToTest.GetNombre() + ".Save()\r\n\r\n";
            source += "//Commit\r\n\r\n";
            source += "/* Assertions */\r\n";
            source += "If &" + objectToTest.GetNombre() + ".Fail()\r\n";
            source += "\tFor &Message in &" + objectToTest.GetNombre() + ".GetMessages()\r\n";
            source += "\t\tAssertStringEquals.Call(&Message.Description,'')\r\n";
            source += "\tEndfor\r\n";
            source += "Else\r\n";
            source += "\tFor &Message in &" + objectToTest.GetNombre() + ".GetMessages()\r\n";
            source += "\t\tAssertStringEquals.Call(&Message.Description,'Data has been successfully added.')\r\n";
            source += "\tEndfor\r\n\r\n";

            source += "\t&" + objectToTest.GetNombre() + ".Load(";
            bool hasKey = false;
            foreach (KBParameterHandler parm in objectToTest.GetAttTrn())
            {
                if (parm.GetEsClave())
                {
                    source += parm.defaultValue() + ", ";
                    hasKey = true;
                }
            }
            source = hasKey ? source.Substring(0, source.Length - 2) : source;
            source += ")\r\n";
            foreach (KBParameterHandler parm in objectToTest.GetAttTrn())
            {
                source += "\t&" + parm.GetVariable().Name + " = ";
                source += "&" + objectToTest.GetNombre() + "." + parm.GetVariable().Name + "\r\n";
            }
            source += "\r\n";
            
            foreach (KBParameterHandler parm in objectToTest.GetAttTrn())
            {
                if (parm.isNumeric())
                {
                    source += "\tAssertNumericEquals.Call(&" + parm.GetVariable().Name + ", 0)\r\n\r\n";
                }
                else
                {
                    if (parm.isString())
                    {
                        source += "\tAssertStringEquals.Call(&" + parm.GetVariable().Name + ", '')\r\n\r\n";
                    }
                }
            }
            source += "Endif\r\n\r\n";
            source += "/* Teardown */\r\n";
            return source;
        }

        //private String GetSourceForDataProvider(DTDataProvider objectToTest)
        //{
        //    String source = "/* Setup */\r\n\r\n";
        //    source += "/* Parameters definition */\r\n";
        //    foreach (KBParameterHandler parm in objectToTest.GetParametros())
        //    {
        //        if (parm.GetTipo() != Constants.PARM_OUT) // Si no es out
        //        {
        //            if (parm.isSimple())
        //                source += "&" + parm.GetVariable().Name + " = " + parm.defaultValue() + "\r\n";
        //            else
        //                source += "//&" + parm.GetVariable().Name + " = \r\n";
        //        }
        //    }
        //    source += "\r\n";
        //    source += "/* Object call */\r\n";
        //    source += objectToTest.GetNombre() + ".Call(";
        //    foreach (KBParameterHandler parm in objectToTest.GetParametros())
        //    {
        //        if (parm.GetEstaEnSignature())
        //            source += "&" + parm.GetVariable().Name + ",";
        //    }
        //    source = source.Substring(0, source.Length - 1);
        //    source += ")\r\n";

        //    source += "\r\n";
        //    source += "/* Expected values definition */\r\n";
        //    foreach (KBParameterHandler parm in KBObjectHandler.GetInstance().GetAtt(objectToTest.GetOutput(), objectToTest.GetTipoOutput()))
        //    {
        //        String outputVar = objectToTest.GetOutput();
        //        if (objectToTest.GetIsCollSDT())
        //            outputVar += Constants.ITEM;
        //        if (!objectToTest.GetIsCollOutput() && !objectToTest.GetIsCollSDT())
        //            outputVar += Constants.VALOR_ESPERADO;
        //        if (parm.isSimple())
        //            source += "&" + outputVar + "." + parm.GetVarName() + " = " + parm.defaultValue() + "\r\n";
        //        else
        //            source += "//&" + outputVar + "." + parm.GetVarName() + " = \r\n";
        //    }
        //    if (objectToTest.GetIsCollSDT())
        //    {
        //        String nombre = objectToTest.GetOutput();
        //        if (!objectToTest.GetIsCollOutput())
        //            nombre += Constants.VALOR_ESPERADO;
        //        source += "&" + nombre + ".Add(&" + objectToTest.GetOutput() + Constants.ITEM + ")\r\n";
        //    }
        //    if (objectToTest.GetIsCollOutput())
        //    {
        //        source += "&" + objectToTest.GetOutput() + Constants.VALOR_ESPERADO + ".Add(&" + objectToTest.GetOutput() + ")\r\n";
        //    }

        //    source += "\r\n";
        //    source += "/* Assertions */\r\n";
        //    source += "&" + Constants.RESULTADO + " = &" + objectToTest.GetOutput() + Constants.RESULTADO + ".ToXML()\r\n";
        //    source += "&" + Constants.VALOR_ESPERADO + " = &" + objectToTest.GetOutput() + Constants.VALOR_ESPERADO + ".ToXML()\r\n";
        //    source += "AssertStringEquals.Call(&" + Constants.RESULTADO + ", &" + Constants.VALOR_ESPERADO + ")\r\n";
        //    source += "\r\n/* Teardown */\r\n";
        //    return source;
        //}

    }
}
