
namespace NetFocus.Components.CommandBar
{
	using System;

	public class CommandBarMenu : CommandBarItem
	{
		public event EventHandler DropDown;
		private CommandBarItemCollection items = new CommandBarItemCollection();

		public CommandBarMenu(string text) : base(text)
		{
		}

		public CommandBarItemCollection Items
		{
			get { return this.items; }
		}

		protected virtual void OnDropDown(EventArgs e)
		{
			NotifySelect(e);
			if (this.DropDown != null)
			{
				this.DropDown(this, e);
			}
		}

		internal void PerformDropDown(EventArgs e)
		{
			this.OnDropDown(e);
		}
	}
}
