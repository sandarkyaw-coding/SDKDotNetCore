** Start Date ** (11.4.24)

suggestion => crtl + . 

break point => F9
for each line => F10
for details => F11

connection -> default value = null 

global variable declare -> with _ 

http method
- get  [No Accept Body]
- post -> create
- put -> update the whole object         
- patch -> update each part of object
- delete [No Accept Body]

----------------------------------
** Dot Net ** (14.4.24)

dataset => dataTable
dataTable => dataRow
dataRow => dataColumn

parameter = @ sign => insert value

---------------------------------
** Dapper ** (26.4.24)

toList => DB Execute
Query

---------------------------------
** EFCore ** (27.4.24)

Need microsoft.Sql  

DbContext => Connect DB -> C#

---------------------------------
** Web API CRUD - Dot Net Core ** (28.4.24)
Controller - Model 

------------------------------------------
** Web API CRUD - Dapper ** (1.4.24)

params => auto skip null value

-----------------------------------
** Web API CRUD - Ado Dot Net ** (5.2.24)

_________________________________________

-- update int to something access

-- extension class must be static class

Scaffold-DbContext "Server=.;Database=SDKDotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context AppDbContext

dotnet ef dbcontext scaffold "Server=.;Database=SDKDotNetCore;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -t Tbl_Blog -f

dotnet ef dbcontext scaffold "Server=.;Database=SDKDotNetCore;User Id=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -t Tbl_PieChart -f

dotnet ef dbcontext scaffold "Server=.;Database=SDKDotNetCore;User ID=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models -c AppDbContext -f