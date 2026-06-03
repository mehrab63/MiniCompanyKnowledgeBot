namespace MiniCompanyKnowledgeBot.Models.Dtos
{
    public class ResultDto<T>
    {
        public ResultDto(bool IsSuccess, List<string>? Message, T Data)
        {
            this.IsSuccess = IsSuccess;
            this.Message = Message;
            this.Data = Data;
        }
        public ResultDto(bool IsSuccess)
        {
            this.IsSuccess = IsSuccess;
        }

        public T Data { get; set; }
        public List<string> Message { get; set; }
        public bool IsSuccess { get; private set; }

    }
}
