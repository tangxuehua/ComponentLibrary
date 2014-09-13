
using System;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Drawing;

using NetFocus.Components.TextEditor.Undo;


namespace NetFocus.Components.TextEditor.Document
{
	/// <summary>
	/// Describes the caret marker
	/// </summary>
	public enum LineViewerStyle {
		/// <summary>
		/// No line viewer will be displayed
		/// </summary>
		None,
		
		/// <summary>
		/// The row in which the caret is will be marked
		/// </summary>
		FullRow
	}
	
	/// <summary>
	/// Describes the indent style
	/// </summary>
	public enum IndentStyle { //缩进格式
		/// <summary>
		/// No indentation occurs
		/// </summary>
		None,
		
		/// <summary>
		/// The indentation from the line above will be
		/// taken to indent the curent line
		/// </summary>
		Auto, 
		
		/// <summary>
		/// Inteligent, context sensitive indentation will occur
		/// </summary>
		Smart
	}
	
	/// <summary>
	/// Describes the bracket highlighting style
	/// </summary>
	public enum BracketHighlightingStyle {
		
		/// <summary>
		/// Brackets won't be highlighted
		/// </summary>
		None,
		
		/// <summary>
		/// Brackets will be highlighted if the caret is on the bracket
		/// </summary>
		OnBracket,
		
		/// <summary>
		/// Brackets will be highlighted if the caret is after the bracket
		/// </summary>
		AfterBracket
	}
	
	/// <summary>
	/// Describes the selection mode of the text area
	/// </summary>
	public enum DocumentSelectionMode {
		/// <summary>
		/// The 'normal' selection mode.
		/// </summary>
		Normal,
		
		/// <summary>
		/// Selections will be added to the current selection or new
		/// ones will be created (multi-select mode)
		/// </summary>
		Additive   //附加方式,用于多选的情况.
	}
	
	
	/// <summary>
	/// The default <see cref="IDocument"/> implementation.
	/// </summary>
	internal class DefaultDocument : IDocument
	{	
		bool readOnly = false;
		ILineManager          lineManager = null;
		IBookMarkManager      bookmarkManager      = null;
		FoldingManager        foldingManager       = null;
		ITextBufferStrategy   textBufferStrategy   = null;
		IFormattingStrategy   formattingStrategy   = null;
		TextMarkerStrategy        markerStrategy = null;
		UndoStack             undoStack            = new UndoStack();
		ArrayList updateQueue = new ArrayList();
		ITextEditorProperties textEditorProperties = new DefaultTextEditorProperties();
		
		public TextMarkerStrategy TextMarkerStrategy {
			get {
				return markerStrategy;
			}
			set {
				markerStrategy = value;
			}
		}
		
		public ITextEditorProperties TextEditorProperties {
			get {
				return textEditorProperties;
			}
			set {
				textEditorProperties = value;
			}
		}
		
		public UndoStack UndoStack {
			get {
				return undoStack;
			}
		}
		
		public ArrayList UpdateQueue 
		{
			get 
			{ 
				return updateQueue;
			}
		}
		

		public bool ReadOnly 
		{
			get {
				return readOnly;
			}
			set {
				readOnly = value;
			}
		}
		
		public ILineManager LineManager {
			get {
				return lineManager;
			}
			set {
				lineManager = value;
			}
		}
		
		public IBookMarkManager BookmarkManager 
		{
			get 
			{
				return bookmarkManager;
			}
			set 
			{
				bookmarkManager = value;
			}
		}
		
		public FoldingManager FoldingManager 
		{
			get 
			{
				return foldingManager;
			}
			set 
			{
				foldingManager = value;
			}
		}
		
		public ITextBufferStrategy TextBufferStrategy 
		{
			get {
				return textBufferStrategy;
			}
			set {
				textBufferStrategy = value;
			}
		}
		
		public IFormattingStrategy FormattingStrategy {
			get {
				return formattingStrategy;
			}
			set {
				formattingStrategy = value;
			}
		}
		
		public IHighlightingStrategy HighlightingStrategy {
			get {
				return lineManager.HighlightingStrategy;
			}
			set {
				lineManager.HighlightingStrategy = value;
			}
		}
		
		
		public void RequestUpdate(TextAreaUpdate update)
		{
			updateQueue.Add(update);
		}
		
		public void UpdateSegmentListOnDocumentChange(ArrayList list, DocumentEventArgs e)
		{
			for (int i = 0; i < list.Count; ++i) {
				ISegment fm = (ISegment)list[i];
			
				if (e.Offset <= fm.Offset && fm.Offset <= e.Offset + e.Length ||
				    e.Offset <= fm.Offset + fm.Length && fm.Offset + fm.Length <= e.Offset + e.Length) {
					list.RemoveAt(i);
					--i;
					continue;
				}
				
				if (fm.Offset  <= e.Offset && e.Offset <= fm.Offset + fm.Length) {
					if (e.Text != null) {
						fm.Length += e.Text.Length;
					}
					if (e.Length > 0) {
						fm.Length -= e.Length;
					}
					continue;
				}
				
				if (fm.Offset >= e.Offset) {
					if (e.Text != null) {
						fm.Offset += e.Text.Length;
					}
					if (e.Length > 0) {
						fm.Offset -= e.Length;
					}
				}
			}
		}
		
		
		protected void OnDocumentAboutToBeChanged(DocumentEventArgs e)
		{
			if (DocumentAboutToBeChanged != null) {
				DocumentAboutToBeChanged(this, e);
			}
		}
		
		protected void OnDocumentChanged(DocumentEventArgs e)
		{
			if (DocumentChanged != null) {
				DocumentChanged(this, e);
			}
		}
		
		public event DocumentEventHandler DocumentAboutToBeChanged;
		public event DocumentEventHandler DocumentChanged;
		public void OnUpdateCommited()
		{
			if (UpdateCommited != null) {
				UpdateCommited(this, EventArgs.Empty);
			}
		}
		
		protected virtual void OnTextContentChanged(EventArgs e)
		{
			if (TextContentChanged != null) {
				TextContentChanged(this, e);
			}
		}
		
		public event EventHandler UpdateCommited;
		public event EventHandler TextContentChanged;

		#region the implements of the ILineManager interface
		//这十个方法的执行都有ILineManager来完成,所以这里只是对ILineManager的一个封装.
		public ArrayList LineSegmentCollection 
		{
			get 
			{
				return lineManager.LineSegmentCollection;
			}
		}
		
		public int TotalNumberOfLines 
		{
			get 
			{
				return lineManager.TotalNumberOfLines;
			}
		}
		
		public int GetLineNumberForOffset(int offset)
		{
			return lineManager.GetLineNumberForOffset(offset);
		}
		
		public LineSegment GetLineSegmentForOffset(int offset)
		{
			return lineManager.GetLineSegmentForOffset(offset);
		}
		
		public LineSegment GetLineSegment(int line)
		{
			return lineManager.GetLineSegment(line);
		}
		
		public int GetFirstLogicalLine(int lineNumber)
		{
			return lineManager.GetFirstLogicalLine(lineNumber);
		}
		
		public int GetLastLogicalLine(int lineNumber)
		{
			return lineManager.GetLastLogicalLine(lineNumber);
		}
		
		public int GetVisibleLine(int lineNumber)
		{
			return lineManager.GetVisibleLine(lineNumber);
		}
				
		public int GetNextVisibleLineAbove(int lineNumber, int lineCount)
		{
			return lineManager.GetNextVisibleLineAbove(lineNumber, lineCount);
		}
		
		public int GetNextVisibleLineBelow(int lineNumber, int lineCount)
		{
			return lineManager.GetNextVisibleLineBelow(lineNumber, lineCount);
		}
		

		#endregion
		
		#region ITextBufferStrategy interface
		
		public int TextLength 
		{
			get 
			{
				return textBufferStrategy.Length;
			}
		}
		
		public string TextContent 
		{
			get 
			{
				return GetText(0, textBufferStrategy.Length);
			}
			set 
			{
				OnDocumentAboutToBeChanged(new DocumentEventArgs(this, 0, 0, value));
				
				textBufferStrategy.SetContent(value);
				lineManager.SetContent(value);
				
				OnDocumentChanged(new DocumentEventArgs(this, 0, 0, value));
				OnTextContentChanged(EventArgs.Empty);
			}
		}
		
		public void Insert(int offset, string text)
		{
			if (readOnly) 
			{
				return;
			}
			OnDocumentAboutToBeChanged(new DocumentEventArgs(this, offset, -1, text));
			
			textBufferStrategy.Insert(offset, text);
			lineManager.Insert(offset, text);
			undoStack.Push(new UndoableInsert(this, offset, text));//将当前操作推进UndoStack,以便以后可能的回复.
			
			OnDocumentChanged(new DocumentEventArgs(this, offset, -1, text));
		}
		
		public void Remove(int offset, int length)
		{
			if (readOnly) 
			{
				return;
			}
			OnDocumentAboutToBeChanged(new DocumentEventArgs(this, offset, length));
			
			undoStack.Push(new UndoableDelete(this, offset, GetText(offset, length)));
			textBufferStrategy.Remove(offset, length);
			lineManager.Remove(offset, length);
			
			OnDocumentChanged(new DocumentEventArgs(this, offset, length));
		}
		
		public void Replace(int offset, int length, string text)
		{
			if (readOnly) 
			{
				return;
			}
			OnDocumentAboutToBeChanged(new DocumentEventArgs(this, offset, length, text));
			
			undoStack.Push(new UndoableReplace(this, offset, GetText(offset, length), text));
			textBufferStrategy.Replace(offset, length, text);
			lineManager.Replace(offset, length, text);
			
			OnDocumentChanged(new DocumentEventArgs(this, offset, length, text));
		}
		
		public char GetCharAt(int offset)
		{
			return textBufferStrategy.GetCharAt(offset);
		}
		
		public string GetText(int offset, int length)
		{
			return textBufferStrategy.GetText(offset, length);
		}
		public string GetText(ISegment segment)
		{
			return GetText(segment.Offset, segment.Length);
		}
		
		
		#endregion		

		#region ITextModel interface
		
		public Point OffsetToPosition(int offset)
		{
			int lineNr = GetLineNumberForOffset(offset);
			LineSegment line = GetLineSegment(lineNr);
			return new Point(offset - line.Offset, lineNr);
		}
		
		//根据当前光标所在的位置(一个二维坐标)来得到其所在逻辑存储中的偏移量.
		public int PositionToOffset(Point p)
		{
			if (p.Y >= this.TotalNumberOfLines) 
			{
				return 0;
			}
			LineSegment line = GetLineSegment(p.Y);
			return Math.Min(this.TextLength, line.Offset + Math.Min(line.Length, p.X));
		}
		
		
		#endregion
	}
}
