using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForTraining
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            gvContacts.DataSource = FileController.GetAllContacts(txtSearch.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ContactDetails f = new ContactDetails(0);
            if (f.ShowDialog() == DialogResult.OK)
            {
                gvContacts.DataSource = FileController.GetAllContacts(txtSearch.Text);
                gbDetails.Visible = false;
            }
            f.Dispose();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            ContactDetails f = new ContactDetails(int.Parse(lblID.Text));
            pictureBox1.Image.Dispose();
            if (f.ShowDialog() == DialogResult.OK)
            {
                gvContacts.DataSource = FileController.GetAllContacts(txtSearch.Text);
                gbDetails.Visible = false;
            }
            f.Dispose();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                pictureBox1.Image.Dispose();
                FileController.DeleteContact(int.Parse(lblID.Text), gvContacts.CurrentRow.Cells["Image"].Value.ToString());
                gvContacts.DataSource = FileController.GetAllContacts(txtSearch.Text);
                gbDetails.Visible = false;
            }
        }

        private void gvContacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            gbDetails.Visible = true;
            lblID.Text = gvContacts.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            lblName.Text = gvContacts.Rows[e.RowIndex].Cells["Name"].Value.ToString();
            lblPhone.Text = gvContacts.Rows[e.RowIndex].Cells["Phone"].Value.ToString();
            pictureBox1.Image = System.Drawing.Image.FromFile(Path.GetDirectoryName(Application.ExecutablePath).Replace("\\bin\\Debug", "") + @"\media\" + lblID.Text + gvContacts.Rows[e.RowIndex].Cells["Image"].Value.ToString());
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            gvContacts.DataSource = FileController.GetAllContacts(txtSearch.Text);
        }

        private void gbDetails_Enter(object sender, EventArgs e)
        {

        }

        private void gvContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }
    }
}
