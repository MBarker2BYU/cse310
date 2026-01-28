namespace SwarNet.Structs;

public readonly struct GridCell(int row, int column) : IEquatable<GridCell>
{

    #region Properties
    
    public int Row { get; init; } = row;
    public int Column { get; init; } = column;

    #endregion

    #region IEquatable

    // Allow easy conversion to Point if needed (e.g. for drawing)
    public static implicit operator Point(GridCell cell)
        => new Point(cell.Column, cell.Row); 

    public static implicit operator GridCell(Point p)
        => new GridCell(p.Y, p.X);

    public override bool Equals(object? obj) 
        => obj is GridCell other && Equals(other);
    
    public bool Equals(GridCell other) 
        => Row == other.Row && Column == other.Column;
    
    public override int GetHashCode() 
        => HashCode.Combine(Row, Column);
    
    public override string ToString() 
        => $"R: {Row}, C: {Column}";

    #endregion
}