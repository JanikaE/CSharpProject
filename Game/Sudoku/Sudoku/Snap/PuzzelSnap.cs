﻿using Sudoku.Game;

namespace Sudoku.Snap
{
    public struct PuzzelSnap
    {
        public int H;
        public int W;
        public CellSnap[,] playMat;

        public readonly int Length => H * W;
        public readonly int Square => Length * Length;

        public PuzzelSnap(Puzzel puzzel)
        {
            H = puzzel.H;
            W = puzzel.W;
            playMat = new CellSnap[H * W, H * W];
            for (int i = 0; i < H * W; i++)
            {
                for (int j = 0; j < H * W; j++)
                {
                    playMat[i, j] = new CellSnap(puzzel.PlayMat(i, j));
                }
            }
        }
    }
}
