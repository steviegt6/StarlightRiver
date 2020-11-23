using StarlightRiver.Content.Tiles.Overgrow;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.Content.Items.Overgrow
{
    public class HatchOvergrowItem : QuickTileItem { public HatchOvergrowItem() : base("Skyview Vent", "", TileType<Tiles.Overgrow.HatchOvergrow>(), 0) { } }
    public class SetpieceOvergrowItem : QuickTileItem
    {
        public SetpieceOvergrowItem() : base("Overgrow Altar", "", TileType<SetpieceAltar>(), 0) { }
        public override string Texture => "StarlightRiver/MarioCumming";
    }
    public class BigHatchOvergrowItem : QuickTileItem
    {
        public BigHatchOvergrowItem() : base("Overgrow Godrays", "", TileType<BigHatchOvergrow>(), 0) { }
        public override string Texture => "StarlightRiver/MarioCumming";
    }
}