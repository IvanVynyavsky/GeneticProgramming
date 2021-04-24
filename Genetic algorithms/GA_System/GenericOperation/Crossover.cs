using System;
using System.Collections.Generic;
using System.Text;

namespace GA_System.GenericOperation
{
    public class Crossover
    {
        Random random;
        public Crossover()
        {
            random = new Random();
        }
        public Induvid Crossover_(Induvid dad, Induvid mom)
        {
            //dad.tree.number_ = 0;
            //dad.tree.TraversePreOrder(dad.tree, 0);

            //mom.tree.number_ = 0;
            //mom.tree.TraversePreOrder(mom.tree, 0);

            int dad_num = random.Next(1, dad.tree.number_);
            int mom_num = random.Next(1, mom.tree.number_);

            List<string> trigonometric_nodes = new List<string>() { "sin", "cos"/*, "tg", "ctg" */};
            List<string> functional_nodes = new List<string>() { "+", "-", "*", "/", "sin", "cos"/*, "tg", "ctg" */};
            List<string> terminal_nodes = new List<string>() { "-1", "0", "1", "2", "3"/*, "4", "5", "6", "7", "8", "9", "x"*/ };
            //Console.WriteLine("Crosover strart");
            //Console.WriteLine(dad.tree.result);
            //
            Node_ node_m = new Node_();
            Node_ node_d = new Node_();
            //
            Node_ mom_nodes = new Node_();
            mom_nodes = Find(mom_num, mom.tree, node_m, trigonometric_nodes, functional_nodes, terminal_nodes);
            //
            //Console.WriteLine("mom half");
            // mom_nodes.TraverseInOrder(mom_nodes);
            // Console.WriteLine(mom_nodes.result);
            //
            //Node_ dad_nodes = new Node_();
            //dad_nodes = Find(dad_num, dad.tree,node_d, trigonometric_nodes, functional_nodes, terminal_nodes);
            //
            //Console.WriteLine("dad half");
            //dad_nodes.TraverseInOrder(dad_nodes);
            //Console.WriteLine(dad_nodes.result);
            //
            //var mom_new = Proceed(mom_num, mom.tree, dad_nodes, trigonometric_nodes, functional_nodes, terminal_nodes);
            //mom_new.result = "";
            //mom_new.TraverseInOrder(mom_new);
            //
            var dad_new = Proceed(dad_num, dad.tree, mom_nodes, trigonometric_nodes, functional_nodes, terminal_nodes);
            dad_new.result = "";
            dad_new.TraverseInOrder(dad_new);

            //dad.tree.number_ = 0;
            //dad.tree.TraversePreOrder(dad.tree, 0);

            //Console.WriteLine(dad_new.result);
            //Console.WriteLine("Crosover end");
            return new Induvid(dad_new, dad.Parametrs);

        }
        public Node_ Find(int number, Node_ parent, Node_ node, List<string> trigonometric_nodes, List<string> functional_nodes, List<string> terminal_nodes)
        {
            if (parent != null)
            {
                if (number == parent.number)
                {
                    return FindHeleper(parent, node, trigonometric_nodes, functional_nodes, terminal_nodes);
                }
                Find(number, parent.left, node, trigonometric_nodes, functional_nodes, terminal_nodes);
                Find(number, parent.right, node, trigonometric_nodes, functional_nodes, terminal_nodes);
            }
            return node;
        }
        public Node_ FindHeleper(Node_ parent, Node_ node, List<string> trigonometric_nodes, List<string> functional_nodes, List<string> terminal_nodes)
        {
            if (parent != null && node != null)
            {
                node.value = parent.value;
                if (functional_nodes.Contains(node.value))
                {
                    if (trigonometric_nodes.Contains(node.value))
                    {
                        node.right = new Node_();
                        FindHeleper(parent.right, node.right, trigonometric_nodes, functional_nodes, terminal_nodes);
                    }
                    else
                    {
                        node.left = new Node_();
                        node.right = new Node_();
                        FindHeleper(parent.left, node.left, trigonometric_nodes, functional_nodes, terminal_nodes);
                        FindHeleper(parent.right, node.right, trigonometric_nodes, functional_nodes, terminal_nodes);
                    }
                }
            }
            return node;
        }
        public Node_ Proceed(int number, Node_ parent, Node_ new_parent, List<string> trigonometric_nodes, List<string> functional_nodes, List<string> terminal_nodes)
        {
            if (parent != null)
            {
                if (number == parent.number)
                {
                    Change(parent, new_parent, trigonometric_nodes, functional_nodes, terminal_nodes);
                }
                Proceed(number, parent.left, new_parent, trigonometric_nodes, functional_nodes, terminal_nodes);
                Proceed(number, parent.right, new_parent, trigonometric_nodes, functional_nodes, terminal_nodes);
            }
            return parent;
        }
        public Node_ Change(Node_ parent, Node_ new_parent, List<string> trigonometric_nodes, List<string> functional_nodes, List<string> terminal_nodes)
        {
            if (parent != null && new_parent != null && new_parent.value != null)
            {
                parent.value = new_parent.value;
                if (new_parent.left == null)
                {
                    parent.left = null;
                }
                if (new_parent.right == null)
                {
                    parent.right = null;
                }
                if (new_parent.left == null && new_parent.right == null)
                {
                    return parent;
                }
                if (functional_nodes.Contains(new_parent.value))
                {
                    if (trigonometric_nodes.Contains(new_parent.value) && new_parent.right != null)
                    {
                        parent.left = null;
                        parent.right = new Node_();
                        Change(parent.right, new_parent.right, trigonometric_nodes, functional_nodes, terminal_nodes);
                    }
                    else if (new_parent.left != null && new_parent.right != null)
                    {
                        parent.left = new Node_();
                        parent.right = new Node_();
                        Change(parent.left, new_parent.left, trigonometric_nodes, functional_nodes, terminal_nodes);
                        Change(parent.right, new_parent.right, trigonometric_nodes, functional_nodes, terminal_nodes);
                    }
                }
                return parent;
            }
            return parent;
        }
    }
}
