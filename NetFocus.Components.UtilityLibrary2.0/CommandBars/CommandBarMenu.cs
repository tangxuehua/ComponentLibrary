using System;
using System.Windows.Forms;
using System.Drawing;
using NetFocus.Components.UtilityLibrary.Menus;

namespace NetFocus.Components.UtilityLibrary.CommandBars
{
	/// <summary>
	/// Summary description for PopopMenu.
	/// </summary>
	public class CommandBarMenu : ContextMenu
	{
		// This is just to keep track of the selected
		// menu as well as hold the menuitems in the menubar
		Menu selectedMenuItem = null;
		
		public CommandBarMenu(MenuItem[] items) : base(items)
		{

		}
		
		internal Menu SelectedMenuItem
		{
			set { selectedMenuItem = value; }
			get { return selectedMenuItem; }
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			return base.ProcessCmdKey(ref msg, keyData);
		}

		
	}
}
