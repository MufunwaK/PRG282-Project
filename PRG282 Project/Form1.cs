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
            //link the information from the summary.txt to the form
            var summary = HeroManager.GenerateSummaryReport();

            // Update the summaries and writes the information on the form
           // txtNumOfHeroes.Text = summary["TotalHeroes"];
           // txtAvgAge.Text = summary["AverageAge"];
            txtAvgExamScore.Text = summary["AverageScore"];
            txtHeroPerRank.Text = $"S: {summary["RankS"]}, A: {summary["RankA"]}, B: {summary["RankB"]}, C: {summary["RankC"]}";
            MessageBox.Show("Summary report generated and saved to summary.txt!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvSuperhero.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a superhero to delete.");
                return;
            }

            int selectedID = Convert.ToInt32(dgvSuperhero.SelectedRows[0].Cells["ID"].Value);

            DialogResult confirm = MessageBox.Show(
                "Are you sure you want to delete this hero?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm == DialogResult.Yes)
            {
                FileHandler.DeleteHero(selectedID);
                RefreshDataGrid();
                MessageBox.Show("Hero deleted successfully!");
            }
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text) ||
                    string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtAge.Text) ||
                    string.IsNullOrWhiteSpace(txtSuperpower.Text) ||
                    string.IsNullOrWhiteSpace(txtScore.Text))
                {
                    MessageBox.Show("Please fill in all fields before updating.");
                    return;
                }

                int id = int.Parse(txtID.Text);
                string name = txtName.Text.Trim();
                int age = int.Parse(txtAge.Text);
                string superpower = txtSuperpower.Text.Trim();
                int examScore = int.Parse(txtScore.Text);

                string rank = HeroManager.GetRank(examScore);
                string threat = HeroManager.GetThreatLevel(rank);

                Hero updatedHero = new Hero
                {
                    ID = id,
                    Name = name,
                    Age = age,
                    Superpower = superpower,
                    ExamScore = examScore,
                    Rank = rank,
                    ThreatLevel = threat
                };

                // Use the returned value to decide what to show
                bool success = FileHandler.UpdateHero(updatedHero);

                if (success)
                {
                    RefreshDataGrid();
                    MessageBox.Show("Hero updated successfully!");
                }
                // else: do nothing — the method already showed "not found"
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for ID, Age, and Exam Score.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating hero: " + ex.Message);
            }
        
        }


    

        private void dgvSuperhero_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvSuperhero.Rows[e.RowIndex];

                txtID.Text = row.Cells["ID"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtAge.Text = row.Cells["Age"].Value.ToString();
                txtSuperpower.Text = row.Cells["Superpower"].Value.ToString();
                txtScore.Text = row.Cells["ExamScore"].Value.ToString();
            }
        }
    }
}
