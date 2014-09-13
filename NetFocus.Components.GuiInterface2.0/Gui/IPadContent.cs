using System;
using System.Drawing;
using System.Windows.Forms;


namespace NetFocus.Components.GuiInterface.Gui 
{
	/// <summary>
	/// The IPadContent interface is the basic interface to all "tool" windows in DataStructure
	/// </summary>
	public interface IPadContent : IDisposable
	{
		/// <summary>
		/// Returns or Set the title of the pad.
		/// </summary>
		string Title {
			get;
		}

		/// <summary>
		/// Returns the menu shortcut for the view menu item.
		/// </summary>
		string[] Shortcut  
		{
			get;
			set;
		}
		
		/// <summary>
		/// Returns the Windows.Control for this pad.
		/// </summary>
		Control Control {
			get;
		}
		
		/// <summary>
		/// Is called when the title of this pad has changed.
		/// </summary>
		event EventHandler TitleChanged;


	}
}
