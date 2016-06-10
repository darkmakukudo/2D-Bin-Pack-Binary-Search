using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2D_Binary_Tree_Bin_Pack
{
    public class Packer
    {
        
        internal class Node { 
            public Node rightNode;
            public Node bottomNode;
            public double pos_x;
            public double pos_z;
            public double width;
            public double length;
            public bool isOccupied;
        }

        internal class Box {
            public double length;
            public double width;
            public double volume;
            public Node position;
        }

        private double containerWidth = 48;
        private double containerLength = 93;
        private List<Box> _boxes;
        private Node rootNode;

        public Packer()
        {
            _boxes = new List<Box>();
            Box box1 = new Box {length = 40, width = 24};
            Box box2 = new Box { length = 20, width = 24 };
            Box box3 = new Box { length = 20, width = 24 };
            Box box4 = new Box { length = 20, width = 24 };

            _boxes.Add(box1);
            _boxes.Add(box2);
            _boxes.Add(box3);
            _boxes.Add(box4);

            // Sort boxes into descending order based on volume
            _boxes.ForEach(x => x.volume = (x.length * x.width));
            _boxes = _boxes.OrderByDescending(x => x.volume).ToList();
            
            // Initialize root node
            rootNode = new Node { length = containerLength, width = containerWidth};

            Pack();
            Display();
            Console.ReadLine();
        }

        private void Display()
        {
            foreach (var box in _boxes)
            {
                var positionx = box.position != null ? box.position.pos_x.ToString() : String.Empty;
                var positionz = box.position != null ? box.position.pos_z.ToString() : String.Empty;
                Console.WriteLine("Length : " + box.length + " Width : " + box.width + " Pos_z  : " + positionz + " Pos_x : " + positionx);
            }
        }

        private void Pack()
        {
            foreach (var box in _boxes)
            { 
                var node = FindNode(rootNode, box.width, box.length);
                if (node != null)
                { 
                    // Split rectangles
                    box.position = SplitNode(node, box.width, box.length);
                }
            }
        }

        private Node FindNode(Node rootNode, double boxWidth, double boxLength)
        {
            if (rootNode.isOccupied) 
            {
                var nextNode = FindNode(rootNode.bottomNode, boxWidth, boxLength);

                if (nextNode == null)
                {
                    nextNode = FindNode(rootNode.rightNode, boxWidth, boxLength);
                }

                return nextNode;
            }
            else if (boxWidth <= rootNode.width && boxLength <= rootNode.length)
            {
                return rootNode;
            }
            else 
            {
                return null;
            }
        }

        private Node SplitNode(Node node, double boxWidth, double boxLength)
        {
            node.isOccupied = true;
            node.bottomNode = new Node { pos_z = node.pos_z, pos_x = node.pos_x + boxWidth, length = node.length, width = node.width - boxWidth };
            node.rightNode = new Node { pos_z = node.pos_z + boxLength, pos_x = node.pos_x, length = node.length - boxLength, width = boxWidth };
            return node;
        }
    }
}
