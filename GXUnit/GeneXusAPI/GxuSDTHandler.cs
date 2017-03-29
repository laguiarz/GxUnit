/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed Get function to get/set properties
 *                  Commented out unreferenced code
 * */

using System;
using System.Collections.Generic;

using Artech.Genexus.Common.Objects;
using Artech.Genexus.Common.Parts.SDT;
using Artech.Genexus.Common;
using Artech.Genexus.Common.Types;
using Artech.Genexus.Common.CustomTypes;
using Artech.Architecture.Common.Objects;

using PGGXUnit.Packages.GXUnit.GXUnitCore;
using Artech.Genexus.Common.Parts;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    class GxuSDTHandler
    {
        private KBModel model;

        public GxuSDTHandler()
        {
            this.model = ContextHandler.Model;
        }

        public KBModel Model
        {
            get { return model; }
            set { model = value; }
        }

        public bool CreateSDT(GxuSDT gxuSDT, bool force)
        {
            String msgoutput;

            SDT sdt = GetSDTObject(model, gxuSDT.Name);

            if (sdt == null || force)
            {
                if (sdt == null)
                    sdt = new SDT(model);

                sdt.Name = gxuSDT.Name;

                foreach (GxuProperty p in gxuSDT.Propiedades)
                {
                    sdt.SetPropertyValue(p.Name, p.PropertyValue);
                }

                Folder parentFolder = GxuFolderHandler.GetFolderObject(this.model, gxuSDT.ParentName);
                if (parentFolder != null)
                    sdt.Parent = parentFolder;
                else
                    sdt.Parent = GxuFolderHandler.GetRootFolder(model);

                SDTLevel root = sdt.SDTStructure.Root;

                root.Items.Clear();
                root.IsCollection = gxuSDT.Root.IsCollection;
              
                root.CollectionItemName = gxuSDT.Root.Name;
                
                foreach (GxuSDTItem i in gxuSDT.Root.GetItems())
                {
                    eDBType dataType = GxHelper.ConvertToGXType(i.DataType);
                    root.AddItem(i.Name, dataType, i.Length);
                    root.IsCollection = i.IsCollection;

                }

                //Add Collection-Items to Root
                SDTItem item;
                foreach (GxuSDTCollectionItem colItem in gxuSDT.Root.GetCollectionItems())
                {
                    eDBType dataType = GxHelper.ConvertToGXType(colItem.DataType);
                    item = root.AddItem(colItem.Name + "s", dataType, colItem.Length);
                    item.IsCollection = true;
                    item.CollectionItemName = colItem.Name;
                    if (colItem.DataType == Constants.GxuDataType.SDT)
                    {
                        DataType.ParseInto(model, colItem.ComplexDataType, item);
                    }
                }

                SDTLevel sdtLevel;
                foreach (GXUnitCore.GxuSDTLevel gxuSDTLevel in gxuSDT.GetNiveles())
                {

                        sdtLevel = root.AddLevel(gxuSDTLevel.Name + 's', gxuSDTLevel.Name);
                        sdtLevel.IsCollection = gxuSDTLevel.IsCollection;
                        sdtLevel.Items.Clear();

                        foreach (GxuSDTItem i in gxuSDTLevel.GetItems())
                        {
                            eDBType tipo = GxHelper.ConvertToGXType(i.DataType);
                            item = sdtLevel.AddItem(i.Name, tipo, i.Length);
                            if (i.DataType == Constants.GxuDataType.SDT)
                            {
                                DataType.ParseInto(model, i.ComplexTypeName, item);
                            }
                            sdt.SDTStructure.Dirty = true;
                        }
                }

                sdt.Save();

                msgoutput = "SDT Object " + gxuSDT.Name + " created!";
                GxHelper.WriteOutput(msgoutput);
                return true;
            }
            msgoutput = "SDT Object " + gxuSDT.Name + " already exists!";
            GxHelper.WriteOutput(msgoutput);
            return false;
        }

        public bool DeleteSDT(GxuSDT gxuSDT)
        {
            String msgoutput;

            SDT sdt = GetSDTObject(this.model, gxuSDT.Name);
            if (sdt != null)
            {
                try
                {
                    sdt.Delete();
                    msgoutput = "SDT Object " + gxuSDT.Name + " deleted!";
                    GxHelper.WriteOutput(msgoutput);
                } catch (Exception e)
                {
                    msgoutput = "Failed to delete " + gxuSDT.Name ;
                    GxHelper.WriteOutput(msgoutput);
                    msgoutput = e.Message;
                    GxHelper.WriteOutput(msgoutput);
                }
                return true;
            }
            else
            {
                msgoutput = "SDT Object " + gxuSDT.Name + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
                return false;
            }
        }

        public Variable GetSDTVariable(String varName, String sdtName, bool isColl)
        {
            SDT sdt = null;
#if GXTILO
            if (sdtName.Contains("."))
            {
                sdtName = sdtName.Substring(0, sdtName.IndexOf("."));
                QualifiedName qn = new QualifiedName(sdtName);
                sdt = SDT.Get(ContextHandler.Model, qn);
            }
            else
            {
                QualifiedName qn = new QualifiedName(sdtName);
                sdt = SDT.Get(ContextHandler.Model, qn);
            }

            if (sdt != null)
            {
                Variable var = new Variable(new VariablesPart(sdt));
                var.Name = varName;
                var.Type = eDBType.GX_SDT;
                var.IsCollection = isColl;

                DataType.ParseInto(ContextHandler.Model, sdtName, var);
                return var;
            }
            else
            {
                String msgoutput = "SDT " + sdtName + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
                return null;
            }
#else
            if (sdtName.Contains("."))
            {
                sdt = SDT.Get(ManejadorContexto.Model, sdtName.Substring(0, sdtName.IndexOf(".")));
            }
            else
            {
                sdt = SDT.Get(ManejadorContexto.Model, sdtName);
            }

            if (sdt != null)
            {
                Variable var = new Variable(new VariablesPart(sdt));
                var.Name = varName;
                var.Type = eDBType.GX_SDT;
                var.IsCollection = isColl;

                DataType.ParseInto(ManejadorContexto.Model, sdtName, var);
                return var;
            }
            else
            {
                String msgoutput = "SDT " + sdtName + " does not exists!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
                return null;
            }
#endif
        }

        public LinkedList<GxuParm> GetAtt(String sdtName)
        {
            LinkedList<GxuParm> fields = new LinkedList<GxuParm>();
            SDT sdt = GetSDTObject(ContextHandler.Model, sdtName);
            if (sdt != null)
            {
                foreach (SDTItem item in sdt.SDTStructure.Root.Items)
                {
                    if (GxHelper.isSimple(item.Type))
                        fields.AddLast(new GxuParm(item.Name, item.Type));
                    if (item.Type == eDBType.GX_SDT)
                    {
                        AttCustomType type = item.GetPropertyValue<AttCustomType>(Artech.Genexus.Common.Properties.ATT.DataType);
                        if (type.DataType == (int)eDBType.GX_SDT)
                        {
                            StructureTypeReference strRef = StructureTypeReference.Deserialize(type);
                            string sdtLevelFullName = StructureInfoProvider.GetName(model, strRef);
                            fields.AddLast(new GxuParm(item.Name, sdtLevelFullName, Constants.PARM_INOUT, Constants.Estructurado.SDT, item.IsCollection));
                        }
                    }
                    /*if (item.Type == eDBType.GX_BUSCOMP)
                        campos.AddLast(new Parametro(item.Name, "", Constantes.PARM_INOUT, Constantes.Estructurado.BC, item.IsCollection, false));*/
                }
            }
            return fields;
        }

        //public string getSDTTSourceForProcedure(Variable var, string prefix)
        //{
        //    bool collection = false;
        //    string varName = var.Name + prefix;
        //    try
        //    {
        //        string source = "";
        //        AttCustomType type = var.GetPropertyValue<AttCustomType>(Properties.ATT.DataType);
        //        StructureTypeReference strRef = StructureTypeReference.Deserialize(type);
        //        string sdtLevelFullName = StructureInfoProvider.GetName(model, strRef);
        //        string baseName = StructureInfoProvider.GetBCBaseName(sdtLevelFullName);
        //        SDT sdt = GetSDTObject(model, baseName);
        //        if (sdtLevelFullName == baseName)
        //        {
        //            source += "//For Each ?\r\n";
        //            collection = true;
        //        }
        //        string itemString = collection ? "Item" : "";
        //        foreach (GxuParm parm in GetAtt(sdt.Name))
        //        {
        //            if (parm.IsSimple())
        //                source += "&" + varName + itemString + "." + parm.VariableName + " = " + parm.DefaultValue() + "\r\n";
        //            else
        //                source += "//&" + varName + itemString + "." + parm.VariableName + " = \r\n";
        //        }
        //        if (collection)
        //        {
        //            source += "&" + varName + ".add(&" + varName + "Item)\r\n";
        //            source += "//&" + varName + "Item = new " + sdtLevelFullName + "." + sdt.SDTStructure.Root.CollectionItemName +  " ()\r\n";
        //            source += "//EndFor\r\n";
        //        }

        //        return source;
        //    }
        //    catch
        //    {
        //        return "//&" + var.Name + " =\r\n";
        //    }
        //}

        public bool IsCollection(Variable var)
        {
            AttCustomType type = var.GetPropertyValue<AttCustomType>(Properties.ATT.DataType);
            StructureTypeReference strRef = StructureTypeReference.Deserialize(type);
            string sdtLevelFullName = StructureInfoProvider.GetName(model, strRef);
            string baseName = StructureInfoProvider.GetBCBaseName(sdtLevelFullName);
            return (sdtLevelFullName == baseName);
        }

        public string GetItemType(Variable var)
        {
            AttCustomType type = var.GetPropertyValue<AttCustomType>(Properties.ATT.DataType);
            StructureTypeReference strRef = StructureTypeReference.Deserialize(type);
            string sdtLevelFullName = StructureInfoProvider.GetName(model, strRef);
            string baseName = StructureInfoProvider.GetBCBaseName(sdtLevelFullName);
            SDT sdt = GetSDTObject(model, baseName);
            if (sdt.SDTStructure.Root.CollectionItemName != null)
            {
                return sdtLevelFullName + "." + sdt.SDTStructure.Root.CollectionItemName;
            }
            else
            {
                return sdtLevelFullName;
            }
        }

        public string GetSDTType(Variable var)
        {
            AttCustomType type = var.GetPropertyValue<AttCustomType>(Properties.ATT.DataType);
            StructureTypeReference strRef = StructureTypeReference.Deserialize(type);
            return StructureInfoProvider.GetName(model, strRef);
        }

        
        public static SDT GetSDTObject(KBModel model, string name)
        {
            // Esto depende de la version de la BL de GeneXus.
#if GXTILO
            // Para Tilo:
            return SDT.Get(model, new QualifiedName(name));
#else
            // Para Ev1 y Ev2:
            return SDT.Get(model, name);
#endif
        }
    }
}
