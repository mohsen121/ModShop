using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebScraper.Domain.Entities;

namespace WebScraper.Persistence.Configurations
{
    public class TempPlaceConfiguration : IEntityTypeConfiguration<TempPlace>
    {
        public void Configure(EntityTypeBuilder<TempPlace> builder)
        {
            builder.HasIndex(x => new { x.Name, x.Lat, x.Long }).IsUnique();
        }
    }
}
