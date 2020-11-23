using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Content.Items;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.Core;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Content.Tiles.Vitric
{
    internal class AncientSandstone : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = BasicVitricTileLoader.VitricTileDir + "AncientSandstone";
            return true;
        }

        public override void SetDefaults()
        {
            minPick = int.MaxValue;
            QuickBlock.QuickSet(this, 200, DustID.Copper, SoundID.Tink, new Color(150, 105, 65), ItemType<AncientSandstoneItem>());
            Main.tileMerge[Type][TileType<AncientSandstoneTile>()] = true;
        }
    }

    public class AncientSandstoneItem : QuickTileItem { public AncientSandstoneItem() : base("Ancient Sandstone Brick", "", TileType<AncientSandstone>(), 0, BasicVitricTileLoader.VitricTileDir + "AncientSandstoneItem") { } }


    internal class AncientSandstoneTile : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = BasicVitricTileLoader.VitricTileDir + "AncientSandstoneTile";
            return true;
        }

        public override void SetDefaults()
        {
            minPick = int.MaxValue;
            QuickBlock.QuickSet(this, 200, DustID.Copper, SoundID.Tink, new Color(160, 115, 75), ItemType<AncientSandstoneTileItem>());
            Main.tileMerge[Type][TileType<AncientSandstone>()] = true;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Color color = i % 2 == 0 ? Lighting.GetColor(i, j) * 1.5f : Lighting.GetColor(i, j) * 1.1f;

            spriteBatch.Draw(Main.tileTexture[TileType<AncientSandstone>()], (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition, new Rectangle(tile.frameX, tile.frameY, 16, 16), Lighting.GetColor(i, j));
            spriteBatch.Draw(Main.tileTexture[Type], (new Vector2(i, j) + Helper.TileAdj) * 16 - Main.screenPosition, new Rectangle(tile.frameX, tile.frameY, 16, 16), color);
        }
    }

    public class AncientSandstoneTileItem : QuickTileItem { public AncientSandstoneTileItem() : base("Ancient Sandstone Tiles", "", TileType<AncientSandstoneTile>(), 0, BasicVitricTileLoader.VitricTileDir + "AncientSandstoneTileItem") { } }

    internal class AncientSandstonePlatform : ModTile
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = BasicVitricTileLoader.VitricTileDir + " AncientSandstonePlatform";
            return true;
        }

        public override void SetDefaults()
        {
            Main.tileSolidTop[Type] = true;
            Main.tileBlockLight[Type] = false;
            minPick = 200;
            AddMapEntry(new Color(150, 105, 65));
        }
    }

    internal class AncientSandstonePlatformItem : QuickTileItem { public AncientSandstonePlatformItem() : base("Ancient Sandstone Platform", "", TileType<AncientSandstonePlatform>(), 0, BasicVitricTileLoader.VitricTileDir + " AncientSandstonePlatformItem") { } }

    internal class AncientSandstoneWall : ModWall
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = BasicVitricTileLoader.VitricTileDir + " AncientSandstoneWall";
            return true;
        }

        public override void SetDefaults() => QuickBlock.QuickSetWall(this, DustID.Copper, SoundID.Dig, ItemType<AncientSandstoneWallItem>(), false, new Color(71, 46, 41));
    }

    internal class AncientSandstoneWallItem : QuickWallItem
    {
        public override string Texture => BasicVitricTileLoader.VitricTileDir + "AncientSandstoneWallItem";
        public AncientSandstoneWallItem() : base("Ancient Sandstone Wall", "", WallType<AncientSandstoneWall>(), 0) { }
    }

    internal class AncientSandstonePillarWall : ModWall
    {
        public override bool Autoload(ref string name, ref string texture)
        {
            texture = BasicVitricTileLoader.VitricTileDir + "AncientSandstonePillarWall";
            return true;
        }

        public override void SetDefaults() => QuickBlock.QuickSetWall(this, DustID.Copper, SoundID.Dig, ItemType<AncientSandstonePillarWallItem>(), false, new Color(75, 48, 44));
    }

    internal class AncientSandstonePillarWallItem : QuickWallItem
    {
        public override string Texture => BasicVitricTileLoader.VitricTileDir + "AncientSandstonePillarWallItem";
        public AncientSandstonePillarWallItem() : base("Ancient Sandstone Wall", "", WallType<AncientSandstonePillarWall>(), 0) { }
    }
}