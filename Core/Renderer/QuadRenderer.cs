
#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace Core.Renderer
{
    public partial class QuadRenderComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        private VertexPositionTexture[] verts { get; set; } = null;
        private short[] indexBuffer { get; set; } = null;
        private readonly IGraphicsDeviceService graphicsService;

        public QuadRenderComponent(Game game) : base(game) 
        {
            graphicsService = game.Services.GetService<IGraphicsDeviceService>();
        }

        protected override void LoadContent()
        {
            verts = new VertexPositionTexture[]
            {
                new VertexPositionTexture(
                    new Vector3(0,0,0),
                    new Vector2(1,1)),
                new VertexPositionTexture(
                    new Vector3(0,0,0),
                    new Vector2(0,1)),
                new VertexPositionTexture(
                    new Vector3(0,0,0),
                    new Vector2(0,0)),
                new VertexPositionTexture(
                    new Vector3(0,0,0),
                    new Vector2(1,0))
            };

            indexBuffer = new short[] { 0, 1, 2, 2, 3, 0 };
        }

        public void Render(Vector2 v1, Vector2 v2)
        {

            GraphicsDevice device = graphicsService.GraphicsDevice;

            verts[0].Position.X = v2.X;
            verts[0].Position.Y = v1.Y;

            verts[1].Position.X = v1.X;
            verts[1].Position.Y = v1.Y;

            verts[2].Position.X = v1.X;
            verts[2].Position.Y = v2.Y;

            verts[3].Position.X = v2.X;
            verts[3].Position.Y = v2.Y;

            device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, verts, 0, 4, indexBuffer, 0, 2);
        }
    }
}
