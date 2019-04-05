using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Diagnostics;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            int solved = 0;
            for(int i = 0; i < 500000; i++)
            {
                if(stopWatch.ElapsedMilliseconds % 1000 == 0)
                {
                    Console.WriteLine((i-solved).ToString() + " solved per second");
                    System.Threading.Thread.Sleep(1);
                    solved = i;
                }
                second();
            }
            Console.WriteLine("Done!");
            Console.ReadLine();
        }
        static bool first()
        {
            int[][] tiles =
            {
                new int[] {0,0,0,0 },
                new int[] {4,0,2,0 },
                new int[] {3,0,4,0 },
                new int[] {0,0,1,0 }
            };
            int[][] orig ={
                new int[] {0,0,0,0 },
                new int[] {4,0,2,0 },
                new int[] {3,0,4,0 },
                new int[] {0,0,1,0 }
            };
            solve(tiles, orig);
            return true;
        }
        static bool second()
        {
            int[][] tiles =
{
                new int[] {5,3,0,0,7,0,0,0,0},
                new int[] {6,0,0,1,9,5,0,0,0},
                new int[] {0,9,8,0,0,0,0,6,0},
                new int[] {8,0,0,0,6,0,0,0,3},
                new int[] {4,0,0,8,0,3,0,0,1},
                new int[] {7,0,0,0,2,0,0,0,6},
                new int[] {0,6,0,0,0,0,2,8,0},
                new int[] {0,0,0,4,1,9,0,0,5},
                new int[] {0,0,0,0,8,0,0,7,9}

            };
            int[][] orig ={
                new int[] {5,3,0,0,7,0,0,0,0},
                new int[] {6,0,0,1,9,5,0,0,0},
                new int[] {0,9,8,0,0,0,0,6,0},
                new int[] {8,0,0,0,6,0,0,0,3},
                new int[] {4,0,0,8,0,3,0,0,1},
                new int[] {7,0,0,0,2,0,0,0,6},
                new int[] {0,6,0,0,0,0,2,8,0},
                new int[] {0,0,0,4,1,9,0,0,5},
                new int[] {0,0,0,0,8,0,0,7,9}
            };
            solve(tiles, orig);
            return true;
        }
        static void print(int[][] tiles, int[][] original, int? cx, int? cy)
        {
            Console.Write("\n");
            Console.Write("\n");

            for(int x = 0; x < tiles.Length; x++)
            {
                for (int y = 0; y < tiles.Length; y++)
                {
                    if (tiles[x][y] != 0)
                    {
                        int origin = original[x][y];
                        int newtile = tiles[x][y];
                        if(origin != newtile)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            if(x==cx && y == cy)
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                            }
                        }
                        else { 
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(tiles[x][y]);
                }
                Console.Write("\n");
            }
        }
        static int solve(int[][] tiles, int[][] original)
        {
            int numblanks = 1;
            int size = tiles.Length;
            Dictionary<int, int> possiblesInit = Enumerable.Range(1, size).ToList().Zip(Enumerable.Range(1, size).ToList(), (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
            //print(tiles, original,null,null);
            while (numblanks > 0)
            {
                numblanks = 0;
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        int tile = tiles[i][j];
                        if (tile == 0)
                        {
                            Dictionary<int, int> possibles = new Dictionary<int, int>(possiblesInit);
                            for (int k = 0; k < size; k++)
                            {
                                possibles.Remove(tiles[i][k]);
                            }
                            if (possibles.Count == 1)
                            {
                                tiles[i][j] = possibles.Keys.First();
                            }
                            else
                            {
                                for (int k = 0; k < size; k++)
                                {
                                    possibles.Remove(tiles[k][j]);
                                }
                            }
                            if (possibles.Count == 1)
                            {
                                tiles[i][j] = possibles.Keys.First();
                            }
                            else
                            {
                                int sqrt = (int)Math.Sqrt(size);
                                for (int m = ((int)i / sqrt) * sqrt; m < (((int)i / sqrt) + 1) * sqrt; m++)
                                {
                                    for (int l = ((int)j / sqrt) * sqrt; l < (((int)j / sqrt) + 1) * sqrt; l++)
                                    {
                                        possibles.Remove(tiles[m][l]);
                                    }

                                }
                            }
                            if (possibles.Count == 1)
                            {
                                tiles[i][j] = possibles.Keys.First();
                            }
                            else
                            {
                                
                                numblanks++;
                            }
                            if(tiles[i][j] != 0)
                            {
                               // print(tiles, original, i,j);
                                
                            }
                        }
                    }
                }
            }

            return 0;
        }
    }
}
