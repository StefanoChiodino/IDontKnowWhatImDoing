﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace I_Dont_Know_What_Im_doing
{
    class Program
    {
        private static int XSize = 50;
        private static int YSize = 50;
        private static Cell[,] Cells;
        static readonly Random Random = new Random();
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            // - 2 Prevents horizontal or vertical taskbar
            Console.BufferHeight = XSize = Console.LargestWindowWidth - 2;
            Console.BufferWidth = YSize = Console.LargestWindowHeight - 2;
            Cells = new Cell[XSize, YSize];
            Console.SetWindowSize(XSize, YSize);
            InitCells(Cells);
            //var loop = new Task(() =>
            //{
            do
            {
                RefreshCells(Cells);
                PrintToConsolle(Cells);

                Thread.Sleep(300);
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true);
                    InitCells(Cells);
                }
            } while (true);
            //});
            //loop.Start();

        }

        private static void RefreshCells(Cell[,] cells)
        {
            Enumerable.Range(0, XSize).ToList().ForEach(x => Enumerable.Range(0, YSize).ToList().ForEach(y =>
            {
                cells[x, y].C = (char)Math.Round(Neighbourhood(cells, x, y));
            }));
        }

        private static decimal Neighbourhood(Cell[,] cells, int x, int y)
        {
            List<decimal> values = new List<decimal>();

            int startPosX = (x - 1 < 0) ? x : x - 1;
            int startPosY = (y - 1 < 0) ? y : y - 1;
            int endPosX = (x >= XSize - 1) ? x : x + 1;
            int endPosY = (y >= YSize - 1) ? y : y + 1;


            // See how many are alive
            for (int rowNum = startPosX; rowNum <= endPosX; rowNum++)
            {
                for (int colNum = startPosY; colNum <= endPosY; colNum++)
                {
                    values.Add(cells[rowNum, colNum].C);
                }
            }
            return values.Average();
        }

        private static void InitCells(Cell[,] cells)
        {
            Enumerable.Range(0, XSize).ToList().ForEach(x => Enumerable.Range(0, YSize).ToList().ForEach(y =>
            {
                cells[x, y] = new Cell(GetRandomChar());
            }));
        }

        private static void PrintToConsolle(Cell[,] cells)
        {
            Console.CursorLeft = Console.CursorTop = 0;
            var output = string.Empty;
            for (int y = 0; y < cells.GetUpperBound(1); y++)
            {
                for (int x = 0; x < cells.GetUpperBound(0); x++)
                {
                    output += cells[x, y].C;
                }
                output += Environment.NewLine;
            }
            Console.Write(output);
            //lines.ForEach(Console.WriteLine);
        }
        public static char GetRandomChar()
        {
            // This method returns a Random lowercase letter.
            // ... Between 'a' and 'z' inclusize.
            int num = Random.Next(31, 127); // Zero to 25
            char let = (char)(num);
            return let;
        }
    }

    internal class Cell
    {
        public Cell(char c)
        {
            C = c;
        }
        public char C = new char();
    }
}