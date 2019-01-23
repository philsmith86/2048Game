using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game
{
    class GameManager
    {
        private int playerScore = 0;
        Random rand = new Random();

        private HighScore highScoreManager;
        private int highScore;

        public GameManager()
        {
            highScoreManager = new HighScore();
            highScore = highScoreManager.GetHighScore();
        }

        //Virtual Grid to track pieces
        private int[][] vGrid = new int[][]
        {
            new int[] { 0,0,0,0 },
            new int[] { 0,0,0,0 },
            new int[] { 0,0,0,0 },
            new int[] { 0,0,0,0 },
        };

        //Resets vGrid
        public void Init()
        {
            for (int i = 0; i < vGrid.Length; i++) {
                for (int j = 0; j < vGrid[i].Length; j++) {
                    vGrid[i][j] = 0;
                }
            }
            
            playerScore = 0;
        }

        private void UpdatePlayerScore(int byVal)
        {
            playerScore += byVal;
        }

        //Returns random tile row & col
        public int[] AddRandomTile()
        {
            //So lets pull out the empty spaces into a new array
            List<int[]> gridSpaces = new List<int[]>();

            for (int i = 0; i < vGrid.Length; i++)
            {
                for (int j = 0; j < vGrid[i].Length; j++)
                {
                    if (vGrid[i][j] == 0)
                    {
                        gridSpaces.Add(new int[] { i, j });
                    }
                }
            }

            //Grab a random grid space
            int randomIndex = rand.Next(0, gridSpaces.Count);

            //Todo - what if the grid is full?

            var randomRow = gridSpaces[randomIndex][0];
            var randomCol = gridSpaces[randomIndex][1];

            vGrid[randomRow][randomCol] = 2;

            return gridSpaces[randomIndex];
        }

        private int[][] GetGridForGameLogic(bool simulate)
        {
            if (simulate)
            {
                int[][] simulationGrid = new int[][]
                {
                    new int[] { 0,0,0,0 },
                    new int[] { 0,0,0,0 },
                    new int[] { 0,0,0,0 },
                    new int[] { 0,0,0,0 },
                };

                for (int r = 0; r<vGrid.Length; r++)
                {
                    for (int c = 0; c<vGrid[r].Length; c++)
                    {
                        simulationGrid[r][c] = vGrid[r][c];
                    }
                }

                return simulationGrid;
            }
            return vGrid;
        }

        public bool MoveTilesVertical(bool isDown, bool simulate = false)
        {
            int[] lastValue = new int[] { 0, 0, -1 };
            var madeChanges = false;
            var grid = GetGridForGameLogic(simulate);

            var start = 0;
            var end = grid[0].Length;
            var direction = 1;

            if (isDown)
            {
                start = grid[0].Length - 1;
                end = -1;
                direction = -1;                
            }

            for (int c = 0; c < grid[0].Length; c++)
            {
                //Merge 
                lastValue[2] = -1;

                for (int r = start; r != end; r+=direction)
                {

                    if (grid[r][c] == lastValue[2])
                    {
                        //Combine
                        var lr = lastValue[0];
                        var lc = lastValue[1];

                        grid[lr][lc] = grid[lr][lc] * 2;
                        grid[r][c] = 0;
                        lastValue[2] = -1;
                        madeChanges = true;

                        UpdatePlayerScore(grid[lr][lc]);

                    }
                    else if (grid[r][c] > 0)
                    {
                        //No match, update value
                        lastValue[0] = r;
                        lastValue[1] = c;
                        lastValue[2] = grid[r][c];
                    }
                }

                //Move
                for (int r = start; r!=end; r+=direction)
                {
                    if (grid[r][c] > 0)
                    {
                        var newRow = GetEmptyRowSpace(grid, c, direction);
                        var updatePosition = false;

                        if (isDown && newRow > r)
                        {
                            updatePosition = true;
                            
                        } else if (!isDown && newRow > -1 && newRow < r)
                        {
                            updatePosition = true;
                        }

                        if (updatePosition)
                        {
                            grid[newRow][c] = grid[r][c];
                            grid[r][c] = 0;
                            madeChanges = true;
                        }
                    }
                }
            }

            return madeChanges;
        }

        public bool MoveTilesHorizontal(bool goLeft, bool simulate = false)
        {
            int[] lastValue = new int[] { 0, 0, -1 };
            var madeChanges = false;
            var grid = GetGridForGameLogic(simulate);

            var start = grid.Length - 1;
            var end = -1;
            var direction = -1;

            if (goLeft)
            {
                start = 0;
                end = grid.Length;
                direction = 1;
            }

            for (int r = 0; r < grid.Length; r++)
            {
                //Merge 
                lastValue[2] = -1;

                for (int c = start; c != end; c+=direction)
                {

                    if (grid[r][c] == lastValue[2])
                    {
                        //Combine
                        var lr = lastValue[0];
                        var lc = lastValue[1];

                        grid[lr][lc] = grid[lr][lc] * 2;
                        grid[r][c] = 0;
                        lastValue[2] = -1;
                        madeChanges = true;

                        UpdatePlayerScore(grid[lr][lc]);

                    }
                    else if (grid[r][c] > 0)
                    {
                        //No match, update value
                        lastValue[0] = r;
                        lastValue[1] = c;
                        lastValue[2] = grid[r][c];
                    }
                }

                //Move
                for (int c = start; c != end; c+=direction)
                {
                    if (grid[r][c] > 0)
                    {
                        var newCol = GetEmptyColumnSpace(grid, r, direction);
                        var updatePosition = false;                       

                        if (!goLeft && newCol > c)
                        {
                            updatePosition = true;
                        }
                        else if (goLeft && newCol > -1 && newCol < c)
                        {
                            updatePosition = true;
                        }

                        if (updatePosition)
                        {
                            grid[r][newCol] = grid[r][c];
                            grid[r][c] = 0;
                            madeChanges = true;
                        }
                    }
                }
            }

            return madeChanges;
        }

        private int GetEmptyColumnSpace(int[][] grid, int row, int direction)
        {
            var start = 0;
            var end = grid[row].Length;

            if (direction < 0)
            {
                start = grid[row].Length - 1;
                end = -1;
            }
            
            for (int c = start;  c!=end; c+=direction)
            {
                if (grid[row][c] == 0)
                {
                    return c;
                }
            }
            return -1;
        }

        private int GetEmptyRowSpace(int[][] grid, int col, int direction)
        {
            var start = 0;
            var end = grid.Length - 1;

            if (direction < 0)
            {
                start = grid.Length - 1;
                end = 0;
            }

            for (int r = start; r != end; r += direction)
            {
                if (grid[r][col] == 0)
                {
                    return r;
                }
            }
            return -1;
        }

        public int[][] GetGrid()
        {
            return vGrid;
        }

        public int GetPlayerScore()
        {
            return playerScore;
        }

        public int GetHighScore()
        {
            return highScore;
        }

        public void SetHighScore(int highScore)
        {
            highScoreManager.SaveHighScore(highScore);
        }

        public bool CanMakeNextMove()
        {
            return MoveTilesVertical(true, true) || MoveTilesVertical(false, true) || 
                MoveTilesHorizontal(true, true) || MoveTilesHorizontal(false, true);
        }
    }
}
