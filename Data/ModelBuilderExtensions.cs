using System;
using System.Collections.Generic;
using System.Text;
using Core.Enums;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder SetStatusQueryFilter(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ButtonSettings>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<Category>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<Mail>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<Device>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<Driver>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<DriverQuestion>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<Event>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<EventType>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<Question>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<NotificationSettings>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<Shift>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<Vehicle>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<VehicleDevice>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<VehicleFault>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<VehicleType>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<VehicleTypeQuestion>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);

            //modelBuilder.Entity<ReportSetting>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<ReportSettingCategory>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<ReportSettingDriver>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<ReportSettingFile>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<ReportSettingMail>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<ReportSettingShift>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);
            //modelBuilder.Entity<ReportSettingVehicle>().HasQueryFilter(p => p.Status != (byte)RecordStatus.Deleted);



            return modelBuilder;
        }
    }
}
