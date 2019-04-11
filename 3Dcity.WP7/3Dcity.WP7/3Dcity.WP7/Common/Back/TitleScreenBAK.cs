using System;
using Microsoft.Xna.Framework;
using WindowsGame.Common.Screens;
using WindowsGame.Common.Static;
using WindowsGame.Master.Interfaces;

namespace WindowsGame.Common.Back
{
	public class TitleScreenBAK : BaseScreen, IScreen
	{
		public override void Initialize()
		{
			base.Initialize();
			LoadTextData();
		}

		public override void LoadContent()
		{
			// Not bad settings for default.
			MyGame.Manager.BulletManager.Reset(10, 200, 100);
			base.LoadContent();
		}

		public override Int32 Update(GameTime gameTime)
		{
			base.Update(gameTime);
			if (GamePause)
			{
				return (Int32)CurrScreen;
			}

			// Log delta to monitor performance!
#if DEBUG
			MyGame.Manager.Logger.Info(gameTime.ElapsedGameTime.TotalSeconds.ToString());
#endif

			MyGame.Manager.CollisionManager.ClearCollisionList();

			// Move target unconditionally.
			Single horz = MyGame.Manager.InputManager.Horizontal();
			Single vert = MyGame.Manager.InputManager.Vertical();
			MyGame.Manager.SpriteManager.SetMovement(horz, vert);
			MyGame.Manager.SpriteManager.Update(gameTime);

			Boolean fire = MyGame.Manager.InputManager.Fire();
			if (fire)
			{
				SByte bulletIndex = MyGame.Manager.BulletManager.CheckBullets();
				if (Constants.INVALID_INDEX != bulletIndex)
				{
					Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
					MyGame.Manager.BulletManager.Shoot((Byte)bulletIndex, position);
				}
			}

			// Then bullet and target second.
			MyGame.Manager.BulletManager.Update(gameTime);
			if (MyGame.Manager.CollisionManager.BulletCollisionList.Count > 0)
			{
				// Check collisions here.
			}

			// Update fire icon.
			Byte fireIcon = Convert.ToByte(!MyGame.Manager.BulletManager.CanShoot);
			MyGame.Manager.IconManager.UpdateIcon(MyGame.Manager.IconManager.JoyButton, fireIcon);

			return (Int32)CurrScreen;
		}

		public override void Draw()
		{
			// Sprite sheet #01.
			base.Draw();
			MyGame.Manager.IconManager.DrawControls();


			// Sprite sheet #02.


			// Then bullet and target second.
			MyGame.Manager.BulletManager.Draw();
			MyGame.Manager.SpriteManager.Draw();

			MyGame.Manager.TextManager.Draw(TextDataList);
		}

	}
	//public class TitleScreenBAK : BaseScreen, IScreen
	//{
	//    private Rectangle[] rectangles;
	//    private Vector2[] positions;
	//    private int ex, ey;
	//    private Byte index;
	//    private Single delay;
	//    private Vector2 pos1, pos2, pos3, pos4;

	//    public override void Initialize()
	//    {
	//        base.Initialize();
	//        LoadTextData();

	//        rectangles = MyGame.Manager.ImageManager.EnemyRectangles;
	//        ex = 100;
	//        ey = 100;
	//        positions = GetPositions(ex, ey);
	//        index = MyGame.Manager.ConfigManager.GlobalConfigData.EnemyIndex;
	//        delay = 1200.0f;
	//    }

	//    public override void LoadContent()
	//    {
	//        pos1 = new Vector2(0, 0);
	//        pos2 = new Vector2(0, 80);
	//        pos3 = new Vector2(0, 240);
	//        pos4 = new Vector2(0, 480);

	//        //MyGame.Manager.SoundManager.PlayMusic();
	//        base.LoadContent();
	//    }

	//    public override Int32 Update(GameTime gameTime)
	//    {
	//        base.Update(gameTime);
	//        if (GamePause)
	//        {
	//            return (Int32)CurrScreen;
	//        }

	//        Single horz = MyGame.Manager.InputManager.Horizontal();
	//        Single vert = MyGame.Manager.InputManager.Vertical();

	//        Boolean fire = MyGame.Manager.InputManager.Fire();
	//        if (fire)
	//        {
	//            Vector2 position = MyGame.Manager.SpriteManager.LargeTarget.Position;
	//            MyGame.Manager.BulletManager.Shoot(1, position);
	//        }

	//        Byte myIndex = Convert.ToByte(fire);
	//        MyGame.Manager.IconManager.UpdateIcon(MyGame.Manager.IconManager.JoyButton, myIndex);

	//        // Enemy first.
	//        UpdateTimer(gameTime);
	//        if (Timer > delay)
	//        {
	//            Timer = 0;
	//            index++;
	//            if (index >= Constants.MAX_ENEMYS_SPAWN)
	//            {
	//                index = 0;
	//            }
	//        }

	//        // Then bullet and target second.
	//        MyGame.Manager.BulletManager.Update(gameTime);


	//        MyGame.Manager.SpriteManager.SetMovement(horz, vert);
	//        MyGame.Manager.SpriteManager.Update(gameTime);

	//        return (Int32)CurrScreen;
	//    }

	//    public override void Draw()
	//    {
	//        // Sprite sheet #01.
	//        //base.Draw();
	//        //MyGame.Manager.IconManager.DrawControls();

	//        //MyGame.Manager.TextManager.Draw(TextDataList);

	//        // Sprite sheet #02.

	//        // Enemy first.
	//        Rectangle rectangle = rectangles[index];
	//        var position = positions[index];
	//        Engine.SpriteBatch.Draw(Assets.SpriteSheet02Texture, position, rectangle, Color.White);


	//        // Then bullet and target second.
	//        MyGame.Manager.BulletManager.Draw();
	//        MyGame.Manager.SpriteManager.Draw();
	//    }

	//    private Vector2[] GetPositions(int sx, int sy)
	//    {
	//        positions = positions = new Vector2[Constants.MAX_ENEMYS_SPAWN];
	//        for (Byte loop = 0; loop < Constants.MAX_ENEMYS_SPAWN; loop++)
	//        {
	//            positions[loop] = new Vector2(sx, sy);
	//        }

	//        return positions;
	//    }
	//    private static Vector2 GetPosition(Byte off, int tx, int ty)
	//    {
	//        return new Vector2((120 - off) / 2.0f + tx, (120 - off) / 2.0f + ty);
	//    }

	//}
}

