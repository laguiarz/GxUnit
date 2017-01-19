﻿using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class DTDataProvider : DTObjeto
    {
        private LinkedList<KBParameterHandler> variables;
        private String output;
        private Constantes.Estructurado tipoOutput;
        private bool isCollOutput;
        private String collectionName;
        private bool isCollSDT;
        private String SDTItem;

        public DTDataProvider(String nombre, String output, Constantes.Estructurado tipoOutput, bool isCollOutput, String collectionName, bool isCollSDT, String SDTItem, LinkedList<KBParameterHandler> variables)
            : base (nombre)
        {
            LinkedList<KBParameterHandler> variablesAux = variables;
            if (isCollOutput)
                variablesAux.AddLast(new KBParameterHandler(output, output, Constantes.PARM_OUT, tipoOutput, false, false));
            if (isCollSDT)
                variables.AddLast(new KBParameterHandler(output + Constantes.ITEM, SDTItem, Constantes.PARM_OUT, Constantes.Estructurado.SDT, false, false));
            variablesAux.AddLast(new KBParameterHandler(output + Constantes.RESULTADO, output, Constantes.PARM_OUT, tipoOutput, isCollOutput, true));
            variablesAux.AddLast(new KBParameterHandler(output + Constantes.VALOR_ESPERADO, output, Constantes.PARM_OUT, tipoOutput, isCollOutput, false));
            KBDataProviderHandler mdp = KBDataProviderHandler.GetInstance();
            DTVariable var = new DTVariable(Constantes.RESULTADO, Constantes.Tipo.VARCHAR, 999, 0);
            variablesAux.AddLast(new KBParameterHandler(mdp.GetVariable(nombre, var), Constantes.PARM_OUT, false));
            DTVariable var2 = new DTVariable(Constantes.VALOR_ESPERADO, Constantes.Tipo.VARCHAR, 999, 0);
            variablesAux.AddLast(new KBParameterHandler(mdp.GetVariable(nombre, var2), Constantes.PARM_OUT, false));
            this.variables = variablesAux;
            this.output = output;
            this.tipoOutput = tipoOutput;
            this.isCollOutput = isCollOutput;
            this.collectionName = collectionName;
            this.isCollSDT = isCollSDT;
            this.SDTItem = SDTItem;
        }

        public void AddVariable(String varName, String structName, Constantes.Estructurado Struct, bool isCollection)
        {
            variables.AddLast(new KBParameterHandler(varName, structName, Constantes.PARM_OUT, Struct, isCollection, false));
        }

        public LinkedList<KBParameterHandler> GetParametros()
        {
            return variables;
        }

        public String GetOutput()
        {
            return output;
        }

        public Constantes.Estructurado GetTipoOutput()
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
