<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="deleteafterdone.ascx.cs" Inherits="eToolsWebsite.UserControls.delete_after_done" %>
<h3>Admin Login</h3>
<asp:Label ID="AdminUserName" runat="server" CssClass="label label-info" />
<asp:Label ID="AdminPassword" runat="server" CssClass="label label-success" />
<asp:LinkButton ID="FillInForm" runat="server" CssClass="btn btn-primary" OnClick="FillInForm_Click" Text="Use Admin Credentials" CausesValidation="false" />
