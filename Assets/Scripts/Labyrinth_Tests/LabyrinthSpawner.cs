using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LabyrinthSpawner : MonoBehaviour
{
    public GameObject bottomright;
    public GameObject right;
    public GameObject bottom;
    public GameObject nothing;
    public GameObject Wall;

    public int width;
    public int hight;
    public int chance;

    
    // Start is called before the first frame update
    void Start()
    {
        (bool, bool)[,] ar = new (bool, bool)[width, hight];    

        ar = generate(width, hight, chance);

        for(int i = 0; i<width; i++){
            for(int j = 0; j<hight; j++){
                if(!ar[j, i].Item1){
                    if(!ar[j, i].Item2){
                        Instantiate(bottomright, new Vector3(i, 0.45f, j), Quaternion.identity);
                        continue;       
                    }   
                    Instantiate(right, new Vector3(i, 0.45f, j), Quaternion.identity);
                    continue;
                }

                if(!ar[j, i].Item2){
                    Instantiate(bottom, new Vector3(i, 0.45f, j), Quaternion.identity);
                    continue;
                }

                Instantiate(nothing, new Vector3(i, 0.45f, j), Quaternion.identity);
            }    
        }

        for(int i = -1; i<=hight; i++){
            Instantiate(Wall, new Vector3(i, 0.45f, -1), Quaternion.identity);
            Instantiate(Wall, new Vector3(i, 0.45f, width), Quaternion.identity);
        }
        for(int j = 0; j<width; j++){
            Instantiate(Wall, new Vector3(-1, 0.45f, j), Quaternion.identity);
            Instantiate(Wall, new Vector3(hight, 0.45f, j), Quaternion.identity);
        }
    }

    
        public (bool, bool)[,] generate(int w, int h, int chance) {
            Stack<(int, int)> fallbackPositions = new Stack<(int, int)>();
            // Random rnd = new Random();

            int cx = 0;
            int cy = 0; // with maximum (w, h)
            bool[,] visited = new bool[w,h]; // false = not visited
            (bool, bool)[,] maze = new (bool, bool)[w,h]; //rechts, unten
            int[] comparer = {0,0,0,0};
            
            fallbackPositions.Push((0,0));
            
            while (fallbackPositions.Count > 0) {
                // Console.WriteLine("position is " + cx.ToString()+ " "+ cy.ToString());

                visited[cx,cy] = true;

                int[] availableDirections = (int[])comparer.Clone();

                if (cy > 0 && !visited[cx,cy-1]) availableDirections[0] = 1; //up
                if (cx < w - 1 && !visited[cx+1,cy]) availableDirections[1] = 1; //right
                if (cy < h- 1 && !visited[cx,cy+1]) availableDirections[2] = 1; //down
                if (cx > 0 && !visited[cx-1,cy]) availableDirections[3] = 1; //left

                // foreach(var item in availableDirections)
                // {
                //     Console.WriteLine(item.ToString());
                // }

                if (availableDirections.SequenceEqual(comparer)) {
                    var cpos = fallbackPositions.Pop();
                    cx = cpos.Item1;
                    cy = cpos.Item2;
                    continue;
                }
                
                else if (availableDirections.Sum() > 1) {
                    fallbackPositions.Push((cx, cy));
                    //Example 0 1 1 0
                    int rand = Random.Range(1, availableDirections.Sum()+1); // this all is to choose the way in the case of multiple ways. Example 2
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
            for (int i = 0; i < w; i++) {
                for (int j = 0;j < h; j++) {
                    if (Random.Range(0, chance) == 0 && i<w-1) {
                        maze[i, j].Item1 = true;
                    }
                    if (Random.Range(0, chance) == 0 && i<h-1) {
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
            for (int i = 0; i < w; i++) {
                str += strS;
                str += strB;
            }
            str+="\n";
            strS = " ";
            for (int i = 0; i < w; i++) {
                row = "|";
                for (int j = 0;j < h; j++) {
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
                str = str + row + "\n";
            }
            Debug.Log(str);
            return maze;
        }
}
