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
        
        private GameManager gameManager;
        Control grid;
        Control playerScoreText;
        Control highScoreText;

        public Game()
        {
            InitializeComponent();

            gameManager = new GameManager();

            grid = this.Controls["gameGrid"];
            playerScoreText = this.Controls["playerScoreLabel"];
            highScoreText = this.Controls["highScoreLabel"];

            NewGame();
        }

        private void NewGame()
        {
            gameManager.Init();

            //Get the first 2 random tiles and update the grid ui
            gameManager.AddRandomTile();
            gameManager.AddRandomTile();
            UpdateUi();

        }

        private void UpdateUi()
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

            var playerScore = gameManager.GetPlayerScore();
            var highScore = gameManager.GetHighScore();

            playerScoreLabel.Text = "Score: " + playerScore.ToString();
            highScoreText.Text = "Best: " + highScore.ToString();

            //Check for game completion
            if (playerScore >= 2048)
            {
                if (playerScore > highScore)
                {
                    gameManager.SetHighScore(playerScore);
                }
                MessageBox.Show("You Won!");
            }

            //Check for next move
            if (!gameManager.CanMakeNextMove())
            {
                if (playerScore > highScore)
                {
                    gameManager.SetHighScore(playerScore);
                }

                //Game Over
                var result = MessageBox.Show("Game Over!", "Game Over", MessageBoxButtons.OK);
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    NewGame();
                }
            }

        }

        private void startButton_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void Game_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar.ToString())
            {
                case "w":
                    if (gameManager.MoveTilesUp())
                    {
                        gameManager.AddRandomTile();
                        UpdateUi();
                    }
                    break;
                case "s":
                    if (gameManager.MoveTilesDown())
                    {
                        gameManager.AddRandomTile();
                        UpdateUi();
                    }
                    break;
                case "a":
                    if (gameManager.MoveTilesLeft())
                    {
                        gameManager.AddRandomTile();
                        UpdateUi();
                    }
                    break;
                case "d":
                    if (gameManager.MoveTilesRight())
                    {
                        gameManager.AddRandomTile();
                        UpdateUi();
                    }
                    break;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
