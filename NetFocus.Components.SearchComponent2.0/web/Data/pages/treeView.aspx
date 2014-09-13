<%@ Page %>
<%@ Register TagPrefix="nfcs" Namespace="NetFocus.Components.SearchComponent" Assembly="NetFocus.Components.SearchComponent" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>treeView</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<nfcs:Script Src="../scripts/treeView.js" runat="server" id=Script1></nfcs:Script>
		<nfcs:Style Href="../styles/searchComponentCss.css" runat="server" id=Style1></nfcs:Style>
  </HEAD>
	<body id="treeViewPageBody" topMargin=0>
		<form runat="server" enctype="multipart/form-data" id="treeViewForm">
			<table height=460 cellpadding=3 cellspacing=0 style="BORDER-RIGHT: #b8d1f8 1px solid; BORDER-TOP: #b8d1f8 1px solid; BORDER-LEFT: #b8d1f8 1px solid; BORDER-BOTTOM: #b8d1f8 1px solid">
				<tr>
					<td valign=top>
						<nfcs:TreeView id="treeView1" runat="server" ControlFileName="treeView.ascx"></nfcs:TreeView>
					</td>
				</tr>
			</table>
			
		</form>
	</body>
</HTML>
