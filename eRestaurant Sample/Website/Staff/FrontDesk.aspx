<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FrontDesk.aspx.cs" Inherits="Staff_FrontDesk" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row col-md-12">
        <h1>Front Desk</h1>

        <div class="well">
            <h5>Mock Date/Time</h5>
            <div class="pull-right col-md-5">
                Last Billed Date/Time
                <asp:Repeater ID="AdHocBillDateRepeater" runat="server" ItemType="System.DateTime" DataSourceID="AdHocBillDateDataSource">
                    <ItemTemplate>
                        <%# Item.ToShortDateString() %>
                        &ndash;
                        <%# Item.ToShortTimeString() %>
                     </ItemTemplate>
                 </asp:Repeater>
                        <asp:ObjectDataSource ID="AdHocBillDateDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetLastBillDateTime" TypeName="eRestaurant.BLL.AdHocController"></asp:ObjectDataSource>              
                    </div>
                    <asp:TextBox ID="SearchDate" runat="server" TextMode="Date" Text="2014-10-16" />
                    <asp:TextBox ID="SearchTime" runat="server" TextMode="Time" Text="13:00" />
                    <asp:LinkButton ID="MockDateTime" runat="server" CssClass="btn btn-primary">Post-back new date/time</asp:LinkButton>
                    <asp:LinkButton ID="MockListBillingDateTime" runat="server" CssClass="btn btn-default" OnClick="MockListBillingDateTime_Click">Set to last-billed date/time</asp:LinkButton>         
             </div>

        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
     </div>
</asp:Content>

