using Microsoft.Xna.Framework;
using MonoECS.Collections;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoECS.Engine.Scenes
{
    public interface ISceneManager
    {
        void LoadScene(IScene scene);
        IScene GetActiveScene();
    }

    public class SceneManager : DrawableGameComponent, ISceneManager
    {
        private IScene _activeScene;
        private IScene _prevScene;

        public SceneManager(Game game) : base(game)
        {
            _activeScene = null;
            _prevScene = null;
        }

        public void LoadScene(IScene scene)
        {
            if (_activeScene != null)
            {
                _prevScene = _activeScene;
                _prevScene.Dispose();
                _prevScene.UnloadContent();
            }

            _activeScene = scene;

            _activeScene.LoadContent();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            _activeScene.Dispose();
            _activeScene.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            _activeScene.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _activeScene.Draw(gameTime);
            base.Draw(gameTime);
        }

        public IScene GetActiveScene() => _activeScene;
    }
}
