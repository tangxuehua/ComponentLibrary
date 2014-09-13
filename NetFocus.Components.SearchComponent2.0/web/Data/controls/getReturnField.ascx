<%@ Control targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellSpacing="10" cellPadding="10" height="380" width="100%" border="1">
	<tr>
		<td valign="top" align="left" width="35%">
			<div style="OVERFLOW:auto;HEIGHT:320px">
				<asp:DataList id="sourceFieldDataList" runat="server" Width="100%">
					<HeaderTemplate>
						<table>
							<tr>
								<td><b>字段</b></td>
							</tr>
						</table>
					</HeaderTemplate>
					<ItemTemplate>
						<table>
							<tr>
								<td>
									<asp:CheckBox Runat =server ID="sourceFieldCheckBox" Text ='<%# DataBinder.Eval(Container.DataItem,"FieldFullName")%>'>
									</asp:CheckBox></td>
							</tr>
						</table>
					</ItemTemplate>
				</asp:DataList>
			</div>
		</td>
		<td valign="top" align="left" width="65%">
			<div style="OVERFLOW:auto;HEIGHT:320px">
				<asp:DataList id="selectedFieldDataList" runat="server" Width="100%">
					<HeaderTemplate>
						<table width="100%" cellpadding="0" cellspacing="3" border="0">
							<tr>
								<td width="62%"><b>选择的字段</b></td>
								<td width="19%"><b>别名</b>
								</td>
								<td width="19%"><b>中文名</b>
								</td>
							</tr>
						</table>
					</HeaderTemplate>
					<ItemTemplate>
						<TABLE width="100%" cellpadding="0" cellspacing="3" border="0">
							<TR>
								<TD width="75%">
									<asp:Label Width="100%" Runat=server ID="selectedFieldLabel" Text='<%# DataBinder.Eval(Container.DataItem,"FieldFullName")%>'>
									</asp:Label>
								</TD>
								<TD width="10%">
									<asp:TextBox id=aliasFieldTextBox Text='<%# DataBinder.Eval(Container.DataItem,"AliasName")%>' Runat="server" Width="60">
									</asp:TextBox></TD>
								<TD width="15%">
									<asp:TextBox id=chineseNameFieldTextbox Text='<%# DataBinder.Eval(Container.DataItem,"ChineseName")%>' Runat="server" Width="60">
									</asp:TextBox></TD>
							</TR>
						</TABLE>
					</ItemTemplate>
				</asp:DataList>
			</div>
		</td>
	</tr>
	<tr height="40">
		<td colspan="2" align="center" valign="middle">
			<asp:Button ID="selectFieldSubmitButton" Runat="server" Text="选 择"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:Button id="updateSelectedFieldButton" Text="更 新" Runat="server"></asp:Button>
		</td>
	</tr>
</table>
