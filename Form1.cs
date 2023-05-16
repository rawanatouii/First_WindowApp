using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForTraining
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //        //returns url of main directory which contains "/bin/Debug"
            //        var url = System.IO.Path.GetDirectoryName(
            //System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            //        //correction in path to point it in Root directory
            //        var mainpath = url.Replace("\\bin\\Debug", "");
          //List<Contact> c=  FileController.GetAllContacts();
          //  foreach (Contact t in c)
          //      MessageBox.Show(t.ID+" "+t.Name);
        }

        private void lbl_Click(object sender, EventArgs e)
        {

        }
    }
}
