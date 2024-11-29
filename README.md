## Product-System

A simple application for managing products, including features to list, add, edit, and delete products. The system is divided into two projects:

Front-End:

- Angular v17
- Bootstrap 5 & Icons
- NgRx State Management Examples
- RxJs Examples

Back-End: 

- .NET Core 8.0
- Entity Framework Core
- MSSQL for Database Management

## Application Screenshots

List of Products:

![](/docs/List_of_Products.jpg)

Add Product:

![](/docs/Add_Products.jpg)

Edit Product:

![](/docs/Edit_Products.jpg)

Product API Enpoints:

![](/docs/Product_API_Endpoints.jpg)

Product DB:

![](/docs/ProductDB_Details.jpg)


# Getting started

- Run DB Scripts:
```
Create a Database with name [ProductDB]
Run the script to create tables from (/docs/DB Scripts/Create Table Scripts.sql)
Run the script to add sample data from (/docs/DB Scripts/Sample Data Scripts.sql)
```
- Modify the appsettings.json -> DefaultConnection, with newly created DB.
- Run the PorductApi project and copy the localhost url.
- In ProductApp modify src/app/products/product.service.ts -> apiUrl.
- Run "npm start" from angular code path.
