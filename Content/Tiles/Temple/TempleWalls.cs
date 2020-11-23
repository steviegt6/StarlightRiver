using Microsoft.Xna.Framework;
using StarlightRiver.Content.Items;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Core;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Content.Tiles.Temple
{
    class TempleWall : ModWall { public override void SetDefaults() { QuickBlock.QuickSetWall(this, DustID.Stone, SoundID.Dig, ItemType<TempleWallItem>(), true, new Color(20, 20, 20)); } }
    class TempleWallItem : QuickWallItem { public TempleWallItem() : base("Ancient Temple Brick Wall", "", WallType<TempleWall>(), ItemRarityID.White) { } }

    class TempleWallBig : ModWall { public override void SetDefaults() { QuickBlock.QuickSetWall(this, DustID.Stone, SoundID.Dig, ItemType<TempleWallBigItem>(), true, new Color(20, 20, 20)); } }
    class TempleWallBigItem : QuickWallItem { public TempleWallBigItem() : base("Large Ancient Temple Brick Wall", "", WallType<TempleWallBig>(), ItemRarityID.White) { } }
}
