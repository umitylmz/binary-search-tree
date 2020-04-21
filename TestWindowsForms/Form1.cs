using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWindowsForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        static class Global
        {
            public static string globalVar = "ss";

            public static string preorder = "";
            public static string postorder = "";
            public static string inorder = "";


            public static int[] rands;

            public static int counter =0;
            public static int counter2 = 0;
            public static int counter3 = 0;


            public static string[] textsm;
            public static string[] texts2;
            public static string[] textb;

            public static int[] bsort;


            public static int max;
            public static int maxb;


            public static int cc;


        }


 
        static int partition(int[] arr, int low,
                                           int high)
            {
                int pivot = arr[high];

                int i = (low - 1);
                for (int j = low; j < high; j++)
                {
             
                    if (arr[j] < pivot)
                    {
                        i++;

                       
                        int temp = arr[i];
                        arr[i] = arr[j];
                        arr[j] = temp;
                    }
                }

                int temp1 = arr[i + 1];
                arr[i + 1] = arr[high];
                arr[high] = temp1;

                return i + 1;
            }


          
            static void quickSort(int[] arr, int low, int high)
            {
                if (low < high)
                {

               
                    int pi = partition(arr, low, high);

                    quickSort(arr, low, pi - 1);
                    quickSort(arr, pi + 1, high);
                }
            }

        class Node
        {
            public Node LeftNode { get; set; }
            public Node RightNode { get; set; }
            public int Data { get; set; }
        }

        class BinaryTree
        {
            public Node Root { get; set; }

            public bool Add(int value)
            {
                Node before = null, after = this.Root;

                while (after != null)
                {
                    before = after;
                    if (value < after.Data) 
                        after = after.LeftNode;
                    else if (value > after.Data) 
                        after = after.RightNode;
                    else
                    {
                        return false;
                    }
                }

                Node newNode = new Node();
                newNode.Data = value;

                if (this.Root == null)
                    this.Root = newNode;
                else
                {
                    if (value < before.Data)
                        before.LeftNode = newNode;
                    else
                        before.RightNode = newNode;
                }

                return true;
            }

            public Node Find(int value)
            {
                return this.Find(value, this.Root);
            }

            public void Remove(int value)
            {
                Remove(this.Root, value);
            }

            private Node Remove(Node parent, int key)
            {
                if (parent == null) return parent;

                if (key < parent.Data) parent.LeftNode = Remove(parent.LeftNode, key);
                else if (key > parent.Data)
                    parent.RightNode = Remove(parent.RightNode, key);

                else
                {
                    if (parent.LeftNode == null)
                        return parent.RightNode;
                    else if (parent.RightNode == null)
                        return parent.LeftNode;

                    parent.Data = MinValue(parent.RightNode);

                    parent.RightNode = Remove(parent.RightNode, parent.Data);
                }

                return parent;
            }

            private int MinValue(Node node)
            {
                int minv = node.Data;

                while (node.LeftNode != null)
                {
                    minv = node.LeftNode.Data;
                    node = node.LeftNode;
                }

                return minv;
            }

            private Node Find(int value, Node parent)
            {
                if (parent != null)
                {
                    if (value == parent.Data) return parent;
                    if (value < parent.Data)
                        return Find(value, parent.LeftNode);
                    else
                        return Find(value, parent.RightNode);
                }

                return null;
            }

            public int GetTreeDepth()
            {
                return this.GetTreeDepth(this.Root);
            }

            private int GetTreeDepth(Node parent)
            {
                return parent == null ? 0 : Math.Max(GetTreeDepth(parent.LeftNode), GetTreeDepth(parent.RightNode)) + 1;
            }

            public void TraversePreOrder(Node parent)
            {
                if (parent != null)
                {
                    Console.Write(parent.Data + " ");
                    Global.preorder = Global.preorder + parent.Data + " --> " ;
                    TraversePreOrder(parent.LeftNode);
                    TraversePreOrder(parent.RightNode);
                }
            }

            public void TraverseInOrder(Node parent)
            {
                if (parent != null)
                {
                    TraverseInOrder(parent.LeftNode);
                    Console.Write(parent.Data + " ");
                    Global.inorder = Global.inorder + parent.Data + " --> " ;
                    Global.bsort[Global.cc] = parent.Data;
                    Global.cc++;
                    TraverseInOrder(parent.RightNode);
                }
            }

            public void TraversePostOrder(Node parent)
            {
                if (parent != null)
                {
                    TraversePostOrder(parent.LeftNode);
                    TraversePostOrder(parent.RightNode);
                    Console.Write(parent.Data + " ");
                    Global.postorder = Global.postorder + parent.Data + " --> " ;

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int howmany = int.Parse(maskedTextBox2.Text);

            int[] randoms = new int[howmany];
            string[] texts = new string[howmany];
            Global.textb = new string[howmany];

            Global.texts2 = new string[howmany];



            Random rg = new Random();


            for (int i = 0; i < randoms.Length; i++)
            {

                randoms[i] = rg.Next(1, 1000);
            }

            Global.rands = randoms;
            Global.counter = 0;
            string cs = "";
            string s = "";
            

            int a = 0;
            for (int i = 0; i < Global.rands.Length; i++)
            {
                cs = s;

                s = s + " " + Global.rands[i];

                if (s.Length > 70)
                {
                    texts[a] = cs;
                    a++;
                    s = "";
                    i--;
                }

                if (i == Global.rands.Length - 1)
                {
                    texts[a] = s;
                }
            }



            Global.max = a;
            Global.textsm = texts;
            Console.WriteLine(texts[0]);
            Console.WriteLine(texts[1]);




            maskedTextBox1.Text = texts[Global.counter];

        }

        private void maskedTextBox2_TextChanged(object sender, EventArgs e)
        {
            maskedTextBox3.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int[] sorted = new int[Global.rands.Length];

            Global.rands.CopyTo(sorted, 0);

            quickSort(sorted, 0, Global.rands.Length - 1);

            Global.counter2 = 0;
            string cs = "";
            string s = "";


            int a = 0;
            for (int i = 0; i < sorted.Length; i++)
            {
                cs = s;

                s = s + " " + sorted[i];

                if (s.Length > 42)
                {
                    Global.texts2[a] = cs;
                    a++;
                    s = "";
                    i--;
                }

                if (i == sorted.Length - 1)
                {
                    Global.texts2[a] = s;
                }
            }

            maskedTextBox3.Text = Global.texts2[Global.counter2];


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox2_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void actionTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (!(Global.counter == Global.max))
            {
                Global.counter++;
                maskedTextBox1.Text = Global.textsm[Global.counter];
                Console.WriteLine("AAAAAA");
                Console.WriteLine(Global.max);


            }
            else
            {
                Global.counter = Global.max;
                maskedTextBox1.Text = Global.textsm[Global.counter];
                Console.WriteLine("BBBBBB");

            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (!(Global.counter2 == 0))
            {
                Global.counter2--;
                maskedTextBox3.Text = Global.texts2[Global.counter2];
                Console.WriteLine("AAAAAA");


            }
            else
            {
                Global.counter2 = 0;
                maskedTextBox3.Text = Global.texts2[Global.counter2];
                Console.WriteLine("BBBBBB");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!(Global.counter == 0))
            {
                Global.counter--;
                maskedTextBox1.Text = Global.textsm[Global.counter];
                Console.WriteLine("AAAAAA");


            }
            else
            {
                Global.counter = 0;
                maskedTextBox1.Text = Global.textsm[Global.counter];
                Console.WriteLine("BBBBBB");

            }



        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!(Global.counter2 == Global.max))
            {
                Global.counter2++;
                maskedTextBox3.Text = Global.texts2[Global.counter2];
                Console.WriteLine("AAAAAA");
                Console.WriteLine(Global.max);


            }
            else
            {
                Global.counter2 = Global.max;
                maskedTextBox3.Text = Global.texts2[Global.counter2];
                Console.WriteLine("BBBBBB");

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Global.cc = 0;
            Global.bsort=new int[Global.rands.Length];

        BinaryTree binaryTree = new BinaryTree();
            for (int i = 0; i < Global.rands.Length; i++)
            {
                binaryTree.Add(Global.rands[i]);

            }

          


            int depth = binaryTree.GetTreeDepth();

            Console.WriteLine("PreOrder Traversal:");
            binaryTree.TraversePreOrder(binaryTree.Root);
            Console.WriteLine();


            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();
            binaryTree.TraverseInOrder(binaryTree.Root);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = ts.Milliseconds.ToString();
            Console.WriteLine("RunTime " + elapsedTime);
            label9.Text = elapsedTime+" ms";



            Global.counter3 = 0;
            string cs = "";
            string s = "";


            int a = 0;
            for (int i = 0; i < Global.bsort.Length; i++)
            {
                cs = s;

                s = s + " " + Global.bsort[i];


            


                if (s.Length > 70)
                {
                    Global.textb[a] = cs;
                    a++;
                    s = "";
                    i--;
                }

                if (i == Global.bsort.Length - 1)
                {
                    Global.textb[a] = s;
                }

                if (Global.bsort[i] == 0)
                {
                    Global.textb[a] = cs;
                    break;

                }


            }
            Global.maxb = a;
            maskedTextBox4.Text = Global.textb[Global.counter3];

            Console.WriteLine("PostOrder Traversal:");
            binaryTree.TraversePostOrder(binaryTree.Root);
            Console.WriteLine();

   

            Console.WriteLine("PreOrder Traversal After Removing Operation:");
            binaryTree.TraversePreOrder(binaryTree.Root);
            Console.WriteLine();

            Console.ReadLine();

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox4_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (!(Global.counter3 == Global.maxb))
            {
                Global.counter3++;
                maskedTextBox4.Text = Global.textb[Global.counter3];
             


            }
            else
            {
                Global.counter3 = Global.maxb;
                maskedTextBox4.Text = Global.textb[Global.counter3];
                Console.WriteLine("BBBBBB");

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!(Global.counter3 == 0))
            {
                Global.counter3--;
                maskedTextBox4.Text = Global.textb[Global.counter3];
                Console.WriteLine("AAAAAA");


            }
            else
            {
                Global.counter3 = 0;
                maskedTextBox4.Text = Global.textb[Global.counter3];
                Console.WriteLine("BBBBBB");

            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Global.bsort = new int[Global.rands.Length];

            BinaryTree binaryTree = new BinaryTree();
            for (int i = 0; i < Global.rands.Length; i++)
            {
                binaryTree.Add(Global.rands[i]);

            }

            Global.preorder = "";
            binaryTree.TraversePreOrder(binaryTree.Root);

            textBox1.Text = Global.preorder;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Global.postorder = "";

            Global.bsort = new int[Global.rands.Length];

            BinaryTree binaryTree = new BinaryTree();
            for (int i = 0; i < Global.rands.Length; i++)
            {
                binaryTree.Add(Global.rands[i]);

            }

            binaryTree.TraversePostOrder(binaryTree.Root);
            textBox2.Text = Global.postorder;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Global.inorder = "";

            Global.cc = 0;
            Global.bsort = new int[Global.rands.Length];

            BinaryTree binaryTree = new BinaryTree();
            for (int i = 0; i < Global.rands.Length; i++)
            {
                binaryTree.Add(Global.rands[i]);

            }

            binaryTree.TraverseInOrder(binaryTree.Root);
            textBox3.Text = Global.inorder;


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            int[] sorted = new int[Global.rands.Length];

            Global.rands.CopyTo(sorted, 0);
            System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
            stopWatch.Start();
            quickSort(sorted, 0, Global.rands.Length - 1);
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;

            string elapsedTime = ts.Milliseconds.ToString();
            Console.WriteLine("RunTime " + elapsedTime);
            label8.Text = elapsedTime + " ms";

            Global.counter2 = 0;
            string cs = "";
            string s = "";


            int a = 0;
            for (int i = 0; i < sorted.Length; i++)
            {
                cs = s;

                s = s + " " + sorted[i];

                if (s.Length > 70)
                {
                    Global.texts2[a] = cs;
                    a++;
                    s = "";
                    i--;
                }

                if (i == sorted.Length - 1)
                {
                    Global.texts2[a] = s;
                }
            }

            maskedTextBox3.Text = Global.texts2[Global.counter2];


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
