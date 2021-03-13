using System.Collections.Generic;

namespace Lazcat.Blog.Models.Web
{
    public class Tree<T>
    {
        private readonly LinkedList<Tree<T>> _childrenTree;
        private T _data;

        public Tree(T data)
        {
            _data = data;
            _childrenTree = new LinkedList<Tree<T>>();
        }

        public void AddChild(T data)
        {
            _childrenTree.AddLast(new Tree<T>(data));
        }
    }
}