<%@ Register TagPrefix="nfcs" Namespace="NetFocus.Components.SearchComponent" Assembly="NetFocus.Components.SearchComponent" %>
<%@ Page %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>ItemSearch</title>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<nfcs:Style Href="../styles/searchComponentCss.css" runat="server" ID="Style1"></nfcs:Style>
	</HEAD>
	<body id="ItemSearchBody">
		<form runat="server" enctype="multipart/form-data" id="itemSearchForm">
			<nfcs:ItemSearch id="itemSearch1" runat="server" ControlFileName="itemSearch.ascx"></nfcs:ItemSearch>
		</form>
	</body>
</HTML>
