/*
 * 2017-03-28   LAZ Added SeeResults command 
 * 
 */

using Artech.Common.Framework.Commands;

namespace PGGXUnit.Packages.GXUnit
{
	public class CommandKeys
	{
        //Strings here match GeneXusPackage.Package definition:
        private static CommandKey removeGXUnitObjects   = new CommandKey(GXUnitPackage.guid, "EliminarGXUnit");
        private static CommandKey iniciarGXUnit         = new CommandKey(GXUnitPackage.guid, "IniciarGXUnit");
        private static CommandKey gxunitWindow          = new CommandKey(GXUnitPackage.guid, "GXUnitWindow");
        private static CommandKey gxunitResultsWindow   = new CommandKey(GXUnitPackage.guid, "seeResults");
        private static CommandKey about                 = new CommandKey(GXUnitPackage.guid, "About");

        public static CommandKey RemoveGXUnitObjects
        {
            get { return removeGXUnitObjects; }
        }

        public static CommandKey CreateGXUnitObjects
        {
            get { return iniciarGXUnit; }
        }

        public static CommandKey GXUnitWindow
        {
            get { return gxunitWindow; }
        }

        public static CommandKey GxUnitResultsWindow
        {
            get { return gxunitResultsWindow; }
        }
        public static CommandKey About
        {
            get { return about; }
        }
	}
}