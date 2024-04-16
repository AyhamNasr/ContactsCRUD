using ContactsBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsWindowsForms
{
    public partial class frmListContact : Form
    {
        public frmListContact()
        {
            InitializeComponent();
        }

        private void _RefreshContactsList()
        {
            dgvAllContacts.DataSource = clsContact.GellAllContacts();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            _RefreshContactsList();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
           Form form1 = new frmAddEditContact(-1);
            form1.ShowDialog();
            _RefreshContactsList();
            
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            frmAddEditContact frm = new frmAddEditContact((int)dgvAllContacts.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshContactsList();
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {

            clsContact.DeleteContact((int)dgvAllContacts.CurrentRow.Cells[0].Value);
            _RefreshContactsList();

        }
    }
}
