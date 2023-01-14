using Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Core.Scenes;

public class Scene : IScene
{
    public Guid Id => Guid.NewGuid();

    private Game _game { get; set; }

    private Camera _camera { get; set; }
    private HexMap hexMap { get; set; }
    private Effect gbufferEffect { get; set; }

    public Scene(Game game)
    {
        _game = game;
    }

    public void Initialize()
    {
        _camera = new Camera(_game.GraphicsDevice, _game.Window)
        {
            Position = new Vector3(15, 30, 19),
            LookAtDirection = new Vector3(0, -7, -3)
        };
    }

    public void LoadContent()
    {
        hexMap = new HexMap
        {
            GrassHex = _game.Content.Load<Model>("Models/GrassHex"),
            MountainHex = _game.Content.Load<Model>("Models/MountainHex"),
            StoneHex = _game.Content.Load<Model>("Models/StoneHex"),
            Texture = _game.Content.Load<Texture2D>("Models/ship1_c")
        };
        hexMap.Initialize();
        gbufferEffect = _game.Content.Load<Effect>("Shaders/RenderGBuffer");
    }

    public void Update(GameTime gameTime)
    {
        _camera.Update(gameTime);
    }

    public void Draw(GameTime gameTime)
    {
        _game.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        _game.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        _game.GraphicsDevice.BlendState = BlendState.Opaque;

        hexMap.Draw(_camera.View, _camera.Projection);
    }
}