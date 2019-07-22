using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour
{

    public float edgeLength;
    public GameObject[] corners;
    public GameObject[] edges;

    // public bool mazeFromFile = false;
    // Currently, only procedural maps are allowed

    public int columns = 15;
    public int rows = 15;
    public int gates = 8;
    public int innerColumns = 5;
    public int innerRows = 5;
    public int innerMaximumGates = 5;
    public int loops = 3;
    public float growFactor = 0.75f;
    public int minimumDepth = 4;

    private int[,] maze;

    void Start()
    {
        Cursor.visible = false;
        MazeGenerator mazeGenerator = new MazeGenerator();

        /*
        if (mazeFromFile)
        {
            string path = "Assets/Resources/Map.txt";

            StreamReader reader = new StreamReader(path);
            string line;
            int cols = System.Convert.ToInt32(reader.ReadLine());
            int rows = System.Convert.ToInt32(reader.ReadLine());
            maze = new int[2 * cols + 1, 2 * rows + 1];
            for(int i = 0; i < 2 * cols + 1; i++)
            {
                line = reader.ReadLine();
                for(int j = 0; j < 2 * rows + 1; j++)
                {
                    maze[i, j] = line[j * 2] == '#' ? 1 : 0;
                }
            }
            reader.Close();
        }
        */

        maze = mazeGenerator.GenerateWithGates(columns, rows, gates, innerColumns, innerRows,
            innerMaximumGates, loops, growFactor);

        Debug.Log(mazeGenerator.ConvertToString(maze));


        for(int i = 0; i < maze.GetLength(0); i++)
        {
            for(int j = 0; j < maze.GetLength(1); j++)
            {
                int type = i % 2 * 2 + j % 2;
                Vector3 wallPosition = new Vector3(edgeLength * j / 2f, 0f, edgeLength * i / 2f) + transform.position;
                if (maze[i, j] == 1) {
                    switch (type)
                    {
                        case 0:
                            Instantiate(corners[Random.Range(0, corners.Length)],
                                wallPosition, Quaternion.Euler(0, 90f * Random.Range(0, 4), 0));
                            break;
                        case 1:
                            Instantiate(edges[Random.Range(0, edges.Length)],
                                wallPosition, Quaternion.Euler(0, 90f + 180f * Random.Range(0, 2), 0));
                            break;
                        case 2:
                            Instantiate(edges[Random.Range(0, edges.Length)],
                                wallPosition, Quaternion.Euler(0, 180f * Random.Range(0, 2), 0));
                            break;
                        default:
                            break;
                    }
                }
                else if(maze[i, j] == 2)
                {
                    switch (type)
                    {
                        case 1:
                            Instantiate(edges[Random.Range(0, edges.Length)],
                                wallPosition, Quaternion.Euler(0, 180f * Random.Range(0, 2), 0));
                            break;
                        case 2:
                            Instantiate(edges[Random.Range(0, edges.Length)],
                                wallPosition, Quaternion.Euler(0, 90f + 180f * Random.Range(0, 2), 0));
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    void Update()
    {

    }
}
