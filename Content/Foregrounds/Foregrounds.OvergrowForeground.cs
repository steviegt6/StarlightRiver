﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StarlightRiver.Core;
using Terraria;

namespace StarlightRiver.Content.Foregrounds
{
    class OvergrowForeground  : Foreground
    {
        public override ParticleSystem particleSystem => new ParticleSystem("StarlightRiver/GUI/Assets/HolyBig", UpdateOvergrowWells);

        private void UpdateOvergrowWells(Particle particle)
        {
            particle.Position.Y = particle.Velocity.Y * (600 - particle.Timer) + particle.StoredPosition.Y - Main.screenPosition.Y + (particle.StoredPosition.Y - Main.screenPosition.Y) * particle.Velocity.X * 0.5f;
            particle.Position.X = particle.StoredPosition.X - Main.screenPosition.X + (particle.StoredPosition.X - Main.screenPosition.X) * particle.Velocity.X;

            particle.Color = Color.White * (particle.Timer > 300 ? ((300 - (particle.Timer - 300)) / 300f) : (particle.Timer / 300f)) * particle.Velocity.X * 0.4f * opacity;

            particle.Timer--;
        }

        public override void Draw(SpriteBatch spriteBatch, float opacity)
        {
            int direction = Main.dungeonX > Main.spawnTileX ? -1 : 1;

            if (StarlightWorld.rottime == 0)
                for (int k = 0; k < 10; k++)
                {
                    for (int i = (int)Main.worldSurface; i < Main.maxTilesY - 200; i += 20)
                    {
                        particleSystem.AddParticle(new Particle(new Vector2(0, 0), new Vector2(0.4f, Main.rand.NextFloat(-2, -1)), 0, Main.rand.NextFloat(1.5f, 2),
                            Color.White * 0.05f, 600, new Vector2(Main.dungeonX * 16 + k * (800 * direction) + Main.rand.Next(30), i * 16)));

                        particleSystem.AddParticle(new Particle(new Vector2(0, 0), new Vector2(0.15f, Main.rand.NextFloat(-2, -1)), 0, Main.rand.NextFloat(0.5f, 0.8f),
                            Color.White * 0.05f, 600, new Vector2(Main.dungeonX * 16 + k * (900 * direction) + Main.rand.Next(15), i * 16)));
                    }
                }

            particleSystem.DrawParticles(Main.spriteBatch);
        }
    }
}