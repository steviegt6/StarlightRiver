using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using StarlightRiver.NPCs;

namespace StarlightRiver.Items.Permafrost
{
    public class AuroracleBell : ModItem
    {
		public override void SetDefaults()
		{
			item.damage = 10;
			item.knockBack = 3f;
			item.mana = 10;
			item.width = 32;
			item.height = 32;
			item.useTime = 36;
			item.useAnimation = 36;
			item.useStyle = ItemUseStyleID.SwingThrow;
			item.value = Item.buyPrice(0, 5, 0, 0);
			item.rare = ItemRarityID.Green;
			item.UseSound = SoundID.Item44;

			item.noMelee = true;
			item.summon = true;
			item.shoot = mod.ProjectileType("TentacleSummonHead");
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			position = Main.MouseWorld; 
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC currentNPC = Main.npc[i];
				if (currentNPC.active
				&& currentNPC.ai[0] == player.whoAmI
				&& currentNPC.type == ModContent.NPCType<AuroracleBellNPC>())
				{
					if (i == currentNPC.whoAmI)
						currentNPC.active = false;
				}
			}

			NPC.NewNPC((int)position.X, (int)position.Y, ModContent.NPCType<AuroracleBellNPC>(), player.whoAmI);

			return false;
		}

		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Call of Auroracle");
            Tooltip.SetDefault("Dock Ock");
        }
    }
}