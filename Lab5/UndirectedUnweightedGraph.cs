namespace Lab5;

public class UndirectedUnweightedGraph
{
    public List<Node> Nodes {get;set;}

    public UndirectedUnweightedGraph()
    {
        Nodes = new();
    }

    public Dictionary<Node,Node> DFS(Node startingNode, bool reset=true)
    {
        Dictionary<Node,Node?> pred = new();

        if(reset)
        {
            foreach(Node node in Nodes)
            {
                pred[node] = null;
                node.Color = Color.White;
            }
        }

        // call the recursive method
        DFSVisit(startingNode, pred);

        return pred;
    }

    private void DFSVisit(Node node, Dictionary<Node, Node> pred)
    {
        node.Color = Color.Gray;

        // sort the neighbors so we visit them in alphabetical order
        node.Neighbors.Sort();

        foreach(Node neighbor in node.Neighbors)
        {
            if(neighbor.Color == Color.White)
            {
                pred[neighbor] = node;
                DFSVisit(neighbor, pred);
            }
        }

        node.Color = Color.Black;
    }


    public Dictionary<Node,(Node, int)> BFS(Node startingNode, bool reset=true)
    {
        Dictionary<Node, (Node?,int)> resultsDictionary = new();

        if(reset) {
            // init the dictionary
            foreach(var node in Nodes)
            {
                node.Color=Color.White;
                resultsDictionary[node] = (null, int.MaxValue);
            }

            // setup at starting node
            startingNode.Color = Color.Gray;
            resultsDictionary[startingNode] = (null, 0);

            // create a queue
            Queue<Node> queue = new();
            queue.Enqueue(startingNode);

            // iteratively process the graph (i.e., adding and visiting nodes in the queue)
        }
    }

}