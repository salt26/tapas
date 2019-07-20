using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public float edgeLength;
    public GameObject[] corners;
    public GameObject[] edges;

    public int mazeColumns;
    public int mazeRows;

    private int[,] m_Maze;

    void Start()
    {
        Cursor.visible = false;
        MazeGenerator m_MazeGenerator = new MazeGenerator();
        m_Maze = m_MazeGenerator.FromDimensions(mazeColumns, mazeRows);

        // make two entrances
        m_Maze[0, 1] = 0;
        m_Maze[2 * mazeColumns, 2 * mazeRows - 1] = 0;

        for(int i = 0; i < 2 * mazeColumns + 1; i++)
        {
            for(int j = 0; j < 2 * mazeRows + 1; j++)
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
