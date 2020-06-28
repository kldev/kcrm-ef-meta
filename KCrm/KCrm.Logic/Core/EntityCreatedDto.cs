using System;

namespace KCrm.Logic.Core {
    public class EntityCreatedDto {
        public Guid Id { get; private set; }

        public EntityCreatedDto() {

        }
        public EntityCreatedDto(Guid id) {
            Id = id;
        }

    }
}
