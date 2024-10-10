using Microsoft.AspNetCore.Mvc;

namespace CoreMvcTemplate.ViewComponents.Layout
{
	public class _LayoutSideBarComponent : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
