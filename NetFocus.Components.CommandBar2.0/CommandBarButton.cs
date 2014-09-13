
namespace NetFocus.Components.CommandBar
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	public class CommandBarButton : CommandBarButtonBase
	{
		public CommandBarButton(string text) : base(text)
		{
		}

		public CommandBarButton(string text, EventHandler clickHandler) : base(text)
		{
			this.Click += clickHandler;
		}

		public CommandBarButton(string text, EventHandler clickHandler, Keys shortcut) : base(text)
		{
			this.Click += clickHandler;
			this.Shortcut = shortcut;
		}

		public CommandBarButton(Image image, string text, EventHandler clickHandler) : base(image, text)
		{
			this.Click += clickHandler;
		}

		public CommandBarButton(Image image, string text, EventHandler clickHandler, Keys shortcut) : base(image, text)
		{
			this.Click += clickHandler;
			this.Shortcut = shortcut;
		}

		public override string ToString()
		{
			return "Button(" + this.Text + ")";
		}
	}
}
