using KCrm.Core;
using KCrm.Core.Entity.Projects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KCrm.Data.Projects.Config {
    public class ProjectActivityConfig  : AppEntityTypeConfiguration<ProjectActivityEntity> {
        protected override void ConfigureEntity(EntityTypeBuilder<ProjectActivityEntity> builder) {

            // https://www.npgsql.org/efcore/mapping/json.html?tabs=fluent-api%2Cpoco
            builder.Property (x => x.ProjectId).IsRequired ( );
            
            builder.Property (x => x.EventData)
                .IsRequired ( ).HasColumnType ("jsonb");

            builder.Property (x => x.EventName).IsRequired ( );
            builder.HasIndex (x => x.EventName);
            builder.HasIndex (x => x.ProjectId);

            builder.Property (x => x.Id).ValueGeneratedOnAdd  ( );
            builder.HasKey (x => x.Id);
        }
    }
}
