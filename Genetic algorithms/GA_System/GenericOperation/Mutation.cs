using System;
using System.Collections.Generic;
using System.Text;

namespace GA_System.GenericOperation
{
    public class Mutation
    {
        Random random;
        public Mutation()
        {
            random = new Random();
        }
        public Induvid Mutation_(Induvid induvid)
        {
            //Console.WriteLine("Mutation_ start");
            //Console.WriteLine(induvid.tree.number_);
            //induvid.tree.number_ = 0;
            //induvid.tree.TraversePreOrder(induvid.tree,0);
            //
            //Console.WriteLine(induvid.tree.number_);
            //
            int number = random.Next(1, induvid.tree.number_-1);
            List<string> trigonometric_nodes = new List<string>() { "sin", "cos"/*, "tg", "ctg" */};
            List<string> functional_nodes = new List<string>() { "+", "-", "*", "/", "sin", "cos"/*, "tg", "ctg" */};
            List<string> terminal_nodes = new List<string>() { "-1", "0", "1", "2", "3"/*, "4", "5", "6", "7", "8", "9", "x"*/ };
            //Console.WriteLine("Number : "+ number);
            //Console.WriteLine(induvid.tree.result);
            //induvid.tree.TraverseInOrder2(induvid.tree);
            var node = Find(number, induvid.tree, functional_nodes, terminal_nodes, trigonometric_nodes);
            node.result = "";
            node.TraverseInOrder(node);
            //Console.WriteLine(node.result);
            //Console.WriteLine("Mutation_ end");
            //induvid.tree.number_ = 0;
            //induvid.tree.TraversePreOrder(induvid.tree, 0);
            return new Induvid(node, induvid.Parametrs);
        }

        public Node_ Find(int number, Node_ parent, List<string> functional_nodes, List<string> terminal_nodes, List<string> trigonometric_nodes)
        {
            if (parent != null)
            {
                if (number == parent.number)
                {
                    return Change(parent, functional_nodes, terminal_nodes, trigonometric_nodes);
                }
                Find(number, parent.left, functional_nodes, terminal_nodes, trigonometric_nodes);
                Find(number, parent.right, functional_nodes, terminal_nodes, trigonometric_nodes);
            }
            return parent;
        }

        public Node_ Change(Node_ parent, List<string> functional_nodes, List<string> terminal_nodes, List<string> trigonometric_nodes)
        {
            if (parent != null)
            {
                string old_parent_value = parent.value;
                if (functional_nodes.Contains(parent.value))
                {
                    parent.value = functional_nodes[random.Next(0, functional_nodes.Count)];
                    if (trigonometric_nodes.Contains(parent.value))
                    {
                        if (trigonometric_nodes.Contains(old_parent_value))
                        {
                            Change(parent.right, functional_nodes, terminal_nodes, trigonometric_nodes);
                        }
                        else
                        {
                            parent.left = null;
                            Change(parent.right, functional_nodes, terminal_nodes, trigonometric_nodes);
                        }
                    }
                    else
                    {
                        if (terminal_nodes.Contains(old_parent_value))
                        {
                            Change(parent.left, functional_nodes, terminal_nodes, trigonometric_nodes);
                            Change(parent.right, functional_nodes, terminal_nodes, trigonometric_nodes);
                        }
                        else
                        {
                            parent.left = new Node_();
                            Change(parent.left, functional_nodes, terminal_nodes, trigonometric_nodes);
                            Change(parent.right, functional_nodes, terminal_nodes, trigonometric_nodes);
                        }

                    }
                }
                else
                {
                    parent.value = terminal_nodes[random.Next(0, terminal_nodes.Count)];
                }
                return parent;
            }
            return parent;
        }
    }
}
