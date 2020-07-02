namespace PropertyManager.ResponseModels
{
    public class ResponseMessage
    {
        public int Status { get; private set; }
        public ApiResponse Payload { get; private set; }

        public ResponseMessage(int status, ApiResponse payload)
        {
            Status = status;
            Payload = payload;
        }
    }
}
