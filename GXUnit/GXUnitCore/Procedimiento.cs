using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class Procedimiento : DTObjeto
    {
        private String source;
        private String rules;
        private String folder;
        private LinkedList<DTVariable> variables;
        private LinkedList<DTPropiedad> propiedades;
        //cambiar variablesrule a unalista de variables del CORE
        private LinkedList<KBParameterHandler> variablesrule;

        /* Crea procedimientos de GXUint y el runner */
        public Procedimiento(String nombre, String source, String rules, String folder, LinkedList<DTVariable> variables, LinkedList<DTPropiedad> propiedades)
            : base(nombre)
        {
            this.source      = source;
            this.rules       = rules;
            this.folder      = folder;
            this.variables   = variables;
            this.propiedades = propiedades;
        }

        /* Crea el procedimiento en la funcion GetProcedimiento */
        public Procedimiento(String nombre, String source, String rules, String folder, LinkedList<DTVariable> variables, LinkedList<DTPropiedad> propiedades, LinkedList<KBParameterHandler> variablesrule)
            : base(nombre)
        {
            this.source         = source;
            this.rules          = rules;
            this.folder         = folder;
            this.variables      = variables;
            this.propiedades    = propiedades;
            this.variablesrule  = variablesrule;
        }

        /* Crea el procedimiento en la funcion GetDTTestCase */
        public Procedimiento(String Nombre, String source, LinkedList<KBParameterHandler> variablesrule)
            : base(Nombre)
        {
            this.source = source;
            this.variablesrule = new LinkedList<KBParameterHandler>();
            foreach (KBParameterHandler param in variablesrule)
            {
                if (param.isSDT())
                {
                    KBSDTHandler msdt = new KBSDTHandler();
                    if (param.GetTipo() == Constants.PARM_IN)
                    {
                        if (msdt.isColl(param.GetVariable()))
                        {
                            this.variablesrule.AddLast(new KBParameterHandler(param.GetVarName() + Constants.ITEM, msdt.getItemType(param.GetVariable()), param.GetTipo(), Constants.Estructurado.SDT, false, false));
                        }
                    }
                    else
                    {
                        try
                        {
                            KBProcedureHandler mp = new KBProcedureHandler();
                            DTVariable var = new DTVariable(Constants.RESULTADO, Constants.Tipo.VARCHAR, 999, 0);
                            this.variablesrule.AddLast(new KBParameterHandler(mp.GetVariable(Nombre, var), Constants.PARM_OUT, false));
                            DTVariable var2 = new DTVariable(Constants.VALOR_ESPERADO, Constants.Tipo.VARCHAR, 999, 0);
                            this.variablesrule.AddLast(new KBParameterHandler(mp.GetVariable(Nombre, var2), Constants.PARM_OUT, false));

                            this.variablesrule.AddLast(new KBParameterHandler(param.GetVarName() + Constants.VALOR_ESPERADO, msdt.getSDTType(param.GetVariable()), param.GetTipo(), Constants.Estructurado.SDT, false, false));
                            if (msdt.isColl(param.GetVariable()))
                            {
                                this.variablesrule.AddLast(new KBParameterHandler(param.GetVarName() + Constants.VALOR_ESPERADO + Constants.ITEM, msdt.getItemType(param.GetVariable()), param.GetTipo(), Constants.Estructurado.SDT, false, false));
                            }
                        }
                        catch
                        {
                            this.variablesrule.AddLast(param);
                        }

                    }
                }
                this.variablesrule.AddLast(param);
            }
        }

        public LinkedList<DTVariable> GetVariables()
        {
            return this.variables;
        }

        public LinkedList<KBParameterHandler> GetVariablesRules()
        {
            return this.variablesrule;
        }

        public LinkedList<DTPropiedad> GetPropiedades()
        {
            return this.propiedades;
        }

        public String GetSource()
        {
            return this.source;
        }

        public String GetRules()
        {
            return this.rules;
        }

        public String GetFolder()
        {
            return this.folder;
        }

    }
}
