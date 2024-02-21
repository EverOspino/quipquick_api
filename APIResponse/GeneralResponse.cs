namespace quipquick_api.APIResponse
{
    public class GeneralResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string ErrorMessage { get; set; }
        public GeneralResponse()
        {
            Success = true;
        }
    }
}
