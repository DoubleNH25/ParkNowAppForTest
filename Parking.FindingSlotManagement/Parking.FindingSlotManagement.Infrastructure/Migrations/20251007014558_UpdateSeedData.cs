using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Parking.FindingSlotManagement.Infrastructure.Migrations
{
    public partial class UpdateSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConflictRequests",
                columns: table => new
                {
                    ConflictRequestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConflictRequests", x => x.ConflictRequestId);
                });

            migrationBuilder.CreateTable(
                name: "Fees",
                columns: table => new
                {
                    FeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfParking = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fees", x => x.FeeId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    TrafficId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.TrafficId);
                });

            migrationBuilder.CreateTable(
                name: "ApproveParkings",
                columns: table => new
                {
                    ApproveParkingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteForAdmin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: true),
                    ParkingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproveParkings", x => x.ApproveParkingId);
                });

            migrationBuilder.CreateTable(
                name: "FieldWorkParkingImgs",
                columns: table => new
                {
                    FieldWorkParkingImgId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproveParkingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldWorkParkingImgs", x => x.FieldWorkParkingImgId);
                    table.ForeignKey(
                        name: "FK_ApprovePar_FieldWorkPas",
                        column: x => x.ApproveParkingId,
                        principalTable: "ApproveParkings",
                        principalColumn: "ApproveParkingId");
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    BillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BusinessId = table.Column<int>(type: "int", nullable: true),
                    WalletId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.BillId);
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckinTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckoutTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateBook = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    GuestName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GuestPhone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    TotalPrice = table.Column<decimal>(type: "money", nullable: true),
                    QRImage = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UnPaidMoney = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsRating = table.Column<bool>(type: "bit", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    VehicleInforID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingID);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    BookingDetailsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeSlotId = table.Column<int>(type: "int", nullable: true),
                    BookingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.BookingDetailsId);
                    table.ForeignKey(
                        name: "FK__Booking__BookingDetails",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "BookingID");
                });

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    BusinessProfileId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FrontIdentification = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BackIdentification = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    BusinessLicense = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.BusinessProfileId);
                    table.ForeignKey(
                        name: "FK_Fee_BusinessProfiles",
                        column: x => x.FeeId,
                        principalTable: "Fees",
                        principalColumn: "FeeId");
                });

            migrationBuilder.CreateTable(
                name: "Parking",
                columns: table => new
                {
                    ParkingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Latitude = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    Longitude = table.Column<decimal>(type: "decimal(10,6)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    MotoSpot = table.Column<int>(type: "int", nullable: true),
                    CarSpot = table.Column<int>(type: "int", nullable: true),
                    IsFull = table.Column<bool>(type: "bit", nullable: true),
                    IsPrepayment = table.Column<bool>(type: "bit", nullable: true),
                    IsOvernight = table.Column<bool>(type: "bit", nullable: true),
                    Stars = table.Column<float>(type: "real", nullable: true),
                    TotalStars = table.Column<float>(type: "real", nullable: true),
                    StarsCount = table.Column<int>(type: "int", nullable: true),
                    BusinessId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parking", x => x.ParkingId);
                    table.ForeignKey(
                        name: "FK__BusiessPro__Parking",
                        column: x => x.BusinessId,
                        principalTable: "Business",
                        principalColumn: "BusinessProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingPrice",
                columns: table => new
                {
                    ParkingPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingPriceName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsWholeDay = table.Column<bool>(type: "bit", nullable: false),
                    StartingTime = table.Column<int>(type: "int", nullable: true),
                    HasPenaltyPrice = table.Column<bool>(type: "bit", nullable: true),
                    PenaltyPrice = table.Column<decimal>(type: "money", nullable: true),
                    PenaltyPriceStepTime = table.Column<float>(type: "real", nullable: true),
                    IsExtrafee = table.Column<bool>(type: "bit", nullable: true),
                    ExtraTimeStep = table.Column<float>(type: "real", nullable: true),
                    BusinessId = table.Column<int>(type: "int", nullable: true),
                    TrafficId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingPrice", x => x.ParkingPriceId);
                    table.ForeignKey(
                        name: "FK__Business__ParkingPri",
                        column: x => x.BusinessId,
                        principalTable: "Business",
                        principalColumn: "BusinessProfileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__VehicleTy_Parkingpri",
                        column: x => x.TrafficId,
                        principalTable: "VehicleType",
                        principalColumn: "TrafficId");
                });

            migrationBuilder.CreateTable(
                name: "Floors",
                columns: table => new
                {
                    FloorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FloorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    ParkingID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Floors", x => x.FloorId);
                    table.ForeignKey(
                        name: "FK__Floors__ParkingI__47DBAE45",
                        column: x => x.ParkingID,
                        principalTable: "Parking",
                        principalColumn: "ParkingId");
                });

            migrationBuilder.CreateTable(
                name: "ParkingSpotImage",
                columns: table => new
                {
                    ParkingSpotImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImgPath = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ParkingID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSpotImage", x => x.ParkingSpotImageId);
                    table.ForeignKey(
                        name: "FK__ParkingSp__Parki__3C69FB99",
                        column: x => x.ParkingID,
                        principalTable: "Parking",
                        principalColumn: "ParkingId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(255)", maxLength: 255, nullable: true),
                    Phone = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    Avatar = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    Devicetoken = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    IsCensorship = table.Column<bool>(type: "bit", nullable: true),
                    ManagerID = table.Column<int>(type: "int", nullable: true),
                    RoleID = table.Column<int>(type: "int", nullable: true),
                    IdCardNo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IdCardDate = table.Column<DateTime>(type: "date", nullable: true),
                    IdCardIssuedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BanCount = table.Column<int>(type: "int", nullable: true),
                    ParkingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK__Parking__Users",
                        column: x => x.ParkingId,
                        principalTable: "Parking",
                        principalColumn: "ParkingId");
                    table.ForeignKey(
                        name: "FK__Users__ManagerID__267ABA7A",
                        column: x => x.ManagerID,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK__Users__RoleID__276EDEB3",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "ParkingHasPrice",
                columns: table => new
                {
                    ParkingHasPriceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParkingId = table.Column<int>(type: "int", nullable: true),
                    ParkingPriceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingHasPrice", x => x.ParkingHasPriceId);
                    table.ForeignKey(
                        name: "FK_ParkingHasPrice_Parking",
                        column: x => x.ParkingId,
                        principalTable: "Parking",
                        principalColumn: "ParkingId");
                    table.ForeignKey(
                        name: "FK_ParkingHasPrice_ParkingPrice",
                        column: x => x.ParkingPriceId,
                        principalTable: "ParkingPrice",
                        principalColumn: "ParkingPriceId");
                });

            migrationBuilder.CreateTable(
                name: "TimeLine",
                columns: table => new
                {
                    TimeLineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    ExtraFee = table.Column<decimal>(type: "money", nullable: true),
                    ParkingPriceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLine", x => x.TimeLineId);
                    table.ForeignKey(
                        name: "FK_Timeline_ParkingPrice",
                        column: x => x.ParkingPriceId,
                        principalTable: "ParkingPrice",
                        principalColumn: "ParkingPriceId");
                });

            migrationBuilder.CreateTable(
                name: "ParkingSlots",
                columns: table => new
                {
                    ParkingSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: true),
                    RowIndex = table.Column<int>(type: "int", nullable: true),
                    ColumnIndex = table.Column<int>(type: "int", nullable: true),
                    TrafficID = table.Column<int>(type: "int", nullable: true),
                    FloorID = table.Column<int>(type: "int", nullable: true),
                    IsBackup = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingSlots", x => x.ParkingSlotId);
                    table.ForeignKey(
                        name: "FK__ParkingSl__Floor__5441852A",
                        column: x => x.FloorID,
                        principalTable: "Floors",
                        principalColumn: "FloorId");
                    table.ForeignKey(
                        name: "FK__ParkingSl__Traff",
                        column: x => x.TrafficID,
                        principalTable: "VehicleType",
                        principalColumn: "TrafficId");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteAddress",
                columns: table => new
                {
                    FavoriteAddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TagName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteAddress", x => x.FavoriteAddressId);
                    table.ForeignKey(
                        name: "FK__FavoriteA__UserI__33D4B598",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "PayPal",
                columns: table => new
                {
                    PayPalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    SecretKey = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ManagerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayPal", x => x.PayPalId);
                    table.ForeignKey(
                        name: "FK__PayPal__ManagerI__2D27B809",
                        column: x => x.ManagerID,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    VehicleInforId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlate = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    VehicleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(225)", maxLength: 225, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true),
                    TrafficID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.VehicleInforId);
                    table.ForeignKey(
                        name: "FK__VehicleIn__Traff",
                        column: x => x.TrafficID,
                        principalTable: "VehicleType",
                        principalColumn: "TrafficId");
                    table.ForeignKey(
                        name: "FK__VehicleIn__UserI__4AB81AF0",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "VnPay",
                columns: table => new
                {
                    VnPayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TmnCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    HashSecret = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VnPay", x => x.VnPayId);
                    table.ForeignKey(
                        name: "FK__VnPay__User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    WalletId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Debt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.WalletId);
                    table.ForeignKey(
                        name: "FK_Wallets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "TimeSlot",
                columns: table => new
                {
                    TimeSlotId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParkingSlotId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlot", x => x.TimeSlotId);
                    table.ForeignKey(
                        name: "FK_Parkingslot_BookedSlots",
                        column: x => x.ParkingSlotId,
                        principalTable: "ParkingSlots",
                        principalColumn: "ParkingSlotId");
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WalletId = table.Column<int>(type: "int", nullable: true),
                    BookingId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Booking_BookingPayments",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "BookingID");
                    table.ForeignKey(
                        name: "FK_Wallet_BookingPayments",
                        column: x => x.WalletId,
                        principalTable: "Wallets",
                        principalColumn: "WalletId");
                });

            migrationBuilder.InsertData(
                table: "ConflictRequests",
                columns: new[] { "ConflictRequestId", "BookingId", "Message", "ParkingId", "Status" },
                values: new object[] { 1, 1, "Tranh chấp về thời gian giữ chỗ", 1, "Resolved" });

            migrationBuilder.InsertData(
                table: "Fees",
                columns: new[] { "FeeId", "BusinessType", "Name", "NumberOfParking", "Price" },
                values: new object[,]
                {
                    { 1, "Cá nhân", "Phí đăng ký cá nhân", "1-5", 50000m },
                    { 2, "Doanh nghiệp nhỏ", "Phí đăng ký doanh nghiệp nhỏ", "6-20", 100000m },
                    { 3, "Doanh nghiệp lớn", "Phí đăng ký doanh nghiệp lớn", "21-50", 200000m },
                    { 4, "Tập đoàn", "Phí đăng ký tập đoàn", "50+", 500000m }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Manager" },
                    { 2, true, "Keeper" },
                    { 3, true, "Customer" },
                    { 4, true, "Staff" }
                });

            migrationBuilder.InsertData(
                table: "VehicleType",
                columns: new[] { "TrafficId", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, true, "Xe máy" },
                    { 2, true, "Ô tô" },
                    { 3, true, "Xe đạp" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "BanCount", "DateOfBirth", "Devicetoken", "Email", "Gender", "IdCardDate", "IdCardIssuedBy", "IdCardNo", "IsActive", "IsCensorship", "ManagerID", "Name", "ParkingId", "PasswordHash", "PasswordSalt", "Phone", "RoleID" },
                values: new object[] { 1, "Hà Nội, Việt Nam", "https://via.placeholder.com/150", null, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "admin@parkz.com", "Nam", null, null, null, true, true, null, "Admin", null, new byte[] { 17, 72, 207, 34, 131, 134, 149, 214, 106, 59, 180, 129, 68, 17, 50, 198, 202, 157, 230, 112, 247, 5, 157, 38, 49, 6, 229, 167, 136, 44, 223, 252, 30, 26, 201, 88, 175, 169, 31, 69, 4, 183, 154, 17, 252, 187, 35, 229, 236, 55, 201, 219, 112, 246, 249, 7, 178, 118, 87, 135, 14, 40, 59, 89 }, new byte[] { 2, 67, 113, 205, 13, 120, 198, 181, 35, 250, 246, 139, 46, 165, 99, 213, 240, 227, 240, 89, 160, 232, 79, 177, 124, 154, 180, 72, 134, 193, 144, 108, 167, 103, 100, 43, 205, 156, 155, 228, 203, 190, 3, 178, 229, 147, 228, 85, 179, 72, 135, 44, 18, 34, 97, 57, 131, 172, 231, 188, 164, 239, 213, 205, 185, 123, 247, 23, 184, 74, 15, 160, 189, 201, 1, 215, 163, 148, 204, 235, 40, 64, 119, 16, 160, 38, 213, 187, 180, 32, 68, 229, 140, 234, 97, 20, 170, 119, 255, 142, 61, 73, 227, 112, 116, 10, 153, 33, 213, 46, 179, 21, 167, 117, 130, 59, 206, 176, 213, 146, 123, 124, 184, 233, 162, 142, 112, 12 }, "0123456789", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "BanCount", "DateOfBirth", "Devicetoken", "Email", "Gender", "IdCardDate", "IdCardIssuedBy", "IdCardNo", "IsActive", "IsCensorship", "ManagerID", "Name", "ParkingId", "PasswordHash", "PasswordSalt", "Phone", "RoleID" },
                values: new object[] { 2, "TP. Hồ Chí Minh, Việt Nam", "https://via.placeholder.com/150", null, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "manager1@parkz.com", "Nam", null, null, null, true, true, null, "Nguyễn Văn A", null, new byte[] { 17, 72, 207, 34, 131, 134, 149, 214, 106, 59, 180, 129, 68, 17, 50, 198, 202, 157, 230, 112, 247, 5, 157, 38, 49, 6, 229, 167, 136, 44, 223, 252, 30, 26, 201, 88, 175, 169, 31, 69, 4, 183, 154, 17, 252, 187, 35, 229, 236, 55, 201, 219, 112, 246, 249, 7, 178, 118, 87, 135, 14, 40, 59, 89 }, new byte[] { 2, 67, 113, 205, 13, 120, 198, 181, 35, 250, 246, 139, 46, 165, 99, 213, 240, 227, 240, 89, 160, 232, 79, 177, 124, 154, 180, 72, 134, 193, 144, 108, 167, 103, 100, 43, 205, 156, 155, 228, 203, 190, 3, 178, 229, 147, 228, 85, 179, 72, 135, 44, 18, 34, 97, 57, 131, 172, 231, 188, 164, 239, 213, 205, 185, 123, 247, 23, 184, 74, 15, 160, 189, 201, 1, 215, 163, 148, 204, 235, 40, 64, 119, 16, 160, 38, 213, 187, 180, 32, 68, 229, 140, 234, 97, 20, 170, 119, 255, 142, 61, 73, 227, 112, 116, 10, 153, 33, 213, 46, 179, 21, 167, 117, 130, 59, 206, 176, 213, 146, 123, 124, 184, 233, 162, 142, 112, 12 }, "0123456788", 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "BanCount", "DateOfBirth", "Devicetoken", "Email", "Gender", "IdCardDate", "IdCardIssuedBy", "IdCardNo", "IsActive", "IsCensorship", "ManagerID", "Name", "ParkingId", "PasswordHash", "PasswordSalt", "Phone", "RoleID" },
                values: new object[] { 5, "Cần Thơ, Việt Nam", "https://via.placeholder.com/150", null, new DateTime(1995, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "customer1@parkz.com", "Nữ", null, null, null, true, true, null, "Phạm Thị D", null, new byte[] { 17, 72, 207, 34, 131, 134, 149, 214, 106, 59, 180, 129, 68, 17, 50, 198, 202, 157, 230, 112, 247, 5, 157, 38, 49, 6, 229, 167, 136, 44, 223, 252, 30, 26, 201, 88, 175, 169, 31, 69, 4, 183, 154, 17, 252, 187, 35, 229, 236, 55, 201, 219, 112, 246, 249, 7, 178, 118, 87, 135, 14, 40, 59, 89 }, new byte[] { 2, 67, 113, 205, 13, 120, 198, 181, 35, 250, 246, 139, 46, 165, 99, 213, 240, 227, 240, 89, 160, 232, 79, 177, 124, 154, 180, 72, 134, 193, 144, 108, 167, 103, 100, 43, 205, 156, 155, 228, 203, 190, 3, 178, 229, 147, 228, 85, 179, 72, 135, 44, 18, 34, 97, 57, 131, 172, 231, 188, 164, 239, 213, 205, 185, 123, 247, 23, 184, 74, 15, 160, 189, 201, 1, 215, 163, 148, 204, 235, 40, 64, 119, 16, 160, 38, 213, 187, 180, 32, 68, 229, 140, 234, 97, 20, 170, 119, 255, 142, 61, 73, 227, 112, 116, 10, 153, 33, 213, 46, 179, 21, 167, 117, 130, 59, 206, 176, 213, 146, 123, 124, 184, 233, 162, 142, 112, 12 }, "0123456785", 3 });

            migrationBuilder.InsertData(
                table: "Business",
                columns: new[] { "BusinessProfileId", "Address", "BackIdentification", "BusinessLicense", "FeeId", "FrontIdentification", "Name", "Type", "UserID" },
                values: new object[,]
                {
                    { 1, "123 Đường ABC, Quận 1, TP. Hồ Chí Minh", "https://via.placeholder.com/300x200", "https://via.placeholder.com/300x200", 2, "https://via.placeholder.com/300x200", "Công ty TNHH Bãi đỗ xe ABC", "Doanh nghiệp", 2 },
                    { 2, "456 Đường XYZ, Quận 2, TP. Hồ Chí Minh", "https://via.placeholder.com/300x200", "https://via.placeholder.com/300x200", 1, "https://via.placeholder.com/300x200", "Bãi đỗ xe XYZ", "Cá nhân", 1 }
                });

            migrationBuilder.InsertData(
                table: "FavoriteAddress",
                columns: new[] { "FavoriteAddressId", "Address", "TagName", "UserID" },
                values: new object[] { 1, "01 Lê Lợi, Đà Nẵng", "Nhà", 5 });

            migrationBuilder.InsertData(
                table: "PayPal",
                columns: new[] { "PayPalId", "ClientId", "ManagerID", "SecretKey" },
                values: new object[] { 1, "sample-client-id", 2, "sample-secret" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Address", "Avatar", "BanCount", "DateOfBirth", "Devicetoken", "Email", "Gender", "IdCardDate", "IdCardIssuedBy", "IdCardNo", "IsActive", "IsCensorship", "ManagerID", "Name", "ParkingId", "PasswordHash", "PasswordSalt", "Phone", "RoleID" },
                values: new object[,]
                {
                    { 3, "Đà Nẵng, Việt Nam", "https://via.placeholder.com/150", null, new DateTime(1992, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "staff1@parkz.com", "Nữ", null, null, null, true, true, 2, "Trần Thị B", null, new byte[] { 17, 72, 207, 34, 131, 134, 149, 214, 106, 59, 180, 129, 68, 17, 50, 198, 202, 157, 230, 112, 247, 5, 157, 38, 49, 6, 229, 167, 136, 44, 223, 252, 30, 26, 201, 88, 175, 169, 31, 69, 4, 183, 154, 17, 252, 187, 35, 229, 236, 55, 201, 219, 112, 246, 249, 7, 178, 118, 87, 135, 14, 40, 59, 89 }, new byte[] { 2, 67, 113, 205, 13, 120, 198, 181, 35, 250, 246, 139, 46, 165, 99, 213, 240, 227, 240, 89, 160, 232, 79, 177, 124, 154, 180, 72, 134, 193, 144, 108, 167, 103, 100, 43, 205, 156, 155, 228, 203, 190, 3, 178, 229, 147, 228, 85, 179, 72, 135, 44, 18, 34, 97, 57, 131, 172, 231, 188, 164, 239, 213, 205, 185, 123, 247, 23, 184, 74, 15, 160, 189, 201, 1, 215, 163, 148, 204, 235, 40, 64, 119, 16, 160, 38, 213, 187, 180, 32, 68, 229, 140, 234, 97, 20, 170, 119, 255, 142, 61, 73, 227, 112, 116, 10, 153, 33, 213, 46, 179, 21, 167, 117, 130, 59, 206, 176, 213, 146, 123, 124, 184, 233, 162, 142, 112, 12 }, "0123456787", 4 },
                    { 4, "Hải Phòng, Việt Nam", "https://via.placeholder.com/150", null, new DateTime(1988, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "keeper1@parkz.com", "Nam", null, null, null, true, true, 2, "Lê Văn C", null, new byte[] { 17, 72, 207, 34, 131, 134, 149, 214, 106, 59, 180, 129, 68, 17, 50, 198, 202, 157, 230, 112, 247, 5, 157, 38, 49, 6, 229, 167, 136, 44, 223, 252, 30, 26, 201, 88, 175, 169, 31, 69, 4, 183, 154, 17, 252, 187, 35, 229, 236, 55, 201, 219, 112, 246, 249, 7, 178, 118, 87, 135, 14, 40, 59, 89 }, new byte[] { 2, 67, 113, 205, 13, 120, 198, 181, 35, 250, 246, 139, 46, 165, 99, 213, 240, 227, 240, 89, 160, 232, 79, 177, 124, 154, 180, 72, 134, 193, 144, 108, 167, 103, 100, 43, 205, 156, 155, 228, 203, 190, 3, 178, 229, 147, 228, 85, 179, 72, 135, 44, 18, 34, 97, 57, 131, 172, 231, 188, 164, 239, 213, 205, 185, 123, 247, 23, 184, 74, 15, 160, 189, 201, 1, 215, 163, 148, 204, 235, 40, 64, 119, 16, 160, 38, 213, 187, 180, 32, 68, 229, 140, 234, 97, 20, 170, 119, 255, 142, 61, 73, 227, 112, 116, 10, 153, 33, 213, 46, 179, 21, 167, 117, 130, 59, 206, 176, 213, 146, 123, 124, 184, 233, 162, 142, 112, 12 }, "0123456786", 2 }
                });

            migrationBuilder.InsertData(
                table: "Vehicle",
                columns: new[] { "VehicleInforId", "Color", "LicensePlate", "TrafficID", "UserID", "VehicleName" },
                values: new object[,]
                {
                    { 1, "Đỏ", "29A1-12345", 1, 5, "Honda Wave RSX" },
                    { 2, "Trắng", "30A1-67890", 2, 5, "Toyota Vios" }
                });

            migrationBuilder.InsertData(
                table: "VnPay",
                columns: new[] { "VnPayId", "HashSecret", "TmnCode", "UserId" },
                values: new object[] { 1, "HASHSECRET456", "TMNCODE123", 2 });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "WalletId", "Balance", "Debt", "UserId" },
                values: new object[,]
                {
                    { 1, 1000000m, 0m, 1 },
                    { 2, 500000m, 0m, 2 },
                    { 5, 300000m, 0m, 5 }
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "BillId", "BusinessId", "Price", "Status", "Time", "WalletId" },
                values: new object[] { 1, 1, 30000m, "Paid", new DateTime(2024, 1, 10, 10, 5, 0, 0, DateTimeKind.Utc), 5 });

            migrationBuilder.InsertData(
                table: "Booking",
                columns: new[] { "BookingID", "CheckinTime", "CheckoutTime", "DateBook", "EndTime", "GuestName", "GuestPhone", "IsRating", "QRImage", "StartTime", "Status", "TotalPrice", "UnPaidMoney", "UserID", "VehicleInforID" },
                values: new object[] { 1, new DateTime(2024, 1, 10, 8, 5, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 10, 10, 2, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 9, 12, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 10, 10, 0, 0, 0, DateTimeKind.Utc), "Pham Thi D", "0123456785", true, "https://via.placeholder.com/150", new DateTime(2024, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), "Completed", 30000m, 0m, 5, 1 });

            migrationBuilder.InsertData(
                table: "Parking",
                columns: new[] { "ParkingId", "Address", "BusinessId", "CarSpot", "Code", "Description", "IsActive", "IsAvailable", "IsFull", "IsOvernight", "IsPrepayment", "Latitude", "Longitude", "MotoSpot", "Name", "Stars", "StarsCount", "TotalStars" },
                values: new object[,]
                {
                    { 1, "123 Đường ABC, Quận 1, TP. Hồ Chí Minh", 1, 30, "PARK001", "Bãi đỗ xe hiện đại với hệ thống camera giám sát", true, true, false, true, true, 10.7769m, 106.7009m, 50, "Bãi đỗ xe ABC - Tầng hầm", 4.5f, 10, 45f },
                    { 2, "456 Đường XYZ, Quận 2, TP. Hồ Chí Minh", 2, 20, "PARK002", "Bãi đỗ xe ngoài trời với giá cả hợp lý", true, true, false, false, false, 10.7879m, 106.7119m, 30, "Bãi đỗ xe XYZ - Tầng trệt", 4f, 8, 32f }
                });

            migrationBuilder.InsertData(
                table: "ParkingPrice",
                columns: new[] { "ParkingPriceId", "BusinessId", "ExtraTimeStep", "HasPenaltyPrice", "IsActive", "IsExtrafee", "IsWholeDay", "ParkingPriceName", "PenaltyPrice", "PenaltyPriceStepTime", "StartingTime", "TrafficId" },
                values: new object[,]
                {
                    { 1, 1, 15f, true, true, true, false, "Giá xe ô tô - Giờ", 5000m, 15f, 0, 2 },
                    { 2, 1, null, false, true, false, true, "Giá xe ô tô - Ngày", null, null, 0, 2 },
                    { 3, 1, 15f, true, true, true, false, "Giá xe máy - Giờ", 2000m, 15f, 0, 1 },
                    { 4, 1, null, false, true, false, true, "Giá xe máy - Ngày", null, null, 0, 1 },
                    { 5, 2, 15f, true, true, true, false, "Giá xe ô tô - Giờ", 3000m, 15f, 0, 2 },
                    { 6, 2, 15f, true, true, true, false, "Giá xe máy - Giờ", 1000m, 15f, 0, 1 }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "WalletId", "Balance", "Debt", "UserId" },
                values: new object[,]
                {
                    { 3, 200000m, 0m, 3 },
                    { 4, 150000m, 0m, 4 }
                });

            migrationBuilder.InsertData(
                table: "ApproveParkings",
                columns: new[] { "ApproveParkingId", "CreatedDate", "Note", "NoteForAdmin", "ParkingId", "StaffId", "Status" },
                values: new object[] { 1, new DateTime(2024, 1, 5, 9, 0, 0, 0, DateTimeKind.Utc), "Giám sát hiện trường", "Hồ sơ đạt yêu cầu", 1, 3, "Approved" });

            migrationBuilder.InsertData(
                table: "Floors",
                columns: new[] { "FloorId", "FloorName", "IsActive", "ParkingID" },
                values: new object[,]
                {
                    { 1, "Tầng B1", true, 1 },
                    { 2, "Tầng B2", true, 1 },
                    { 3, "Tầng B3", true, 1 },
                    { 4, "Khu A", true, 2 },
                    { 5, "Khu B", true, 2 }
                });

            migrationBuilder.InsertData(
                table: "ParkingHasPrice",
                columns: new[] { "ParkingHasPriceId", "ParkingId", "ParkingPriceId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 1, 2 },
                    { 3, 1, 3 },
                    { 4, 1, 4 },
                    { 5, 2, 5 },
                    { 6, 2, 6 }
                });

            migrationBuilder.InsertData(
                table: "ParkingSpotImage",
                columns: new[] { "ParkingSpotImageId", "ImgPath", "ParkingID" },
                values: new object[,]
                {
                    { 1, "https://via.placeholder.com/600x400", 1 },
                    { 2, "https://via.placeholder.com/600x400", 2 }
                });

            migrationBuilder.InsertData(
                table: "TimeLine",
                columns: new[] { "TimeLineId", "Description", "EndTime", "ExtraFee", "IsActive", "Name", "ParkingPriceId", "Price", "StartTime" },
                values: new object[,]
                {
                    { 1, "Giá giờ đầu cho ô tô", new TimeSpan(0, 1, 0, 0, 0), 0m, true, "Ô tô - Giờ đầu", 1, 15000m, new TimeSpan(0, 0, 0, 0, 0) },
                    { 2, "Phụ phí mỗi 30 phút sau giờ đầu", new TimeSpan(0, 23, 59, 59, 0), 0m, true, "Ô tô - Mỗi 30 phút tiếp theo", 1, 5000m, new TimeSpan(0, 1, 0, 0, 0) },
                    { 3, "Giá giờ đầu cho xe máy", new TimeSpan(0, 1, 0, 0, 0), 0m, true, "Xe máy - Giờ đầu", 3, 5000m, new TimeSpan(0, 0, 0, 0, 0) },
                    { 4, "Phụ phí mỗi 30 phút sau giờ đầu", new TimeSpan(0, 23, 59, 59, 0), 0m, true, "Xe máy - Mỗi 30 phút tiếp theo", 3, 2000m, new TimeSpan(0, 1, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "TransactionId", "BookingId", "CreatedDate", "Description", "PaymentMethod", "Price", "Status", "WalletId" },
                values: new object[] { 1, 1, new DateTime(2024, 1, 10, 10, 2, 0, 0, DateTimeKind.Utc), "Payment on checkout", "Cash", 30000m, "Success", 5 });

            migrationBuilder.InsertData(
                table: "FieldWorkParkingImgs",
                columns: new[] { "FieldWorkParkingImgId", "ApproveParkingId", "Url" },
                values: new object[] { 1, 1, "https://via.placeholder.com/400x300" });

            migrationBuilder.InsertData(
                table: "ParkingSlots",
                columns: new[] { "ParkingSlotId", "ColumnIndex", "FloorID", "IsAvailable", "IsBackup", "Name", "RowIndex", "TrafficID" },
                values: new object[,]
                {
                    { 1, 1, 1, true, false, "A01", 1, 2 },
                    { 2, 2, 1, true, false, "A02", 1, 2 },
                    { 3, 3, 1, true, false, "A03", 1, 2 },
                    { 4, 4, 1, true, false, "A04", 1, 2 },
                    { 5, 5, 1, true, false, "A05", 1, 2 },
                    { 6, 1, 1, true, false, "A06", 2, 2 },
                    { 7, 2, 1, true, false, "A07", 2, 2 },
                    { 8, 3, 1, true, false, "A08", 2, 2 },
                    { 9, 4, 1, true, false, "A09", 2, 2 },
                    { 10, 5, 1, true, false, "A10", 2, 2 },
                    { 11, 1, 1, true, false, "A11", 3, 2 },
                    { 12, 2, 1, true, false, "A12", 3, 2 },
                    { 13, 3, 1, true, false, "A13", 3, 2 },
                    { 14, 4, 1, true, false, "A14", 3, 2 },
                    { 15, 5, 1, true, false, "A15", 3, 2 },
                    { 16, 1, 1, true, false, "A16", 4, 2 },
                    { 17, 2, 1, true, false, "A17", 4, 2 },
                    { 18, 3, 1, true, false, "A18", 4, 2 },
                    { 19, 4, 1, true, false, "A19", 4, 2 },
                    { 20, 5, 1, true, false, "A20", 4, 2 },
                    { 21, 1, 2, true, false, "B01", 1, 2 },
                    { 22, 2, 2, true, false, "B02", 1, 2 },
                    { 23, 3, 2, true, false, "B03", 1, 2 },
                    { 24, 4, 2, true, false, "B04", 1, 2 },
                    { 25, 5, 2, true, false, "B05", 1, 2 },
                    { 26, 1, 2, true, false, "B06", 2, 2 },
                    { 27, 2, 2, true, false, "B07", 2, 2 },
                    { 28, 3, 2, true, false, "B08", 2, 2 },
                    { 29, 4, 2, true, false, "B09", 2, 2 },
                    { 30, 5, 2, true, false, "B10", 2, 2 },
                    { 31, 1, 2, true, false, "B11", 3, 2 },
                    { 32, 2, 2, true, false, "B12", 3, 2 },
                    { 33, 3, 2, true, false, "B13", 3, 2 },
                    { 34, 4, 2, true, false, "B14", 3, 2 },
                    { 35, 5, 2, true, false, "B15", 3, 2 },
                    { 36, 1, 2, true, false, "B16", 4, 2 },
                    { 37, 2, 2, true, false, "B17", 4, 2 },
                    { 38, 3, 2, true, false, "B18", 4, 2 },
                    { 39, 4, 2, true, false, "B19", 4, 2 },
                    { 40, 5, 2, true, false, "B20", 4, 2 },
                    { 41, 1, 3, true, false, "M01", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "ParkingSlots",
                columns: new[] { "ParkingSlotId", "ColumnIndex", "FloorID", "IsAvailable", "IsBackup", "Name", "RowIndex", "TrafficID" },
                values: new object[,]
                {
                    { 42, 2, 3, true, false, "M02", 1, 1 },
                    { 43, 3, 3, true, false, "M03", 1, 1 },
                    { 44, 4, 3, true, false, "M04", 1, 1 },
                    { 45, 5, 3, true, false, "M05", 1, 1 },
                    { 46, 6, 3, true, false, "M06", 1, 1 },
                    { 47, 7, 3, true, false, "M07", 1, 1 },
                    { 48, 8, 3, true, false, "M08", 1, 1 },
                    { 49, 9, 3, true, false, "M09", 1, 1 },
                    { 50, 10, 3, true, false, "M10", 1, 1 },
                    { 51, 1, 3, true, false, "M11", 2, 1 },
                    { 52, 2, 3, true, false, "M12", 2, 1 },
                    { 53, 3, 3, true, false, "M13", 2, 1 },
                    { 54, 4, 3, true, false, "M14", 2, 1 },
                    { 55, 5, 3, true, false, "M15", 2, 1 },
                    { 56, 6, 3, true, false, "M16", 2, 1 },
                    { 57, 7, 3, true, false, "M17", 2, 1 },
                    { 58, 8, 3, true, false, "M18", 2, 1 },
                    { 59, 9, 3, true, false, "M19", 2, 1 },
                    { 60, 10, 3, true, false, "M20", 2, 1 },
                    { 61, 1, 3, true, false, "M21", 3, 1 },
                    { 62, 2, 3, true, false, "M22", 3, 1 },
                    { 63, 3, 3, true, false, "M23", 3, 1 },
                    { 64, 4, 3, true, false, "M24", 3, 1 },
                    { 65, 5, 3, true, false, "M25", 3, 1 },
                    { 66, 6, 3, true, false, "M26", 3, 1 },
                    { 67, 7, 3, true, false, "M27", 3, 1 },
                    { 68, 8, 3, true, false, "M28", 3, 1 },
                    { 69, 9, 3, true, false, "M29", 3, 1 },
                    { 70, 10, 3, true, false, "M30", 3, 1 },
                    { 71, 1, 4, true, false, "C01", 1, 2 },
                    { 72, 2, 4, true, false, "C02", 1, 2 },
                    { 73, 3, 4, true, false, "C03", 1, 2 },
                    { 74, 4, 4, true, false, "C04", 1, 2 },
                    { 75, 5, 4, true, false, "C05", 1, 2 },
                    { 76, 1, 4, true, false, "C06", 2, 2 },
                    { 77, 2, 4, true, false, "C07", 2, 2 },
                    { 78, 3, 4, true, false, "C08", 2, 2 },
                    { 79, 4, 4, true, false, "C09", 2, 2 },
                    { 80, 5, 4, true, false, "C10", 2, 2 },
                    { 81, 1, 4, true, false, "C11", 3, 2 },
                    { 82, 2, 4, true, false, "C12", 3, 2 },
                    { 83, 3, 4, true, false, "C13", 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "ParkingSlots",
                columns: new[] { "ParkingSlotId", "ColumnIndex", "FloorID", "IsAvailable", "IsBackup", "Name", "RowIndex", "TrafficID" },
                values: new object[,]
                {
                    { 84, 4, 4, true, false, "C14", 3, 2 },
                    { 85, 5, 4, true, false, "C15", 3, 2 },
                    { 86, 1, 5, true, false, "D01", 1, 1 },
                    { 87, 2, 5, true, false, "D02", 1, 1 },
                    { 88, 3, 5, true, false, "D03", 1, 1 },
                    { 89, 4, 5, true, false, "D04", 1, 1 },
                    { 90, 5, 5, true, false, "D05", 1, 1 },
                    { 91, 1, 5, true, false, "D06", 2, 1 },
                    { 92, 2, 5, true, false, "D07", 2, 1 },
                    { 93, 3, 5, true, false, "D08", 2, 1 },
                    { 94, 4, 5, true, false, "D09", 2, 1 },
                    { 95, 5, 5, true, false, "D10", 2, 1 },
                    { 96, 1, 5, true, false, "D11", 3, 1 },
                    { 97, 2, 5, true, false, "D12", 3, 1 },
                    { 98, 3, 5, true, false, "D13", 3, 1 },
                    { 99, 4, 5, true, false, "D14", 3, 1 },
                    { 100, 5, 5, true, false, "D15", 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "TimeSlot",
                columns: new[] { "TimeSlotId", "CreatedDate", "EndTime", "ParkingSlotId", "StartTime", "Status" },
                values: new object[] { 1, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 1, 1, 6, 0, 0, 0, DateTimeKind.Unspecified), "Active" });

            migrationBuilder.InsertData(
                table: "TimeSlot",
                columns: new[] { "TimeSlotId", "CreatedDate", "EndTime", "ParkingSlotId", "StartTime", "Status" },
                values: new object[] { 2, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 2, 6, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2024, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified), "Active" });

            migrationBuilder.InsertData(
                table: "BookingDetails",
                columns: new[] { "BookingDetailsId", "BookingId", "TimeSlotId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ApproveParkings_ParkingId",
                table: "ApproveParkings",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveParkings_StaffId",
                table: "ApproveParkings",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_BusinessId",
                table: "Bills",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_WalletId",
                table: "Bills",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserID",
                table: "Booking",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_VehicleInforID",
                table: "Booking",
                column: "VehicleInforID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_TimeSlotId",
                table: "BookingDetails",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Business_FeeId",
                table: "Business",
                column: "FeeId");

            migrationBuilder.CreateIndex(
                name: "UQ__Business__1788CCAD877AB68C",
                table: "Business",
                column: "UserID",
                unique: true,
                filter: "([UserID] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAddress_UserID",
                table: "FavoriteAddress",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_FieldWorkParkingImgs_ApproveParkingId",
                table: "FieldWorkParkingImgs",
                column: "ApproveParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_ParkingID",
                table: "Floors",
                column: "ParkingID");

            migrationBuilder.CreateIndex(
                name: "IX_Parking_BusinessId",
                table: "Parking",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingHasPrice_ParkingId",
                table: "ParkingHasPrice",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingHasPrice_ParkingPriceId",
                table: "ParkingHasPrice",
                column: "ParkingPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPrice_BusinessId",
                table: "ParkingPrice",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingPrice_TrafficId",
                table: "ParkingPrice",
                column: "TrafficId");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlots_FloorID",
                table: "ParkingSlots",
                column: "FloorID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlots_TrafficID",
                table: "ParkingSlots",
                column: "TrafficID");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSpotImage_ParkingID",
                table: "ParkingSpotImage",
                column: "ParkingID");

            migrationBuilder.CreateIndex(
                name: "IX_PayPal_ManagerID",
                table: "PayPal",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeLine_ParkingPriceId",
                table: "TimeLine",
                column: "ParkingPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlot_ParkingSlotId",
                table: "TimeSlot",
                column: "ParkingSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BookingId",
                table: "Transaction",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_WalletId",
                table: "Transaction",
                column: "WalletId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ManagerID",
                table: "Users",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ParkingId",
                table: "Users",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInfor_TrafficID",
                table: "Vehicle",
                column: "TrafficID");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleInfor_UserID",
                table: "Vehicle",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_userId_VnPay",
                table: "VnPay",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Parking_ApproveParkings",
                table: "ApproveParkings",
                column: "ParkingId",
                principalTable: "Parking",
                principalColumn: "ParkingId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_ApproveParkings",
                table: "ApproveParkings",
                column: "StaffId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_businessPro_Bills",
                table: "Bills",
                column: "BusinessId",
                principalTable: "Business",
                principalColumn: "BusinessProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallet_Bills",
                table: "Bills",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK__Booking__UserID__5070F446",
                table: "Booking",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Booking__Vehicle",
                table: "Booking",
                column: "VehicleInforID",
                principalTable: "Vehicle",
                principalColumn: "VehicleInforId");

            migrationBuilder.AddForeignKey(
                name: "FK__TimeSlot__BookingDetails",
                table: "BookingDetails",
                column: "TimeSlotId",
                principalTable: "TimeSlot",
                principalColumn: "TimeSlotId");

            migrationBuilder.AddForeignKey(
                name: "fk_IsManager",
                table: "Business",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Parking__Users",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "ConflictRequests");

            migrationBuilder.DropTable(
                name: "FavoriteAddress");

            migrationBuilder.DropTable(
                name: "FieldWorkParkingImgs");

            migrationBuilder.DropTable(
                name: "ParkingHasPrice");

            migrationBuilder.DropTable(
                name: "ParkingSpotImage");

            migrationBuilder.DropTable(
                name: "PayPal");

            migrationBuilder.DropTable(
                name: "TimeLine");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "VnPay");

            migrationBuilder.DropTable(
                name: "TimeSlot");

            migrationBuilder.DropTable(
                name: "ApproveParkings");

            migrationBuilder.DropTable(
                name: "ParkingPrice");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropTable(
                name: "ParkingSlots");

            migrationBuilder.DropTable(
                name: "Vehicle");

            migrationBuilder.DropTable(
                name: "Floors");

            migrationBuilder.DropTable(
                name: "VehicleType");

            migrationBuilder.DropTable(
                name: "Parking");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "Fees");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
