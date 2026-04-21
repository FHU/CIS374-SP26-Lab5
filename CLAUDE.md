# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Commands

```bash
# Build the solution
dotnet build Lab5.sln

# Run all tests
dotnet test UnitTests/UnitTests.csproj

# Run a single test by name
dotnet test UnitTests/UnitTests.csproj --filter "FullyQualifiedName~Graph1DFS"

# Run the console app
dotnet run --project Lab5/Lab5.csproj
```

## Architecture

This is a .NET 9 solution with two projects:

- **Lab5/** — class library + console app implementing an undirected unweighted graph
- **UnitTests/** — MSTest project that references Lab5 and tests the graph algorithms

### Core classes

**`Node`** ([Lab5/Node.cs](Lab5/Node.cs)) — Graph vertex with a `Name`, a `Color` (White/Gray/Black for traversal state), and a `List<Node> Neighbors`. Implements `IComparable<Node>` by name so neighbors can be sorted alphabetically during traversal.

**`UndirectedUnweightedGraph`** ([Lab5/UndirectedUnweightedGraph.cs](Lab5/UndirectedUnweightedGraph.cs)) — Adjacency-list graph. Loads from a text file: first non-comment line lists node names, subsequent lines define edges (one pair per line). Implements:
- `DFS(Node startingNode)` — recursive DFS returning a `Dictionary<Node, Node>` (node → predecessor). Neighbors visited in alphabetical order.
- `BFS(Node startingNode)` — iterative BFS returning a `Dictionary<Node, (Node pred, int dist)>`.
- `IsReachable(string, string)` — uses DFS to check connectivity.
- `ConnectedComponents` (property) — **not yet implemented**; should iterate DFS over all unvisited nodes.

### Graph file format

```
# comment lines start with #
a  b  c  d  e      ← space-separated node names (first non-comment line)
a b                ← edges, one per line (two node names, any non-word delimiter)
b c
```

Graph files live in `Lab5/graphs/` and are mirrored in `UnitTests/graphs/`. Unit tests load files via relative path `../../../graphs/<file>.txt` from the test output directory.

### Traversal color convention

Nodes use the standard BFS/DFS three-color scheme: **White** = unvisited, **Gray** = discovered/in-progress, **Black** = finished. The `DFS` method resets all colors before traversal by default (`reset = true`); pass `false` to continue an existing traversal (used by `ConnectedComponents`).
