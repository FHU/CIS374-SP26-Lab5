# Lab 5 — Undirected Graph Traversal

CIS 374 · Freed-Hardeman University

Implements an undirected, unweighted graph with DFS, BFS, reachability checking, and connected-component counting.

## Project structure

```
Lab5/
├── Lab5/                          # Class library + console app
│   ├── Node.cs                    # Graph vertex (name, color, neighbors)
│   ├── UndirectedUnweightedGraph.cs
│   ├── Program.cs
│   └── graphs/                    # Input graph files
└── UnitTests/                     # MSTest project
    ├── UnitTest.cs
    └── graphs/                    # Mirror of Lab5/graphs/
```

## Building and running

```bash
# Build
dotnet build Lab5.sln

# Run all tests
dotnet test UnitTests/UnitTests.csproj

# Run a single test
dotnet test UnitTests/UnitTests.csproj --filter "FullyQualifiedName~Graph1DFS"

# Run the console app
dotnet run --project Lab5/Lab5.csproj
```

## Graph file format

Graph files live in `Lab5/graphs/` (mirrored in `UnitTests/graphs/`).

```
# comment lines start with #
a  b  c  d  e      ← space-separated node names (first non-comment line)
a b                ← one edge per line (two node names)
b c
```

## API overview

### `Node`

| Member | Description |
|--------|-------------|
| `Name` | String identifier |
| `Color` | `White` / `Gray` / `Black` (traversal state) |
| `Neighbors` | `List<Node>` adjacency list |

Implements `IComparable<Node>` by name so neighbors are visited alphabetically.

### `UndirectedUnweightedGraph`

| Method / Property | Returns | Description |
|-------------------|---------|-------------|
| `DFS(startingNode)` | `Dictionary<Node, Node>` | Recursive DFS; maps each node to its predecessor. Neighbors visited alphabetically. |
| `BFS(startingNode)` | `Dictionary<Node, (Node pred, int dist)>` | Iterative BFS; maps each node to its predecessor and distance from source. |
| `IsReachable(from, to)` | `bool` | Returns `true` if the two named nodes are in the same component. Throws if either name is invalid. |
| `ConnectedComponents` | `int` | Number of connected components (iterates DFS over all unvisited nodes). |
| `GetNodeByName(name)` | `Node` | Looks up a node by name. |

### Traversal color convention

| Color | Meaning |
|-------|---------|
| White | Unvisited |
| Gray | Discovered / in progress |
| Black | Finished |

`DFS` resets all colors before traversal by default (`reset = true`). Pass `reset: false` to continue an existing traversal (used internally by `ConnectedComponents`).
