using System;
using System.Collections.Generic;
using System.Linq;

namespace bis{
    //return: false -> mauer, true -> keine Mauer
    class Mazegen {

        public void test(){
            // Console.WriteLine("test worked");
        }

        public (bool, bool)[,] generate(int width, int height, int chance) {
            Stack<(int, int)> fallbackPositions = new Stack<(int, int)>();
            Random rnd = new Random();

            int cx = 0;
            int cy = 0; // with maximum (width, height)
            bool[,] visited = new bool[width,height]; // false = not visited
            (bool, bool)[,] maze = new (bool, bool)[width,height]; //rechts, unten
            int[] comparer = {0,0,0,0};
            
            fallbackPositions.Push((0,0));
            
            while (fallbackPositions.Count > 0) {
                // Console.WriteLine("position is " + cx.ToString()+ " "+ cy.ToString());

                visited[cx,cy] = true;

                int[] availableDirections = (int[])comparer.Clone();

                if (cy > 0 && !visited[cx,cy-1]) availableDirections[0] = 1; //up
                if (cx < width - 1 && !visited[cx+1,cy]) availableDirections[1] = 1; //right
                if (cy < height- 1 && !visited[cx,cy+1]) availableDirections[2] = 1; //down
                if (cx > 0 && !visited[cx-1,cy]) availableDirections[3] = 1; //left

                foreach(var item in availableDirections)
                {
                    // Console.WriteLine(item.ToString());
                }

                if (availableDirections.SequenceEqual(comparer)) {
                    var cpos = fallbackPositions.Pop();
                    cx = cpos.Item1;
                    cy = cpos.Item2;
                    continue;
                }
                
                else if (availableDirections.Sum() > 1) {
                    fallbackPositions.Push((cx, cy));
                    //Example 0 1 1 0
                    int rand = rnd.Next(1, availableDirections.Sum()+1); // this all is to choose the way in the case of multiple ways. Example 2
                    int counter = 0;
                    for (int i = 0; i < 4; i++) {
                        if (availableDirections[i] == 1) counter++; //first it.: nothing. second: counter 1, rand 2. third: counter 2, rand 2 -> break
                        if (rand == counter) {
                            availableDirections = (int[])comparer.Clone();
                            availableDirections[i] = 1;
                            break;
                        };
                    }
                }
                 
                //the following part expects only one direction to be available
                if (availableDirections[0] == 1) {//up
                    maze[cx,cy-1].Item2 = true;
                    cy--;
                } else if (availableDirections[1] == 1) {//right
                    maze[cx,cy].Item1 = true;
                    cx++;
                } else if (availableDirections[2] == 1) {//down
                    maze[cx,cy].Item2 = true;
                    cy++;
                } else if (availableDirections[3] == 1) {//left
                    maze[cx-1,cy].Item1 = true;
                    cx--;
                }
            }
            for (int i = 0; i < width; i++) {
                for (int j = 0;j < height; j++) {
                    if (rnd.Next(0, chance) == 0 && i<width-1) {
                        maze[i, j].Item1 = true;
                    }
                    if (rnd.Next(0, chance) == 0 && i<height-1) {
                        maze[j, i].Item2 = true; //kp warum einmal i,j und einmal j,i aber nur so funktionierts. wirkt eig sehr unlogisch
                    }
                }
            }

            //visual rep:
            
            string strE = "|";
            string strS = " ";
            string strB = "_";
            string row = strE;
            string str = "";
            for (int i = 0; i < width; i++) {
                str += strS;
                str += strB;
            }
            // Console.WriteLine(str);
            strS = " ";
            for (int i = 0; i < width; i++) {
                row = "|";
                for (int j = 0;j < height; j++) {
                    if (!maze[j, i].Item2) {
                        row += strB;
                    } else {
                        row += strS;
                    }
                    if (!maze[j,i].Item1) {
                        row += strE;
                    } else {
                        row += strS;
                    }
                }
                // Console.WriteLine(row);
            }
            // Console.WriteLine(maze.ToString());
            return maze;
        }
    }

    class Program
    {
        static void Main()
        {
            Mazegen mymaze = new Mazegen();
            mymaze.generate(10, 10, 20);
        }
    }
}
//*/