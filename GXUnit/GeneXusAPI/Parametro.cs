using Artech.Genexus.Common;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class Parametro
    {
        private Variable variable;

        //tipo:
        //Constantes.PARM_INOUT
        //Constantes.PARM_IN
        //Constantes.PARM_OUT
        private String tipo;
        private bool esClave;
        private bool esSoloLectura;
        private bool estaEnSignature;
        private String varName;
        private eDBType varType;
        private String nomTipoComplejo;

        //Variable basada en Tipo complejo (BC o SDT)
        public Parametro(String varName, String Name, String t, Constantes.Estructurado objeto, bool isColl, bool estaEnSig)
        {
            if (objeto == Constantes.Estructurado.BC)
                this.variable = ManejadorTransaccion.GetInstance().GetBCVariable(varName, Name, isColl);
            else
                this.variable = (new ManejadorSDT()).GetSDTVariable(varName, Name, isColl);
            this.varName = variable.Name;
            this.varType = variable.Type;
            this.tipo = t;
            this.estaEnSignature = estaEnSig;
            this.nomTipoComplejo = Name;
        }

        //Variable basada en Atributo
        public Parametro(String trnName, String attName, String t, bool clv, bool soloLectura)
        {
            variable = ManejadorTransaccion.GetInstance().GetVariable(trnName, attName);
            varName = variable.Name;
            varType = variable.Type;
            tipo = t;
            esClave = clv;
            esSoloLectura = soloLectura;
            nomTipoComplejo = null;
        }

        //Variable a partir de VariableGX
        public Parametro(Variable var, String t, bool estaEnSig)
        {
            if (estaEnSig && FuncionesAuxiliares.isStandardVar(var.Name))
                var.Name = var.Name + "1";
            variable = var;
            varName = variable.Name;
            varType = variable.Type;
            tipo = t;
            estaEnSignature = estaEnSig;
        }

        //Variable simple
        public Parametro(String name, eDBType type)
        {
            varName = name;
            varType = type;
        }

        public Variable GetVariable()
        {
            return variable;
        }

        public String GetTipo()
        {
            return tipo;
        }

        public bool isNumeric()
        {
            return FuncionesAuxiliares.isNumeric(varType);
        }

        public bool isString()
        {
            return FuncionesAuxiliares.isString(varType);
        }

        public bool isBoolean()
        {
            return FuncionesAuxiliares.isBoolean(varType);
        }

        public bool isDate()
        {
            return FuncionesAuxiliares.isDate(varType);
        }

        public bool isSimple()
        {
            return FuncionesAuxiliares.isSimple(varType);
        }

        public bool isSDT()
        {
            return varType == eDBType.GX_SDT;
        }

        public String defaultValue()
        {
            String def = "''";
            if (this.isBoolean())
                def = "false";
            else
                if (this.isNumeric())
                    def = "0";
                else
                    if (this.isDate())
                        def = "ymdtod(1,1,1)";
            return def;
        }

        public bool GetEsClave()
        {
            return esClave;
        }

        public bool GetEsSoloLectura()
        {
            return esSoloLectura;
        }

        public bool GetEstaEnSignature()
        {
            return estaEnSignature;
        }

        public String GetVarName()
        {
            return varName;
        }

        public eDBType GetVarType()
        {
            return varType;
        }

        public String GetNomTipoComplejo()
        {
            return nomTipoComplejo;
        }
    }
}
