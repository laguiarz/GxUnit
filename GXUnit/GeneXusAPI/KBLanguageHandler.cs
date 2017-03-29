using Artech.Common.Properties;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Entities;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class KBLanguageHandler
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
        
        public static void SetModelLanguage()
        {
            //We want the generator for the model we are generating right now
            GxModel gxModel = KBManager.GetTargetModel();

            if (gxModel != null)
            {
                lenguaje = (GeneratorType)gxModel.Environment.Generator;   
            }
        }

        public static String GetWebRoot()
        {
            GxModel gxModel = KBManager.GetTargetModel();
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
