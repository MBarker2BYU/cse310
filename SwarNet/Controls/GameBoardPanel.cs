using SwarNet.Enums;
using SwarNet.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SwarNet
{
    public partial class GameBoardPanel : Panel
    {
        private const int GRID_SIZE = 10;

        private Point? hoveredCell = null;
        private List<Ship> placedShips = new List<Ship>();
        private Dictionary<Point, bool> hitMissMarkers = new Dictionary<Point, bool>(); // true = Hit, false = Miss
        private Ship? currentPlacingShip;

        public bool IsPlacementMode { get; private set; }
        public bool IsReadOnly { get; set; } = false;

        // Events
        public event EventHandler<CellClickEventArgs>? CellClicked;
        public event EventHandler? PlacementComplete;

        // Colors
        private readonly Color backgroundColor = Color.FromArgb(15, 15, 35);
        private readonly Color gridLineColor = Color.FromArgb(80, 0, 255, 255);
        private readonly Color textColor = Color.Cyan;
        private readonly Color hoverCenter = Color.FromArgb(140, 0, 255, 255);
        private readonly Color hoverEdge = Color.Transparent;
        private readonly Color hitColor = Color.FromArgb(220, 255, 0, 0);
        private readonly Color missColor = Color.FromArgb(220, 255, 255, 255);

        public GameBoardPanel()
        {
            DoubleBuffered = true;
            BackColor = backgroundColor;
            ResizeRedraw = true;

            MouseMove += GameBoardPanel_MouseMove;
            MouseLeave += (s, e) => { hoveredCell = null; Invalidate(); };
            MouseClick += GameBoardPanel_MouseClick;
        }

        public void StartPlacement()
        {
            placedShips.Clear();
            hitMissMarkers.Clear();
            IsPlacementMode = true;
            IsReadOnly = false;

            placedShips.Add(new Ship(VesselType.Carrier));
            placedShips.Add(new Ship(VesselType.Battleship));
            placedShips.Add(new Ship(VesselType.Destoryer));
            placedShips.Add(new Ship(VesselType.Submarine));
            placedShips.Add(new Ship(VesselType.PatrolBoat));

            currentPlacingShip = placedShips[0];
            Invalidate();
        }

        public void EndPlacement()
        {
            IsPlacementMode = false;
            currentPlacingShip = null;
            PlacementComplete?.Invoke(this, EventArgs.Empty);
            Invalidate();
        }

        public void MarkHit(int row, int col)
        {
            hitMissMarkers[new Point(col, row)] = true;

            foreach (var ship in placedShips)
            {
                if (ship.IsPlaced && IsCellInShip(ship, row, col))
                {
                    ship.TakeHit();
                    break;
                }
            }

            Invalidate();
        }

        public void MarkMiss(int row, int col)
        {
            hitMissMarkers[new Point(col, row)] = false;
            Invalidate();
        }

        private bool IsCellInShip(Ship ship, int row, int col)
        {
            if (!ship.IsPlaced) return false;

            if (ship.IsHorizontal)
                return row == ship.StartRow && col >= ship.StartCol && col < ship.StartCol + ship.Length;

            return col == ship.StartCol && row >= ship.StartRow && row < ship.StartRow + ship.Length;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int cellSize = Math.Min((ClientSize.Width - 80) / GRID_SIZE, (ClientSize.Height - 80) / GRID_SIZE);
            int offsetX = (ClientSize.Width - GRID_SIZE * cellSize) / 2;
            int offsetY = (ClientSize.Height - GRID_SIZE * cellSize) / 2;

            // Grid lines (always bright)
            using (var pen = new Pen(gridLineColor, 1.5f))
            {
                for (int i = 0; i <= GRID_SIZE; i++)
                {
                    int x = offsetX + i * cellSize;
                    int y = offsetY + i * cellSize;
                    g.DrawLine(pen, x, offsetY, x, offsetY + GRID_SIZE * cellSize);
                    g.DrawLine(pen, offsetX, y, offsetX + GRID_SIZE * cellSize, y);
                }
            }

            // Ships
            foreach (var ship in placedShips)
            {
                if (!ship.IsPlaced) continue;

                int x = offsetX + ship.StartCol * cellSize;
                int y = offsetY + ship.StartRow * cellSize;
                int w = ship.IsHorizontal ? ship.Length * cellSize : cellSize;
                int h = ship.IsHorizontal ? cellSize : ship.Length * cellSize;

                using (var brush = new SolidBrush(ship.CurrentColor))
                    g.FillRectangle(brush, x, y, w, h);

                using (var pen = new Pen(Color.White, 1.5f))
                    g.DrawRectangle(pen, x, y, w, h);
            }

            // Hover glow - ONLY active during placement and not read-only
            if (IsPlacementMode && !IsReadOnly && hoveredCell.HasValue)
            {
                int x = offsetX + hoveredCell.Value.X * cellSize;
                int y = offsetY + hoveredCell.Value.Y * cellSize;

                using (var glowPen = new Pen(Color.FromArgb(180, 0, 255, 255), 4))
                    g.DrawRectangle(glowPen, x, y, cellSize, cellSize);

                using (var path = new GraphicsPath())
                {
                    path.AddRectangle(new Rectangle(x, y, cellSize, cellSize));
                    using (var brush = new PathGradientBrush(path))
                    {
                        brush.CenterColor = hoverCenter;
                        brush.SurroundColors = new[] { hoverEdge };
                        g.FillPath(brush, path);
                    }
                }
            }

            // Hit/Miss markers
            foreach (var kvp in hitMissMarkers)
            {
                int x = offsetX + kvp.Key.X * cellSize;
                int y = offsetY + kvp.Key.Y * cellSize;
                Color c = kvp.Value ? hitColor : missColor;

                using (var brush = new SolidBrush(c))
                    g.FillEllipse(brush, x + 6, y + 6, cellSize - 12, cellSize - 12);
            }

            // Labels
            float fontSize = Math.Max(8f, cellSize * 0.28f);
            using (var font = new Font("Consolas", fontSize, FontStyle.Bold))
            using (var brush = new SolidBrush(textColor))
            {
                for (int i = 0; i < GRID_SIZE; i++)
                {
                    string t = ((char)('A' + i)).ToString();
                    var sz = g.MeasureString(t, font);
                    g.DrawString(t, font, brush,
                        offsetX + i * cellSize + (cellSize - sz.Width) / 2,
                        offsetY - sz.Height - 6);

                    g.DrawString(t, font, brush,
                        offsetX + i * cellSize + (cellSize - sz.Width) / 2,
                        offsetY + GRID_SIZE * cellSize + 6);

                    t = (i + 1).ToString();
                    sz = g.MeasureString(t, font);
                    g.DrawString(t, font, brush,
                        offsetX - sz.Width - 8,
                        offsetY + i * cellSize + (cellSize - sz.Height) / 2);

                    g.DrawString(t, font, brush,
                        offsetX + GRID_SIZE * cellSize + 8,
                        offsetY + i * cellSize + (cellSize - sz.Height) / 2);
                }
            }
        }

        private void GameBoardPanel_MouseMove(object? sender, MouseEventArgs e)
        {
            if (IsReadOnly || !IsPlacementMode) return;

            int cellSize = Math.Min((ClientSize.Width - 80) / GRID_SIZE, (ClientSize.Height - 80) / GRID_SIZE);
            int offsetX = (ClientSize.Width - GRID_SIZE * cellSize) / 2;
            int offsetY = (ClientSize.Height - GRID_SIZE * cellSize) / 2;

            int col = (e.X - offsetX) / cellSize;
            int row = (e.Y - offsetY) / cellSize;

            hoveredCell = (col >= 0 && col < GRID_SIZE && row >= 0 && row < GRID_SIZE)
                ? new Point(col, row)
                : null;

            Invalidate();
        }

        private void GameBoardPanel_MouseClick(object? sender, MouseEventArgs e)
        {
            if (IsReadOnly || !IsPlacementMode || currentPlacingShip == null) return;

            int cellSize = Math.Min((ClientSize.Width - 80) / GRID_SIZE, (ClientSize.Height - 80) / GRID_SIZE);
            int offsetX = (ClientSize.Width - GRID_SIZE * cellSize) / 2;
            int offsetY = (ClientSize.Height - GRID_SIZE * cellSize) / 2;

            int col = (e.X - offsetX) / cellSize;
            int row = (e.Y - offsetY) / cellSize;

            if (col < 0 || col >= GRID_SIZE || row < 0 || row >= GRID_SIZE) return;

            if (e.Button == MouseButtons.Right)
            {
                // Rotate if clicking any cell of the current ship
                var cells = GetShipCells(currentPlacingShip);
                if (Array.Exists(cells, p => p.X == col && p.Y == row))
                {
                    currentPlacingShip.IsHorizontal = !currentPlacingShip.IsHorizontal;
                    Invalidate();
                }
                return;
            }

            // Left click - place
            currentPlacingShip.StartRow = row;
            currentPlacingShip.StartCol = col;

            if (IsValidPlacement(currentPlacingShip))
            {
                currentPlacingShip.IsPlaced = true;
                currentPlacingShip = null;
                Invalidate();

                if (placedShips.All(s => s.IsPlaced))
                {
                    EndPlacement();
                }
            }
        }

        private Point[] GetShipCells(Ship ship)
        {
            var cells = new Point[ship.Length];
            if (ship.IsHorizontal)
                for (int i = 0; i < ship.Length; i++)
                    cells[i] = new Point(ship.StartCol + i, ship.StartRow);
            else
                for (int i = 0; i < ship.Length; i++)
                    cells[i] = new Point(ship.StartCol, ship.StartRow + i);
            return cells;
        }

        private bool IsValidPlacement(Ship ship)
        {
            if (ship.IsHorizontal && ship.StartCol + ship.Length > GRID_SIZE) return false;
            if (!ship.IsHorizontal && ship.StartRow + ship.Length > GRID_SIZE) return false;

            var newCells = GetShipCells(ship);
            foreach (var existing in placedShips)
            {
                if (!existing.IsPlaced) continue;
                var existingCells = GetShipCells(existing);
                if (newCells.Intersect(existingCells).Any())
                    return false;
            }

            return true;
        }
    }

    public class CellClickEventArgs : EventArgs
    {
        public int Row { get; }
        public int Col { get; }

        public CellClickEventArgs(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}