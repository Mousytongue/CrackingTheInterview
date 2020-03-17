using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrackingTheInterview
{
    #region DataStructures
    class Hashmap
    {
        struct HashPair
        {
            public string key;
            public string value;
            public HashPair(string k, string v)
            {
                key = k;
                value = v;
            }
        }


        List<HashPair>[] HashTable = new List<HashPair>[500];

        public Hashmap()
        {
            for (int i = 0; i < HashTable.Length; i++)
            {
                HashTable[i] = new List<HashPair>();
            }
        }


        public string Get(string key)
        {
            int loc = Hash(key);

            for (int i = 0; i < HashTable[loc].Count; i++)
            {
                if (HashTable[loc][i].key == key)
                    return HashTable[loc][i].value;
            }
            return null;
        }

        public void Add(string key, string value)
        {
            int loc = Hash(key);
            HashPair entry = new HashPair(key, value);

            HashTable[loc].Add(entry);
        }

        public int Hash(string key)
        {
            int total = 0;
            char[] c;
            c = key.ToCharArray();

            for (int i = 0; i < c.GetUpperBound(0); i++)
            {
                total += (int)c[i];
            }

            return total % HashTable.Length;
        }

        public void TestDist()
        {
            for (int i = 0; i < HashTable.Length; i++)
            {
                Console.WriteLine(HashTable[i].Count);
            }

        }
    }

    class LinkedList
    {
        public class Node
        {
            public int data;
            public Node next;
            public Node(int d)
            {
                data = d;
            }
        }

        public Node head;

        public void AppendEnd(int d)
        {
            Node end = new Node(d);
            if (head == null)
            {
                head = end;
                return;
            }

            Node curr = head;

            while (curr.next != null)
            {
                curr = curr.next;
            }

            curr.next = end;
        }

        public void PrintList()
        {
            Node curr = head;
            while (curr != null)
            {
                Console.WriteLine(curr.data);
                curr = curr.next;
            }
            Console.WriteLine(" ");
        }

        public void MergeAtPoint(Node a, Node b, int point)
        {
            int count = 0;

            while (a != null)
            {
                count++;
                if (count == point)
                {
                    a.next = b.next;
                    return;
                }

                a = a.next;
                b = b.next;
            }
        }
    }

    class BinarySearchTree
    {
        public BSTNode head;
        public class BSTNode
        {
            public int key;
            public BSTNode left;
            public BSTNode right;
            public BSTNode parent;
            public BSTNode(int k) { key = k; }
        }

        public void Insert(int k)
        {
            BSTNode entry = new BSTNode(k);

            if (head == null)
            {
                head = entry;
                return;
            }
            BSTNode ptr = head;
            while (true)
            {
                //Duplicate
                if (ptr.key == k)
                {
                    return;
                }
                else if (ptr.key > k)
                {
                    if (ptr.left == null)
                    {
                        ptr.left = entry;
                        return;
                    }
                    ptr = ptr.left;
                }
                else
                {
                    if (ptr.right == null)
                    {
                        ptr.right = entry;
                        return;
                    }
                    ptr = ptr.right;
                }
            }
        }

        public void PrintPreOrder(BSTNode n)
        {
            if (n != null)
            {
                string s = "";
                s += n.key + " : ";
                if (n.left != null)
                    s += n.left.key;
                s += "-";
                if (n.right != null)
                    s += n.right.key;

                Console.WriteLine(s);
                PrintPreOrder(n.left);
                PrintPreOrder(n.right);
            }
        }

        public void PrintInOrder(BSTNode n)
        {
            if (n != null)
            {
                PrintInOrder(n.left);
                string s = "";
                s += n.key + " : ";
                if (n.left != null)
                    s += n.left.key;
                s += "-";
                if (n.right != null)
                    s += n.right.key;
                Console.WriteLine(s);
                PrintInOrder(n.right);
            }
        }

        public void PrintPostOrder(BSTNode n)
        {
            if (n != null)
            {
                PrintPostOrder(n.left);
                PrintPostOrder(n.right);
                string s = "";
                s += n.key + " : ";
                if (n.left != null)
                    s += n.left.key;
                s += "-";
                if (n.right != null)
                    s += n.right.key;
                Console.WriteLine(s);
            }
        }
    }

    class Graph
    {
        public class GraphNode
        {
            public int key;
            public List<GraphNode> adjacent;

            public GraphNode(int k)
            {
                key = k;
            }

            public void AddNeighbor(int k)
            {
                foreach (GraphNode n in adjacent)
                {
                    if (n.key == k)
                        return;
                }
                GraphNode entry = new GraphNode(k);
                adjacent.Add(entry);
            }
        }

        public GraphNode head = new GraphNode(0);

        void DepthFirstSearch()
        {
            //too keep track of visited nodes, create an array holding them, check if it contains then proceed

            // if root is null return
            if (head == null)
                return;

            List<GraphNode> visited = new List<GraphNode>();
            DFSUtil(head, visited);

        }

        void DFSUtil(GraphNode n, List<GraphNode> v)
        {
            v.Add(n);

            //Do work here
            Console.WriteLine(n.key);

            foreach (GraphNode c in n.adjacent)
            {
                if (!v.Contains(c))
                {
                    DFSUtil(c, v);
                }
            }


        }

        void BreadthFirstSearch()
        {
            if (head == null)
                return;

            //create a queue to hold a list of nodes to search
            Queue<GraphNode> que = new Queue<GraphNode>();

            // also need a way to see if the node has been visited
            List<GraphNode> visited = new List<GraphNode>();


            //for all neighbors, add to the list to be searched if not visted first
            while (que.Peek() != null)
            {
                GraphNode curr = que.Dequeue();
                visited.Add(curr);
                //Do work
                Console.WriteLine(curr.key);

                foreach (GraphNode n in curr.adjacent)
                {
                    if (!visited.Contains(n))
                        que.Enqueue(n);
                }
            }
        }

        //better version so the above methods
        //passes in a root node - not bound to graph class
        //uses hashset instead of a list - quicker lookup
        void dfs(GraphNode head)
        {
            if (head == null)
                return;

            HashSet<GraphNode> vis = new HashSet<GraphNode>();
            dfshelper(head, vis);
        }

        void dfshelper(GraphNode head, HashSet<GraphNode> vis)
        {
            //do work

            foreach (GraphNode n in head.adjacent)
            {
                if (!vis.Contains(n))
                {
                    vis.Add(n);
                    dfshelper(n, vis);
                }
            }
        }

        void bfs(GraphNode head)
        {
            if (head == null)
                return;

            Queue<GraphNode> que = new Queue<GraphNode>();
            HashSet<GraphNode> vis = new HashSet<GraphNode>();

            que.Enqueue(head);

            while (que.Peek() != null)
            {
                GraphNode curr = que.Dequeue();
                vis.Add(curr);
                //do work

                foreach (GraphNode n in curr.adjacent)
                {
                    if (!vis.Contains(n))
                        que.Enqueue(n);
                }
            }

        }
    }

    //Stacks are Last-In-First-Out (LIFO)
    public class MyStack<T>
    {
        private class StackNode<T>
        {
            public T data;
            public StackNode<T> next;
            public StackNode(T data) { this.data = data; }
        }

        private StackNode<T> top;
        
        public T pop()
        {
            if (top == null) throw new Exception();

            T item = top.data;
            top = top.next;
            return item;
        }

        public void Push(T data)
        {
            StackNode<T> s = new StackNode<T>(data);
            s.next = top;
            top = s;
        }

        public T Peek()
        {
            if (top == null) throw new Exception();
            return top.data;
        }

        public bool IsEmpty()
        {
            return top == null;
        }
    }

    //Queues are First-In-First-Out (FIFO)
    public class MyQueue<T>
    {
        private class QueueNode<T>
        {
            public T data;
            public QueueNode<T> next;

            public QueueNode(T data)
            {
                this.data = data;
            }
        }

        private QueueNode<T> first;
        private QueueNode<T> last;

        public void add(T item)
        {
            QueueNode<T> q = new QueueNode<T>(item);
            if (last != null)
            {
                last.next = q;
                last = q;
            }
            else
            {
                first = q;
                last = q;
            }
        }

        public T Remove()
        {
            if (first == null) throw new Exception();
            T data = first.data;
            first = first.next;
            if (first == null)
                last = null;
            return data;
        }

        public T peek()
        {
            if (first == null) throw new Exception();
            return first.data;
        }

        public bool IsEmpty()
        {
            return first == null;
        }
    }

    //Made for 3.1
    class FixedMultiStack
    {
        private int numberOfStacks = 3;
        private int stackCapacity;
        private int[] values;
        private int[] sizes;

        public FixedMultiStack(int stackSize)
        {
            stackCapacity = stackSize;
            values = new int[stackSize * numberOfStacks];
            sizes = new int[numberOfStacks];
        }

        public void push(int stackNum, int value)
        {
            //check that we have space for the next element
            if (isFull(stackNum))
                throw new Exception();
            //increment stack pointer and then update top value
            sizes[stackNum]++;
            values[indexOfTop(stackNum)] = value;
        }

        public int pop(int stackNum)
        {
            if (isEmpty(stackNum))
                throw new Exception();
            int topIndex = indexOfTop(stackNum);   //find topindex of that stack
            int value = values[topIndex];          //grab that value
            values[topIndex] = 0;                  //clear that value
            sizes[stackNum]--;                     //decrement index counter
            return value;                          //return value
        }

        public int peek(int stackNum)
        {
            if (isEmpty(stackNum))
                throw new Exception();
            return values[indexOfTop(stackNum)];
        }

        public bool isEmpty(int stackNum)
        {
            return sizes[stackNum] == 0;
        }

        public bool isFull(int stackNum)
        {
            return sizes[stackNum] == stackCapacity;
        }

        public int indexOfTop(int stackNum)
        {
            int offset = stackNum * stackCapacity;
            int size = sizes[stackNum];
            return offset + size - 1;
        }

    }

    //Made For Question 4.7
    class Project
    {
        private List<Project> children = new List<Project>();
        private Dictionary<string, Project> map = new Dictionary<string, Project>();
        private string name;
        private int dependancies = 0;

        public Project(string n) { name = n; }

        public void addNeighbor(Project node)
        {
            if (!map.ContainsKey(node.getName()))
            {
                children.Add(node);
                map.Add(node.getName(), node);
                node.incrementDependencies();
            }
        }

        public void incrementDependencies() { dependancies++; }
        public void decrementDependencies() { dependancies--; }

        public string getName() { return name; }
        public List<Project> getChildren() { return children; }
        public int getNumberDependencies() { return dependancies; }
    }   
    class ProjectGraph
    {
        private List<Project> nodes = new List<Project>();
        private Dictionary<string, Project> map = new Dictionary<string, Project>();

        public Project getOrCreateNode(string name)
        {
            if (!map.ContainsKey(name))
            {
                Project node = new Project(name);
                nodes.Add(node);
                map.Add(name, node);
            }

            Project proj;
            map.TryGetValue(name, out proj);
            return proj;
        }

        public void addEdge(string startName, string endName)
        {
            Project start = getOrCreateNode(startName);
            Project end = getOrCreateNode(endName);
            start.addNeighbor(end);
        }

        public List<Project> getNodes() { return nodes; }

        public void PrintAll()
        {
            foreach (Project p in nodes)
            {
                string printable = p.getName() + " : ";

                List<Project> children = p.getChildren();
                foreach (Project c in children)
                {
                    printable += c.getName() + ", ";
                }
                Console.WriteLine(printable);
            }
        }
    }

    
    #endregion

    class CrackingTheInterviewQuestions
    {
        //Section 1.  Arrays and Strings
        #region Section 1 ----- 9/9 Complete -----
        #region 1.1-IsUnique
        //Implement an algorithm to determine if a string has all unique characters. What if you cannot use additional data structures?
        public bool IsUnique(string str)
        {
            if (str.Length > 256)
                return false;

            bool[] boolArr = new bool[256];
            char[] charArr = str.ToCharArray();

            for (int i = 0; i < charArr.Length; i++)
            {
                int index = (int)charArr[i];

                if (boolArr[index] == true)
                    return false;
                else
                    boolArr[index] = true;
            }
            return true;
        }
        #endregion

        #region 1.2-CheckPermutation
        //Given two strings, write a method to decide if one is a permutation of the other.
        public bool CheckPermutation(string strA, string strB)
        {
            if (strA.Length != strB.Length)
                return false;

            char[] arrA = strA.ToCharArray();
            char[] arrB = strB.ToCharArray();
            Array.Sort(arrA);
            Array.Sort(arrB);

            //Loop to check each character
            for (int i = 0; i < arrA.Length; i++)
            {
                if (arrA[i] != arrB[i])
                    return false;
            }
            return true;
        }
        #endregion

        #region 1.3-URLify
        // Write a method to replace all spaces in a string with '%20'. You may assume that the string 
        // has sufficient space at the end to hold the additional characters, and that you are given the "true" length of the string. 
        // (Note: If implementing in Java, please use a character array so that you can perform this operation in place.)
        public char[] Urlify(char[] arr, int trueLength)
        {
            //The str length is longer then true count due to white space
            //true count is the number of valid characters

            int spaceCount = 0;
            int index;

            for (int i = 0; i < trueLength; i++)
            {
                if (arr[i] == ' ')
                    spaceCount++;
            }

            //give the total index extra spaces depending on amount of white spaces
            index = trueLength + spaceCount * 2;

            //mark the end of the array
            if (trueLength < arr.Length) arr[trueLength] = '\0';

            //Interates from the end to the front. 

            for (int i = trueLength - 1; i >= 0; i--)
            {
                //If it finds a whitespace, replaces 3 with those characters
                if (arr[i] == ' ')
                {
                    arr[index - 1] = '0';
                    arr[index - 2] = '2';
                    arr[index - 3] = '%';
                    index = index - 3;
                }
                //If not then it just puts the character in place and decrements the index
                else
                {
                    arr[index - 1] = arr[i];
                    index--;
                }
            }
            return arr;
        }
        #endregion

        #region 1.4-PalindromePermutation
        // Given a string, write a function to check if it is a permutation of a palindrome. 
        // A palindrome is a word or phrase that is the same forwards and backwards. 
        // A permutation is a rearrangement of letters. The palindrome does not need to be limited to just dictionary words.
        public bool PalindromePermutation(string str)
        {
            //this is currently case sensitive
            //to make it case insenseitive, i need to do either find a new sorting mech, or a new comparison mech


            // sort the characters?
            char[] arr = str.ToCharArray();
            Array.Sort(arr);
            // check how many matches are even and odd?
            int oddCount = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                int matchCount = 1;
                if (arr[i] == ' ')
                    continue;
                char letter = arr[i];
                int counter = 1;

                if (i + counter >= arr.Length)
                    continue;
                while (arr[i + counter] == letter)
                {
                    matchCount++;
                    counter++;
                    if (i + counter >= arr.Length)
                        break;
                }
                //was odd
                if (matchCount % 2 == 1)
                {
                    oddCount++;
                }
                i = i + matchCount - 1;
            }

            if (oddCount > 1)
                return false;
            else
                return true;
        }

        //Book version does not work as intended.
        //Believe the problem lies with conversion between
        //Char and a numberic value - table isnt generating properly
        public bool PalindromePermutationBook(string str)
        {
            int[] table = buildCharFrequencyTable(str);
            return checkMaxOneOdd(table);
        }

        bool checkMaxOneOdd(int[] table)
        {
            bool foundOdd = false;
            foreach (int num in table)
            {
                if (num % 2 == 1)
                {
                    if (foundOdd)
                        return false;
                    foundOdd = true;
                }
            }
            return true;
        }

        int[] buildCharFrequencyTable(string str)
        {
            //Creates the table
            //int[] table = new int[(int)Char.GetNumericValue('z') - (int)Char.GetNumericValue('a') + 1];
            int[] table = new int[Convert.ToInt32('z') - Convert.ToInt32('a') + 1];


            foreach (char c in str.ToCharArray())
            {
                int x = getCharNumber(c);
                if (x != -1)
                    table[x]++;
            }
            return table;
        }

        int getCharNumber(char c)
        {
            int a = Convert.ToInt32('a');
            int z = Convert.ToInt32('z');
            int val = Convert.ToInt32(c);
            if (a <= val && val <= z)
            {
                return val - a;
            }
            return -1;
        }
        #endregion

        #region 1.5-OneAway-Incomplete
        // There are three types of edits that can be performed on strings: insert a character, remove a character, or replace a character. 
        // Given two strings, write a function to check if they are one edit (or zero edits) away.
        public bool OneAway(string strA, string strB)
        {
            //Checking their sizes, if they are 2 or more away, they are too mistached
            if ((Math.Abs(strA.Length - strA.Length) >= 2))
            {
                return false;
            }

            //replace
            int mismatchcount = 0;
            for (int i = 0; i < strB.Length; i++)
            {
                //incase B is smaller than A
                if (strB.Length - 1 < i)
                {
                    mismatchcount++;
                    continue;
                }
                //If letters dont match
                if (strA[i] != strB[i])
                    mismatchcount++;
            }
            //If B is bigger, its another mismatch.. if its 2 bigger it'll be caught earlier
            if (strA.Length < strB.Length)
                mismatchcount++;

            if (mismatchcount > 1)
                return false;

            //remove one
            //walk through each, if one is mismatch, have them check next in line to see if they can continue;
            // Add and remove are the same thing, the mismatch noted and indexed approperiately

            int smallerStrSize = Math.Min(strA.Length, strB.Length);
            int Aindex = 0;
            int Bindex = 0;
            for (int i = 0; i < smallerStrSize; i++)
            {
                //Mismatch
                if (strA[i + Aindex] != strB[i + Aindex])
                {
                    mismatchcount++;
                    if (strA[i + 1] == strB[i])
                    {
                        Aindex++;
                    }
                    else if (strB[i + 1] == strA[i])
                    {
                        Bindex++;
                    }
                    else
                        mismatchcount++;
                }
            }
            return true;

        }
        #endregion

        #region 1.6-StringCompression
        // Implement a method to perform basic string compression using the counts of repeated characters. For example, the string aabcccccaaa would become a2blc5a3. 
        // If the "compressed" string would not become smaller than the original string, your method should return the original string. 
        // You can assume the string has only uppercase and lowercase letters (a - z).
        public string StringCompression(string str)
        {
            string strCompressed = "";

            if (str.Length < 3)
                return str;

            int num;
            for (int i = 0; i < str.Length; i++)
            {
                num = 1;
                char letter = str[i];

                int start = i;
                while (start+num < str.Length)
                {
                    if (str[start + num] == letter)
                    {
                        num++;
                        i++;
                    }
                    else
                        break;
                }


                //At this point the num represents number repeated, and letter is the char
                strCompressed += (letter.ToString() + num.ToString());

            }

            if (str.Length <= strCompressed.Length)
                return str;
            else
                return strCompressed;
        }
        #endregion

        #region 1.7-RotateMatrix
        // Given an image represented by an NxN matrix, where each pixel in the image is 4 bytes, write a method to rotate the image by 90 degrees.
        // Can you do this in place? - No I cannot... You can ... ugh
        // this solution does not do it in place- but however it can handle NxM image size - little more flexible
        //Time O(n^2), Space O(n^2)
        public int[,] RotateMatrix(int[,] matrix)
        {
            int matrixI = matrix.GetLength(0);
            int matrixJ = matrix.GetLength(1);
            int[,] rotMatrix = new int[matrixJ, matrixI];

            int rotmatrixI = rotMatrix.GetLength(0) - 1;
            int rotmatrixJ = rotMatrix.GetLength(1) - 1;

            for (int j = rotmatrixJ; j >= 0; j--)
            {
                for (int i = 0; i < matrixJ; i++)
                {
                    rotMatrix[i, j] = matrix[rotmatrixJ - j, i];
                }
            }


            return rotMatrix;
        }
        #endregion

        #region 1.8-ZeroMatrix
        // Write an algorithm such that if an element in an MxN matrix is 0, its entire row and column are set to 0.
        public bool ZeroMatrix(int[,] matrix)
        {
            if (matrix == null)
                return false;

            //to keep track of which row and column will be set to zeros
            bool[] boolColumns = new bool[matrix.GetLength(1)];
            bool[] boolRows = new bool[matrix.GetLength(0)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        boolColumns[j] = true;
                        boolRows[i] = true;
                    }
                }
            }
            for (int i = 0; i < boolColumns.Length; i++)
            {
                if (boolColumns[i] == true)
                    ZeroColumn(i, matrix);
            }
            for (int i = 0; i < boolRows.Length; i++)
            {
                if (boolRows[i] == true)
                    ZeroRow(i, matrix);
            }

            return true;
        }

        void ZeroRow(int num, int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
                matrix[num, i] = 0;
        }
        void ZeroColumn(int num, int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
                matrix[i, num] = 0;
        }

        #endregion

        #region 1.9-StringRotation
        // Assume you have a method isSubstringwhich checks ifoneword isa substring of another. 
        // Given two strings, sl and s2, write code to check if s2 is a rotation of sl using only one call to isSubstring 
        // (e.g., "waterbottle" is a rotation of"erbottlewat")
        public bool StringRotation(string s1, string s2)
        {
            // waterbottle s2     terbottlewa  s2
            // waterbottle s2     terbottlewaterbottlewa s3
            // s2 will exist inside s3 if rotation is true;

            //Need to check to see if the strings are the same length and more than zero
            if (s1.Length != s2.Length || s1.Length == 0)
                return false;

            //is s2 a rotation of s1
            string s3 = s2 + s2;
            return IsSubstring(s1, s3);
        }

        bool IsSubstring(string s1, string s2)
        {
            if (s2.Contains(s1))
                return true;
            return false;
        }
        #endregion
        #endregion

        //Section 2.  Linked Lists
        #region Section 2 ----- 8/8 Complete -----
        #region 2.1-RemoveDups
        //Write code to remove duplicates from an unsorted linked list
        public void RemoveDups(LinkedList.Node root)
        {
            HashSet<int> set = new HashSet<int>();
            LinkedList.Node n = root;
            LinkedList.Node prev = null;
            while (n != null)
            {
                if (set.Contains(n.data))
                {
                    prev.next = n.next;
                }
                else
                {
                    set.Add(n.data);
                    prev = n;
                }
                n = n.next;
            }
        }
        #endregion

        #region 2.2-ReturnKthToLast
        // Implement an algorithm to find the kth to last element of a singly linked list. 
        public int ReturnKToLastNode(LinkedList.Node root, int k)
        {
            if (root == null)
                return 0;

            int index = ReturnKToLastNode(root.next, k) + 1;
            if (index == k)
            {
                Console.WriteLine("K to Last Node is: " + root.data);
            }
            return index;
        }
        #endregion

        #region 2.3-DeleteMiddleNode
        // Implement an algorithm to delete a node in the middle of a singly linked list, given only access to that node
        // (i.e., any node but the first and last node, not necessarily the exact middle) 
        public void DeleteMiddleNode(LinkedList.Node root)
        {
            if (root == null)
                return;
            LinkedList.Node next = root.next;
            if (next == null)
                root = null;
            else
            {
                root.data = next.data;
                root.next = next.next;
            }
        }
        #endregion

        #region 2.4-Partition
        //  Write code to partition a linked list around a value x, such that all nodes less than x come before all nodes greater than or equal to x. 
        // If xis contained within the list, the values of x only need to be after the elements less than x (see below). 
        // The partition element x can appear anywhere in the "right partition"; it does not need to appear between the left and right partitions. 
        public void Partition(LinkedList.Node root, int point)
        {
            LinkedList.Node curr = root;
            if (curr == null)
            {
                return;
            }

            List<int> lesser = new List<int>();
            List<int> greater = new List<int>();
            List<int> match = new List<int>();

            while (curr != null)
            {
                if (curr.data < point)
                {
                    lesser.Add(curr.data);
                }
                else if (curr.data == point)
                {
                    match.Add(curr.data);
                }
                else
                {
                    greater.Add(curr.data);
                }
                curr = curr.next;
            }

            curr = root;

            foreach (int n in lesser)
            {
                curr.data = n;
                curr = curr.next;
            }

            foreach (int n in match)
            {
                curr.data = n;
                curr = curr.next;
            }

            foreach (int n in greater)
            {
                curr.data = n;
                curr = curr.next;
            }
        }
        #endregion

        #region 2.5-SumLists
        // You have two numbers represented by a linked list, where each node contains a single digit. 
        // The digits are stored in reverse order, such that the 1 's digit is at the head of the list. 
        // Write a function that adds the two numbers and returns the sum as a linked list.
        public void SumLists(LinkedList.Node first, LinkedList.Node second)
        {
            List<int> firstList = new List<int>();
            List<int> secondList = new List<int>();

            LinkedList.Node curr = first;
            while (curr != null)
            {
                firstList.Add(curr.data);
                curr = curr.next;
            }

            curr = second;
            while (curr != null)
            {
                secondList.Add(curr.data);
                curr = curr.next;
            }


            string numberOne = "";
            string numberTwo = "";


            for (int i = firstList.Count - 1; i >= 0; i--)
            {
                numberOne += firstList[i];
            }

            for (int i = secondList.Count - 1; i >= 0; i--)
            {
                numberTwo += secondList[i];
            }

            int sum = Convert.ToInt32(numberOne) + Convert.ToInt32(numberTwo);
            char[] sumarr = sum.ToString().ToCharArray();
            LinkedList ll = new LinkedList();
            for (int i = sumarr.Length - 1; i >= 0; i--)
            {
                int n = (int)Char.GetNumericValue(sumarr[i]);
                ll.AppendEnd(n);
            }

            ll.PrintList(); ;

        }
        #endregion

        #region 2.6-PalindromeCheck
        // Implement a function to check if a linked list is a palindrome.
        public bool PalindromeCheck(LinkedList.Node n)
        {
            List<int> forward = new List<int>();

            LinkedList.Node curr = n;
            while (curr != null)
            {
                forward.Add(curr.data);
                curr = curr.next;
            }

            int size = forward.Count - 1;
            for (int i = 0; i < size; i++)
            {
                if (forward[i] != forward[size - i])
                {
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region 2.7-Intersection
        // Given two (singly) linked lists, determine if the two lists intersect. Return the intersecting node. 
        // Note that the intersection is defined based on reference, not value. 
        // That is, if the kth node of the first linked list is the exact same node (by reference) as the jth node of the second linked list, then they are intersecting. 
        public LinkedList.Node Intersection(LinkedList.Node a, LinkedList.Node b)
        {
            // Find the tail of both lists
            LinkedList.Node firstTail = a;
            int firstCount = 0;
            while (firstTail != null)
            {
                firstCount++;
                firstTail = firstTail.next;
            }
            //First list tail exists, and count is the number on first list

            LinkedList.Node secondTail = b;
            int secondCount = 0;
            while (secondTail != null)
            {
                secondCount++;
                secondTail = secondTail.next;
            }

            //difference between both counts
            int difference = Math.Abs(firstCount - secondCount);

            if (firstTail != secondTail)
                return null;

            if (firstCount > secondCount)
            {
                return IntersectionHelper(a, b, difference);
            }
            else
            {
                return IntersectionHelper(b, a, difference);
            }
        }

        public LinkedList.Node IntersectionHelper(LinkedList.Node a, LinkedList.Node b, int diff)
        {
            LinkedList.Node aPtr = a;
            LinkedList.Node bPtr = b;

            for (int i = 0; i < diff; i++)
            {
                a = a.next;
            }

            while (a != null)
            {
                if (a == b)
                {
                    return a;
                }

                a = a.next;
                b = b.next;
            }
            return null;
        }
        #endregion

        #region 2.8-LoopDetection
        //Given a circular linked list, implement an algorithm that returns the node at the beginning of the loop.
        public LinkedList.Node LoopDetection(LinkedList.Node root)
        {
            if (root == null)
                return null;

            HashSet<LinkedList.Node> hash = new HashSet<LinkedList.Node>();
            LinkedList.Node curr = root;
            hash.Add(curr);
            while (curr.next != null)
            {
                curr = curr.next;
                if (!hash.Contains(curr))
                {
                    hash.Add(curr);
                }
                else
                {
                    return curr;
                }
            }
            //if the code reaches this point, this isnt a corrupted list, it nulled out at the end.
            return null;
        }
        #endregion
        #endregion

        //Section 3.  Stacks and Queues
        #region Section 3 ----- 1/6 Complete -----
        #region 3.1-ThreeInOne
        // Describe how you could use a single array to implement three stacks
        // First approach - split the stack into three equal portions
        // This data structure will be attempted in the Data structures section
        #endregion
        #region 3.2-StackMin
        //How would you design a stack which, in addition to push and pop, has a function
        //min which return the minimum element? Push, pop, and min should all operate in O(1) time
        public class MyStack
        {
            private class StackNode
            {
                public int data;
                public StackNode next;
                public StackNode prevMin;
                public StackNode(int data) {
                    this.data = data;
                }
            }

            private StackNode top;
            private StackNode min;

            public int pop()
            {
                if (top == null) throw new Exception();

                if (top == min)
                    min = top.prevMin;

                int item = top.data;
                top = top.next;
                return item;
            }

            public int GetMinimum()
            {
                if (min == null) throw new Exception();
                else
                    return min.data;
            }

            public void Push(int data)
            {
                StackNode s = new StackNode(data);
                s.prevMin = min;
                s.next = top;
                top = s;

                if (min == null) min = s;
                else if (s.data < min.data) s = min;
            }

            public int Peek()
            {
                if (top == null) throw new Exception();
                return top.data;
            }

            public bool IsEmpty()
            {
                return top == null;
            }
        }
        #endregion
        #endregion

        //Section 4.  Trees and Graphs
        #region Section 4 ----- 8/12 Complete -----
        #region 4.1-RouteBetweenNodes
        //Given a directed graph, design an algorithm to find out whether there is a route between two nodes.
        public bool RouteBetweenNodes(Graph.GraphNode start, Graph.GraphNode end)
        {
            if (start == null)
                return false;

            //List<GraphNode> visited = new List<GraphNode>();
            HashSet<Graph.GraphNode> visited = new HashSet<Graph.GraphNode>();
            Queue<Graph.GraphNode> que = new Queue<Graph.GraphNode>();

            visited.Add(start);
            que.Enqueue(start);

            while (que.Peek() != null)
            {
                Graph.GraphNode curr = que.Dequeue();
                visited.Add(curr);

                //Check for match here
                if (curr == end)
                {
                    return true;
                }

                foreach (Graph.GraphNode n in curr.adjacent)
                {
                    if (!visited.Contains(n))
                        que.Enqueue(n);
                }
            }
            return false;
        }
        #endregion

        #region 4.2-MinimalTree
        //Given a sorted (increasing order) array with unique integer elements, write an algorithm to create a binary search tree with minimal height. 
        public BinarySearchTree MinimalTree(int[] arr)
        {
            BinarySearchTree tree = new BinarySearchTree();

            int mid = (int)(arr.Length - 1) / 2;
            tree.Insert(arr[mid]);

            tree = MTHelper(0, mid, arr, tree);
            tree = MTHelper(mid + 1, arr.Length - 1, arr, tree);

            return tree;
        }

        public BinarySearchTree MTHelper(int start, int end, int[] arr, BinarySearchTree tree)
        {
            //Add middle element, call both sides
            int mid = (int)(end + start) / 2;

            if (start - end == 0)
            {
                //base case, reached 1, insert node and return tree;
                tree.Insert(arr[start]);
                return tree;
            }

            tree.Insert(arr[mid]);

            tree = MTHelper(start, mid, arr, tree);
            tree = MTHelper(mid + 1, end, arr, tree);

            return tree;
        }
        #endregion

        #region 4.3 ListOfDepths
        //Given a binary tree, design an algorithm which creates a linked list of all the nodes at each depth 
        //(e.g., if you have a tree with depth D, you'll have D linked lists). 
        public List<LinkedList<BinarySearchTree.BSTNode>> ListOfDepths(BinarySearchTree.BSTNode root)
        {
            if (root == null)
                return null;

            int level = 0;
            List<LinkedList<BinarySearchTree.BSTNode>> lists = new List<LinkedList<BinarySearchTree.BSTNode>>();
            ListOfDepths(root, lists, level);

            return lists;
        }


        //This is the helper function
        void ListOfDepths(BinarySearchTree.BSTNode root, List<LinkedList<BinarySearchTree.BSTNode>> lists, int level)
        {
            if (lists[level] == null)
            {
                //Havent reached this level before, initialize a linked list for this and add the curr root;
                LinkedList<BinarySearchTree.BSTNode> list = new LinkedList<BinarySearchTree.BSTNode>();
                list.AddLast(root);
            }
            else
            {
                //have reached this level before, just add the root to it current list
                lists[level].AddLast(root);
            }

            if (root.left != null)
            {
                ListOfDepths(root.left, lists, level + 1);
            }

            if (root.right != null)
            {
                ListOfDepths(root.right, lists, level + 1);
            }
        }
        #endregion

        #region 4.4-CheckBalanced
        //4.4 Check Balanced
        // Implement a function to check if a binary tree is balanced. For the purposes of this question, 
        // a balanced tree is defined to be a tree such that the heights of the two subtrees of any node never differ by more than one
        public bool CheckBalanced(BinarySearchTree.BSTNode head)
        {
            if (head == null)
                return false;
            // implement a simple traversal

            int maxdepthleft = 1;
            int maxdepthright = 1;
            int mindepthleft = 1;
            int mindepthright = 1;

            if (head.left != null)
            {
                maxdepthleft = MaxTreeDepth(head.left, maxdepthleft);
                mindepthleft = MinTreeDepth(head.left, mindepthleft);
            }
            if (head.right != null)
            {
                maxdepthright = MaxTreeDepth(head.right, maxdepthright);
                mindepthright = MinTreeDepth(head.right, mindepthright);
            }

            int MaxDepth = Math.Max(maxdepthleft, maxdepthright);
            int MinDepth = Math.Min(mindepthleft, mindepthright);

            Console.WriteLine(MinDepth + ", " + MaxDepth);

            if (Math.Abs(MaxDepth - MinDepth) >= 2)
                return false;
            else
                return true;
        }

        int MaxTreeDepth(BinarySearchTree.BSTNode head, int count)
        {
            int tree1 = count;
            int tree2 = count;


            if (head.left != null)
            {
                tree1 = MaxTreeDepth(head.left, tree1 + 1);
            }
            if (head.right != null)
            {
                tree2 = MaxTreeDepth(head.right, tree2 + 1);
            }

            return Math.Max(tree1, tree2);

        }

        int MinTreeDepth(BinarySearchTree.BSTNode head, int count)
        {
            int tree1 = count;
            int tree2 = count;


            if (head.left != null)
            {
                tree1 = MaxTreeDepth(head.left, tree1 + 1);
            }
            if (head.right != null)
            {
                tree2 = MaxTreeDepth(head.right, tree2 + 1);
            }

            return Math.Min(tree1, tree2);
        }
        #endregion

        #region 4.5-ValidateBST
        //Implement a function to check if a binary tree is a binary search tree
        public bool ValidateBST(BinarySearchTree.BSTNode root)
        {

            //Traverse the tree, if any point has the left node greater than the right node. .
            //Is there a case where the above could not exist, and still be wrong further down?
            //Create a in-order traversal pattern. Add each to a list, check to see if the list is sorted

            List<BinarySearchTree.BSTNode> list = new List<BinarySearchTree.BSTNode>();
            bool isValid = true;
            IsBinarySearchTreeHelper(root, list, isValid);

            return isValid;

        }

        void IsBinarySearchTreeHelper(BinarySearchTree.BSTNode root, List<BinarySearchTree.BSTNode> list, bool valid)
        {
            if (valid == false)
                return;
            if (root == null)
                return;

            IsBinarySearchTreeHelper(root.left, list, valid);

            if (valid == false)
                return;

            if (list.Count == 0)
                list.Add(root);
            else if (list[list.Count - 1].key >= root.key)
            {
                valid = false;
                return;
            }
            list.Add(root);

            IsBinarySearchTreeHelper(root.right, list, valid);
        }
        #endregion

        #region 4.6-Successor
        //Write an algorithm to find the "next" node (in-order successor) of a given node, you may assume a node knows their parent
        public BinarySearchTree.BSTNode Successor(BinarySearchTree.BSTNode root)
        {
            //check to see if right child exists, if it does, traverse down its left side to see if a lesser exists
            if (root.right != null)
                return FurthestLeftNode(root.right);


            if (root.parent == null)
                return root;

            ////traverse up parent path until parent has a right child that isnt the previous child
            while (root.parent != null)
            {
                if (root.parent.right != root)
                {
                    if (root.parent.right == null)
                        return root;
                    root = root.parent.right;
                    break;
                }
            }

            return FurthestLeftNode(root);
        }

        BinarySearchTree.BSTNode FurthestLeftNode(BinarySearchTree.BSTNode root)
        {
            while (root.left != null)
                root = root.left;
            return root;
        }
        #endregion

        #region 4.7-BuildOrder
        // You are given a list of projects and a list of dependencies (which is a list of pairs of projects, where the second project is dependent on the first project). 
        // All of a project's dependencies must be built before the project is. Find a build order that will allow the projects to be built. 
        // If there is no valid build order, return an error.
        public Project[] BuildOrder(string[] projects, string[,] dependancies)
        {
            ProjectGraph graph = buildGraph(projects, dependancies);
            return orderProjects(graph.getNodes());
        }

        ProjectGraph buildGraph(string[] projects, string[,] dependancies)
        {
            ProjectGraph graph = new ProjectGraph();
            foreach (string s in projects)
                graph.getOrCreateNode(s);
            for (int i = 0; i <= projects.Length; i++)
            {
                string first = dependancies[i, 1];
                string second = dependancies[i, 0];
                graph.addEdge(first, second);
            }
            return graph;
        }

        Project[] orderProjects(List<Project> projects)
        {
            Project[] order = new Project[projects.Capacity];

            //add roots to the build order first
            int endOfList = addNonDependent(order, projects, 0);
            
            int toBeProcessed = 0;
            while (toBeProcessed < order.Length - 1)
            {
                Project current = order[toBeProcessed];

                //If this ever is null, there are no remaining objects with zero dependancies - no valid order exists
                if (current == null)
                {
                    return null;
                }

                //remove this project as a possible dependency, since it made the order list
                List<Project> children = current.getChildren();
                foreach(Project proj in children)
                {
                    proj.decrementDependencies();
                }

                //iterate through the list again, looking for non dependant projects of the projects you just cleared
                endOfList = addNonDependent(order, children, endOfList);
                toBeProcessed++;
            }
            return order;
        }

        int addNonDependent(Project[] order, List<Project> projects, int offset)
        {
            foreach (Project proj in projects)
            {
                if (proj.getNumberDependencies() == 0)
                {
                    order[offset] = proj;
                    offset++;
                }
            }
            return offset;
        }
        #endregion

        #region 4.8-FirstCommonAncestor
        // Design an algorithm and write code to find the first common ancestor of two nodes in a binary tree. 
        // Avoid storing additional nodes in a data structure. NOTE: This is not necessarily a binary search tree
        public BinarySearchTree.BSTNode FirstCommonAncestor(BinarySearchTree.BSTNode root, BinarySearchTree.BSTNode node1, BinarySearchTree.BSTNode node2)
        {
            if (root == null || node1 == null || node2 == null)
                return null;

            //Store parent/child path.. First variable is child, second is parent
            Dictionary<BinarySearchTree.BSTNode, BinarySearchTree.BSTNode> dict = new Dictionary<BinarySearchTree.BSTNode, BinarySearchTree.BSTNode>();
            
            if (root.left != null)
            {
                dict.Add(root.left, root);
                FindNode(root.left, node1, dict);
                FindNode(root.left, node2, dict);
            }

            if( root.right != null)
            {
                dict.Add(root.right, root);
                FindNode(root.right, node1, dict);
                FindNode(root.right, node2, dict);
            }

            BinarySearchTree.BSTNode v;
            BinarySearchTree.BSTNode v2;
            if (!dict.TryGetValue(node1, out v) || !dict.TryGetValue(node2, out v2))
            {
                //One or both of the requested nodes were not found
                return null;
            }

            HashSet<BinarySearchTree.BSTNode> hash = new HashSet<BinarySearchTree.BSTNode>();
            BinarySearchTree.BSTNode curr1 = node1;
            while (dict.TryGetValue(curr1, out curr1))
            {
                hash.Add(curr1);
            }

            BinarySearchTree.BSTNode curr2 = node2;
            while (dict.TryGetValue(curr2, out curr2))
            {
                if (hash.Contains(curr2))
                    return curr2;
                //No need to add these to the hashset, i wont be scanning a third time, if no matches occur here, no common ancestors
            }
            return null;
        }

        //Calling method
        public BinarySearchTree.BSTNode FirstCommonAncestorBook(BinarySearchTree.BSTNode root, BinarySearchTree.BSTNode node1, BinarySearchTree.BSTNode node2)
        {
            //Check to see if valid input
            if (root == null || node1 == null || node2 == null)
                return root;

            //Sends nodes to the helper method and returns result
            return ancestorHelper(root, node1, node2);
        }


        BinarySearchTree.BSTNode ancestorHelper(BinarySearchTree.BSTNode root, BinarySearchTree.BSTNode node1, BinarySearchTree.BSTNode node2)
        {
            //Check to see if valid input
            if (root == null || node1 == null || node2 == null)
                return root;

            //Find Which side node1 is on
            bool isNode1Left = covers(root.left, node1);
            //Find which side node2 is on
            bool isNode2Left = covers(root.left, node2);

            //if nodes are on different sides, this root is the point they diverge - found the common ancestor
            //This does not gaurentee that they exist on the right, either way, returning the root would be the correct return
            if (isNode1Left != isNode2Left)
                return root;

            //if both nodes are on the same side, resent that sides child back into this method and return the results
            BinarySearchTree.BSTNode child = isNode1Left ? child = root.left : child = root.right;
            return ancestorHelper(child, node1, node2);
        }

        bool covers(BinarySearchTree.BSTNode root, BinarySearchTree.BSTNode node)
        {
            //check to see if root is valid
            if (root == null)
                return false;

            //check to see if root matches the search node
            if (root == node)
                return true;

            //use this method as a recursive function to search for node. Return true if found
            //if both cover calls return false, it doesnt exist, if one returns true, it'll return true
            return covers(root.left, node) || covers(root.right, node);
        }

        void FindNode(BinarySearchTree.BSTNode root, BinarySearchTree.BSTNode node, Dictionary<BinarySearchTree.BSTNode, BinarySearchTree.BSTNode> dict)
        {
            if (root.left != null)
            {
                dict.Add(root.left, root);
                if (root.left == node)
                    return;
                FindNode(root.left, node, dict);
            }

            if (root.right != null)
            {
                dict.Add(root.right, root);
                if (root.right == node)
                    return;
                FindNode(root.right, node, dict);
            }
        }

        #endregion

        #region 4.10-CheckSubtree
        public bool CheckSubtree(BinarySearchTree.BSTNode n1, BinarySearchTree.BSTNode n2)
        {
            if (n1 == null || n2 == null)
            {
                return false;
            }

            BinarySearchTree.BSTNode n = checkTreeHelper(n1, n2);
            if (n != n2) return false;
            return IsSubtree(n, n2);
        }

        BinarySearchTree.BSTNode checkTreeHelper(BinarySearchTree.BSTNode root, BinarySearchTree.BSTNode n)
        {
            if (root == n) return root;

            if (root.left != null)
            {
                root = checkTreeHelper(root.left, n);
                if (root == n) return root;
            }
            if (root.right != null)
            {
                root = checkTreeHelper(root.right, n);
                if (root == n) return root;
            }
            return root;
        }

        bool IsSubtree(BinarySearchTree.BSTNode n1, BinarySearchTree.BSTNode n2)
        {
            if (n1 != n2)
                return false;

            bool left = true;
            bool right = true;

            if( n1.left!=null && n2.left != null)
            {
                left = IsSubtree(n1.left, n2.left);
            }
            if (n1.right != null && n2.right != null)
            {
                right = IsSubtree(n1.right, n2.right);
            }

            return left && right;
        }
        #endregion
        #endregion

        //Section 15 - Threads and DeadLocks
        #region Section 15 ----- 2/7 Complete -----
        //15.1-ThreadvProcess
        // What is the difference between a thread and a process?

        // A thread consist of a single thread of execution, a process is a larger sequence of events that
        // may contain multiple threads of execution
        // A process does not share resources with another process unless inter-process communication is used (pipes/ files/ sockets ect)
        // Multiple threads within the same process will share the same process's resources.
        // Any change a thread makes will be immediately seen by other threads in that process.
        // Think of a process as a particular instance of a programs execution

        //15.2-ContextSwitch
        // How would you measure the time spend in a context switch

        // A context switch is when a process being executed is halted so another process can begin.
        // The cpu must put state information of the current process into memory and load the resources needed for the new processes
        // To measure the impact of the context switch, place a timestamp on the last step of the first process, and another timestamp
        // at the first step of the new processes. This will give you the time it took to complete the context switch.
        #endregion

        //Bonus
        #region BonusQuestions
        #region TeamScores - StringManip
        public string[] TeamScores(string[] matchScores)
        {
            //match scores are inputs that'll come in the form of
            //TeamName1 Score : TeamName2 Score

            //iterate through the matchScore array
            //Find out who won, and add teams as nessessary
            Dictionary<string, int> teamdict = new Dictionary<string, int>();

            foreach (string s in matchScores)
            {
                string[] words = s.Split(' ');
                string team1 = words[0];
                string team1score = words[1];
                string team2 = words[3];
                string team2score = words[4];            

                if (!teamdict.ContainsKey(team1))
                    teamdict.Add(team1, 0);
                if (!teamdict.ContainsKey(team2))
                    teamdict.Add(team2, 0);

                if (Convert.ToInt32(team1score) > Convert.ToInt32(team2score))
                {
                    teamdict[team1]++;
                }
                else if (Convert.ToInt32(team1score) < Convert.ToInt32(team2score))
                {
                    teamdict[team2]++;
                }
                else
                {
                    //tie
                }
            }

            string[] finalwinscores = new string[teamdict.Count];
            int count = 0;
            foreach (KeyValuePair<string, int> entry in teamdict)
            {
                string s = entry.Key + " " + entry.Value.ToString();
                finalwinscores[count] = s;
                count++;
            }
            return finalwinscores;
        }
        #endregion
        #endregion

    }

    //Note: Most are insufficient tests - rarely are edge cases tested
    class TestCases
    {
        CrackingTheInterviewQuestions CIQ = new CrackingTheInterviewQuestions();

        #region Section 1 ----- 9/9 Complete -----
        //1.1
        public void TestIsUnique()
        {
            Console.WriteLine("Testing 1.1 IsUnique");
            Console.WriteLine("Test 1 (true): " + CIQ.IsUnique("Jacob"));
            Console.WriteLine("Test 2 (false): " + CIQ.IsUnique("Jacobo"));
            Console.WriteLine("");
        }
        //1.2
        public void TestCheckPermutation()
        {
            Console.WriteLine("Testing 1.2 CheckPermutation");
            Console.WriteLine("Test 1 (true): " + CIQ.CheckPermutation("Abbacae", "bbAcaea"));
            Console.WriteLine("Test 2 (false): " + CIQ.CheckPermutation("Jacob", "bocaj"));
            Console.WriteLine("");
        }
        //1.3
        public void TestURLify()
        {
            Console.WriteLine("Testing 1.3 URLify");
            char[] arr = ("Yogit is The King      ").ToCharArray();
            Console.WriteLine("Pre URL String: " + new string(arr));
            Console.WriteLine("Post URL String: " + new string(CIQ.Urlify(arr, 17)));
            Console.WriteLine("");
        }
        //1.4
        public void TestPalindromePermutation()
        {
            Console.WriteLine("Testing 1.4 PalindromePermutation");
            Console.WriteLine("Test 1 (true): " + CIQ.PalindromePermutation("racecar"));
            Console.WriteLine("Test 2 (false): " + CIQ.PalindromePermutation("donkey"));
            Console.WriteLine("Test 3 (true): " + CIQ.PalindromePermutation("terry yrret"));
            Console.WriteLine("Test 4 (true): " + CIQ.PalindromePermutation("r ace ca r"));
            Console.WriteLine("Test 5 (true): " + CIQ.PalindromePermutationBook("racecar"));
            Console.WriteLine("Test 6 (false): " + CIQ.PalindromePermutationBook("donkey"));
            Console.WriteLine("Test 7 (true): " + CIQ.PalindromePermutationBook("terry yrret"));
            Console.WriteLine("Test 8 (true): " + CIQ.PalindromePermutationBook("r ace ca r"));
            Console.WriteLine("");
        }
        //1.5
        public void TestOneAway()
        {
            Console.WriteLine("Testing 1.5 OneAway");

            Console.WriteLine("Algorithm is incomplete");

            Console.WriteLine("");
        }
        //1.6
        public void TestStringCompression()
        {
            Console.WriteLine("Testing 1.6 StringCompression");
            Console.WriteLine("Before: aaabbbbrrraaKKKaaaa - After: " + CIQ.StringCompression("aaabbbbrrraaKKKaaaa"));
            Console.WriteLine("Before: aabcccccaaa - After: " + CIQ.StringCompression("aabcccccaaa"));
            Console.WriteLine("Before: a - After: " + CIQ.StringCompression("a"));
            Console.WriteLine("Before: aa - After: " + CIQ.StringCompression("aa"));
            Console.WriteLine("Before: aaa - After: " + CIQ.StringCompression("aaa"));
            Console.WriteLine("Before: aaaa - After: " + CIQ.StringCompression("aaaa"));
            Console.WriteLine("");
        }
        //1.7
        public void TestRotateMatrix()
        {
            Console.WriteLine("Testing 1.7 Rotate Matrix");
            int[,] matrix =
            {
                { 1, 2 , 3 },
                { 4, 5, 6 },
                { 7, 8, 9 },
                { 10, 11, 12 }
            };
            Console.WriteLine("Before:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for(int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + ", ");
                }
                Console.WriteLine("");
            }

            matrix = CIQ.RotateMatrix(matrix);
            Console.WriteLine("After:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + ", ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
        //1.8
        public void TestZeroMatrix()
        {
            Console.WriteLine("Testing 1.8 Zero Matrix");
            int[,] matrix =
            {
                { 1, 2 , 3 },
                { 4, 5, 6 },
                { 7, 0, 9 },
                { 10, 11, 12 }
            };
            Console.WriteLine("Before:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + ", ");
                }
                Console.WriteLine("");
            }

            CIQ.ZeroMatrix(matrix);
            Console.WriteLine("After:");
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + ", ");
                }
                Console.WriteLine("");
            }
            Console.WriteLine("");
        }
        //1.9
        public void TestStringRotation()
        {
            string s1 = "waterbottle";
            string s2 = "terbottlewa";
            string s3 = "waterbottle";
            string s4 = "waterboy";

            Console.WriteLine("Testing String Rotation");
            Console.WriteLine("True: " + CIQ.StringRotation(s1, s2));
            Console.WriteLine("False: " + CIQ.StringRotation(s3, s4));
            Console.WriteLine("");
        }
        #endregion

        #region Section 2 ----- 0/8 Complete -----
        #endregion

        #region Section 3 ----- 0/6 Complete -----
        #endregion

        #region Section 4 ----- 1/12 Complete -----
        //4.7
        public void TestBuildOrder()
        {
            Console.WriteLine("Testing 4.7-BuildOrder");

            string[] projects = new string[7] { "a", "b", "c", "d", "e", "f", "g" };

            string[,] dependancies = new string[8, 8];
            dependancies[0, 0] = "f";
            dependancies[0, 1] = "a";
            dependancies[1, 0] = "c";
            dependancies[1, 1] = "a";
            dependancies[2, 0] = "b";
            dependancies[2, 1] = "a";
            dependancies[3, 0] = "f";
            dependancies[3, 1] = "b";
            dependancies[4, 0] = "f";
            dependancies[4, 1] = "c";
            dependancies[5, 0] = "a";
            dependancies[5, 1] = "e";
            dependancies[6, 0] = "b";
            dependancies[6, 1] = "e";
            dependancies[7, 0] = "d";
            dependancies[7, 1] = "g";

            Project[] buildOrder = CIQ.BuildOrder(projects, dependancies);
            Console.WriteLine("Valid Build Order");
            foreach (Project p in buildOrder)
            {
                if (p != null)
                Console.Write(p.getName() + ", ");
            }
            Console.WriteLine(" ");
            Console.WriteLine(" ");
        }

        //4.10
        public void TestCheckSubtree()
        {
            BinarySearchTree bst1 = new BinarySearchTree();
            bst1.Insert(2);
            bst1.Insert(1);
            bst1.Insert(3);

            BinarySearchTree bst2 = new BinarySearchTree();
            bst2.Insert(2);
            bst2.Insert(1);
            bst2.Insert(3);
            bst2.Insert(4);

            Console.WriteLine("Testing CheckSubtree - This Doesnt Test properly - Nodes need to be same, not same value");
            Console.WriteLine("False: " + CIQ.CheckSubtree(bst1.head, bst2.head));

            bst1.Insert(4);
            Console.WriteLine("True: " + CIQ.CheckSubtree(bst1.head, bst2.head));
            Console.WriteLine(" ");
        }
        #endregion

        #region
        public void TestTeamScores()
        {
            Console.WriteLine("Testing Team Scores");

            string score1 = "Seahawks 14 : Broncoes 7";
            string score2 = "Jaguars 12 : Broncoes 7";
            string score3 = "Seahawks 14 : NinnyPickets 60";
            string score4 = "NinnyPickets 14 : Jaguars 7";
            string[] scorearr = new string[4];
            scorearr[0] = score1;
            scorearr[1] = score2;
            scorearr[2] = score3;
            scorearr[3] = score4;
            string[] scores = CIQ.TeamScores(scorearr);
            foreach(string s in scores)
            {
                Console.WriteLine(s);
            }

            Console.WriteLine(" ");
        }
        #endregion
    }

    class Program
    {
        static void Main(string[] args)
        {
            TestCases TC = new TestCases();

            #region Section 1 ----- 9/9 Complete -----
            //1.1
            TC.TestIsUnique();
            //1.2
            TC.TestCheckPermutation();
            //1.3
            TC.TestURLify();
            //1.4
            TC.TestPalindromePermutation();
            //1.5
            TC.TestOneAway();
            //1.6
            TC.TestStringCompression();
            //1.7
            TC.TestRotateMatrix();
            //1.8
            TC.TestZeroMatrix();
            //1.9
            TC.TestStringRotation();
            #endregion ----- 6/9 complete -----

            #region Section 2 ----- 0/8 Complete -----
            #endregion

            #region Section 3 ----- 0/6 Complete -----
            #endregion

            #region Section 4 ----- 1/12 Complete -----
            //4.7
            TC.TestBuildOrder();
            //4.10
            TC.TestCheckSubtree();
            #endregion

            #region bonus
            TC.TestTeamScores();
            #endregion
        }
    }
}
