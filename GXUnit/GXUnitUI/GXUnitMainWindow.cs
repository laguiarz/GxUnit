using Artech.Architecture.Common.Objects;
using Artech.Architecture.UI.Framework.Packages;
using Artech.Architecture.UI.Framework.Services;
using Artech.Common.Framework.Selection;
using Artech.FrameworkDE;
using Artech.Genexus.Common.Objects;
using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using PGGXUnit.Packages.GXUnit.GXUnitCore;
using PGGXUnit.Packages.GXUnit.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PGGXUnit.Packages.GXUnit.GXUnitUI
{
    [Guid("CC753F38-8B9F-49B8-95D0-4BDF6D9CDB4D")]
    public partial class GXUnitMainWindow : AbstractToolWindow, ISelectionListener // */UserControl
    {
        public static Guid guid = typeof(GXUnitMainWindow).GUID;
        private static GXUnitMainWindow instance = new GXUnitMainWindow();
        private TreeNode auxNode = null;
        private bool firstCheck = true;
        private bool checking = false;

        private GXUnitMainWindow()
        {
            InitializeComponent();

            ImageList myImageList = new ImageList();
            myImageList.Images.Add(Items.TestCaseIconMini);
            myImageList.Images.Add(Items.TreeViewTestSuite);
            myImageList.ColorDepth = ColorDepth.Depth32Bit;
            this.SuitesTree.ImageList = myImageList;

            this.SuitesTree.ExpandAll();
        }


        public static GXUnitMainWindow getInstance()
        {
            return instance;
        }


        public bool OnSelectChange(ISelectionContainer sc)
        {
            if (!string.IsNullOrEmpty(ManejadorContexto.KBName) && ManejadorContexto.Model != null)
            {
                try
                {
                    this.cargarNodosTest();
                }
                catch(Exception e)
                {
                    FuncionesAuxiliares.EscribirOutput(e.StackTrace);
                }
            }
            return true;
        }


        private void SuitesTree_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            TreeNode n = this.SuitesTree.GetNodeAt(e.Location);
            if (n == null)
            {
                // Clearing Menu Options
                this.SuitesTreeCMenu.Items.Clear();
                this.SuitesTreeCMenu.Items.Add(this.nuevoTestCaseToolStripMenuItem);
                this.SuitesTreeCMenu.Items.Add(this.nuevoToolStripMenuItem);
                instance.auxNode = null;
            }
            else
            {
                Rectangle rec = n.Bounds;
                rec.Width = rec.Width + 18;
                rec.Offset(-18, 0);

                if (!rec.Contains(e.Location))
                {
                    // Clearing Menu Options
                    this.SuitesTreeCMenu.Items.Clear();
                    this.SuitesTreeCMenu.Items.Add(this.nuevoTestCaseToolStripMenuItem);
                    this.SuitesTreeCMenu.Items.Add(this.nuevoToolStripMenuItem);
                    instance.auxNode = null;
                }
            }
        }


        private void SuitesTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Clearing Menu Options
                this.SuitesTreeCMenu.Items.Clear();

                // Adding Options
                if (e.Node != null)
                {
                    Rectangle rec = e.Node.Bounds;
                    rec.Width = rec.Width + 18;
                    rec.Offset(-18, 0);

                    if (!rec.Contains(e.Location))
                    {
                        // Clearing Menu Options
                        this.SuitesTreeCMenu.Items.Clear();
                        this.SuitesTreeCMenu.Items.Add(this.nuevoTestCaseToolStripMenuItem);
                        this.SuitesTreeCMenu.Items.Add(this.nuevoToolStripMenuItem);
                        instance.auxNode = null;
                    }
                    else
                    {
                        if (e.Node.Tag.Equals("TestSuite"))
                        {
                            this.SuitesTreeCMenu.Items.Add(this.nuevoTestCaseToolStripMenuItem);
                            this.SuitesTreeCMenu.Items.Add(this.nuevoToolStripMenuItem);
                        }

                        auxNode = e.Node;

                        // Adding Delete At Last to Order Items
                        this.SuitesTreeCMenu.Items.Add(this.eliminarToolStripMenuItem);
                    }
                }
                else
                {
                    this.SuitesTreeCMenu.Items.Add(this.nuevoTestCaseToolStripMenuItem);
                    this.SuitesTreeCMenu.Items.Add(this.nuevoToolStripMenuItem);
                    instance.auxNode = null;
                }
            }
            else
            {
                auxNode = e.Node;
            }
        }


        private void SuitesTree_NodeMouseDoubleClick(object sender, EventArgs e)
        {
            if ((instance.auxNode != null) && (((string)(instance.auxNode.Tag)).Equals("TestCase")))
            {
                try
                {
                    KBObject test = ManejadorObjeto.GetInstance().GetKBObjectTest(instance.auxNode.Text);
                    UIServices.Objects.Open(test, OpenDocumentOptions.CurrentVersionPart(test.Guid, null));
                }
                catch { }
            }
        }

        private void NuevaSuite()
        {
            KBModel kbmodel = ManejadorContexto.Model;

            TreeNode node = instance.auxNode;
            instance.auxNode = null;
            TestSuite suite;
            string suiteName = InputBox.Show("Please enter the Test Suite Name", "Name:");
            if (!suiteName.Equals(""))
            {
                TreeNode suiteNode = new TreeNode(suiteName, 1, 1);
                suiteNode.Tag = "TestSuite";
                string parentFolder = "GXUnitSuites";
                try
                {
                    if (node != null)
                    {
                        parentFolder = node.Text;
                        suite = new TestSuite(suiteName, parentFolder);
                        node.Nodes.Insert(SuitesTree.Nodes.Count, suiteNode);
                        node.Expand();
                        node.TreeView.SelectedNode = null;
                    }
                    else
                    {
                        suite = new TestSuite(suiteName, parentFolder);
                        SuitesTree.Nodes.Insert(SuitesTree.Nodes.Count, suiteNode);
                    }

                    SuitesTree.Invalidate();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.GetBaseException().Message);

                }
            }
        }

        public void AddSuiteToTree(TreeNode suiteNode)
        {
            SuitesTree.Nodes.Insert(SuitesTree.Nodes.Count, suiteNode);
            SuitesTree.Invalidate();
        }

        private void nuevaTestSuiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ManejadorContexto.GXUnitInicializado)
            {
                if (MessageBox.Show("Create GXUnit Objects?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    GXUnitInicializador i = GXUnitInicializador.GetInstance();
                    i.InicializarGXUnit();
                    ManejadorContexto.GXUnitInicializado = true;
                    NuevaSuite();
                }
            }
            else
            {
                NuevaSuite();
            }
        }

        private void nuevoTestCaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ManejadorContexto.GXUnitInicializado)
            {
                if (MessageBox.Show("Create GXUnit Objects?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    GXUnitInicializador i = GXUnitInicializador.GetInstance();
                    i.InicializarGXUnit();
                    ManejadorContexto.GXUnitInicializado = true;
                    NuevoTC();
                }
            }
            else
            {
                NuevoTC();
            }
        }

        private void NuevoTC()
        {
            KBModel kbmodel = ManejadorContexto.Model;

            Folder f = null;
            if (auxNode == null)
            {
                f = ManejadorFolder.GetFolderObject(kbmodel, "GXUnitSuites");
            }
            else
            {
                f = ManejadorFolder.GetFolderObject(kbmodel, auxNode.Text);
            }
#if GXTILO
          //  CreateObjectOptions options = new CreateObjectOptions();
            //options.Folder = f;
           // KBObject o = UIServices.NewObjectDialog.CreateObject(options);
#else
            KBObject o = UIServices.NewObjectDialog.CreateObject(f);
#endif
        }

        public void agregarTestCase(TestCase test, string nodeName)
        {
            String testName = test.Name;
            TreeNode testNode = new TreeNode(testName, 0, 0);

            testNode.Tag = "TestCase";
            testNode.Name = test.Name;

            if ((auxNode == null) || (auxNode.Name != nodeName))
            {
                auxNode = buscarNodo(nodeName, SuitesTree.Nodes);
            }
            if (auxNode != null)
            {
                if (!auxNode.Nodes.ContainsKey(testName))
                    auxNode.Nodes.Add(testNode);

                auxNode.Expand();
                auxNode.TreeView.SelectedNode = auxNode;
                auxNode = null;
                SuitesTree.Invalidate();
            }
        }

        private TreeNode buscarNodo(string nodeName, TreeNodeCollection colection)
        {
            TreeNode result = null;
            foreach (TreeNode n in colection)
            {
                if (((string)(n.Tag)).Equals("TestSuite"))
                {
                    if (n.Text == nodeName)
                        result = n;
                    else
                    {
                        result = buscarNodo(nodeName, n.Nodes);
                    }

                    if (result != null)
                        break;
                }
            }
            return result;
        }


        private void eliminarTestSuiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string msgoutput = null;
            // Removing GeneXus Object
            KBObject o = ManejadorObjeto.GetInstance().GetKBObject(instance.auxNode.Text);
            if (o == null)
            {
                msgoutput = "Object " + instance.auxNode.Text + " does not exists!";
                FuncionesAuxiliares.EscribirOutput(msgoutput);
            }
            else
            {
                if (((string)(instance.auxNode.Tag)).Equals("TestCase"))
                    o.Delete();
                else
                    borrarSuite(o);

                SuitesTree.Nodes.Remove(instance.auxNode);
                instance.auxNode = null;
                SuitesTree.Invalidate();
            }
        }

        private void checkSelectAll_Click(object sender, EventArgs e)
        {
            foreach (TreeNode n in this.SuitesTree.Nodes)
            {
                checkSuite(n, this.checkSelectAll.Checked);
            }
        }


        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (UIServices.KB.CurrentKB != null)
            {
                cargarNodosTest();
                this.SuitesTree.ExpandAll();
            }
        }
        

        private void checkSuite(TreeNode node, bool check)
        {
            node.Checked = check;
            foreach (TreeNode n in node.Nodes)
            {
                n.Checked = check;
                if (((string)(n.Tag)).Equals("TestSuite"))
                    checkSuite(n, check);
            }
        }


        private void SuitesTree_AfterCheck(object sender, EventArgs e)
        {
            if (!checking)
            {
                TreeNode node = ((TreeViewEventArgs)(e)).Node;
                bool cerrar = false;

                if (firstCheck)
                {
                    firstCheck = false;
                    cerrar = true;

                    foreach (TreeNode n in node.Nodes)
                    {
                        n.Checked = node.Checked;
                    }

                    seleccionarPadres(node);
                }
                else
                {
                    foreach (TreeNode n in node.Nodes)
                    {
                        n.Checked = node.Checked;
                    }
                }

                if (cerrar)
                    firstCheck = true;
            }
        }


        private void seleccionarPadres(TreeNode node)
        {
            checking = true;
            TreeNode parentNode = null;
            if (node.Checked)
            {
                parentNode = node.Parent;
                while (parentNode != null)
                {
                    parentNode.Checked = true;
                    parentNode = parentNode.Parent;
                }
            }
            else
            {
                bool masSeleccionados = false;
                parentNode = node.Parent;
                if (parentNode != null)
                {
                    foreach (TreeNode n in parentNode.Nodes)
                    {
                        if (n.Checked)
                        {
                            masSeleccionados = true;
                            break;
                        }
                    }

                    if (!masSeleccionados)
                    {
                        parentNode.Checked = false;
                        seleccionarPadres(parentNode);
                    }
                }
            }
            checking = false;
        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            if (UIServices.KB.CurrentKB != null)
            {
                if (!ManejadorContexto.GXUnitInicializado)
                {
                    if (MessageBox.Show("Create GXUnit Objects?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        GXUnitInicializador i = GXUnitInicializador.GetInstance();
                        i.InicializarGXUnit();
                        ManejadorContexto.GXUnitInicializado = true;
                        Test();
                    }
                }
                else
                {
                    Test();
                }
            }
            else
            {
                MessageBox.Show("Open a knowledge base first.", "GXUnit");
            }
            
        }

        private void Test()
        {
            ManejadorRunner m = ManejadorRunner.GetInstance();
            LinkedList<DTTestCase> tests = generarTestsSeleccionados();
            if (testsSelected(tests))
            {
                if (testsSaved(tests))
                {
                    string XMLPath;
                    m.CrearRunner(tests, out XMLPath);
                    ManejadorLenguaje.SetLenguajeModelo();

                    //si hay que forzar la generacion del runner solo lo genero(no lo ejecuto, se ejecuta en el evento after build)
                    if (ManejadorLenguaje.Lenguaje == Artech.Genexus.Common.Entities.GeneratorType.CSharpWeb)
                    {
                        m.GeneraRunner();
                    }
                    else if (ManejadorLenguaje.Lenguaje == Artech.Genexus.Common.Entities.GeneratorType.JavaWeb)
                    {
                        //si no hay que forzar mando ejecutar 
                        m.EjecutarRunner();
                    }
                }
                else
                {
                    MessageBox.Show("Please save the test case before continuing", "GXUnit");
                }
            }
            else
            {
                MessageBox.Show("Select at least one test case", "GXUnit");
            }
        }

        private bool testsSelected(LinkedList<DTTestCase> tests)
        {
            foreach (DTTestCase item in tests)
            {
                // No es una Suite, es un Test Case
                if (!item.GetSuite())
                {
                    return true;
                }
            }
            return false;
        }

        private bool testsSaved(LinkedList<DTTestCase> tests)
        {
            foreach (DTTestCase item in tests)
            {
                if (!item.GetSuite())
                {
                    Procedure proc = ManejadorProcedimiento.GetProcedureObject(ManejadorContexto.Model, item.GetNombre());
                    if (proc == null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private LinkedList<DTTestCase> generarTestsSeleccionados()
        {
            LinkedList<DTTestCase> Tests = new LinkedList<DTTestCase>();
            agregarTestCasesSeleccionados(Tests, SuitesTree.Nodes);
            return Tests;
        }

        private void agregarTestCasesSeleccionados(LinkedList<DTTestCase> lista, TreeNodeCollection nodes)
        {
            String parentname;
            foreach (TreeNode n in nodes)
            {
                if (n.Parent != null)
                    parentname = n.Parent.Text;
                else 
                    parentname = "GXUnitSuites";

                if (n.Checked)
                {
                    if (((string)(n.Tag)).Equals("TestCase"))
                        lista.AddLast(new DTTestCase(n.Text,parentname,false));

                    if (((string)(n.Tag)).Equals("TestSuite"))
                        lista.AddLast(new DTTestCase(n.Text,parentname,true));
                }
                
                agregarTestCasesSeleccionados(lista, n.Nodes);
            }         
        }

        private void uncheckNodes(TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                n.Checked = false;
                uncheckNodes(n);
            }
        }

        public override Icon Icon
        {
            get
            {
                return Items.GXUnitIconMini;
            }
        }

        public void nuevoNodoTest(string test)
        {
            bool existe = false;
            foreach (TreeNode n in this.SuitesTree.Nodes)
            {
                if (n.Text == test)
                {
                    existe = true;
                }
            }
            if (!existe)
            {
                int count = this.SuitesTree.Nodes.Count;
                this.SuitesTree.Nodes.Insert(count + 1, test);
            }
        }

        public void limpiarNodosTest()
        {
            this.SuitesTree.Nodes.Clear();
        }

        public void cargarNodosTest()
        {
            this.SuitesTree.Nodes.Clear();
            int count = 0;
            TreeNode auxNode = null;

            ManejadorFolder mf = new ManejadorFolder();
            Folder folder = mf.GetFolder("GXUnitSuites");
            if (folder != null)
            {
                foreach (Folder f in folder.SubFolders)
                {
                    auxNode = new TreeNode();
                    auxNode.Name = f.Name;
                    auxNode.Text = f.Name;
                    auxNode.Tag = "TestSuite";
                    auxNode.ImageIndex = 1;
                    auxNode.SelectedImageIndex = 1;
                    this.SuitesTree.Nodes.Insert(count, auxNode);
                    cargarFolder(auxNode, f);
                    count++;
                }
            }

            this.SuitesTree.Sort();

            this.checkSelectAll.Checked = false;
        }

        public void cargarFolder(TreeNode node, Folder folder)
        {
            TreeNode auxNode = null;
            int count = 0;
            foreach (Folder f in folder.SubFolders)
            {
                auxNode = new TreeNode();
                auxNode.Name = f.Name;
                auxNode.Text = f.Name;
                auxNode.Tag = "TestSuite";
                auxNode.ImageIndex = 1;
                auxNode.SelectedImageIndex = 1;
                node.Nodes.Insert(count, auxNode);
                cargarFolder(auxNode, f);
                count++;
            }
            foreach (KBObject o in folder.Objects)
            {
                if (o.GetType() != folder.GetType())
                {
                    auxNode = new TreeNode();
                    auxNode.Name = o.Name;
                    auxNode.Text = o.Name;

                    auxNode.Tag = "TestCase";
                    auxNode.ImageIndex = 0;
                    auxNode.SelectedImageIndex = 0;

                    node.Nodes.Insert(count, auxNode);
                    count++;
                }
            }
        }

        private void borrarSuite(KBObject o)
        {
            Folder folder = ((Folder)o);
            foreach (Folder f in folder.SubFolders)
            {
                borrarSuite(f);
            }
            foreach (KBObject obj in folder.Objects)
            {
                obj.Delete();
            }
            folder.Delete();
        }


        public bool isSuitesFolderChild(Folder folder)
        {
            if (folder.Parent == null)
                return false;
            if (folder.Parent.Name == "GXUnitSuites")
                return true;
            if (folder.Parent is Folder)
                return isSuitesFolderChild(folder.Parent as Folder);

                return false;
        }

        public List<TreeNode> getListaSuites()
        {
            List<TreeNode> listaSuites = new List<TreeNode>();
            foreach(TreeNode n in this.SuitesTree.Nodes)
            {
                listaSuites.Add(n);
            }

            return listaSuites;
        }

        private void SuitesTree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        private void SuitesTree_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void SuitesTree_DragOver(object sender, DragEventArgs e)
        {
            Point targetPoint = SuitesTree.PointToClient(new Point(e.X, e.Y));
            SuitesTree.SelectedNode = SuitesTree.GetNodeAt(targetPoint);
        }

        private void SuitesTree_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(TreeNode)))
            {
                Point targetPoint = SuitesTree.PointToClient(new Point(e.X, e.Y));
                TreeNode targetNode = SuitesTree.GetNodeAt(targetPoint);
                TreeNode draggedNode = (TreeNode)e.Data.GetData(typeof(TreeNode));
                if (targetNode != null && !draggedNode.Equals(targetNode) && !ContainsNode(draggedNode, targetNode) && targetNode.Tag.Equals("TestSuite"))
                {
                    try
                    {
                        KBModel model = ManejadorContexto.Model;
                        Folder DestinationFolder = ManejadorFolder.GetFolderObject(model, targetNode.Text);

                        KBObject obj = ManejadorObjeto.GetInstance().GetKBObject(draggedNode.Text);
                        obj.Parent = DestinationFolder;
                        obj.Dirty = true;
                        obj.Save();

                        targetNode.Nodes.Add((TreeNode)draggedNode.Clone());
                        targetNode.Expand();

                        //Remove Original Node
                        draggedNode.Remove();

                        // Uncheck All
                        foreach (TreeNode n in this.SuitesTree.Nodes)
                        {
                            n.Checked = false;
                            uncheckNodes(n);
                        }
                    }
                    catch (Exception exc)
                    {
                        FuncionesAuxiliares.EscribirOutput(exc.Message);
                    }
                }
            }
        }

        private bool ContainsNode(TreeNode node1, TreeNode node2)
        {
            if (node2.Parent == null) return false;
            if (node2.Parent.Equals(node1)) return true;
            return ContainsNode(node1, node2.Parent);
        }
    }
}
