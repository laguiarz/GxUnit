using Artech.Architecture.Common.Services;
using Artech.Genexus.Common;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class GxHelper
    {

        //Writes msg in the GeneXus output
        public static bool WriteOutput(String msg)
        {
            IOutputService output = CommonServices.Output;
            output.AddLine(msg);
            return true;
        }

        //public static Constants.DataType GetInternalType(eDBType type)
        //{
        //    Constants.DataType internalType;
        //    switch (type)
        //    {
        //        case eDBType.Boolean:
        //            internalType = Constants.DataType.Boolean;
        //            break;
        //        case eDBType.CHARACTER:
        //            internalType = Constants.DataType.CHARACTER;
        //            break;
        //        case eDBType.DATE:
        //            internalType = Constants.DataType.DATE;
        //            break;
        //        case eDBType.DATETIME:
        //            internalType = Constants.DataType.DATETIME;
        //            break;
        //        case eDBType.GX_BUSCOMP:
        //            internalType = Constants.DataType.BC;
        //            break;
        //        case eDBType.GX_BUSCOMP_LEVEL:
        //            internalType = Constants.DataType.BC_LEVEL;
        //            break;
        //        case eDBType.GX_EXTERNAL_OBJECT:
        //            internalType = Constants.DataType.EXTERNAL_OBJECT;
        //            break;
        //        case eDBType.GX_SDT:
        //            internalType = Constants.DataType.SDT;
        //            break;
        //        case eDBType.INT:
        //            internalType = Constants.DataType.INT;
        //            break;
        //        case eDBType.LONGVARCHAR:
        //            internalType = Constants.DataType.LONGVARCHAR;
        //            break;
        //        case eDBType.NUMERIC:
        //            internalType = Constants.DataType.NUMERIC;
        //            break;
        //        case eDBType.VARCHAR:
        //            internalType = Constants.DataType.VARCHAR;
        //            break;
        //        default:
        //            internalType = Constants.DataType.NUMERIC;
        //            break;
        //    }
        //    return internalType;
        //}

        public static eDBType ConvertToGXType(Constants.GxuDataType type)
        {
            eDBType dbType;
            switch (type)
            {
                case Constants.GxuDataType.Boolean:
                    dbType = eDBType.Boolean;
                    break;
                case Constants.GxuDataType.CHARACTER:
                    dbType = eDBType.CHARACTER;
                    break;
                case Constants.GxuDataType.DATE:
                    dbType = eDBType.DATE;
                    break;
                case Constants.GxuDataType.DATETIME:
                    dbType = eDBType.DATETIME;
                    break;
                case Constants.GxuDataType.BC:
                    dbType = eDBType.GX_BUSCOMP;
                    break;
                case Constants.GxuDataType.BC_LEVEL:
                    dbType = eDBType.GX_BUSCOMP_LEVEL;
                    break;
                case Constants.GxuDataType.EXTERNAL_OBJECT:
                    dbType = eDBType.GX_EXTERNAL_OBJECT;
                    break;
                case Constants.GxuDataType.SDT:
                    dbType = eDBType.GX_SDT;
                    break;
                case Constants.GxuDataType.INT:
                    dbType = eDBType.INT;
                    break;
                case Constants.GxuDataType.LONGVARCHAR:
                    dbType = eDBType.LONGVARCHAR;
                    break;
                case Constants.GxuDataType.NUMERIC:
                    dbType = eDBType.NUMERIC;
                    break;
                case Constants.GxuDataType.VARCHAR:
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

        ////public static String defaultValue(Variable var)
        ////{
        ////    String def = "''";
        ////    if (var.Type == eDBType.Boolean)
        ////        def = "true";
        ////    else
        ////        if ((var.Type == eDBType.INT) || (var.Type == eDBType.NUMERIC))
        ////            def = "0";
        ////        else
        ////            if ((var.Type == eDBType.DATE) || (var.Type == eDBType.DATETIME))
        ////                def = "ymdtod(1,1,1)";
        ////    return def;
        ////}

        //public static bool isStandardVar(string var)
        //{
        //    return (var == "Line") || (var == "Output") || (var == "Page") || (var == "Pgmdesc") || (var == "Pgmname") || (var == "Time") || (var == "Today");
        //}

    }
}
