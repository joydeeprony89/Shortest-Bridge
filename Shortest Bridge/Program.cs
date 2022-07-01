using System;
using System.Collections.Generic;

namespace Shortest_Bridge
{
  class Program
  {
    static void Main(string[] args)
    {
      /*
       *  grid = [[1,1,1,1,1],[1,0,0,0,1],[1,0,1,0,1],[1,0,0,0,1],[1,1,1,1,1]]
       *  Output: 1
       */
      Console.WriteLine("Hello World!");
    }
  }

  public class Solution
  {
    public int ShortestBridge(int[][] grid)
    {
      // As we know there are two island , first we need to find one of the island so that we can start the BFS for that island and find the no of min 0's bw another island
      // BFS why ? consider this as a graph, at each level we would like to change 0 to 1, so no of 0's which we need to convert to 1 to join both the island would be the no of level in bw, thats why we do BFS.
      // Step 1 - DO DFS , fill the visited set, which is the one of the island
      // Step 2 - once the visited set is ready, we can create a Queue out of this to start the BFS

      int ROW = grid.Length;
      int COLUMN = grid[0].Length;
      var directions = new int[4][] { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 } };
      var visited = new bool[ROW, COLUMN];
      Queue<(int, int)> q = new Queue<(int, int)>();

      bool InValidBoundary(int i, int j)
      {
        return i < 0 || j < 0 || i >= ROW || j >= COLUMN;
      }

      void DFS(int r, int c)
      {
        if (InValidBoundary(r, c) || visited[r, c] || grid[r][c] == 0) return;

        visited[r, c] = true;
        q.Enqueue((r, c));
        foreach (var dir in directions)
        {
          int newR = r + dir[0];
          int newC = c + dir[1];
          DFS(newR, newC);
        }
      }

      int BFS()
      {
        int count = 0;
        while (q.Count > 0)
        {
          int size = q.Count;
          while (size-- > 0)
          {
            var (r, c) = q.Dequeue();
            foreach (var dir in directions)
            {
              int newR = r + dir[0];
              int newC = c + dir[1];
              if (!InValidBoundary(newR, newC) && !visited[newR, newC])
              {
                if (grid[newR][newC] == 1)
                {
                  return count;
                }

                q.Enqueue((newR, newC));
                visited[newR, newC] = true;
              }
            }
          }
          count++;
        }
        return -1;
      }

      bool isFill = false;
      for (int i = 0; i < ROW; i++)
      {
        if (isFill)
        {
          break;
        }
        for (int j = 0; j < COLUMN; j++)
        {
          if (grid[i][j] == 1)
          {
            DFS(i, j);
            // we have to find the first island only, once found break this loop
            isFill = true;
            break;
          }
        }
      }

      return BFS();
    }
  }
}
