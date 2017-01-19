using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class DTPropiedad
    {
        private String nombre;
        private object valor;

        public DTPropiedad()
        { 
        }

        public DTPropiedad(String nombre, Object valor)
        {
            this.nombre = nombre;
            this.valor = valor;
        }

        public String GetNombre()
        {
            return this.nombre;
        }

        public Object GetValor()
        {
            return this.valor;
        }
    }
}
