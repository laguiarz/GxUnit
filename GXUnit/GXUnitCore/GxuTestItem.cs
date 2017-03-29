/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 *                  Commented out unreferenced code
 * */

using System;
using System.Collections.Generic;

namespace PGGXUnit.Packages.GXUnit.GXUnitCore
{
    public class GxuTestItem : GxuObject
    {
        private String folder;
        private bool isSuite = false;

        public GxuTestItem(String name)
            : base(name)
        {
        }

        public GxuTestItem(String name, String folder, bool suite)
            : base(name)
        {
            if (folder != null)
                this.folder = folder;
            else this.folder = Constants.SUITES_FOLDER;

            isSuite = suite;
        }

        public bool IsSuite
        {
            get { return isSuite; }
            set { isSuite = value; }
        }

        public String Folder
        {
            get { return folder; }
            set { folder = value; }

        }

        //        private String Source;
        //          private LinkedList<DTVariable> Variables;
        //       private Type Tipo;
        //       private String ObjectToTest;

        //public GxuTestCase(String nombre, String source, String folder, LinkedList<DTVariable> variables, Type tipo, String oToTest)
        //    : base(nombre)
        //{
        //    Source = source;
        //    Folder = folder;
        //    Variables = variables;
        //    Tipo = tipo;
        //    ObjectToTest = oToTest;

        //}
        //public String GetSource()
        //{
        //    return Source;
        //}
        //public LinkedList<DTVariable> GetVariables()
        //{
        //    return Variables;
        //}

        ////public Type GetTipo()
        ////{
        ////    return Tipo;
        ////}

        //public String GetObjectToTest()
        //{
        //    return ObjectToTest;
        //}

        //public void SetSource(String source)
        //{
        //    Source = source;
        //}

        ////    public void SetFolder(String folder)
        ////    {
        ////        Folder = folder;
        ////    }

        ////    public void SetVariables(LinkedList<DTVariable> vars)
        ////    {
        ////        Variables = vars;
        ////    }

        ////    public void SetTipo(Type tipo)
        ////    {
        ////        Tipo = tipo;
        ////    }

        ////    public void SetObjectToTest(String oToT)
        ////    {
        ////        ObjectToTest = oToT;
        ////    }
        ////}
    }
}
