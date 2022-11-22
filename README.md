<a href="https://github.com/ngotruong09">
  <p align="center">
    <img src="./doc/bookstore.png" alt="BookStore">
  </p>
</a>

## Overview

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

Set up sql server connection string tại appsettings.json

```bash
{
...
  "ConnectionStrings": {
    "Default": "Server=YourServer;Database=BookStoreDB;Trusted_Connection=True"
  },
...
}
```

Sau đó đứng tại thư muc `MyAbp.BookStore` chạy câu lệnh sau:

````bash
dotnet run --migrate-database
````

Bước tiếp theo, đứng tại thư mục `MyAbp.BookStore` chúng ta run câu lệnh:

````bash
abp install-libs
````

Câu lệnh trên sẽ pull các javascript package mà project sử dụng.

Sau khi câu lệnh trên chạy xong, chúng ta mở IDE Visual studio lên và chạy project.

## How to create this solution
````bash
dotnet run --migrate-database
````



