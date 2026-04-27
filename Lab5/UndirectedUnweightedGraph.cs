using System.Text.RegularExpressions;

namespace Lab5;

public class UndirectedUnweightedGraph
{
    public List<Node> Nodes {get;set;}

    public UndirectedUnweightedGraph()
    {
        Nodes = new();
    }

    public UndirectedUnweightedGraph(string path)
    {
        Nodes = new List<Node>();

        List<string> lines = new List<string>();

        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (line == "")
                    {
                        continue;
                    }
                    if (line[0] == '#')
                    {
                        continue;
                    }

                    lines.Add(line);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        // process the lines
        if (lines.Count < 1)
        {
            // empty file
            Console.WriteLine("Graph file was empty");
            return;
        }

        // Add nodes
        string[] nodeNames = Regex.Split(lines[0], @"\W+");

        foreach (var name in nodeNames)
        {
            Nodes.Add(new Node(name));
        }

        // Add edges
        for (int i = 1; i < lines.Count; i++)
        {
            // extract node names
            nodeNames = Regex.Split(lines[i], @"\W+");
            if (nodeNames.Length < 2)
            {
                throw new Exception("Two nodes are required for each edge.");
            }

            // add edge between those nodes
            AddEdge(nodeNames[0], nodeNames[1]);
        }
    }

    public void AddEdge(string node1Name, string node2Name)
    {
        Node? node1 = GetNodeByName(node1Name);
        Node? node2 = GetNodeByName(node2Name);

        if (node1 == null || node2 == null)
        {
            throw new Exception("Invalid node name");
        }

        node1.Neighbors.Add(node2);
        node2.Neighbors.Add(node1);
    }

    public Node? GetNodeByName(string nodeName)
    {
        var node = Nodes.Find(node => node.Name == nodeName.Trim());

        return node;
    }

    public Dictionary<Node,Node?> DFS(Node startingNode, bool reset=true)
    {
        Dictionary<Node,Node?> pred = new();

        foreach(Node node in Nodes)
        {
            pred[node] = null;
            if(reset)
            {
                node.Color = Color.White;
            }
        }

        // call the recursive method
        DFSVisit(startingNode, pred);

        return pred;
    }

    private void DFSVisit(Node node, Dictionary<Node, Node?> pred)
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


    // TODO
    public Dictionary<Node,(Node, int)> BFS(Node startingNode, bool reset=true)
    {
        Dictionary<Node, (Node?,int)> resultsDictionary = new();

        // init the dictionary
        foreach(var node in Nodes)
        {
            if(reset)
            {
                node.Color=Color.White;
            }
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

    public bool IsReachable(string node1name, string node2name)
    {
        // convert node names into node objects
        Node? node1 = GetNodeByName(node1name);
        Node? node2 = GetNodeByName(node2name);

        if(node1==null || node2==null)
        {
            throw new Exception($"{node1name} or {node2name} does not exist.");
        }

        // the same node is reachable from itself
        if( node1 == node2 )
        {
            return true;
        }

        // do a DFS
        var pred = DFS(node1);
        
        // was a predecessor for node2 found? 
        return pred[node2] != null;
    }

    public int ConnectedComponents
    {
        get
        {
            int numConnectedComponents = 0;

            //reset all nodes to White
            foreach(var node in Nodes)
            {
                node.Color = Color.White;
            }

            // iterate over the nodes
            foreach(var node in Nodes)
            {
               // if the node is white, 
               if( node.Color == Color.White)
                {
                    // run a dfs() from that node
                    DFS(node, false);
                    // increment count
                     numConnectedComponents++;
                } 
            }

            return numConnectedComponents;
        }
    }

}