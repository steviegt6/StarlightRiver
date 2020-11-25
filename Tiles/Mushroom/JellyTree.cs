using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using StarlightRiver.Items;
using static Terraria.ModLoader.ModContent;
using Terraria.ID;
using StarlightRiver.Projectiles.Dummies;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace StarlightRiver.Tiles.Mushroom
{
    class JellyTree : DummyTile
    {
        public override int DummyType => ProjectileType<JellyTreeDummy>();

        public override bool Autoload(ref string name, ref string texture)
        {
            texture = "StarlightRiver/Invisible";
            return true;
        }

        public override void SetDefaults()
        {
            QuickBlock.QuickSetFurniture(this, 1, 1, DustType<Dusts.BlueStamina>(), 1, false, new Microsoft.Xna.Framework.Color(100, 200, 220), false, false, "Jelly Tree");
        }
    }
     class JellyTreeDummy : Dummy
    {
        public JellyTreeDummy() : base(TileType<JellyTree>(), 1 * 16, 1 * 16) { }
        bool initialized = false;
        Vector2 start;
        Vector2 c1;
        Vector2 c2;
		Vector2 mid;
		Vector2 c3;
		Vector2 c4;
        Vector2 end;
        public override void Update()
        {

			if (!initialized)
			{
				start = projectile.position + new Vector2(0, 32);
				c1 = new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-180, 0));
				c1 += start;
				c2 = new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-180, 0));
				c2 += start;
				mid = new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-180, 0));
				mid += start;
				c3 = new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-180, 0));
				c3 += mid;
				c4 = new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-180, 0));
				c4 += mid;
				end = new Vector2(Main.rand.Next(-100, 100), -200);
				end += mid;
				initialized = true;
			}
        }
        BasicEffect effect2 = new BasicEffect(Main.graphics.GraphicsDevice);
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (initialized)
            {
                float dist = (start - end).Length();
				float dist2 = (start - end).Length();
				JellyTreeHelper.DrawTwoBezier(spriteBatch, lightColor, end, mid, start, c1, c2, c3, c4, 3 / dist, 3/dist2, effect2, 0);
			}
			return false;
        }        
    }
    internal static class JellyTreeHelper
    {
        public static void DrawTwoBezier(SpriteBatch spriteBatch, Color lightColor, Vector2 endpoint, Vector2 midpoint, Vector2 startPoint, Vector2 c1, Vector2 c2, Vector2 c3, Vector2 c4, float chainsPerUse, float chainsPerUse2, BasicEffect effect2, float rotDis = 0f)
		{
			List<Vector2> points = new List<Vector2>();
			float length = (startPoint - midpoint).Length();
			for (float i = 0; i <= 1; i += chainsPerUse)
			{
				float sin = 1 + (float)Math.Sin(i * length / 10);
				float cos = 1 + (float)Math.Cos(i * length / 10);
				Color color = new Color(0.5f + cos * 0.2f, 0.8f, 0.5f + sin * 0.2f);
				Vector2 distBetween;
				float projTrueRotation;
				if (i != 0)
				{
					float x = EX(i, startPoint.X, c1.X, c2.X, midpoint.X);
					float y = WHY(i, startPoint.Y, c1.Y, c2.Y, midpoint.Y);
					distBetween = new Vector2(x -
				   EX(i - chainsPerUse, startPoint.X, c1.X, midpoint.X),
				   y -
				   WHY(i - chainsPerUse, startPoint.Y, c1.Y, midpoint.Y));
					projTrueRotation = distBetween.ToRotation() - MathHelper.PiOver2 + rotDis;
					/*Main.spriteBatch.Draw(texture, new Vector2(x - Main.screenPosition.X, y - Main.screenPosition.Y),
				   new Rectangle(0, 0, texture.Width, texture.Height), color, projTrueRotation,
				   new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), 1, SpriteEffects.None, 0);*/
					points.Add(new Vector2(x, y));
				}
			}
			length = (midpoint - endpoint).Length();
			for (float i = 0; i <= 1; i += chainsPerUse2)
			{
				float sin = 1 + (float)Math.Sin(i * length / 10);
				float cos = 1 + (float)Math.Cos(i * length / 10);
				Color color = new Color(0.5f + cos * 0.2f, 0.8f, 0.5f + sin * 0.2f);
				Vector2 distBetween;
				float projTrueRotation;
				if (i != 0)
				{
					float x = EX(i, midpoint.X, c3.X, c4.X, endpoint.X);
					float y = WHY(i, midpoint.Y, c3.Y, c4.Y, endpoint.Y);
					distBetween = new Vector2(x -
				   EX(i - chainsPerUse2, midpoint.X, c3.X, endpoint.X),
				   y -
				   WHY(i - chainsPerUse2, midpoint.Y, c3.Y, endpoint.Y));
					projTrueRotation = distBetween.ToRotation() - MathHelper.PiOver2 + rotDis;
					/*Main.spriteBatch.Draw(texture, new Vector2(x - Main.screenPosition.X, y - Main.screenPosition.Y),
				   new Rectangle(0, 0, texture.Width, texture.Height), color, projTrueRotation,
				   new Vector2(texture.Width * 0.5f, texture.Height * 0.5f), 1, SpriteEffects.None, 0);*/
					points.Add(new Vector2(x, y));
				}
			}
			DrawPrims(points, effect2);
		}
		public static void DrawPrims(List<Vector2> points, BasicEffect effect2)
        {
			//Effect effect = mod.GetEffect("Effects/trailShaders");

			effect2.VertexColorEnabled = true;
			VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[points.Count * 6];
			int currentIndex = 0;
			float lerper = 1;
			int Cap = points.Count * 50;
			void AddVertex(Vector2 position, Color color, Vector2 uv)
			{
				if (currentIndex < vertices.Length)
					vertices[currentIndex++] = new VertexPositionColorTexture(new Vector3(position - Main.screenPosition, 0f), color, uv);
			}
			void PrepareBasicShader()
			{
				int width2 = Main.graphics.GraphicsDevice.Viewport.Width;
				int height = Main.graphics.GraphicsDevice.Viewport.Height;
				Vector2 zoom = Main.GameViewMatrix.Zoom;
				Matrix view = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up) * Matrix.CreateTranslation(width2 / 2, height / -2, 0) * Matrix.CreateRotationZ(MathHelper.Pi) * Matrix.CreateScale(zoom.X, zoom.Y, 1f);
				Matrix projection = Matrix.CreateOrthographic(width2, height, 0, 1000);
				effect2.View = view;
				effect2.Projection = projection;
				foreach (EffectPass pass in effect2.CurrentTechnique.Passes)
				{
					pass.Apply();
				}
			}
			float width = 8;
			float alphaValue = 1f;
			for (int i = 0; i < points.Count; i++)
			{
				if (i == 0)
				{
					Color c = Color.Cyan;
					Vector2 normalAhead = CurveNormal(points, i + 1);
					Vector2 secondUp = points[i + 1] - normalAhead * width;
					Vector2 secondDown = points[i + 1] + normalAhead * width;
					AddVertex(points[i], c * alphaValue, new Vector2((float)Math.Sin(lerper / 20f), (float)Math.Sin(lerper / 20f)));
					AddVertex(secondUp, c * alphaValue, new Vector2((float)Math.Sin(lerper / 20f), (float)Math.Sin(lerper / 20f)));
					AddVertex(secondDown, c * alphaValue, new Vector2((float)Math.Sin(lerper / 20f), (float)Math.Sin(lerper / 20f)));
				}
				else
				{
					if (i != points.Count - 1)
					{
						Color c = Color.Cyan;
						Vector2 normal = CurveNormal(points, i);
						Vector2 normalAhead = CurveNormal(points, i + 1);
						float j = (Cap + ((float)(Math.Sin(lerper / 10f)) * 1) - i * 0.1f) / Cap;
						width *= j;
						Vector2 firstUp = points[i] - normal * width;
						Vector2 firstDown = points[i] + normal * width;
						Vector2 secondUp = points[i + 1] - normalAhead * width;
						Vector2 secondDown = points[i + 1] + normalAhead * width;

						AddVertex(firstUp, c * alphaValue, new Vector2(1));
						AddVertex(secondDown, c * alphaValue, new Vector2(0));
						AddVertex(firstDown, c * alphaValue, new Vector2(0));


						AddVertex(secondUp, c * alphaValue, new Vector2(1));
						AddVertex(secondDown, c * alphaValue, new Vector2(0));
						AddVertex(firstUp, c * alphaValue, new Vector2(0));
					}
					else
					{

					}
				}
			}
			PrepareBasicShader();
			Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, vertices, 0, points.Count * 2);
		}

		private static Vector2 CurveNormal(List<Vector2> points, int index)
		{
			if (points.Count == 1) return points[0];

			if (index == 0)
			{
				return Clockwise90(Vector2.Normalize(points[1] - points[0]));
			}
			if (index == points.Count - 1)
			{
				return Clockwise90(Vector2.Normalize(points[index] - points[index - 1]));
			}
			return Clockwise90(Vector2.Normalize(points[index + 1] - points[index - 1]));
		}

		private static Vector2 Clockwise90(Vector2 vector)
		{
			return new Vector2(-vector.Y, vector.X);
		}
		#region os's shit
		public static float EX(float t,
		float x0, float x1, float x2, float x3)
		{
			return (float)(
				x0 * Math.Pow(1 - t, 3) +
				x1 * 3 * t * Math.Pow(1 - t, 2) +
				x2 * 3 * Math.Pow(t, 2) * (1 - t) +
				x3 * Math.Pow(t, 3)
			);
		}

		public static float WHY(float t,
			float y0, float y1, float y2, float y3)
		{
			return (float)(
				 y0 * Math.Pow(1 - t, 3) +
				 y1 * 3 * t * Math.Pow(1 - t, 2) +
				 y2 * 3 * Math.Pow(t, 2) * (1 - t) +
				 y3 * Math.Pow(t, 3)
			 );
		}
		public static float EX(float t,
   float x0, float x1, float x2)
		{
			return (float)(
				x0 * Math.Pow(1 - t, 2) +
				x1 * 2 * t * (1 - t) +
				x2 * Math.Pow(t, 2)
			);
		}

		public static float WHY(float t,
			float y0, float y1, float y2)
		{
			return (float)(
				y0 * Math.Pow(1 - t, 2) +
				y1 * 2 * t * (1 - t) +
				y2 * Math.Pow(t, 2)
			);
		}
		#endregion

	}
    class JellyTreeItem : QuickTileItem
    {
        public override string Texture => "StarlightRiver/MarioCumming";

        public JellyTreeItem() : base("Jelly Tree", "dab", TileType<JellyTree>(), 0) { }
    }
}
