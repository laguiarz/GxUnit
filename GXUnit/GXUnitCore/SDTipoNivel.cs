using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class SDTipoNivel
    {
        private String nombre;
        private LinkedList<SDTipoNivelItem> items;
        private bool escoleccion;
        private LinkedList<SDTipoNivel> niveles;

        public SDTipoNivel()
        { 
        }

        public SDTipoNivel( String nombre,LinkedList<SDTipoNivelItem> items, bool coleccion, LinkedList<SDTipoNivel> niveles)
        {
            this.nombre         = nombre;
            this.items          = items;
            this.escoleccion    = coleccion;
            this.niveles        = niveles;
        }

        public String GetNombre()
        {
            return this.nombre;
        }

        public LinkedList<SDTipoNivelItem> GetItems()
        {
            return this.items;
        }

        public LinkedList<SDTipoNivel> GetNiveles()
        {
            return this.niveles;
        }

        public bool EsColeccion()
        {
            return this.escoleccion;
        }

    }
}
