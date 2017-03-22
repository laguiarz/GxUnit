using Artech.Architecture.Common.Services;
using Artech.Genexus.Common;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class GxHelper
    {

        /// <summary>
        /// Writes msg in the GeneXus output
        /// </summary>
        /// <param name="msg">Message to show in the GeneXus output</param>
        /// <returns>true</returns>
        public static bool WriteOutput(String msg)
        {
            IOutputService output = CommonServices.Output;
            output.AddLine(msg);
            return true;
        }

        public static Constants.Tipo GetInternalType(eDBType type)
        {
            Constants.Tipo internalType;
            switch (type)
            {
                case eDBType.Boolean:
                    internalType = Constants.Tipo.Boolean;
                    break;
                case eDBType.CHARACTER:
                    internalType = Constants.Tipo.CHARACTER;
                    break;
                case eDBType.DATE:
                    internalType = Constants.Tipo.DATE;
                    break;
                case eDBType.DATETIME:
                    internalType = Constants.Tipo.DATETIME;
                    break;
                case eDBType.GX_BUSCOMP:
                    internalType = Constants.Tipo.BC;
                    break;
                case eDBType.GX_BUSCOMP_LEVEL:
                    internalType = Constants.Tipo.BC_LEVEL;
                    break;
                case eDBType.GX_EXTERNAL_OBJECT:
                    internalType = Constants.Tipo.EXTERNAL_OBJECT;
                    break;
                case eDBType.GX_SDT:
                    internalType = Constants.Tipo.SDT;
                    break;
                case eDBType.INT:
                    internalType = Constants.Tipo.INT;
                    break;
                case eDBType.LONGVARCHAR:
                    internalType = Constants.Tipo.LONGVARCHAR;
                    break;
                case eDBType.NUMERIC:
                    internalType = Constants.Tipo.NUMERIC;
                    break;
                case eDBType.VARCHAR:
                    internalType = Constants.Tipo.VARCHAR;
                    break;
                default:
                    internalType = Constants.Tipo.NUMERIC;
                    break;
            }
            return internalType;
        }

        public static eDBType GetGXType(Constants.Tipo type)
        {
            eDBType dbType;
            switch (type)
            {
                case Constants.Tipo.Boolean:
                    dbType = eDBType.Boolean;
                    break;
                case Constants.Tipo.CHARACTER:
                    dbType = eDBType.CHARACTER;
                    break;
                case Constants.Tipo.DATE:
                    dbType = eDBType.DATE;
                    break;
                case Constants.Tipo.DATETIME:
                    dbType = eDBType.DATETIME;
                    break;
                case Constants.Tipo.BC:
                    dbType = eDBType.GX_BUSCOMP;
                    break;
                case Constants.Tipo.BC_LEVEL:
                    dbType = eDBType.GX_BUSCOMP_LEVEL;
                    break;
                case Constants.Tipo.EXTERNAL_OBJECT:
                    dbType = eDBType.GX_EXTERNAL_OBJECT;
                    break;
                case Constants.Tipo.SDT:
                    dbType = eDBType.GX_SDT;
                    break;
                case Constants.Tipo.INT:
                    dbType = eDBType.INT;
                    break;
                case Constants.Tipo.LONGVARCHAR:
                    dbType = eDBType.LONGVARCHAR;
                    break;
                case Constants.Tipo.NUMERIC:
                    dbType = eDBType.NUMERIC;
                    break;
                case Constants.Tipo.VARCHAR:
                    dbType = eDBType.VARCHAR;
                    break;
                default:
                    dbType = eDBType.NUMERIC;
                    break;
            }
            return dbType;
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
            return GxHelper.isBoolean(varType) || GxHelper.isNumeric(varType) || GxHelper.isString(varType) || GxHelper.isDate(varType);
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
