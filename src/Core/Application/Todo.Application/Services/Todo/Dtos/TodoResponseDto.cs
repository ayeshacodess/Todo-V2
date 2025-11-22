namespace Todo.Application.Services.Todo.Dtos
{
    public class TodoResponseDto
    {
        public int Id { get; set; }
        public int AssignedTo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
