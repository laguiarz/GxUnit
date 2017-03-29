/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 * */
using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class GxuProcedure : GxuObject
    {
        private String source;
        private String rules;
        private String folder;
        private LinkedList<GxuVariable> variables;
        private LinkedList<GxuProperty> properties;
        private LinkedList<GxuParm> parmRule;

        public GxuProcedure(String name, String source, String rules, String folder, LinkedList<GxuVariable> variables, LinkedList<GxuProperty> properties)
            : base(name)
        {
            this.source      = source;
            this.rules       = rules;
            this.folder      = folder;
            this.variables   = variables;
            this.properties = properties;
        }

        public GxuProcedure(String name, String source, String rules, String folder, LinkedList<GxuVariable> variables, LinkedList<GxuProperty> properties, LinkedList<GxuParm> parmRule)
            : base(name)
        {
            this.source         = source;
            this.rules          = rules;
            this.folder         = folder;
            this.variables      = variables;
            this.properties     = properties;
            this.parmRule       = parmRule;
        }

        public GxuProcedure(String name, String source, LinkedList<GxuParm> parms)
            : base(name)
        {
            this.source = source;
            this.parmRule = new LinkedList<GxuParm>();

            foreach (GxuParm parm in parms)
            {
                if (parm.IsSDT())
                {
                    GxuSDTHandler msdt = new GxuSDTHandler();
                    if (parm.InOutType == Constants.PARM_IN)
                    {
                        if (msdt.IsCollection(parm.Variable))
                        {
                            this.parmRule.AddLast(new GxuParm(parm.VariableName + Constants.ITEM, msdt.GetItemType(parm.Variable), parm.InOutType, Constants.Estructurado.SDT, false));
                        }
                    }
                    else
                    {
                        try
                        {
                            GxuProcedureHandler mp = new GxuProcedureHandler();
                            GxuVariable var = new GxuVariable(Constants.RESULTADO, Constants.GxuDataType.VARCHAR, 999, 0);
                            this.parmRule.AddLast(new GxuParm( mp.GetVariable(name, var), Constants.PARM_OUT));
                            GxuVariable var2 = new GxuVariable(Constants.VALOR_ESPERADO, Constants.GxuDataType.VARCHAR, 999, 0);
                            this.parmRule.AddLast(new GxuParm(mp.GetVariable(name, var2), Constants.PARM_OUT));

                            this.parmRule.AddLast(new GxuParm(parm.VariableName + Constants.VALOR_ESPERADO, msdt.GetSDTType(parm.Variable), parm.InOutType, Constants.Estructurado.SDT, false));
                            if (msdt.IsCollection(parm.Variable))
                            {
                                this.parmRule.AddLast(new GxuParm(parm.VariableName + Constants.VALOR_ESPERADO + Constants.ITEM, msdt.GetItemType(parm.Variable), parm.InOutType, Constants.Estructurado.SDT, false));
                            }
                        }
                        catch
                        {
                            this.parmRule.AddLast(parm);
                        }

                    }
                }
                this.parmRule.AddLast(parm);
            }
        }

        public LinkedList<GxuVariable> GetVariables()
        {
            return this.variables;
        }

        public LinkedList<GxuParm> GetVariablesRules()
        {
            return this.parmRule;
        }

        public LinkedList<GxuProperty> GetProperties()
        {
            return this.properties;
        }

        public String Source
        {
            get { return source; }
            set { source = value; }
        }

        public String Rules
        {
            get { return rules; }
            set { rules = value; }
        }

        public String Folder
        {
            get { return folder; }
            set { folder = value; }
        }

    }
}
