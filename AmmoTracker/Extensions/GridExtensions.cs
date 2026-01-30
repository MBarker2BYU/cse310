using System.Reflection;

namespace AmmoTracker.Extensions;

public static class GridExtensions
{
    public static void AddColumnFormat(this DataGridView grid, string propertyName, bool visible = true)
        => grid.AddColumnFormat(propertyName, "", "", visible);
    
    public static void AddColumnFormat(this DataGridView grid, string propertyName, string headerName = "", string format = "", bool visible = true)
    {
        var column = new DataGridViewTextBoxColumn();

        column.Name = propertyName;
        column.DataPropertyName = propertyName;

        if(!string.IsNullOrEmpty(headerName))
            column.HeaderText = headerName;

        if(!string.IsNullOrEmpty(format))
            column.DefaultCellStyle = new DataGridViewCellStyle { Format = format };

        column.Visible = visible;

        grid.Columns.Add(column);
    }

    /// <summary>
    /// Fixes the header resize draw issue.
    /// </summary>
    /// <param name="grid">The grid.</param>
    public static void FixHeaderResizeDrawIssue(this DataGridView grid)
    {
        typeof(DataGridView)
            .GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic)!
            .SetValue(grid, true, null);

        grid.EnableHeadersVisualStyles = false;

        var headerStyle = grid.ColumnHeadersDefaultCellStyle;
        headerStyle.BackColor = SystemColors.Control;
        headerStyle.ForeColor = SystemColors.ControlText;
        headerStyle.SelectionBackColor = SystemColors.Control;
        headerStyle.SelectionForeColor = SystemColors.ControlText;
        headerStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

        grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
    }

}