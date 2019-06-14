<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="eToolsWebsite.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <div class="row">
        <div class="col-md-6">
            <h3>Team Members</h3>
            <ul>
                <li>Weite Geng</li>  
                <li>Huang Zhou</li>
                <li>Haopeng Zhang</li>
                <li>Na Wang</li>
            </ul>
        </div>  
        <div class="col-md-6">
            <h3>Security Roles</h3>
            <ul>
                <li>Webmaster</li>
                <li>Employee</li>
                <li>Customer</li>                                
            </ul>
            <h3>Password: Password1</h3>
            <p></p>
        </div>
        <div class="col-md-6">
            <h3>Connection String</h3>
            <ul>
                <li>eToolsDB is the string used for connecting to the database.</li>
            </ul>
        </div>
     </div>
</asp:Content>
