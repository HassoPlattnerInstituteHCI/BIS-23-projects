using System.Collections;
using System.Collections.Generic;
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

    
    // Start is called before the first frame update
    void Start()
    {
        (bool, bool)[,] ar = new (bool, bool)[width, hight];    

        generateMaze(width, hight, ar);

        for(int i = 0; i<hight; i++){
            for(int j = 0; j<width; j++){
                if(ar[i, j].Item1){
                    if(ar[i, j].Item2){
                        Instantiate(bottomright, new Vector3(i, 1, j), Quaternion.identity);
                        continue;       
                    }   
                    Instantiate(right, new Vector3(i, 1, j), Quaternion.identity);
                    continue;
                }

                if(ar[i, j].Item2){
                    Instantiate(bottom, new Vector3(i, 1, j), Quaternion.identity);
                    continue;
                }

                Instantiate(nothing, new Vector3(i, 1, j), Quaternion.identity);
            }    
        }

        for(int i = -1; i<=hight; i++){
            Instantiate(Wall, new Vector3(i, 1, -1), Quaternion.identity);
            Instantiate(Wall, new Vector3(i, 1, width), Quaternion.identity);
        }
        for(int j = 0; j<width; j++){
            Instantiate(Wall, new Vector3(-1, 1, j), Quaternion.identity);
            Instantiate(Wall, new Vector3(hight, 1, j), Quaternion.identity);
        }
    }

    (bool, bool)[,] generateMaze(int w, int h, (bool, bool)[,] a){
        for(int i = 0; i<hight; i++){
            for(int j = 0; j<width; j++){
                a[i, j] = (Random.value >= 0.5, Random.value >= 0.5);
            }
        }
        return a;
    }

}
