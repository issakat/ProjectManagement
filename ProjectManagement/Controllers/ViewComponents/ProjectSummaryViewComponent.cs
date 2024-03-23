using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Models;

namespace ProjectManagement.ViewComponents
{
    public class ProjectSummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Project project)
        {
            return View(project);
        }
    }
}
