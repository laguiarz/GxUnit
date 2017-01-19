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
        
        public LinkedList<Parametro> GetVariablesTrn()
        {
            LinkedList<Parametro> variables = new LinkedList<Parametro>();
            foreach (DTAtributo att in atributos)
                variables.AddLast(new Parametro(this.GetNombre(), att.GetNombre(), Constantes.PARM_INOUT, att.GetEsClave(), att.GetEsSoloLectura()));
            variables.AddLast(new Parametro(this.GetNombre(), this.GetNombre(), Constantes.PARM_INOUT, Constantes.Estructurado.BC, false, false));
            variables.AddLast(new Parametro("Messages", "Messages", Constantes.PARM_INOUT, Constantes.Estructurado.SDT, false, false));
            variables.AddLast(new Parametro("Message", "Messages.Message", Constantes.PARM_INOUT, Constantes.Estructurado.SDT, false, false));
            return variables;
        }

        public LinkedList<Parametro> GetAttTrn()
        {
            LinkedList<Parametro> variables = new LinkedList<Parametro>();
            foreach (DTAtributo att in atributos)
                variables.AddLast(new Parametro(this.GetNombre(), att.GetNombre(), Constantes.PARM_INOUT, att.GetEsClave(), att.GetEsSoloLectura()));
            return variables;
        }
    }
}
