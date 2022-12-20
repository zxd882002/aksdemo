namespace WeatherForecastAPI.Infrastructure.AlphaBetaTree
{
    public static class TreeNodeCreator<T>
        where T : IValueableTreeNodeElement<T>
    {
        public static AbstractTreeNode<T> CreateMaxRootNode(T element, int deep)
        {
            MaxTreeNode<T> node = new MaxTreeNode<T>();
            node.Element = element;
            node.ParentNode = null;
            node.Deep = deep;
            return node;
        }

        public static AbstractTreeNode<T> CreateMinRootNode(T element, int deep)
        {
            MinTreeNode<T> node = new MinTreeNode<T>();
            node.Element = element;
            node.ParentNode = null;
            node.Deep = deep;
            return node;
        }
    }
}
