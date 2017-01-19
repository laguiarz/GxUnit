using Artech.Architecture.Common.Objects;
using Artech.Architecture.UI.Framework.Services;
using Artech.Common.Properties;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Entities;
using Artech.Genexus.Common.Services;
using Artech.Udm.Framework;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class KBManager
    {

        public static GxModel getTargetModel()
        {
            IKBService kbserv = UIServices.KB;
            return getTargetModel(kbserv.CurrentKB).GetAs<GxModel>();
        }

        public static GxModel getModel()
        {
            IKBService kbserv = UIServices.KB;
            return getModel(kbserv.CurrentKB).GetAs<GxModel>();
        }

        public static KBModel getModel(KnowledgeBase kb)
        {
            // Esto depende de la version de la BL de GeneXus.
#if GXEV1
            // Para Ev1:
            return kb.WorkingEnvironment.DesignModel;
#else
            // Para Ev2 y Tilo:
            return kb.DesignModel;
#endif
        }

        public static KBModel getTargetModel(KnowledgeBase kb)
        {
            // Esto depende de la version de la BL de GeneXus.
#if GXEV1
            // Para Ev1:
            return kb.WorkingEnvironment.TargetModel;
#else
            // Para Ev2 y Tilo:
            return kb.DesignModel.Environment.TargetModel;
#endif
        }

        //Obtiene la url para ejecutar el objeto "nombre", segun lenguaje que se este generando
        public static String GetUrlEjecutar(String nombre)
        {
            GxModel gxModel = getModel();

            if (gxModel != null)
            {
                switch ((GeneratorType)gxModel.Environment.Generator)
                {
                    case GeneratorType.JavaWeb:
                        return PropertyAccessor.GetValueString(gxModel.Environment, Properties.JAVA.WebRoot) + "a" + nombre;
                    case GeneratorType.CSharpWeb:
                        return PropertyAccessor.GetValueString(gxModel.Environment, Properties.CSHARP.WebRoot) + "a" + nombre + ".aspx";
                    case GeneratorType.RubyWeb:
                        return PropertyAccessor.GetValueString(gxModel.Environment, Properties.RUBY.WebRoot) + nombre.ToLower() + ".rb";
                    default:
                        return null;

                }

            }
            return null;
        }

        public static void GXBuildObject(EntityKey objKey)
        {
#if GXTILO
            // Para Tilo:
            GenexusUIServices.Build.Build(objKey);
#else
            // Para Ev1 y Ev2:
           GenexusUIServices.Build.Build(objKey, true);
#endif
        }

        public static void GXRebuildObject(EntityKey objKey)
        {
#if GXTILO
            // Para Tilo:
            GenexusUIServices.Build.Rebuild(objKey);
#else
            // Para Ev1 y Ev2:
           GenexusUIServices.Build.Rebuild(objKey, true);
#endif
        }

        public static void GXRunObject(EntityKey objKey)
        {
#if GXTILO
            // Para Tilo:
            GenexusUIServices.Build.Run(objKey);
#else
            // Para Ev1 y Ev2:
           GenexusUIServices.Build.Run(objKey, true);
#endif
        }

        public static string getTargetPath()
        {
            GxModel modelo = KBManager.getTargetModel();

            string targetPath;

            if ((bool)modelo.Environment.Properties.GetPropertyValue("IS_WEB_GEN") == true)
            {
                targetPath = modelo.WebTargetFullPath;
            }
            else
            {
                targetPath = modelo.TargetFullPath;
            }

            return targetPath;
        }
    }
}
