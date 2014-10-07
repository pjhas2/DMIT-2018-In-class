<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reservation.aspx.cs" Inherits="Reservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row col-md-12">
        <h1>Reservations</h1>
        <p>&nbsp;</p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Special Event:"></asp:Label>
&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="ViewObjectDataSource" DataTextField="Description" DataValueField="Reservations">
                <asp:ListItem>Select an Event</asp:ListItem>
                <asp:ListItem>No Event</asp:ListItem>
            </asp:DropDownList>
&nbsp;<asp:LinkButton ID="ViewReservations" runat="server">View Reservations</asp:LinkButton>
&nbsp;<asp:ObjectDataSource ID="ViewObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllSpecialEvents" TypeName="eRestaurant.BLL.RestaurantAdminController"></asp:ObjectDataSource>
        </p>
        <p>&nbsp;</p>
        <p>
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataSourceID="GridObjectDataSource">
                <Columns>
                    <asp:BoundField DataField="EventCode" HeaderText="EventCode" SortExpression="EventCode" />
                    <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
                    <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                </Columns>
                <EmptyDataTemplate>
                    No data to display.
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource ID="GridObjectDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllReservations" TypeName="eRestaurant.BLL.ReservationBySpecialEvent"></asp:ObjectDataSource>
        </p>
    </div>
    

</asp:Content>

