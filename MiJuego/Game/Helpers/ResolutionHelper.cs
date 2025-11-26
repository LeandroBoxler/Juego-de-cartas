using Microsoft.Xna.Framework.Graphics;

namespace MiJuego.Helpers;
public static class ResolutionHelper
{
    public const int VirtualWidth = 800;
    public const int VirtualHeight = 600;
    public static float ScaleX { get; private set; }
    public static float ScaleY { get; private set; }

    public static void UpdateScale(GraphicsDevice graphicsDevice)
    {
        ScaleX = (float)graphicsDevice.Viewport.Width / VirtualWidth;
        ScaleY = (float)graphicsDevice.Viewport.Height / VirtualHeight;
    }
}