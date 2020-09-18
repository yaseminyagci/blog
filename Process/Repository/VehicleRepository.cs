using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Data;
using Data.Entities;

namespace Process.Repository
{
    //public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    //{
    //    private readonly ApplicationDbContext _context;
    //    private readonly IRepository<Question> _questionRepository;
    //    private readonly IRepository<VehicleFault> _vehicleFaultRepository;

    //    public VehicleRepository(ApplicationDbContext context, IRepository<Question> questionRepository, IRepository<VehicleFault> vehicleFaultRepository) : base(context)
    //    {
    //        _context = context;
    //        _questionRepository = questionRepository;
    //        _vehicleFaultRepository = vehicleFaultRepository;
    //    }

    //    public async Task<Vehicle> GetBySerialNoAsync(string serialNo)
    //    {
    //        var isExist = await IsExistAsync(c => c.VehicleDevices.Any()).ConfigureAwait(false);
    //        if (isExist)
    //            return await GetSingleFirstAsync(c => c.VehicleDevices.First().Device.SerialNo == serialNo, "VehicleDevices.Device").ConfigureAwait(false);

    //        return await Task.FromResult<Vehicle>(null).ConfigureAwait(false);
    //    }

    //    public List<Category> GetVehicleQuestionsAsync(Vehicle vehicle, Shift shift)
    //    {

    //        //var questions = _questionRepository.GetAll();
    //        var questions = _questionRepository.FindBy(c => c.VehicleTypeQuestions.Any(e => e.VehicleTypeId == vehicle.VehicleTypeId) && c.ShiftId == shift.Id);
    //        //c.VehicleTypeQuestions.Any(e => e.VehicleTypeId == vehicle.VehicleTypeId) &&
    //        //c.ShiftId == shift.Id
    //        var groupByCategory = questions.ToList().GroupBy(c => c.Category).Select(c => c.Key).ToList();
    //        return groupByCategory;
    //    }

    //    public async Task AddFaultAsync(int vehicleId)
    //    {
    //        var vehicle = await GetSingleFirstAsync(c => c.Id == vehicleId, "VehicleFaults").ConfigureAwait(false);
    //        await _vehicleFaultRepository.AddAsync(new VehicleFault()
    //        {
    //            FaultStatus = FaultStatusEnum.Fault,
    //            Vehicle = vehicle

    //        }).ConfigureAwait(false);
    //        vehicle.Status = (byte)RecordStatus.Maintenance;
    //    }

    //    public async Task AddFaultAsync(Vehicle vehicle)
    //    {
    //        await _vehicleFaultRepository.AddAsync(new VehicleFault()
    //        {
    //            FaultStatus = FaultStatusEnum.Fault,
    //            Vehicle = vehicle

    //        }).ConfigureAwait(false);
    //        vehicle.Status = (byte)RecordStatus.Maintenance;
    //    }

    //    public async Task DeleteFaultAsync(int vehicleId)
    //    {
    //        var vehicle = await GetSingleFirstAsync(c => c.Id == vehicleId).ConfigureAwait(false);
    //        var faults = _vehicleFaultRepository.FindBy(c =>
    //           c.VehicleId == vehicleId && c.FaultStatus == FaultStatusEnum.Fault);
    //        foreach (var fault in faults)
    //        {
    //            fault.FaultStatus = FaultStatusEnum.Normal;
    //        }
    //        vehicle.Status = (byte)RecordStatus.Active;
    //    }

    //    public async Task DeleteFaultAsync(Vehicle vehicle)
    //    {
    //        var faults = _vehicleFaultRepository.FindBy(c =>
    //            c.VehicleId == vehicle.Id && c.FaultStatus == FaultStatusEnum.Fault);
    //        foreach (var fault in faults)
    //        {
    //            fault.FaultStatus = FaultStatusEnum.Normal;
    //        }
    //        vehicle.Status = (byte)RecordStatus.Active;
    //    }

    //}
}
