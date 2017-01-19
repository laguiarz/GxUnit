using Artech.Architecture.Common.Services;
using Artech.Genexus.Common;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class FuncionesAuxiliares
    {

        /// <summary>
        /// Escribe un mensaje en el Output de GX
        /// </summary>
        /// <param name="msg">Mensaje a escribir</param>
        /// <returns>true</returns>
        public static bool EscribirOutput(String msg)
        {
            IOutputService output = CommonServices.Output;
            //output.StartSection("LogGXUnit");
            output.AddLine(msg);
            //output.EndSection("LogGXUnit", true);
            return true;
        }

        public static Constantes.Tipo GetTipoInterno(eDBType type)
        {
            Constantes.Tipo tipo;
            switch (type)
            {
                case eDBType.Boolean:
                    tipo = Constantes.Tipo.Boolean;
                    break;
                case eDBType.CHARACTER:
                    tipo = Constantes.Tipo.CHARACTER;
                    break;
                case eDBType.DATE:
                    tipo = Constantes.Tipo.DATE;
                    break;
                case eDBType.DATETIME:
                    tipo = Constantes.Tipo.DATETIME;
                    break;
                case eDBType.GX_BUSCOMP:
                    tipo = Constantes.Tipo.BC;
                    break;
                case eDBType.GX_BUSCOMP_LEVEL:
                    tipo = Constantes.Tipo.BC_LEVEL;
                    break;
                case eDBType.GX_EXTERNAL_OBJECT:
                    tipo = Constantes.Tipo.EXTERNAL_OBJECT;
                    break;
                case eDBType.GX_SDT:
                    tipo = Constantes.Tipo.SDT;
                    break;
                case eDBType.INT:
                    tipo = Constantes.Tipo.INT;
                    break;
                case eDBType.LONGVARCHAR:
                    tipo = Constantes.Tipo.LONGVARCHAR;
                    break;
                case eDBType.NUMERIC:
                    tipo = Constantes.Tipo.NUMERIC;
                    break;
                case eDBType.VARCHAR:
                    tipo = Constantes.Tipo.VARCHAR;
                    break;
                default:
                    tipo = Constantes.Tipo.NUMERIC;
                    break;
            }
            return tipo;
        }

        public static eDBType GetTipoGX(Constantes.Tipo type)
        {
            eDBType tipo;
            switch (type)
            {
                case Constantes.Tipo.Boolean:
                    tipo = eDBType.Boolean;
                    break;
                case Constantes.Tipo.CHARACTER:
                    tipo = eDBType.CHARACTER;
                    break;
                case Constantes.Tipo.DATE:
                    tipo = eDBType.DATE;
                    break;
                case Constantes.Tipo.DATETIME:
                    tipo = eDBType.DATETIME;
                    break;
                case Constantes.Tipo.BC:
                    tipo = eDBType.GX_BUSCOMP;
                    break;
                case Constantes.Tipo.BC_LEVEL:
                    tipo = eDBType.GX_BUSCOMP_LEVEL;
                    break;
                case Constantes.Tipo.EXTERNAL_OBJECT:
                    tipo = eDBType.GX_EXTERNAL_OBJECT;
                    break;
                case Constantes.Tipo.SDT:
                    tipo = eDBType.GX_SDT;
                    break;
                case Constantes.Tipo.INT:
                    tipo = eDBType.INT;
                    break;
                case Constantes.Tipo.LONGVARCHAR:
                    tipo = eDBType.LONGVARCHAR;
                    break;
                case Constantes.Tipo.NUMERIC:
                    tipo = eDBType.NUMERIC;
                    break;
                case Constantes.Tipo.VARCHAR:
                    tipo = eDBType.VARCHAR;
                    break;
                default:
                    tipo = eDBType.NUMERIC;
                    break;
            }
            return tipo;
        }

        public static bool isNumeric(eDBType varType)
        {
            return varType == eDBType.INT || varType == eDBType.NUMERIC;
        }

        public static bool isString(eDBType varType)
        {
            return varType == eDBType.CHARACTER || varType == eDBType.LONGVARCHAR || varType == eDBType.VARCHAR;
        }

        public static bool isBoolean(eDBType varType)
        {
            return varType == eDBType.Boolean;
        }

        public static bool isDate(eDBType varType)
        {
            return (varType == eDBType.DATE) || (varType == eDBType.DATETIME);
        }

        public static bool isSimple(eDBType varType)
        {
            return FuncionesAuxiliares.isBoolean(varType) || FuncionesAuxiliares.isNumeric(varType) || FuncionesAuxiliares.isString(varType) || FuncionesAuxiliares.isDate(varType);
        }

        public static String defaultValue(Variable var)
        {
            String def = "''";
            if (var.Type == eDBType.Boolean)
                def = "true";
            else
                if ((var.Type == eDBType.INT) || (var.Type == eDBType.NUMERIC))
                    def = "0";
                else
                    if ((var.Type == eDBType.DATE) || (var.Type == eDBType.DATETIME))
                        def = "ymdtod(1,1,1)";
            return def;
        }

        public static bool isStandardVar(string var)
        {
            return (var == "Line") || (var == "Output") || (var == "Page") || (var == "Pgmdesc") || (var == "Pgmname") || (var == "Time") || (var == "Today");
        }

    }
}
