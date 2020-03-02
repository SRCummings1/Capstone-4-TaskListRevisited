using System;
using System.Collections.Generic;

namespace Capstone3_TaskListRevisited1.Models
{
    public partial class Tasks
    {
        public string TaskDescription { get; set; }
        public DateTime DueDate { get; set; }
        public bool Completed { get; set; }
        public string TaskOwnerId { get; set; }
        public int Id { get; set; }

        public virtual AspNetUsers TaskOwner { get; set; }
    }
}
