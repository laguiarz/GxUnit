using Artech.Architecture.Common.Descriptors;
using Artech.Architecture.Common.Defaults;
using Artech.Architecture.Common.Events;
using Artech.Architecture.Common.Objects;
using Artech.Architecture.Common.Services;
using Artech.Architecture.UI.Framework.Packages;
using Artech.Architecture.UI.Framework;
using Artech.Architecture.UI.Framework.Controls;
using Artech.Common.Properties;

using System;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Practices.CompositeUI.EventBroker;

using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using PGGXUnit.Packages.GXUnit.GXUnitUI;
using Artech.Genexus.Common;
using System.Collections.Generic;
using Artech.Genexus.Common.Parts;
using System.Xml.XPath;
using Artech.Common.Diagnostics;
using Artech.Genexus.Common.Entities;
using System.IO;

namespace PGGXUnit.Packages.GXUnit
{
	[Guid("c3fa0379-b89e-4e07-b680-c9d50508ee8d")]
    public class GXUnitPackage : AbstractPackageUI
    {

        public static Guid guid = typeof(GXUnitPackage).GUID;
        public static GXUnitMainWindow gXUnitWindow;
        public static GXUnitResultsViewer resultViewer;

		public override string Name
		{
			get { return "GXUnit"; }
		}

        public override void Initialize(IGxServiceProvider services)
        {
            base.Initialize(services);

            // Adding the Default Test Case Provider
          //  DefaultManager.Manager.RegisterDefaultProvider(new TestCaseDefaultProvider());

           // LoadCategories();
            LoadCommandTargets();
            //LoadObjectTypes();
        }

        private void LoadCommandTargets()
        {
            AddCommandTarget(new CommandManager());
        }

        [EventSubscription(UIEvents.AfterOpenKB)]
        public override void OnAfterOpenKB(object sender, KBEventArgs args)
        {
            base.OnAfterOpenKB(sender, args);

            ContextHandler.Model = KBManager.GetModel(args.KnowledgeBase);
            ContextHandler.KBName = args.KnowledgeBase.Name;
 //           ContextHandler.ObjectToTest = "";

            GxuFolderHandler mf = new GxuFolderHandler();
            Folder folder = mf.GetFolder(Constants.GXUNIT_FOLDER);
            if (folder != null)
            {
                ContextHandler.GXUnitInitialized = true;
                GXUnitMainWindow.getInstance().LoadTestTrees();
                GXUnitResultsViewer.getInstance().PopulateListBox();
            }
            else
            {
                ContextHandler.GXUnitInitialized = false;
            }
        }

        [EventSubscription(UIEvents.AfterDeleteKBObject)]
        public void OnAfterDeleteKBObject(object sender, KBObjectEventArgs args)
        {
            GXUnitMainWindow.getInstance().LoadTestTrees();
        }

        [EventSubscription(ArchitectureEvents.AfterKBObjectImport)]
        public void OnAfterImport(object sender, EventArgs args)
        {
            GXUnit.GXUnitUI.GXUnitMainWindow.getInstance().LoadTestTrees();
        }

        [EventSubscription(ArchitectureEvents.AfterCloseKB)]
        public void OnAfterCloseKB(object sender, EventArgs args)
        {
            GXUnit.GXUnitUI.GXUnitMainWindow.getInstance().limpiarNodosTest();
            ContextHandler.GXUnitInitialized = false;
        }

        public override IToolWindow CreateToolWindow(System.Guid toolWindowId)
        {
            if (toolWindowId.Equals(GXUnitMainWindow.guid))
            {
                if (gXUnitWindow == null)
                    gXUnitWindow = GXUnitMainWindow.getInstance();
                return gXUnitWindow;
            } else
            {
                if (toolWindowId.Equals(GXUnitResultsViewer.guid))
                {
                    if (resultViewer == null)
                        resultViewer = GXUnitResultsViewer.getInstance();
                    return resultViewer;
                }
            }

            return base.CreateToolWindow(toolWindowId);
        }

        //private void LoadCategories()
        //{
        //    //Insert all categories here
        //    //KBObjectCategoryDescriptor myCategory = new KBObjectCategoryDescriptor(new Guid("ef0f3552-a93d-4f10-9b94-4442e32ea719"), "New Category", Items.Folder);
        //    //this.AddCategory(myCategory.Id, myCategory.Name, myCategory.Icon);
        //}      //public override void PostInitialize()
        //{
        //    base.PostInitialize();
        //    AddProperty();
        //}
        /// <summary>
        /// Metodo utilitario auxiliar para la creacion de la propiedad "Testeable"
        /// </summary>
        //private void AddProperty()
        //{
        //    string transactionKeyProc = DefinitionsHelper.GetPropertiesDefinitionKey<PGGXUnit.Packages.GXUnit.GeneXusAPI.TestCase>();
        //    PropertiesDefinition myDefinitions = CreateMyPropertiesDefinition(transactionKeyProc);
        //    AddPropertiesDefinition(transactionKeyProc, myDefinitions);
        //}

        //private PropertiesDefinition CreateMyPropertiesDefinition(string objectClass)
        //{
        //    PropertiesDefinition myProperties = new PropertiesDefinition(objectClass);
        //    myProperties.AddDefinition("TestCase", typeof(bool), false, null);
        //    myProperties.AddDefinition("ObjectToTest", typeof(String), "", null);
        //    return myProperties;
        //}       //private void LoadObjectTypes()
        //{
        //    this.AddObjectType<TestCase>();
        //}
        ////[EventSubscription(UIEvents.AfterCreateKBObject)]
        ////public void OnAfterCreateKBObject(object sender, KBObjectEventArgs args)
        ////{
        ////    if (args.KBObject.GetPropertyValue("TestCase") != null && (bool)args.KBObject.GetPropertyValue("TestCase"))
        ////    {
        ////        //if (!string.IsNullOrEmpty(ContextHandler.ObjectToTest))
        ////        //{
        ////        //    IEnumerator<KBObjectPart> enumerator = args.KBObject.Parts.GetEnumerator();
        ////        //    KBObjectPart part = null;
        ////        //    string source = null;
        ////        //    while (enumerator.MoveNext())
        ////        //    {
        ////        //        part = enumerator.Current;
        ////        //        try
        ////        //        {
        ////        //            source = ((ProcedurePart)(part)).Source;
        ////        //            if (source != null)
        ////        //            {
        ////        //                ((ProcedurePart)(part)).Source = source;
        ////        //                break;
        ////        //            }

        ////        //        }
        ////        //        catch (Exception e)
        ////        //        {
        ////        //            GxHelper.WriteOutput(e.Message);
        ////        //        }
        ////        //    }
        ////        //}
        ////    }

        ////    ContextHandler.ObjectToTest = "";
        ////}


        //[EventSubscription(GXEvents.AfterBuild)]
        //public void OnAfterBuild(object sender, EventArgs args)
        //{
        //    RunnerHandler mr = RunnerHandler.GetInstance();

        //    KBLanguageHandler.SetLenguajeModelo();
        //    if (KBLanguageHandler.Lenguaje == GeneratorType.CSharpWeb)
        //    {
        //        GxHelper.WriteOutput("GXUnit Steps After Build (csharp)");
        //        if (!ContextHandler.ForceExecuteRunner)
        //        {
        //            if (ContextHandler.Execute)
        //            {
        //                ContextHandler.ForceExecuteRunner = true;
        //                mr.ExecuteRunnerProc();
        //            }
        //        }
        //        else
        //        {
        //            ContextHandler.ForceExecuteRunner = false;
        //            if (ContextHandler.Execute)
        //            {
        //                ContextHandler.Execute = false;
        //                bool error = false;
        //                try
        //                {
        //                   // createTestOutputFile();
        //                }
        //                catch (Exception ex)
        //                {
        //                    ex.ToString();
        //                    error = true;
        //                }
        //                if (error)
        //                {
        //                    Thread.Sleep(1000);
        //                   // createTestOutputFile();
        //                }
        //            }
        //        }
        //    }
        //    else if (KBLanguageHandler.Lenguaje == GeneratorType.JavaWeb)
        //    {
        //        GxHelper.WriteOutput("GXUnit Steps After Build (java)");

        //        if (ContextHandler.Execute)
        //        {
        //            ContextHandler.Execute = false;
        //            bool error = false;
        //            try
        //            {
        //               // createTestOutputFile();
        //            }
        //            catch (Exception ex)
        //            {
        //                ex.ToString();
        //                error = true;
        //            }
        //            if (error)
        //            {
        //                Thread.Sleep(1000);
        //               // createTestOutputFile();
        //            }
        //        }
        //    }
        //}

        ////private void createTestOutputFile()
        ////{
        ////    string LastXMLName = ContextHandler.LastXMLName;

        ////    string outputPath = MoveTestOutputToGXUnitStorage(LastXMLName);
        ////    GxHelper.WriteOutput("GXUnit_OnAfterBuild- Results located at " + outputPath);
        ////}


        //public string MoveTestOutputToGXUnitStorage(String fileName)
        //{
        //    try
        //    {
        //        string kbPath = KBManager.getTargetPath();
        //        string resultPath = kbPath.Trim() + Constants.RESULT_PATH;

        //        DirectoryInfo di = Directory.CreateDirectory(resultPath);

        //        string sourcePath = Path.Combine(kbPath, fileName);
        //        string targetPath = Path.Combine(resultPath, fileName);
        //        File.Copy(sourcePath, targetPath);
        //        File.Delete(sourcePath);

        //        return targetPath;
        //    }
        //    catch (Exception e)
        //    {
        //        GxHelper.WriteOutput("Exception: " + e.Message);
        //        return "";
        //    }
        //}


    }
}
