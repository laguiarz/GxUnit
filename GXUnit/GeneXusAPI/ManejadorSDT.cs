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
    class ManejadorSDT
    {
        private KBModel model;

        public ManejadorSDT()
        {
            this.model = ManejadorContexto.Model;
        }

        public KBModel GetModel()
        {
            return model;
        }

        public void SetModel(KBModel m)
        {
            model = m;
        }

        public bool CrearSDT(SDTipo sdtipo, bool force)
        {
            String msgoutput;
            SDT sdt = GetSDTObject(model, sdtipo.GetNombre());
            if (sdt == null || force)
            {
                if (sdt == null)
                    sdt = new SDT(model);
                sdt.Name = sdtipo.GetNombre();
                //Agrego propiedades
                foreach (DTPropiedad p in sdtipo.GetPropiedades())
                {
                    //FuncionesAuxiliares.EscribirOutput(p.GetNombre());
                    sdt.SetPropertyValue(p.GetNombre(), p.GetValor());
                }

                Folder foldPadre = ManejadorFolder.GetFolderObject(this.model, sdtipo.GetPadre());
                if (foldPadre != null)
                    sdt.Parent = foldPadre;
                else
                    sdt.Parent = ManejadorFolder.GetRootFolder(model);
                SDTLevel n = sdt.SDTStructure.Root;
                n.Items.Clear();
                n.IsCollection = sdtipo.GetRoot().EsColeccion();
                n.CollectionItemName = sdtipo.GetRoot().GetNombre();
                
                foreach (SDTipoNivelItem i in sdtipo.GetRoot().GetItems())
                {
                    eDBType tipo = FuncionesAuxiliares.GetTipoGX(i.GetTipo());
                    n.AddItem(i.GetNombre(), tipo, i.GetLongitud());
                }

                SDTLevel n2;
                SDTItem item;
                //Agrego Niveles
                foreach (SDTipoNivel nivel in sdtipo.GetNiveles())
                {
                    n2 = n.AddLevel(nivel.GetNombre() + 's', nivel.GetNombre());
                    n2.IsCollection = nivel.EsColeccion();
                    n2.Items.Clear();
                                                           
                    foreach (SDTipoNivelItem i in nivel.GetItems())
                    {
                        eDBType tipo = FuncionesAuxiliares.GetTipoGX(i.GetTipo());
                        item = n2.AddItem(i.GetNombre(), tipo, i.GetLongitud());
                        if (i.GetTipo() == Constantes.Tipo.SDT)
                        {
                            DataType.ParseInto(model, i.GetTipoCompuesto(), item);
                        }
                        sdt.SDTStructure.Dirty = true;

                    }
                    sdt.Save();
                }
                
                msgoutput = "SDT Object " + sdtipo.GetNombre() + " created!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
                return true;
            }
            msgoutput = "SDT Object " + sdtipo.GetNombre() + " already exists!";
            FuncionesAuxiliares.EscribirOutput(msgoutput);
            return false;
        }

        public bool EliminarSDT(SDTipo sdttipo)
        {
            String msgoutput;

            SDT sdt = GetSDTObject(this.model, sdttipo.GetNombre());
            if (sdt != null)
            {
                sdt.Delete();
                msgoutput = "SDT Object " + sdttipo.GetNombre() + " deleted!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
                return true;
            }
            else
            {
                msgoutput = "SDT Object " + sdttipo.GetNombre() + " does not exists!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
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
                sdt = SDT.Get(ManejadorContexto.Model, qn);
            }
            else
            {
                QualifiedName qn = new QualifiedName(sdtName);
                sdt = SDT.Get(ManejadorContexto.Model, qn);
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

        public LinkedList<Parametro> GetAtt(String sdtName)
        {
            LinkedList<Parametro> campos = new LinkedList<Parametro>();
            SDT sdt = GetSDTObject(ManejadorContexto.Model, sdtName);
            if (sdt != null)
            {
                foreach (SDTItem item in sdt.SDTStructure.Root.Items)
                {
                    if (FuncionesAuxiliares.isSimple(item.Type))
                        campos.AddLast(new Parametro(item.Name, item.Type));
                    if (item.Type == eDBType.GX_SDT)
                    {
                        AttCustomType type = item.GetPropertyValue<AttCustomType>(Artech.Genexus.Common.Properties.ATT.DataType);
                        if (type.DataType == (int)eDBType.GX_SDT)
                        {
                            StructureTypeReference strRef = StructureTypeReference.Deserialize(type);
                            string sdtLevelFullName = StructureInfoProvider.GetName(model, strRef);
                            campos.AddLast(new Parametro(item.Name, sdtLevelFullName, Constantes.PARM_INOUT, Constantes.Estructurado.SDT, item.IsCollection, false));
                        }
                    }
                    /*if (item.Type == eDBType.GX_BUSCOMP)
                        campos.AddLast(new Parametro(item.Name, "", Constantes.PARM_INOUT, Constantes.Estructurado.BC, item.IsCollection, false));*/
                }
            }
            return campos;
        }

        public string getSDTTSourceForProcedure(Variable var, string prefix)
        {
            bool collection = false;
            string varName = var.Name + prefix;
            try
            {
                string source = "";
                AttCustomType type = var.GetPropertyValue<AttCustomType>(Properties.ATT.DataType);
                StructureTypeReference strRef = StructureTypeReference.Deserialize(type);
                string sdtLevelFullName = StructureInfoProvider.GetName(model, strRef);
                string baseName = StructureInfoProvider.GetBCBaseName(sdtLevelFullName);
                SDT sdt = GetSDTObject(model, baseName);
                if (sdtLevelFullName == baseName)
                {
                    source += "//For Each ?\r\n";
                    collection = true;
                }
                string itemString = collection ? "Item" : "";
                foreach (Parametro parm in GetAtt(sdt.Name))
                {
                    if (parm.isSimple())
                        source += "&" + varName + itemString + "." + parm.GetVarName() + " = " + parm.defaultValue() + "\r\n";
                    else
                        source += "//&" + varName + itemString + "." + parm.GetVarName() + " = \r\n";
                }
                if (collection)
                {
                    source += "&" + varName + ".add(&" + varName + "Item)\r\n";
                    source += "//&" + varName + "Item = new " + sdtLevelFullName + "." + sdt.SDTStructure.Root.CollectionItemName +  " ()\r\n";
                    source += "//EndFor\r\n";
                }

                return source;
            }
            catch
            {
                return "//&" + var.Name + " =\r\n";
            }
        }

        public bool isColl(Variable var)
        {
            AttCustomType type = var.GetPropertyValue<AttCustomType>(Properties.ATT.DataType);
            StructureTypeReference strRef = StructureTypeReference.Deserialize(type);
            string sdtLevelFullName = StructureInfoProvider.GetName(model, strRef);
            string baseName = StructureInfoProvider.GetBCBaseName(sdtLevelFullName);
            return (sdtLevelFullName == baseName);
        }

        public string getItemType(Variable var)
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

        public string getSDTType(Variable var)
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
