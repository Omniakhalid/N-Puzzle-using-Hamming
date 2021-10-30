using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    class Node
    {
        public int[,] grid; // stat of the game, 0-based
        public int h; // hurastic value
        public int g; // cost to reach this node from the start node
        public int f; // h + g
        public int x; // x location of the empty slot
        public int y; // y loaction if the empty slot
        public Node parent;
        public static int size;
        Node(Node parent)
        {
            grid = parent.grid.Clone() as int[,];
            g = parent.g + 1;
            this.parent = parent;
            this.x = parent.x;
            this.y = parent.y;
        }
        public Node(int[,] arr, int size)
        {
            Node.size = size;
            grid = new int[size, size];
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    grid[i, j] = arr[i, j];
                    if (arr[i, j] == 0)
                    {
                        x = i;
                        y = j;
                    }
                }
            }
            f = 0;
        }


        public Node Move_Up(Node parent)
        {
            Node child = new Node(parent);
            child.grid[parent.x, parent.y] = parent.grid[parent.x + 1, parent.y];
            child.grid[parent.x + 1, parent.y] = parent.grid[parent.x, parent.y];
            child.x = parent.x + 1;
            child.y = parent.y;
            child.h = Find_Hamming_Distance(child);
            child.f = child.h + child.g;
            return child;
        }

        public Node Move_Down(Node parent)
        {
            Node child = new Node(parent);
            child.grid[parent.x, parent.y] = parent.grid[parent.x - 1, parent.y];
            child.grid[parent.x - 1, parent.y] = parent.grid[parent.x, parent.y];
            child.x = parent.x - 1;
            child.y = parent.y;
            child.h = Find_Hamming_Distance(child);
            child.f = child.h + child.g;
            return child;
        }

        public Node Move_Right(Node parent)
        {
            Node child = new Node(parent);
            child.grid[parent.x, parent.y] = parent.grid[parent.x, parent.y - 1];
            child.grid[parent.x, parent.y - 1] = parent.grid[parent.x, parent.y];
            child.y = parent.y - 1 ;
            child.x = parent.x;
            child.h = Find_Hamming_Distance(child);
            child.f = child.h + child.g;
            return child;
        }

        public Node Move_Left(Node parent)
        {
            Node child = new Node(parent);
            child.grid[parent.x, parent.y] = parent.grid[parent.x, parent.y + 1];
            child.grid[parent.x, parent.y + 1] = parent.grid[parent.x, parent.y];
            child.y= parent.y + 1;
            child.x = parent.x;
            child.h = Find_Hamming_Distance(child);
            child.f = child.h + child.g;
            return child;
        }

        public int Find_Hamming_Distance(Node node)
        {
            int h = 0;
            int count = 0;
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    if (i == size - 1 && j == size - 1)
                        count = 0;
                    else
                        count++;
                
                    if (node.grid[i, j] != count)
                    {
                        h++;
                    }
                 
                }
            }
            return h;
        }

        public void Print_State()
        {
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    Console.Write(grid[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public bool Is_Solvable()
        {
            //convert the 2D array to 1D for easier calculations
            int k = 0;
            int[] arr = new int[size*size];
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    arr[k] = grid[i, j];
                    k++;
                }
            }

            //finding number of inversions
            int num_inver = 0;

            for(int i = 0; i < size * size; i++)
            {
                for(int j = i + 1; j < size * size; j++)
                {
                    if (arr[j] != 0 && arr[j] != 0 && arr[i] > arr[j])
                        num_inver++;
                }
            }

            if(size % 2 != 0) //if odd
            {
                if (num_inver % 2 == 0)
                {
                    return true;
                }
                else
                    return false;
            }
            else //if even
            {
                if ((size - x) % 2 == 0 && num_inver % 2 != 0)
                    return true;
                else if ((size - x) % 2 != 0 && num_inver % 2 == 0)
                    return true;
                else 
                    return false;
            }
        }

        public bool Is_Final_State()
        {
            int count = 1;
            int[,] arr = new int[size, size];
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    arr[i, j] = count;
                    count++;
                }
            }
            arr[size - 1, size - 1] = 0;

            return Is_Equal(arr);
            
        }

        public bool Is_Equal(int [,] arr)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (arr[i, j] != grid[i, j])
                        return false;
                }
            }
            return true;
        }
    }
}
