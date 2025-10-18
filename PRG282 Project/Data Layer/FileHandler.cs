using PRG282_Project.Business_Logic_Layer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PRG282_Project.Data_Layer;

namespace PRG282_Project.Data_Layer
{
    internal class FileHandler
    {
        // Path to the existing file
        private static string filePath = "superheroes.txt";

        // Save a hero to the text file
        public static void SaveHero(Hero hero)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true)) // 'true' appends to file
                {
                    writer.WriteLine($"{hero.ID},{hero.Name},{hero.Age},{hero.Superpower},{hero.ExamScore},{hero.Rank},{hero.ThreatLevel}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving hero: " + ex.Message);
            }
        }
      
        // Load all heroes from the text file
        public static List<Hero> LoadHeroes()
        {
            List<Hero> heroes = new List<Hero>();

            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath);

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 7)
                        {
                            Hero hero = new Hero
                            {
                                ID = int.Parse(parts[0]),
                                Name = parts[1],
                                Age = int.Parse(parts[2]),
                                Superpower = parts[3],
                                ExamScore = int.Parse(parts[4]),
                                Rank = parts[5],
                                ThreatLevel = parts[6]
                            };
                            heroes.Add(hero);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading heroes: " + ex.Message);
            }

            return heroes;
        }

        

        // have all heroes (overwrite file)
        private static void SaveAllHeroes(List<Hero> heroes)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, false)) // false = overwrite
                {
                    foreach (var hero in heroes)
                    {
                        writer.WriteLine($"{hero.ID},{hero.Name},{hero.Age},{hero.Superpower},{hero.ExamScore},{hero.Rank},{hero.ThreatLevel}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving heroes: " + ex.Message);
            }
        }

        // Delete a hero by ID
        public static void DeleteHero(int heroID)
        {
            try
            {
                var heroes = LoadHeroes();
                var updatedHeroes = heroes.Where(h => h.ID != heroID).ToList();
                SaveAllHeroes(updatedHeroes);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting hero: " + ex.Message);
            }
        }

        //Update a hero record
        public static bool UpdateHero(Hero updatedHero)
        {
            try
            {
                List<Hero> heroes = LoadHeroes();

                bool found = false;
                for (int i = 0; i < heroes.Count; i++)
                {
                    if (heroes[i].ID == updatedHero.ID)
                    {
                        heroes[i] = updatedHero; // replace the record
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    SaveAllHeroes(heroes);
                    return true; // update succeeded
                }
                else
                {
                    MessageBox.Show("Hero not found in the file!");
                    return false; // update failed
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating hero: " + ex.Message);
                return false;
            }
        }

    }
}

    

