<Query Kind="Statements">
  <Connection>
    <ID>ec8f290d-3276-4682-b5b2-dc2197d5d8e5</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>eRestaurant</Database>
  </Connection>
</Query>

/*Example:1 : Querying data from eRestaurant */
var result = from row in Tables
			where row.Capacity > 3
			select row;

			//The following line won't work in you project..
			
result.Dump();//the .Dump() method is an extension method in LinqPad - it's not in .NET

/*Example:2 : Query a simple array of strings */
string[] names = {"Dan","Don","Sam","Dwayne","Laurel","Steve"};
names.Dump();

var shortlist = from person in names
				where person.StartsWith("D")
				select person;
shortlist.Dump();		

/*Example:3 : Find the most recent Bill and its total */
// Get all the bills that have been placed
var allBills = from thingy in Bills
				where thingy.OrderPlaced.HasValue
				select new
				{
					BillDate = thingy.BillDate,
					IsReady = thingy.OrderReady
				};

allBills.Count().Dump(); // show the count of items
allBills.Take(5).Dump(); // show the first 5 bills