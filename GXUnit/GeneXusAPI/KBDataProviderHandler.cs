using Artech.Architecture.Common.Objects;
using Artech.Genexus.Common;
using Artech.Genexus.Common.CustomTypes;
using Artech.Genexus.Common.Objects;
using Artech.Genexus.Common.Parts.SDT;
using Artech.Genexus.Common.Types;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    class KBDataProviderHandler
    {
        private static KBDataProviderHandler instance = new KBDataProviderHandler();

        public KBDataProviderHandler()
        {
        }

        public static KBDataProviderHandler GetInstance()
        {
            return instance;
        }

        public DTDataProvider GetDTDataProvider(String objName)
        {
            DTDataProvider dp = null;
            DataProvider dataP = GetDataProviderObject(ContextHandler.Model, objName);
            if (dataP != null)
            {
                //obtengo variables de la regla parm//en un futuro se modifica para variables del CORE
                LinkedList<KBParameterHandler> variablesrule = GetSignature(objName);

                //Agregar las cosas del output
                DataProviderOutputReference dpOutput = dataP.GetPropertyValue<DataProviderOutputReference>(Properties.DPRV.Output);
                string collectionName = dataP.GetPropertyValue<string>(Properties.DPRV.CollectionName);
                bool isCollOutput = dataP.GetPropertyValue<bool>(Properties.DPRV.Collection);

                KBObject obj = KBObject.Get(ContextHandler.Model, dpOutput.EntityKey);

                bool isCollSDT = false;
                String SDTItem = "";
                Constants.Estructurado objType;
                if (obj is Transaction)
                    objType = Constants.Estructurado.BC;
                else
                {
                    objType = Constants.Estructurado.SDT;
                    if (obj is SDT)
                    {
                        SDTLevel n = ((SDT)obj).SDTStructure.Root;
                        isCollSDT = n.IsCollection;
                        SDTItem = n.Name + "." + n.CollectionItemName;
                    }
                }

                String nombretipo = obj.Name;

                dp = new DTDataProvider(objName, nombretipo, objType, isCollOutput, collectionName, isCollSDT, SDTItem, variablesrule);
            }
            else
            {
                String msgoutput = "DataProvider " + objName + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
            }
            return dp;
        }

        public LinkedList<KBParameterHandler> GetSignature(String objName)
        {
            LinkedList<KBParameterHandler> variablesrule = new LinkedList<KBParameterHandler>();
            DataProvider pr = GetDataProviderObject(ContextHandler.Model, objName);
            if (pr != null)
            {
                foreach (Signature sign in pr.GetSignatures())
                {
                    foreach (Parameter parm in sign.Parameters)
                    {
                        if (!parm.IsAttribute && parm.Object != null)
                        {
                            KBParameterHandler p = new KBParameterHandler((Variable)parm.Object, parm.Accessor.ToString(), true);
                            variablesrule.AddLast(p);
                        }

                    }
                }

            }
            return variablesrule;
        }

        public Variable GetVariable(String objName, DTVariable var)
        {
            DataProvider pr = GetDataProviderObject(ContextHandler.Model, objName);
            Variable realVar = new Variable(pr.Variables);
            realVar.Name = var.GetNombre();
            if (var.GetNombreTipoCompuesto() != null)
            {
                DataType.ParseInto(ContextHandler.Model, var.GetNombreTipoCompuesto(), realVar);
                realVar.IsCollection = false;
            }
            else
            {
                realVar.Type = GxHelper.GetGXType(var.GetTipo());
                realVar.Length = var.GetLongitud();
                realVar.Decimals = var.GetDecimales();
            }

            return realVar;
        }

        public static DataProvider GetDataProviderObject(KBModel model, string fullName)
        {
            // Esto depende de la version de la BL de GeneXus.
#if GXTILO
            // Para Tilo:
            return DataProvider.Get(model, new QualifiedName(fullName));
#else
            // Para Ev1 y Ev2:
            return DataProvider.Get(model, fullName);
#endif
        }

    }
}
