using System.Runtime.InteropServices;
using System.Collections.Generic;

using Artech.Architecture.Common.Descriptors;
using Artech.Architecture.Common.Objects;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Parts;
using Artech.Genexus.Common.Objects;
using Artech.Genexus.Common.Types;
using Artech.Common.Properties;

using PGGXUnit.Packages.GXUnit.Utils;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using Artech.Architecture.UI.Framework.Services;
using Artech.Architecture.Common.Defaults;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
	[Guid("4deda4ab-1e50-4c81-a57d-d0dca8d1081b")]
    [KBObjectDescriptor("227b233e-e173-44cf-a29d-588fceb19b5c", "TestCase", "TestCase", "PGGXUnit.Packages.GXUnit.Items, Object")]
	[KBObjectComposition(true, typeof(DocumentationPart))]
    public class TestCase : Procedure
	{
        
		public TestCase(KBModel model)
			: base(model)
		{
            this.SetPropertyValue("TestCase", true);
            this.SetPropertyValue(Artech.Genexus.Common.Properties.PRC.CommitOnExit, Artech.Genexus.Common.Properties.PRC.CommitOnExit_Enum.No);
            IApplyDefaultTarget procedurePart = (IApplyDefaultTarget)this.ProcedurePart;
            procedurePart.IsDefault = true;
            procedurePart.SetProviderData("TestCaseDefaultProvider", "CreateTestCaseSource");
		}

        public void ReGenerarTestCase()
        {
            Procedimiento testcase = ManejadorDTTestCase.GetInstance().GetDTTestCase((string)this.GetPropertyValue("ObjectToTest"));
            if (testcase != null)
            {
                this.ProcedurePart.Source = testcase.GetSource();
                foreach (Parametro p in testcase.GetVariablesRules())
                {
                    Variable var = new Variable(p.GetVarName(), this.Variables);
                    var.AttributeBasedOn = p.GetVariable().AttributeBasedOn;
                    var.Decimals = p.GetVariable().Decimals;
                    var.Description = p.GetVariable().Description;
                    var.DomainBasedOn = p.GetVariable().DomainBasedOn;
                    var.DomainKey = p.GetVariable().DomainKey;
                    var.IsCollection = p.GetVariable().IsCollection;
                    var.Length = p.GetVariable().Length;
                    var.Name = p.GetVarName();
                    var.Signed = p.GetVariable().Signed;
                    var.Type = p.GetVariable().Type;

                    DataType.ParseInto(ManejadorContexto.Model, p.GetVarName(), var);
                    this.Variables.Variables.Add(var);
                }
            }
        }

        public bool selectObjectToTest()
        {
            string objectToTest = "";
            SelectObjectOptions options = new SelectObjectOptions();
            options.MultipleSelection = false;
            options.ObjectTypes.Clear();
            options.ObjectTypes.Add(Artech.Architecture.Common.Descriptors.KBObjectDescriptor.Get("Procedure"));
            options.ObjectTypes.Add(Artech.Architecture.Common.Descriptors.KBObjectDescriptor.Get("Transaction"));
            options.ObjectTypes.Add(Artech.Architecture.Common.Descriptors.KBObjectDescriptor.Get("DataProvider"));

            KBObject kbObjectSelected = UIServices.SelectObjectDialog.SelectObject(options);

            if (kbObjectSelected != null)
            {
                objectToTest = kbObjectSelected.Name;
                ManejadorContexto.ObjectToTest = objectToTest;
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public string getObjectToTest()
        {
            return (string)this.GetPropertyValue("ObjectToTest");
        }
	}
}
