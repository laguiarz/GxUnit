using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class DTTransaccion : GxuObject
    {
        private LinkedList<GxuAttribute> atributos;

        public DTTransaccion(String nombre, LinkedList<GxuAttribute> atributos)
            : base (nombre)
        {
            this.atributos = atributos;
        }
        
        public LinkedList<GxuParm> GetVariablesTrn()
        {
            LinkedList<GxuParm> variables = new LinkedList<GxuParm>();
            foreach (GxuAttribute att in atributos)
                variables.AddLast(new GxuParm(this.Name, att.Name, Constants.PARM_INOUT, att.IsKey, att.IsReadOnly));
            variables.AddLast(new GxuParm(this.Name, this.Name, Constants.PARM_INOUT, Constants.Estructurado.BC, false));
            variables.AddLast(new GxuParm("Messages", "Messages", Constants.PARM_INOUT, Constants.Estructurado.SDT, false));
            variables.AddLast(new GxuParm("Message", "Messages.Message", Constants.PARM_INOUT, Constants.Estructurado.SDT, false));
            return variables;
        }

        public LinkedList<GxuParm> GetAttTrn()
        {
            LinkedList<GxuParm> variables = new LinkedList<GxuParm>();
            foreach (GxuAttribute att in atributos)
                variables.AddLast(new GxuParm(this.Name, att.Name, Constants.PARM_INOUT, att.IsKey, att.IsReadOnly));
            return variables;
        }
    }
}
