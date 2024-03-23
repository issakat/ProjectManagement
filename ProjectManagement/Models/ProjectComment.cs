namespace ProjectManagement.Models
{
    public class ProjectComment
    {
        public ProjectComment()
        {
            Project = new Project();
        }

        public int Id { get; set; }
        public string? CommentText { get; set; }
        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
