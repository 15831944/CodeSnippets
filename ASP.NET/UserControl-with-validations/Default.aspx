<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="FOD_Default" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register Src="~/Controls/TestControl.ascx" TagPrefix="uc1" TagName="TestControl" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <fieldset style="width: 200px">
            <legend>Set 1 with UserControl</legend>
            <uc1:TestControl runat="server" id="TestControl1" />
        </fieldset>
        <br />
        <fieldset style="width: 200px">
            <legend>Set 2 with UserControl</legend>
            <uc1:TestControl runat="server" id="TestControl2" />
        </fieldset>
        <br />
        <fieldset style="width: 200px">
            <legend>Ordinary set</legend>
            <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Width="170px">
                <ValidationSettings ValidationGroup="vgNewTask">
                    <RequiredField IsRequired="true" />
                    <RegularExpression ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*),(\s)*)*(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)(,)?" />
                </ValidationSettings>
            </dx:ASPxTextBox>
        </fieldset>
        <br />
        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Submit" CausesValidation="true" ValidationGroup="vgNewTask"
            OnClick="ASPxButton1_Click" />
        <br />
        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Validated" Visible="false" />
    </form>
</body>
</html>
