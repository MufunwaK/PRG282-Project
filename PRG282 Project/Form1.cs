using PRG282_Project.Business_Logic_Layer;
using PRG282_Project.Data_Layer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PRG282_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            RefreshDataGrid();
        }

    // --- RefreshDataGrid method ---
    private void RefreshDataGrid()
        {
            var heroes = FileHandler.LoadHeroes(); // reads heroes from file
            dgvSuperhero.DataSource = null;             // reset the grid
            dgvSuperhero.DataSource = heroes;           // bind the list
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                int heroID = int.Parse(txtID.Text);
                string name = txtName.Text;
                int age = int.Parse(txtAge.Text);
                string superpower = txtSuperpower.Text;
                int examScore = int.Parse(txtScore.Text);
                
                // Create hero and save to text file
                var hero = HeroManager.CreateHero(heroID, name, age, superpower, examScore);
                HeroManager.AddHero(hero);
                FileHandler.SaveHero(hero);

                RefreshDataGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
                      // reload the grid with updated heroes


        }

        private void btnView_Click(object sender, EventArgs e)
        {
          
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
        "Are you sure you want to exit?",
        "Confirm Exit",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
