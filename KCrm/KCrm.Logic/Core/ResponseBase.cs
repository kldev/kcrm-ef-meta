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

    public class MessageResult {
        public string Message { get; set; }
    }

    public class MessageResponse : ResponseBase<MessageResult> {
        public MessageResponse() : base (new MessageResult ( ) {Message = "OK"}) {
        } 
        public MessageResponse(string message) : base (new MessageResult ( ) {Message = message}) {

        }
    }
}
