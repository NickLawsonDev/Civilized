using Microsoft.Xna.Framework;
using System;

namespace Core.Scenes;

public interface IScene
{
    Guid Id { get; }

    void Initialize();
    void LoadContent();
    void Update(GameTime gameTime);
    void Draw(GameTime gameTime);
}