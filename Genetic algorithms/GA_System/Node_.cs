using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace GA_System
{
    [Serializable]
    public class Node_
    {
        public string result;
        public string value { get; set; }
        public Node_ left { get; set; }
        public Node_ right { get; set; }
        public int number = 0;
        public int number_ = 0;

        public Node_(string item)
        {
            value = item;
            left = right = null;
            result = "";
        }
        public Node_()
        {
            left = right = null;
            result = "";
        }
        public void TraverseInOrder(Node_ parent)
        {
            if (parent != null)
            {
                result += "(";
                TraverseInOrder(parent.left);
                result += parent.value;
                TraverseInOrder(parent.right);
                result += ")";
            }
        }
        public void TraverseInOrder2(Node_ parent)
        {
            if (parent != null)
            {
                Console.Write("(");
                TraverseInOrder2(parent.left);
                Console.Write(parent.value + " number(" + parent.number + ") ");
                TraverseInOrder2(parent.right);
                Console.Write(")");
            }
        }
        public void TraversePreOrder(Node_ parent, int num)
        {
            if (parent != null)
            {
                parent.number = num;
                number_++;
                TraversePreOrder(parent.left, number_);
                TraversePreOrder(parent.right, number_);
            }
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
