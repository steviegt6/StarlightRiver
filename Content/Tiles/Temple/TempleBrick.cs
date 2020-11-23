using Microsoft.Xna.Framework;
using StarlightRiver.Content.Items;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Core;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Content.Tiles.Temple
{
    class TempleBrick : ModTile { public override void SetDefaults() => QuickBlock.QuickSet(this, 0, DustID.Stone, SoundID.Tink, new Color(160, 160, 150), ItemType<TempleBrickItem>()); }
    class TempleBrickItem : QuickTileItem { public TempleBrickItem() : base("Ancient Temple Bricks", "", TileType<TempleBrick>(), 0) { } }
}
