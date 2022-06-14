using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Tree
{
    public class Node
    {
        public string data;
        public Node left, right;

        public Node(string d)
        {
            data = d;
            left = null;
            right = null;
        }
    };

    public class NodeInfo
    {
        public Node Node;
        public string Text;
        public int StartPos;
        public int Size { get { return Text.Length; } }
        public int EndPos { get { return StartPos + Size; } set { StartPos = value - Size; } }
        public NodeInfo Parent, Left, Right;
    }
}
