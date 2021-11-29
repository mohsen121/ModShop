using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebScraper.Domain.Entities;

namespace WebScraper.Persistence.Configurations
{
    public class KeyWordTimeFrameConfiguration : IEntityTypeConfiguration<KeyWordTimeFrame>
    {

        public void Configure(EntityTypeBuilder<KeyWordTimeFrame> builder)
        {
            builder.HasIndex(x => new { x.KeyWordId, x.FromTime, x.ToTime }).IsUnique();
        }
    }
}
