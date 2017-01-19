using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public abstract class DTObjeto
    {
        private String nombre;

        public DTObjeto(String nombre)
        {
            this.nombre = nombre;
        }

        public String GetNombre()
        {
            return this.nombre;
        }
    }
}
