<%@ Control targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<p align="center">
	<table cellpadding="40" cellspacing="0" border="0" height="400" width="100%">
		<tr>
			<td valign="middle" align="center">
				<table cellpadding="0" cellspacing="10" border="0" width="470" style="BORDER-RIGHT: #ffcc66 1px solid; BORDER-TOP: #ffcc66 1px solid; BORDER-LEFT: #ffcc66 1px solid; BORDER-BOTTOM: #ffcc66 1px solid">
					<tr>
						<td colspan="2" align="center">
							<b>连接数据源</b>
						</td>
					</tr>
					<tr height="30">
						<td width="110">驱动程序类型</td>
						<td>
							<asp:DropDownList Width="200" Runat="server" ID="dataSourceDriverTypeDropDownList"></asp:DropDownList></td>
					</tr>
					<tr height="30">
						<td width="110">数据源字符串</td>
						<td>
							<asp:TextBox Width="300" Runat="server" ID="dataSourceStringTextBox"></asp:TextBox></td>
					</tr>
					<tr height="40">
						<td colspan="2" align="center" valign="middle">
							<asp:Button ID="submitDataSourceButton" Runat="server" Text="连 接"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp; 
							&nbsp;&nbsp;&nbsp;
							<asp:Button ID="cancelDataSourceButton" Runat="server" Text="清 空"></asp:Button>
						</td>
					</tr>
					<tr>
						<td colspan="2" align="right"><asp:Label ForeColor="red" Runat="server" ID="showErrorLabel"></asp:Label></td>
					</tr>
				</table>
			</td>
		</tr>
	</table>
</p>
