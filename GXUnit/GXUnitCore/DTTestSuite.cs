using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    class DTTestSuite : DTObjeto
    {
        private String Source;
        private String Folder;
        private LinkedList<DTVariable> Variables;
        private Type Tipo;
        //        private String ObjectToTest;

        //        /// <summary>
        //        /// Constructor de la clase TesteableObject
        //        /// </summary>
        //        /// <param name="tipo">Tipo del objeto Gx testable</param>
        //        /// <param name="nombre">Nombre del objeto Gx testable</param>
        //        /// <param name="source">Source del objeto Gx testable</param>
        //        /// <param name="folder">Folder padre del objeto Gx testable</param>
        //        /// <param name="variables">Variables del objeto Gx testable</param>

        public DTTestSuite (String nombre, String source, String folder, LinkedList<DTVariable> variables, Type tipo) : base(nombre)
        {
            Source = source;
            Folder = folder;
            Variables = variables;
            Tipo = tipo;
        }

        public DTTestSuite(String nombre)
            : base(nombre)
        {
        }

        public String GetSource()
        {
            return Source;
        }

        public String GetFolder()
        {
            return Folder;
        }

        public LinkedList<DTVariable> GetVariables()
        {
            return Variables;
        }

        public Type GetTipo()
        {
            return Tipo;
        }

        public void SetSource(String source)
        {
            Source = source;
        }

        public void SetFolder(String folder)
        {
            Folder = folder;
        }

        public void SetVariables(LinkedList<DTVariable> vars)
        {
            Variables = vars;
        }

        public void SetTipo(Type tipo)
        {
            Tipo = tipo;
        }
    }
}
