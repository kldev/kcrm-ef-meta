using MediatR;

namespace KCrm.Logic.Core {
    public class ResponseBase<T> : IRequest<Unit> {
        public T Data { get; private set; }
        public ErrorDto Error { get; private set; } = null;

        public ResponseBase() { }
        public ResponseBase(T data) {
            Data = data;
        }
        public ResponseBase(ErrorDto error) {
            Error = error;
        }
    }
}
