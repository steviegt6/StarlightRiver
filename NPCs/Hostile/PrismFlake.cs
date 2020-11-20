using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using StarlightRiver;
using static Terraria.ModLoader.ModContent;

namespace StarlightRiver.NPCs.Hostile
{
    internal class PrismFlake : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Prism Flake");
            Main.npcFrameCount[npc.type] = 1;
        }

        public override void SetDefaults()
        {
            npc.width = 30;
            npc.height = 34;
            npc.damage = 18;
            npc.defense = 12;
            npc.noGravity = true;
            npc.lifeMax = 50;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.value = 500f;
            npc.knockBackResist = 0.2f;
            npc.noGravity = true;
            npc.alpha = 100;
        }

        private float alpha;
        int[] spikes = new int[6];
        bool spawnedSpikes = false;
        public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
        {
            alpha += 0.05f;
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            Color shadeColor = Main.hslToRgb((alpha / 3) % 1, 1f, 0.7f); ;
            StarlightRiver.PrismShader.Parameters["alpha"].SetValue(alpha * 2 % 6);
            StarlightRiver.PrismShader.Parameters["shineSpeed"].SetValue(0.7f);
            StarlightRiver.PrismShader.Parameters["tentacle"].SetValue(mod.GetTexture("NPCs/Hostile/PrismFlakeLightmap"));
            StarlightRiver.PrismShader.Parameters["lightColour"].SetValue(drawColor.ToVector3());
            StarlightRiver.PrismShader.Parameters["prismColor"].SetValue(shadeColor.ToVector3());
            StarlightRiver.PrismShader.Parameters["shaderLerp"].SetValue(1f);
            StarlightRiver.PrismShader.CurrentTechnique.Passes[0].Apply();
            Vector2 drawOrigin = new Vector2(npc.width / 2, npc.height / 2);
            Vector2 drawPos = npc.position - Main.screenPosition;
            shadeColor.A = 150;
            if (spawnedSpikes)
            {
                for (int i = 0; i < 6; i++)
                {
                    Projectile projectile = Main.projectile[spikes[i]];
                    Vector2 drawOrigin2 = new Vector2(projectile.width / 2, projectile.height / 2);
                    Vector2 drawPos2 = projectile.position - Main.screenPosition;
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos2 + drawOrigin2, null, shadeColor, projectile.rotation, drawOrigin2, projectile.scale, SpriteEffects.None, 0f);
                    projectile.timeLeft = 3;
                }
            }
            spriteBatch.Draw(Main.npcTexture[npc.type], drawPos + drawOrigin, null, shadeColor, npc.rotation, drawOrigin, npc.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, null, null, null, null, null, Main.GameViewMatrix.TransformationMatrix);
            return false;
        }
        public override void AI()
        {
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!spawnedSpikes)
            {
                spawnedSpikes = true;
                for (int i = 0; i < 6; i++)
                {
                    spikes[i] = Projectile.NewProjectile(npc.Center, Vector2.Zero, ModContent.ProjectileType<PrismFlakeSpike>(), npc.damage, npc.knockBackResist, player.whoAmI, npc.whoAmI, i);
                }
            }
        }
    }
   internal class PrismFlakeSpike : ModProjectile
   {
      public override void SetStaticDefaults()
      {
            DisplayName.SetDefault("Prism Flake Spike");
      }

       public override void SetDefaults()
       {
           projectile.hostile = true;
            projectile.width = 10;
            projectile.height = 18;
            projectile.aiStyle = -1;
            projectile.friendly = false;
            projectile.damage = 15;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
        }
        float rotateSpeed = 0;

        bool attacking = false;
        Vector2 posToBe = Vector2.Zero;
        float speed = 15;
        bool launching = false;
        int launchTimer;
        public override void AI()
        {
            launchTimer++;
            NPC parent = Main.npc[(int)projectile.ai[0]];
            Player player = projectile.Owner();
            projectile.ai[1] += rotateSpeed;
            if (!attacking)
            {
                rotateSpeed += 0.005f;
                Vector2 offset = new Vector2((float)Math.Sin((double)projectile.ai[1]), (float)Math.Cos((double)projectile.ai[1]));
                projectile.rotation = offset.ToRotation();
                projectile.position = offset + parent.position;
                if (launchTimer % 300 == 100)
                {
                    rotateSpeed = 0;
                    attacking = true;
                    launching = true;
                    Vector2 direction = player.position - parent.Center;
                    direction.Normalize();
                    direction *= speed;
                    projectile.velocity = direction;
                    projectile.tileCollide = true;
                }
            }
            else if (!launching)
            {

            }
            if (launchTimer % 300 == 200)
            {
                launching = false;
                attacking = false;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.velocity = Vector2.Zero;
            return false;
        }
    }
}