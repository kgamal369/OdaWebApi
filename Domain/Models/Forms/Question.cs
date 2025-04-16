using System;
using System.Collections.Generic;

namespace OdaWepApi.Domain.Models.Forms;

public partial class Question
{
    public int Questionid { get; set; }

    public string Questiontext { get; set; } = null!;

    public DateTime? Createdat { get; set; }

    public virtual ICollection<Answer> Answers { get; set; } = new List<Answer>();

    public virtual ICollection<Customeranswer> Customeranswers { get; set; } = new List<Customeranswer>();
}
