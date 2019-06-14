<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="eToolsWebsite._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row">
        <center>
            <asp:Image ID="TeamLogo" runat="server" ImageUrl="~/image/team-logo.png"/>
        </center>
    </div>
    
    <h1>eTools-2018-E01-Team-D</h1>
    <a href="http://dmit-2018.github.io/docs/project/eTools/jan2018/index.html">Lab Specs</a>
    <div class="row">
        <div class="col-md-4">
            <h2>Team Members</h2>
            <h3>Weite Geng - gta0814</h3>
            <ul>
                <li>Implement the security system</li>
                <li>Purchasing Subsystem</li>
            </ul>
            <h3>Hunag Zhou - hzhounait</h3>
            <ul>
                <li>Initial setup(Provided documentation on the home pages and about page, update Nuget Packages, Create the class libary), adding webpages and navigations</li>
                <li>Sales Subsystem</li>
            </ul>
            <h3>Na Wang - nwang15</h3>
                <ul>
                    <li>Created the entiies by reverse engineering</li>
                    <li>Add the error messages</li>
                    <li>Recieving Subsystem</li>
                </ul>
            <h3>Haopeng Zhang - hzhang47</h3>
                <ul>
                    <li>Implement the user message control</li>
                    <li>Rentals Subsystem</li>
                </ul>
        </div>
        <div class="col-md-4">
            <h2>Known Bugs</h2>
            <p>Please list your known bugs below</p>
            <ul>
                <li>List bugs here/incomplete work</li>
            </ul>
        </div>
    </div>

</asp:Content>
