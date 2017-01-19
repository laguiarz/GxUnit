using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;


namespace PGGXUnit.Packages.GXUnit.Utils
{
    public partial class InputBox : Form
    {

        // the InputBox
        private static InputBox newInputBox;

        // the string name that will be returned to the calling form
        private static string returnString;

        public InputBox(string labelText)
        {
            InitializeComponent();
            label1.Text = labelText;
            this.label2.Visible = false;
        }

        public static string Show(string inputBoxText, string labelText)
        {

            newInputBox = new InputBox(labelText);
            newInputBox.Text = inputBoxText;
            newInputBox.ShowDialog();

            return returnString;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox.Text.Equals(""))
            {
                this.label2.Text = "* Required field";
                this.label2.Visible = true;
            }
            else
            {
                returnString = textBox.Text;
                newInputBox.Dispose();
            }
        }


        private void InputBox_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            returnString = "";
            newInputBox.Dispose();
        }
    }
}
