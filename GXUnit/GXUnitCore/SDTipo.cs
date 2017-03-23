using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class SDTipo
    {

        String nombre;
        String padre;

        GxuSDTLevel root;
        LinkedList<GxuSDTLevel> niveles;
        private LinkedList<DTPropiedad> propiedades;

        public SDTipo(String nombre)
        {
            this.nombre = nombre;
        }

        public SDTipo(String nombre, String padre, LinkedList<GxuSDTLevel> niveles, GxuSDTLevel root, LinkedList<DTPropiedad> prop)
        {
            this.nombre     = nombre;
            this.padre      = padre;
            this.niveles    = niveles;
            this.root       = root;
            this.propiedades = prop;
        }

        public String GetNombre()
        {
            return this.nombre;
        }

        public String GetPadre()
        {
            return this.padre;
        }

        public LinkedList<GxuSDTLevel> GetNiveles()
        {
            if (this.niveles == null)
                return this.root.GetSubLevels();
             else
            return this.niveles;
        }


        public GxuSDTLevel GetRoot()
        {
            return this.root;
        }

        public LinkedList<DTPropiedad> GetPropiedades()
        {
            return this.propiedades;
        }

    }
}
