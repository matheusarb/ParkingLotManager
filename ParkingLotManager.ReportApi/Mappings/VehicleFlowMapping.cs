using AutoMapper;
using ParkingLotManager.ReportApi.DTOs;
using ParkingLotManager.ReportApi.Models;

namespace ParkingLotManager.ReportApi.Mappings;

public class VehicleFlowMapping : Profile
{
    public VehicleFlowMapping()
    {
        CreateMap<VehicleModel, VehicleModelDto>();

        CreateMap(typeof(ParkingLotGenericResponseDto<>), typeof(ParkingLotGenericResponseDto<>));        
    }
}
