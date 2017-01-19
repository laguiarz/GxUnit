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
        private LinkedList<Parametro> variablesrule;

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
        public Procedimiento(String nombre, String source, String rules, String folder, LinkedList<DTVariable> variables, LinkedList<DTPropiedad> propiedades, LinkedList<Parametro> variablesrule)
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
        public Procedimiento(String Nombre, String source, LinkedList<Parametro> variablesrule)
            : base(Nombre)
        {
            this.source = source;
            this.variablesrule = new LinkedList<Parametro>();
            foreach (Parametro param in variablesrule)
            {
                if (param.isSDT())
                {
                    ManejadorSDT msdt = new ManejadorSDT();
                    if (param.GetTipo() == Constantes.PARM_IN)
                    {
                        if (msdt.isColl(param.GetVariable()))
                        {
                            this.variablesrule.AddLast(new Parametro(param.GetVarName() + Constantes.ITEM, msdt.getItemType(param.GetVariable()), param.GetTipo(), Constantes.Estructurado.SDT, false, false));
                        }
                    }
                    else
                    {
                        try
                        {
                            ManejadorProcedimiento mp = new ManejadorProcedimiento();
                            DTVariable var = new DTVariable(Constantes.RESULTADO, Constantes.Tipo.VARCHAR, 999, 0);
                            this.variablesrule.AddLast(new Parametro(mp.GetVariable(Nombre, var), Constantes.PARM_OUT, false));
                            DTVariable var2 = new DTVariable(Constantes.VALOR_ESPERADO, Constantes.Tipo.VARCHAR, 999, 0);
                            this.variablesrule.AddLast(new Parametro(mp.GetVariable(Nombre, var2), Constantes.PARM_OUT, false));

                            this.variablesrule.AddLast(new Parametro(param.GetVarName() + Constantes.VALOR_ESPERADO, msdt.getSDTType(param.GetVariable()), param.GetTipo(), Constantes.Estructurado.SDT, false, false));
                            if (msdt.isColl(param.GetVariable()))
                            {
                                this.variablesrule.AddLast(new Parametro(param.GetVarName() + Constantes.VALOR_ESPERADO + Constantes.ITEM, msdt.getItemType(param.GetVariable()), param.GetTipo(), Constantes.Estructurado.SDT, false, false));
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

        public LinkedList<Parametro> GetVariablesRules()
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
