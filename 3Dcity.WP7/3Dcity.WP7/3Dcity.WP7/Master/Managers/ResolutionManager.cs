using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame.Master.Managers
{
	// http://www.david-amador.com/2010/03/xna-2d-independent-resolution-rendering.
	public interface IResolutionManager
	{
		void Initialize();
		void LoadContent(Boolean isFullScreen, UInt16 screenWide, UInt16 screenHigh, Boolean useExposed, UInt16 exposeWide, UInt16 exposeHigh);
		void BeginDraw(Color color);
		void ApplyFullScreen(Boolean fullScreen);

		Matrix TransformationMatrix { get; }
		Matrix InvertTransformationMatrix { get; }
		Vector2 ViewPortVector2 { get; }
	}

	public class ResolutionManager : IResolutionManager
	{
		private GraphicsDeviceManager _Device;
		private Int32 _Width, _Height;
		private Int32 _VWidth, _VHeight;
		private Boolean _FullScreen;

		private Int32 displayModeWidth, displayModeHeight;
		private Viewport fullViewport, showViewport;

		public void Initialize()
		{
			_Device = Engine.GraphicsDeviceManager;
			_Width = _Device.PreferredBackBufferWidth;
			_Height = _Device.PreferredBackBufferHeight;

			displayModeWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			displayModeHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			SwapDisplayMode(ref displayModeWidth, ref displayModeHeight);
		}

		public void LoadContent(Boolean isFullScreen, UInt16 screenWide, UInt16 screenHigh, Boolean useExposed, UInt16 exposeWide, UInt16 exposeHigh)
		{
			_FullScreen = isFullScreen;
			_VWidth = screenWide;
			_VHeight = screenHigh;

			if (useExposed)
			{
				_Width = exposeWide;
				_Height = exposeHigh;
			}
			else
			{
				_Width = displayModeWidth;
				_Height = displayModeHeight;
			}

			ApplyResolutionSettings();
			fullViewport = GetFullViewport();
			showViewport = GetShowViewport();

			TransformationMatrix = Matrix.CreateScale((float)showViewport.Width / _VWidth, (float)showViewport.Width / _VWidth, 1f);
			InvertTransformationMatrix = Matrix.Invert(TransformationMatrix);
			ViewPortVector2 = new Vector2(showViewport.X, showViewport.Y);
		}

		public void BeginDraw(Color color)
		{
			// Start by reseting viewport to (0,0,1,1)
			_Device.GraphicsDevice.Viewport = fullViewport;

			// Clear to Black
			_Device.GraphicsDevice.Clear(color);

			// Calculate Proper Viewport according to Aspect Ratio
			_Device.GraphicsDevice.Viewport = showViewport;

			// and clear that
			// This way we are gonna have black bars if aspect ratio requires it and
			// the clear color on the rest
			_Device.GraphicsDevice.Clear(color);
		}

		public void ApplyFullScreen(Boolean fullScreen)
		{
			_FullScreen = fullScreen;
			ApplyResolutionSettings();
		}

		public Matrix TransformationMatrix { get; private set; }
		public Matrix InvertTransformationMatrix { get; private set; }
		public Vector2 ViewPortVector2 { get; private set; }

		private void ApplyResolutionSettings()
		{
			if (!_FullScreen)
			{
				if (_Width <= displayModeWidth && _Height <= displayModeHeight)
				{
					_Device.PreferredBackBufferWidth = _Width;
					_Device.PreferredBackBufferHeight = _Height;
					_Device.IsFullScreen = _FullScreen;
					_Device.ApplyChanges();
				}
			}
			else
			{
				// If we are using full screen mode, we should check to make sure that the display
				// adapter can handle the video mode we are trying to set.  To do this, we will
				// iterate through the display modes supported by the adapter and check them against
				// the mode we want to set.
				DisplayModeCollection collection = GraphicsAdapter.DefaultAdapter.SupportedDisplayModes;
				if (GraphicsAdapter.DefaultAdapter.SupportedDisplayModes.Any(dm => (dm.Width == _Width) && (dm.Height == _Height)))
				{
					_Device.PreferredBackBufferWidth = _Width;
					_Device.PreferredBackBufferHeight = _Height;
					_Device.IsFullScreen = _FullScreen;
					_Device.ApplyChanges();
				}
			}

			_Width = _Device.PreferredBackBufferWidth;
			_Height = _Device.PreferredBackBufferHeight;
		}

		private Viewport GetFullViewport()
		{
			Viewport vp = new Viewport();
			vp.X = vp.Y = 0;
			vp.Width = _Width;
			vp.Height = _Height;

			return vp;
		}

		private Viewport GetShowViewport()
		{
			Single targetAspectRatio = (float)_VWidth / (float)_VHeight;

			// figure out the largest area that fits in this resolution at the desired aspect ratio
			Int32 width = _Device.PreferredBackBufferWidth;
			Int32 height = (Int32)(width / targetAspectRatio + .5f);

			if (height > _Device.PreferredBackBufferHeight)
			{
				height = _Device.PreferredBackBufferHeight;
				// PillarBox
				width = (Int32)(height * targetAspectRatio + .5f);
			}

			Viewport viewport = new Viewport
			{
				X = (_Device.PreferredBackBufferWidth / 2) - (width / 2),
				Y = (_Device.PreferredBackBufferHeight / 2) - (height / 2),
				Width = width,
				Height = height,
				MinDepth = 0,
				MaxDepth = 1
			};

			return viewport;

		}

		private static void SwapDisplayMode(ref Int32 displayModeWidth, ref Int32 displayModeHeight)
		{
			if (displayModeWidth > displayModeHeight)
			{
				return;
			}

			Int32 temp = displayModeWidth;
			displayModeWidth = displayModeHeight;
			displayModeHeight = temp;
		}

	}
}