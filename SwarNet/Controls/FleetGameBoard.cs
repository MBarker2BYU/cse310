using SwarNet.Enums;
using SwarNet.EventArgs;
using SwarNet.Models;
using SwarNet.Structs;
using System.Data.Common;
using System.Drawing.Drawing2D;

namespace SwarNet.Controls;

public sealed  class FleetGameBoard : Panel
{

    #region Constants

    public const int GRID_SIZE = 10;

    private const float PEN_WIDTH = 1.5f;
    private const int ALPHA_VALUE_48 = 48;
    private const int ALPHA_VALUE_128 = 128;
    private const int ALPHA_VALUE_180 = 180;
    private const int ALPHA_VALUE_220 = 220;

    // Colors
    private static readonly Color m_BackgroundColor = Color.FromArgb(15, 15, 35);
    private static readonly Color m_GridLineColor = Color.FromArgb(80, 0, 255, 255);
    private static readonly Color m_TextColor = Color.Cyan;

    private static readonly Color m_HoverColor = Color.FromArgb(0, 255, 255);
    private static readonly Color m_HitColor = Color.FromArgb(ALPHA_VALUE_220, 255, 0, 0);
    private static readonly Color m_MissColor = Color.FromArgb(255, 255, 255);
    private static readonly Color m_FleetColor = Color.FromArgb(ALPHA_VALUE_220, 74, 74, 74);
    private static readonly Color m_PlacementColor = Color.FromArgb(255, 191, 0);
    private static readonly Color m_PlacementErrorColor = Color.FromArgb(255, 0, 0);
    private static readonly Color m_SunkenColor = Color.FromArgb(0, 50, 196);

    //Pens
    private static readonly Pen m_GridPen = new Pen(m_GridLineColor, PEN_WIDTH);
    private static readonly Pen m_FleetPen = new Pen(Color.WhiteSmoke, PEN_WIDTH);
    private static readonly Pen m_HitPen = new Pen(Color.FromArgb(ALPHA_VALUE_180, m_HitColor), PEN_WIDTH);
    private static readonly Pen m_MissPen = new Pen(Color.FromArgb(ALPHA_VALUE_180, m_MissColor), PEN_WIDTH);
    private static readonly Pen m_HoverPen = new Pen(Color.FromArgb(ALPHA_VALUE_180, m_HoverColor), PEN_WIDTH);
    private static readonly Pen m_PlacementPen = new Pen(Color.FromArgb(ALPHA_VALUE_180, m_PlacementColor), PEN_WIDTH);
    private static readonly Pen m_PlacementErrorPen = new Pen(m_PlacementErrorColor, PEN_WIDTH);
    private static readonly Pen m_SunkenPen = new Pen(m_SunkenColor, PEN_WIDTH);

    //Brushes
    private static readonly Brush m_TextBrush = new SolidBrush(m_TextColor);
    private static readonly Brush m_FleetBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_220, m_FleetColor));
    private static readonly Brush m_HitBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_128, m_HitColor));
    private static readonly Brush m_MissBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_128, m_MissColor));
    private static readonly Brush m_HoverBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_48, m_HoverColor));
    private static readonly Brush m_PlacementBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_48, m_PlacementColor));
    private static readonly Brush m_PlacementErrorBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_48, m_PlacementErrorColor));
    private static readonly Brush m_SunkenBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_48, m_SunkenColor));

    //Fonts
    private readonly string m_FontName = "Consolas";
    
    #region Events

    public event EventHandler<GridCellClickedEventArgs>? GridCellClicked;

    private void OnGridCellClicked(GridCellClickedEventArgs args)
        => GridCellClicked?.Invoke(this, args);

    private void OnGridCellClicked(GridCell gridCell, MouseButtons button, int clicks)
        => OnGridCellClicked(new GridCellClickedEventArgs(gridCell, button, clicks));

    #endregion

    #endregion

    #region Methods

    #region Constructors

    public FleetGameBoard()
    {
        m_BoardFont = new Font(m_FontName, 10f, FontStyle.Bold);

        Initialization();
    }

    #endregion

    private void Initialization()
    {
        DoubleBuffered = true;
        base.BackColor = m_BackgroundColor;
        ResizeRedraw = true;
        
        Size = new Size(400, 400);

        MouseMove += OnMouseMove;
        MouseLeave += (s, e) =>
        {
            m_HoverCell = null;    
            Invalidate();
        };
        MouseClick += OnMouseClick;
    }

    private void OnMouseClick(object? sender, MouseEventArgs e)    
    {
        if(!HoverEnabled) 
            return;

        if (m_CellSize <= 0)
            return;

        var column = (e.X - m_OffsetX) / m_CellSize;
        var row = (e.Y - m_OffsetY) / m_CellSize;

        GridCell? clickedCell = (column is >= 0 and < GRID_SIZE && row is >= 0 and < GRID_SIZE)
            ? new GridCell(row, column) : null;

        if(clickedCell == null)
            return;

        OnGridCellClicked(new GridCellClickedEventArgs(clickedCell.Value, e.Button, e.Clicks));
    }

    private void OnMouseMove(object? sender, MouseEventArgs e)
    {
        if (m_CellSize <= 0) return;

        var column = (e.X - m_OffsetX) / m_CellSize;
        var row = (e.Y - m_OffsetY) / m_CellSize;

        GridCell? hoverCell = (column is >= 0 and < GRID_SIZE && row is >= 0 and < GRID_SIZE)
            ? new GridCell(row, column) : null;

        if (Equals(m_HoverCell, hoverCell))
            return;

        m_HoverCell = hoverCell;
        
        Invalidate();
    }

    private void DrawGrid(Graphics graphics)
    {

        for (var i = 0; i <= GRID_SIZE; i++)
        {
            var x = m_OffsetX + i * m_CellSize;
            var y = m_OffsetY + i * m_CellSize;

            graphics.DrawLine(m_GridPen, x, m_OffsetY, x, m_OffsetY + GRID_SIZE * m_CellSize);
            graphics.DrawLine(m_GridPen, m_OffsetX, y, m_OffsetX + GRID_SIZE * m_CellSize, y);
        }
    }

    private void DrawGridLabels(Graphics graphics)
    {

        for (var i = 0; i < GRID_SIZE; i++)
        {
            // Letters A–J
            var number = (i + 1).ToString();
            var numberSize = graphics.MeasureString(number, m_BoardFont);

            var numberX = m_OffsetX + (i * m_CellSize) + (m_CellSize - numberSize.Width) / 2f;
            var topY = m_OffsetY - numberSize.Height - 6;
            float bottomY = m_OffsetY + (GRID_SIZE * m_CellSize) + 6;

            graphics.DrawString(number, m_BoardFont, m_TextBrush, numberX, topY);
            graphics.DrawString(number, m_BoardFont, m_TextBrush, numberX, bottomY);

            // Numbers 1–10
            var letter = ((char)('A' + i)).ToString();
            var letterSize = graphics.MeasureString(letter, m_BoardFont);

            var letterY = m_OffsetY + (i * m_CellSize) + (m_CellSize - letterSize.Height) / 2f;
            var leftX = m_OffsetX - letterSize.Width - 8;
            float rightX = m_OffsetX + (GRID_SIZE * m_CellSize) + 8;

            graphics.DrawString(letter, m_BoardFont, m_TextBrush, leftX, letterY);
            graphics.DrawString(letter, m_BoardFont, m_TextBrush, rightX, letterY);
        }

    }

    private void DrawHover(Graphics graphics)
    {
        // Hover glow
        if (!m_HoverCell.HasValue) return;

        var x = m_OffsetX + m_HoverCell.Value.Column * m_CellSize;
        var y = m_OffsetY + m_HoverCell.Value.Row * m_CellSize;

        graphics.DrawRectangle(m_HoverPen, x, y, m_CellSize, m_CellSize);

        graphics.FillRectangle(m_HoverBrush, x, y, m_CellSize, m_CellSize);
    }

    private Rectangle? MergeGridCells(IEnumerable<GridCell> gridCells)
    {
        var rect = Rectangle.Empty;

        foreach (var gridCell in gridCells)
        {
            var x = m_OffsetX + gridCell.Column * m_CellSize;
            var y = m_OffsetY + gridCell.Row * m_CellSize;

            rect = rect.IsEmpty ? 
                new Rectangle(x, y, m_CellSize, m_CellSize) : 
                Rectangle.Union(rect, new Rectangle(x, y, m_CellSize, m_CellSize));
        }

        return rect;
    }

    private void DrawShips(Graphics graphics, IEnumerable<ShipInfo> ships, bool sunk = false)
    {
        var fillBrush = sunk ? m_SunkenBrush : m_FleetBrush;
        var outlinePen = sunk ? m_SunkenPen : m_FleetPen;

        foreach (var ship in ships)
        {
            var rect = MergeGridCells(ship.Location);
            if(rect == null)
                continue;

            DrawShip(graphics, rect.Value, fillBrush, outlinePen);
        }
    }

    private void DrawShip(Graphics graphics, Rectangle rectangle, Brush fillBrush, Pen outlinePen)
    {
        graphics.FillRectangle(fillBrush, rectangle);
        graphics.DrawRectangle(outlinePen, rectangle);
    }

    private void DrawPegs(Graphics graphics, IEnumerable<GridCell> pegs, ShotReport report = ShotReport.Hit)
    {
        var originalSmoothingMode = graphics.SmoothingMode;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        foreach (var gridCell in pegs)
        {
            var x = m_OffsetX + gridCell.Column * m_CellSize;
            var y = m_OffsetY + gridCell.Row * m_CellSize;
            var inset = Math.Max(1, m_CellSize / 4);
            var rect = new Rectangle(x + inset, y + inset, m_CellSize - inset * 2, m_CellSize - inset * 2);

            if (report == ShotReport.Hit)
            {
                graphics.FillEllipse(m_HitBrush, rect);
                graphics.DrawEllipse(m_HitPen, rect);
            }
            else
            {
                graphics.FillEllipse(m_MissBrush, rect);
                graphics.DrawEllipse(m_MissPen, rect);
            }
        }

        graphics.SmoothingMode = originalSmoothingMode;
    }
    
    public void PostSITREP(BattleFieldSITREP sitrep)
    {
        if (IsAttackBoard)
        {
            m_Ships = sitrep.EnemySunk;
            m_Hits = sitrep.Hits;
            m_Misses = sitrep.Misses;
        }
        else
        {
            m_Ships = sitrep.Fleet;
            m_Hits = sitrep.Damage;
        }

        Invalidate();
    }

    #region Overrides

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var graphics = e.Graphics;
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        DrawGrid(graphics);
        DrawGridLabels(graphics);

        if (HoverEnabled)
            DrawHover(graphics);

        if(m_Ships != null)
            DrawShips(graphics, m_Ships, IsAttackBoard);

        if (m_Hits != null)
            DrawPegs(graphics, m_Hits);

        if (m_Misses != null)
            DrawPegs(graphics, m_Misses, ShotReport.Miss);
    }

    protected override void OnSizeChanged(System.EventArgs e)
    {

        var cellSize = Math.Min((ClientSize.Width - 80) / GRID_SIZE, (ClientSize.Height - 80) / GRID_SIZE);

        if (cellSize != m_CellSize)
        {
            m_CellSize = cellSize;
            m_OffsetX = (ClientSize.Width - GRID_SIZE * m_CellSize) / 2;
            m_OffsetY = (ClientSize.Height - GRID_SIZE * m_CellSize) / 2;

            var fontSize = Math.Max(8f, m_CellSize * 0.28f);

            if (Math.Abs(m_BoardFont.Size - fontSize) > 0.01f)
            {
                m_BoardFont?.Dispose();
                m_BoardFont = new Font(m_FontName, fontSize, FontStyle.Bold);
            }
        }

        Width = Height;

        base.OnSizeChanged(e);

        Invalidate();
    }

    #endregion

    #endregion

    #region Properties

    private int m_OffsetX = 0;
    
    private int m_OffsetY = 0;
    
    private int m_CellSize = 0;
    
    private Font m_BoardFont;
    
    private GridCell? m_HoverCell;

    private IEnumerable<ShipInfo>? m_Ships;

    private IEnumerable<GridCell>? m_Hits;

    private IEnumerable<GridCell>? m_Misses;

    private bool m_HoverEnabled = true;

    public bool HoverEnabled
    {
        get => m_HoverEnabled;
        set
        {
            if (m_HoverEnabled == value)
                return;

            m_HoverEnabled = value;

            Invalidate();
        }
    }

    private bool m_IsAttackBoard = false;

    public bool IsAttackBoard
    {
        get => m_IsAttackBoard;
        set
        {
            if(m_IsAttackBoard == value)
                return;

            m_IsAttackBoard = value;

            Invalidate();
        }
    }

    public new Color BackColor
    {
        get => base.BackColor;
        set { }
    }

    #endregion

}