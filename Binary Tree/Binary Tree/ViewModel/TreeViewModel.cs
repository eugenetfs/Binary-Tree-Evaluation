using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Binary_Tree
{
    internal class TreeViewModel : INotifyPropertyChanged
    {
        #region Public Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private Members
        private string convertText;
        private RelayCommand convertCommand;
        #endregion

        #region Properties
        public string ConvertText
        {
            get { return convertText; }
            set
            {
                convertText = value;
                OnPropertyChanged("ConvertText");
            }
        }
        public RelayCommand ConvertCommand
        {
            get
            {
                if (convertCommand == null)
                    convertCommand = new RelayCommand(OnConvertCommand);
                return convertCommand;
            }
            set { convertCommand = value; }
        }
        #endregion

        #region Constructor
        public TreeViewModel() { }
        #endregion

        #region Private Methods
        private void OnConvertCommand()
        {
            if (!string.IsNullOrEmpty(ConvertText))
            {
                CreateBinaryTree(ConvertText, false);
            }
        }

        private int CreateBinaryTree(string str, bool isUnitTest)
        {
            Printer printer = new Printer();
            int result = 0;

            Node node = ExpressionTree(ConvertToPostfix(SplitString(str)));
            result = EvaluateTree(node);
            if (!isUnitTest)
            {
                printer.Print(node);
                if (node != null)
                {
                    Console.Write("\nAnswer: " + result);
                }
            }

            return result;
        }

        private List<string> SplitString(string s)
        {
            s = s.Replace(" ", string.Empty);
            List<string> list = new List<string>();
            string tmpStr = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '-')
                {
                    if (i == 0)
                    {
                        tmpStr = tmpStr + s[i];
                    }
                    else
                    {
                        if (IsOperator(s[i - 1].ToString()))
                        {
                            tmpStr = tmpStr + s[i];
                        }
                        else
                        {
                            list.Add(s[i].ToString());
                        }
                    }
                }
                else
                {
                    if (IsOperator(s[i].ToString()))
                    {
                        list.Add(s[i].ToString());
                    }
                }

                if (s[i] == '(')
                {
                    list.Add(s[i].ToString());
                }

                if (s[i] == ')')
                {
                    list.Add(s[i].ToString());
                }

                // if number is more than 1 digit
                if (char.IsDigit(s[i]))
                {
                    tmpStr = tmpStr + s[i];

                    if (i + 1 < s.Length &&
                        !char.IsDigit(s[i + 1]))
                    {
                        list.Add(tmpStr);
                        tmpStr = "";
                    }
                }
            }

            return list;
        }

        private List<string> ConvertToPostfix(List<string> infix)
        {
            // Convert to postfix to remove bracket 
            List<string> list = new List<string>();
            Stack<string> stack = new Stack<string>();

            for (int i = 0; i < infix.Count; ++i)
            {
                string c = infix[i];
                int tmpInt = 0;
                if (int.TryParse(infix[i], out tmpInt))
                {
                    // If the scanned character is an
                    // operand, add it to output.
                    list.Add(c);
                }
                else if (c == "(")
                {
                    // If the scanned character is an '(',
                    // push it to the stack.
                    stack.Push(c);
                }
                else if (c == ")")
                {
                    // If the scanned character is an ')',
                    // pop and output from the stack 
                    // until an '(' is encountered.
                    while (stack.Count > 0 &&
                           stack.Peek() != "(")
                    {
                        list.Add(stack.Pop());
                    }

                    if (stack.Count > 0 && stack.Peek() == "(")
                    {
                        stack.Pop();
                    }
                }
                else // an operator is encountered
                {
                    stack.Push(c);
                }
            }

            // pop all the operators from the stack
            while (stack.Count > 0)
            {
                list.Add(stack.Pop());
            }

            return list;
        }

        private bool IsOperator(string ch)
        {
            if (ch == "+" || ch == "-" || ch == "*" || ch == "/" || ch == "x" || ch == "÷" || ch == "^")
            {
                return true;
            }
            return false;
        }

        private Node ExpressionTree(List<string> postfix)
        {
            try
            {
                // Inorder traversal
                Stack<Node> st = new Stack<Node>();
                Node t1, t2, temp;

                for (int i = 0; i < postfix.Count; i++)
                {
                    if (!IsOperator(postfix[i]))
                    {
                        if (postfix[i][0] == '-')
                        {
                            temp = new Node("-");
                            t1 = new Node(postfix[i].Substring(1));
                            temp.right = t1;
                        }
                        else
                        {
                            temp = new Node(postfix[i].ToString());
                        }
                        st.Push(temp);
                    }
                    else
                    {
                        temp = new Node(postfix[i].ToString());

                        t1 = st.Pop();
                        t2 = st.Pop();

                        temp.left = t2;
                        temp.right = t1;

                        st.Push(temp);
                    }
                }
                temp = st.Pop();
                return temp;
            }
            catch
            {
                Console.WriteLine("\nInvalid infix expression");
                return null;
            }
        }

        private int EvaluateTree(Node root)
        {
            if (root == null)
            {
                return 0;
            }

            // Leaf node
            if (root.left == null && root.right == null)
            {
                return int.Parse(root.data);
            }

            int leftNode = EvaluateTree(root.left);
            int rightNode = EvaluateTree(root.right);

            if (root.data.Equals("+"))
            {
                return leftNode + rightNode;
            }

            if (root.data.Equals("-"))
            {
                return leftNode - rightNode;
            }     
            else if (root.data.Equals("*") ||
                     root.data.Equals("x"))
            {
                return leftNode * rightNode;
            }
            else if(root.data.Equals("÷") ||
                    root.data.Equals("/"))
            {
                return leftNode / rightNode;
            }       

            return 0;
        }
        #endregion

        #region Protected Methods
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
