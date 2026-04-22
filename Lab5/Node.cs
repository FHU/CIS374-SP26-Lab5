namespace Lab5;

public enum Color
{
    White, Gray, Black
}

public class Node : IComparable<Node>
{
    public string Name { get; set; }
    public List<Node> Neighbors {get;set;}
    public Color Color { get; set; }

    public Node(string name = "", Color color=Color.White)
    {
        Name = name;
        Color = color;
        Neighbors = new List<Node>();
    }

    public override bool Equals(object? obj)
    {
        return obj is Node other && other.Name == Name;
    }

    public override string ToString()
    {
        return Name;
    }

    public int CompareTo(Node? other)
    {
        return this.Name.CompareTo(other.Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
}