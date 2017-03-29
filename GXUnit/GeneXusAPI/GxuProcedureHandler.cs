/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 * */

using System;
using System.Net;
using System.IO;

using Artech.Genexus.Common.Objects;
using Artech.Architecture.Common.Objects;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Types;

using PGGXUnit.Packages.GXUnit.GXUnitCore;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    class GxuProcedureHandler
    {
        private KBModel model;

        public GxuProcedureHandler()
        {
            this.model = ContextHandler.Model;
        }

        public KBModel Model
        {
            get { return model; }
            set { model = value; }

        }

        public bool CreateProcedure(GxuProcedure gxuProcedure, bool force)
        {
            String msgoutput;

            try
            {
                Procedure proc = GetProcedureObject(this.model, gxuProcedure.Name);
                if (proc != null && !force)
                {

                    msgoutput = "Procedure Object " + gxuProcedure.Name + " already exists!";
                    GxHelper.WriteOutput(msgoutput);
                    return false;
                }
                else
                {
                    if (proc == null)
                        proc = new Procedure(model);

                    proc.Name = gxuProcedure.Name;
                    proc.ProcedurePart.Source = gxuProcedure.Source;
                    proc.Rules.Source = gxuProcedure.Rules;

                    Folder parentFolder = GxuFolderHandler.GetFolderObject(this.model, gxuProcedure.Folder);
                    if (parentFolder != null)
                        proc.Parent = parentFolder;
                    else
                        proc.Parent = GxuFolderHandler.GetRootFolder(model);
                }

                //Agrego Variables
                foreach (GxuVariable var in gxuProcedure.GetVariables())
                {
                    AddVariable(proc, var);
                }

                //Agrego propiedades
                foreach (GxuProperty p in gxuProcedure.GetProperties())
                {
                    proc.SetPropertyValue(p.Name, p.PropertyValue);
                }

                proc.Save();

                msgoutput = "Procedure Object " + gxuProcedure.Name + " created!";
                GxHelper.WriteOutput(msgoutput);
            }
            catch (Exception e)
            {
                GxHelper.WriteOutput("Failed to Create " + gxuProcedure.Name);
                GxHelper.WriteOutput(e.Message);
            }
            return true;
        }


        //public bool ModificarProcedimiento(GxuProcedure procedimiento)
        //{
        //    String msgoutput;

        //    Procedure proc = GetProcedureObject(this.model, procedimiento.Name);
        //    if (proc != null)
        //    {
        //        proc.Rules.Source = procedimiento.Rules;
        //        proc.ProcedurePart.Source = procedimiento.Source;
        //    }
        //    else
        //    {
        //        msgoutput = "Procedure Object " + procedimiento.Name + " does not exists!";
        //        GxHelper.WriteOutput(msgoutput);
        //        return false;
        //    }

        //    //Agrego Variables
        //    foreach (DTVariable var in procedimiento.GetVariables())
        //    {
        //        AgregarVariable(proc, var);
        //    }

        //    //Agrego propiedades
        //    foreach (GxuProperty p in procedimiento.GetProperties())
        //    {
        //        proc.SetPropertyValue(p.Name, p.PropertyValue);
        //    }
        //    proc.Dirty = true;
        //    proc.Save();

        //    msgoutput = "Procedure Object " + procedimiento.Name + " modified!";
        //    GxHelper.WriteOutput(msgoutput);
        //    return true;
        //}

        public bool DeleteProcedure(String nombre)
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

  
        private void AddVariable(Procedure proc, GxuVariable var1)
        {
            Variable var = new Variable(proc.Variables);
            var.Name = var1.Name;
            if (var1.DataType == Constants.GxuDataType.SDT || var1.DataType == Constants.GxuDataType.BC)
            {
                DataType.ParseInto(model, var1.ComplexTypeName, var);
                var.IsCollection = var1.IsCollection;
                RemoveVariable(proc, var);
                proc.Variables.Variables.Add(var);
            }
            else
            {
                if (var1.ComplexTypeName != null)
                {
                    DataType.ParseInto(model, var1.ComplexTypeName, var);
                    var.IsCollection = var1.IsCollection;
                    RemoveVariable(proc, var);
                    proc.Variables.Variables.Add(var);
                }
                else
                {
                    var.Type = GxHelper.ConvertToGXType(var1.DataType);
                    var.Length = var1.Length;
                    var.Decimals = var1.Length;
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

        private String GetUrlToRun(String name)
        {
            string url = KBManager.GetUrlToRun(name);
            return url;
        }

 
        public void Build(String procName)
        {
            Procedure proc = GetProcedureObject(model, procName);
            if (proc != null)
            {
                KBManager.GXRebuildObject(proc.Key);
            }
            else
            {
                GxHelper.WriteOutput("Failed to obtain procedure from model");
            }
        }
        
        public void Save(String procName)
        {
            Procedure proc = GetProcedureObject(model, procName);
            proc.Dirty = true;
            proc.ProcedurePart.Dirty = true;
            proc.Save();
        }

        public void Run(String procName)
        {
            Procedure proc = GetProcedureObject(model, procName);
            KBManager.GXRunObject(proc.Key);
        }

        public Variable GetVariable(String procName, GxuVariable gxuVariable)
        {
            Procedure proc = GetProcedureObject(ContextHandler.Model, procName);
            Variable variable = new Variable(proc.Variables);
            variable.Name = gxuVariable.Name;
            if (gxuVariable.ComplexTypeName != null)
            {
                DataType.ParseInto(ContextHandler.Model, gxuVariable.ComplexTypeName, variable);
                variable.IsCollection = false;
            }
            else
            {
                variable.Type = GxHelper.ConvertToGXType(gxuVariable.DataType);
                variable.Length = gxuVariable.Length;
                variable.Decimals = gxuVariable.Decimals;
            }

            return variable;
        }

        public static Procedure GetProcedureObject(KBModel kbmodel, string procName)
        {
            // Esto depende de la version de la BL de GeneXus.
#if GXTILO
            // Para Tilo:

            return Procedure.Get(kbmodel, new QualifiedName(procName));
          
#else
            // Para Ev1 y Ev2:
            return Procedure.Get(model, procName);
#endif
        }
    }

    //public string EjecutarProcedimiento(String nombre)
    //{
    //    Procedure proc = GetProcedureObject(model, nombre);

    //    HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(GetUrlToRun(nombre));
    //    myRequest.Method = "POST";
    //    myRequest.ContentType = "application/x-www-form-urlencoded";
    //    Stream newStream = myRequest.GetRequestStream();
    //    newStream.Close();
    //    HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();
    //    Stream streamResponse = myHttpWebResponse.GetResponseStream();
    //    return nombre;
    //}
    //public LinkedList<KBParameterHandler> GetSignatureProcedimiento(String nombre)
    //{
    //    LinkedList<KBParameterHandler> variablesrule = new LinkedList<KBParameterHandler>();
    //    Procedure pr = GetProcedureObject(model, nombre);
    //    if (pr != null)
    //    {
    //        foreach (Signature sign in pr.GetSignatures())
    //        { 
    //            foreach (Parameter parm in sign.Parameters)
    //            {
    //                if (!parm.IsAttribute)
    //                {
    //                    KBParameterHandler p = new KBParameterHandler((Variable)parm.Object, parm.Accessor.ToString(), true);
    //                    variablesrule.AddLast(p);
    //                }
    //                else
    //                {
    //                    KBParameterHandler p = new KBParameterHandler(KBTransactionHandler.GetInstance().GetTrnName(parm.Object.Name), parm.Object.Name, parm.Accessor.ToString(), true, true);
    //                    variablesrule.AddLast(p);
    //                }

    //            }
    //        }

    //    }
    //    return variablesrule;
    //}

    //public Procedimiento GetProcedimiento(String nombre)
    //{
    //    Procedimiento p = null;
    //    Procedure proc = GetProcedureObject(model, nombre);
    //    if (proc != null)
    //    {
    //        LinkedList<DTVariable> variables = new LinkedList<DTVariable>();
    //        DTVariable variable;
    //        foreach (Variable var in proc.Variables.Variables)
    //        {
    //            Constants.Tipo tipo = GxHelper.GetInternalType(var.Type);
    //            if (tipo != Constants.Tipo.NUMERIC && tipo != Constants.Tipo.CHARACTER && tipo != Constants.Tipo.VARCHAR && tipo != Constants.Tipo.LONGVARCHAR)
    //                variable = new DTVariable(var.Name, tipo);
    //            else
    //                variable = new DTVariable(var.Name, tipo, var.Length, var.Decimals);
    //            variables.AddFirst(variable);
    //        }
    //        LinkedList<DTPropiedad> propiedades = new LinkedList<DTPropiedad>();
    //        DTPropiedad propiedad;
    //        foreach (Property prop in proc.Properties)
    //        {
    //            propiedad = new DTPropiedad(prop.Name, prop.Value);
    //            propiedades.AddFirst(propiedad);
    //        }
    //        //obtengo variables de la regla parm//en un futuro se modifica para variables del CORE
    //        LinkedList<KBParameterHandler> variablesrule = GetSignatureProcedimiento(nombre);
    //        p = new Procedimiento(nombre, proc.ProcedurePart.Source, proc.Rules.Source, "Objects", variables, propiedades, variablesrule);
    //    }
    //    else
    //    {
    //        String msgoutput = "Procedure " + nombre + " does not exists!";
    //        GxHelper.WriteOutput(msgoutput);
    //    }
    //    return p;
    //}
}
