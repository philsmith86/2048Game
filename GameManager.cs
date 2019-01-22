using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048Game
{
    class GameManager
    {
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
            for (int i = 0; i<vGrid.Length; i++){
                for (int j = 0; j<vGrid[i].Length; j++){
                    vGrid[i][j] = 0;
                }
            }
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
        public void MoveTilesUp()
        {

        }

        public void MoveTilesDown()
        {

        }

        public void MoveTilesLeft()
        {

        }

        public void MoveTilesRight()
        {

        }



        //Returns current vGrid
        public int[][] GetGrid()
        {
            return vGrid;
        }

    }
}
