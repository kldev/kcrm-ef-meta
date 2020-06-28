using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KCrm.Core.Entity.Tags;
using KCrm.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KCrm.ConsoleSample {
    public class TagSample : SampleBase<TagContext> {
        private TagGroup CreateRandomGroup(int i) {
            var groupId = Guid.NewGuid ( );
            var group = new TagGroup ( ) { Name = $"Random Group {i}", Id = groupId };
            var count = _random.Next (0, 250);

            var j = 0;
            while (j++ < count) {
                ((List<Tag>)group.Tags).Add (new Tag { Name = $"RG {i}- Tag {j}, ", Id = Guid.NewGuid ( ), TagGroupId = groupId });
            }

            return group;
        }

        private List<Tag> TagsList() {
            return new List<Tag> {
                new Tag { Id = TagHelper.EnglishTagId, Name = "English required"},
                new Tag { Id = TagHelper.GermanTagId, Name = "German required"},
                new Tag { Id = TagHelper.ImportantTagId, Name = "Important"},
                new Tag { Id =  TagHelper.DesignOnly, Name = "Design only"}
            };
        }

        protected override async Task SeedData() {
            if (await Context.TagGroups.CountAsync ( ) == 0) {

                _logger.LogInformation ("Seed projects tags");

                await Context.TagGroups.AddAsync (new TagGroup ( ) {
                    Id = TagHelper.RootId,
                    Name = "Projects tags",
                    Tags = TagsList ( )
                });

                var i = 0;
                while (i++ < 10) {
                    await Context.TagGroups.AddAsync (CreateRandomGroup (i));
                }

                await Context.TagGroups.AddRangeAsync (new TagGroup ( ) { Id = Guid.NewGuid ( ), Name = "Empty Group" });
                await Context.SaveChangesAsync ( );

                _logger.LogInformation ("Seed completed");
            }
            else {
                _logger.LogInformation ("projects tags already seeded");
            }
        }
    }
}
