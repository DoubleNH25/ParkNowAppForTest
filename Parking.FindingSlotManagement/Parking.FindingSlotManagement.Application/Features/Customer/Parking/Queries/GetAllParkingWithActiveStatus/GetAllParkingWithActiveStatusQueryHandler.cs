using AutoMapper;
using MediatR;
using Parking.FindingSlotManagement.Application.Contracts.Persistence;
using Parking.FindingSlotManagement.Application.Mapping;
using Parking.FindingSlotManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Parking.FindingSlotManagement.Application.Features.Customer.Parking.Queries.GetAllParkingWithActiveStatus
{
    public class GetAllParkingWithActiveStatusQueryHandler : IRequestHandler<GetAllParkingWithActiveStatusQuery, ServiceResponse<IEnumerable<GetAllParkingWithActiveStatusResponse>>>
    {
        private readonly IParkingRepository _parkingRepository;
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });

        public GetAllParkingWithActiveStatusQueryHandler(IParkingRepository parkingRepository)
        {
            _parkingRepository = parkingRepository;
        }

        public async Task<ServiceResponse<IEnumerable<GetAllParkingWithActiveStatusResponse>>> Handle(GetAllParkingWithActiveStatusQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.PageNo <= 0)
                {
                    request.PageNo = 1;
                }
                if (request.PageSize <= 0)
                {
                    request.PageSize = 1;
                }

                // Filter for active parking lots only
                Expression<Func<Domain.Entities.Parking, bool>> filter = x => x.IsActive == true;

                var include = new List<Expression<Func<Domain.Entities.Parking, object>>>
                {
                    x => x.ParkingSpotImages,
                };

                var lst = await _parkingRepository.GetAllItemWithPagination(filter, include, x => x.ParkingId, true, request.PageNo, request.PageSize);
                var _mapper = config.CreateMapper();
                var lstDto = _mapper.Map<IEnumerable<GetAllParkingWithActiveStatusResponse>>(lst);

                if (lstDto.Count() <= 0)
                {
                    return new ServiceResponse<IEnumerable<GetAllParkingWithActiveStatusResponse>>
                    {
                        Message = "Không tìm thấy bãi giữ xe đang hoạt động.",
                        Success = true,
                        StatusCode = 200
                    };
                }

                return new ServiceResponse<IEnumerable<GetAllParkingWithActiveStatusResponse>>
                {
                    Data = lstDto,
                    Success = true,
                    StatusCode = 200,
                    Message = "Thành công",
                    Count = lstDto.Count()
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}