<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TestControl.ascx.cs" Inherits="Controls_TestControl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>



<div id="days" runat="server">
    <dx:ASPxSpinEdit ID="ASPxSpinEditDays" runat="server" Width="155px" Height="21px" NumberType="Integer"
        MaxValue="1000" MinValue="1" OnValidation="ASPxSpinEditDaysHours_Validation" EnableClientSideAPI="true"
        ClientSideEvents-Validation='<%# GetDaysValidator() %>'> 
        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="vgNewTask" />
        <%--<ClientSideEvents Validation='<%# GetDaysValidator() %>' />--%>
    </dx:ASPxSpinEdit>
</div>

<div id="hours" runat="server">
    <dx:ASPxTextBox ID="ASPxTextBoxHours" runat="server" Width="155px" Height="21px" NumberType="Integer"
        MaxValue="23" MinValue="1" OnValidation="ASPxSpinEditDaysHours_Validation" EnableClientSideAPI="true"
        ClientSideEvents-Validation='<%# GetHoursValidator() %>'>
        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="vgNewTask">
            <%--<RegularExpression ValidationExpression="((\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*),(\s)*)*(\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*)(,)?"
                ErrorText="Грешен мейл" />--%>
        </ValidationSettings>
        <%--<ClientSideEvents Validation='<%# GetHoursValidator() %>' />--%>
    </dx:ASPxTextBox>
</div>
