<%@ Control targetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table height="160" cellSpacing="4" cellPadding="3" width="100%" border="1">
	<tr height="30">
		<td vAlign="bottom" align="left" width="170">字段&nbsp;<asp:dropdownlist id="conditionFieldDropDownList" Runat="server" Width="140" AutoPostBack="true"></asp:dropdownlist>
		</td>
		<td vAlign="bottom" align="left" width="110">类型&nbsp;<asp:label id="conditionFieldDataTypeLabel" Runat="server" Width="80px" Text="UnKnown"></asp:label>
		</td>
		<td vAlign="bottom" align="left" width="120">符号&nbsp;<asp:dropdownlist id="operatorDropDownList" Runat="server" Width="90">
				<asp:ListItem Value="=">=</asp:ListItem>
				<asp:ListItem Value="&gt;">&gt;</asp:ListItem>
				<asp:ListItem Value="&lt;">&lt;</asp:ListItem>
				<asp:ListItem Value="&gt;=">&gt;=</asp:ListItem>
				<asp:ListItem Value="&lt;=">&lt;=</asp:ListItem>
				<asp:ListItem Value="LIKE">LIKE</asp:ListItem>
			</asp:dropdownlist>
		</td>
		<td vAlign="bottom" align="left" width="*">名称&nbsp;<asp:textbox id="conditionNameTextBox" Runat="server" Width="65px"></asp:textbox>
		</td>
	</tr>
	<tr height="30">
		<td vAlign="bottom" align="left" width="170">控件&nbsp;<asp:dropdownlist id="inputValueControlTypeDropdownlist" Runat="server" Width="140">
				<asp:ListItem Value="TextBox">文本框</asp:ListItem>
				<asp:ListItem Value="DropDownList">下拉框</asp:ListItem>
				<asp:ListItem Value="DataTimePicker">日期控件</asp:ListItem>
			</asp:dropdownlist>
		</td>
		<td vAlign="bottom" align="left" width="110">初值&nbsp;<asp:textbox id="initialValueTextBox" Runat="server" Width="80px"></asp:textbox></td>
		<td vAlign="bottom" align="left" width="120">中文&nbsp;<asp:textbox id="fieldChineseNameTextbox" Runat="server" Width="90px"></asp:textbox>
		</td>
		<td vAlign="bottom" align="right" width="*">&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="addConditionButton" Runat="server" Text="添加条件"></asp:button></td>
	</tr>
	<tr height="95">
		<td vAlign="top" colSpan="4"><asp:datalist id="conditionsDataList" Width="100%" BorderWidth="1px" GridLines="Both" CellPadding="0"
				BackColor="White" BorderStyle="None" BorderColor="#CCCCCC" runat="server">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<HeaderTemplate>
					<table>
						<tr>
							<td>
								<asp:Label ID="Label1" Runat="server" Width="120px" Text="字段"></asp:Label>
							</td>
							<td>
								<asp:Label ID="Label2" Runat="server" Width="70px" Text="类型"></asp:Label>
							</td>
							<td>
								<asp:Label ID="Label10" Runat="server" Width="70px" Text="中文"></asp:Label>
							</td>
							<td>
								<asp:Label ID="Label11" Runat="server" Width="50px" Text="符号"></asp:Label>
							</td>
							<td>
								<asp:Label ID="Label3" Runat="server" Width="60px" Text="控件"></asp:Label>
							</td>
							<td>
								<asp:Label ID="Label4" Runat="server" Width="60px" Text="名称"></asp:Label>
							</td>
							<td>
								<asp:Label ID="Label6" Runat="server" Width="60px" Text="初始值"></asp:Label>
							</td>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<ItemTemplate>
					<table>
						<tr>
							<td>
								<asp:Label ID="fieldNameLabel" Runat="server" Width="120px" Text ='<%# DataBinder.Eval(Container.DataItem,"FieldFullName")%>'>
								</asp:Label>
							</td>
							<td>
								<asp:Label ID="fieldDataTypeLabel" Runat="server" Width="70px" Text ='<%# DataBinder.Eval(Container.DataItem,"FieldDataType")%>'>
								</asp:Label>
							</td>
							<td>
								<asp:Label ID="Label12" Runat="server" Width="70px" Text='<%# DataBinder.Eval(Container.DataItem,"ChineseName")%>'>
								</asp:Label>
							</td>
							<td>
								<asp:Label ID="Label13" Runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem,"Operator")%>'>
								</asp:Label>
							</td>
							<td>
								<asp:Label ID="inputValueControlTypeLabel" Runat="server" Width="60px" Text ='<%# DataBinder.Eval(Container.DataItem,"InputControlType")%>'>
								</asp:Label>
							</td>
							<td>
								<asp:Label ID="conditionNameLabel" Runat="server" Width="60px" Text ='<%# DataBinder.Eval(Container.DataItem,"ConditionName")%>'>
								</asp:Label>
							</td>
							<td>
								<asp:Label ID="initialValueLabel" Runat="server" Width="60px" Text ='<%# DataBinder.Eval(Container.DataItem,"InitialValue")%>'>
								</asp:Label>
							</td>
						</tr>
					</table>
				</ItemTemplate>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="White"></HeaderStyle>
			</asp:datalist>
			<p align="right">
				<asp:button id="deletaAtomConditionButton" Runat="server" Text="删除条件"></asp:button></p>
		</td>
	</tr>
</table>
<br>
<table height="170" cellSpacing="4" cellPadding="3" width="100%" border="1">
	<tr height="30">
		<td vAlign="middle" align="left" width="150">条件1&nbsp;<asp:dropdownlist id="currentAllConditionsDropdownlist1" Runat="server" Width="70px"></asp:dropdownlist>
		</td>
		<td vAlign="middle" align="left" width="150">条件2&nbsp;<asp:dropdownlist id="currentAllConditionsDropdownlist2" Runat="server" Width="70px"></asp:dropdownlist></td>
		<td vAlign="middle" align="left" width="100">关系&nbsp;<asp:dropdownlist id="andOrRelationDropdownlist" Runat="server" Width="50px">
				<asp:ListItem Value="And">与</asp:ListItem>
				<asp:ListItem Value="Or">或</asp:ListItem>
			</asp:dropdownlist>
		</td>
		<td vAlign="middle" align="left" width="120">名称&nbsp;<asp:textbox id="complicatedConditionNameTextbox" Runat="server" Width="70px"></asp:textbox>
		</td>
		<td vAlign="middle" align="right" width="*"><asp:button id="addComplicatedConditionButton" Runat="server" Text="添加组合"></asp:button></td>
	</tr>
	<tr height="130">
		<td vAlign="top" colSpan="5"><asp:datalist id="complicatedConditionsDatalist" Width="100%" BorderWidth="1px" GridLines="Both"
				CellPadding="0" BackColor="White" BorderStyle="None" BorderColor="#CCCCCC" runat="server">
				<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="#669999"></SelectedItemStyle>
				<HeaderTemplate>
					<table>
						<tr>
							<td>
								<asp:Label ID="Label7" Runat="server" Width="90px" Text="条件1"></asp:Label>
							</td>
							<td>
								<asp:Label ID="Label5" Runat="server" Width="90px" Text="关 系"></asp:Label>
							</td>
							<td>
								<asp:Label ID="Label8" Runat="server" Width="90px" Text="条件2"></asp:Label>
							</td>
							<td>
								<asp:Label ID="Label9" Runat="server" Width="150px" Text="组合条件名称"></asp:Label>
							</td>
						</tr>
					</table>
				</HeaderTemplate>
				<ItemStyle ForeColor="#000066"></ItemStyle>
				<ItemTemplate>
					<table>
						<tr>
							<td>
								<asp:Label ID="condition1Label" Runat="server" Width="90px" Text ='<%# DataBinder.Eval(Container.DataItem,"Condition1")%>'>
								</asp:Label>
							</td>
							<td>
								<asp:Label ID="relationLabel" Runat="server" Width="90px" Text ='<%# DataBinder.Eval(Container.DataItem,"Relation")%>'>
								</asp:Label>
							</td>
							<td>
								<asp:Label ID="condition2Label" Runat="server" Width="90px" Text ='<%# DataBinder.Eval(Container.DataItem,"Condition2")%>'>
								</asp:Label>
							</td>
							<td>
								<asp:Label ID="complicatedConditionLabel" Runat="server" Width="150px" Text ='<%# DataBinder.Eval(Container.DataItem,"ConditionName")%>'>
								</asp:Label>
							</td>
						</tr>
					</table>
				</ItemTemplate>
				<FooterStyle ForeColor="#000066" BackColor="White"></FooterStyle>
				<HeaderStyle Font-Bold="True" ForeColor="White" BackColor="White"></HeaderStyle>
			</asp:datalist>
			<P align="right">
				<asp:button id="deletaComplicatedConditionButton" Runat="server" Text="删除组合"></asp:button></P>
		</td>
	</tr>
</table>
