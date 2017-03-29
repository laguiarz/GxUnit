/*
2017-03-27  LAZ Removed all unreferenced code
 */

using Artech.Architecture.Common.Objects;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    public class ContextHandler
    {
        private static KBModel model = null;
        private static string kbName = "";
        private static bool gxUnitInitialized = false;
  
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

     
    }
}
