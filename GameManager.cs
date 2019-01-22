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

        //Moves tiles in the specified direction
        public bool MoveTilesUp()
        {

            int[] lastValue = new int[] { 0, 0, -1 };
            var madeChanges = false;

            for (int c = 0; c < vGrid[0].Length; c++)
            {
                //Merge 
                lastValue[2] = -1;

                for (int r = 0; r<vGrid.Length; r++)
                {

                    if (vGrid[r][c] == lastValue[2])
                    {
                        //Combine
                        var lr = lastValue[0];
                        var lc = lastValue[1];

                        vGrid[lr][lc] = vGrid[lr][lc] * 2;
                        vGrid[r][c] = 0;
                        lastValue[2] = -1;
                        madeChanges = true;

                    }
                    else if (vGrid[r][c] > 0)
                    {
                        //No match, update value
                        lastValue[0] = r;
                        lastValue[1] = c;
                        lastValue[2] = vGrid[r][c];
                    }
                }

                //Move
                for (int r = 0; r<vGrid.Length; r++)
                {
                    if (vGrid[r][c] > 0)
                    {
                        var newRow = GetEmptyRowSpace(c, 1);
                        if (newRow > -1)
                        {
                            vGrid[newRow][c] = vGrid[r][c];
                            vGrid[r][c] = 0;
                            madeChanges = true;
                        }
                    }
                }
            }

            return madeChanges;
        }

        public bool MoveTilesDown()
        {
            int[] lastValue = new int[] { 0, 0, -1 };
            var madeChanges = false;

            for (int c = 0; c < vGrid[0].Length; c++)
            {
                //Merge 
                lastValue[2] = -1;

                for (int r = vGrid.Length - 1; r >= 0; r--)
                {

                    if (vGrid[r][c] == lastValue[2])
                    {
                        //Combine
                        var lr = lastValue[0];
                        var lc = lastValue[1];

                        vGrid[lr][lc] = vGrid[lr][lc] * 2;
                        vGrid[r][c] = 0;
                        lastValue[2] = -1;
                        madeChanges = true;

                    }
                    else if (vGrid[r][c] > 0)
                    {
                        //No match, update value
                        lastValue[0] = r;
                        lastValue[1] = c;
                        lastValue[2] = vGrid[r][c];
                    }
                }

                //Move
                for (int r = vGrid.Length - 1; r >= 0; r--)
                {
                    if (vGrid[r][c] > 0)
                    {
                        var newRow = GetEmptyRowSpace(c, -1);
                        if (newRow > -1)
                        {
                            vGrid[newRow][c] = vGrid[r][c];
                            vGrid[r][c] = 0;
                            madeChanges = true;
                        }
                    }
                }
            }

            return madeChanges;
        }

        public bool MoveTilesLeft()
        {
            int[] lastValue = new int[] { 0, 0, -1 };
            var madeChanges = false;

            for (int r = 0; r < vGrid.Length; r++)
            {
                //Merge 
                lastValue[2] = -1;

                for (int c = 0; c<vGrid.Length; c++)
                {

                    if (vGrid[r][c] == lastValue[2])
                    {
                        //Combine
                        var lr = lastValue[0];
                        var lc = lastValue[1];

                        vGrid[lr][lc] = vGrid[lr][lc] * 2;
                        vGrid[r][c] = 0;
                        lastValue[2] = -1;
                        madeChanges = true;

                    }
                    else if (vGrid[r][c] > 0)
                    {
                        //No match, update value
                        lastValue[0] = r;
                        lastValue[1] = c;
                        lastValue[2] = vGrid[r][c];
                    }
                }

                //Move
                for (int c =0; c<vGrid.Length; c++)
                {
                    if (vGrid[r][c] > 0)
                    {
                        var newCol = GetEmptyColumnSpace(r, 1);
                        if (newCol > -1)
                        {
                            vGrid[r][newCol] = vGrid[r][c];
                            vGrid[r][c] = 0;
                            madeChanges = true;
                        }
                    }
                }
            }

            return madeChanges;
        }

        public bool MoveTilesRight()
        {
            int[] lastValue = new int[] { 0, 0, -1 };
            var madeChanges = false;

            for (int r = 0; r < vGrid.Length; r++)
            {
                //Merge 
                lastValue[2] = -1;

                for (int c = vGrid.Length - 1; c >= 0; c--)
                {

                    if (vGrid[r][c] == lastValue[2])
                    {
                        //Combine
                        var lr = lastValue[0];
                        var lc = lastValue[1];

                        vGrid[lr][lc] = vGrid[lr][lc] * 2;
                        vGrid[r][c] = 0;
                        lastValue[2] = -1;
                        madeChanges = true;

                    }
                    else if (vGrid[r][c] > 0)
                    {
                        //No match, update value
                        lastValue[0] = r;
                        lastValue[1] = c;
                        lastValue[2] = vGrid[r][c];
                    }
                }

                //Move
                for (int c = vGrid.Length - 1; c >= 0; c--)
                {
                    if (vGrid[r][c] > 0)
                    {
                        var newCol = GetEmptyColumnSpace(r, -1);
                        if (newCol > -1)
                        {
                            vGrid[r][newCol] = vGrid[r][c];
                            vGrid[r][c] = 0;
                            madeChanges = true;
                        }
                    }
                }
            }

            return madeChanges;
        }

        private int GetEmptyColumnSpace(int row, int direction)
        {
            var start = 0;
            var end = vGrid[row].Length-1;

            if (direction < 0)
            {
                start = vGrid[row].Length - 1;
                end = 0;
            }
            
            for (int c = start;  c!=end; c+=direction)
            {
                if (vGrid[row][c] == 0)
                {
                    return c;
                }
            }
            return -1;
        }

        private int GetEmptyRowSpace(int col, int direction)
        {
            var start = 0;
            var end = vGrid.Length - 1;

            if (direction < 0)
            {
                start = vGrid.Length - 1;
                end = 0;
            }

            for (int r = start; r != end; r += direction)
            {
                if (vGrid[r][col] == 0)
                {
                    return r;
                }
            }
            return -1;
        }


        //Returns current vGrid
        public int[][] GetGrid()
        {
            return vGrid;
        }

    }
}
