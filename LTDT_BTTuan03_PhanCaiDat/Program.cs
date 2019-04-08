using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LTDT_BTTuan03_PhanCaiDat
{
    class Program
    {
        static void Main(string[] args)
        {
            GRAPH g = new GRAPH();
            //string input_filename = @"D:\STUDY\IT\Semester3\GraphTheory\4_Projects\LTDT_BTTuan02_PhanCaiDat\input.txt";
            string input_filename = "../../input.txt";
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
                        int label = 0;
                        LinkedList<int> label0List = new LinkedList<int>();
                        for (i = 0; i < n; i++)
                        {
                            label0List.AddLast(i);
                        }

                        //Step 2:
                        for (i = 0; i < n; i++)
                        {
                            if (label0List.FirstOrDefault() == i)
                            {
                                label++;
                                visit(g, i, label);
                            }
                        }
                        
                        /*
                        LinkedList<int> firstList = new LinkedList<int>();
                        LinkedList<int> VisitedList = new LinkedList<int>();
                        int[] visit = new int[n];
                        
                        for (int j = 0; j < n; j++)
                        {
                            firstList.AddFirst(j);
                        }
                        VisitedList.AddFirst(i);
                        visit[i] = 1;
                        stack.Push(i);
                        if (true)
                        {
                            label++;
                            visit(i, label);
                        }
                        */

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
