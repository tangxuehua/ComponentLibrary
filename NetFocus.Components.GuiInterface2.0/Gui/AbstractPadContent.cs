
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NetFocus.Components.GuiInterface.Gui
{
	public abstract class AbstractPadContent : IPadContent
	{
		string title;
		string[] shortcut = null;
		public abstract Control Control {
			get;
		}
		
		public virtual string Title {
			get {
				return title;
			}
		}

		public string[] Shortcut 
		{
			get 
			{
				return shortcut;
			}
			set 
			{
				shortcut = value;
			}
		}
		
		public virtual void Dispose()
		{
		
		}

		public AbstractPadContent(string title)
		{
			this.title = title;
		}
		
		protected virtual void OnTitleChanged(EventArgs e)
		{
			if (TitleChanged != null) {
				TitleChanged(this, e);
			}
		}
		
		public event EventHandler TitleChanged;


	}
}
