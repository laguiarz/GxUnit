using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class DTTransaccion : DTObjeto
    {
        private LinkedList<DTAtributo> atributos;

        public DTTransaccion(String nombre, LinkedList<DTAtributo> atributos)
            : base (nombre)
        {
            this.atributos = atributos;
        }
        
        public LinkedList<KBParameterHandler> GetVariablesTrn()
        {
            LinkedList<KBParameterHandler> variables = new LinkedList<KBParameterHandler>();
            foreach (DTAtributo att in atributos)
                variables.AddLast(new KBParameterHandler(this.GetNombre(), att.GetNombre(), Constants.PARM_INOUT, att.GetEsClave(), att.GetEsSoloLectura()));
            variables.AddLast(new KBParameterHandler(this.GetNombre(), this.GetNombre(), Constants.PARM_INOUT, Constants.Estructurado.BC, false, false));
            variables.AddLast(new KBParameterHandler("Messages", "Messages", Constants.PARM_INOUT, Constants.Estructurado.SDT, false, false));
            variables.AddLast(new KBParameterHandler("Message", "Messages.Message", Constants.PARM_INOUT, Constants.Estructurado.SDT, false, false));
            return variables;
        }

        public LinkedList<KBParameterHandler> GetAttTrn()
        {
            LinkedList<KBParameterHandler> variables = new LinkedList<KBParameterHandler>();
            foreach (DTAtributo att in atributos)
                variables.AddLast(new KBParameterHandler(this.GetNombre(), att.GetNombre(), Constants.PARM_INOUT, att.GetEsClave(), att.GetEsSoloLectura()));
            return variables;
        }
    }
}
