using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class MazeController : MonoBehaviour
{
    public float edgeLength;
    public float height;
    public GameObject[] corners;
    public GameObject[] edges;
    public GameObject[] doors;
    public GameObject switches;
    public float[] addProbability;

    public enum Input
    {
        fromText,
        fromImage,
        None
    }
    public Input input = Input.fromText;

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


        if (input == Input.fromText)
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
        else if(input == Input.None)
        {
            //maze = mazeGenerator.GenerateWithGates(columns, rows, gates, innerColumns, innerRows,
            //    loops, growFactor);
            maze = mazeGenerator.GenerateZoneGates();
            Debug.Log(mazeGenerator.ConvertToString(maze));
        }
        else
        {
            Sprite sprite = Resources.Load<Sprite>("Map");
            Debug.Log(sprite.texture.width);

            maze = new int[sprite.texture.width, sprite.texture.height];

            for(int i = 0; i < maze.GetLength(0); i++)
            {
                for(int j = 0; j < maze.GetLength(1); j++)
                {
                    Color color = sprite.texture.GetPixel(i, j);
                    if(color == Color.black)
                    {
                        maze[i, j] = 1;
                    }
                    else if(color == Color.red)
                    {
                        maze[i, j] = 2;
                    }
                    else if(color == Color.blue)
                    {
                        maze[i, j] = 3;
                    }
                }
            }
        }

        GameObject mazeGameObject = new GameObject("Maze");
        mazeGameObject.transform.position = transform.position;
        GameObject mazeWall1 = new GameObject("Wall");
        GameObject mazeWall2 = new GameObject("Door");
        GameObject mazeSwitch = new GameObject("Switch");
        mazeWall1.transform.parent = mazeGameObject.transform;
        mazeWall2.transform.parent = mazeGameObject.transform;
        mazeSwitch.transform.parent = mazeGameObject.transform;


        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for(int j = 0; j < maze.GetLength(1); j++)
            {
                int type = i % 2 * 2 + j % 2;
                if (maze[i, j] == 1)
                {
                    for (int k = 0; k < addProbability.Length; k++)
                    {
                        if (Random.Range(0f, 1f) > addProbability[k]) break;
                        Vector3 wallPosition = new Vector3(edgeLength * j / 2f, height * k, edgeLength * i / 2f);

                        GameObject wall = null;
                        switch (type)
                        {
                            case 0:
                                wall = Instantiate(corners[Random.Range(0, corners.Length)],
                                    wallPosition, Quaternion.Euler(-90f, 90f * Random.Range(0, 4), 0));
                                break;
                            case 1:
                                wall = Instantiate(edges[Random.Range(0, edges.Length)],
                                    wallPosition, Quaternion.Euler(-90f, 90f + 180f * Random.Range(0, 2), 0));
                                break;
                            case 2:
                                wall = Instantiate(edges[Random.Range(0, edges.Length)],
                                    wallPosition, Quaternion.Euler(-90f, 180f * Random.Range(0, 2), 0));
                                break;
                            default:
                                break;
                        }
                        if (wall != null)
                        {
                            wall.transform.parent = mazeWall1.transform;
                            wall.name = i + ", " + j;
                            //wall.transform.localScale = Vector3.Scale(wall.transform.localScale,
                            //    new Vector3(1f, Random.Range(1f - irregularity, 1f + irregularity), 1f));
                        }
                    }
                }
                else if (maze[i, j] == 2)
                {
                    GameObject wall = null;
                    Vector3 wallPosition = new Vector3(edgeLength * j / 2f, 0f, edgeLength * i / 2f);
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
                        wall.name = i + ", " + j;
                    }
                }
                else if (maze[i, j] == 3)
                {
                    Vector3 wallPosition = new Vector3(edgeLength * j / 2f, 0f, edgeLength * i / 2f);
                    GameObject floorSwitch = Instantiate(switches, wallPosition, Quaternion.Euler(0, 90f * Random.Range(0, 4), 0));
                    floorSwitch.transform.parent = mazeSwitch.transform;
                    floorSwitch.name = i + ", " + j;
                }
            }
        }

    }

    void Update()
    {

    }
}
