using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MiJuego.Helpers;

    public static class SpriteHelper
    {
        private static ContentManager _content;

        public static void Initialize(ContentManager content)
        {
            _content = content;
        }

        public static Texture2D Load(string texture)
    {
            if (_content == null)
                throw new InvalidOperationException("SpriteHelper is not initialized. Call Initialize() with a valid ContentManager before loading assets.");
            return _content.Load<Texture2D>(texture);
        }
    }
