using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator
{
    // Adjustable Maze Generator
    private static int[,] permutation;
    private static int[] dx, dy;

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
        dx = new int[4] { -1, 0, 1, 0 };
        dy = new int[4] { 0, 1, 0, -1 };
    }

    public int[,] GenerateWithGates(int rows, int cols, int innerRows, int innerCols, int innerGates, float growFactor, int gates)
    {
        int gateCount = 1;
        int[,] maze = Generate(rows, cols, innerRows, innerCols, innerGates, growFactor);

        // Currently, this maze only makes TREE structure

        // 0 : border gate
        switch (Random.Range(0, 4))
        {
            case 0:
                maze[0, cols] = 2;
                break;
            case 1:
                maze[2 * rows, cols] = 2;
                break;
            case 2:
                maze[rows, 0] = 2;
                break;
            case 3:
                maze[rows, 2 * cols] = 2;
                break;
            default:
                break;
        }

        // 1 : gates for inner rectangle
        int x1 = (rows - innerRows) / 2 * 2, x2 = (rows + innerRows) / 2 * 2;
        int y1 = (cols - innerCols) / 2 * 2, y2 = (cols + innerCols) / 2 * 2;
        for(int i = x1; i < x2; i += 2)
        {
            if (maze[i + 1, y1] != 1)
            {
                maze[i + 1, y1] = 2;
                gateCount++;
            }
            if (maze[i + 1, y2] != 1)
            {
                maze[i + 1, y2] = 2;
                gateCount++;
            }
        }
        for(int j = y1; j < y2; j += 2)
        {
            if (maze[x1, j + 1] != 1)
            {
                maze[x1, j + 1] = 2;
                gateCount++;
            }
            if (maze[x2, j + 1] != 1)
            {
                maze[x2, j + 1] = 2;
                gateCount++;
            }
        }

        // 2. put gates randomly
        // Maybe it'll be better to position gates at the CENTROID of the tree.
        while (gateCount < gates)
        {
            int x, y;
            if(Random.Range(0, 2) == 0)
            {
                x = Random.Range(0, cols + 1) * 2;
                y = Random.Range(0, rows) * 2 + 1;
            } else
            {
                x = Random.Range(0, cols) * 2 + 1;
                y = Random.Range(0, rows + 1) * 2;
            }
            if (maze[x, y] == 0 && (x < x1 || x >= x2 || y < y1 || y >= y2))
            {
                maze[x, y] = 2;
                gateCount++;
            }
        }
        

        return maze;
    }

    public int[,] Generate(int rows, int cols, int innerRows, int innerCols, int innerGates, float growFactor)
    {
        if(innerRows == 0 || innerCols == 0)
        {
            return Generate(rows, cols, growFactor);
        }
        else
        {
            int[,] maze = new int[2 * rows + 1, 2 * cols + 1];
            bool[,] vis = new bool[rows, cols];
            List<int> list = new List<int>();
            InitializeMaze(maze);

            int x1 = (rows - innerRows) / 2, x2 = (rows + innerRows) / 2;
            int y1 = (cols - innerCols) / 2, y2 = (cols + innerCols) / 2;
            for(int i = x1; i < x2; i++)
            {
                for(int j = y1; j < y2; j++)
                {
                    vis[i, j] = true;
                    if (i == x1 || i == x2 - 1 || j == y1 || j == y2 - 1)
                    {
                        list.Insert(Random.Range(0, list.Count + 1), i * cols + j);
                    }
                }
            }
            list.RemoveRange(innerGates, list.Count - innerGates);

            
            for(int i = x1 + 1; i < x2; i++)
            {
                for(int j = y1 + 1; j < y2; j++)
                {
                    maze[2 * i, 2 * j] = 0;
                }
            }
            for(int i = x1; i < x2; i++)
            {
                for(int j = y1 + 1; j < y2; j++)
                {
                    maze[2 * i + 1, 2 * j] = 0;
                }
            }
            for (int i = x1 + 1; i < x2; i++)
            {
                for (int j = y1; j < y2; j++)
                {
                    maze[2 * i, 2 * j + 1] = 0;
                }
            }
            TreeBuilder(maze, vis, list, growFactor);

            return maze;
        }
    }

    public int[,] Generate(int rows, int cols, float growFactor)
    {
        int[,] maze = new int[2 * rows + 1, 2 * cols + 1];
        bool[,] vis = new bool[rows, cols];
        List<int> list = new List<int>();
        InitializeMaze(maze);

        int x = Random.Range(0, rows);
        int y = Random.Range(0, cols);
        list.Add(x * cols + y);
        vis[x, y] = true;
        TreeBuilder(maze, vis, list, growFactor);
        
        return maze;
    }

    private void TreeBuilder(int[,] maze, bool[,] vis, List<int> list, float growFactor)
    {
        int rows = maze.GetLength(0) / 2;
        int cols = maze.GetLength(1) / 2;
        while (list.Count > 0)
        {
            bool hasFind = false;
            int permutationIndex = Random.Range(0, 24);
            int index = Random.Range(0f, 1f) < growFactor ? list.Count - 1 : Random.Range(0, list.Count);
            for (int i = 0; i < 4; i++)
            {
                int dir = permutation[permutationIndex, i];
                int x = list[index] / cols + dx[dir];
                int y = list[index] % cols + dy[dir];
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
    }

    private void InitializeMaze(int[,] maze)
    {
        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                maze[i, j] = (i % 2 == 1 && j % 2 == 1) ? 0 : 1;
            }
        }
    }

    public string ConvertToString(int[,] maze)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < maze.GetLength(0); i++)
        {
            for (int j = 0; j < maze.GetLength(1); j++)
            {
                sb.Append(maze[i, j] == 0 ? ".." : "##");
            }
            sb.Append("\n");
        }

        return sb.ToString();
    }
}
