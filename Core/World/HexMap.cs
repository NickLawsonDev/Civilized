using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Core.World;

public class HexMap
{
    public Model GrassHex { get; set; }
    public Model MountainHex { get; set; }
    public Model StoneHex { get; set; }
    public Texture2D Texture { get; set; }

    private List<Hex> _hexs = new List<Hex>();

    public HexMap()
    {
    }

    public void Initialize()
    {
        for (var column = 0; column < 10; column++)
        {
            for (var row = 0; row < 10; row++)
            {
                var hex = new Hex(column, row, GrassHex, MountainHex, StoneHex, Texture);
                _hexs.Add(hex);
            }
        }
    }

    public void Draw(Matrix view, Matrix projection)
    {
        foreach(var hex in _hexs)
        {
            hex.Draw(view, projection);
        }
    }
}