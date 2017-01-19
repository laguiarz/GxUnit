using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class SDTipoNivelItem
    {
        private String nombre;
        private Constantes.Tipo tipo;
        private String tipocompuesto;
        private int longitud;

        public SDTipoNivelItem()
        { 
        }

        public SDTipoNivelItem(String nombre, Constantes.Tipo tipo, int longitud)
        {
            this.nombre     = nombre;
            this.tipo       = tipo;
            this.longitud   = longitud;
        }

        public SDTipoNivelItem(String nombre, Constantes.Tipo tipo, String tipocompuesto)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.tipocompuesto = tipocompuesto;
        }

        public String GetNombre()
        {
            return this.nombre;
        }

        public Constantes.Tipo GetTipo()
        {
            return this.tipo;
        }

        public int GetLongitud()
        {
            return this.longitud;
        }

        public String GetTipoCompuesto()
        {
            return this.tipocompuesto;
        }

    }
}
