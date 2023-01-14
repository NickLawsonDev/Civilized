using Core.Scenes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Civilized.Core.Scenes;

public class SceneService : ISceneService
{
    public List<IScene> Scenes { get; set; } = new List<IScene>();
    public IScene CurrentScene => Scenes.First();

    public void AddScene(IScene scene, int index = 0)
    {
        if (scene == null) throw new ArgumentNullException(nameof(scene));

        if (index > 0)
        {
            Scenes.Insert(index, scene);
        }
        else
        {
            Scenes.Add(scene);
        }
    }

    public void RemoveScene(IScene scene)
    {
        if (scene == null) throw new ArgumentNullException(nameof(scene));

        Scenes.Remove(scene);
    }

    public void PlayScene(IScene scene)
    {
        if (scene == null) throw new ArgumentNullException(nameof(scene));

        Scenes.Insert(0, scene);
    }

    public void LoadContent()
    {
        foreach(var scene in Scenes)
        {
            scene.LoadContent();
        }
    }

    public void Update(GameTime gameTime)
    {
        foreach(var scene in Scenes)
        {
            scene.Update(gameTime);
        }
    }

    public void Draw(GameTime gameTime)
    {
        foreach(var scene in Scenes)
        {
            scene.Draw(gameTime);
        }
    }
}