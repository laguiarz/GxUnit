using Artech.Architecture.Common.Objects;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Objects;
using Artech.Genexus.Common.Parts;
using Artech.Genexus.Common.Types;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    class KBTransactionHandler
    {
        private static KBTransactionHandler instance = new KBTransactionHandler();

        public KBTransactionHandler()
        {
        }

        public static KBTransactionHandler GetInstance()
        {
            return instance;
        }

        public DTTransaccion GetDTTransaccion(String nombre)
        {
            DTTransaccion t = null;
            Transaction trn = GetTransactionObject(ContextHandler.Model, nombre);
            if (trn != null)
            {
                if (!trn.IsBusinessComponent)
                {
                    String msg = "The Transaction must be Business Component";
                    ContextHandler.Message = msg;
                    return null;
                }

                LinkedList<DTAtributo> atributos = new LinkedList<DTAtributo>();
                foreach (TransactionAttribute att in trn.Structure.GetAttributes())
                {
                    if (att.TableAttribute != null && att.TableAttribute.Table != null /*&& att.TableAttribute.Table.Name.ToLower() == nombre.ToLower()*/)
                    {
                        Constants.Tipo tipo = GxHelper.GetInternalType(att.Attribute.Type);
                        atributos.AddLast(new DTAtributo(att.Name, tipo, att.IsKey, att.IsInferred || att.Attribute.Formula != null));
                    }
                }
                t = new DTTransaccion(nombre, atributos);
            }
            else
            {
                String msgoutput = "Transaction " + nombre + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
            }
            return t;
        }

        public Variable GetVariable(String trnName, String attName)
        {
            Transaction trn = GetTransactionObject(ContextHandler.Model, trnName);
            if (trn != null)
            {
                foreach (TransactionAttribute att in trn.Structure.GetAttributes())
                {
                    if (att.Name == attName)
                    {
                        Variable var = new Variable(trn.Variables);
                        var.Name = att.Attribute.Name;
                        var.AttributeBasedOn = att.Attribute;
                        var.Length = att.Attribute.Length;
                        var.Decimals = att.Attribute.Decimals;
                        var.Type = att.Attribute.Type;
                        
                        //DataType.ParseInto(mc.GetModelo(), attName, var);
                        return var;
                    }
                }
                String msgoutput = "Attribute " + attName + " does not exists in Transaction " + trnName + "!";
                GxHelper.WriteOutput(msgoutput);
            }
            else
            {
                String msgoutput = "Transaction " + trnName + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
            }
            return null;
        }

        public Variable GetBCVariable(String varName, String trnName, bool isColl)
        {
            Transaction trn = GetTransactionObject(ContextHandler.Model, trnName);
            if (trn != null)
            {
                
                Variable var = new Variable(trn.Variables);
                var.Name = varName;
                var.Type = eDBType.GX_BUSCOMP;
                var.IsCollection = isColl;

                DataType.ParseInto(ContextHandler.Model, trnName, var);
                return var;
            }
            else
            {
                String msgoutput = "Transaction " + trnName + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
                return null;
            }
        }

        public LinkedList<KBParameterHandler> GetAtt(String bc)
        {
            LinkedList<KBParameterHandler> atributos = new LinkedList<KBParameterHandler>();
            Transaction trn = GetTransactionObject(ContextHandler.Model, bc);
            if (trn != null)
            {
                foreach (TransactionAttribute att in trn.Structure.GetAttributes())
                {
                    if (!att.Attribute.IsReadOnly && att.Attribute.Formula == null)
                    {
                        Variable var = new Variable(trn.Variables);
                        var.Name = att.Attribute.Name;
                        var.AttributeBasedOn = att.Attribute;
                        var.Length = att.Attribute.Length;
                        var.Decimals = att.Attribute.Decimals;
                        var.Type = att.Attribute.Type;
                        atributos.AddLast(new KBParameterHandler(var, Constants.PARM_OUT, false));
                    }
                }
            }
            else
            {
                String msgoutput = "Transaction " + bc + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
            }
            return atributos;
        }

        //Retorna el nombre de la TRN que contiene al Atributo
        public String GetTrnName(String attribute)
        {
            foreach (KBObject trn in ContextHandler.Model.Objects.GetAll())
            {
                if (trn is Transaction)
                {
                    foreach (TransactionAttribute att in ((Transaction)trn).Structure.GetAttributes())
                    {
                        if (att.Name.ToLower() == attribute.ToLower())
                        {
                            return trn.Name;
                        }
                    }
                }
            }
            return "";
        }

        public static Transaction GetTransactionObject(KBModel model, string name)
        {
            // Esto depende de la version de la BL de GeneXus.
#if GXTILO
            // Para Tilo:
            return Transaction.Get(model, new QualifiedName(name));
#else
            // Para Ev1 y Ev2:
            return Transaction.Get(model, name);
#endif
        }
    }
}
