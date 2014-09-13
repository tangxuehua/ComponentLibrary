using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace NetFocus.Components.UtilityLibrary.WinControls 
{
	/// <summary>
	/// Summary description for ColorListBox.
	/// </summary>
	public class ColorListBox : System.Windows.Forms.ListBox
	{
		
		string[] colorArray = null;
		public ColorListBox()
		{
			DrawMode = DrawMode.OwnerDrawFixed;
			ItemHeight = ItemHeight + 1;
		}

		public String[] ColorArray
		{
			get
			{
				return colorArray;
			}
			set
			{
				colorArray = value;
				Items.AddRange(value);
			}
		}
        
		protected override void OnPaint(PaintEventArgs pe)
		{
			base.OnPaint(pe);
		}

		protected override void OnDrawItem(DrawItemEventArgs e)
		{
			
			Graphics g = e.Graphics;
			Rectangle bounds = e.Bounds;
			bool selected = (e.State & DrawItemState.Selected) > 0;
			bool editSel = (e.State & DrawItemState.ComboBoxEdit ) > 0;
			if ( e.Index != -1 )
				DrawListBoxItem(g, bounds, e.Index, selected, editSel);
			
		}

		protected void DrawListBoxItem(Graphics g, Rectangle bounds, int Index, bool selected, bool editSel)
		{
			// Draw List box item
			if ( Index != -1)
			{
				if ( selected && !editSel)
					// Draw highlight rectangle
					g.FillRectangle(new SolidBrush(SystemColors.Highlight), bounds.Left, bounds.Top, bounds.Width, bounds.Height);
				else
					// Erase highlight rectangle
					g.FillRectangle(new SolidBrush(SystemColors.Window), bounds.Left, bounds.Top, bounds.Width, bounds.Height);
				
				string item = (string)Items[Index];
				Color currentColor = Color.FromName(item);

				Brush brush;
				if ( selected )
					brush =  new SolidBrush(SystemColors.HighlightText);
				else
					brush = new SolidBrush(SystemColors.MenuText);
				
				g.FillRectangle(new SolidBrush(currentColor), bounds.Left+2, bounds.Top+2, 20, bounds.Height-4);
				Pen blackPen = new Pen(new SolidBrush(Color.Black), 1);
				g.DrawRectangle(blackPen, new Rectangle(bounds.Left+1, bounds.Top+1, 21, bounds.Height-3));
				g.DrawString(item, SystemInformation.MenuFont, brush, new Point(bounds.Left + 28, bounds.Top));
				
			}
		}

	}
}
