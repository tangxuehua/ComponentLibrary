<%@ Register TagPrefix="nfcs" Namespace="NetFocus.Components.SearchComponent" Assembly="NetFocus.Components.SearchComponent" %>
<%@ Page %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>itemManage</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<nfcs:Style Href="../styles/searchComponentCss.css" runat="server" ID="Style1"></nfcs:Style>
	</HEAD>
	<body id="ItemManageBody">
		<form runat="server" enctype="multipart/form-data" id="itemManageForm">
							<table height=460 cellpadding=3 cellspacing=0 style="BORDER-RIGHT: #b8d1f8 1px solid; BORDER-TOP: #b8d1f8 1px solid; BORDER-LEFT: #b8d1f8 1px solid; BORDER-BOTTOM: #b8d1f8 1px solid">
				<tr>
					<td valign=top>
			<nfcs:ItemManage id="itemManage1" runat="server" ControlFileName="itemManage.ascx"></nfcs:ItemManage>
								</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
