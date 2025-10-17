using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRG282_Project.Business_Logic_Layer;
using PRG282_Project.Data_Layer;

namespace PRG282_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Windows Forms setup
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Create the superheroes.txt file if it doesn't exist
             string filename = "superheroes.txt";

             if (!File.Exists(filename))
             {
                 try
                 {
                     File.WriteAllText(filename, ""); // Creates an empty file
                     MessageBox.Show("superheroes.txt created successfully!");
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("Error creating file: " + ex.Message);
                 }
             } 
       


            Application.Run(new Form1());
        }
    }
}

