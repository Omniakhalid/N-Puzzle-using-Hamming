using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Puzzle
{
    class Temp_PQueue
    {
        List<Node> pqueue;

        public Temp_PQueue()
        {
            pqueue = new List<Node>();
        }

        public void push(Node node)
        {
            if (pqueue.Count() == 0)
                pqueue.Add(node);
            else
            {
                int index = pqueue.FindIndex((Node x) => x.Is_Equal(node.grid) == true);
                if (index != -1)
                {
                    if (node.f < pqueue[index].f)
                    {
                        pqueue.RemoveAt(index);
                        pqueue.Insert(index, node);
                    }
                }

                else
                {
                    for (int i = 0; i < pqueue.Count(); i++)
                    {
                        if (node.f < pqueue[i].f)
                        {
                            pqueue.Insert(i, node);
                            break;
                        }
                        else if(i == pqueue.Count() - 1)
                        {
                            pqueue.Add(node);
                            break;
                        }
                    }

                } 
            }
        }

        public void pop()
        {
            pqueue.RemoveAt(0);
        }

        public Node peek()
        {
            return pqueue[0];
        }

        public int Count()
        {
            return pqueue.Count();
        }
    }
}
