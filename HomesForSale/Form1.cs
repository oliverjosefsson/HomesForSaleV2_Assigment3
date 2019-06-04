using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PropertyManager;
using System.Data.SqlClient;

namespace HomesForSale
{
    public partial class Form1 : Form
    {
        private int id, dbID;
        private string file;
        private EstateManager estateManager;
        private SqlConnection cn;
        public Form1()
        {
            InitializeComponent();
            InitializeComponents();
        }
        /// <summary>
        /// method to initialize the GUI:s interface.
        /// </summary>
        private void InitializeComponents()
        {
            cn = new SqlConnection(global::HomesForSale.Properties.Settings.Default.HomesDBConnectionString);
            file = null;
            id = 1;
            dbID = 100; 

            estateManager = new EstateManager();
            btnDelete.Enabled = false;
            btnChange.Enabled = false;
            btnInfo.Enabled = false;
            cdCountry.DataSource = Enum.GetValues(typeof(Countries));
            cdLegalForm.DataSource = Enum.GetValues(typeof(LegalForm));

            cbType.DropDownStyle = ComboBoxStyle.DropDownList;
            cdCountry.DropDownStyle = ComboBoxStyle.DropDownList;
            cdLegalForm.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        ///if redidental radiobutton is checked, the list of type will fill with residental types.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbResidental_CheckedChanged(object sender, EventArgs e){cbType.DataSource = Enum.GetValues(typeof(ResidentalType));}


        /// <summary>
        ///if commercial radiobutton is checked, the list of type will fill with commercial types.
        /// </summary>
        private void rdCommercial_CheckedChanged(object sender, EventArgs e){cbType.DataSource = Enum.GetValues(typeof(CommericalType));}

        /// <summary> 
        /// if any object is selected in the view the change and delete buttons will be enabled.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvProperties_SelectedIndexChanged(object sender, EventArgs e){btnDelete.Enabled = true; btnChange.Enabled = true; btnInfo.Enabled = true; }
        
        /// <summary>
        ///method to add a object to the list in listmanager and add to the view. 
        ///creates a Property and uses the switch case to give the property a value. 
        /// </summary>
        private void btnAdd_Click(object sender, EventArgs e){

            Property property;
            string typeOfProperty = cbType.Text;

            switch (typeOfProperty)
            {
                case "Apartment":
                    property = new Apartment();
                    break;

                case "House":
                    property = new House();
                    break;

                case "Shop":
                    property = new Shop();
                    break;

                case "Townhouse":
                    property = new Townhouse();
                    break;

                case "Warehouse":
                    property = new Warehouse();
                    break;

                case "Villa":
                    property = new Villa();
                    break;
                default:
                    typeOfProperty = cbType.Text;
                    return;
            }
            property.Id = id;
            AddProperty(property);
            estateManager.Add(property);
            test(); // check how many objects in listmanager.
            fillList();
            id++;
        }


        /// <summary> 
        /// method that adds the values to the property, based on input from gui.
        /// </summary>
        /// <param name="property"></param>
        private void AddProperty(Property property)
        {
            Address address = new Address
            {
                Country = cdCountry.Text,
                City = tbCity.Text,
                Street = tbStreet.Text,
                ZipCode = tbZipCode.Text
            };

            property.Address = address;
            property.LegalForm = cdLegalForm.Text;
            property.Type = cbType.Text;
            property.UseForm = getCategory();
           
        }

        /// <summary>
        /// button to delete a selected object in the viewlist, also gets deleted in the list in listmanager.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            estateManager.DeleteAt(lvProperties.SelectedIndices[0]);

            fillList();
            btnDelete.Enabled = false;
            btnChange.Enabled = false;
            test();
        }

        /// <summary>
        /// method for changing an objects values, and refresh the list and viewlist with the new values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChange_Click(object sender, EventArgs e)
        {

            Property property;
            string typeOfProperty = cbType.Text;
            int selectedObjectId = Int32.Parse(lvProperties.SelectedItems[0].SubItems[0].Text);

            switch (typeOfProperty)
            {
                case "Apartment":
                    property = new Apartment();
                    break;

                case "House":
                    property = new House();

                    break;

                case "Shop":
                    property = new Shop();

                    break;

                case "TownHouse":
                    property = new Townhouse();

                    break;

                case "Warehouse":
                    property = new Warehouse();

                    break;

                case "Villa":
                    property = new Villa();
                    property.Id = id;
                    break;
                default:
                    typeOfProperty = cbType.Text;
                    return;
            }
            property.Id = selectedObjectId;
            AddProperty(property);
           
            test();
            btnChange.Enabled = false;
            btnDelete.Enabled = false;
            for(int i=0; i<estateManager.Count; i++)
            {
                if (estateManager.GetAt(i).Id == selectedObjectId)
                    estateManager.ChangeAt(property, i);
            }
            fillList();
        }

        /// <summary>
        /// method that iterates the listmanager, and adds the objects to the listview.
        /// </summary>
        private void fillList()
        {
            lvProperties.Items.Clear();
            for(int i = 0; i < estateManager.Count; i++)
            {
                addPropertyToListView(estateManager.GetAt(i));
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="property"></param>
        private void addPropertyToListView(Property property)
        {
            ListViewItem lvi = new ListViewItem(property.Id.ToString());
            lvi.SubItems.Add(property.UseForm);
            lvi.SubItems.Add(property.Type);
            lvi.SubItems.Add(property.LegalForm);
            lvi.SubItems.Add(property.Address.Country);
            lvi.SubItems.Add(property.Address.City);
            lvi.SubItems.Add(property.Address.ZipCode);
            lvi.SubItems.Add(property.Address.Street);

            lvProperties.Items.Add(lvi);
        }

        /// <summary>
        ///displays the number of objects that exist in the list
        /// </summary>
        private void test()
        {
            label1.Text = estateManager.Count.ToString();
        }

        /// <summary>
        /// returns value of radiobuttons
        /// </summary>
        /// <returns></returns>
        private string getCategory()
        {
            if (rbResidental.Checked){return rbResidental.Text;}
            else{return rdCommercial.Text;}
        }

        /// <summary>
        /// Opens a file and fills the lis with objects within the file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            try
            { 
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.InitialDirectory = @"C:\";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    estateManager.BinaryDeSerialize(ofd.FileName);
                }
                fillList();

                id = (estateManager.GetAt(estateManager.Count - 1).Id) + 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        /// <summary>
        /// creates a new file to save.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (estateManager.Count != 0)
                {
                    DialogResult dialogResult = MessageBox.Show("Do you wish to save your list before creating a new one?"
                    , "Save work...", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (file != null)
                        {
                            estateManager.BinarySerialize(file);
                        }
                        else
                        {
                            saveAs();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// method to save the current file 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (file != null)
                {
                    estateManager.BinarySerialize(file);
                }
                else
                {
                    saveAs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// method to save the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuFileSaveAs_Click(object sender, EventArgs e)
        {
            saveAs();
        }
      
        private void saveAs ()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.InitialDirectory = @"C:\";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    estateManager.BinarySerialize(sfd.FileName);
                    file = sfd.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you wish to save your list before Exit?"
                , "Save work...", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                saveAs();
            }
            else
            {
                clearDataBase();
        
                Application.Exit();
            }

        }

        /// <summary>
        /// method that gives information about a property, 
        /// when it is selected and the button info is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInfo_Click_1(object sender, EventArgs e)
        {
            string typeOfProperty = cbType.Text;
            int selectedObjectId = Int32.Parse(lvProperties.SelectedItems[0].SubItems[0].Text);

            for (int i = 0; i < estateManager.Count; i++)
            {
                if (estateManager.GetAt(i).Id == selectedObjectId)
                {
                    MessageBox.Show(estateManager.GetAt(i).ToString(), "Info");
                }
            }
            btnInfo.Enabled = false;
            btnChange.Enabled = false;
            btnDelete.Enabled = false;
        }

        /// <summary>
        /// method that searches for objects in the list that matches the searchstring
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string SearchForCity = tbCitySearch.Text.ToUpper();
            string SearchForType = tbTypeSearch.Text.ToUpper();
            bool notfound = true;

            for (int i = 0; i < estateManager.Count; i++)
            {
                Property property = estateManager.GetAt(i);
                string propertyCity = property.Address.City.ToUpper();
                string propertyType = property.Type.ToUpper();

                if (SearchForCity == propertyCity && SearchForType == propertyType)
                {
                    notfound = false;
                    lvProperties.Items.Clear();
                    addPropertyToListView(property);
                }
            }
            if (notfound)
            {
                    MessageBox.Show("Object could not be found or does not exist, try another input");
            }
        }

        /// <summary>
        /// starts the method addToDataBase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_addToDB_Click(object sender, EventArgs e)
        {
            addToDataBase();
        }
        /// <summary>
        /// method that get selected value in gridview and 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_changeValuesInDB_Click(object sender, EventArgs e)
        {
            int value = Int32.Parse(dgw_dataBase.CurrentRow.Cells[0].Value.ToString());
            changeInDataBase(value);
        }

        /// <summary>
        /// button that fetch the selected id in database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_DeleteFromDB_Click(object sender, EventArgs e)
        {
            int selectedID = Int32.Parse(dgw_dataBase.CurrentRow.Cells[0].Value.ToString());
            deleteFromDataBase(selectedID);
        }

        /// <summary>
        /// delete property in database where id is value
        /// </summary>
        /// <param name="value"></param>
        private void deleteFromDataBase(int property_Id)
        {
            string category = getCategory();

            try
            {
                string sql = "DELETE Property WHERE Property_Id =" + property_Id + ";";
                SqlCommand exeSql = new SqlCommand(sql, cn);
                cn.Open();
                exeSql.ExecuteNonQuery();
                MessageBox.Show("Property with id: " + property_Id +" removed", "Messege", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.propertyTableAdapter.Fill(this.homesDBDataSet.Property);
                dbID--;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { cn.Close(); }
        }

        /// <summary>
        /// adds a new property to the database with inputs from gui.
        /// </summary>
        private void addToDataBase()
        {
            string category = getCategory();

            try
            {
                string sql = "INSERT INTO Property (Property_Id,Property_Category," +
                    "Property_Type,Property_LegalForm,Property_Country,Property_City," +
                    "Property_ZipCode,Property_Street) values(" + dbID + ",'" + category 
                    + "','" + cbType.Text + "','" + cdLegalForm.Text + "','" 
                    + cdCountry.Text + "','" + tbCity.Text + "','" + tbZipCode.Text 
                    + "','" + tbStreet.Text + "')";
                SqlCommand exeSql = new SqlCommand(sql, cn);
                cn.Open();
                exeSql.ExecuteNonQuery();
                MessageBox.Show("Added new record done!", "Messege", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.propertyTableAdapter.Fill(this.homesDBDataSet.Property);
                dbID++;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { cn.Close(); }
        }

        /// <summary>
        /// Change property values in database where id = property_Id
        /// </summary>
        /// <param name="Property_Id"></param>
        private void changeInDataBase(int value)
        {
            string category = getCategory();

            try
            {
                string sql = "UPDATE Property SET Property_Category ='" + category 
                        + "', Property_Type ='" + cbType.Text 
                        + "', Property_LegalForm ='" + cdLegalForm.Text 
                        + "', Property_Country ='" + cdCountry.Text 
                        + "', Property_City ='" + tbCity.Text 
                        + "', Property_ZipCode ='" + tbZipCode.Text 
                        + "', Property_Street ='" + tbStreet.Text 
                        + "' WHERE Property_Id =" + value + ";";
                SqlCommand exeSql = new SqlCommand(sql, cn);
                cn.Open();
                exeSql.ExecuteNonQuery();
                MessageBox.Show("Changed Property with id: " + value, "Messege", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.propertyTableAdapter.Fill(this.homesDBDataSet.Property);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally { cn.Close(); }
        }

        /// <summary>
        /// deletes all items in the database
        /// </summary>
        private void clearDataBase()
        {
            try
            {
                string sql = "DELETE FROM Property";
                SqlCommand exeSql = new SqlCommand(sql, cn);
                cn.Open();
                exeSql.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                e.ToString();
            }
            finally { cn.Close(); }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'homesDBDataSet.Property' table. You can move, or remove it, as needed.
           propertyTableAdapter.Fill(homesDBDataSet.Property);
        }
    }
}
