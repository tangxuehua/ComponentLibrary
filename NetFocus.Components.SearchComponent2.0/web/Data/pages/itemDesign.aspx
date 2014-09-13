<%@ Page %>
<%@ Register TagPrefix="nfcs" Namespace="NetFocus.Components.SearchComponent" Assembly="NetFocus.Components.SearchComponent" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>itemDesign</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<nfcs:Style Href="../styles/searchComponentCss.css" runat="server" ID="Style1"></nfcs:Style>
  </HEAD>
	<body id="mainPageBody" topMargin=0>
		<form runat="server" enctype="multipart/form-data" id="itemDesign">
					<table height=460 cellpadding=3 cellspacing=0 style="BORDER-RIGHT: #b8d1f8 1px solid; BORDER-TOP: #b8d1f8 1px solid; BORDER-LEFT: #b8d1f8 1px solid; BORDER-BOTTOM: #b8d1f8 1px solid">
				<tr>
					<td valign=top>
			<nfcs:ItemDesign id="itemDesign1" runat="server" ControlFileName="itemDesign.ascx"></nfcs:ItemDesign>
								</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
