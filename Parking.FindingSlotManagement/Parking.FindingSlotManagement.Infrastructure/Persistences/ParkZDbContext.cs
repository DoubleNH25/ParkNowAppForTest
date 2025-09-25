using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Parking.FindingSlotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Parking.FindingSlotManagement.Infrastructure.Persistences
{
    public class ParkZDbContext : DbContext
    {
        public ParkZDbContext(DbContextOptions<ParkZDbContext> options)
            : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; } = null!;
        public DbSet<BusinessProfile> BusinessProfiles { get; set; } = null!;
        public DbSet<FavoriteAddress> FavoriteAddresses { get; set; } = null!;
        public DbSet<Floor> Floors { get; set; } = null!;
        public DbSet<TimeLine> TimeLines { get; set; } = null!;
        public DbSet<Domain.Entities.Parking> Parkings { get; set; } = null!;
        public DbSet<ParkingHasPrice> ParkingHasPrices { get; set; } = null!;
        public DbSet<ParkingPrice> ParkingPrices { get; set; } = null!;
        public DbSet<ParkingSlot> ParkingSlots { get; set; } = null!;
        public DbSet<ParkingSpotImage> ParkingSpotImages { get; set; } = null!;
        public DbSet<PayPal> PayPals { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Traffic> Traffics { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<VehicleInfor> VehicleInfors { get; set; } = null!;
        public DbSet<Fee> Fees { get; set; } = null!;
        public DbSet<Bill> Bills { get; set; } = null!;
        public DbSet<Wallet> Wallets { get; set; } = null!;
        public DbSet<FieldWorkParkingImg> FieldWorkParkingImgs { get; set; } = null!;
        public DbSet<TimeSlot> TimeSlots { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;
        public DbSet<ApproveParking> ApproveParkings { get; set; } = null!;
        public DbSet<Domain.Entities.VnPay> VnPays { get; set; } = null!;
        public DbSet<BookingDetails> BookingDetails { get; set; }
        public DbSet<ConflictRequest> ConflictRequests { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Seed data sẽ được thêm tự động khi migration chạy
            SeedData(modelBuilder);
            
            //OnModelCreatingPartial(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Traffic (bổ sung thêm để tránh conflict với TrafficConfiguration)
            modelBuilder.Entity<Traffic>().HasData(
                new Traffic { TrafficId = 3, Name = "Xe đạp", IsActive = true }
            );

            // Seed Fees (bổ sung thêm để tránh conflict với FeeConfiguration)
            modelBuilder.Entity<Fee>().HasData(
                new Fee { FeeId = 3, BusinessType = "Doanh nghiệp lớn", Price = 200000, Name = "Phí đăng ký doanh nghiệp lớn", NumberOfParking = "21-50" },
                new Fee { FeeId = 4, BusinessType = "Tập đoàn", Price = 500000, Name = "Phí đăng ký tập đoàn", NumberOfParking = "50+" }
            );

            // Tạo password hash cho "123456"
            var passwordHash = CreatePasswordHash("123456");

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Name = "Admin",
                    Email = "admin@parkz.com",
                    PasswordHash = passwordHash.hash,
                    PasswordSalt = passwordHash.salt,
                    Phone = "0123456789",
                    RoleId = 1,
                    IsActive = true,
                    IsCensorship = true,
                    Avatar = "https://via.placeholder.com/150",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Gender = "Nam",
                    Address = "Hà Nội, Việt Nam"
                },
                new User
                {
                    UserId = 2,
                    Name = "Nguyễn Văn A",
                    Email = "manager1@parkz.com",
                    PasswordHash = passwordHash.hash,
                    PasswordSalt = passwordHash.salt,
                    Phone = "0123456788",
                    RoleId = 1,
                    IsActive = true,
                    IsCensorship = true,
                    Avatar = "https://via.placeholder.com/150",
                    DateOfBirth = new DateTime(1985, 5, 15),
                    Gender = "Nam",
                    Address = "TP. Hồ Chí Minh, Việt Nam"
                },
                new User
                {
                    UserId = 3,
                    Name = "Trần Thị B",
                    Email = "staff1@parkz.com",
                    PasswordHash = passwordHash.hash,
                    PasswordSalt = passwordHash.salt,
                    Phone = "0123456787",
                    RoleId = 4,
                    IsActive = true,
                    IsCensorship = true,
                    Avatar = "https://via.placeholder.com/150",
                    DateOfBirth = new DateTime(1992, 8, 20),
                    Gender = "Nữ",
                    Address = "Đà Nẵng, Việt Nam",
                    ManagerId = 2
                },
                new User
                {
                    UserId = 4,
                    Name = "Lê Văn C",
                    Email = "keeper1@parkz.com",
                    PasswordHash = passwordHash.hash,
                    PasswordSalt = passwordHash.salt,
                    Phone = "0123456786",
                    RoleId = 2,
                    IsActive = true,
                    IsCensorship = true,
                    Avatar = "https://via.placeholder.com/150",
                    DateOfBirth = new DateTime(1988, 3, 10),
                    Gender = "Nam",
                    Address = "Hải Phòng, Việt Nam",
                    ManagerId = 2
                },
                new User
                {
                    UserId = 5,
                    Name = "Phạm Thị D",
                    Email = "customer1@parkz.com",
                    PasswordHash = passwordHash.hash,
                    PasswordSalt = passwordHash.salt,
                    Phone = "0123456785",
                    RoleId = 3,
                    IsActive = true,
                    IsCensorship = true,
                    Avatar = "https://via.placeholder.com/150",
                    DateOfBirth = new DateTime(1995, 12, 5),
                    Gender = "Nữ",
                    Address = "Cần Thơ, Việt Nam"
                }
            );

            // Seed BusinessProfiles
            modelBuilder.Entity<BusinessProfile>().HasData(
                new BusinessProfile
                {
                    BusinessProfileId = 1,
                    Name = "Công ty TNHH Bãi đỗ xe ABC",
                    Address = "123 Đường ABC, Quận 1, TP. Hồ Chí Minh",
                    FrontIdentification = "https://via.placeholder.com/300x200",
                    BackIdentification = "https://via.placeholder.com/300x200",
                    BusinessLicense = "https://via.placeholder.com/300x200",
                    UserId = 2,
                    Type = "Doanh nghiệp",
                    FeeId = 2
                },
                new BusinessProfile
                {
                    BusinessProfileId = 2,
                    Name = "Bãi đỗ xe XYZ",
                    Address = "456 Đường XYZ, Quận 2, TP. Hồ Chí Minh",
                    FrontIdentification = "https://via.placeholder.com/300x200",
                    BackIdentification = "https://via.placeholder.com/300x200",
                    BusinessLicense = "https://via.placeholder.com/300x200",
                    UserId = 1,
                    Type = "Cá nhân",
                    FeeId = 1
                }
            );

            // Seed Parkings
            modelBuilder.Entity<Domain.Entities.Parking>().HasData(
                new Domain.Entities.Parking
                {
                    ParkingId = 1,
                    Code = "PARK001",
                    Name = "Bãi đỗ xe ABC - Tầng hầm",
                    Address = "123 Đường ABC, Quận 1, TP. Hồ Chí Minh",
                    Latitude = 10.7769m,
                    Longitude = 106.7009m,
                    Description = "Bãi đỗ xe hiện đại với hệ thống camera giám sát",
                    IsActive = true,
                    IsAvailable = true,
                    MotoSpot = 50,
                    CarSpot = 30,
                    IsFull = false,
                    IsPrepayment = true,
                    IsOvernight = true,
                    Stars = 4.5f,
                    TotalStars = 45f,
                    StarsCount = 10,
                    BusinessId = 1
                },
                new Domain.Entities.Parking
                {
                    ParkingId = 2,
                    Code = "PARK002",
                    Name = "Bãi đỗ xe XYZ - Tầng trệt",
                    Address = "456 Đường XYZ, Quận 2, TP. Hồ Chí Minh",
                    Latitude = 10.7879m,
                    Longitude = 106.7119m,
                    Description = "Bãi đỗ xe ngoài trời với giá cả hợp lý",
                    IsActive = true,
                    IsAvailable = true,
                    MotoSpot = 30,
                    CarSpot = 20,
                    IsFull = false,
                    IsPrepayment = false,
                    IsOvernight = false,
                    Stars = 4.0f,
                    TotalStars = 32f,
                    StarsCount = 8,
                    BusinessId = 2
                }
            );

            // Seed Floors
            modelBuilder.Entity<Floor>().HasData(
                new Floor { FloorId = 1, FloorName = "Tầng B1", IsActive = true, ParkingId = 1 },
                new Floor { FloorId = 2, FloorName = "Tầng B2", IsActive = true, ParkingId = 1 },
                new Floor { FloorId = 3, FloorName = "Tầng B3", IsActive = true, ParkingId = 1 },
                new Floor { FloorId = 4, FloorName = "Khu A", IsActive = true, ParkingId = 2 },
                new Floor { FloorId = 5, FloorName = "Khu B", IsActive = true, ParkingId = 2 }
            );

            // Seed Wallets (bổ sung thêm để tránh conflict với WalletConfiguration)
            modelBuilder.Entity<Wallet>().HasData(
                new Wallet { WalletId = 2, Balance = 500000, Debt = 0, UserId = 2 },
                new Wallet { WalletId = 3, Balance = 200000, Debt = 0, UserId = 3 },
                new Wallet { WalletId = 4, Balance = 150000, Debt = 0, UserId = 4 },
                new Wallet { WalletId = 5, Balance = 300000, Debt = 0, UserId = 5 }
            );

            // Seed ParkingSlots
            var parkingSlots = new List<ParkingSlot>();
            
            // Generate parking slots for Floor 1 (B1) - 20 car slots
            for (int i = 1; i <= 20; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    Name = $"A{i:D2}",
                    IsAvailable = true,
                    RowIndex = (i - 1) / 5 + 1,
                    ColumnIndex = (i - 1) % 5 + 1,
                    TrafficId = 1, // Car (from TrafficConfiguration)
                    FloorId = 1,
                    IsBackup = false
                });
            }

            // Generate parking slots for Floor 2 (B2) - 20 car slots
            for (int i = 21; i <= 40; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    Name = $"B{i - 20:D2}",
                    IsAvailable = true,
                    RowIndex = (i - 21) / 5 + 1,
                    ColumnIndex = (i - 21) % 5 + 1,
                    TrafficId = 1, // Car
                    FloorId = 2,
                    IsBackup = false
                });
            }

            // Generate parking slots for Floor 3 (B3) - 30 moto slots
            for (int i = 41; i <= 70; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    Name = $"M{i - 40:D2}",
                    IsAvailable = true,
                    RowIndex = (i - 41) / 10 + 1,
                    ColumnIndex = (i - 41) % 10 + 1,
                    TrafficId = 2, // Moto
                    FloorId = 3,
                    IsBackup = false
                });
            }

            // Generate parking slots for Floor 4 (Khu A) - 15 car slots
            for (int i = 71; i <= 85; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    Name = $"C{i - 70:D2}",
                    IsAvailable = true,
                    RowIndex = (i - 71) / 5 + 1,
                    ColumnIndex = (i - 71) % 5 + 1,
                    TrafficId = 1, // Car
                    FloorId = 4,
                    IsBackup = false
                });
            }

            // Generate parking slots for Floor 5 (Khu B) - 15 moto slots
            for (int i = 86; i <= 100; i++)
            {
                parkingSlots.Add(new ParkingSlot
                {
                    ParkingSlotId = i,
                    Name = $"D{i - 85:D2}",
                    IsAvailable = true,
                    RowIndex = (i - 86) / 5 + 1,
                    ColumnIndex = (i - 86) % 5 + 1,
                    TrafficId = 2, // Moto
                    FloorId = 5,
                    IsBackup = false
                });
            }

            modelBuilder.Entity<ParkingSlot>().HasData(parkingSlots.ToArray());

            // Seed ParkingPrices
            modelBuilder.Entity<ParkingPrice>().HasData(
                // Car prices for Business 1
                new ParkingPrice
                {
                    ParkingPriceId = 1,
                    ParkingPriceName = "Giá xe ô tô - Giờ",
                    IsActive = true,
                    IsWholeDay = false,
                    StartingTime = 0,
                    HasPenaltyPrice = true,
                    PenaltyPrice = 5000,
                    PenaltyPriceStepTime = 15,
                    IsExtrafee = true,
                    ExtraTimeStep = 15,
                    BusinessId = 1,
                    TrafficId = 1
                },
                new ParkingPrice
                {
                    ParkingPriceId = 2,
                    ParkingPriceName = "Giá xe ô tô - Ngày",
                    IsActive = true,
                    IsWholeDay = true,
                    StartingTime = 0,
                    HasPenaltyPrice = false,
                    PenaltyPrice = null,
                    PenaltyPriceStepTime = null,
                    IsExtrafee = false,
                    ExtraTimeStep = null,
                    BusinessId = 1,
                    TrafficId = 1
                },
                // Moto prices for Business 1
                new ParkingPrice
                {
                    ParkingPriceId = 3,
                    ParkingPriceName = "Giá xe máy - Giờ",
                    IsActive = true,
                    IsWholeDay = false,
                    StartingTime = 0,
                    HasPenaltyPrice = true,
                    PenaltyPrice = 2000,
                    PenaltyPriceStepTime = 15,
                    IsExtrafee = true,
                    ExtraTimeStep = 15,
                    BusinessId = 1,
                    TrafficId = 2
                },
                new ParkingPrice
                {
                    ParkingPriceId = 4,
                    ParkingPriceName = "Giá xe máy - Ngày",
                    IsActive = true,
                    IsWholeDay = true,
                    StartingTime = 0,
                    HasPenaltyPrice = false,
                    PenaltyPrice = null,
                    PenaltyPriceStepTime = null,
                    IsExtrafee = false,
                    ExtraTimeStep = null,
                    BusinessId = 1,
                    TrafficId = 2
                },
                // Car prices for Business 2
                new ParkingPrice
                {
                    ParkingPriceId = 5,
                    ParkingPriceName = "Giá xe ô tô - Giờ",
                    IsActive = true,
                    IsWholeDay = false,
                    StartingTime = 0,
                    HasPenaltyPrice = true,
                    PenaltyPrice = 3000,
                    PenaltyPriceStepTime = 15,
                    IsExtrafee = true,
                    ExtraTimeStep = 15,
                    BusinessId = 2,
                    TrafficId = 1
                },
                // Moto prices for Business 2
                new ParkingPrice
                {
                    ParkingPriceId = 6,
                    ParkingPriceName = "Giá xe máy - Giờ",
                    IsActive = true,
                    IsWholeDay = false,
                    StartingTime = 0,
                    HasPenaltyPrice = true,
                    PenaltyPrice = 1000,
                    PenaltyPriceStepTime = 15,
                    IsExtrafee = true,
                    ExtraTimeStep = 15,
                    BusinessId = 2,
                    TrafficId = 2
                }
            );

            // Seed ParkingHasPrices
            modelBuilder.Entity<ParkingHasPrice>().HasData(
                // Parking 1 has all prices
                new ParkingHasPrice { ParkingHasPriceId = 1, ParkingId = 1, ParkingPriceId = 1 },
                new ParkingHasPrice { ParkingHasPriceId = 2, ParkingId = 1, ParkingPriceId = 2 },
                new ParkingHasPrice { ParkingHasPriceId = 3, ParkingId = 1, ParkingPriceId = 3 },
                new ParkingHasPrice { ParkingHasPriceId = 4, ParkingId = 1, ParkingPriceId = 4 },
                
                // Parking 2 has hourly prices only
                new ParkingHasPrice { ParkingHasPriceId = 5, ParkingId = 2, ParkingPriceId = 5 },
                new ParkingHasPrice { ParkingHasPriceId = 6, ParkingId = 2, ParkingPriceId = 6 }
            );

            // Seed VehicleInfors
            modelBuilder.Entity<VehicleInfor>().HasData(
                new VehicleInfor
                {
                    VehicleInforId = 1,
                    LicensePlate = "29A1-12345",
                    VehicleName = "Honda Wave RSX",
                    Color = "Đỏ",
                    UserId = 5,
                    TrafficId = 2
                },
                new VehicleInfor
                {
                    VehicleInforId = 2,
                    LicensePlate = "30A1-67890",
                    VehicleName = "Toyota Vios",
                    Color = "Trắng",
                    UserId = 5,
                    TrafficId = 1
                }
            );

            // Seed TimeSlots
            modelBuilder.Entity<TimeSlot>().HasData(
                new TimeSlot
                {
                    TimeSlotId = 1,
                    StartTime = new DateTime(2024, 1, 1, 6, 0, 0),
                    EndTime = new DateTime(2024, 1, 1, 18, 0, 0),
                    Status = "Active",
                    CreatedDate = DateTime.Now,
                    ParkingSlotId = 1
                },
                new TimeSlot
                {
                    TimeSlotId = 2,
                    StartTime = new DateTime(2024, 1, 1, 18, 0, 0),
                    EndTime = new DateTime(2024, 1, 2, 6, 0, 0),
                    Status = "Active",
                    CreatedDate = DateTime.Now,
                    ParkingSlotId = 1
                }
            );
        }

        private (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            using var hmac = new System.Security.Cryptography.HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }

        public async Task SeedDataAsync()
        {
            var seedDataService = new SeedDataService(this);
            await seedDataService.SeedDataAsync();
        }
    }
}
