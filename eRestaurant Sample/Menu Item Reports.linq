<Query Kind="Expression">
  <Connection>
    <ID>3a1368a6-5c3f-4eb2-8d35-2a915a86c49e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

//This query is for pulling out data to be used in a 
//Details report. The query gets all the menu items
//and their categories, sorting them bt the category
//description and then by the menu item description
from cat in Items
orderby cat.MenuCategory.Description, cat.Description
select new 
{
	CategoryDescription = cat.MenuCategory.Description,
	ItemDescription = cat.Description,
	Price = cat.CurrentPrice,
	Calories = cat.Calories,
	Comment = cat.Comment
}