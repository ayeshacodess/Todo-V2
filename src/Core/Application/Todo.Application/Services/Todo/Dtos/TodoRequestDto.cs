namespace Todo.Application.Services.Todo.Dtos
{
    public class TodoRequestDto
    {
        public int AssignedTo { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
    }
}
