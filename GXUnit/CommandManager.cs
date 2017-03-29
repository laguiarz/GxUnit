using System.Windows.Forms;
using Artech.Architecture.UI.Framework.Helper;
using Artech.Architecture.UI.Framework.Services;
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
            AddCommand(CommandKeys.RemoveGXUnitObjects, new ExecHandler(RunCommand_DeleteGXUnitObjects));
            AddCommand(CommandKeys.CreateGXUnitObjects, new ExecHandler(RunCommand_CreateGXUnitObjects));
            AddCommand(CommandKeys.GXUnitWindow, new ExecHandler(RunCommand_GXUnitWindow));
            AddCommand(CommandKeys.GxUnitResultsWindow, new ExecHandler(RunCommand_GXUnitResultsWindow));
            AddCommand(CommandKeys.About, new ExecHandler(RunCommand_About));
		}

        public bool RunCommand_DeleteGXUnitObjects(CommandData commandData)
		{
            if (UIServices.KB.CurrentKB != null)
            {
                if (MessageBox.Show("Do you want to delete all GXUnit Objects (You might need to delete all your test cases for this to work)?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    //Eliminar Runner
                    GxuProcedureHandler mp = new GxuProcedureHandler();

                    ////Eliminar Todos los TestCases
                    //TestCaseManager mtc = TestCaseManager.GetInstance();
                    //LinkedList<DTTestCase> testCases = mtc.ListarTestCases();
                    //foreach (DTTestCase tc in testCases)
                    //{
                    //    mtc.EliminarTestCase(tc);
                    //}

                    //Delete Tools
                    mp.DeleteProcedure("GXUnit_RunTests");
                    mp.DeleteProcedure("GXUnit_LoadTests");
                    mp.DeleteProcedure("GXUnit_GetCurrentMillisecs");
                    mp.DeleteProcedure("GXUnit_GetElapsedMilliseconds");

                    //Delete Asserts
                    mp.DeleteProcedure("AssertStringEquals");
                    mp.DeleteProcedure("AssertNumericEquals");

                    //Delete Session Handling
                    mp.DeleteProcedure("GetGXUnitSession");
                    mp.DeleteProcedure("SetGXUnitSession");

                    //Delete RESTInvoker
                    mp.DeleteProcedure("RESTInvoker");

                    //Delete SDTs
                    GxuSDTHandler msdt = new GxuSDTHandler();
                    GxuSDT sdt = new GxuSDT("GXUnitSuite");
                    msdt.DeleteSDT(sdt);
                    sdt = new GxuSDT("GXUnitTestCase");
                    msdt.DeleteSDT(sdt);
                    sdt = new GxuSDT("GXUnitAssert");
                    msdt.DeleteSDT(sdt);

                    //Delete Folders
                    GxuFolderHandler mf = new GxuFolderHandler();
                    GxuFolder folder = new GxuFolder(Constants.SUITES_FOLDER, "");
                    mf.DeleteFolder(folder);

                    ContextHandler.GXUnitInitialized = false;
                }
                GXUnitMainWindow.getInstance().LoadTestTrees();
                return true;
            }
            else
            {
                MessageBox.Show("Open a knowledge base first.", "GXUnit");
                return false;
            }
        }

        public bool RunCommand_CreateGXUnitObjects(CommandData commandData)
        {
            if (UIServices.KB.CurrentKB != null)
            {
                GxuInitializer i = GxuInitializer.GetInstance();
                i.InitializeGXUnit();
                ContextHandler.GXUnitInitialized = true;
                return true;
            }
            else
            {
                MessageBox.Show("Open a knowledge base first.", "GXUnit");
                return false;
            }
        }

        public bool RunCommand_About(CommandData commandData)
        {
            AboutBox m = new AboutBox();
            m.ShowDialog();
            return true;
        }

        public bool RunCommand_GXUnitWindow(CommandData commandData)
        {
            if (GXUnitPackage.gXUnitWindow == null)
                GXUnitPackage.gXUnitWindow = GXUnitMainWindow.getInstance();
            UIServices.ToolWindows.ShowToolWindow(GXUnitMainWindow.guid);
            UIServices.ToolWindows.FocusToolWindow(GXUnitMainWindow.guid);
            GXUnitPackage.gXUnitWindow.SetFocus();
            return true;
        }

        public bool RunCommand_GXUnitResultsWindow(CommandData commandData)
        {
            if (GXUnitPackage.resultViewer == null)
                GXUnitPackage.resultViewer = GXUnitResultsViewer.getInstance();
            UIServices.ToolWindows.ShowToolWindow(GXUnitResultsViewer.guid);
            UIServices.ToolWindows.FocusToolWindow(GXUnitResultsViewer.guid);
            GXUnitPackage.resultViewer.SetFocus();
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
