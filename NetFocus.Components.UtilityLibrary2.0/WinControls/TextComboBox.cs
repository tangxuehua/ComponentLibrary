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
	/// Summary description for TextComboBox.
	/// </summary>
	public class TextComboBox : ComboBoxBase
	{
		
		// For use when hosted by a toolbar
		public TextComboBox(bool toolBarUse) : base(toolBarUse)
		{
			// Override parent, we don't want to do all the painting ourselves
			// since we want to let the edit control deal with the text for editing
			// the parent class ComboBoxBase knows to do the right stuff with 
			// non-editable comboboxes as well as editable comboboxes as long
			// as we change these flags below
			SetStyle(ControlStyles.AllPaintingInWmPaint
				|ControlStyles.UserPaint|ControlStyles.Opaque, false);
		}		
		
		public TextComboBox()
		{
			// Override parent, we don't want to do all the painting ourselves
			// since we want to let the edit control deal with the text for editing
			// the parent class ComboBoxBase knows to do the right stuff with 
			// non-editable comboboxes as well as editable comboboxes as long
			// as we change these flags below
			SetStyle(ControlStyles.AllPaintingInWmPaint
				|ControlStyles.UserPaint|ControlStyles.Opaque, false);
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
				SolidBrush brush;
				if ( selected && editSel) 
					brush =  new SolidBrush(SystemColors.MenuText);
				else if ( selected )
					brush =  new SolidBrush(SystemColors.HighlightText);
				else
					brush = new SolidBrush(SystemColors.MenuText);

				Size textSize = TextUtil.GetTextSize(g, Items[Index].ToString(), Font);
				int top = bounds.Top + (bounds.Height - textSize.Height)/2;
				g.DrawString(Items[Index].ToString(), Font, brush, new Point(bounds.Left + 1, top));
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
				SolidBrush brush;
				if ( selected && editSel) 
					brush =  new SolidBrush(SystemColors.MenuText);
				else if ( selected )
					brush =  new SolidBrush(SystemColors.HighlightText);
				else
					brush = new SolidBrush(SystemColors.MenuText);

				Size textSize = TextUtil.GetTextSize(g, Items[Index].ToString(), Font);
				int top = bounds.Top + (bounds.Height - textSize.Height)/2;
				// Clipping rectangle
				Rectangle clipRect = new Rectangle(bounds.Left + 4, top, bounds.Width - ARROW_WIDTH - 4, top+textSize.Height);
				g.DrawString(Items[Index].ToString(), Font, brush, clipRect);
			}
		}
		
	}
}
