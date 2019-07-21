using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameController : MonoBehaviour
{

    public float edgeLength;
    public GameObject[] corners;
    public GameObject[] edges;

    public bool mazeFromFile = false;

    public int mazeColumns = 15;
    public int mazeRows = 15;
    public int mazeInnerColumns = 5;
    public int mazeInnerRows = 5;

    private int[,] m_Maze;

    void Start()
    {
        Cursor.visible = false;
        MazeGenerator m_MazeGenerator = new MazeGenerator();
        // simple setting
        m_MazeGenerator.setRatio(.75f);

        if (mazeFromFile)
        {
            string path = "Assets/Resources/Map.txt";

            StreamReader reader = new StreamReader(path);
            string line;
            int cols = System.Convert.ToInt32(reader.ReadLine());
            int rows = System.Convert.ToInt32(reader.ReadLine());
            m_Maze = new int[2 * cols + 1, 2 * rows + 1];
            for(int i = 0; i < 2 * cols + 1; i++)
            {
                line = reader.ReadLine();
                for(int j = 0; j < 2 * rows + 1; j++)
                {
                    m_Maze[i, j] = line[j * 2] == '#' ? 1 : 0;
                }
            }
            reader.Close();
        }
        else
        {
            m_Maze = m_MazeGenerator.FromDimensions(mazeColumns, mazeRows, mazeInnerColumns, mazeInnerRows);
            // make two entrances
            m_Maze[0, 1] = 0;
            m_Maze[2 * mazeColumns, 2 * mazeRows - 1] = 0;

            Debug.Log(m_MazeGenerator.ConvertToString(m_Maze));
        }


        for(int i = 0; i < m_Maze.GetLength(0); i++)
        {
            for(int j = 0; j < m_Maze.GetLength(1); j++)
            {
                if (m_Maze[i, j] == 0) continue;
                int type = i % 2 * 2 + j % 2;
                switch (type)
                {
                    case 0:
                        Instantiate(corners[Random.Range(0, corners.Length)], 
                            new Vector3(edgeLength * j / 2f, 0f, edgeLength * i / 2f) + transform.position,
                            Quaternion.Euler(0, 90f * Random.Range(0, 4), 0));
                        break;
                    case 1:
                        Instantiate(edges[Random.Range(0, edges.Length)],
                            new Vector3(edgeLength * j / 2f, 0f, edgeLength * i / 2f) + transform.position, 
                            Quaternion.Euler(0, 90f + 180f * Random.Range(0, 2), 0));
                        break;
                    case 2:
                        Instantiate(edges[Random.Range(0, edges.Length)],
                            new Vector3(edgeLength * j / 2f, 0f, edgeLength * i / 2f) + transform.position, 
                            Quaternion.Euler(0, 180f * Random.Range(0, 2), 0));
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void Update()
    {

    }
}
