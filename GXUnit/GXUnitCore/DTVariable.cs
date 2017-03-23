using System;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class DTVariable
    {

        private String nombre;
        private Constants.Tipo tipo;
        private String nombretipocompuesto = null;
        private int longitud;
        private int decimales;
        private bool isCollection;

        public DTVariable()
        {
        }

        public DTVariable(String nombre, Constants.Tipo tipo)
        {
            this.nombre = nombre;
            this.tipo = tipo;
        }


        public DTVariable(String nombre, Constants.Tipo tipo, int longitud, int decimales)
        {
            this.nombre     = nombre;
            this.tipo       = tipo;
            this.longitud   = longitud;
            this.decimales  = decimales;
            
        }

        public DTVariable(String nombre, Constants.Tipo tipo, String nombretipo)
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

        public DTVariable(String nombre, String nombretipo, bool isCollection)
        {
            this.nombre = nombre;
            this.nombretipocompuesto = nombretipo;
            this.isCollection = isCollection;

        }

        public DTVariable(String nombre, Constants.Tipo tipo, String nombretipo, int longitud, int decimales)
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

        public Constants.Tipo GetTipo()
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

        public bool GetIsCollection()
        {
            return this.isCollection;
        }
    }
}
