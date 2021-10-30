using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace N_Puzzle
{
    class Main_prog
    {
        static void Main(string[] args)
        {
            //Start to read matrix from file

            string[] lineItems;

            int size;

            FileStream fille = new FileStream("C:\\Users\\Omnia\\Desktop\\UNI\\1st Term\\Algorithms\\Project\\[MS1 Tests] N Puzzle\\Sample Test\\Solvable Puzzles\\24 Puzzle 2.txt", FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fille);
            string line = sr.ReadLine();
            size = int.Parse(line);

            int[,] arr = new int[size, size];
            Console.WriteLine("matrix size =" + size);

            Console.WriteLine("=============================");

            sr.ReadLine();
            Console.Write("\nThe initial matrix is : \n");
            for (int i = 0; i < size; i++)
            {
                line = sr.ReadLine();
                lineItems = line.Split(' ');
                Console.Write("\n");
                for (int j = 0; j < size; j++)
                {

                    arr[i, j] = int.Parse(lineItems[j]);
                    Console.Write("{0}\t", arr[i, j]);

                }
            }
            Console.Write("\n");
            sr.Close();

            //Matrix from file read

            Node puzzle = new Node(arr, size);
            if (puzzle.Is_Solvable())
            {
                Console.WriteLine("solvable");
                Console.WriteLine("Number of moves = " + A_Star(puzzle));

            }
            else
                Console.WriteLine("not solvable");
        }

        static int A_Star(Node parent)
        {
            Queue finished_list = new Queue();
            Temp_PQueue opened_nodes = new Temp_PQueue();
            Node q, child;


            if (parent.Is_Final_State())
                return 0;
            if (parent.x + 1 < Node.size - 1)
            {
                opened_nodes.push(parent.Move_Up(parent));
            }
            if (parent.x - 1 >= 0)
            {
                opened_nodes.push(parent.Move_Down(parent));
            }
            if (parent.y + 1 < Node.size - 1)
            {
                opened_nodes.push(parent.Move_Left(parent));
            }
            if (parent.y - 1 >= 0)
            {
                opened_nodes.push(parent.Move_Right(parent));
            }

            finished_list.Enqueue(parent);

            q = opened_nodes.peek();
            while (opened_nodes.Count() != 0)
            {

                if (q.Is_Final_State())
                    return q.g;
                opened_nodes.pop();
                if (q.x + 1 <= Node.size - 1)
                {
                    child = q.Move_Up(q);
                    if (!child.Is_Equal(q.grid))
                        opened_nodes.push(child);
                }
                if (q.x - 1 >= 0)
                {
                    child = q.Move_Down(q);
                    if (!child.Is_Equal(q.grid))
                        opened_nodes.push(child);
                }
                if (q.y + 1 <= Node.size - 1)
                {
                    child = q.Move_Left(q);
                    if (!child.Is_Equal(q.grid))
                        opened_nodes.push(child);
                }
                if (q.y - 1 >= 0)
                {
                    child = q.Move_Right(q);
                    if (!child.Is_Equal(q.grid))
                        opened_nodes.push(child);
                }

                finished_list.Enqueue(q);
                q = opened_nodes.peek();
            }
            return 0;
        }
    }
}