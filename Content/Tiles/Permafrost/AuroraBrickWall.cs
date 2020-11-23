using Microsoft.Xna.Framework;
using StarlightRiver.Content.Items;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Core;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Content.Tiles.Permafrost
{
    class AuroraBrickWall : ModWall
    {
        public override void SetDefaults() => QuickBlock.QuickSetWall(this, DustID.Ice, SoundID.Tink, ItemType<AuroraBrickWallItem>(), true, new Color(33, 65, 94));
    }

    class AuroraBrickWallItem : QuickWallItem
    {
        public AuroraBrickWallItem() : base("Aurora BrickWall", "Oooh... Preeetttyyy", WallType<AuroraBrickWall>(), ItemRarityID.White) { }
    }
}
