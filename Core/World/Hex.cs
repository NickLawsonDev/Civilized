using System;
using Civilized.src.Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.World;

public class Hex
{
    private readonly float WIDTH_MULTIPLIER = MathF.Sqrt(3) / 2;

    //Column + Row + S = 0
    public int Column { get; set; }
    public int Row { get; set; }
    public int S { get; set; }

    public Model @Model { get; set; }
    public Texture2D Texture { get; set; }
    public TileType Type { get; set; }

    public Hex(int column, int row, Model grass, Model mountain, Model stone, Texture2D texture)
    {
        Column = column;
        Row = row;
        S = -(column + row);

        var random = new Random();
        Type = (TileType)random.Next(0, 3);

        switch (Type)
        {
            case TileType.Grass:
                Model = grass;
                break;
            case TileType.Mountain:
                Model = mountain;
                break;
            case TileType.Stone:
                Model = stone;
                break;
        }

        Texture = texture;
    }

    public Vector3 GetPosition()
    {
        var shouldOffset = (Row % 2) == 0;
        const float radius = 1f;
        const float height = radius * 2;
        var width = WIDTH_MULTIPLIER * height;

        var offset = shouldOffset ? width / 2 : 0;
        var horizontal = width;
        const float vertical = height * 0.75f;

        var x = (Column * horizontal) + offset;
        var y = Row * vertical;

        return new Vector3(x, 0, y);
    }

    public void Draw(Matrix view, Matrix projection)
    {
        foreach (ModelMesh mesh in Model.Meshes)
        {
            foreach (BasicEffect effect in mesh.Effects)
            {
                effect.World = Matrix.CreateWorld(GetPosition(), Vector3.Forward, Vector3.Up);
                effect.View = view;
                effect.Projection = projection;
            }

            mesh.Draw();
        }
    }
}