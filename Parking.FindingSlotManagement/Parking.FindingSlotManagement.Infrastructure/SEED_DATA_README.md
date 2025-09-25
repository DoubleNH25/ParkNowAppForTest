# Seed Data System - Tự động với Migration

## Tổng quan
Hệ thống seed data đã được tích hợp trực tiếp vào `OnModelCreating` của `ParkZDbContext`. Khi bạn chạy `add-migration` và `update-database`, seed data sẽ **TỰ ĐỘNG** được thêm vào database.

## ✅ Cách hoạt động

### 1. Seed Data tự động với Migration
- Seed data được định nghĩa trong `OnModelCreating` của `ParkZDbContext`
- Khi chạy `add-migration`, EF Core sẽ tự động tạo migration với seed data
- Khi chạy `update-database`, seed data sẽ được thêm vào database

### 2. Quy trình đúng:
```bash
# 1. Tạo migration (seed data sẽ được bao gồm tự động)
dotnet ef migrations add InitialCreate --project Parking.FindingSlotManagement.Infrastructure --startup-project Parking.FindingSlotManagement.Api

# 2. Cập nhật database (seed data sẽ được thêm tự động)
dotnet ef database update --project Parking.FindingSlotManagement.Infrastructure --startup-project Parking.FindingSlotManagement.Api

# 3. Xong! Dữ liệu đã có trong database
```

## 📊 Dữ liệu được seed tự động:

### ✅ Các entity được seed:
- **Roles**: Manager, Keeper, Customer, Staff (từ RoleConfiguration)
- **Traffic**: Xe ô tô, Xe máy (từ TrafficConfiguration) + Xe đạp (bổ sung)
- **Fees**: Tư nhân, Doanh nghiệp (từ FeeConfiguration) + Doanh nghiệp lớn, Tập đoàn (bổ sung)
- **Users**: 5 người dùng với mật khẩu `123456`
- **BusinessProfiles**: 2 doanh nghiệp mẫu
- **Parkings**: 2 bãi đỗ xe
- **Floors**: 5 tầng
- **ParkingSlots**: 100 chỗ đỗ xe (xe máy và ô tô)
- **ParkingPrices**: 6 loại giá đỗ xe (theo giờ và theo ngày)
- **ParkingHasPrices**: Liên kết bãi đỗ với giá
- **Wallets**: Ví cho tất cả user (WalletId=1 từ WalletConfiguration + 4 ví bổ sung)
- **VehicleInfors**: 2 phương tiện mẫu
- **TimeSlots**: 2 khung giờ mẫu (6h-18h và 18h-6h)

### 🔐 Thông tin đăng nhập:
- **Mật khẩu mặc định**: `123456`
- **Tài khoản**:
  - admin@parkz.com (Admin)
  - manager1@parkz.com (Manager)
  - staff1@parkz.com (Staff)
  - keeper1@parkz.com (Keeper)
  - customer1@parkz.com (Customer)

## 🚀 Ưu điểm của cách này:

1. **Tự động**: Không cần chạy thêm lệnh nào
2. **Đồng bộ**: Seed data luôn đi cùng với migration
3. **Rollback**: Có thể rollback migration và seed data cùng lúc
4. **Version control**: Seed data được track trong migration files
5. **Production safe**: Seed data chỉ chạy khi migration được apply

## ⚠️ Lưu ý:

- Seed data chỉ được thêm **một lần** khi migration được apply
- Nếu muốn thêm seed data mới, cần tạo migration mới
- Seed data được hash password đúng cách
- Tất cả foreign key relationships được đảm bảo

## Thông tin đăng nhập mặc định

Tất cả người dùng được tạo với mật khẩu mặc định: **123456**

### Danh sách tài khoản:
- **Admin**: admin@parkz.com (Role: Manager)
- **Manager**: manager1@parkz.com (Role: Manager)
- **Staff**: staff1@parkz.com (Role: Staff)
- **Keeper**: keeper1@parkz.com (Role: Keeper)
- **Customer**: customer1@parkz.com (Role: Customer)

## Lưu ý
- Seed data chỉ được thêm nếu bảng chưa có dữ liệu
- Hệ thống sẽ kiểm tra `AnyAsync()` trước khi thêm dữ liệu
- Dữ liệu được seed theo thứ tự phụ thuộc (dependencies)
- Tất cả mật khẩu được hash bằng HMACSHA512
