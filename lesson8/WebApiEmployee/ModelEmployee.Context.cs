﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------
using AutoMapper;
using System.Data.Entity.Infrastructure.Interception;
namespace WebApiEmployee
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class mydatabaseEntities : DbContext
    {
        //static readonly DatabaseLogger databaseLogger = new DatabaseLogger(@"c:/cfn/log/mysql.log", true);
        static mydatabaseEntities()
        {
            Mapper.Initialize(
               cfg =>
               {
                   cfg.CreateMap<Deparments, Deparments>().ForMember(x => x.Employee, opt => opt.Ignore());
                   cfg.CreateMap<Employee, Employee>().ForMember(e => e.Deparments, opt => opt.Ignore());
               }
               );
            //databaseLogger.StartLogging();
            //DbInterception.Add(databaseLogger);
        }
        public mydatabaseEntities()
            : base("name=mydatabaseEntities")
        {
            
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Deparments> Deparments { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }

        protected override void Dispose(bool disposing)
        {
            //DbInterception.Remove(databaseLogger);
            //databaseLogger.StopLogging();
            base.Dispose(disposing);
        }
    }
}
