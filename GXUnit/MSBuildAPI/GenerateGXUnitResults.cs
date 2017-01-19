using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Artech.Architecture.Common.Services;
using Artech.Genexus.Common;
using Artech.MsBuild.Common;
using Microsoft.Build.Framework;
using PGGXUnit.Packages.GXUnit.GeneXusAPI;

namespace PGGXUnit.Packages.GXUnit.MSBuildAPI
{
    //public class GenerateGXUnitResults : ArtechTask
    //{
    //    private string xmlName;

    //    [Required]
    //    public string XMLName
    //    {
    //        get { return xmlName; }
    //        set { xmlName = value; }
    //    }

    //    public override bool Execute()
    //    {
    //        OutputSubscribe();
    //        CommonServices.Output.StartSection("Generate GXUnit Results Task");

    //        ManejadorContexto.Model = KB.DesignModel;

    //        String nom = System.DateTime.Now.ToString("R_yyyyMMdd_HHmmss");
    //        ManejadorResultado mr = ManejadorResultado.GetInstance();
    //        GxModel modelo = KBManager.getTargetModel();
    //        string xmlPath = Path.Combine(modelo.WebTargetFullPath, xmlName);

    //       // List<string> res = mr.GetResultado(xmlPath, xmlName);
    //       // foreach (string s in res)
    //       // {
    //       //     CommonServices.Output.AddLine(s);
    //       // }

    //        ManejadorProcedimiento mp = new ManejadorProcedimiento();
    //        mp.EliminarProcedimiento("RunnerProcedure");

    //        CommonServices.Output.EndSection("Generate GXUnit Results Task", true);
    //        OutputUnsubscribe();
    //        return true;
    //    }
    //}
}
