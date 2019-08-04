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

    public int[,] GenerateZoneGates()
    {
        // parameters //////
        int size0 = 32;
        int size1 = 22;
        int size2 = 8;
        float growFactor = 0.75f;
        float openness = 0.25f;

        int cornerGateDist = 3;
        int edgeGateDist = 6;
        int centerGateDist = 2; 

        ////////////////////

        int size01 = (size0 + size1) / 2;
        int sCorner = (size0 - size1) / 2;
        int[] dxVal = new int[8] { 0, 0, size01, size01, 0, sCorner, sCorner, size01 };
        int[] dyVal = new int[8] { 0, size01, 0, size01, sCorner, 0, size01, sCorner };
        int[] xVal = new int[8] { sCorner, sCorner, sCorner, sCorner, sCorner, size1, size1, sCorner };
        int[] yVal = new int[8] { sCorner, sCorner, sCorner, sCorner, size1, sCorner, sCorner, size1 };

        int[,] maze = new int[2 * size0 + 1, 2 * size0 + 1];
        int len = maze.GetLength(0);

        for (int i = 0; i < 8; i++) {
            int[,] boundMaze = Generate(xVal[i], yVal[i], growFactor);
            for(int j = 0; j < boundMaze.GetLength(0); j++)
            {
                for(int k = 0; k < boundMaze.GetLength(1); k++)
                {
                    maze[dxVal[i] * 2 + j, dyVal[i] * 2 + k] = boundMaze[j, k];
                }
            }
        }

        bool[,] mask = new bool[size1, size1];
        int size12 = (size1 + size2) / 2;
        for(int i = size1 - size12; i < size12; i++)
        {
            for(int j = size1 - size12; j < size12; j++)
            {
                mask[i, j] = true;
            }
        }
        int[,] centerMaze = Generate(mask, growFactor);
        for(int i = 0; i < centerMaze.GetLength(0); i++)
        {
            for(int j = 0; j < centerMaze.GetLength(1); j++)
            {
                maze[i + sCorner * 2, j + sCorner * 2] = centerMaze[i, j]; 
            }
        }
        
        for(int i = size0 - size2 + 1; i < size0 + size2; i++)
        {
            for(int j = size0 - size2 + 1; j < size0 + size2; j++)
            {
                maze[i, j] = 0;
            }
        }

        for(int i = 0; i < len; i++)
        {
            for(int j = 0; j < len; j++)
            {
                if ((i % 2 != j % 2) &&
                    i != 0 && i != sCorner * 2 && i != len - 1 && i != len - sCorner * 2 - 1 &&
                    j != 0 && j != sCorner * 2 && j != len - 1 && j != len - sCorner * 2 - 1 &&
                       !((i >= size0 - size2 && i <= size0 + size2) && (j == size0 - size2 || j == size0 + size2)) &&
                       !((j >= size0 - size2 && j <= size0 + size2) && (i == size0 - size2 || i == size0 + size2)))
                {
                    maze[i, j] = Random.Range(0f, 1f) < openness ? 0 : maze[i, j];
                }
            }
        }

        for(int i = 2; i < len - 2; i += 2)
        {
            for(int j = 2; j < len - 2; j += 2)
            {
                int cnt = 0;
                for(int k = 0; k < 4; k++)
                {
                    if (maze[i + dx[k], j + dy[k]] == 0) cnt++;
                }
                if (cnt == 4) maze[i, j] = 0;
            }
        }

        maze[sCorner * 2, cornerGateDist * 2 - 1] = 2;
        maze[cornerGateDist * 2 - 1, sCorner * 2] = 2;
        maze[sCorner * 2, len - cornerGateDist * 2] = 2;
        maze[cornerGateDist * 2 - 1, len - sCorner * 2 - 1] = 2;
        maze[len - cornerGateDist * 2, sCorner * 2] = 2;
        maze[len - sCorner * 2 - 1, cornerGateDist * 2 - 1] = 2;
        maze[len - cornerGateDist * 2, len - sCorner * 2 - 1] = 2;
        maze[len - sCorner * 2 - 1, len - cornerGateDist * 2] = 2;

        maze[size0 - edgeGateDist * 2 + 1, sCorner * 2] = 2;
        maze[size0 + edgeGateDist * 2 - 1, sCorner * 2] = 2;
        maze[size0 - edgeGateDist * 2 + 1, len - sCorner * 2 - 1] = 2;
        maze[size0 + edgeGateDist * 2 - 1, len - sCorner * 2 - 1] = 2;
        maze[sCorner * 2, size0 - edgeGateDist * 2 + 1] = 2;
        maze[sCorner * 2, size0 + edgeGateDist * 2 - 1] = 2;
        maze[len - sCorner * 2 - 1, size0 - edgeGateDist * 2 + 1] = 2;
        maze[len - sCorner * 2 - 1, size0 + edgeGateDist * 2 - 1] = 2;

        maze[size0 - centerGateDist * 2 + 1, size0 - size2] = 0;
        maze[size0 + centerGateDist * 2 - 1, size0 - size2] = 0;
        maze[size0 - centerGateDist * 2 + 1, size0 + size2] = 0;
        maze[size0 + centerGateDist * 2 - 1, size0 + size2] = 0;
        maze[size0 - size2, size0 - centerGateDist * 2 + 1] = 0;
        maze[size0 - size2, size0 + centerGateDist * 2 - 1] = 0;
        maze[size0 + size2, size0 - centerGateDist * 2 + 1] = 0;
        maze[size0 + size2, size0 + centerGateDist * 2 - 1] = 0;

        //maze[len - cornerGateDist * 2 - 2, sCorner * 2] = 2;


        return maze;
    }

    public int[,] GenerateWithGates(int rows, int cols, int gates, int innerRows, int innerCols, 
        int loops, float growFactor)
    {
        int gateCount = 1;
        int[,] maze = Generate(rows, cols, innerRows, innerCols, growFactor);

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
        // j is for infinite loop killer
        for(int i = 0, j = 0; i < gates && j < 10000; j++)
        {
            int x, y;
            if (Random.Range(0, 2) == 0)
            {
                x = Random.Range(0, rows + 1) * 2;
                y = Random.Range(0, cols) * 2 + 1;
            }
            else
            {
                x = Random.Range(0, rows) * 2 + 1;
                y = Random.Range(0, cols + 1) * 2;
            }
            if (maze[x, y] == 0 && (x < x1 || x >= x2 || y < y1 || y >= y2) && (x != 0 && x != 2 * rows && y != 0 && y != 2 * cols))
            {
                maze[x, y] = 2; i++;
            }
        }

        // 3. wall -> gate randomly
        // but first, let's index the maze
        int[,] index = Indexing(maze);
        

        // j is for infinite loop killer
        for (int i = 0, j = 0; i < loops && j < 10000; j++)
        {
            int x, y;
            if (Random.Range(0, 2) == 0)
            {
                x = Random.Range(1, rows) * 2;
                y = Random.Range(0, cols) * 2 + 1;
                if (index[x - 1, y] == index[x + 1, y]) continue;
            }
            else
            {
                x = Random.Range(0, rows) * 2 + 1;
                y = Random.Range(1, cols) * 2;
                if (index[x, y - 1] == index[x, y + 1]) continue;
            }
            if (maze[x, y] == 1 && (x < x1 || x > x2 || y < y1 || y > y2))
            {
                maze[x, y] = 2;
                i++;
            }
            Debug.Log(x + " " + y + " " + maze[x, y] + " " + j);
        }


        return maze;
    }

    public int[,] Generate(int rows, int cols, int innerRows, int innerCols, float growFactor)
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
                    if (i == rows / 2 || j == cols / 2)
                    {
                        list.Insert(Random.Range(0, list.Count + 1), i * cols + j);
                    }
                }
            }

            for (int i = 2 * x1 + 1; i < 2 * x2; i++)
            {
                for(int j = 2 * y1 + 1; j < 2 * y2; j++)
                {
                    maze[i, j] = 0;
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

    public int[,] Generate(bool[,] mask, float growFactor)
    {
        int rows = mask.GetLength(0);
        int cols = mask.GetLength(1);
        int[,] maze = new int[rows * 2 + 1, cols * 2 + 1];
        bool[,] vis = new bool[rows, cols];
        List<int> list = new List<int>();
        InitializeMaze(maze);

        for (int i = 0; i < rows; i++)
        {
            for(int j = 0; j < cols; j++)
            {
                vis[i, j] = mask[i, j];
            }
        }

        int x, y;
        do
        {
            x = Random.Range(0, rows);
            y = Random.Range(0, cols);
        } while (vis[x, y]);

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
                sb.Append(maze[i, j] == 0 ? ".." : maze[i, j] == 1 ? "##" : "//");
            }
            sb.Append("\n");
        }

        return sb.ToString();
    }

    private int[,] Indexing(int[,] maze)
    {
        int value = 1;
        int[,] index = new int[maze.GetLength(0), maze.GetLength(1)];
        for(int i = 0; i < maze.GetLength(0); i++)
        {
            for(int j = 0; j < maze.GetLength(1); j++)
            {
                if (maze[i, j] == 0 || index[i, j] == 0) Fill(index, maze, i, j, value++);
            }
        }
        return index;
    }

    private void Fill(int[,] index, int[,] maze, int x, int y, int value)
    {
        if (x < 0 || x >= index.GetLength(0) || y < 0 || y >= index.GetLength(1)) return;
        if (index[x, y] != 0 || maze[x, y] != 0) return;
        index[x, y] = value;
        for (int i = 0; i < 4; i++)
        {
            Fill(index, maze, x + dx[i], y + dy[i], value);
        }
    }
}
