## About this solution

Đây là project dạng non-layered Abp. Các tính năng có trong project gồm:
- [x] View, thêm, xóa, sửa book
- [x] Xuất file theo 2 định dạng CSV, EXCEL
- [x] Tạo thêm client để sử dụng cho phân hệ mobile
- [ ] Cung cấp các api CRUD book cho phân hệ mobile   

## How to run

The application needs to connect to a database. Run the following command in the `MyAbp.BookStore` directory:

````bash
dotnet run --migrate-database
````

This will create and seed the initial database. Then you can run the application with any IDE that supports .NET.

## How to create this solution
````bash
dotnet run --migrate-database
````



