using Microsoft.AspNetCore.Mvc;
using Section11.ViewModels;

namespace Section11.ViewComponents
{
    public class StronglyTypedGridViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(List<ProgramLang> programLangs)
        {
            return View(programLangs);
        }
    }
}
