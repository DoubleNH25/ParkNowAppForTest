using Microsoft.EntityFrameworkCore;
using Parking.FindingSlotManagement.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Parking.FindingSlotManagement.Infrastructure.Persistences
{
    public class SeedDataService
    {
        private readonly ParkZDbContext _context;

        public SeedDataService(ParkZDbContext context)
        {
            _context = context;
        }

        public async Task SeedDataAsync()
        {
            // Ensure database is created
            await _context.Database.EnsureCreatedAsync();

            // Seed data in order of dependencies
            await SeedTrafficAsync();
            await SeedFeeAsync();
            await SeedUsersAsync();
            await SeedBusinessProfilesAsync();
            await SeedParkingsAsync();
            await SeedFloorsAsync();
            await SeedParkingSlotsAsync();
            await SeedParkingPricesAsync();
            await SeedParkingHasPricesAsync();
            await SeedWalletsAsync();

            await _context.SaveChangesAsync();
        }

        private async Task SeedTrafficAsync()
        {
            if (!await _context.Traffics.AnyAsync())
            {
                var traffics = new List<Traffic>
                {
                    new Traffic { TrafficId = 1, Name = "Xe máy", IsActive = true },
                    new Traffic { TrafficId = 2, Name = "Ô tô", IsActive = true },
                    new Traffic { TrafficId = 3, Name = "Xe đạp", IsActive = true }
                };

                await _context.Traffics.AddRangeAsync(traffics);
            }
        }

        private async Task SeedFeeAsync()
        {
            if (!await _context.Fees.AnyAsync())
            {
                var fees = new List<Fee>
                {
                    new Fee { FeeId = 1, BusinessType = "Cá nhân", Price = 50000, Name = "Phí đăng ký cá nhân", NumberOfParking = "1-5" },
                    new Fee { FeeId = 2, BusinessType = "Doanh nghiệp nhỏ", Price = 100000, Name = "Phí đăng ký doanh nghiệp nhỏ", NumberOfParking = "6-20" },
                    new Fee { FeeId = 3, BusinessType = "Doanh nghiệp lớn", Price = 200000, Name = "Phí đăng ký doanh nghiệp lớn", NumberOfParking = "21-50" },
                    new Fee { FeeId = 4, BusinessType = "Tập đoàn", Price = 500000, Name = "Phí đăng ký tập đoàn", NumberOfParking = "50+" }
                };

                await _context.Fees.AddRangeAsync(fees);
            }
        }

        private async Task SeedUsersAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                // Create password hash for default password "123456"
                var passwordHash = CreatePasswordHash("123456");

                var users = new List<User>
                {
                    // Admin user
                    new User
                    {
                        UserId = 1,
                        Name = "Admin",
                        Email = "admin@parkz.com",
                        PasswordHash = passwordHash.hash,
                        PasswordSalt = passwordHash.salt,
                        Phone = "0123456789",
                        RoleId = 1, // Manager role
                        IsActive = true,
                        IsCensorship = true,
                        Avatar = "https://via.placeholder.com/150",
                        DateOfBirth = new DateTime(1990, 1, 1),
                        Gender = "Nam",
                        Address = "Hà Nội, Việt Nam"
                    },
                    // Business Manager
                    new User
                    {
                        UserId = 2,
                        Name = "Nguyễn Văn A",
                        Email = "manager1@parkz.com",
                        PasswordHash = passwordHash.hash,
                        PasswordSalt = passwordHash.salt,
                        Phone = "0123456788",
                        RoleId = 1, // Manager role
                        IsActive = true,
                        IsCensorship = true,
                        Avatar = "https://via.placeholder.com/150",
                        DateOfBirth = new DateTime(1985, 5, 15),
                        Gender = "Nam",
                        Address = "TP. Hồ Chí Minh, Việt Nam"
                    },
                    // Staff
                    new User
                    {
                        UserId = 3,
                        Name = "Trần Thị B",
                        Email = "staff1@parkz.com",
                        PasswordHash = passwordHash.hash,
                        PasswordSalt = passwordHash.salt,
                        Phone = "0123456787",
                        RoleId = 4, // Staff role
                        IsActive = true,
                        IsCensorship = true,
                        Avatar = "https://via.placeholder.com/150",
                        DateOfBirth = new DateTime(1992, 8, 20),
                        Gender = "Nữ",
                        Address = "Đà Nẵng, Việt Nam",
                        ManagerId = 2
                    },
                    // Keeper
                    new User
                    {
                        UserId = 4,
                        Name = "Lê Văn C",
                        Email = "keeper1@parkz.com",
                        PasswordHash = passwordHash.hash,
                        PasswordSalt = passwordHash.salt,
                        Phone = "0123456786",
                        RoleId = 2, // Keeper role
                        IsActive = true,
                        IsCensorship = true,
                        Avatar = "https://via.placeholder.com/150",
                        DateOfBirth = new DateTime(1988, 3, 10),
                        Gender = "Nam",
                        Address = "Hải Phòng, Việt Nam",
                        ManagerId = 2
                    },
                    // Customer
                    new User
                    {
                        UserId = 5,
                        Name = "Phạm Thị D",
                        Email = "customer1@parkz.com",
                        PasswordHash = passwordHash.hash,
                        PasswordSalt = passwordHash.salt,
                        Phone = "0123456785",
                        RoleId = 3, // Customer role
                        IsActive = true,
                        IsCensorship = true,
                        Avatar = "https://via.placeholder.com/150",
                        DateOfBirth = new DateTime(1995, 12, 5),
                        Gender = "Nữ",
                        Address = "Cần Thơ, Việt Nam"
                    }
                };

                await _context.Users.AddRangeAsync(users);
            }
        }

        private async Task SeedBusinessProfilesAsync()
        {
            if (!await _context.BusinessProfiles.AnyAsync())
            {
                var businessProfiles = new List<BusinessProfile>
                {
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
                };

                await _context.BusinessProfiles.AddRangeAsync(businessProfiles);
            }
        }

        private async Task SeedParkingsAsync()
        {
            if (!await _context.Parkings.AnyAsync())
            {
                var parkings = new List<Domain.Entities.Parking>
                {
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
                };

                await _context.Parkings.AddRangeAsync(parkings);
            }
        }

        private async Task SeedFloorsAsync()
        {
            if (!await _context.Floors.AnyAsync())
            {
                var floors = new List<Floor>
                {
                    // Floors for Parking 1
                    new Floor { FloorId = 1, FloorName = "Tầng B1", IsActive = true, ParkingId = 1 },
                    new Floor { FloorId = 2, FloorName = "Tầng B2", IsActive = true, ParkingId = 1 },
                    new Floor { FloorId = 3, FloorName = "Tầng B3", IsActive = true, ParkingId = 1 },
                    
                    // Floors for Parking 2
                    new Floor { FloorId = 4, FloorName = "Khu A", IsActive = true, ParkingId = 2 },
                    new Floor { FloorId = 5, FloorName = "Khu B", IsActive = true, ParkingId = 2 }
                };

                await _context.Floors.AddRangeAsync(floors);
            }
        }

        private async Task SeedParkingSlotsAsync()
        {
            if (!await _context.ParkingSlots.AnyAsync())
            {
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
                        TrafficId = 2, // Car
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
                        TrafficId = 2, // Car
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
                        TrafficId = 1, // Moto
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
                        TrafficId = 2, // Car
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
                        TrafficId = 1, // Moto
                        FloorId = 5,
                        IsBackup = false
                    });
                }

                await _context.ParkingSlots.AddRangeAsync(parkingSlots);
            }
        }

        private async Task SeedParkingPricesAsync()
        {
            if (!await _context.ParkingPrices.AnyAsync())
            {
                var parkingPrices = new List<ParkingPrice>
                {
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
                        TrafficId = 2
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
                        TrafficId = 2
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
                        TrafficId = 1
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
                        TrafficId = 1
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
                        TrafficId = 2
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
                        TrafficId = 1
                    }
                };

                await _context.ParkingPrices.AddRangeAsync(parkingPrices);
            }
        }

        private async Task SeedParkingHasPricesAsync()
        {
            if (!await _context.ParkingHasPrices.AnyAsync())
            {
                var parkingHasPrices = new List<ParkingHasPrice>
                {
                    // Parking 1 has all prices
                    new ParkingHasPrice { ParkingHasPriceId = 1, ParkingId = 1, ParkingPriceId = 1 },
                    new ParkingHasPrice { ParkingHasPriceId = 2, ParkingId = 1, ParkingPriceId = 2 },
                    new ParkingHasPrice { ParkingHasPriceId = 3, ParkingId = 1, ParkingPriceId = 3 },
                    new ParkingHasPrice { ParkingHasPriceId = 4, ParkingId = 1, ParkingPriceId = 4 },
                    
                    // Parking 2 has hourly prices only
                    new ParkingHasPrice { ParkingHasPriceId = 5, ParkingId = 2, ParkingPriceId = 5 },
                    new ParkingHasPrice { ParkingHasPriceId = 6, ParkingId = 2, ParkingPriceId = 6 }
                };

                await _context.ParkingHasPrices.AddRangeAsync(parkingHasPrices);
            }
        }

        private async Task SeedWalletsAsync()
        {
            if (!await _context.Wallets.AnyAsync())
            {
                var wallets = new List<Wallet>
                {
                    new Wallet { WalletId = 1, Balance = 1000000, Debt = 0, UserId = 1 },
                    new Wallet { WalletId = 2, Balance = 500000, Debt = 0, UserId = 2 },
                    new Wallet { WalletId = 3, Balance = 200000, Debt = 0, UserId = 3 },
                    new Wallet { WalletId = 4, Balance = 150000, Debt = 0, UserId = 4 },
                    new Wallet { WalletId = 5, Balance = 300000, Debt = 0, UserId = 5 }
                };

                await _context.Wallets.AddRangeAsync(wallets);
            }
        }

        private (byte[] hash, byte[] salt) CreatePasswordHash(string password)
        {
            using var hmac = new HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }
    }
}
