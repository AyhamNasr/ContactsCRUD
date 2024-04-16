using ContactsBusinessLayer;
using CountryBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContactsWindowsForms
{
    public partial class frmAddEditContact : Form
    {
        public enum enMode { AddNew = 0 ,  Update = 1}
        private enMode _Mode ;

        int _ContactId;
        clsContact Contact;
        public frmAddEditContact(int contactId)
        {
            InitializeComponent();
            _ContactId = contactId;
            if (_ContactId == -1 )
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;
        }

        private void _FillComboBoxbyCountries()
        {
            DataTable dt = clsCountry.GetAllCountries();

            foreach ( DataRow row in dt.Rows )
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _LodeData()
        {
            _FillComboBoxbyCountries();
            cbCountry.SelectedIndex = 0;

            if(_Mode == enMode.AddNew)
            {
                lbMode.Text = "Add New Mode";
                Contact = new clsContact();
                return;
            }
            Contact = clsContact.Find(_ContactId);

            if( Contact == null )
            {
                MessageBox.Show("This form will be closed becouse no cntact with id ");
                this.Close();
            }

            lbMode.Text = "Edit Contact Id = "+ _ContactId;
            lbContactID.Text = _ContactId.ToString();
            tbFirstNAme.Text = Contact.FirstName;
            tbLastName.Text = Contact.LastName;
            tbEmail.Text = Contact.Email;
            tbPhone.Text = Contact.Phone;
            dtDateOfBirth.Value = Contact.DateOfBirth;
            if (Contact.ImagePath != "")
            {
                pictureBox1.Load(Contact.ImagePath);
            }
            linkLabel2.Visible = (Contact.ImagePath != "");
            //cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.FindCountry(_ContactId).CountryName);
            //this will select the country in the combobox.
            cbCountry.SelectedIndex = cbCountry.FindString(clsCountry.FindCountry(Contact.CountryID).CountryName);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void frmAddEditContact_Load(object sender, EventArgs e)
        {
            _LodeData();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int CountryId = clsCountry.FindCountry(cbCountry.Text).CountryID;
            Contact.FirstName = tbFirstNAme.Text;
            Contact.LastName = tbLastName.Text;
            Contact.Email = tbEmail.Text;
            Contact.Phone = tbPhone.Text;
            Contact.Address = tbAddress.Text;
            Contact.DateOfBirth = dtDateOfBirth.Value;
            Contact.CountryID = cbCountry.SelectedIndex;
            if(pictureBox1.ImageLocation != null)
            {
                Contact.ImagePath = pictureBox1.ImageLocation.ToString();
            }
            else
            {
                Contact.ImagePath = "";
            }

            if(Contact.save())
            {
                MessageBox.Show("Data Saved Successfully.");
            }
            else
            {
                MessageBox.Show("Data Not Saved");
            }

            _Mode = enMode.Update;
            lbMode.Text = "Edit Contact Id = " + Contact.ID;
            lbContactID.Text = Contact.ID.ToString();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                //MessageBox.Show("Selected Image is:" + selectedFilePath);

                pictureBox1.Load(selectedFilePath);
                // ...

                linkLabel2.Visible = true;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pictureBox1.ImageLocation = null;
            linkLabel2.Visible = false;

        }
    }
}
