using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models;

public partial class Answer
{
    public int Answerid { get; set; }

    public int? Questionid { get; set; }

    public string Answertext { get; set; } = null!;

    public char Answercode { get; set; }

    public byte[]? AnswerPhoto { get; set; }

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Customeranswer> Customeranswers { get; set; } = new List<Customeranswer>();

    public virtual Question? Question { get; set; }
}
