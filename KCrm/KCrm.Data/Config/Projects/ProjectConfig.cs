﻿using System;
using KCrm.Core;
using KCrm.Core.Entity.Projects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace KCrm.Data.Config.Projects {
    public class ProjectConfig : AppEntityTypeConfiguration<Project> {
        protected override void ConfigureEntity(EntityTypeBuilder<Project> builder) {
            var projectTypeConverter = new EnumToStringConverter<ProjectType> ( );

            builder.HasKey (p => p.Id);

            builder.Property (x => x.Name).IsRequired ( ).HasMaxLength (1024).IsUnicode ( );
            builder.HasIndex (x => x.Name).IsUnique ( );

            builder.Property (x => x.Description).IsUnicode ( ).HasMaxLength (8096);
            builder.Property (x => x.ProjectType).HasConversion (projectTypeConverter).IsRequired ( ).IsUnicode ( );

            builder.Property (x => x.EndDateTimeUtc)
                .HasConversion (x => x,
                    x => x.HasValue ? DateTime.SpecifyKind (x.Value, DateTimeKind.Utc) : default (DateTime?));

            builder.Property (x => x.StartDateTimeUtc)
                .HasConversion (x => x,
                    x => x.HasValue ? DateTime.SpecifyKind (x.Value, DateTimeKind.Utc) : default (DateTime?));

            builder.Property (x => x.PlanedEndDateTimeUtc)
                .HasConversion (x => x,
                    x => x.HasValue ? DateTime.SpecifyKind (x.Value, DateTimeKind.Utc) : default (DateTime?));
        }
    }
}
