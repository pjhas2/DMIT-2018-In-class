<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Admin_Security_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row jumbotron">
        <h1>Site Administration</h1>
    </div>
    <div class="row">
        <div class="col-md-9">
            <h2>Users</h2>
        </div>
        <div class="col-md-3">
            <h2>Roles</h2>
        </div>
    </div>
    <%--List of Waiters--%>
    <asp:ObjectDataSource runat="server" ID="WaiterDataSource" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllWaiters" TypeName="eRestaurant.BLL.RestaurantAdminController"></asp:ObjectDataSource>
</asp:Content>

