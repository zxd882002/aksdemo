using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecastAPI.Infrastructure.AlphaBetaTree
{
    public static class TreeNodeCreator<T>
        where T : IValueableTreeNodeElement<T>
    {
        public static AbstractTreeNode<T> CreateMaxHeaderTreeNode(T element, int deep, AbstractTreeNode<T>? parentNode)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<T> childElements = element.GetChildElements();
            Debug.WriteLine($"CreateMaxHeaderTreeNode->GetChildElements, deep = {deep} ellapse = {sw.ElapsedMilliseconds}");
            

            if (!childElements.Any())
            {
                // if no child element, current node is leaf node
                AbstractTreeNode<T> leafNode = new ValueTreeNode<T>();
                leafNode.Element = element;
                leafNode.ParentNode = parentNode;
                return leafNode;
            }

            MaxTreeNode<T> node = new MaxTreeNode<T>();
            node.Element = element;
            node.ParentNode = parentNode;

            List<AbstractTreeNode<T>> childTreeNodes = new List<AbstractTreeNode<T>>();
            foreach (T childElement in childElements)
            {
                AbstractTreeNode<T> childTreeNode;
                if (deep == 0)
                {
                    // let's stop here, set the child node as leaf node
                    childTreeNode = new ValueTreeNode<T>();
                    childTreeNode.Element = childElement;
                    childTreeNode.ParentNode = node;
                }
                else
                {
                    childTreeNode = CreateMinHeaderTreeNode(childElement, deep - 1, node);
                }
                childTreeNodes.Add(childTreeNode);
            };
            node.ChildNodes = childTreeNodes;
            sw.Stop();
            Task.Run(() =>Debug.WriteLine($"CreateMaxHeaderTreeNode ended, deep = {deep} ellapse = {sw.ElapsedMilliseconds}"));
            return node;
        }

        public static AbstractTreeNode<T> CreateMinHeaderTreeNode(T element, int deep, AbstractTreeNode<T>? parentNode)
        {
            List<T> childElements = element.GetChildElements();
            if (!childElements.Any())
            {
                // if no child element, current node is leaf node
                AbstractTreeNode<T> leafNode = new ValueTreeNode<T>();
                leafNode.Element = element;
                leafNode.ParentNode = parentNode;
                return leafNode;
            }

            MinTreeNode<T> node = new MinTreeNode<T>();
            node.Element = element;
            node.ParentNode = parentNode;

            List<AbstractTreeNode<T>> childTreeNodes = new List<AbstractTreeNode<T>>();
            foreach (T childElement in childElements)
            {
                AbstractTreeNode<T> childTreeNode;
                if (deep == 0)
                {
                    // let's stop here, set the child node as leaf node
                    childTreeNode = new ValueTreeNode<T>();
                    childTreeNode.Element = childElement;
                    childTreeNode.ParentNode = node;
                }
                else
                {
                    childTreeNode = CreateMaxHeaderTreeNode(childElement, deep - 1, node);
                }
                childTreeNodes.Add(childTreeNode);
            };
            node.ChildNodes = childTreeNodes;
            return node;
        }
    }
}
