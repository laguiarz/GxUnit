using Artech.Architecture.Common.Objects;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class ContextHandler
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

        private static bool execute = false;
        public static bool Execute
        {
            get
            {
                return execute;
            }
            set
            {
                execute = value;
            }
        }

        private static bool forceExecuteRunner = false;
        public static bool ForceExecuteRunner
        {
            get
            {
                return forceExecuteRunner;
            }
            set
            {
                forceExecuteRunner = value;
            }
        }

        private static bool gxUnitInitialized = false;
        public static bool GXUnitInitialized
        {
            get
            {
                return gxUnitInitialized;
            }
            set
            {
                gxUnitInitialized = value;
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
