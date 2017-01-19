using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

using Artech.Architecture.Common.Defaults;
using Artech.Architecture.Common.Objects;
using Artech.Udm.Framework.References;
using Artech.Common.Helpers.IO;
using Artech.Common.Helpers.Templates;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Parts;
using Artech.Genexus.Common.Types;

using PGGXUnit.Packages.GXUnit.GXUnitCore;
using PGGXUnit.Packages.GXUnit.GXUnitUI;
using Artech.Genexus.Common.CustomTypes;
using Artech.Genexus.Common.Objects;


namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    internal class TestCaseDefaultProvider : IDefaultProvider
	{
		#region IDefaultProvider Members

		/// <summary>
		/// Gets the identifiation string of this default provider.
		/// </summary>
		public string Id
		{
			get { return "TestCaseDefaultProvider"; }
		}

		/// <summary>
		/// Gets the default data to be used on a new object or part.
		/// </summary>
		public bool InitializeProviderData(IApplyDefaultTarget target, out string providerData)
		{
			providerData = null;
			return false;
		}


        private Procedimiento contruirProcedimientoMaestro()
        {
            string objectToTest = ManejadorContexto.ObjectToTest;

            Procedimiento testcase = ManejadorDTTestCase.GetInstance().GetDTTestCase(objectToTest);

            return testcase;
        }

    
        /// <summary>
		/// Resolves the template path and custom parameters to be used given an object or part.
		/// </summary>
		public bool GetTemplate(IApplyDefaultTarget target, string providerData, out string templatePath, Generator.GeneratorParameters parameters)
		{
			templatePath = string.Empty;

			if (target is ProcedurePart)
			{
                if (providerData == "CreateTestCaseSource")
				{
                    templatePath = GetTemplatePath("TestCaseSourceCall.dkt");

                    TestCase testcase = ((TestCase)(target.Object));
                    bool objectselected = false;
                    string objectToTest = testcase.getObjectToTest();
                    if (objectToTest == "")
                    {
                        objectselected = testcase.selectObjectToTest();
                        objectToTest = ManejadorContexto.ObjectToTest;
                    }
                    else
                    {
                        objectselected = true;
                        ManejadorContexto.ObjectToTest = objectToTest;
                    }
                    
                    if (objectselected)
                    {
                        testcase.SetPropertyValue("ObjectToTest", objectToTest);

                        Procedimiento proc = contruirProcedimientoMaestro();
                        if (proc != null)
                        {
                            string source = proc.GetSource();

                            foreach (Parametro p in proc.GetVariablesRules())
                            {
                                string nombre = getNombre(p.GetVariable().Name, testcase);

                                Variable var = new Variable(p.GetVarName(), testcase.Variables);
                                var.AttributeBasedOn = p.GetVariable().AttributeBasedOn;
                                var.Decimals = p.GetVariable().Decimals;
                                var.Description = p.GetVariable().Description;
                                var.DomainBasedOn = p.GetVariable().DomainBasedOn;
                                var.DomainKey = p.GetVariable().DomainKey;
                                var.IsCollection = p.GetVariable().IsCollection;
                                var.Name = nombre;
                                var.Signed = p.GetVariable().Signed;
                                var.Type = p.GetVariable().Type;
                                var.Length = p.GetVariable().Length;

                                AttCustomType type = p.GetVariable().GetPropertyValue<AttCustomType>(Properties.ATT.DataType);
                                if (type.DataType == (int)eDBType.GX_SDT)
                                {
                                    StructureTypeReference strRef = StructureTypeReference.Deserialize(type);
                                    string sdtLevelFullName = StructureInfoProvider.GetName(ManejadorContexto.Model, strRef);
                                    DataType.ParseInto(ManejadorContexto.Model, sdtLevelFullName, var);
                                }

                                if (p.GetNomTipoComplejo() != null)
                                    DataType.ParseInto(ManejadorContexto.Model, p.GetNomTipoComplejo(), var);
                                testcase.Variables.Variables.Add(var);
                            }

                            parameters.Properties.Add("Source", source);
                        }
                        else
                        {
                            parameters.Properties.Add("Source", "// " + ManejadorContexto.Message);
                        }
                    }
                    else
                    {
                        parameters.Properties.Add("Source", "// No object selected to test.");
                    }
                    string parentName = null;
                    if (GXUnitMainWindow.getInstance().isSuitesFolderChild((Folder)testcase.Parent))
                    {
                        parentName = testcase.Parent.Name;
                    }
                    else
                    {
                        List<TreeNode> listaSuites = GXUnitMainWindow.getInstance().getListaSuites();
                        SelectSuite selSuite = new SelectSuite(listaSuites);
                        parentName = selSuite.selectedSuite;
                        testcase.Parent = (new ManejadorFolder()).GetFolder(parentName);
                    }
                    GXUnitMainWindow.getInstance().agregarTestCase(testcase, parentName);
            		AddParameters(target, parameters);
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Changes a reference from this default target.
		/// Used to control whether references in default part are weak or not (or even to suppress them).
		/// </summary>
		public virtual bool UpdateReference(IApplyDefaultTarget target, EntityReference reference)
		{
            reference.ReferenceType = ReferenceType.Weak;
			return false;
		}

        public virtual bool ShouldApplyTemplate(IApplyDefaultTarget applyDefaultTarget, string template)
        {
            return false;
        }
	
		#endregion

		#region helper methods

		protected string GetTemplatePath(string templateFileName)
		{
			return Path.Combine(BaseTemplatePath, templateFileName);
		}

		private string baseTemplatePath;

		protected string BaseTemplatePath
		{
			get
			{
				if (baseTemplatePath == null)
					baseTemplatePath = Path.Combine(PathHelper.StartupPath, "Templates");

				return baseTemplatePath;
			}
		}

		protected void AddParameters(IApplyDefaultTarget target, Generator.GeneratorParameters parameters)
		{
		}

		#endregion

        private string getNombre(string nom, TestCase test)
        {
            foreach (Variable v in test.Variables.Variables)
            {
                if (v.Name == nom)
                    return getNombre(nom + "1", test);
            }
            return nom;
        }
	}
}