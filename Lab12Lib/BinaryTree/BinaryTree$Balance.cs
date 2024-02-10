using Lab12Lib.Exceptions;

namespace Lab12Lib.BinaryTree
{
    public partial class BinaryTree<T>
    {
        private static int Height(BinaryTreeNode<T>? node)
        {
            if (node == null) return 0;
            var left = Height(node.Left);
            var right = Height(node.Right);
            return (left > right ? left : right) + 1;
        }

        private static int BalanceFactor(BinaryTreeNode<T> node)
            => Height(node.Left) - Height(node.Right);


        private static BinaryTreeNode<T> RotateLL(BinaryTreeNode<T> node)
        {
            var pivot = node.Left ?? throw new RotateArgumentException();
            node.Left = pivot.Right;
            pivot.Right = node;
            return pivot;
        }

        private static BinaryTreeNode<T> RotateRR(BinaryTreeNode<T> node)
        {
            var pivot = node.Right ?? throw new RotateArgumentException();
            node.Right = pivot.Left;
            pivot.Left = node;
            return pivot;
        }

        private static BinaryTreeNode<T> RotateLR(BinaryTreeNode<T> node)
        {
            var pivot = node.Left ?? throw new RotateArgumentException();
            node.Left = RotateRR(pivot);
            return RotateLL(node);
        }

        private static BinaryTreeNode<T> RotateRL(BinaryTreeNode<T> node)
        {
            var pivot = node.Right ?? throw new RotateArgumentException();
            node.Right = RotateLL(pivot);
            return RotateRR(node);
        }

        private static BinaryTreeNode<T> Balance(BinaryTreeNode<T> node)
        {
            var bf = BalanceFactor(node);
            if (bf > 1 && node.Left is not null)
                node = BalanceFactor(node.Left) > 0
                    ? RotateLL(node)
                    : RotateLR(node);
            else if (bf < -1 && node.Right is not null)
                node = BalanceFactor(node.Right) > 0
                    ? RotateRL(node)
                    : RotateRR(node);
            return node;
        }
    }
}