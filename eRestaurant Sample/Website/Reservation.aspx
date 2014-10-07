<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reservation.aspx.cs" Inherits="Reservation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row col-md-12">
        <h1>Reservations</h1>
        <p>&nbsp;</p>
        <p>
            <asp:Label ID="Label1" runat="server" Text="Special Event:"></asp:Label>
&nbsp;<asp:DropDownList ID="SpecialEventsDropDownList" runat="server" DataSourceID="ViewODS" DataTextField="Description" DataValueField="EventCode" AppendDataBoundItems="True">
                <asp:ListItem Value="none">[Select an Event]</asp:ListItem>
                <asp:ListItem Value="">[No Event]</asp:ListItem>
            </asp:DropDownList>
&nbsp;<asp:LinkButton ID="ViewReservations" runat="server">View Reservations</asp:LinkButton>
&nbsp;<asp:ObjectDataSource ID="ViewODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListAllSpecialEvents" TypeName="eRestaurant.BLL.RestaurantAdminController"></asp:ObjectDataSource>
        </p>
        <p>&nbsp;</p>
        <p>
            <asp:GridView ID="ReservationGridView" runat="server" AutoGenerateColumns="False" DataSourceID="GridODS">
                <Columns>
                    <asp:BoundField DataField="ReservationID" HeaderText="ReservationID" SortExpression="ReservationID" />
                    <asp:BoundField DataField="CustomerName" HeaderText="CustomerName" SortExpression="CustomerName" />
                    <asp:BoundField DataField="ReservationDate" HeaderText="ReservationDate" SortExpression="ReservationDate" />
                    <asp:BoundField DataField="NumberInParty" HeaderText="NumberInParty" SortExpression="NumberInParty" />
                    <asp:BoundField DataField="ContactPhone" HeaderText="ContactPhone" SortExpression="ContactPhone" />
                    <asp:BoundField DataField="ReservationStatus" HeaderText="ReservationStatus" SortExpression="ReservationStatus" />
                    <asp:BoundField DataField="EventCode" HeaderText="EventCode" SortExpression="EventCode" />
                </Columns>
                <EmptyDataTemplate>
                    No data to display.
                </EmptyDataTemplate>
            </asp:GridView>
            <asp:ObjectDataSource ID="GridODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="LookupReservationsBySpecialEvent" TypeName="eRestaurant.BLL.RestaurantAdminController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="SpecialEventsDropDownList" Name="eventCode" PropertyName="SelectedValue" Type="String" ></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </p>
    </div>
    

</asp:Content>

