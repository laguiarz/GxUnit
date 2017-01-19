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

namespace PGGXUnit.Packages.GXUnit
{
	[Guid("c3fa0379-b89e-4e07-b680-c9d50508ee8d")]
    public class GXUnitPackage : AbstractPackageUI
    {

        public static Guid guid = typeof(GXUnitPackage).GUID;
        public static GXUnitMainWindow GXUnitWindow;

		public override string Name
		{
			get { return "GXUnit"; }
		}

        public override void Initialize(IGxServiceProvider services)
        {
            base.Initialize(services);

            // Adding the Default Test Case Provider
            DefaultManager.Manager.RegisterDefaultProvider(new TestCaseDefaultProvider());

            LoadCategories();
            LoadCommandTargets();
            LoadObjectTypes();
            //LoadPartTypes();
            //LoadEditors();
        }


        public override void PostInitialize()
        {
            base.PostInitialize();
            AddProperty();
        }


        /// <summary>
        /// Metodo utilitario auxiliar para la creacion de la propiedad "Testeable"
        /// </summary>
        private void AddProperty()
        {
            string transactionKeyProc = DefinitionsHelper.GetPropertiesDefinitionKey<PGGXUnit.Packages.GXUnit.GeneXusAPI.TestCase>();
            PropertiesDefinition myDefinitions = CreateMyPropertiesDefinition(transactionKeyProc);
            AddPropertiesDefinition(transactionKeyProc, myDefinitions);
        }

        private PropertiesDefinition CreateMyPropertiesDefinition(string objectClass)
        {
            PropertiesDefinition myProperties = new PropertiesDefinition(objectClass);
            myProperties.AddDefinition("TestCase", typeof(bool), false, null);
            myProperties.AddDefinition("ObjectToTest", typeof(String), "", null);
            return myProperties;
        }

        private void LoadCategories()
        {
            //Insert all categories here
            //KBObjectCategoryDescriptor myCategory = new KBObjectCategoryDescriptor(new Guid("ef0f3552-a93d-4f10-9b94-4442e32ea719"), "New Category", Items.Folder);
            //this.AddCategory(myCategory.Id, myCategory.Name, myCategory.Icon);
        }

        private void LoadObjectTypes()
        {
           // this.AddObjectType<Resultado>();
            this.AddObjectType<TestCase>();
        }

        //private void LoadPartTypes()
        //{
        //    this.AddPart<ResultadoPart>();
        //}

        //private void LoadEditors()
        //{
        //    this.AddEditor<MyEditor>(typeof(ResultadoPart).GUID);
        //}

        private void LoadCommandTargets()
        {
            AddCommandTarget(new CommandManager());
        }

        [EventSubscription(UIEvents.AfterOpenKB)]
        public override void OnAfterOpenKB(object sender, KBEventArgs args)
        {
            base.OnAfterOpenKB(sender, args);

            ManejadorContexto.Model = KBManager.getModel(args.KnowledgeBase);
            ManejadorContexto.KBName = args.KnowledgeBase.Name;
            ManejadorContexto.ObjectToTest = "";

            ManejadorFolder mf = new ManejadorFolder();
            Folder folder = mf.GetFolder("GXUnit");
            if (folder != null)
            {
                ManejadorContexto.GXUnitInicializado = true;
                GXUnit.GXUnitUI.GXUnitMainWindow.getInstance().cargarNodosTest();
            }
            else
            {
                ManejadorContexto.GXUnitInicializado = false;
            }
        }

        [EventSubscription(UIEvents.AfterDeleteKBObject)]
        public void OnAfterDeleteKBObject(object sender, KBObjectEventArgs args)
        {
            GXUnitMainWindow.getInstance().cargarNodosTest();
        }
 
        [EventSubscription(UIEvents.AfterCreateKBObject)]
        public void OnAfterCreateKBObject(object sender, KBObjectEventArgs args)
        {
            if (args.KBObject.GetPropertyValue("TestCase") != null && (bool)args.KBObject.GetPropertyValue("TestCase"))
            {
                if (!string.IsNullOrEmpty(ManejadorContexto.ObjectToTest))
                {
                    IEnumerator<KBObjectPart> enumerator = args.KBObject.Parts.GetEnumerator();
                    KBObjectPart part = null;
                    string source = null;
                    while (enumerator.MoveNext())
                    {
                        part = enumerator.Current;
                        try
                        {
                            source = ((ProcedurePart)(part)).Source;
                            if (source != null)
                            {
                                ((ProcedurePart)(part)).Source = source;
                                break;
                            }

                        }
                        catch (Exception e)
                        {
                            FuncionesAuxiliares.EscribirOutput(e.Message);
                        }
                    }
                }
            }

            ManejadorContexto.ObjectToTest = "";
        }

        //[EventSubscription(UIEvents.BeforeOpenKBObject)]
        //public void OnAfterOpenKBObject(object sender, KBObjectEventArgs args)
        //{
        //    if (args.KBObject.GetPropertyValue("TestCase") != null && (bool)args.KBObject.GetPropertyValue("TestCase"))
        //    {
        //        IEnumerator<KBObjectPart> enumerator = args.KBObject.Parts.GetEnumerator();
                
        //        KBObjectPart part = null;
        //        string source = null;
        //        while (enumerator.MoveNext())
        //        {
        //            part = enumerator.Current;
        //            try
        //            {
        //                source = ((ProcedurePart)(part)).Source;
        //                if (source != null)
        //                {
        //                    break;
        //                }

        //            }
        //            catch (Exception e)
        //            {
        //                FuncionesAuxiliares.EscribirOutput(e.Message);
        //            }
        //        }
        //    }
        //}

        //public override void ReadPart(KBObjectPart part, XPathNavigator partData, ImportOptions options, IReferenceResolver resolver, OutputMessages output)
        //{
        //    XPathNavigator partProps = partData.SelectSingleNode("ArchivoResultado");
        //    if (partProps != null)
        //    {
        //        if (part is ResultadoPart)
        //        {
        //            ResultadoPart myPart = part as ResultadoPart;
        //            myPart.ArchivoResultado = partProps.TypedValue.ToString();
        //        }
        //    }
        //}

        [EventSubscription(GXEvents.AfterBuild)]
        public void OnAfterBuild(object sender, EventArgs args)
        {
            //FuncionesAuxiliares.EscribirOutput("AfterBuild");
            ManejadorRunner mr = ManejadorRunner.GetInstance();

            ManejadorLenguaje.SetLenguajeModelo();
            if (ManejadorLenguaje.Lenguaje == GeneratorType.CSharpWeb)
            {
                if (!ManejadorContexto.ForceEjecutarRunner)
                {
                    if (ManejadorContexto.Ejecutar)
                    {
                        ManejadorContexto.ForceEjecutarRunner = true;
                        mr.EjecutarRunner();
                    }
                }
                else
                {
                    ManejadorContexto.ForceEjecutarRunner = false;
                    if (ManejadorContexto.Ejecutar)
                    {
                        ManejadorContexto.Ejecutar = false;
                        bool error = false;
                        try
                        {
                            crearResultado();
                        }
                        catch (Exception ex)
                        {
                            ex.ToString();
                            error = true;
                        }
                        if (error)
                        {
                            Thread.Sleep(1000);
                            crearResultado();
                        }
                    }
                }
            }
            else if (ManejadorLenguaje.Lenguaje == GeneratorType.JavaWeb)
            {
                if (ManejadorContexto.Ejecutar)
                {
                    ManejadorContexto.Ejecutar = false;
                    bool error = false;
                    try
                    {
                        crearResultado();
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        error = true;
                    }
                    if (error)
                    {
                        Thread.Sleep(1000);
                        crearResultado();
                    }
                }
            }
        }

        private void crearResultado()
        {
            string LastXMLName = ManejadorContexto.LastXMLName;

            ManejadorResultado mr = ManejadorResultado.GetInstance();
            string outputPath = mr.CreateResult(LastXMLName);
            FuncionesAuxiliares.EscribirOutput("GXUnit_OnAfterBuild- Results located at " + outputPath);
            ManejadorProcedimiento mp = new ManejadorProcedimiento();
            mp.EliminarProcedimiento(Constantes.RUNNER_PROC);
        }

        [EventSubscription(ArchitectureEvents.AfterKBObjectImport)]
        public void OnAfterImport(object sender, EventArgs args)
        {
            GXUnit.GXUnitUI.GXUnitMainWindow.getInstance().cargarNodosTest();
        }
        
        [EventSubscription(ArchitectureEvents.AfterCloseKB)]
        public void OnAfterCloseKB(object sender, EventArgs args)
        {
            GXUnit.GXUnitUI.GXUnitMainWindow.getInstance().limpiarNodosTest();
            ManejadorContexto.GXUnitInicializado = false;
        }

        public override IToolWindow CreateToolWindow(System.Guid toolWindowId)
        {
            if (toolWindowId.Equals(GXUnitMainWindow.guid))
            {
                if (GXUnitWindow == null)
                    GXUnitWindow = GXUnitMainWindow.getInstance();
                return GXUnitWindow;
            }

            return base.CreateToolWindow(toolWindowId);
        }
	}
}
