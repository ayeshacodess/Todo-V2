namespace Todo.Domain.Entities
{
    public class TodoItem
    {
        public int Id { get; set; }
        public int AssignedTo { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CompletedDate { get; set; }
        public int Status { get; set; }

    }
}
