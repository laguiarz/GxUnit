using Artech.Architecture.Common.Objects;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class ManejadorContexto
    {
        private static KBModel model = null;
        public static KBModel Model
        {
            get
            {
                return model;
            }
            set
            {
                model = value;
            }
        }

        private static string kbName = "";
        public static string KBName
        {
            get
            {
                return kbName;
            }
            set
            {
                kbName = value;
            }
        }

        private static string lastXMLName = "";
        public static string LastXMLName
        {
            get
            {
                return lastXMLName;
            }
            set
            {
                lastXMLName = value;
            }
        }

        private static string objectToTest = "";
        public static string ObjectToTest
        {
            get
            {
                return objectToTest;
            }
            set
            {
                objectToTest = value;
            }
        }

        private static bool ejecutar = false;
        public static bool Ejecutar
        {
            get
            {
                return ejecutar;
            }
            set
            {
                ejecutar = value;
            }
        }

        private static bool forceEjecutarRunner = false;
        public static bool ForceEjecutarRunner
        {
            get
            {
                return forceEjecutarRunner;
            }
            set
            {
                forceEjecutarRunner = value;
            }
        }

        private static bool gxUnitInicializado = false;
        public static bool GXUnitInicializado
        {
            get
            {
                return gxUnitInicializado;
            }
            set
            {
                gxUnitInicializado = value;
            }
        }

        private static string message = "";
        public static string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
            }
        }
    }
}
