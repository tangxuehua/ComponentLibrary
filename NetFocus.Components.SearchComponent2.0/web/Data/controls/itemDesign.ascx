<%@ Control %>
<%@ Register TagPrefix="nfcw" Namespace="NetFocus.Components.WebControls" Assembly="NetFocus.Components.SearchComponent" %>
<%@ Register TagPrefix="MyUserControl" TagName="validateDataSource" Src="validateDataSource.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="previewSQL" Src="previewSQL.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="setOrder" Src="setOrder.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="setConditions" Src="setConditions.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="getReturnField" Src="getReturnField.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="setTableRelation" Src="setTableRelation.ascx" %>
<%@ Register TagPrefix="MyUserControl" TagName="getTable" Src="getTable.ascx" %>
<nfcw:TabStrip id="tabStrip1" TargetID="MultiPage1" BackColor="#b8d1f8" width="550px" style="BORDER-BOTTOM: #ffcc66 2px solid"
	SolidTabHoverStyle="background-color:#ffcc66;color:#000000;border-top:1px solid #ffffff;border-right:1px solid #000000;font-weight:bold;" TabDefaultStyle="background-color:#b8d1f8;color:#000000;height:25;width:70;text-align:center;padding-bottom:-1px solid #ffffff;"
	runat="server" TabSelectedStyle="background-color:#ffcc66;color:#000000;border-top:1px solid #ffffff;border-right:1px solid #000000;font-weight:bold;"
	SepSelectedStyle="display:none;">
	<nfcw:Tab Text="数据源" TargetID="page1" ToolTip="设置该查询的数据源" />
	<nfcw:Tab Text="表 格" TargetID="page2" ToolTip="设置要选择的表格" />
	<nfcw:Tab Text="表关系" TargetID="page3" ToolTip="设置表格之间的关系" />
	<nfcw:Tab Text="返回字段" TargetID="page4" ToolTip="设置要选择的字段" />
	<nfcw:Tab Text="条件设置" TargetID="page5" ToolTip="设置查询的条件" />
	<nfcw:Tab Text="排序设置" TargetID="page6" ToolTip="设置排序条件" />
	<nfcw:Tab Text="Sql预览" TargetID="page7" ToolTip="SQL语句实时预览" />
</nfcw:TabStrip>
<nfcw:MultiPage id="MultiPage1" width="550px" runat="server">
	<nfcw:PageView id="page1" runat="server">
		<MyUserControl:validateDataSource id="ValidateDataSource1" runat="server"></MyUserControl:validateDataSource>
	</nfcw:PageView>
	<nfcw:PageView id="page2" runat="server">
		<MyUserControl:getTable id="GetTable1" runat="server"></MyUserControl:getTable>
	</nfcw:PageView>
	<nfcw:PageView id="page3" runat="server">
		<MyUserControl:setTableRelation id="SetTableRelation1" runat="server"></MyUserControl:setTableRelation>
	</nfcw:PageView>
	<nfcw:PageView id="page4" runat="server">
		<MyUserControl:getReturnField id="GetReturnField1" runat="server"></MyUserControl:getReturnField>
	</nfcw:PageView>
	<nfcw:PageView id="page5" runat="server">
		<MyUserControl:setConditions id="SetConditions1" runat="server"></MyUserControl:setConditions>
	</nfcw:PageView>
	<nfcw:PageView id="page6" runat="server">
		<MyUserControl:setOrder id="SetOrder1" runat="server"></MyUserControl:setOrder>
	</nfcw:PageView>
	<nfcw:PageView id="page7" runat="server">
		<MyUserControl:previewSQL id="PreviewSQL1" runat="server"></MyUserControl:previewSQL>
	</nfcw:PageView>
</nfcw:MultiPage>
