using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

using Artech.Architecture.UI.Framework.Editors;
using Artech.Common.Properties;

//using PGGXUnit.Packages.GXUnit.GeneXusAPI;

namespace PGGXUnit.Packages.GXUnit
{
	public partial class MyEditor : BaseEditor
	{
		public MyEditor()
		{
			InitializeComponent();
		}

        public ResultadoPart ResultadoPart
		{
			get
			{
                return base.Part as ResultadoPart;
			}
		}
        
		public string ArchivoResultado
		{
            get { return ResultadoPart.ArchivoResultado; }
            set { ResultadoPart.ArchivoResultado = value; 
                  base.Part.Dirty = true;}
		}
        
		protected override void OnLoad(EventArgs e)
        {
            base.Initialize();
            MostrarResultado();
        }

        private void expandirResultado2(TreeNode nodo)
        {
            if (nodo.Text.Equals("Assert") && nodo.FirstNode.Text.Equals("--> FAIL"))
            {
                nodo.Parent.ExpandAll();
            }
            else if (nodo.Text.Equals("Suite"))
            {
                nodo.Expand();
            }
            else
            {

                foreach (TreeNode n in nodo.Nodes)
                {
                    expandirResultado2(n);

                }
            }
        }

        private void expandirResultado(TreeView treeView1){
            foreach (TreeNode nodo in treeView1.Nodes)
            {
                expandirResultado2(nodo);
            }
        }
        private int expandirResultado3(TreeNodeCollection nodos)
        {
            foreach (TreeNode nodo in nodos)
            {
                if (nodo.Text.Equals("Assert") && nodo.FirstNode.Text.Equals("--> FAIL"))
                {
                    nodo.Parent.ExpandAll();
                    
                    return 1;
                }
                else if (nodo.Text.Equals("Suite"))
                {
                    nodo.Expand();
                    expandirResultado3(nodo.Nodes);
                }
                else 
                {
                    expandirResultado3(nodo.Nodes);
                }
                
            }
            return 0;
        }
        private void MostrarResultado() 
        {
            XmlDocument xml = new XmlDocument();
            xml.InnerXml = ResultadoPart.ArchivoResultado;
            treeView1.Nodes.Clear();
            treeView1.Font = new Font(this.Font, FontStyle.Bold);
            
            ConvertXmlNodeToTreeNode(xml, treeView1.Nodes);
            expandirResultado3(treeView1.Nodes);

            XmlNodeList suites = xml.GetElementsByTagName("GXUnitSuite");
            XmlNodeList lista = ((XmlElement)suites[0]).GetElementsByTagName("TestName");
            label1.Text = "Run: " + lista.Count.ToString();

            lista = ((XmlElement)suites[0]).GetElementsByTagName("TestCase");
            int i = 0;
            foreach (XmlElement nodo in lista)
            {
                if (nodo.FirstChild.Name.Equals("TestName"))
                {

                    XmlNodeList nNombre = nodo.GetElementsByTagName("Result");
                    foreach (XmlNode nodo2 in nNombre)
                    {
                        if (nodo2.InnerText.Equals("FAIL"))
                        {
                            i++;
                            break;
                        }
                    }
                }

            } 

            label2.Text = "With errors: " + i.ToString();          
        }

        private void ConvertXmlNodeToTreeNode(XmlNode xmlNode,TreeNodeCollection treeNodes)
        {

            TreeNode newTreeNode = null;

            switch (xmlNode.NodeType)
            {
                case XmlNodeType.ProcessingInstruction:
                case XmlNodeType.XmlDeclaration:
                    //newTreeNode.Text = "<?" + xmlNode.Name + " " +
                      //xmlNode.Value + "?>";
                    break;
                case XmlNodeType.Element:
                    if (xmlNode.Name.Equals("Suite") || (xmlNode.Name.Equals("GXUnitSuite") && !xmlNode.FirstChild.Name.Equals("SuiteName")) || xmlNode.Name.Equals("TestTimeExecution") || xmlNode.Name.Equals("Assert")
                        || xmlNode.Name.Equals("TestCase") || xmlNode.Name.Equals("Expected") || xmlNode.Name.Equals("Obtained"))
                    {
                        if (xmlNode.Name.Equals("TestCase")){
                            if (!tieneEsteHijo("TestCase",xmlNode)){
                                newTreeNode = treeNodes.Add(xmlNode.Name);
                                newTreeNode.Text = xmlNode.Name; //+ " " + xmlNode.ChildNodes.Item(1).FirstChild.Value + " ms";
                                
                            }
                        }
                        else if (xmlNode.Name.Equals("Assert"))
                        {
                            if (!tieneEsteHijo("Assert", xmlNode))
                            {
                                newTreeNode = treeNodes.Add(xmlNode.Name);
                                newTreeNode.Text = xmlNode.Name;
                                
                            }

                        }
                        else if (xmlNode.Name.Equals("Suite"))
                        {
                            if (!tieneEsteHijo("Suite", xmlNode))
                            {
                                newTreeNode = treeNodes.Add(xmlNode.Name);
                                newTreeNode.Text = xmlNode.Name;

                            }

                        }
                        else
                        {
                            newTreeNode = treeNodes.Add(xmlNode.Name);
                            newTreeNode.Text = xmlNode.Name;
                        }
                    }
                    break;
                case XmlNodeType.Attribute:
                    //newTreeNode.Text = "ATTRIBUTE: " + xmlNode.Name;
                    break;
                case XmlNodeType.Text:
                case XmlNodeType.CDATA:
                    if (!xmlNode.Value.Equals("GXUnitSuites"))
                    {
                        newTreeNode = treeNodes.Add(xmlNode.Name);
                        newTreeNode.Text = "--> " + xmlNode.Value;
                    }
                    if (xmlNode.Value.Equals("FAIL"))
                    {
                        newTreeNode.BackColor = Color.Red;

                    }
                    else if (xmlNode.Value.Equals("OK"))
                    {
                        newTreeNode.BackColor = Color.Green;
                    }
                    else if (xmlNode.ParentNode.Name.Equals("TestTimeExecution"))
                    {
                        newTreeNode.Text = newTreeNode.Text + " ms";
                    }
                    break;
                case XmlNodeType.Comment:
                    //newTreeNode.Text = "<!--" + xmlNode.Value + "-->";
                    break;
            }

            if (xmlNode.Attributes != null && newTreeNode != null)
            {
                foreach (XmlAttribute attribute in xmlNode.Attributes)
                {
                    ConvertXmlNodeToTreeNode(attribute, newTreeNode.Nodes);
                }
            }
            foreach (XmlNode childNode in xmlNode.ChildNodes)
            {
                if (newTreeNode != null )
                    ConvertXmlNodeToTreeNode(childNode, newTreeNode.Nodes);
                else ConvertXmlNodeToTreeNode(childNode, treeNodes);
            }
        }

        private bool tieneEsteHijo(String hijo, XmlNode xmlNode) {
            if (xmlNode.FirstChild.Name.Equals(hijo)) {
                return true;
            }
            return false;
        }

		private void AttachToEvents()
		{
			//textBox.TextChanged += new EventHandler(textBox_TextChanged);
			//textBox.SelectionChanged += new EventHandler(textBox_SelectionChanged);
		}

		void textBox_TextChanged(object sender, EventArgs e)
		{
			//RtfText = textBox.Rtf;
			this.DocumentChanged(null);
		}

		void textBox_SelectionChanged(object sender, EventArgs e)
		{
			// We don't actually have a proper 'object' to be bound to the Property Inspector
			// so we use this SelectionProxy just for illustration purposes
			List<object> selection = new List<object>(1);
			//selection.Add(new SelectionProxy(textBox));
			SetSelection(selection , null);
		}

		public override void UpdateView()
		{
		}

		#region SelectionProxy class

		public class SelectionProxy : IDescriptive
		{
			private RichTextBox container;

			public SelectionProxy(RichTextBox container)
			{
				this.container = container;
			}

			public Font SelectionFont
			{
				get { return container.SelectionFont; }
				set
				{
					container.SelectionFont = value;
				}
			}

			public Color SelectionColor
			{
				get { return container.SelectionColor; }
				set
				{
					container.SelectionColor = value;
				}
			}

			public Color SelectionBackColor
			{
				get { return container.SelectionBackColor; }
				set
				{
					container.SelectionBackColor = value;
				}
			}

			public HorizontalAlignment SelectionAlignment
			{
				get { return container.SelectionAlignment; }
				set
				{
					container.SelectionAlignment = value;
				}
			}

			#region IDescriptive Members

			[Browsable(false)]
			public string Description
			{
				get { return string.Empty; }
			}

			[Browsable(false)]
			public string Type
			{
				get { return "Selected text"; }
			}

			bool IDescriptive.IsReadOnly { get { return false; } }
			#endregion
		}

		#endregion

        private void button_expand_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }

        private void button_collapse_Click(object sender, EventArgs e)
        {
            treeView1.CollapseAll();
        }
	}
}
