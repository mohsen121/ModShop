using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using WebScraper.Domain.Entities;

namespace WebScraper.Persistence.Configurations
{
    public class SearchKeyConfiguration : IEntityTypeConfiguration<SearchKey>
    {
        public void Configure(EntityTypeBuilder<SearchKey> builder)
        {
            builder.HasIndex(x => x.Word).IsUnique();
        }
    }
}
