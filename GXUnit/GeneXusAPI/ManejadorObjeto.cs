using Artech.Architecture.Common.Objects;
using Artech.Genexus.Common.Objects;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    class ManejadorObjeto
    {
        private KBModel model;
        private static ManejadorObjeto instance = new ManejadorObjeto();

        private ManejadorObjeto()
        {
        }

        public static ManejadorObjeto GetInstance()
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
                ManejadorProcedimiento mp = new ManejadorProcedimiento();
                return mp.GetProcedimiento(name);
            }
            else if (objeto is Transaction) {
                    return ManejadorTransaccion.GetInstance().GetDTTransaccion(name);
                }
            else if (objeto is DataProvider) {
                        return ManejadorDataProvider.GetInstance().GetDTDataProvider(name);
                    }
            return null;
        }

        public KBObject GetKBObject(string name)
        {
            foreach (KBObject obj in ManejadorContexto.Model.Objects.GetAll())
            {
                if (obj.Name.ToLower() == name.ToLower())
                    return obj;
            }
            return null;
        }

        public KBObject GetKBObjectTest(string name)
        {
            foreach (KBObject obj in ManejadorContexto.Model.Objects.GetAll())
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

        public LinkedList<Parametro> GetAtt(String objeto, Constantes.Estructurado Struct)
        {
            ManejadorSDT msdt;
            if (Struct == Constantes.Estructurado.BC)
                return ManejadorTransaccion.GetInstance().GetAtt(objeto);
            else
                msdt = new ManejadorSDT();
                return msdt.GetAtt(objeto);
        }
    }

}
