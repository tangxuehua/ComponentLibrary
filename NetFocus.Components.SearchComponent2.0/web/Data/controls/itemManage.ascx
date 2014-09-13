<%@ Control targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellSpacing="10" cellPadding="0" height=320 width="100%" border=0>
	<tr>
		<td bgcolor="white" valign="top" style="BORDER-RIGHT: #b8d1f8 1px solid; BORDER-TOP: #b8d1f8 1px solid; BORDER-LEFT: #b8d1f8 1px solid; BORDER-BOTTOM: #b8d1f8 1px solid">
			<asp:DataList id="queryItemDataList" runat="server" Width="100%" RepeatDirection="Horizontal"
				RepeatColumns="4">
				<ItemStyle HorizontalAlign="Center" Height="120" VerticalAlign="Top"></ItemStyle>
				<ItemTemplate>
					<asp:image Runat="server" id="queryItemImage"></asp:image>
					<br>
					<%# DataBinder.Eval(Container.DataItem,"name") %>
				</ItemTemplate>
			</asp:DataList>
		</td>
	</tr>
</table>
<table height="100" cellSpacing="10" cellPadding="0" width="100%" border="0">
	<tr>
		<td style="BORDER-RIGHT: #b8d1f8 1px solid; BORDER-TOP: #b8d1f8 1px solid; BORDER-LEFT: #b8d1f8 1px solid; BORDER-BOTTOM: #b8d1f8 1px solid"
			vAlign="middle" bgColor="white">
			<table height="100" cellSpacing="10" cellPadding="0" width="100%" border="0">
				<tr>
					<td align="left">
						<asp:dropdownlist id="queryItemDropDownList" Width="150" Runat="server"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;
						<asp:TextBox id="queryItemTextBox" runat="server" Width="150"></asp:TextBox>&nbsp;&nbsp;
						<asp:TextBox id="queryItemDescriptionTextbox" runat="server" Width="170px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align="right">
						<asp:Button id="createQueryItemButton" runat="server" Text="Ìí ¼Ó"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;
						<asp:Button id="updateQueryItemButton" runat="server" Text="ÐÞ ¸Ä"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;
						<asp:Button id="deleteQueryItemButton" runat="server" Text="É¾ ³ý"></asp:Button></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
