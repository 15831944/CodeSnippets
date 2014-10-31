<%@ Page Title="" Language="C#" MasterPageFile="~/SmartIT.master" AutoEventWireup="true" CodeFile="BindError.aspx.cs" Inherits="DevTasks_Test" %>

<%@ Register Assembly="DevExpress.Web.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Data.Linq" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<!-- EnableViewState = "false" е важно за да се update-ват контролите в грида, които са bind-нати с някакви данни -->

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="true" KeyFieldName="ID" EnableCallBacks="true"
        OnDataBinding="ASPxGridView1_DataBinding">
        <Columns>
            <dx:GridViewDataTextColumn FieldName="ID" />
            <dx:GridViewDataTextColumn FieldName="OrderID" />
            <dx:GridViewDataTextColumn FieldName="Name" />
            <dx:GridViewDataTextColumn FieldName="Status" />
            <dx:GridViewDataColumn Name="ButtonColumn" Width="200px">
                <DataItemTemplate>
                    <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Button" OnInit="ASPxButton1_Init"></dx:ASPxButton>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" OnInit="ASPxLabel1_Init" EnableViewState="false"
                        Text='<%# string.Format("TaskID = {0}\nOrderID = {1}", Eval("ID"), Eval("OrderID")) %>'></dx:ASPxLabel>
                </DataItemTemplate>
            </dx:GridViewDataColumn>
            <dx:GridViewCommandColumn ButtonType="Image">

            </dx:GridViewCommandColumn>
        </Columns>
        <Settings ShowFilterRow="true" ShowFilterBar="Auto" />
        <SettingsPager PageSize="10" />
        <Styles>
            <Header Wrap="True" />
        </Styles>
    </dx:ASPxGridView>
    <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Postback outside grid"></dx:ASPxButton>
    <%--<asp:LinqDataSource ID="LinqDataSource1" runat="server" OnSelecting="LinqDataSource1_Selecting"></asp:LinqDataSource>--%>
    <%--<cc1:LinqServerModeDataSource ID="LinqServerModeDataSource1" runat="server" OnSelecting="LinqServerModeDataSource1_Selecting" />--%>
</asp:Content>

