using Core.Scenes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Civilized.Core.Scenes
{
    public interface ISceneService
    {
        IScene CurrentScene { get; }
        List<IScene> Scenes { get; set; }

        void AddScene(IScene scene, int index = 0);
        void PlayScene(IScene scene);
        void RemoveScene(IScene scene);
        void LoadContent();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
    }
}