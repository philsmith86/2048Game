using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048Game
{
    public partial class Game : Form
    {
        //Colors
        Dictionary<string, Color> gameColors = new Dictionary<string, Color>()
        {
            { "tile-blank", Color.FromArgb(204,193,181) },
            { "tile-filled", Color.FromArgb(238,228,218) },
        };
        
        //Game vars
        private int rows = 4;
        private int columns = 4;
        private GameManager gameManager;
        Control grid;
        private int playerScore = 0;


        public Game()
        {
            InitializeComponent();

            gameManager = new GameManager();

            grid = this.Controls["gameGrid"];

            NewGame();
        }

        private void NewGame()
        {
            playerScore = 0;
            gameManager.Init();

            //Get the first 2 random tiles and update the grid ui
            gameManager.AddRandomTile();
            gameManager.AddRandomTile();
            UpdateGridUi();

        }

        private void UpdateGridUi()
        {
            int[][] vGrid = gameManager.GetGrid();

            for (int r = 0; r<vGrid.Length; r++)
            {
                for (int c = 0; c<vGrid[r].Length; c++)
                {
                    if (vGrid[r][c] == 0)
                    {
                        var id = (r+1).ToString() + "_" + (c+1).ToString();
                        grid.Controls["tile_" + id].BackColor = gameColors["tile-blank"];
                        grid.Controls["tile_" + id].Controls["label_" + id].Text = "";
                    } else
                    {
                        var id = (r + 1).ToString() + "_" + (c + 1).ToString();
                        grid.Controls["tile_" + id].BackColor = gameColors["tile-filled"];
                        grid.Controls["tile_" + id].Controls["label_" + id].Text = vGrid[r][c].ToString();
                    }

                }
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void Game_KeyPress(object sender, KeyPressEventArgs e)
        {
           Console.WriteLine(e.KeyChar.ToString());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
