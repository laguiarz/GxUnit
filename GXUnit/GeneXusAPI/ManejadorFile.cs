using Artech.Architecture.Common.Objects;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    class ManejadorFile
    {
        private KBModel model;

        public ManejadorFile()
        {
            this.model = ManejadorContexto.Model;
        }

        public KBModel GetModel()
        {
            return model;
        }

    }
}
