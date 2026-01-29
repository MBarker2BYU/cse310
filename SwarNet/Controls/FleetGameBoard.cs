// ***********************************************************************
// Assembly         : SwarNet
// Author           : Matthew D. Barker
// Created          : 01-26-2026
//
// Last Modified By : Matthew D. Barker
// Last Modified On : 01-28-2026
// ***********************************************************************
// <copyright file="FleetGameBoard.cs" company="SwarNet">
//     Copyright (c) Matthew D. Barker. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using SwarNet.Enums;
using SwarNet.EventArgs;
using SwarNet.Structs;
using System.Drawing.Drawing2D;
using Timer = System.Windows.Forms.Timer;

namespace SwarNet.Controls;

/// <summary>
/// Class FleetGameBoard. This class cannot be inherited.
/// Implements the <see cref="System.Windows.Forms.Panel" />
/// </summary>
/// <seealso cref="System.Windows.Forms.Panel" />
public sealed  class FleetGameBoard : Panel
{

    #region Constants

    /// <summary>
    /// The grid size
    /// </summary>
    public const int GRID_SIZE = 10;

    /// <summary>
    /// The pen width
    /// </summary>
    private const float PEN_WIDTH = 1.5f;
    /// <summary>
    /// The alpha value 48
    /// </summary>
    private const int ALPHA_VALUE_48 = 48;
    /// <summary>
    /// The alpha value 128
    /// </summary>
    private const int ALPHA_VALUE_128 = 128;
    /// <summary>
    /// The alpha value 180
    /// </summary>
    private const int ALPHA_VALUE_180 = 180;
    /// <summary>
    /// The alpha value 220
    /// </summary>
    private const int ALPHA_VALUE_220 = 220;

    // Colors
    /// <summary>
    /// The m background color
    /// </summary>
    private static readonly Color m_BackgroundColor = Color.FromArgb(15, 15, 35);
    /// <summary>
    /// The m grid line color
    /// </summary>
    private static readonly Color m_GridLineColor = Color.FromArgb(80, 0, 255, 255);
    /// <summary>
    /// The m text color
    /// </summary>
    private static readonly Color m_TextColor = Color.Cyan;

    /// <summary>
    /// The m hover color
    /// </summary>
    private static readonly Color m_HoverColor = Color.FromArgb(0, 255, 255);
    /// <summary>
    /// The m hit color
    /// </summary>
    private static readonly Color m_HitColor = Color.FromArgb(ALPHA_VALUE_220, 255, 0, 0);
    /// <summary>
    /// The m miss color
    /// </summary>
    private static readonly Color m_MissColor = Color.FromArgb(255, 255, 255);
    /// <summary>
    /// The m fleet color
    /// </summary>
    private static readonly Color m_FleetColor = Color.FromArgb(ALPHA_VALUE_220, 74, 74, 74);
    /// <summary>
    /// The m placement color
    /// </summary>
    private static readonly Color m_PlacementColor = Color.FromArgb(255, 191, 0);
    /// <summary>
    /// The m placement error color
    /// </summary>
    private static readonly Color m_PlacementErrorColor = Color.FromArgb(255, 0, 0);
    /// <summary>
    /// The m sunken color
    /// </summary>
    private static readonly Color m_SunkenColor = Color.FromArgb(0, 50, 196);

    //Pens
    /// <summary>
    /// The m grid pen
    /// </summary>
    private static readonly Pen m_GridPen = new Pen(m_GridLineColor, PEN_WIDTH);
    /// <summary>
    /// The m fleet pen
    /// </summary>
    private static readonly Pen m_FleetPen = new Pen(Color.WhiteSmoke, PEN_WIDTH);
    /// <summary>
    /// The m hit pen
    /// </summary>
    private static readonly Pen m_HitPen = new Pen(Color.FromArgb(ALPHA_VALUE_180, m_HitColor), PEN_WIDTH);
    /// <summary>
    /// The m miss pen
    /// </summary>
    private static readonly Pen m_MissPen = new Pen(Color.FromArgb(ALPHA_VALUE_180, m_MissColor), PEN_WIDTH);
    /// <summary>
    /// The m hover pen
    /// </summary>
    private static readonly Pen m_HoverPen = new Pen(Color.FromArgb(ALPHA_VALUE_180, m_HoverColor), PEN_WIDTH);
    /// <summary>
    /// The m placement pen
    /// </summary>
    private static readonly Pen m_PlacementPen = new Pen(Color.FromArgb(ALPHA_VALUE_180, m_PlacementColor), PEN_WIDTH);
    /// <summary>
    /// The m placement error pen
    /// </summary>
    private static readonly Pen m_PlacementErrorPen = new Pen(m_PlacementErrorColor, PEN_WIDTH);
    /// <summary>
    /// The m sunken pen
    /// </summary>
    private static readonly Pen m_SunkenPen = new Pen(m_SunkenColor, PEN_WIDTH);

    //Brushes
    /// <summary>
    /// The m text brush
    /// </summary>
    private static readonly Brush m_TextBrush = new SolidBrush(m_TextColor);
    /// <summary>
    /// The m fleet brush
    /// </summary>
    private static readonly Brush m_FleetBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_220, m_FleetColor));
    /// <summary>
    /// The m hit brush
    /// </summary>
    private static readonly Brush m_HitBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_128, m_HitColor));
    /// <summary>
    /// The m miss brush
    /// </summary>
    private static readonly Brush m_MissBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_128, m_MissColor));
    /// <summary>
    /// The m hover brush
    /// </summary>
    private static readonly Brush m_HoverBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_48, m_HoverColor));
    /// <summary>
    /// The m placement brush
    /// </summary>
    private static readonly Brush m_PlacementBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_48, m_PlacementColor));
    /// <summary>
    /// The m placement error brush
    /// </summary>
    private static readonly Brush m_PlacementErrorBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_48, m_PlacementErrorColor));
    /// <summary>
    /// The m sunken brush
    /// </summary>
    private static readonly Brush m_SunkenBrush = new SolidBrush(Color.FromArgb(ALPHA_VALUE_48, m_SunkenColor));

    /// <summary>
    /// The m overlay pen
    /// </summary>
    private static readonly Brush m_OverlayPen = new SolidBrush(m_PlacementColor);

    //Fonts
    /// <summary>
    /// The m font name
    /// </summary>
    private readonly string m_FontName = "Consolas";

    #region Events

    /// <summary>
    /// Occurs when [grid cell clicked].
    /// </summary>
    public event EventHandler<GridCellClickedEventArgs>? GridCellClicked;

    /// <summary>
    /// Handles the <see cref="E:GridCellClicked" /> event.
    /// </summary>
    /// <param name="args">The <see cref="GridCellClickedEventArgs"/> instance containing the event data.</param>
    private void OnGridCellClicked(GridCellClickedEventArgs args)
        => GridCellClicked?.Invoke(this, args);

    /// <summary>
    /// Called when [grid cell clicked].
    /// </summary>
    /// <param name="gridCell">The grid cell.</param>
    /// <param name="button">The button.</param>
    /// <param name="clicks">The clicks.</param>
    private void OnGridCellClicked(GridCell gridCell, MouseButtons button, int clicks)
        => OnGridCellClicked(new GridCellClickedEventArgs(gridCell, button, clicks));

    #endregion

    #endregion

    #region Methods

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="FleetGameBoard"/> class.
    /// </summary>
    public FleetGameBoard()
    {
        m_BoardFont = new Font(m_FontName, 10f, FontStyle.Bold);

        Initialization();
    }

    #endregion

    /// <summary>
    /// Initializations this instance.
    /// </summary>
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

        m_Timer = new Timer();
        m_Timer.Tick += TimerOnTick;
    }

    /// <summary>
    /// Timers the on tick.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    private void TimerOnTick(object? sender, System.EventArgs e)
    {
        m_Timer.Stop();

        m_OverlayMessage = string.Empty;

        Invalidate();
    }

    /// <summary>
    /// Handles the <see cref="E:MouseClick" /> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
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

    /// <summary>
    /// Handles the <see cref="E:MouseMove" /> event.
    /// </summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
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

    /// <summary>
    /// Draws the grid.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
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

    /// <summary>
    /// Draws the grid labels.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
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

    /// <summary>
    /// Draws the overlay.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    private void DrawOverlay(Graphics graphics)
    {
        using var stringFormat = new StringFormat();
        {
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            var rect = new RectangleF(0, 0, Width, Height);

            graphics.DrawString(m_OverlayMessage, m_OverlayFont, m_OverlayPen, rect, stringFormat);
        }
    }

    /// <summary>
    /// Draws the hover.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    private void DrawHover(Graphics graphics)
    {
        // Hover glow
        if (!m_HoverCell.HasValue) return;

        var x = m_OffsetX + m_HoverCell.Value.Column * m_CellSize;
        var y = m_OffsetY + m_HoverCell.Value.Row * m_CellSize;

        graphics.DrawRectangle(m_HoverPen, x, y, m_CellSize, m_CellSize);

        graphics.FillRectangle(m_HoverBrush, x, y, m_CellSize, m_CellSize);
    }

    /// <summary>
    /// Merges the grid cells.
    /// </summary>
    /// <param name="gridCells">The grid cells.</param>
    /// <returns>System.Nullable{Rectangle}.</returns>
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

    /// <summary>
    /// Draws the ships.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="ships">The ships.</param>
    /// <param name="sunk">if set to <c>true</c> [sunk].</param>
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

    /// <summary>
    /// Draws the ship.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="rectangle">The rectangle.</param>
    /// <param name="fillBrush">The fill brush.</param>
    /// <param name="outlinePen">The outline pen.</param>
    private void DrawShip(Graphics graphics, Rectangle rectangle, Brush fillBrush, Pen outlinePen)
    {
        graphics.FillRectangle(fillBrush, rectangle);
        graphics.DrawRectangle(outlinePen, rectangle);
    }

    /// <summary>
    /// Draws the pegs.
    /// </summary>
    /// <param name="graphics">The graphics.</param>
    /// <param name="pegs">The pegs.</param>
    /// <param name="report">The report.</param>
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

    /// <summary>
    /// Posts the sitrep.
    /// </summary>
    /// <param name="sitrep">The sitrep.</param>
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

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
    /// </summary>
    /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
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

        if (!string.IsNullOrEmpty(m_OverlayMessage))
            DrawOverlay(graphics);

    }

    /// <summary>
    /// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
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

        m_OverlayFont = new Font(m_FontName, 32, FontStyle.Bold);

        Width = Height;

        base.OnSizeChanged(e);

        Invalidate();
    }

    #endregion

    #endregion

    #region Properties

    /// <summary>
    /// The m offset x
    /// </summary>
    private int m_OffsetX = 0;

    /// <summary>
    /// The m offset y
    /// </summary>
    private int m_OffsetY = 0;

    /// <summary>
    /// The m cell size
    /// </summary>
    private int m_CellSize = 0;

    /// <summary>
    /// The m board font
    /// </summary>
    private Font m_BoardFont;

    /// <summary>
    /// The m hover cell
    /// </summary>
    private GridCell? m_HoverCell;

    /// <summary>
    /// The m ships
    /// </summary>
    private IEnumerable<ShipInfo>? m_Ships;

    /// <summary>
    /// The m hits
    /// </summary>
    private IEnumerable<GridCell>? m_Hits;

    /// <summary>
    /// The m misses
    /// </summary>
    private IEnumerable<GridCell>? m_Misses;

    /// <summary>
    /// The m hover enabled
    /// </summary>
    private bool m_HoverEnabled = true;

    /// <summary>
    /// Gets or sets a value indicating whether [hover enabled].
    /// </summary>
    /// <value><c>true</c> if [hover enabled]; otherwise, <c>false</c>.</value>
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

    /// <summary>
    /// The m is attack board
    /// </summary>
    private bool m_IsAttackBoard = false;

    /// <summary>
    /// Gets or sets a value indicating whether this instance is attack board.
    /// </summary>
    /// <value><c>true</c> if this instance is attack board; otherwise, <c>false</c>.</value>
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

    /// <summary>
    /// Gets or sets the color of the back.
    /// </summary>
    /// <value>The color of the back.</value>
    public new Color BackColor
    {
        get => base.BackColor;
        set { }
    }

    /// <summary>
    /// The m overlay message
    /// </summary>
    private string m_OverlayMessage;

    /// <summary>
    /// Gets or sets the overlay message.
    /// </summary>
    /// <value>The overlay message.</value>
    public string OverlayMessage
    {
        get => m_OverlayMessage;
        set
        {
            if(!string.IsNullOrEmpty(m_OverlayMessage))
                return;
            m_OverlayMessage = value;

            m_Timer.Interval = 2000;
            m_Timer.Start();
            
            Invalidate();
        }
    }

    /// <summary>
    /// The m timer
    /// </summary>
    private Timer m_Timer;

    /// <summary>
    /// The m overlay font
    /// </summary>
    private Font m_OverlayFont;

    #endregion
}