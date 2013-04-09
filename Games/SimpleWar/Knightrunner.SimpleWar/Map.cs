using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knightrunner.SimpleWar
{
    public class Map
    {
        public const string DefaultTerrainTypeName = "Plain";

        private Location[,] locations;

        public Map(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.locations = new Location[height, width];
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    locations[row, col] = new Location { TerrainType = TerrainTypeRepository.Instance.Get(DefaultTerrainTypeName), Row = row, Column = col };
                }
            }
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Location GetLocation(int row, int col)
        {
            return locations[row, col];
        }

        public IEnumerable<Location> Locations
        {
            get
            {
                for (int row = 0; row < Height; row++)
                {
                    for (int col = 0; col < Width; col++)
                    {
                        yield return locations[row, col];
                    }
                }
            }
        }
    }
}
