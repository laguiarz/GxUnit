using System.Windows.Forms;
using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
//using Artech.Genexus.Common;
//using Artech.Common.Properties;

//using Artech.Architecture.Common.Objects;
using Artech.Architecture.UI.Framework.Helper;
using Artech.Architecture.UI.Framework.Services;
//using Artech.Genexus.Common.Parts.SDT;
//using Artech.Genexus.Common.Types;
//using Artech.Genexus.Common.Objects;
using Artech.Common.Framework.Commands;

using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using PGGXUnit.Packages.GXUnit.GXUnitUI;
using PGGXUnit.Packages.GXUnit.Utils;

namespace PGGXUnit.Packages.GXUnit
{
	class CommandManager : CommandDelegator
	{
		
		public CommandManager()
		{
            // Elimina los objetos de GXUnit
            AddCommand(CommandKeys.EliminarGXUnit, new ExecHandler(ExecEliminarGXUnit));

            // Inicia GXUnit
            AddCommand(CommandKeys.IniciarGXUnit, new ExecHandler(ExecIniciarGXUnit));

            //Open GXUnit Toolwindow
            AddCommand(CommandKeys.GXUnitWindow, new ExecHandler(ExecGXUnitWindow));

            //About
            AddCommand(CommandKeys.About, new ExecHandler(ExecAbout));
		}

        // NOT HANDLED (CFBP)
		public bool TestAll(CommandData commandData)
		{
            if (!ContextHandler.GXUnitInitialized)
            {
                if (MessageBox.Show("Create GXUnit Objects?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    GXUnitInicializador i = GXUnitInicializador.GetInstance();
                    i.InicializarGXUnit();
                    ContextHandler.GXUnitInitialized = true;
                    Test();
                }
            }
            else
            {
                Test();
            }
			return true;
		}

        private void Test()
        {
            ManejadorRunner m = ManejadorRunner.GetInstance();
            TestCaseManager tc = TestCaseManager.GetInstance();
            m.CrearGenerarRunner(tc.ListarTestCases());
            m.EjecutarRunner();
        }

        public bool ExecEliminarGXUnit(CommandData commandData)
		{
            if (UIServices.KB.CurrentKB != null)
            {
                if (MessageBox.Show("Do you want to delete all GXUnit Objects?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Eliminar Runner
                    KBProcedureHandler mp = new KBProcedureHandler();
                    ManejadorRunner mr = ManejadorRunner.GetInstance();
                    mp.EliminarProcedimiento(mr.GetNombre());

                    //Eliminar Todos los TestCases
                    TestCaseManager mtc = TestCaseManager.GetInstance();
                    LinkedList<DTTestCase> testCases = mtc.ListarTestCases();
                    foreach (DTTestCase tc in testCases)
                    {
                        mtc.EliminarTestCase(tc);
                    }

                    //Eliminar Asserts
                    mp.EliminarProcedimiento("AssertStringEquals");
                    mp.EliminarProcedimiento("AssertNumericEquals");

                    //Eliminar Manejadores de sesi�n
                    mp.EliminarProcedimiento("GetGXUnitSession");
                    mp.EliminarProcedimiento("SetGXUnitSession");

                    //Eliminar RESTInvoker
                    mp.EliminarProcedimiento("RESTInvoker");

                    //Eliminar SDTs
                    KBSDTHandler msdt = new KBSDTHandler();
                    SDTipo sdt = new SDTipo("GXUnitSuite");
                    msdt.EliminarSDT(sdt);
                    sdt = new SDTipo("GXUnitTestCase");
                    msdt.EliminarSDT(sdt);
                    sdt = new SDTipo("GXUnitAssert");
                    msdt.EliminarSDT(sdt);

                    //Eliminar Carpetas
                    KBFolderHandler mf = new KBFolderHandler();
                    DTFolder folder = new DTFolder(Constantes.carpetaSuites, "");
                    mf.DeleteFolder(folder);

                    ContextHandler.GXUnitInitialized = false;
                }
                GXUnitMainWindow.getInstance().cargarNodosTest();
                return true;
            }
            else
            {
                MessageBox.Show("Open a knowledge base first.", "GXUnit");
                return false;
            }
        }

        public bool ExecIniciarGXUnit(CommandData commandData)
        {
            if (UIServices.KB.CurrentKB != null)
            {
                GXUnitInicializador i = GXUnitInicializador.GetInstance();
                i.InicializarGXUnit();
                ContextHandler.GXUnitInitialized = true;
                return true;
            }
            else
            {
                MessageBox.Show("Open a knowledge base first.", "GXUnit");
                return false;
            }
        }

        public bool ExecAbout(CommandData commandData)
        {
            AboutBox m = new AboutBox();
            m.ShowDialog();
            return true;
        }

        public bool ExecGXUnitWindow(CommandData commandData)
        {
            if (GXUnitPackage.GXUnitWindow == null)
                GXUnitPackage.GXUnitWindow = GXUnitMainWindow.getInstance();
            UIServices.ToolWindows.ShowToolWindow(GXUnitMainWindow.guid);
            UIServices.ToolWindows.FocusToolWindow(GXUnitMainWindow.guid);
            GXUnitPackage.GXUnitWindow.SetFocus();
            return true;
        }

        //// NOT HANDLED (CFBP)
        //public bool ExecAutoGenerate(CommandData commandData)
        //{
        //    SelectObjectOptions options = new SelectObjectOptions();
        //    options.MultipleSelection = false;
        //    options.ObjectTypes.Clear();
        //    options.ObjectTypes.Add(Artech.Architecture.Common.Descriptors.KBObjectDescriptor.Get("TestCase"));
        //    options.ObjectTypes.Add(Artech.Architecture.Common.Descriptors.KBObjectDescriptor.Get("Procedure"));
        //    KBObject kbObjectSelected = UIServices.SelectObjectDialog.SelectObject(options);

        //    if (kbObjectSelected.Name != "")
        //        TestCaseManager.GetInstance().ReGenerarTestCase(kbObjectSelected.Name);
        //    return true;
        //}

	}
}
