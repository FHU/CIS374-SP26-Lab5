using Lab5;

namespace UnitTests;

[TestClass]
public class UnitTests
{
    [TestMethod]
    public void Graph1IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("d", "e"));
        Assert.IsTrue(undirectedGraph.IsReachable("d", "c"));
    }

    [TestMethod]
    public void Graph1ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");

        Assert.AreEqual(1, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void Graph1DFS()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");

        // Compute the actual node->pred dictionary. 
        Dictionary<Node, Node> actualResults = undirectedGraph.DFS( undirectedGraph.GetNodeByName("a") );

        // Create the correct node->pred dictionary. 
        Dictionary<Node, Node> expectedResults = new Dictionary<Node, Node>();
        expectedResults.Add(new Node("a"), null);
        expectedResults.Add(new Node("b"), new Node("a"));
        expectedResults.Add(new Node("c"), new Node("b"));
        expectedResults.Add(new Node("d"), new Node("b"));
        expectedResults.Add(new Node("e"), new Node("b"));

        // check that the number of items in the actual results match the expected results
        Assert.AreEqual(expectedResults.Count, actualResults.Count);
        
        // check every key-value pair and confirm the actual results match the expected results
        foreach( var kvp in expectedResults) 
        {
            Assert.IsTrue( actualResults.ContainsKey(kvp.Key) );
            Assert.AreEqual( expectedResults[kvp.Key] , actualResults[kvp.Key]);
        }
    }


    [TestMethod]
    public void Graph2IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph2.txt");

        Assert.IsFalse(undirectedGraph.IsReachable("a", "c"));
        Assert.IsFalse(undirectedGraph.IsReachable("e", "c"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "e"));
        Assert.IsFalse(undirectedGraph.IsReachable("b", "a"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "b"));

    }

    [TestMethod]
    public void Graph2ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph2.txt");

        Assert.AreEqual(5, undirectedGraph.ConnectedComponents);
    }


    [TestMethod]
    public void Graph3IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph3.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "d"));
        Assert.IsTrue(undirectedGraph.IsReachable("h", "g"));

        Assert.IsFalse(undirectedGraph.IsReachable("a", "h"));
        Assert.IsFalse(undirectedGraph.IsReachable("c", "i"));
        Assert.IsFalse(undirectedGraph.IsReachable("g", "b"));

    }

    [TestMethod]
    public void Graph3ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph3.txt");

        Assert.AreEqual(3, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void Graph4IsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph4.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "i"));
        Assert.IsTrue(undirectedGraph.IsReachable("g", "b"));
        Assert.IsTrue(undirectedGraph.IsReachable("c", "f"));
        Assert.IsTrue(undirectedGraph.IsReachable("a", "d"));
        Assert.IsTrue(undirectedGraph.IsReachable("b", "i"));

    }

    [TestMethod]
    public void Graph4ConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/graph4.txt");

        Assert.AreEqual(1, undirectedGraph.ConnectedComponents);
    }

    [TestMethod]
    public void SavannahIsReachable()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/Savannah.txt");

        Assert.IsTrue(undirectedGraph.IsReachable("a", "c"));
        Assert.IsTrue(undirectedGraph.IsReachable("e", "i"));
        Assert.IsTrue(undirectedGraph.IsReachable("g", "b"));
        Assert.IsTrue(undirectedGraph.IsReachable("c", "f"));
        Assert.IsTrue(undirectedGraph.IsReachable("a", "j"));
        Assert.IsTrue(undirectedGraph.IsReachable("b", "i"));


        Assert.IsFalse(undirectedGraph.IsReachable("a", "d"));
        Assert.IsFalse(undirectedGraph.IsReachable("d", "j"));

    }

    [TestMethod]
    public void SavannahConnectedComponents()
    {
        UndirectedUnweightedGraph undirectedGraph = new UndirectedUnweightedGraph("../../../graphs/Savannah.txt");

        Assert.AreEqual(2, undirectedGraph.ConnectedComponents);
    }

    // ── BFS ──────────────────────────────────────────────────────────────────

    [TestMethod]
    public void Graph1BFS()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");

        Dictionary<Node, (Node pred, int dist)> actual = g.BFS(g.GetNodeByName("a"));

        var expected = new Dictionary<Node, (Node pred, int dist)>
        {
            { new Node("a"), (null,        0) },
            { new Node("b"), (new Node("a"), 1) },
            { new Node("c"), (new Node("b"), 2) },
            { new Node("d"), (new Node("b"), 2) },
            { new Node("e"), (new Node("b"), 2) },
        };

        Assert.AreEqual(expected.Count, actual.Count);
        foreach (var kvp in expected)
        {
            Assert.IsTrue(actual.ContainsKey(kvp.Key));
            Assert.AreEqual(kvp.Value.pred, actual[kvp.Key].pred);
            Assert.AreEqual(kvp.Value.dist, actual[kvp.Key].dist);
        }
    }

    [TestMethod]
    public void Graph4BFS()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/graph4.txt");

        Dictionary<Node, (Node pred, int dist)> actual = g.BFS(g.GetNodeByName("a"));

        var expected = new Dictionary<Node, (Node pred, int dist)>
        {
            { new Node("a"), (null,           0) },
            { new Node("b"), (new Node("a"),  1) },
            { new Node("c"), (new Node("b"),  2) },
            { new Node("d"), (new Node("c"),  3) },
            { new Node("e"), (new Node("d"),  4) },
            { new Node("f"), (new Node("e"),  5) },
            { new Node("g"), (new Node("f"),  6) },
            { new Node("h"), (new Node("g"),  7) },
            { new Node("i"), (new Node("h"),  8) },
        };

        Assert.AreEqual(expected.Count, actual.Count);
        foreach (var kvp in expected)
        {
            Assert.IsTrue(actual.ContainsKey(kvp.Key));
            Assert.AreEqual(kvp.Value.pred, actual[kvp.Key].pred);
            Assert.AreEqual(kvp.Value.dist, actual[kvp.Key].dist);
        }
    }

    // ── DFS ──────────────────────────────────────────────────────────────────

    [TestMethod]
    public void Graph2DFS()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/graph2.txt");

        Dictionary<Node, Node> actual = g.DFS(g.GetNodeByName("a"));

        var expected = new Dictionary<Node, Node>
        {
            { new Node("a"), null },
            { new Node("b"), null },
            { new Node("c"), null },
            { new Node("d"), null },
            { new Node("e"), null },
        };

        Assert.AreEqual(expected.Count, actual.Count);
        foreach (var kvp in expected)
        {
            Assert.IsTrue(actual.ContainsKey(kvp.Key));
            Assert.AreEqual(kvp.Value, actual[kvp.Key]);
        }
    }

    [TestMethod]
    public void Graph4DFS()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/graph4.txt");

        Dictionary<Node, Node> actual = g.DFS(g.GetNodeByName("a"));

        var expected = new Dictionary<Node, Node>
        {
            { new Node("a"), null           },
            { new Node("b"), new Node("a")  },
            { new Node("c"), new Node("b")  },
            { new Node("d"), new Node("c")  },
            { new Node("e"), new Node("d")  },
            { new Node("f"), new Node("e")  },
            { new Node("g"), new Node("f")  },
            { new Node("h"), new Node("g")  },
            { new Node("i"), new Node("h")  },
        };

        Assert.AreEqual(expected.Count, actual.Count);
        foreach (var kvp in expected)
        {
            Assert.IsTrue(actual.ContainsKey(kvp.Key));
            Assert.AreEqual(kvp.Value, actual[kvp.Key]);
        }
    }

    // ── Social graphs ─────────────────────────────────────────────────────────

    [TestMethod]
    public void AllisonIsReachable()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/Allison.txt");

        Assert.IsTrue(g.IsReachable("plants", "mountainlion"));
        Assert.IsTrue(g.IsReachable("insects", "coyote"));
    }

    [TestMethod]
    public void AllisonConnectedComponents()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/Allison.txt");

        Assert.AreEqual(1, g.ConnectedComponents);
    }

    [TestMethod]
    public void BeRealIsReachable()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/BeReal.txt");

        Assert.IsTrue(g.IsReachable("gb", "mw"));
        Assert.IsTrue(g.IsReachable("kc", "ew"));
    }

    [TestMethod]
    public void BeRealConnectedComponents()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/BeReal.txt");

        Assert.AreEqual(1, g.ConnectedComponents);
    }

    [TestMethod]
    public void KayleeIsReachable()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/Kaylee.txt");

        Assert.IsTrue(g.IsReachable("rs", "jr"));
        Assert.IsFalse(g.IsReachable("ab", "ko"));
    }

    [TestMethod]
    public void KayleeConnectedComponents()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/Kaylee.txt");

        Assert.AreEqual(2, g.ConnectedComponents);
    }

    // ── Exception handling ────────────────────────────────────────────────────

    [TestMethod]
    public void IsReachableThrowsForInvalidNode()
    {
        UndirectedUnweightedGraph g = new UndirectedUnweightedGraph("../../../graphs/graph1.txt");
        try
        {
            g.IsReachable("a", "zzz");
            Assert.Fail("Expected an exception for an invalid node name.");
        }
        catch (Exception)
        {
            // expected
        }
    }
}

