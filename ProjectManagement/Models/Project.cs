namespace ProjectManagement.Models
{
    public class Project
    {
        public DateTime CreatedDate { get; set; }
        public string? TaskTitle { get; set; }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<ProjectComment> Comments { get; set; } = new List<ProjectComment>();
    }
}
