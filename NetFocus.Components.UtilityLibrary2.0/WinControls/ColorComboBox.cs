using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;
using NetFocus.Components.UtilityLibrary.Win32;
using NetFocus.Components.UtilityLibrary.General;
using NetFocus.Components.UtilityLibrary.Menus;

namespace NetFocus.Components.UtilityLibrary.WinControls
{
	/// <summary>
	/// Summary description for ColorComboBox.
	/// </summary>
	public class ColorComboBox : ComboBoxBase
	{

		private const int PREVIEW_BOX_WIDTH = 20;
		
		// For use when hosted by a toolbar
		public ColorComboBox(bool toolBarUse) : base(toolBarUse)
		{
			DropDownStyle = ComboBoxStyle.DropDownList;
			Items.AddRange(ColorUtil.KnownColorNames);
		}
		public ColorComboBox()
		{
			DropDownStyle = ComboBoxStyle.DropDownList;
			Items.AddRange(ColorUtil.KnownColorNames);
		}

		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			
			// Call base class to do the "Flat ComboBox" drawing
			base.OnDrawItem(e);
			
			// Draw text
			Graphics g = e.Graphics;
			Rectangle bounds = e.Bounds;
			bool selected = (e.State & DrawItemState.Selected) > 0;
			bool editSel = (e.State & DrawItemState.ComboBoxEdit ) > 0;
			if ( e.Index != -1 )
				DrawComboBoxItem(g, bounds, e.Index, selected, editSel);
		
		}
		
		protected override void DrawComboBoxItem(Graphics g, Rectangle bounds, int Index, bool selected, bool editSel)
		{
			
			// Call base class to do the "Flat ComboBox" drawing
			base.DrawComboBoxItem(g, bounds, Index, selected, editSel);
			if ( Index != -1)
			{
				string item = Items[Index].ToString();
				Color currentColor = Color.FromName(item);

				Brush brush;
				if ( selected && editSel) 
					brush =  new SolidBrush(SystemColors.MenuText);
				else if ( selected )
					brush =  new SolidBrush(SystemColors.HighlightText);
				else
					brush = new SolidBrush(SystemColors.MenuText);
				
				g.FillRectangle(new SolidBrush(currentColor), bounds.Left+2, bounds.Top+2, PREVIEW_BOX_WIDTH , bounds.Height-4);
				Pen blackPen = new Pen(new SolidBrush(Color.Black), 1);
				g.DrawRectangle(blackPen, new Rectangle(bounds.Left+1, bounds.Top+1, PREVIEW_BOX_WIDTH+1, bounds.Height-3));

                Size textSize = TextUtil.GetTextSize(g, Items[Index].ToString(), Font);
				int top = bounds.Top + (bounds.Height - textSize.Height)/2;
				g.DrawString(item, Font, brush, new Point(bounds.Left + 28, top));

			}
		
		}

		protected override void DrawComboBoxItemEx(Graphics g, Rectangle bounds, int Index, bool selected, bool editSel)
		{
			
			// This "hack" is necessary to avoid a clipping bug that comes from the fact that sometimes
			// we are drawing using the Graphics object for the edit control in the combobox and sometimes
			// we are using the graphics object for the combobox itself. If we use the same function to do our custom
			// drawing it is hard to adjust for the clipping because of what was said about
			base.DrawComboBoxItemEx(g, bounds, Index, selected, editSel);
			if ( Index != -1)
			{
				string item = Items[Index].ToString();
				Color currentColor = Color.FromName(item);
				SolidBrush brush;

				if ( selected && editSel) 
					brush =  new SolidBrush(SystemColors.MenuText);
				else if ( selected )
					brush =  new SolidBrush(SystemColors.HighlightText);
				else
					brush = new SolidBrush(SystemColors.MenuText);

				Rectangle rc = bounds;
				rc.Inflate(-3, -3);
				g.FillRectangle(new SolidBrush(currentColor), rc.Left+2, rc.Top+2, PREVIEW_BOX_WIDTH , rc.Height-4);
				Pen blackPen = new Pen(new SolidBrush(Color.Black), 1);
				g.DrawRectangle(blackPen, new Rectangle(rc.Left+1, rc.Top+1, PREVIEW_BOX_WIDTH+1, rc.Height-3));

				Size textSize = TextUtil.GetTextSize(g, Items[Index].ToString(), Font);
				int top = bounds.Top + (bounds.Height - textSize.Height)/2;

				// Clipping rectangle
				Rectangle clipRect = new Rectangle(bounds.Left + 31, top, bounds.Width - 31 - ARROW_WIDTH - 4, top+textSize.Height);
                g.DrawString(Items[Index].ToString(), Font, brush, clipRect);
			
			}
		}


	}


}
