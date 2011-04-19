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
            <th></th>
        </tr>
    <% foreach (var product in Model.AvailableProducts)
       { %>
           <tr>
               <td><%= product.ProductId %></td>
               <td><%: product.Name %></td>
               <td align="right"><%: product.Price.ToString("c") %></td>
               <td><%= Html.ActionLink("Add to order", "AddToOrder", "Order", new {productId = product.ProductId}, null) %></td>
           </tr>
    <% } %>
    </table>

    <h2>Products In Cart</h2>
    <table>
        <tr>
            <th>ID</th>
            <th>Name</th>
            <th>Price</th>
            <th></th>
        </tr>
        <% foreach (var product in Model.ProductsInOrder)
           { %>
            <tr>
               <td><%= product.ProductId %></td>
               <td><%: product.Name %></td>
               <td align="right"><%: product.Price.ToString("c") %></td>
               <td></td>
            </tr>
        <% } %>
    </table>
</asp:Content>
