using Artech.Architecture.Common.Objects;
using Artech.Genexus.Common.Objects;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    class KBObjectHandler
    {
        private KBModel model;
        private static KBObjectHandler instance = new KBObjectHandler();

        private KBObjectHandler()
        {
        }

        public static KBObjectHandler GetInstance()
        {
            return instance;
        }

        public KBModel GetModel()
        {
            return model;
        }

        public void GetModel(KBModel m)
        {
            model = m;
        }

        public DTObjeto GetDTObjeto(string name)
        {
            KBObject objeto = GetKBObjectTest(name);
            if (objeto is Procedure)
            {
                KBProcedureHandler mp = new KBProcedureHandler();
                return mp.GetProcedimiento(name);
            }
            else if (objeto is Transaction) {
                    return KBTransactionHandler.GetInstance().GetDTTransaccion(name);
                }
            else if (objeto is DataProvider) {
                        return KBDataProviderHandler.GetInstance().GetDTDataProvider(name);
                    }
            return null;
        }

        public KBObject GetKBObject(string name)
        {
            foreach (KBObject obj in ContextHandler.Model.Objects.GetAll())
            {
                if (obj.Name.ToLower() == name.ToLower())
                    return obj;
            }
            return null;
        }

        public KBObject GetKBObjectTest(string name)
        {
            foreach (KBObject obj in ContextHandler.Model.Objects.GetAll())
            {
                if (EsObjetoTesteable(obj) && (obj.Name.ToLower() == name.ToLower()))
                    return obj;
            }
            return null;
        }

        private bool EsObjetoTesteable(KBObject obj)
        {
            return (obj is Procedure) || (obj is Transaction) || (obj is DataProvider);
        }

        public LinkedList<KBParameterHandler> GetAtt(String objeto, Constantes.Estructurado Struct)
        {
            KBSDTHandler msdt;
            if (Struct == Constantes.Estructurado.BC)
                return KBTransactionHandler.GetInstance().GetAtt(objeto);
            else
                msdt = new KBSDTHandler();
                return msdt.GetAtt(objeto);
        }
    }

}
