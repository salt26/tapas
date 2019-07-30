using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour
{

    public float edgeLength;
    public GameObject[] corners;
    public GameObject[] edges;
    public GameObject[] doors;

    public bool mazeFromFile = false;

    // public int columns = 15;
    // public int rows = 15;
    // public int gates = 8;
    // public int innerColumns = 5;
    // public int innerRows = 5;
    // public int loops = 3;
    // public float growFactor = 0.75f;
    // public int minimumDepth = 4;

    private int[,] maze;

    void Start()
    {
        Cursor.visible = false;
        MazeGenerator mazeGenerator = new MazeGenerator();


        if (mazeFromFile)
        {
            string path = "Assets/Resources/Map.txt";

            StreamReader reader = new StreamReader(path);
            string line;
            int cols = System.Convert.ToInt32(reader.ReadLine());
            int rows = System.Convert.ToInt32(reader.ReadLine());
            maze = new int[2 * cols + 1, 2 * rows + 1];
            for (int i = 0; i < 2 * cols + 1; i++)
            {
                line = reader.ReadLine();
                for (int j = 0; j < 2 * rows + 1; j++)
                {
                    maze[i, j] = line[j * 2] == '#' ? 1 : line[j * 2] == '/' ? 2 : 0;
                }
            }
            reader.Close();
        }
        else
        {
            //maze = mazeGenerator.GenerateWithGates(columns, rows, gates, innerColumns, innerRows,
            //    loops, growFactor);
            maze = mazeGenerator.GenerateZoneGates();


            Debug.Log(mazeGenerator.ConvertToString(maze));
        }

        GameObject mazeGameObject = new GameObject("Maze");
        mazeGameObject.transform.position = transform.position;
        GameObject mazeWall1 = new GameObject("Wall");
        GameObject mazeWall2 = new GameObject("Door");
        mazeWall1.transform.parent = mazeGameObject.transform;
        mazeWall2.transform.parent = mazeGameObject.transform;


        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for(int j = 0; j < maze.GetLength(1); j++)
            {
                int type = i % 2 * 2 + j % 2;
                Vector3 wallPosition = new Vector3(edgeLength * j / 2f, 0f, edgeLength * i / 2f);
                if (maze[i, j] == 1) {
                    GameObject wall = null;
                    switch (type)
                    {
                        case 0:
                            wall = Instantiate(corners[Random.Range(0, corners.Length)],
                                wallPosition, Quaternion.Euler(0, 90f * Random.Range(0, 4), 0));
                            break;
                        case 2:
                            wall = Instantiate(edges[Random.Range(0, edges.Length)],
                                wallPosition, Quaternion.Euler(0, 90f + 180f * Random.Range(0, 2), 0));
                            break;
                        case 1:
                            wall = Instantiate(edges[Random.Range(0, edges.Length)],
                                wallPosition, Quaternion.Euler(0, 180f * Random.Range(0, 2), 0));
                            break;
                        default:
                            break;
                    }
                    if (wall != null) wall.transform.parent = mazeWall1.transform;
                }
                else if(maze[i, j] == 2)
                {
                    GameObject wall = null;
                    switch (type)
                    {
                        case 1:
                            wall = Instantiate(doors[Random.Range(0, doors.Length)],
                                wallPosition, Quaternion.Euler(0, 180f * Random.Range(0, 2), 0));
                            break;
                        case 2:
                            wall = Instantiate(doors[Random.Range(0, doors.Length)],
                                wallPosition, Quaternion.Euler(0, 90f + 180f * Random.Range(0, 2), 0));
                            break;
                        default:
                            break;
                    }
                    if (wall != null)
                    {
                        wall.transform.parent = mazeWall2.transform;
                        wall.name = i + "//" + j;
                    }
                }
            }
        }

    }

    void Update()
    {

    }
}
