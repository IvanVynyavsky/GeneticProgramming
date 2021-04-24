using Jace;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GA_System
{
    [Serializable]
    public class Induvid : ICloneable
    {
        [NonSerialized]
        CalculationEngine engine;
        public Parametrs Parametrs { get; private set; }
        public int Age { get; set; }
        public double LifeTime { get; set; }
        public double InduvidEvaluation { get; set; }
        public string Induvid_toString { get; set; }
        public Node_ tree { get; set; }
        public int depth { get; set; }

        public double Probability { get; set; }
        public double SectorStart { get; set; }
        public double SectotEnd { get; set; }


        public Induvid(Node_ tree,Parametrs p )
        {
            Age = 1;
            //
            Parametrs = new Parametrs();
            Parametrs = p;
            //
            this.tree = new Node_();
            this.tree = tree;
            //
            tree.TraversePreOrder(tree, 0);
            //
            Induvid_toString = tree.result;
            //
            tree.number_ = 0;
            tree.TraversePreOrder(tree, 0);
            //
            engine = new CalculationEngine();
            //
            InduvidEvaluation = GetInduvidEvaluation();
        }
        public Induvid(Parametrs p)
        {
            Age = 1;
            //
            Parametrs = new Parametrs();
            Parametrs = p;
            //
            tree = new Node_();
            tree = GenerateTree();
            //
            tree.TraversePreOrder(tree,0);
            //
            Induvid_toString = GetInduvidToString();
            //
            tree.number_ = 0;
            tree.TraversePreOrder(tree, 0);
            //
            engine = new CalculationEngine();
            //
            InduvidEvaluation = GetInduvidEvaluation();
        }
        private double GetInduvidEvaluation()
        {
            CalculationEngine engine = new CalculationEngine();
            Dictionary<string, double> variables = new Dictionary<string, double>();

            double error = 0;
            for (int i = 0; i < Parametrs.X.Count; i++)
            {
                variables.Add("x", Parametrs.X[i]);
                string save_division = SafeDivision(Induvid_toString, Parametrs.X[i]);
                error += Math.Pow(Parametrs.Y[i] - engine.Calculate(save_division, variables), 2);
                variables.Remove("x");
            }
            return error;
        }
        private string GetInduvidToString()
        {
            tree.TraverseInOrder(tree);
            return tree.result;
        }
        private Node_ GenerateTree()
        {
            Random random = new Random();

            int max_depth = Parametrs.MaxDepth;
            int min_depth = Parametrs.MinDepth;

            List<string> trigonometric_nodes = new List<string>() { "sin", "cos"/*, "tg", "ctg" */};
            List<string> functional_nodes = new List<string>() { "+", "-", "*", "/", "sin", "cos"/*, "tg", "ctg" */};
            List<string> terminal_nodes = new List<string>() { "-1", "0", "1", "2", "3"/*, "4", "5", "6", "7", "8", "9", "x"*/ };

            this.depth = random.Next(min_depth, max_depth + 1);
            int s = functional_nodes.Count + terminal_nodes.Count;
            int ltc = depth;

            Node_ res_tree = new Node_();

            res_tree = RandTree(ltc, res_tree, terminal_nodes, functional_nodes, min_depth, max_depth, s, trigonometric_nodes);

            return res_tree;
        }
        public static Node_ RandTree(int ltc, Node_ node_, List<string> terminal_nodes, List<string> functional_nodes, int min_depth, int max_depth, int s, List<string> trigonometric_nodes)
        {
            Random random = new Random();
            //terminal_nodes
            if (ltc == 0)
            {
                //Console.WriteLine("ltc = 0 terminal node add");
                int index = random.Next(0, terminal_nodes.Count);
                string terminal_node_value = terminal_nodes[index];
                // add terminal_nodes
                //Console.WriteLine(terminal_node_value);
                //Console.ReadLine();
                node_.value = terminal_node_value;
                //node_.number = num;
                //num++;
            }
            else
            {
                //functional_nodes
                if (ltc >= min_depth)
                {
                    //Console.WriteLine("ltc >= min_depth functional_nodes add");
                    int index = random.Next(0, functional_nodes.Count);
                    string functional_node_value = functional_nodes[index];
                    // add functional_nodes
                    //Console.WriteLine(functional_node_value);
                    node_.value = functional_node_value;
                    //node_.number = num;
                    //num = num + 1;
                    //Console.WriteLine(node_.number);
                    //Console.WriteLine(num);
                    //Console.ReadLine();
                    if (trigonometric_nodes.Contains(functional_node_value))
                    {
                        node_.right = RandTree(ltc - 1, new Node_(), terminal_nodes, functional_nodes, min_depth, max_depth, s, trigonometric_nodes);
                    }
                    else
                    {
                        node_.left = RandTree(ltc - 1, new Node_(), terminal_nodes, functional_nodes, min_depth, max_depth, s, trigonometric_nodes);
                        node_.right = RandTree(ltc - 1, new Node_(), terminal_nodes, functional_nodes, min_depth, max_depth, s, trigonometric_nodes);
                    }
                    //call this function(ltc-1)
                    // generate children

                }
                else
                {
                    var r = random.Next(0, s);
                    //functional_nodes
                    if (r < functional_nodes.Count)
                    {
                        //Console.WriteLine("r < functional_nodes.Count functional_nodes add");
                        int index = random.Next(0, functional_nodes.Count);
                        string functional_node_value = functional_nodes[index];
                        // add functional_nodes
                        //Console.WriteLine(functional_node_value);
                        node_.value = functional_node_value;
                        //node_.number = num;
                        //num = num + 1;
                        if (trigonometric_nodes.Contains(functional_node_value))
                        {
                            node_.right = RandTree(ltc - 1, new Node_(), terminal_nodes, functional_nodes, min_depth, max_depth, s, trigonometric_nodes);
                        }
                        else
                        {
                            node_.left = RandTree(ltc - 1, new Node_(), terminal_nodes, functional_nodes, min_depth, max_depth, s, trigonometric_nodes);
                            node_.right = RandTree(ltc - 1, new Node_(), terminal_nodes, functional_nodes, min_depth, max_depth, s, trigonometric_nodes);
                        }

                        //call this function(ltc-1)
                        // generate children

                    }
                    //terminal_nodes
                    else
                    {
                        //Console.WriteLine("r > functional_nodes.Count terminal_nodes add");
                        int index = random.Next(0, terminal_nodes.Count);
                        string terminal_node_value = terminal_nodes[index];
                        // add terminal_nodes
                        //Console.WriteLine(terminal_node_value);
                        node_.value = terminal_node_value;
                        //node_.number = num;
                        //num++;
                        //  Console.ReadLine();
                    }
                }
            }

            return node_;
        }

        public string SafeDivision(string equation,double x_value)
        {
            for (int i = 0; i < equation.Length - 1; i++)
            {
                if (equation[i] == '/')
                {
                    equation = CheckIsSafe(equation, i+1, x_value);
                }
            }
            return equation;
        }

        public string CheckIsSafe(string equation, int start_index, double x_value)
        {
            int replace_start_index = start_index;
            string fun_value = "";
            int amount_of_open_braket = 0;
            int amount_of_close_braket = 0;
            bool tem = true;
            // / (
            while (tem)
            {
                if (equation[start_index] == '(')
                {
                    amount_of_open_braket++;
                }
                if (equation[start_index] == ')')
                {
                    amount_of_close_braket++;
                }
                if (equation[start_index] == '/')
                {
                    equation = CheckIsSafe(equation, start_index+1, x_value);
                }
                fun_value += equation[start_index];
                start_index++;

                if (amount_of_close_braket == amount_of_open_braket)
                    tem = false;   
            }
            Dictionary<string, double> variables = new Dictionary<string, double>();

            variables.Add("x", x_value);
            if (engine.Calculate(fun_value,variables) == 0.0)
            {
                equation = equation.Remove(replace_start_index, start_index - replace_start_index);
                equation = equation.Insert(replace_start_index, "(1)");
            }

            variables.Remove("x");
            return equation;
        }
        public object Clone()
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, this);
                ms.Position = 0;

                return formatter.Deserialize(ms);
            }
        }
    }
}