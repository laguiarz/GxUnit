using System;
using System.Collections.Generic;
using System.Net;
using System.IO;

using Artech.Genexus.Common.Objects;
using Artech.Architecture.Common.Objects;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Types;
using Artech.Common.Properties;

using PGGXUnit.Packages.GXUnit.GXUnitCore;
using Artech.Genexus.Common.Services;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    class KBProcedureHandler
    {
        private KBModel model;

        public KBProcedureHandler()
        {
            this.model = ContextHandler.Model;
        }

        public KBModel GetModel()
        {
            return model;
        }

        public void SetModel(KBModel model)
        {
            this.model = model;
        }

        public bool CrearProcedimiento(Procedimiento procedimiento, bool force)
        {
            String msgoutput;

            Procedure proc = GetProcedureObject(this.model, procedimiento.GetNombre());
            if (proc != null && !force)
            {
                
                msgoutput = "Procedure Object " + procedimiento.GetNombre() + " already exists!";
                GxHelper.WriteOutput(msgoutput);
                return false;
            }
            else
            {
                if (proc == null)
                    proc = new Procedure(model);

                proc.Name = procedimiento.GetNombre();
                proc.ProcedurePart.Source = procedimiento.GetSource();
                proc.Rules.Source = procedimiento.GetRules();
                Folder foldPadre = KBFolderHandler.GetFolderObject(this.model, procedimiento.GetFolder());
                if (foldPadre != null)
                    proc.Parent = foldPadre;
                else
                    proc.Parent = KBFolderHandler.GetRootFolder(model);
            }

            //Agrego Variables
            foreach (DTVariable var in procedimiento.GetVariables())
            {
                AgregarVariable(proc, var);
            }

            //Agrego propiedades
            foreach (DTPropiedad p in procedimiento.GetPropiedades())
            {
                //FuncionesAuxiliares.EscribirOutput(p.GetNombre());
                proc.SetPropertyValue(p.GetNombre(), p.GetValor());
            }

            proc.Save();

            msgoutput = "Procedure Object " + procedimiento.GetNombre() + " created!";
            GxHelper.WriteOutput(msgoutput);
            //FuncionesAuxiliares.EscribirOutput(msgoutput);
            return true;
        }


        public bool ModificarProcedimiento(Procedimiento procedimiento)
        {
            String msgoutput;

            Procedure proc = GetProcedureObject(this.model, procedimiento.GetNombre());
            if (proc != null)
            {
                proc.Rules.Source = procedimiento.GetRules();
                proc.ProcedurePart.Source = procedimiento.GetSource();
            }
            else
            {
                msgoutput = "Procedure Object " + procedimiento.GetNombre() + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
                return false;
            }

            //Agrego Variables
            foreach (DTVariable var in procedimiento.GetVariables())
            {
                AgregarVariable(proc, var);
            }

            //Agrego propiedades
            foreach (DTPropiedad p in procedimiento.GetPropiedades())
            {
                proc.SetPropertyValue(p.GetNombre(), p.GetValor());
            }
            proc.Dirty = true;
            proc.Save();

            msgoutput = "Procedure Object " + procedimiento.GetNombre() + " modified!";
            GxHelper.WriteOutput(msgoutput);
            return true;
        }

        public bool EliminarProcedimiento(String nombre)
        {
            String msgoutput;

            Procedure proc = GetProcedureObject(this.model, nombre);
            if (proc != null)
            {
                try
                {
                    proc.Delete();
                    msgoutput = "Procedure Object " + nombre + " deleted!";
                    GxHelper.WriteOutput(msgoutput);
                }
                catch (Exception e)
                {
                    msgoutput = "Failed to delete " + nombre;
                    GxHelper.WriteOutput(msgoutput);
                    msgoutput = e.Message;
                    GxHelper.WriteOutput(msgoutput);
                }
                return true;
            }
            else
            {
                msgoutput = "Procedure Object " + nombre + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
                return false;
            }
        }

        public LinkedList<KBParameterHandler> GetSignatureProcedimiento(String nombre)
        {
            LinkedList<KBParameterHandler> variablesrule = new LinkedList<KBParameterHandler>();
            Procedure pr = GetProcedureObject(model, nombre);
            if (pr != null)
            {
                foreach (Signature sign in pr.GetSignatures())
                { 
                    foreach (Parameter parm in sign.Parameters)
                    {
                        if (!parm.IsAttribute)
                        {
                            KBParameterHandler p = new KBParameterHandler((Variable)parm.Object, parm.Accessor.ToString(), true);
                            variablesrule.AddLast(p);
                        }
                        else
                        {
                            KBParameterHandler p = new KBParameterHandler(KBTransactionHandler.GetInstance().GetTrnName(parm.Object.Name), parm.Object.Name, parm.Accessor.ToString(), true, true);
                            variablesrule.AddLast(p);
                        }
                        
                    }
                }
                 
            }
            return variablesrule;
        }

        public Procedimiento GetProcedimiento(String nombre)
        {
            Procedimiento p = null;
            Procedure proc = GetProcedureObject(model, nombre);
            if (proc != null)
            {
                LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
                DTVariable variable;
                foreach (Variable var in proc.Variables.Variables)
                {
                    Constants.Tipo tipo = GxHelper.GetInternalType(var.Type);
                    if (tipo != Constants.Tipo.NUMERIC && tipo != Constants.Tipo.CHARACTER && tipo != Constants.Tipo.VARCHAR && tipo != Constants.Tipo.LONGVARCHAR)
                        variable = new DTVariable(var.Name, tipo);
                    else
                        variable = new DTVariable(var.Name, tipo, var.Length, var.Decimals);
                    variables.AddFirst(variable);
                }
                LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();
                DTPropiedad propiedad;
                foreach (Property prop in proc.Properties)
                {
                    propiedad = new DTPropiedad(prop.Name, prop.Value);
                    propiedades.AddFirst(propiedad);
                }
                //obtengo variables de la regla parm//en un futuro se modifica para variables del CORE
                LinkedList<KBParameterHandler> variablesrule = GetSignatureProcedimiento(nombre);
                p = new Procedimiento(nombre, proc.ProcedurePart.Source, proc.Rules.Source, "Objects", variables, propiedades, variablesrule);
            }
            else
            {
                String msgoutput = "Procedure " + nombre + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
            }
            return p;
        }

        private void AgregarVariable(Procedure proc, DTVariable var1)
        {
            Variable var = new Variable(proc.Variables);
            var.Name = var1.GetNombre();
            if (var1.GetTipo() == Constants.Tipo.SDT || var1.GetTipo() == Constants.Tipo.BC)
            {
                DataType.ParseInto(model, var1.GetNombreTipoCompuesto(), var);
                var.IsCollection = var1.GetIsCollection();
                RemoveVariable(proc, var);
                proc.Variables.Variables.Add(var);
            }
            else
            {
                if (var1.GetNombreTipoCompuesto() != null)
                {
                    DataType.ParseInto(model, var1.GetNombreTipoCompuesto(), var);
                    var.IsCollection = var1.GetIsCollection();
                    RemoveVariable(proc, var);
                    proc.Variables.Variables.Add(var);
                }
                else
                {
                    var.Type = GxHelper.GetGXType(var1.GetTipo());
                    var.Length = var1.GetLongitud();
                    var.Decimals = var1.GetDecimales();
                    RemoveVariable(proc, var);
                    proc.Variables.Variables.Add(var);
                }

            }
        }

        private bool RemoveVariable(Procedure proc, Variable var)
        {
            foreach (Variable v in proc.Variables.Variables)
            {
                if (v.Name.ToUpper() == var.Name.ToUpper())
                {
                    proc.Variables.Variables.Remove(v);
                    return true;
                }
            }
            return false;
        }

        private String GetUrlEjecutar(String nombre)
        {
            string retorno = KBManager.GetUrlToRun(nombre);
            //FuncionesAuxiliares.EscribirOutput(retorno);
            return retorno;
        }

        public string EjecutarProcedimiento(String nombre)
        {
            Procedure proc = GetProcedureObject(model, nombre);
                       
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(GetUrlEjecutar(nombre));
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            Stream newStream = myRequest.GetRequestStream();
            newStream.Close();
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();
            Stream streamResponse = myHttpWebResponse.GetResponseStream();
            return nombre;
        }

        public void GenerarProcedimiento(String nombre)
        {
            Procedure proc = GetProcedureObject(model, nombre);
            KBManager.GXRebuildObject(proc.Key);
        }
        
        public void SalvarProcedimiento(String nombre)
        {
            Procedure proc = GetProcedureObject(model, nombre);
            proc.Dirty = true;
            proc.ProcedurePart.Dirty = true;
            proc.Save();
        }

        public void RunProcedimiento(String nombre)
        {
            Procedure proc = GetProcedureObject(model, nombre);
            KBManager.GXRunObject(proc.Key);
        }

        public Variable GetVariable(String nombre, DTVariable var)
        {
            Procedure pr = GetProcedureObject(ContextHandler.Model, nombre);
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

        public static Procedure GetProcedureObject(KBModel model, string procName)
        {
            // Esto depende de la version de la BL de GeneXus.
#if GXTILO
            // Para Tilo:
            return Procedure.Get(model, new QualifiedName(procName));
#else
            // Para Ev1 y Ev2:
            return Procedure.Get(model, procName);
#endif
        }
    }

}
