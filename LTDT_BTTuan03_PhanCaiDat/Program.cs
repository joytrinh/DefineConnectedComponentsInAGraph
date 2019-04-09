using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace LTDT_BTTuan03_PhanCaiDat
{
    class Program
    {
        static void Main(string[] args)
        {
            GRAPH g = new GRAPH();
            string input_filename = "../../../input.txt";
            //string input_filename = args[0];
            findConnectedComponents(input_filename, g);
            Console.ReadLine();
        }
        private static void findConnectedComponents(string input_filename, GRAPH g)
        {
            try
            {
                using (StreamReader reader = new StreamReader(input_filename))
                {
                    string line = reader.ReadLine();
                    int n = g.numberOfVertexes = int.Parse(line);
                    if (n > 2)
                    {
                        //Declare variables
                        g.matrix = new int[n, n];
                        int i = 0;
                        int sIndex = 0;

                        //Add each element in input.txt into matrix
                        line = reader.ReadToEnd().Replace("\r\n", " ");
                        string[] s = line.Split(' ');

                        while (sIndex < n * 2)
                            for (i = 0; i < n; i++)
                                for (int j = 0; j < n; j++)
                                {
                                    g.matrix[i, j] = int.Parse(s[sIndex]);
                                    sIndex++;
                                }
                        reader.Close();

                        //Step 1:
                        int label = 1;
                        ArrayList visitedList = new ArrayList();
                        ArrayList notVisitedList = new ArrayList();
                        int[] visitedArray = new int[n];
                        Stack<int> stack = new Stack<int>();

                        //Add all vertex into notVisitedList
                        for (i = 0; i < n; i++)
                            notVisitedList.Add(i);

                        //Visit the first vertex and add label 1 for it and remove it from notVisitedList
                        visitedList.Add(i);
                        visitedArray[i] = 1;
                        stack.Push(i);
                        notVisitedList.Remove(i);
                         
                        while (stack.Count != 0)
                        {
                            i = stack.Peek();
                            int count = 0;
                            for (int j = 0; j < n; j++)
                            {
                                if (g.matrix[i, j] > 0 && visitedArray[j] != 1)
                                {
                                    visitedArray[j] = 1;
                                    visitedList.Add(j);
                                    stack.Push(j);
                                    for (int k = 0; k < visitedList.Count; k++)
                                    {
                                        if (int.Parse(notVisitedList[k].ToString()) == j)
                                        {
                                            notVisitedList.Remove(k);
                                        }
                                    }
                                    break;
                                }
                                else
                                    count++;
                            }
                            if (count == n)
                            {
                                Console.WriteLine("So thanh phan lien thong: " +label);
                                for (i = 0; i < visitedList.Count; i++)
                                    Console.WriteLine(visitedList[i] + " ==> ");
                                if (notVisitedList.Count != 0)
                                {
                                    i = int.Parse(notVisitedList[0].ToString());
                                    label++;
                                    stack.Clear();
                                    stack.Push(i);
                                    visitedList.Clear();
                                }
                                else
                                    break;
                            }
                        }                        
                    }
                    else
                        Console.WriteLine("The number of Vertexes must be greater than 2.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: ");
                Console.WriteLine(e.Message);
            }
        }
        private static void visit(GRAPH g, int i, int label)
        {
            LinkedList<int> labelList = new LinkedList<int>();
            for (int j = 0; j < g.numberOfVertexes; j++)
            {
                if (g.matrix[i,j] > 0 && label == 0)
                {
                    visit(g, j, label);
                }
            }
        }
    }
    class GRAPH
    {
        private int _numberOfVertexes;
        private int[,] _matrix;

        public int numberOfVertexes
        {
            get { return _numberOfVertexes; }
            set
            {
                if (value > 2)
                    _numberOfVertexes = value;
            }
        }

        public int[,] matrix
        {
            get { return _matrix; }
            set { _matrix = value; }
        }
    }
}
