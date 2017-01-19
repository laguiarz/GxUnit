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
    class ManejadorDataProvider
    {
        private static ManejadorDataProvider instance = new ManejadorDataProvider();

        public ManejadorDataProvider()
        {
        }

        public static ManejadorDataProvider GetInstance()
        {
            return instance;
        }

        public DTDataProvider GetDTDataProvider(String nombre)
        {
            DTDataProvider dp = null;
            DataProvider dataP = GetDataProviderObject(ManejadorContexto.Model, nombre);
            if (dataP != null)
            {
                //obtengo variables de la regla parm//en un futuro se modifica para variables del CORE
                LinkedList<Parametro> variablesrule = GetSignature(nombre);

                //Agregar las cosas del output
                DataProviderOutputReference dpOutput = dataP.GetPropertyValue<DataProviderOutputReference>(Properties.DPRV.Output);
                string collectionName = dataP.GetPropertyValue<string>(Properties.DPRV.CollectionName);
                bool isCollOutput = dataP.GetPropertyValue<bool>(Properties.DPRV.Collection);

                KBObject ob = KBObject.Get(ManejadorContexto.Model, dpOutput.EntityKey);

                bool isCollSDT = false;
                String SDTItem = "";
                Constantes.Estructurado tipo;
                if (ob is Transaction)
                    tipo = Constantes.Estructurado.BC;
                else
                {
                    tipo = Constantes.Estructurado.SDT;
                    if (ob is SDT)
                    {
                        SDTLevel n = ((SDT)ob).SDTStructure.Root;
                        isCollSDT = n.IsCollection;
                        SDTItem = n.Name + "." + n.CollectionItemName;
                    }
                }

                String nombretipo = ob.Name;

                dp = new DTDataProvider(nombre, nombretipo, tipo, isCollOutput, collectionName, isCollSDT, SDTItem, variablesrule);
            }
            else
            {
                String msgoutput = "DataProvider " + nombre + " does not exists!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
            }
            return dp;
        }

        public LinkedList<Parametro> GetSignature(String nombre)
        {
            LinkedList<Parametro> variablesrule = new LinkedList<Parametro>();
            DataProvider pr = GetDataProviderObject(ManejadorContexto.Model, nombre);
            if (pr != null)
            {
                foreach (Signature sign in pr.GetSignatures())
                {
                    foreach (Parameter parm in sign.Parameters)
                    {
                        if (!parm.IsAttribute && parm.Object != null)
                        {
                            Parametro p = new Parametro((Variable)parm.Object, parm.Accessor.ToString(), true);
                            variablesrule.AddLast(p);
                        }

                    }
                }

            }
            return variablesrule;
        }

        public Variable GetVariable(String nombre, DTVariable var)
        {
            DataProvider pr = GetDataProviderObject(ManejadorContexto.Model, nombre);
            Variable realVar = new Variable(pr.Variables);
            realVar.Name = var.GetNombre();
            if (var.GetNombreTipoCompuesto() != null)
            {
                DataType.ParseInto(ManejadorContexto.Model, var.GetNombreTipoCompuesto(), realVar);
                realVar.IsCollection = false;
            }
            else
            {
                realVar.Type = FuncionesAuxiliares.GetTipoGX(var.GetTipo());
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
