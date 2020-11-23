using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Content.NPCs
{
    internal interface IDynamicMapIcon
    {
        void DrawOnMap(SpriteBatch spriteBatch, Vector2 center, float scale, Color color);
    }
}