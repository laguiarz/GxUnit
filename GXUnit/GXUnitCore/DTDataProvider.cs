using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class DTDataProvider : DTObjeto
    {
        private LinkedList<KBParameterHandler> variables;
        private String output;
        private Constants.Estructurado tipoOutput;
        private bool isCollOutput;
        private String collectionName;
        private bool isCollSDT;
        private String SDTItem;

        public DTDataProvider(String nombre, String output, Constants.Estructurado tipoOutput, bool isCollOutput, String collectionName, bool isCollSDT, String SDTItem, LinkedList<KBParameterHandler> variables)
            : base (nombre)
        {
            LinkedList<KBParameterHandler> variablesAux = variables;
            if (isCollOutput)
                variablesAux.AddLast(new KBParameterHandler(output, output, Constants.PARM_OUT, tipoOutput, false, false));
            if (isCollSDT)
                variables.AddLast(new KBParameterHandler(output + Constants.ITEM, SDTItem, Constants.PARM_OUT, Constants.Estructurado.SDT, false, false));
            variablesAux.AddLast(new KBParameterHandler(output + Constants.RESULTADO, output, Constants.PARM_OUT, tipoOutput, isCollOutput, true));
            variablesAux.AddLast(new KBParameterHandler(output + Constants.VALOR_ESPERADO, output, Constants.PARM_OUT, tipoOutput, isCollOutput, false));
            KBDataProviderHandler mdp = KBDataProviderHandler.GetInstance();
            DTVariable var = new DTVariable(Constants.RESULTADO, Constants.Tipo.VARCHAR, 999, 0);
            variablesAux.AddLast(new KBParameterHandler(mdp.GetVariable(nombre, var), Constants.PARM_OUT, false));
            DTVariable var2 = new DTVariable(Constants.VALOR_ESPERADO, Constants.Tipo.VARCHAR, 999, 0);
            variablesAux.AddLast(new KBParameterHandler(mdp.GetVariable(nombre, var2), Constants.PARM_OUT, false));
            this.variables = variablesAux;
            this.output = output;
            this.tipoOutput = tipoOutput;
            this.isCollOutput = isCollOutput;
            this.collectionName = collectionName;
            this.isCollSDT = isCollSDT;
            this.SDTItem = SDTItem;
        }

        public void AddVariable(String varName, String structName, Constants.Estructurado Struct, bool isCollection)
        {
            variables.AddLast(new KBParameterHandler(varName, structName, Constants.PARM_OUT, Struct, isCollection, false));
        }

        public LinkedList<KBParameterHandler> GetParametros()
        {
            return variables;
        }

        public String GetOutput()
        {
            return output;
        }

        public Constants.Estructurado GetTipoOutput()
        {
            return tipoOutput;
        }

        public bool GetIsCollOutput()
        {
            return isCollOutput;
        }

        public String GetCollectionName()
        {
            return collectionName;
        }

        public bool GetIsCollSDT()
        {
            return isCollSDT;
        }

        public String GetSDTItem()
        {
            return SDTItem;
        }
    }
}
