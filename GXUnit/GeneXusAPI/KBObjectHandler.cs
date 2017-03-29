using Artech.Architecture.Common.Objects;
using Artech.Genexus.Common.Objects;

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

        public KBModel Model
        {
            set {
                model = value;
            }
            get {
                return model;
            }
        }

        ////public DTObjeto GetDTObjeto(string name)
        ////{
        ////    KBObject objeto = GetKBObjectTest(name);
        ////    if (objeto is Procedure)
        ////    {
        ////        KBProcedureHandler mp = new KBProcedureHandler();
        ////        return mp.GetProcedimiento(name);
        ////    }
        ////    else if (objeto is Transaction) {
        ////            return KBTransactionHandler.GetInstance().GetDTTransaccion(name);
        ////        }
        ////    //else if (objeto is DataProvider) {
        ////    //            return KBDataProviderHandler.GetInstance().GetDTDataProvider(name);
        ////    //        }
        ////    return null;
        ////}

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
                if (ObjectIsTesteable(obj) && (obj.Name.ToLower() == name.ToLower()))
                    return obj;
            }
            return null;
        }

        private bool ObjectIsTesteable(KBObject obj)
        {
            return (obj is Procedure) || (obj is Transaction) || (obj is DataProvider);
        }

        //public LinkedList<KBParameterHandler> GetAtt(String objeto, Constants.Estructurado Struct)
        //{
        //    KBSDTHandler msdt;
        //    if (Struct == Constants.Estructurado.BC)
        //        return KBTransactionHandler.GetInstance().GetAtt(objeto);
        //    else
        //        msdt = new KBSDTHandler();
        //        return msdt.GetAtt(objeto);
        //}
    }

}
