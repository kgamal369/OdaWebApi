using System;
namespace OdaWepApi.Domain.DTOs
{
    public class CustomerAnswersOutDTO
    {
        public int Customeranswerid { get; set; }
        public int? Questionid { get; set; }
        public string Questiontext { get; set; } = null!;
        public int? Answerid { get; set; }
        public string Answertext { get; set; } = null!;
        public char Answercode { get; set; }
    }
}