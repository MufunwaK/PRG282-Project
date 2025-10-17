using PRG282_Project;
using PRG282_Project.Business_Logic_Layer;
using PRG282_Project.Data_Layer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG282_Project.Business_Logic_Layer
{
    internal class HeroManager
    {
        public static void AddHero(Hero hero)
        {
            hero.Rank = GetRank(hero.ExamScore);
            hero.ThreatLevel = GetThreatLevel(hero.Rank);

            // Show a pop-up with the Threat Level
            // System.Windows.Forms.MessageBox.Show($"Threat Level: {hero.ThreatLevel}");
            System.Windows.Forms.MessageBox.Show(
                          $"Hero has been added!\n" +
                          $"Name: {hero.Name}\n" +
                          $"Rank: {hero.Rank}\n" +
                          $"Threat Level: {hero.ThreatLevel}"
 );



           // FileHandler.SaveHero(hero);
        }

        // Calculate rank based on exam score
        public static string GetRank(int examScore)
        {
            if (examScore >= 81) 
                return "S";
            else if (examScore >= 61) 
                return "A";
            else if (examScore >= 41) 
                return "B";
            else 
                return "C";
        }

        // Determine threat level based on rank
       
          public static string GetThreatLevel(string rank)
        {
            if (rank == "S")
                return "Catastrophic";
            else if (rank == "A")
                return "High";
            else if (rank == "B")
                return "Moderate";
            else if (rank == "C")
                return "Low";
            else
                return "Minimal";
        }


        // Create a hero and set Rank & ThreatLevel
        public static Hero CreateHero(int heroID, string name, int age, string superpower, int examScore)
        {
            Hero hero = new Hero
            {
                ID = heroID,
                Name = name,
                Age = age,
                Superpower = superpower,
                ExamScore = examScore,
                Rank = GetRank(examScore),
                ThreatLevel = GetThreatLevel(GetRank(examScore))
            };

            return hero;
        }
    }

}

