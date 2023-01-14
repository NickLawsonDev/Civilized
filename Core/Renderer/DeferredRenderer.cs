using Core.Scenes;
using Core.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.Renderer;

public class DeferredRenderer : DrawableGameComponent
{
    private QuadRenderComponent quadRenderer;
    private RenderTarget2D colorRT { get; set; }
    private RenderTarget2D normalRT { get; set; }
    private RenderTarget2D depthRT { get; set; }
    private Effect clearBufferEffect { get; set; }
    private Scene scene { get; set; }
    private SpriteBatch spriteBatch { get; set; }

    public DeferredRenderer(Game game) : base(game) 
    { 
        scene = new Scene(Game);
    }

    public override void Initialize()
    {
        base.Initialize();
        quadRenderer = new QuadRenderComponent(Game);
        Game.Components.Add(quadRenderer);
        scene.Initialize();
    }

    protected override void LoadContent()
    {
        scene.LoadContent();

        var backBufferWidth = GraphicsDevice.PresentationParameters.BackBufferWidth;
        var backBufferHeight = GraphicsDevice.PresentationParameters.BackBufferHeight;

        colorRT = new RenderTarget2D(GraphicsDevice, backBufferWidth, backBufferHeight, false, SurfaceFormat.Color, DepthFormat.Depth24);
        normalRT = new RenderTarget2D(GraphicsDevice, backBufferWidth, backBufferHeight, false, SurfaceFormat.Color, DepthFormat.None);
        depthRT = new RenderTarget2D(GraphicsDevice, backBufferWidth, backBufferHeight, false, SurfaceFormat.Single, DepthFormat.None);

        clearBufferEffect = Game.Content.Load<Effect>("Shaders/ClearGBuffer");

        spriteBatch = new SpriteBatch(Game.GraphicsDevice);
        base.LoadContent();
    }

    public override void Update(GameTime gameTime)
    {
        scene.Update(gameTime);
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        SetGBuffer();
        ClearGBuffer();
        scene.Draw(gameTime);
        ResolveGBuffer();
        base.Draw(gameTime);
    }

    private void SetGBuffer()
    {
        GraphicsDevice.SetRenderTargets(colorRT, normalRT, depthRT);
    }

    private void ResolveGBuffer()
    {
        GraphicsDevice.SetRenderTargets(null);
    }

    private void ClearGBuffer()
    {
        clearBufferEffect.Techniques[0].Passes[0].Apply();
        quadRenderer.Render(Vector2.One * -1, Vector2.One);
    }
}