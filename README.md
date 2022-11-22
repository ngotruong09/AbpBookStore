## Tổng quan

Đây là project dạng non-layered Abp. Các tính năng có trong project gồm:
- [x] View, thêm, xóa, sửa book
- [x] Xuất file theo 2 định dạng CSV, EXCEL
- [x] Tạo thêm client để sử dụng cho phân hệ mobile
- [x] Cung cấp các api CRUD book cho phân hệ mobile   

Mô hình hệ thống

<p align="center">
  <img src="./doc/system.png" alt="Hệ thống" style="width:400px; height:300px;" >
</p>

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



