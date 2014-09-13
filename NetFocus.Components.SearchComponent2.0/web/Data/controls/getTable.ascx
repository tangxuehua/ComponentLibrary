<%@ Control targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table height="380" cellSpacing="10" cellPadding="10" width="100%" border="1">
	<tr>
		<td vAlign="top" align="left" width="35%">
			<div style="OVERFLOW: auto; HEIGHT: 320px"><asp:datalist id="sourceTableDataList" runat="server">
					<HeaderTemplate>
						<table>
							<tr>
								<td><b>表格</b></td>
							</tr>
						</table>
					</HeaderTemplate>
					<ItemTemplate>
						<table>
							<tr>
								<td>
									<asp:CheckBox Runat =server ID="sourceTableCheckBox" Text ='<%# DataBinder.Eval(Container.DataItem,"TableName")%>'>
									</asp:CheckBox></td>
							</tr>
						</table>
					</ItemTemplate>
				</asp:datalist></div>
		</td>
		<td vAlign="top" align="left" width="65%">
			<div style="OVERFLOW: auto; HEIGHT: 320px"><asp:datalist id="selectedTableDataList" runat="server" Width="100%">
					<HeaderTemplate>
						<table width="100%" cellpadding="0" cellspacing="3" border="0">
							<tr>
								<td width="63%"><b>选择的表格</b></td>
								<td width="18%" align="left"><b>别名</b>
								</td>
								<td width="19%"><b>中文名</b>
								</td>
							</tr>
						</table>
					</HeaderTemplate>
					<ItemTemplate>
						<TABLE width="100%" cellpadding="0" cellspacing="3" border="0">
							<TR>
								<TD width="70%">
									<asp:Label Width="100%" Runat=server ID="selectedTableLabel" Text='<%# DataBinder.Eval(Container.DataItem,"TableName")%>'>
									</asp:Label>
								</TD>
								<TD width="10%">
									<asp:TextBox id=aliasTextBox Text='<%# DataBinder.Eval(Container.DataItem,"AliasName")%>' Runat="server" Width="60">
									</asp:TextBox></TD>
								<TD width="20%">
									<asp:TextBox id=chineseNameTextBox Text='<%# DataBinder.Eval(Container.DataItem,"ChineseName")%>' Runat="server" Width="60">
									</asp:TextBox></TD>
							</TR>
						</TABLE>
					</ItemTemplate>
				</asp:datalist></div>
		</td>
	</tr>
	<tr height="40">
		<td vAlign="middle" align="center" colSpan="2"><asp:button id="selectTableSubmitButton" Text="选 择" Runat="server"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
			&nbsp;&nbsp;<asp:button id="updateSelectedTableButton" Text="更 新" Runat="server"></asp:button>
		</td>
	</tr>
</table>
