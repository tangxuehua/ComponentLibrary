<%@ Control targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellSpacing="10" cellPadding="0" height=320 width="100%" border=0>
	<tr>
		<td vAlign="top" bgColor="white" style="BORDER-RIGHT: #b8d1f8 1px solid; BORDER-TOP: #b8d1f8 1px solid; BORDER-LEFT: #b8d1f8 1px solid; BORDER-BOTTOM: #b8d1f8 1px solid">
		<asp:datalist id="queryKindDataList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"
				Width="100%">
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" Height="100"></ItemStyle>
				<ItemTemplate>
					<asp:Image Runat="server" id="queryKindImage"></asp:Image>
					<br>
					<asp:Label ID="queryKindIdLabel" Visible=False Runat=server Text='<%# DataBinder.Eval(Container.DataItem,"id") %>'>
					</asp:Label>
					<asp:HyperLink ID="queryKindHyperLink" Runat="server" target="mainFrame">
						<%# DataBinder.Eval(Container.DataItem,"name") %>
					</asp:HyperLink>
				</ItemTemplate>
			</asp:datalist>
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
						<asp:dropdownlist id="queryKindDropDownList" Width="150" Runat="server"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;
						<asp:TextBox id="queryKindTextBox" runat="server" Width="150"></asp:TextBox>&nbsp;&nbsp;
						<asp:TextBox id="queryKindDescriptionTextbox" runat="server" Width="170px"></asp:TextBox>
					</td>
				</tr>
				<tr>
					<td align="right">&nbsp;
						<asp:Button id="createQueryKindButton" runat="server" Text="Ìí ¼Ó"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;
						<asp:Button id="updateQueryKindButton" runat="server" Text="ÐÞ ¸Ä"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
						&nbsp;
						<asp:Button id="deleteQueryKindButton" runat="server" Text="É¾ ³ý"></asp:Button></td>
				</tr>
			</table>
		</td>
	</tr>
</table>
