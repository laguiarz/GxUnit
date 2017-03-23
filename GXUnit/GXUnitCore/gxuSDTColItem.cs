using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class gxuSDTColItem
    {
        private String nombre;
        private Constants.Tipo tipo;
        private String tipocompuesto;
        private int longitud;

        public gxuSDTColItem()
        { 
        }

        public gxuSDTColItem(String nombre, Constants.Tipo tipo, int longitud)
        {
            this.nombre     = nombre;
            this.tipo       = tipo;
            this.longitud   = longitud;
        }

        public gxuSDTColItem(String nombre, Constants.Tipo tipo, String tipocompuesto)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.tipocompuesto = tipocompuesto;
        }

        public String GetNombre()
        {
            return this.nombre;
        }

        public Constants.Tipo GetTipo()
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
