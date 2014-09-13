<%@ Control targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table cellSpacing="10" cellPadding="10" height="380" width="100%" border="1">
	<tr>
		<td valign="top" align="left" width="35%">
			<div style="OVERFLOW:auto;HEIGHT:320px">
				<asp:DataList id="sourceSortFieldDataList" runat="server" Width="100%">
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
				<asp:DataList id="selectedSortFieldDataList" runat="server" Width="100%">
					<HeaderTemplate>
						<table width="100%" cellpadding="0" cellspacing="3" border="0">
							<tr>
								<td width="75%"><b>选择的字段</b></td>
								<td width="25%"><b>别名</b>
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
								<TD width="25%">
									<asp:Label Width="100%" Visible=False Runat=server ID="sortLabel" Text='<%# DataBinder.Eval(Container.DataItem,"SortType")%>'>
									</asp:Label>
									<asp:DropDownList id="sortDropDownList" Runat="server" Width="60">
										<asp:ListItem Value="Asc">升序</asp:ListItem>
										<asp:ListItem Value="Desc">降序</asp:ListItem>
									</asp:DropDownList></TD>
							</TR>
						</TABLE>
					</ItemTemplate>
				</asp:DataList>
			</div>
		</td>
	</tr>
	<tr height="40">
		<td colspan="2" align="center" valign="middle">
			<asp:Button ID="selectSortFieldSubmitButton" Runat="server" Text="选 择"></asp:Button><FONT face="宋体">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</FONT>
			<asp:button id="updateSelectedSortFieldButton" Text="更 新" Runat="server"></asp:button>
		</td>
	</tr>
</table>
