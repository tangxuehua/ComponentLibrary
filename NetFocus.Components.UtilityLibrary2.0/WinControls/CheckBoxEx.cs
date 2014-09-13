using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using NetFocus.Components.UtilityLibrary.General;
using NetFocus.Components.UtilityLibrary.Win32;

namespace NetFocus.Components.UtilityLibrary.WinControls
{
	
	/// <summary>
	/// Summary description for CheckBoxEx.
	/// </summary>
	public class CheckBoxEx : System.Windows.Forms.CheckBox
	{
		
		const int CHECK_MARK_SIZE = 11;
		DrawState drawState = DrawState.Normal;
		Bitmap checkMarkChecked = null;
		Bitmap checkMarkUnchecked = null;
		Bitmap checkMarkHotChecked = null;
		Bitmap checkMarkHotUnchecked = null;
		Bitmap checkMarkDisableChecked = null;
		Bitmap checkMarkDisableUnchecked = null;
		Color oldHighLightColor = Color.Empty;
		Color oldControlDarkColor = Color.Empty;
		
		public CheckBoxEx()
		{
			// Our control needs to have Flat style set
			FlatStyle = FlatStyle.Flat;
			// Load checkmark bitmap
			checkMarkChecked = ResourceUtil.LoadBitmapResource(Type.GetType("NetFocus.Components.UtilityLibrary.WinControls.CheckBoxEx"),
				                                "Resources.Controls", "CheckMarkChecked");
			Debug.Assert(checkMarkChecked != null);
			checkMarkUnchecked = ResourceUtil.LoadBitmapResource(Type.GetType("NetFocus.Components.UtilityLibrary.WinControls.CheckBoxEx"),
				                                "Resources.Controls", "CheckMarkUnchecked");
			Debug.Assert(checkMarkUnchecked != null);
		}

		public new FlatStyle FlatStyle 
		{
			// Don't let the user change this property
			// to anything other than flat, otherwise
			// there would be painting problems
			get { return base.FlatStyle; } 
			set 
			{
				if ( value != FlatStyle.Flat )
				{
					// Throw an exception to tell the user
					// that this property needs to be "Flat" 
					// if he is to use this class
					string message = "FlatStyle needs to be set to Flat for this class";
					ArgumentException argumentException = new ArgumentException("FlatStyle", message);
					throw(argumentException);
				}
				else 
					base.FlatStyle = value;
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			// Set state to hot
			base.OnMouseEnter(e);
			drawState = DrawState.Hot;
			Invalidate();
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			// Set state to Normal
			base.OnMouseLeave(e);
			if ( !ContainsFocus )
			{
				drawState = DrawState.Normal;
				Invalidate();
			}
		}
      
		protected override void OnGotFocus(EventArgs e)
		{
			// Set state to Hot
			base.OnGotFocus(e);
			drawState = DrawState.Hot;
			Invalidate();
		}
        
		protected override void OnLostFocus(EventArgs e)
		{
			// Set state to Normal
			base.OnLostFocus(e);
			drawState = DrawState.Normal;
			Invalidate();
		}

		void DrawCheckBoxState(DrawState state)
		{
			// In case system colors has changed
			FilterImages();
			Rectangle rect = ClientRectangle;

			// Create DC for the whole edit window instead of just for the client area
			IntPtr hDC = WindowsAPI.GetDC(Handle);
			Rectangle checkMark = new Rectangle(rect.Left, rect.Top + (rect.Height-CHECK_MARK_SIZE)/2, 
				CHECK_MARK_SIZE, CHECK_MARK_SIZE);
			
			using (Graphics g = Graphics.FromHdc(hDC))
			{
				if ( state == DrawState.Normal )
				{
					// Draw normal black circle
					if ( Checked ) 
						g.DrawImage(checkMarkChecked, checkMark.Left, checkMark.Top);
					else
						g.DrawImage(checkMarkUnchecked, checkMark.Left, checkMark.Top);
				}
				else if ( state == DrawState.Hot )
				{
					if ( Checked ) 
						g.DrawImage(checkMarkHotChecked, checkMark.Left, checkMark.Top);
					else
						g.DrawImage(checkMarkHotUnchecked, checkMark.Left, checkMark.Top);
					
				}
				else if ( state == DrawState.Disable )
				{
					if ( Checked ) 
						g.DrawImage(checkMarkDisableChecked, checkMark.Left, checkMark.Top);
					else
						g.DrawImage(checkMarkDisableUnchecked, checkMark.Left, checkMark.Top);
				}
			}

			// Release DC
			WindowsAPI.ReleaseDC(Handle, hDC);
        }

		void FilterImages()
		{
			// Filter images only if the color information has
			// changed or if this is the first time we are getting it
			if ( oldHighLightColor == Color.Empty ||
				!(oldHighLightColor.R == SystemColors.Highlight.R && oldHighLightColor.G == SystemColors.Highlight.G
						 && oldHighLightColor.B == SystemColors.Highlight.B ) )
			{
				oldHighLightColor = SystemColors.Highlight;
				checkMarkHotChecked = DoFiltering(checkMarkChecked, Color.Black, SystemColors.Highlight);
				checkMarkHotUnchecked = DoFiltering(checkMarkUnchecked, Color.Black, SystemColors.Highlight);
 			}

			if ( oldControlDarkColor == Color.Empty ||
				!(oldHighLightColor.R == SystemColors.Highlight.R && oldHighLightColor.G == SystemColors.Highlight.G
				&& oldHighLightColor.B == SystemColors.Highlight.B ) )
			{
				oldControlDarkColor = SystemColors.ControlDark;
				checkMarkDisableChecked = DoFiltering(checkMarkChecked, Color.Black, SystemColors.ControlDark);
				checkMarkDisableUnchecked = DoFiltering(checkMarkUnchecked, Color.Black, SystemColors.ControlDark);
			}
		}


		Bitmap DoFiltering(Bitmap bitmap, Color oldColor, Color newColor)
		{
			Bitmap copy = (Bitmap)bitmap.Clone();
			for ( int i = 0; i < bitmap.Width; i++ )
			{
				for ( int j = 0; j < bitmap.Height; j++ )
				{
					Color currentPixel = copy.GetPixel(i, j);
					// Compare just RGB portion
					if ( currentPixel.R == oldColor.R && currentPixel.G == oldColor.G
						 && currentPixel.B == oldColor.B)
						copy.SetPixel(i, j, newColor);
				}
			}
			return copy;
		}

		protected override  void WndProc(ref Message m)
		{
			bool callBase = true;
						
			switch(m.Msg)
			{
				case ((int)Msg.WM_PAINT):
				{
					// Let the edit control do its painting
					base.WndProc(ref m);
					// Now do our custom painting
					DrawCheckBoxState(Enabled?drawState:DrawState.Disable);
					callBase = false;
				}
					break;
				default:
					break;
			}

			if ( callBase )
				base.WndProc(ref m);
		
		}

		
	}
}
