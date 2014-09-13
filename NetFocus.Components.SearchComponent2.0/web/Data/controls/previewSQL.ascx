<%@ Control targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellSpacing="10" cellPadding="10" height="380" width="100%" border="1" bgcolor="#ffffff">
	<tr height="30">
		<td valign="top" align="center">
			<asp:Button Runat="server" ID="showSQLButton" Text="¸ü ÐÂ"></asp:Button>
		</td>
	</tr>
	<tr height="*">
		<td valign="top">
			<Div Runat="server" ID="showSQLDiv" style="OVERFLOW: auto; HEIGHT: 320px">
				<asp:Table id="showSQLTable" runat="server">
					<asp:TableRow ID="selectRow" Runat="server">
						<asp:TableCell Runat=server ID="selectCell" ColumnSpan=2 Font-Bold=True Text=""></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow ID="selectContentRow" Runat="server">
						<asp:TableCell Runat=server ID="Tablecell1" Width="30px"></asp:TableCell>
						<asp:TableCell Runat=server ID="selectContentCell" ForeColor=#000066 Text=""></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow ID="fromRow" Runat="server">
						<asp:TableCell Runat=server ID="fromCell" ColumnSpan=2 Font-Bold=True Text=""></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow ID="fromContentRow" Runat="server">
						<asp:TableCell Runat=server ID="Tablecell3" Width="30px"></asp:TableCell>
						<asp:TableCell Runat=server ID="fromContentCell" ForeColor=#000066 Text=""></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow ID="whereRow" Runat="server">
						<asp:TableCell Runat=server ID="whereCell" ColumnSpan=2 Font-Bold=True Text=""></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow ID="whereContentRow" Runat="server">
						<asp:TableCell Runat=server ID="Tablecell4" Width="30px"></asp:TableCell>
						<asp:TableCell Runat=server ID="whereContentCell" ForeColor=#000066 Text=""></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow ID="orderRow" Runat="server">
						<asp:TableCell Runat=server ID="orderCell" ColumnSpan=2 Font-Bold=True Text=""></asp:TableCell>
					</asp:TableRow>
					<asp:TableRow ID="orderContentRow" Runat="server">
						<asp:TableCell Runat=server ID="Tablecell5" Width="30px"></asp:TableCell>
						<asp:TableCell Runat=server ID="orderContentCell" ForeColor=#000066 Text=""></asp:TableCell>
					</asp:TableRow>
				</asp:Table>
			</Div>
		</td>
	</tr>
</table>
