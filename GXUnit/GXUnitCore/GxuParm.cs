/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed GetParentName function to get/set properties
 *                  
 *                  Also this supported parameters of type BC but i removed that since it is not used anywyere yet... when we need it we can recover it
 *                  for now i'm trying to leave this the simplest i can so there's no need to understand/maintain all the code that there is..
 * */
 
 using Artech.Genexus.Common;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class GxuParm
    {
        private Variable variable;
        private String inoutType;
        private bool isKey;
        private bool isReadOnly;
        private String varName;
        private eDBType varType;
        private String complexTypeName;

        public GxuParm(String varName, String complexTypeName, String inoutType, Constants.Estructurado obj, bool isCollection)
        {
            //if (obj == Constants.Estructurado.BC)
            //    this.variable = KBTransactionHandler.GetInstance().GetBCVariable(varName, complexTypeName, isCollection);
            //else
                this.variable = (new GxuSDTHandler()).GetSDTVariable(varName, complexTypeName, isCollection);

            this.varName            = variable.Name;
            this.varType            = variable.Type;
            this.inoutType          = inoutType;
            this.complexTypeName    = complexTypeName;
        }

        //public GxuParm(String trnName, String attName, String inoutType, bool isKey, bool isReadOnly)
        //{
        //    variable = KBTransactionHandler.GetInstance().GetVariable(trnName, attName);
        //    varName = variable.Name;
        //    varType = variable.Type;
        //    this.inoutType = inoutType;
        //    this.isKey = isKey;
        //    this.isReadOnly = isReadOnly;
        //    complexTypeName = null;
        //}

        public GxuParm(Variable var, String inoutType)
        {
            this.variable = var;
            this.varName = variable.Name;
            this.varType = variable.Type;
            this.inoutType = inoutType;
        }

        public GxuParm(String name, eDBType type)
        {
            varName = name;
            varType = type;
        }

        public Variable Variable
        {
            get { return variable; }
            set { variable = value; }
        }

        public String InOutType
        {
            get { return inoutType; }
            set { inoutType = value; }
        }

        public bool IsKey
        {
            get { return isKey; }
            set { isKey = value; }
        }

        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set { isReadOnly = value; }

        }

        public String  VariableName
        {
            get { return varName;  }
            set { varName = value; }
        }

        public eDBType VariableType
        {
            get { return varType;  }
            set { varType = value; }
        }

        public String ComplexTypeName
        {

            get { return complexTypeName; }
            set { complexTypeName = value; }

        }


        public bool IsNumeric()
        {
            return GxHelper.isNumeric(varType);
        }

        public bool IsString()
        {
            return GxHelper.isString(varType);
        }

        public bool IsBoolean()
        {
            return GxHelper.isBoolean(varType);
        }

        public bool IsDate()
        {
            return GxHelper.isDate(varType);
        }

        public bool IsSimple()
        {
            return GxHelper.isSimple(varType);
        }

        public bool IsSDT()
        {
            return varType == eDBType.GX_SDT;
        }

        public String DefaultValue()
        {
            String def = "''";
            if (this.IsBoolean())
                def = "false";
            else
                if (this.IsNumeric())
                    def = "0";
                else
                    if (this.IsDate())
                        def = "ymdtod(1,1,1)";
            return def;
        }

    }
}
