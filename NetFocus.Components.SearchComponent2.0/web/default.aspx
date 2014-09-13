<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="test._default" %>
<%@ Register TagPrefix="nfcs" Namespace="NetFocus.Components.SearchComponent" Assembly="NetFocus.Components.SearchComponent" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>通用查询组件测试页面</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
  </HEAD>
	<body MS_POSITIONING="FlowLayout" leftMargin=15 rightMargin=15 >
		<form id="Form1" method="post" runat="server">
			<p align=center style="FONT-WEIGHT: bold; FONT-SIZE: 24px; COLOR: navy; FONT-FAMILY: 楷体_GB2312">基于Web的公共查询组件</p>
			
			<table width="90%" cellpadding="5" cellspacing="5" border="1" height=500>
				<tr>
					<td width="70" valign="top" align="center">
						<br>
						<asp:Button Runat="server" ID="button1" Text="查询类别管理"></asp:Button>
						<br>
						<br>
						<asp:Button Runat="server" ID="Button2" Text=" 查询项管理 "></asp:Button>
						<br>
						<br>
						<asp:Button Runat="server" ID="Button4" Text=" 查询项设计 "></asp:Button>
						<br>
						<br>
						<asp:Button Runat="server" ID="Button3" Text="查询应用界面"></asp:Button>
					</td>
					<td align="center" valign="top">
						<nfcs:WebSearchComponent id="c1" DataPath="data" runat="server" ControlUseType="ItemDesign"></nfcs:WebSearchComponent>
					</td>
				</tr>
			</table>
		</form></FORM>
	</body>
</HTML>
