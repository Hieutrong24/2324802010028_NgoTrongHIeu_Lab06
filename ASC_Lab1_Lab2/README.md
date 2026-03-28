# ASC - Lab 1 + Lab 2 SQL Server Edition

Đây là solution:

## Cấu trúc solution

- `ASC.Model`: entity models, constants, audit contract
- `ASC.Utilities`: utility helpers
- `ASC.DataAccess`: repository pattern, unit of work pattern
- `ASC.Business`: business services
- `ASC.Web`: ASP.NET Core MVC web application

## Chức năng đã có

- Cấu hình `ApplicationSettings` bằng `appsettings.json` + `IOptions`
- Kiến trúc phân lớp tách project
- `Repository` + `UnitOfWork`
- `ApplicationDbContext` dùng **SQL Server**
- Tự động tạo database trên **SQL Server local** khi chạy lần đầu
- Tự động seed dữ liệu từ `appsettings.json`
- Tự động seed tài khoản hệ thống bằng ASP.NET Core Identity
- Giao diện public + secure-style dashboard  
- Danh sách / tạo service request
- Danh sách products
- Danh sách master data

Connection string mặc định trong `ASC.Web/appsettings.json`:

```json
"DefaultConnection": "Server=ASUSVIVOBOOK;Database=ASC_Lab1_Lab2_DB;Trusted_Connection=True;Encrypt=True;TrustServerCertificate=True;MultipleActiveResultSets=True"
```

## Dữ liệu seed sẵn

Khi chạy lần đầu, hệ thống sẽ tự tạo database và seed đầy đủ dữ liệu:

- 3 role: `Admin`, `Engineer`, `Customer`
- 5 user hệ thống
- 4 nhóm master data và 20 giá trị master data
- 12 products
- 24 service requests

## Tài khoản seed sẵn

- Admin: `admin@asc.local` / `Admin@12345`
- Engineer: `engineer1@asc.local` / `Engineer@12345`
- Engineer: `engineer2@asc.local` / `Engineer2@12345`
- Customer: `customer1@asc.local` / `Customer@12345`
- Customer: `customer2@asc.local` / `Customer2@12345`

## Cách chạy

 
Lệnh:

```bash
dotnet restore
dotnet run --project ASC.Web
```
