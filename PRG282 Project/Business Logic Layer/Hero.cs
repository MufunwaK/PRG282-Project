using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PRG282_Project.Business_Logic_Layer;

namespace PRG282_Project.Business_Logic_Layer
{

    //The Hero class is basically a blueprint for superhero objects.
    //Its purpose is to represent a single superhero and all the information
    //associated with them in a structured way.
    internal class Hero
    {
        //Fields
        private string threatLevel;
        private int examScore;
        private string name;
        private string superpower;
        private string rank;
        private int iD;
        private int age;

        public Hero()
        {
        }

        //Parameterized constructor
        public Hero(string name, string superpower, string rank, string threatLevel, int iD, int age, int examScore)
        {
            Name = name;
            Superpower = superpower;
            Rank = rank;
            ThreatLevel = threatLevel;
            ID = iD;
            Age = age;
            ExamScore = examScore;
        }

        //Properties
        public string Name { get => name; set => name = value; }
        public string Superpower { get => superpower; set => superpower = value; }
        public string Rank { get => rank; set => rank = value; }
        public string ThreatLevel { get => threatLevel; set => threatLevel = value; }
        public int ID { get => iD; set => iD = value; }
        public int Age { get => age; set => age = value; }
        public int ExamScore { get => examScore; set => examScore = value; }

    }
}
