using AutoMapper;

namespace KCrm.Logic.Services {
    public abstract class BaseHandler {
        protected IMapper _mapper;

        protected BaseHandler(IMapper mapper) {
            _mapper = mapper;
        }
    }
}
