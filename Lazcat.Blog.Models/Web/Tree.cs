using System.Collections.Generic;

namespace Lazcat.Blog.Models.Web
{
    public class Tree<T>
    {
        private T _data;
        private readonly LinkedList<Tree<T>> _childrenTree;

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