using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Day17CS
{
    public class VaultNode
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Queue<VaultNode> Children { get; set; }

        public VaultNode Parent { get; set; }

        public int Depth => this.Parent?.Depth+1 ?? 0;

        public string LastDirection { get; set; }

        public string DirectionString => this.Parent != null ? string.Concat(this.Parent.DirectionString, this.LastDirection) : string.Empty;

        public VaultNode()
        {
            this.Children = new Queue<VaultNode>();
        }

        public VaultNode(VaultNode parent)
        {
            this.Parent = parent;
            this.Children = new Queue<VaultNode>();
        }

        public VaultNode AddChild(VaultNode child)
        {
            this.Children.Enqueue(child);
            child.Parent = this;
            return child;
        }
    }
}
