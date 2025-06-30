using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interface;

namespace Section12.ViewComponents
{
    public class DemoViewComponent : ViewComponent
    {
        private readonly IProgramLanguageService _languageService;
        public DemoViewComponent(IProgramLanguageService languageService)
        {
            this._languageService = languageService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            Guid id = _languageService.GetID();
            return View(id);
        }
    }
}
