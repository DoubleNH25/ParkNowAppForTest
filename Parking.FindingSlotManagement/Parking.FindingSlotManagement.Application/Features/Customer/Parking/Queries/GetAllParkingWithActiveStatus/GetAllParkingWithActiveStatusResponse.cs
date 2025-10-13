using Parking.FindingSlotManagement.Application.Features.Customer.Parking.Queries.GetParkingDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Parking.Queries.GetAllParkingWithActiveStatus
{
    public class GetAllParkingWithActiveStatusResponse
    {
        public int ParkingId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string? Description { get; set; }
        public float? Stars { get; set; }
        public int? MotoSpot { get; set; }
        public int? CarSpot { get; set; }
        public bool? IsFull { get; set; }
        public bool? IsPrepayment { get; set; }
        public bool? IsOvernight { get; set; }
        public string? Avatar { get; set; }
    }
}