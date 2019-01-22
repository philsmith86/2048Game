using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game
{
    class HighScore
    {

        DirectoryInfo currentDir;
        string filePath;

        public HighScore() {
            currentDir = new DirectoryInfo(".");
            filePath = currentDir.FullName + @"\highScore.txt";
            Console.WriteLine(filePath);
        }

        public int GetHighScore()
        {
            if (File.Exists(filePath))
            {
                StreamReader sr = File.OpenText(filePath);
                var score = Int32.Parse(sr.ReadLine());
                sr.Close();
                return score;
            }
            return 0;            
        }

        public void SaveHighScore(int newScore)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            StreamWriter sw = File.CreateText(filePath);
            sw.WriteLine(newScore);
            sw.Close();
        }
        

            
    }
}
