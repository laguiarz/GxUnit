using System.Windows.Forms;
using PGGXUnit.Packages.GXUnit.Utils;
namespace PGGXUnit.Packages.GXUnit.GXUnitUI
{
    partial class SelectSuite
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectSuite));
            this.button2 = new System.Windows.Forms.Button();
            this.SuitesTree = new System.Windows.Forms.TreeView();
            this.myImageList = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxNewSuite = new System.Windows.Forms.TextBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(197, 216);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Accept";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // SuitesTree
            // 
            this.SuitesTree.ImageIndex = 0;
            this.SuitesTree.ImageList = this.myImageList;
            this.SuitesTree.Location = new System.Drawing.Point(13, 13);
            this.SuitesTree.Name = "SuitesTree";
            this.SuitesTree.SelectedImageIndex = 0;
            this.SuitesTree.Size = new System.Drawing.Size(259, 197);
            this.SuitesTree.TabIndex = 3;
            this.SuitesTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SuitesTree_NodeMouseDoubleClick);
            // 
            // myImageList
            // 
            this.myImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("myImageList.ImageStream")));
            this.myImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.myImageList.Images.SetKeyName(0, "TestSuite.ico");
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(13, 242);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 2);
            this.label1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 252);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Or create new";
            // 
            // textBoxNewSuite
            // 
            this.textBoxNewSuite.Location = new System.Drawing.Point(13, 268);
            this.textBoxNewSuite.Name = "textBoxNewSuite";
            this.textBoxNewSuite.Size = new System.Drawing.Size(179, 20);
            this.textBoxNewSuite.TabIndex = 6;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(197, 265);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.TabIndex = 7;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // SelectSuite
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 301);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.textBoxNewSuite);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.SuitesTree);
            this.Controls.Add(this.button2);
            this.Name = "SelectSuite";
            this.Text = "Select a Suite";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void button2_Click(object sender, System.EventArgs e)
        {
            TreeNode selectedNode = this.SuitesTree.SelectedNode;
            if (selectedNode == null)
            {
                MessageBox.Show("Please select a suite to continue");
            }
            else
            {
                this.selectedSuite = selectedNode.Text;
                this.Dispose();
            }
        }

        void SuitesTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.selectedSuite = e.Node.Text;
            this.Dispose();
        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TreeView SuitesTree;
        private ImageList myImageList;
        private Label label1;
        private Label label2;
        private TextBox textBoxNewSuite;
        private Button buttonCreate;
    }
}