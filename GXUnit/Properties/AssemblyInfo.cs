using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Artech.Architecture.Common.Packages;
using Artech.Architecture.Common.Descriptors;
using PGGXUnit.Packages.GXUnit;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using PGGXUnit.Packages.GXUnit.GeneXusAPI;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("GXUnit")]
[assembly: AssemblyDescription("sites.google.com/site/proyectogxunit")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Proyect GXUnit for unit testing in GeneXus")]
[assembly: AssemblyProduct("GXUnit")]
[assembly: AssemblyCopyright("2013")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// The following attributes are declarations related to this assembly
// as a GeneXus Package
[assembly: PackageAttribute(typeof(PGGXUnit.Packages.GXUnit.GXUnitPackage))]

[assembly: KBObjectsDeclarationAttribute(
	typeof(TestCase)
	)]

[assembly: KBObjectsDeclarationAttribute(
    typeof(TestSuite)
    )]

[assembly: KBObjectsDeclarationAttribute(
    typeof(Resultado)
    )]

[assembly: KBObjectPartsDeclarationAttribute(
    typeof(ResultadoPart)
    )]
// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("33150324-ce8c-47be-9368-72ec8f3a2809")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.1.0.0")]
[assembly: AssemblyFileVersion("1.1.0.0")]
