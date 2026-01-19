using SwarNet.Enums;
using SwarNet.Utilites;

namespace SwarNet.Models
{
    public class Ship
    {
        public VesselType Type { get; }
        public string Name { get; }
        public int Length { get; }
        public int Health { get; private set; } 
        public bool IsHorizontal { get; set; } = true;
        public int StartRow { get; set; }
        public int StartCol { get; set; }
        public bool IsPlaced { get; set; }

        public Color CurrentColor =>
            Health == Length ? Color.FromArgb(50, 0, 255, 100) :        // Neon Green - healthy
            Health > 0 ? Color.FromArgb(50, 255, 180, 0) :               // Neon Amber - damaged
            Color.FromArgb(50, 255, 0, 80);                              // Neon Red - sunk

        public Ship(VesselType type)
        {
            Type = type;
            Name = type.ToStringFromCamelCase();
            Length = GetDefaultLength(type);
            Health = Length;
        }

        private static int GetDefaultLength(VesselType type) => type switch
        {
            VesselType.Carrier => 5,
            VesselType.Battleship => 4,
            VesselType.Destoryer => 3,
            VesselType.Submarine => 3,
            VesselType.PatrolBoat => 2,
            _ => 1
        };

        public void TakeHit()
        {
            if (Health > 0)
                Health--;
        }

        public bool IsSunk => Health <= 0;
    }
}