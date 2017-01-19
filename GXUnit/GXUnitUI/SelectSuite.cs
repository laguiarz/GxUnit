using PGGXUnit.Packages.GXUnit.GeneXusAPI;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PGGXUnit.Packages.GXUnit.GXUnitUI
{

    public partial class SelectSuite : Form
    {

        public string selectedSuite = "";


        public SelectSuite()
        {
            InitializeComponent();
        }

        public SelectSuite(List<TreeNode> suites)
        {
            InitializeComponent();

            // Agregamos las suites al TreeView
            TreeNode auxNode = null;
            foreach (TreeNode n in suites)
            {
                auxNode = (TreeNode)n.Clone();
                auxNode.Nodes.Clear();
                auxNode.ImageIndex = 0;
                auxNode.SelectedImageIndex = 0;
                SuitesTree.Nodes.Add(auxNode);
                AddOnlySuites(auxNode, n.Nodes);
            }

            this.SuitesTree.ExpandAll();

            ShowDialog();
        }


        private void AddOnlySuites(TreeNode node, TreeNodeCollection collection)
        {
            TreeNode auxNode = null;
            foreach (TreeNode n in collection)
            {
                if (n.Tag.Equals((string)("TestSuite")))
                {
                    auxNode = (TreeNode)(n.Clone());
                    auxNode.Nodes.Clear();
                    auxNode.ImageIndex = 0;
                    auxNode.SelectedImageIndex = 0;
                    node.Nodes.Add(auxNode);
                    AddOnlySuites(auxNode, n.Nodes);
                }
            }
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (textBoxNewSuite.Text == "")
            {
                MessageBox.Show("Please provide a name for the suite");
            }
            else
            {
                try
                {
                    TestSuite suite = new TestSuite(textBoxNewSuite.Text, "GXUnitSuites");
                    TreeNode suiteNode = new TreeNode(textBoxNewSuite.Text, 1, 1);
                    suiteNode.Tag = "TestSuite";

                    GXUnitMainWindow.getInstance().AddSuiteToTree(suiteNode);

                    this.selectedSuite = textBoxNewSuite.Text;
                    this.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
