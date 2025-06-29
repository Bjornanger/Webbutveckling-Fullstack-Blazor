# Webbutveckling Labb 1 - Initial planering

## Endpoints

### Product Endpoints

| Path                            | Method | Request            | Response  | ResponseCodes | Description                  | 
| ------------------------------- | ------ | ------------------ | --------- | ------------- | ---------------------------- | 
| "/products"                     | GET    | NONE               | Product[] | 200, 404      | Get all products             |      
| "/products/{id}"                | GET    | int Id             | Product   | 200, 404      | Get product by id            |      
| "/products/name/{name}"         | GET    | string Name        | Product   | 200, 404      | Get product by name          |      
| "/products/category/{category}" | GET    | string Category    | Product[] | 200, 404      | Get all products in category |      
| "/products"                     | POST   | Product            | NONE      | 200, 400      | Add new product              |      
| "/products/{id}"                | PUT    | int Id, Product    | NONE      | 200, 400      | Update product               |      
| "/products/status{id}"          | PATCH  | NONE               | NONE      | 200           | Toggle status on product     |   
| "/products/{id}                 | DELETE | int Id             | NONE      | 200, 404      | Delete product               |      

### User Administration Endpoints

| Path               | Method | Request      | Response   | ResponseCodes | Description       |
| ------------------ | ------ | ------------ | ---------- | ------------- | ----------------- |
| "/users/customers" | GET    | NONE         | Customer[] | 200           | Get all customers |
| "/users/admins"    | GET    | NONE         | Admin[]    | 200           | Get all admins    |
| "/users/{userId}"  | GET    | int userId   | Customer   | 200, 404      | Get user by id    |
| "/users/{email}"   | GET    | string Email | Customer   | 200, 404      | Get user by email |
| "/users/customers" | POST   | User         | NONE       | 200, 400      | Add new customer  |
| "/users/admins"    | POST   | User         | NONE       | 200, 400      | Add new admin     |
| "/users/{userId}"  | DELETE | int userId   | NONE       | 200, 404      | Delete customer   |

### Category Endpoints

| Path             | Method | Request  | Response   | ResponseCodes | Description        |
| ---------------- | ------ | -------- | ---------- | ------------- | ------------------ |
| "/category"      | GET    | NONE     | Category[] | 200           | Get all categories |
| "/category"      | POST   | Category | NONE       | 200, 400      | Add new category   |
| "/category/{id}" | DELETE | int Id   | NONE       | 200, 404      | Delete category    |

### Customer Interaction Endpoints

| Path                               | Method | Request                        | Response | ResponseCodes | Description                       |
| ---------------------------------- | ------ | ------------------------------ | -------- | ------------- | --------------------------------- |
| "/order/{userId}"                  | GET    | int userId                     | Order[]  | 200, 400      | Get all items from customer Order |
| "/{userId}"                        | POST   | int userId                     | Order    | 200, 400      | Create a customer order           |
| "/{userId}"                        | PUT    | int userId, ContactInfo        | NONE     | 200, 404      | Update customer contactInfo       |
| "/password/{userId}/{newPassword}" | PATCH  | int userId, string newPassword | NONE     | 200, 404      | Update customer password          |



## Data

### Product

| Property Name | Data Type                  | Description                        |
| ------------- | -------------------------- | ---------------------------------- |
| Id            | int                        | Id for database                    |
| Name          | string                     | Name of product in database        |
| Description   | string                     | Description of product in database |
| Price         | double                     | Price of product                   |
| Category      | Category[]                 | Categories of product              |
| Status        | bool                       | Status of product in database      |
| ImageURL      | string [attr]              | Image source for product           |
| Stock         | int                        | Amount of product in database      |
| ProductOrders | ICollection<ProductOrders> | Many-to-many list                  |

### Category

| Property Name     | Data Type | Description                         |
| ----------------- | --------- | ----------------------------------- |
| Id                | int       | Id for database                     |
| Name              | string    | Name of productcategory in database |
| ProductInCategory | Product[] | Products in category                |

### ProductsOrders

| Property Name | Data Type | Description                            |
| ------------- | --------- | -------------------------------------- |
| Id            | int       | Id for database                        |
| ProductId     | int       | FK to Product.Id                       |
| Product       | Product   | Product in product order               |
| OrderId       | int       | FK to Order.Id                         |
| Order         | Order     | Order in product order                 |
| Amount        | int       | Amount of set product in product order |

### User

| Property Name | Data Type     | Description      |
| ------------- | ------------- | ---------------- |
| Id            | int           | Id for database  |
| Email         | string [attr] | Email of user    |
| Password      | string?       | Password of user |

### Customer : User

| Property Name | Data Type                 | Description                    |
| ------------- | ------------------------- | ------------------------------ |
| FirstName     | string                    | First name of user in database |
| LastName      | string                    | Last name of user in database  |
| ContactInfo   | ContactInfo               | Address of user                |
| Orders        | ICollection<Order> Orders | List of orders made by user    |


### Admin : User

| Property Name | Data Type | Description        |
| ------------- | --------- | ------------------ |
| UserName      | string    | Username for admin |

### ContactInfo

| Property Name | Data Type     | Description                  |
| ------------- | ------------- | ---------------------------- |
| Id            | int           | Id for database              |
| Phone         | string [attr] | Phone number of user         |
| Address       | string [attr] | Street address of user       |
| ZipCode       | string [attr] | Zip code of user             |
| City          | string [attr] | City of residence of user    |
| Region        | string [attr] | Region of residence of user  |
| Country       | string [attr] | Country of residence of user |

### Order

| Property Name   | Data Type                  | Description                     |
| --------------- | -------------------------- | ------------------------------- |
| Id              | int                        | Id for database                 |
| CustomerId      | int                        | Id for customer that made order |
| ProductsInOrder | Product[]                  | Products in order               |
| OrderDate       | DateTime                   | Date and time of making order   |
| TotalPrice      | Double                     | Total price of order            |
| Status          | bool                       | Status of order                 |
| Products        | ICollection<ProductOrders> | Many-to-many list               |


