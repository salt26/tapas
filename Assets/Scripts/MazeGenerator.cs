using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    // Adjustable Maze Generator
    private static int[,] permutation;
    private float ratio;

    static MazeGenerator()
    {
        permutation = new int[24, 4]
        {
            {0, 1, 2, 3 },
            {0, 1, 3, 2 },
            {0, 2, 1, 3 },
            {0, 2, 3, 1 },
            {0, 3, 1, 2 },
            {0, 3, 2, 1 },
            {1, 0, 2, 3 },
            {1, 0, 3, 2 },
            {1, 2, 0, 3 },
            {1, 2, 3, 0 },
            {1, 3, 0, 2 },
            {1, 3, 2, 0 },
            {2, 0, 1, 3 },
            {2, 0, 3, 1 },
            {2, 1, 0, 3 },
            {2, 1, 3, 0 },
            {2, 3, 0, 1 },
            {2, 3, 1, 0 },
            {3, 0, 1, 2 },
            {3, 0, 2, 1 },
            {3, 1, 0, 2 },
            {3, 1, 2, 0 },
            {3, 2, 0, 1 },
            {3, 2, 1, 0 }
        };
    }

    public MazeGenerator()
    {
        ratio = .5f;
    }

    public void setRatio(float r)
    {
        ratio = r;
    }

    public float getRatio()
    {
        return ratio;
    }

    public int[,] FromDimensions(int rows, int cols, out string str)
    {
        int[,] maze = FromDimensions(rows, cols);

        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < 2 * cols + 1; i++)
        {
            for (int j = 0; j < 2 * rows + 1; j++)
            {
                sb.Append(maze[i, j] == 0 ? ".." : "##");
            }
            sb.Append("\n");
        }

        str = sb.ToString();
        return maze;
    }

    public int[,] FromDimensions(int rows, int cols)
    {
        int[,] maze = new int[2 * rows + 1, 2 * cols + 1];
        bool[,] vis = new bool[rows, cols];

        for (int i = 0; i < 2 * cols + 1; i++)
        {
            for (int j = 0; j < 2 * rows + 1; j++)
            {
                maze[i, j] = (i % 2 == 1 && j % 2 == 1) ? 0 : 1;
            }
        }

        int[] dx = new int[4] { -1, 0, 1, 0 };
        int[] dy = new int[4] { 0, -1, 0, 1 };
        List<int> list = new List<int>();
        int x = Random.Range(0, rows);
        int y = Random.Range(0, cols);
        list.Add(x * cols + y);
        vis[x, y] = true;
        while (list.Count > 0)
        {
            bool hasFind = false;
            int permutationIndex = Random.Range(0, 24);
            int index = Random.Range(0f, 1f) < ratio ? list.Count - 1 : Random.Range(0, list.Count);
            for (int i = 0; i < 4; i++)
            {
                int dir = permutation[permutationIndex, i];
                x = list[index] / cols + dx[dir];
                y = list[index] % cols + dy[dir];
                if (0 <= x && x < rows && 0 <= y && y < cols && !vis[x, y])
                {
                    list.Add(x * cols + y);
                    vis[x, y] = true;
                    hasFind = true;
                    maze[2 * x - dx[dir] + 1, 2 * y - dy[dir] + 1] = 0;
                    break;
                }
            }
            if (!hasFind)
            {
                list.RemoveAt(index);
            }
        }

        return maze;
    }
}
