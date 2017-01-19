using Artech.Common.Framework.Commands;

namespace PGGXUnit.Packages.GXUnit
{
	public class CommandKeys
	{
        private static CommandKey eliminarGXUnit = new CommandKey(GXUnitPackage.guid, "EliminarGXUnit");
        private static CommandKey iniciarGXUnit = new CommandKey(GXUnitPackage.guid, "IniciarGXUnit");
        private static CommandKey gxunitWindow = new CommandKey(GXUnitPackage.guid, "GXUnitWindow");
        private static CommandKey about = new CommandKey(GXUnitPackage.guid, "About");

        public static CommandKey EliminarGXUnit { get { return eliminarGXUnit; } }
        public static CommandKey IniciarGXUnit { get { return iniciarGXUnit; } }
        public static CommandKey GXUnitWindow { get { return gxunitWindow; } }
        public static CommandKey About { get { return about; } }
	}
}