<%@ Control targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table height="380" cellSpacing="4" cellPadding="3" width="100%" border="1">
	<tr height="20">
		<td vAlign="middle" align="left" width="10%">表1</td>
		<td vAlign="middle" align="left" width="35%">
			<asp:dropdownlist id="relationFromTable1DropDownList" AutoPostBack="True" Width="170" Runat="server"></asp:dropdownlist></td>
		<td vAlign="middle" align="left" width="10%">表2
		</td>
		<td vAlign="middle" align="left" width="35%" colspan="2">
			<asp:dropdownlist id="relationFromTable2DropDownList" AutoPostBack="True" Width="170" Runat="server"></asp:dropdownlist></td>
	</tr>
	<tr height="20">
		<td vAlign="middle" align="left" width="10%">字段
		</td>
		<td vAlign="middle" align="left" width="35%">
			<asp:dropdownlist id="relationField1Dropdownlist" Width="170" Runat="server"></asp:dropdownlist></td>
		<td vAlign="middle" align="left" width="10%">字段</td>
		<td vAlign="middle" align="left" width="30%">
			<asp:dropdownlist id="relationField2Dropdownlist" Width="170" Runat="server"></asp:dropdownlist>
		</td>
		<td vAlign="middle" align="right">
			<asp:button id="addJoinFieldButton" Runat="server" Text="添加字段"></asp:button></td>
	</tr>
	<tr>
		<td vAlign="top" align="left" colSpan="6" height="100">当前关联的字段<br>
			<asp:listbox id="currentSelectedJoinFieldListBox" Width="100%" runat="server" Height="105px"></asp:listbox>
		</td>
	</tr>
	<tr height="30">
		<td align="right" colspan="5">
			选择关联类型<asp:dropdownlist id="joinTypeDropdownlist" Width="120" Runat="server">
				<asp:ListItem Value="Cross Join">交叉联接</asp:ListItem>
				<asp:ListItem Value="Inner Join">内联接</asp:ListItem>
				<asp:ListItem Value="Left Join">左联接</asp:ListItem>
				<asp:ListItem Value="right Join">右联接</asp:ListItem>
				<asp:ListItem Value="Full Join">全联接</asp:ListItem>
			</asp:dropdownlist>
			&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			<asp:button id="addRelationButton" Runat="server" Text="添加关联"></asp:button></td>
	</tr>
	<tr>
		<td vAlign="top" align="left" colSpan="5">当前所有关系<br>
			<asp:listbox id="currentTableRelationListBox" Width="100%" runat="server" Height="150px"></asp:listbox></td>
	</tr>
</table>
