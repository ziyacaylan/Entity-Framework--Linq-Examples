﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntitySonKisim
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DbOgrencilerEntities : DbContext
    {
        public DbOgrencilerEntities()
            : base("name=DbOgrencilerEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TBL_DERSLER> TBL_DERSLER { get; set; }
        public virtual DbSet<TBL_KULUPLER> TBL_KULUPLER { get; set; }
        public virtual DbSet<TBL_NOTLAR> TBL_NOTLAR { get; set; }
        public virtual DbSet<TBL_OGRENCI> TBL_OGRENCI { get; set; }
        public virtual DbSet<TBL_URUNLER> TBL_URUNLER { get; set; }
    
        public virtual ObjectResult<Kulupler_Result> Kulupler()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Kulupler_Result>("Kulupler");
        }
    }
}
