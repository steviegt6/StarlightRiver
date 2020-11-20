using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs
{
    internal class AuroracleBellNPC : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Auroracle bell");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.width = 32;
            npc.height = 32;
            npc.defense = 12;
            npc.noGravity = true;
            npc.lifeMax = 5000;
            npc.friendly = false;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 500f;
            npc.immortal = true;
            npc.knockBackResist = 0.2f;
            npc.damage = 0;
        }
        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            Main.NewText("Fuck you!");
            base.OnHitByProjectile(projectile, damage, knockback, crit);
			npc.life = 5000;
        }
    }
}