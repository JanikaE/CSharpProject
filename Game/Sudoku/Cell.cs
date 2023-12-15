using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    public class Cell
    {
        public int num;
        public bool canChange;
        public List<int> posibleNums;

        public int row;
        public int col;

        public Cell() 
        {
            num = 0;
            canChange = true;
            posibleNums = new List<int>();
        }
    }
}
