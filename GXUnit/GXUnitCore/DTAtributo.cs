using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class DTAtributo
    {
        private String nombre;
        private Constantes.Tipo tipo;
        private bool esClave;
        private bool esSoloLectura;

        public DTAtributo(String nombre, Constantes.Tipo tipo, bool esClave, bool esSoloLectura)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.esClave = esClave;
            this.esSoloLectura = esSoloLectura;
        }

        public String GetNombre()
        {
            return this.nombre;
        }

        public Constantes.Tipo GetTipo()
        {
            return this.tipo;
        }

        public bool GetEsClave()
        {
            return this.esClave;
        }

        public bool GetEsSoloLectura()
        {
            return this.esSoloLectura;
        }
    }
}
