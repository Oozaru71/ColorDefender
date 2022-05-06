using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;


namespace ColorDefender
{
    public class Board
    {
        private Tile[,] tiles;
        private int cols;
        private int rows;
        
        public Board(int rows, int cols)
        {
            this.rows = rows;
            this.cols = cols;
            this.tiles = new Tile[rows,cols];
            startBoard();
        }

        public Tile[,] getTiles()
        {
            return tiles;
        }

        public void startBoard()
        {
            do
            {
                for (int row = 0; row < rows; row++)
                {
                    for (int col = 0; col < cols; col++)
                    {
                        tiles[row, col] = new Tile(row, col);
                    }

                }

            } while (!isMoveTrue());
        }
        public bool hasTileAt(int row, int col)
        {
            if (tiles[row,col] != null)
                return true;
            else
                return false;
        }
        public Boolean isMoveTrue() 
        {
            for(int col = 0; col < cols; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    int shape = tiles[row, col].getShape();
                    HashSet<Tile> matches = new HashSet<Tile>();
                    locateNexts(row, col, shape, matches);
                    if (matches.Count>= 3)
                        return true;
                }
            }
            return false;
        }
        public void locateNexts(int row, int col, int shape, HashSet<Tile>matches)
        {
            Tile temp=tiles[row, col];
            if (matches.Contains(temp))
            {
                return;
            }
            else
            {
                matches.Add(temp);
            }



            if (isTopMatch(row, col, shape))
            {
                locateNexts(row + 1, col, shape, matches);
            }

            if (isBottomMatch(row, col, shape))
            {
                locateNexts(row - 1, col, shape, matches);
            }
            
            if (isRightMatch(row, col, shape))
            { 
                locateNexts(row, col + 1, shape, matches);
            }

            if (isLeftMatch(row, col, shape))
            {
                locateNexts(row, col - 1, shape, matches);
            }

        }

        public void removeMatchingTiles(HashSet<Tile> validN)
        {
            foreach (Tile t in validN)
            {
                //System.Diagnostics.Debug.WriteLine(tiles[t.getRow(),t.getColumn()].getShapeName());
                removeTileAt(t.getRow(), t.getColumn());
               // System.Diagnostics.Debug.WriteLine("why do i exist"+ tiles[t.getRow(), t.getColumn()].getShapeName());

            }


        }

        public bool isTopMatch(int row, int col, int shape) 
        {
            if (row >= 0 && row <= rows - 2 && tiles[row + 1,col] != null && (tiles[row + 1,col].getShape() == shape || tiles[row+1, col].getShape() == 5))
                return true;
            else
                return false;
        }
        public bool isBottomMatch(int row, int col, int shape)
        {
            if (row >= 1 && row <= rows - 1 && tiles[row - 1, col] != null && (tiles[row - 1, col].getShape() == shape || tiles[row-1, col].getShape() == 5))
                return true;
            else
                return false;
        }
        
        public bool isLeftMatch(int row, int col, int shape)
        {
            if (col >= 1 && col <= cols - 1 && tiles[row, col - 1] != null &&( tiles[row, col -1].getShape() == shape|| tiles[row, col -1 ].getShape() == 5))
                return true;
            else
                return false;
        }
        public bool isRightMatch(int row, int col, int shape)
        {
            if (col >= 0 && col <= cols - 2 && tiles[row, col + 1] != null && (tiles[row, col + 1].getShape() == shape|| tiles[row, col + 1].getShape() == 5))
                return true;
            else
                return false;
        }
        public Tile tileAt(int row, int col)
        {
            return tiles[row,col];
        }

        public void removeTileAt(int row, int col)
        {
            tiles[row,col] = null;
        }
        private void collapseColumn(int colToColapse)
        {
            ArrayList newCol=new ArrayList();
            

            for (int row = 0; row < rows; row++)
            {
                if (hasTileAt(row, colToColapse))
                {
                    newCol.Add(tileAt(row, colToColapse));
                }
            }

            
            int tilesToAdd = rows - newCol.Count;

            newCol.Reverse();

            for (int i = 1; i <= tilesToAdd; i++)
            {
                newCol.Add(new Tile(newCol.Count + i, colToColapse));
            }

            newCol.Reverse();

            if (newCol.Count > 0)
            {
                for (int row = 0; row < newCol.Count; row++)
                {
                    tiles[row,colToColapse] = (Tile)newCol[row];
                    tiles[row,colToColapse].setRow(row);
                    tiles[row,colToColapse].setColumn(colToColapse);
                }

            }
        }

        public void showMe() {
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (tiles[row, col] != null)
                        System.Diagnostics.Debug.Write(tiles[row, col].getShapeName() + tiles[row, col].getRow() + "," + tiles[row, col].getColumn() + " ");
                    else
                        System.Diagnostics.Debug.Write("null.png n,n ");
                    if (col == cols - 1)
                    {
                        System.Diagnostics.Debug.WriteLine("");
                    }
                }

            }
        }
        public void turnTile()
        {
            int fRow=0;
            int fCol=0;
            do {
                var rand = new Random();
                 fRow = rand.Next(rows);
                 fCol = rand.Next(cols);
                tiles[fRow, fCol].turnBad();
            }
            while (tiles[fRow, fCol].getShape()!=-1);
        
        }
        public void turnRainbow()
        {
            int fRow = 0;
            int fCol = 0;
            do
            {
                var rand = new Random();
                fRow = rand.Next(rows);
                fCol = rand.Next(cols);
                tiles[fRow, fCol].turnRainbow();
            }
            while (tiles[fRow, fCol].getShape() != 5);

        }

        public void collapseColumns()
        {
            for (int col = 0; col < cols; col++)
            {
                collapseColumn(col);
            }
        }
    }
}
