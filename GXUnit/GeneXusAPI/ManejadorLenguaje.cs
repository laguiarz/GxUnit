using Artech.Common.Properties;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Entities;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class ManejadorLenguaje
    {
        private static GeneratorType lenguaje;
        public static GeneratorType Lenguaje
        {
            get
            {
                return lenguaje;
            }
            private set { }
        }
        
        public static void SetLenguajeModelo()
        {
            GxModel gxModel = KBManager.getModel();
    
            if (gxModel != null)
            {
                lenguaje = (GeneratorType)gxModel.Environment.Generator;   
                //FuncionesAuxiliares.EscribirOutput("Generando " + Lenguaje.ToString());
            }
        }

        public static String GetWebRoot()
        {
            GxModel gxModel = KBManager.getModel();
            if (gxModel != null)
            {
                switch ((GeneratorType)gxModel.Environment.Generator)
                {
                    case GeneratorType.JavaWeb:
                        return PropertyAccessor.GetValueString(gxModel.Environment, Properties.JAVA.WebRoot);
                    case GeneratorType.CSharpWeb:
                        return PropertyAccessor.GetValueString(gxModel.Environment, Properties.CSHARP.WebRoot);
                    case GeneratorType.RubyWeb:
                        return PropertyAccessor.GetValueString(gxModel.Environment, Properties.RUBY.WebRoot);
                    default:
                        return null;                     
                }
            } 
            return null;
        }

    }
}
