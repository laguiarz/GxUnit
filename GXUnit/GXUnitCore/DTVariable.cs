using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class DTVariable
    {

        private String nombre;
        private Constantes.Tipo tipo;
        private String nombretipocompuesto = null;
        private int longitud;
        private int decimales;


        public DTVariable()
        {
        }

        public DTVariable(String nombre, Constantes.Tipo tipo)
        {
            this.nombre = nombre;
            this.tipo = tipo;
        }


        public DTVariable(String nombre, Constantes.Tipo tipo, int longitud, int decimales)
        {
            this.nombre     = nombre;
            this.tipo       = tipo;
            this.longitud   = longitud;
            this.decimales  = decimales;
            
        }

        public DTVariable(String nombre, Constantes.Tipo tipo, String nombretipo)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.nombretipocompuesto = nombretipo;

        }

        public DTVariable(String nombre, String nombretipo)
        {
            this.nombre = nombre;
            this.nombretipocompuesto = nombretipo;

        }

        public DTVariable(String nombre, Constantes.Tipo tipo, String nombretipo, int longitud, int decimales)
        {
            this.nombre = nombre;
            this.tipo = tipo;
            this.nombretipocompuesto = nombretipo;
            this.longitud = longitud;
            this.decimales = decimales;

        }

        public String GetNombre()
        {
            return this.nombre;
        }

        public Constantes.Tipo GetTipo()
        {
            return this.tipo;
        }

        public String  GetNombreTipoCompuesto()
        {
            return this.nombretipocompuesto;
        }

        public int GetLongitud()
        {
            return this.longitud;
        }

        public int GetDecimales()
        {
            return this.decimales;
        }
    }
}
