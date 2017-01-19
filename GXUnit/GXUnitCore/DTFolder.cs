using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class DTFolder : DTObjeto
    {
        private String NombrePadre;

        public DTFolder(String nom, String nomPadre)
            : base(nom)
        {
            NombrePadre = nomPadre;
        }

        public String GetNombrePadre()
        {
            return NombrePadre;
        }
    }
}
