using System;
using System.Text;
using System.Drawing;
using NetFocus.Components.TextEditor.Document;

namespace NetFocus.Components.TextEditor
{

	
	public class DefaultTextEditorProperties : ITextEditorProperties
	{

		public DefaultTextEditorProperties()
		{

			FontContainer.DefaultFont = new Font("Courier New", 10);
		}
		
		public int TabIndent 
		{
			get 
			{
				return 4;

			}
			set 
			{

			}
		}
		public IndentStyle IndentStyle 
		{
			get 
			{
				return IndentStyle.Smart;
			}
			set 
			{

			}
		}
		
		public DocumentSelectionMode DocumentSelectionMode 
		{
			get 
			{
				return DocumentSelectionMode.Normal;
			}
			set 
			{

			}
		}
		
		public bool ShowQuickClassBrowserPanel 
		{
			get 
			{
				return false;
			}
			set 
			{
			}
		}
		
		public bool AllowCaretBeyondEOL 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool ShowMatchingBracket 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool ShowLineNumbers 
		{
			get 
			{
				return true;
			}
			set 
			{

			}
		}
		public bool ShowSpaces 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool ShowTabs 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool ShowEOLMarker 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool ShowInvalidLines 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool IsIconBarVisible 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool EnableFolding 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool ShowHorizontalRuler 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool ShowVerticalRuler 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool ConvertTabsToSpaces 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool UseAntiAliasedFont 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool CreateBackupCopy 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public bool MouseWheelScrollDown 
		{
			get 
			{
				return true;
			}
			set 
			{

			}
		}
		
		public bool MouseWheelTextZoom 
		{
			get 
			{
				return true;
			}
			set 
			{

			}
		}
		
		public bool HideMouseCursor 
		{
			get 
			{
				return false;
			}
			set 
			{

			}
		}
		public Encoding Encoding 
		{
			get 
			{
				return Encoding.UTF8;
			}
			set 
			{

			}
		}
		
		public int VerticalRulerRow 
		{
			get 
			{
				return 80;
			}
			set 
			{

			}
		}
		public LineViewerStyle LineViewerStyle 
		{
			get 
			{
				return LineViewerStyle.None;
			}
			set 
			{

			}
		}
		public string LineTerminator 
		{
			get 
			{
				LineTerminatorStyle lineTerminatorStyle = LineTerminatorStyle.Windows;
				switch (lineTerminatorStyle) 
				{
					case LineTerminatorStyle.Windows:
						return "\r\n";
					case LineTerminatorStyle.Macintosh:
						return "\r";
				}
				return "\n";
			}
			set 
			{
				throw new System.NotImplementedException();
			}
		}
		public bool AutoInsertCurlyBracket 
		{
			get 
			{
				return true;
			}
			set 
			{

			}
		}
		
		public Font Font 
		{
			get 
			{
				return FontContainer.DefaultFont;
			}
			set 
			{

			}
		}
		
		public bool EnableCodeCompletion 
		{
			get 
			{
				return true;
			}
			set 
			{

			}
		}
		
		public BracketMatchingStyle  BracketMatchingStyle 
		{
			get 
			{
				return BracketMatchingStyle.After;
			}
			set 
			{

			}
		}


	}
}

