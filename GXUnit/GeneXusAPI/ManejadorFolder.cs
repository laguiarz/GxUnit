using Artech.Architecture.Common.Objects;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    class ManejadorFolder
    {
        private KBModel model;
        //private static ManejadorFolder instance = new ManejadorFolder();

        public ManejadorFolder()
        {
            this.model = ManejadorContexto.Model;
        }

        public KBModel GetModel()
        {
            return model;
        }

        public bool CrearFolder(DTFolder folder, bool force)
        {
            String msgoutput;

            Folder fold = GetFolderObject(this.model, folder.GetNombre());
            if (fold != null && !force)
            {
                msgoutput = "Folder " + folder.GetNombre() + " already exists!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
                return false;
            }
            else
            {
                if (fold == null)
                    fold = new Folder(model);
                fold.Name = folder.GetNombre();
                Folder foldPadre = GetFolderObject(this.model, folder.GetNombrePadre());
                if (foldPadre != null)
                    fold.Parent = foldPadre;
                else
                    fold.Parent = GetRootFolder(model);
            }

            fold.Save();

            msgoutput = "Folder " + folder.GetNombre() + " created!";
            FuncionesAuxiliares.EscribirOutput(msgoutput);
            return true;
        }

        public bool EliminarFolder(DTFolder folder)
        {
            String msgoutput;
            Folder fold = GetFolderObject(this.model, folder.GetNombre());
            if (fold != null)
            {
                EliminarHijos(folder.GetNombre());
                fold.Delete();
                msgoutput = "Folder " + folder.GetNombre() + " deleted!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
                return true;
            }
            else
            {
                msgoutput = "Folder " + folder.GetNombre() + " does not exists!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
                return false;
            }
        }

        public void EliminarHijos(String folder)
        {
            foreach (KBObject pr in model.Objects.GetAll())
            {
                if (pr is Folder)
                {
                    if ((((Folder)pr).Parent != null) && (((Folder)pr).Parent.Name == folder))
                        EliminarFolder(GetDTFolder((Folder)pr));
                }
            }
        }

        public Folder GetFolder(String folderName)
        {
            //String msgoutput;
            Folder fold = GetFolderObject(this.model, folderName);
            if (fold == null)
            {
                //msgoutput = "Folder " + folderName + " does not exists!";
                //FuncionesAuxiliares.EscribirOutput(msgoutput);
                return null;
            }
            return fold;
        }

        public DTFolder GetDTFolder(Folder fold)
        {
            return new DTFolder(fold.Name, fold.Parent.Name);
        }

        public static Folder GetFolderObject(KBModel model, string fullName)
        {
            // Esto depende de la version de la BL de GeneXus.
#if GXTILO
            // Para Tilo:
            return Folder.Get(model, new QualifiedName(fullName));
#else
            // Para Ev1 y Ev2:
            return Folder.Get(model, fullName);
#endif
        }

        public static KBObject GetRootFolder(KBModel model)
        {
#if GXTILO
            // Para Tilo:
            return Module.GetRoot(model);
#else
            // Para Ev1 y Ev2:
            return Folder.GetRoot(model);
#endif
        }
    }
}
