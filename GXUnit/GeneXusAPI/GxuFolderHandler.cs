/*
 * 2017-03-27   LAZ Renamed stuff for language consistency.
 *                  Changed GetParentName function to get/set properties
 * */

using Artech.Architecture.Common.Objects;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using System;

namespace PGGXUnit.Packages.GXUnit.GeneXusAPI
{
    class GxuFolderHandler
    {
        private KBModel model;

        public GxuFolderHandler()
        {
            this.model = ContextHandler.Model;
        }

        public KBModel GetModel()
        {
            return model;
        }

        public bool CreateFolder(GxuFolder gxufolder, bool force)
        {
            String msgoutput;

            Folder folder = GetFolderObject(this.model, gxufolder.Name);
            if (folder != null && !force)
            {
                msgoutput = "Folder " + gxufolder.Name + " already exists!";
                GxHelper.WriteOutput(msgoutput);
                return false;
            }
            else
            {
                if (folder == null)
                    folder = new Folder(model);
                folder.Name = gxufolder.Name;

                Folder parentFolder = GetFolderObject(this.model, gxufolder.ParentName);
                if (parentFolder != null)
                    folder.Parent = parentFolder;
                else
                    folder.Parent = GetRootFolder(model);
            }

            folder.Save();

            msgoutput = "Folder " + gxufolder.Name + " created!";
            GxHelper.WriteOutput(msgoutput);
            return true;
        }

        public bool DeleteFolder(GxuFolder gxuFolder)
        {
            String msgoutput;
            Folder folder = GetFolderObject(this.model, gxuFolder.Name);
            if (folder != null)
            {
                try
                {
                    DeleteChildren(gxuFolder.Name);
                    folder.Delete();
                    msgoutput = "Folder " + gxuFolder.Name + " deleted!";
                    GxHelper.WriteOutput(msgoutput);
                }
                catch (Exception e)
                {
                    msgoutput = "Failed to delete Folder " + gxuFolder.Name ;
                    GxHelper.WriteOutput(msgoutput);
                    msgoutput = e.Message;
                    GxHelper.WriteOutput(msgoutput);

                }
                return true;
            }
            else
            {
                msgoutput = "Folder " + gxuFolder.Name + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
                return false;
            }
        }

        public void DeleteChildren(String folderName)
        {
            foreach (KBObject obj in model.Objects.GetAll())
            {
                if (obj is Folder)
                {
                    if ((((Folder)obj).Parent != null) && (((Folder)obj).Parent.Name == folderName))
                        DeleteFolder(GetGxuFolder((Folder)obj));
                }
            }
        }

        public Folder GetFolder(String folderName)
        {
            Folder folder = GetFolderObject(this.model, folderName);
            if (folder == null)
            {
                String msgoutput = "Folder " + folderName + " does not exists!";
                GxHelper.WriteOutput(msgoutput);
                return null;
            }
            return folder;
        }

        public GxuFolder GetGxuFolder(Folder folder)
        {
            return new GxuFolder(folder.Name, folder.Parent.Name);
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
