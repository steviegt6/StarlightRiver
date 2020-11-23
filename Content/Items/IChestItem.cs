using Terraria;
using Terraria.ModLoader;

namespace StarlightRiver.Content.Items
{
    internal interface IChestItem
    {
        int ItemStack(Chest chest);
        bool GenerateCondition(Chest chest);
    }
}