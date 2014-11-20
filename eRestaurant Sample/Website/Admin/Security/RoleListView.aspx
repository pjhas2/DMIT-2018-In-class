<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RoleListView.aspx.cs" Inherits="Admin_Security_RoleListView" %>

<asp:ListView ID="RoleListView" runat="server"
    ItemType="Microsoft.AspNet.Identity.EntityFramework.IdentityRole"
    OnItemCommand="RoleListView_ItemCommand">
    <EmptyDataTemplate>
        <table runat="server">
            <tr>
                <td>
                    No roles in this site.
                    <asp:LinkButton runat="server" CommandName="AddDefaultRoles" Text="Add default security roles" ID="AddDefaultRolesButton" />
                </td>
            </tr>
        </table>
    </EmptyDataTemplate>
    <ItemTemplate>
        <tr>
            <td><asp:Label Text='<%# Item.Name %>' runat="server" ID="NameLabel" /></td>
            <td><asp:Label Text='<%# Item.Users.Count %>' runat="server" ID="UsersCountLabel" /></td>
        </tr>
    </ItemTemplate>
    <LayoutTemplate>
        <table runat="server">
            <tr runat="server">
                <td runat="server">
                    <table runat="server" id="itemPlaceholderContainer"
                        class="table table-condensed table-hover table-striped">
                        <tr runat="server">
                            <th runat="server">Role Name</th>
                            <th runat="server">Users</th>
                        </tr>
                        <tr runat="server" id="itemPlaceholder"></tr>
                    </table>
                </td>
            </tr>
            <tr runat="server">
                <td runat="server">
                    <asp:DataPager runat="server" ID="DataPager2">
                        <Fields>
                            <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                        </Fields>
                    </asp:DataPager>
                </td>
            </tr>
        </table>
    </LayoutTemplate>
</asp:ListView>

