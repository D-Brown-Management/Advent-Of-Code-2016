using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13CS
{
    public class CubeNode
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CubeNode Parent { get; set; }
        public Queue<CubeNode> Children { get; }

        public int Depth => (this.Parent?.Depth+1) ?? 0;

        public CubeNode()
        {
            this.Children = new Queue<CubeNode>();
        }

        public CubeNode(CubeNode parent)
        {
            this.Parent = parent;            
            this.Children = new Queue<CubeNode>();
        }

        public CubeNode AddChild(CubeNode child)
        {
            this.Children.Enqueue(child);
            child.Parent = this;
            return child;
        }
    }
}
