using Microsoft.AspNetCore.Mvc;

namespace CoreMvcTemplate.ViewComponents.Layout
{
	public class _LayoutNavBarComponent :ViewComponent
	{

		public IViewComponentResult Invoke()
		{
				return View();
		}
	}
}
