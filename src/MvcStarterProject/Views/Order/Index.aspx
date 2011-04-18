<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcStarterProject.Models.OrderIndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Order Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Available Products</h2>
    <table>
        <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Price</th>
        </tr>
    <% foreach (var product in Model.AvailableProducts)
       { %>
           <tr>
               <td><%= product.ProductId %></td>
               <td><%: product.Name %></td>
               <td align="right"><%: product.Price.ToString("c") %></td>
           </tr>
    <% } %>
    </table>
</asp:Content>
