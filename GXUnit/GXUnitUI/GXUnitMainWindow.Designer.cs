using PGGXUnit.Packages.GXUnit.GeneXusAPI;
namespace PGGXUnit.Packages.GXUnit.GXUnitUI
{
    partial class GXUnitMainWindow
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.SuitesTree = new System.Windows.Forms.TreeView();
            this.SuitesTreeCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkSelectAll = new System.Windows.Forms.CheckBox();
            this.nuevoTestCaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBuild = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.SuitesTreeCMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // SuitesTree
            // 
            this.SuitesTree.AllowDrop = true;
            this.SuitesTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SuitesTree.BackColor = System.Drawing.Color.White;
            this.SuitesTree.CheckBoxes = true;
            this.SuitesTree.ContextMenuStrip = this.SuitesTreeCMenu;
            this.SuitesTree.Location = new System.Drawing.Point(3, 32);
            this.SuitesTree.Name = "SuitesTree";
            this.SuitesTree.Size = new System.Drawing.Size(306, 280);
            this.SuitesTree.TabIndex = 1;
            this.SuitesTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.SuitesTree_AfterCheck);
            this.SuitesTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.SuitesTree_ItemDrag);
            this.SuitesTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.SuitesTree_NodeMouseClick);
            this.SuitesTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.SuitesTree_DragDrop);
            this.SuitesTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.SuitesTree_DragEnter);
            this.SuitesTree.DragOver += new System.Windows.Forms.DragEventHandler(this.SuitesTree_DragOver);
            this.SuitesTree.DoubleClick += new System.EventHandler(this.SuitesTree_NodeMouseDoubleClick);
            this.SuitesTree.MouseDown += new System.Windows.Forms.MouseEventHandler(this.SuitesTree_MouseDown);
            // 
            // SuitesTreeCMenu
            // 
            this.SuitesTreeCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoToolStripMenuItem});
            this.SuitesTreeCMenu.Name = "SuitesTreeCMenu";
            this.SuitesTreeCMenu.Size = new System.Drawing.Size(153, 26);
            // 
            // nuevoToolStripMenuItem
            // 
            this.nuevoToolStripMenuItem.Name = "nuevoToolStripMenuItem";
            this.nuevoToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.nuevoToolStripMenuItem.Text = "New Test Suite";
            this.nuevoToolStripMenuItem.Click += new System.EventHandler(this.nuevaTestSuiteToolStripMenuItem_Click);
            // 
            // checkSelectAll
            // 
            this.checkSelectAll.AutoSize = true;
            this.checkSelectAll.Location = new System.Drawing.Point(3, 7);
            this.checkSelectAll.Name = "checkSelectAll";
            this.checkSelectAll.Size = new System.Drawing.Size(117, 17);
            this.checkSelectAll.TabIndex = 25;
            this.checkSelectAll.Text = "Select/Unselect All";
            this.checkSelectAll.UseVisualStyleBackColor = true;
            this.checkSelectAll.Click += new System.EventHandler(this.checkSelectAll_Click);
            // 
            // nuevoTestCaseToolStripMenuItem
            // 
            this.nuevoTestCaseToolStripMenuItem.Name = "nuevoTestCaseToolStripMenuItem";
            this.nuevoTestCaseToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.nuevoTestCaseToolStripMenuItem.Text = "New Test Case";
            this.nuevoTestCaseToolStripMenuItem.Click += new System.EventHandler(this.nuevoTestCaseToolStripMenuItem_Click);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(32, 19);
            this.eliminarToolStripMenuItem.Text = "Delete";
            this.eliminarToolStripMenuItem.Click += new System.EventHandler(this.eliminarTestSuiteToolStripMenuItem_Click);
            // 
            // btnBuild
            // 
            this.btnBuild.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuild.Location = new System.Drawing.Point(3, 318);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(122, 23);
            this.btnBuild.TabIndex = 0;
            this.btnBuild.Text = "Test";
            this.btnBuild.UseVisualStyleBackColor = true;
            this.btnBuild.Click += new System.EventHandler(this.btnBuild_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Image = global::PGGXUnit.Packages.GXUnit.Items.UpdatePng;
            this.btnUpdate.Location = new System.Drawing.Point(284, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(25, 23);
            this.btnUpdate.TabIndex = 26;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // GXUnitMainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.checkSelectAll);
            this.Controls.Add(this.SuitesTree);
            this.Controls.Add(this.btnBuild);
            this.Controls.Add(this.btnUpdate);
            this.Name = "GXUnitMainWindow";
            this.Size = new System.Drawing.Size(312, 453);
            this.SuitesTreeCMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion


        private System.Windows.Forms.CheckBox checkSelectAll;
        private System.Windows.Forms.TreeView SuitesTree;
        private System.Windows.Forms.ContextMenuStrip SuitesTreeCMenu;
        private System.Windows.Forms.ToolStripMenuItem nuevoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoTestCaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.Button btnUpdate;
    }
}
