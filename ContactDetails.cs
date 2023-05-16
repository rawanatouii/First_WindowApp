using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForTraining
{
    public partial class ContactDetails : Form
    {
        public ContactDetails(int id)
        {
            InitializeComponent();
            if (id != 0)//load data
            {
                txtID.Text = id.ToString();
                txtID.ReadOnly = true;
                Contact c = FileController.GetContactByID(id);
                txtName.Text = c.Name;
                txtPhone.Text = c.Phone;
              //  pictureBox1.Image = Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath).Replace("\\bin\\Debug", "") + @"\media\" + txtID.Text + c.Image);
                btnsave.Visible = true;
                btnInsert.Visible = false;
                pictureBox1.ImageLocation = Path.GetDirectoryName(Application.ExecutablePath).Replace("\\bin\\Debug", "") + @"\media\" + txtID.Text + c.Image;
            }
            else
            {//new contact
                btnsave.Visible = false;
                btnInsert.Visible = true;
            }
        }

        private void btnsave_Click(object sender, EventArgs e)
        {
             pictureBox1.Image.Dispose();
            FileController.UpdateContact(new Contact
            {
                ID = int.Parse(txtID.Text),
                Name = txtName.Text,
                Phone = txtPhone.Text,
                Image = pictureBox1.ImageLocation
            }, string.IsNullOrEmpty(openFileDialog1.FileName) ? false : true);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(openFileDialog1.OpenFile());
                    pictureBox1.ImageLocation = openFileDialog1.FileName;
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Unable to open file " + exp.Message);
                }
            }
            else
            {
                openFileDialog1.Dispose();
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            FileController.AddContact(new Contact
            {
                ID = int.Parse(txtID.Text),
                Name = txtName.Text,
                Phone = txtPhone.Text,
                Image = openFileDialog1.FileName
            });
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ContactDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
