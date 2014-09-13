
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace NetFocus.Components.UtilityLibrary.Menus
{
	public class MenuControlDesigner :  System.Windows.Forms.Design.ParentControlDesigner
	{
		public override ICollection AssociatedComponents
		{
			get 
			{
				if (base.Control is NetFocus.Components.UtilityLibrary.Menus.MenuControl)
					return ((NetFocus.Components.UtilityLibrary.Menus.MenuControl)base.Control).MenuCommands;
				else
					return base.AssociatedComponents;
			}
		}

		protected override bool DrawGrid
		{
			get { return false; }
		}
	}
}
