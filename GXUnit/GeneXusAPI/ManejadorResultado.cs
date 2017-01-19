using System;
using System.IO;
using System.Xml;

using PGGXUnit.Packages.GXUnit.GXUnitCore;

using Artech.Architecture.Common.Objects;
using Artech.Architecture.UI.Framework.Services;
using Artech.Genexus.Common;
using System.Collections.Generic;
using System.Threading;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{

    class ManejadorResultado
    {

        private static ManejadorResultado instance = new ManejadorResultado();

        private ManejadorResultado()
        {
        }

        public static ManejadorResultado GetInstance()
        {
            return instance;
        }

        //public Resultado CrearResultado(String nombre)
        //{
        //    GxModel modelo = KBManager.getTargetModel();

        //    string targetPath;

        //    if ((bool)modelo.Environment.Properties.GetPropertyValue("IS_WEB_GEN") == true)
        //    {
        //        targetPath = modelo.WebTargetFullPath;
        //    }
        //    else
        //    {
        //        targetPath = modelo.TargetFullPath;
        //    }

        //    string path = Path.Combine(targetPath, ManejadorContexto.LastXMLName);

        //    return CrearResultado(path, nombre);
        //}


        public string CreateResult(String fileName)
        {
            try
            {
                string kbPath = KBManager.getTargetPath();

                string resultPath = kbPath.Trim() + Constantes.RESULT_PATH;

                DirectoryInfo di = Directory.CreateDirectory(resultPath);


                string sourcePath = Path.Combine(kbPath, fileName);
                string targetPath = Path.Combine(resultPath, fileName);
                File.Copy(sourcePath, targetPath);
                File.Delete(sourcePath);

                return targetPath;
            }
            catch (Exception e)
            {
                FuncionesAuxiliares.EscribirOutput("Exception: " + e.Message);
                return "";
            }
        }

        //public Resultado CrearResultado(String path, String nombre)
        //{
        //    String fileStr = "NULL";
        //    XmlDocument doc = new XmlDocument();
        //    Resultado res = null;
        //    try
        //    {
        //        Thread.Sleep(2000);
        //        doc.Load(path);

        //        fileStr = doc.OuterXml;
        //        //borro al archivo xml
        //        File.Delete(path);

        //        res = new Resultado(ManejadorContexto.Model);
        //        res.Name = nombre;
        //        res.Parent = ManejadorFolder.GetFolderObject(ManejadorContexto.Model, Constantes.carpetaResults);
        //        ResultadoPart respart = res.Parts.Get<ResultadoPart>();
        //        respart.ArchivoResultado = fileStr;
        //        res.Save();
        //    }
        //    catch (FileNotFoundException)
        //    {
        //        FuncionesAuxiliares.EscribirOutput("An exception occurred while running the tests. See the execution log for details.");
        //    }
        //    catch (Exception e)
        //    {
        //       FuncionesAuxiliares.EscribirOutput("Exception: " + e.Message);
        //    }
        //    return res;
        //}

        //public List<string> GetResultado(String path, String nombre)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    List<string> res = new List<string>();
        //    try
        //    {
        //        doc.Load(path);
        //        //borro al archivo xml
        //        File.Delete(path);

        //        XmlNodeList tests = doc.GetElementsByTagName("TestName");
        //        foreach (XmlNode test in tests)
        //        {
        //            XmlNodeList asserts = (test.ParentNode as XmlElement).GetElementsByTagName("Result");
        //            foreach (XmlNode assert in asserts)
        //            {
        //                res.Add(test.InnerText + " - " + assert.InnerText);
        //            }
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        FuncionesAuxiliares.EscribirOutput("Exception: " + e.Message);
        //    }
        //    return res;
        //}

        //public void AbrirResultado(Resultado res) {
        //    if (res != null)
        //        UIServices.Objects.Open(res, Artech.Architecture.UI.Framework.Services.OpenDocumentOptions.CurrentVersionPart(res.Guid, null));
        //}

        //public void CrearAbrirResultado(String nombre)
        //{
        //    Resultado res = CrearResultado(nombre);
        //    AbrirResultado(res);
        //}

        //public bool ExisteResultado(string name)
        //{
        //    foreach (KBObject obj in ManejadorContexto.Model.Objects.GetAll())
        //    {
        //        if (obj.Name.ToLower().Equals(name.ToLower()))
        //            return true;
        //    }
        //    return false;
        //}

    }
}
