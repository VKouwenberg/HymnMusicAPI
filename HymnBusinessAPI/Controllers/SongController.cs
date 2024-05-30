using Microsoft.AspNetCore.Mvc;

namespace HymnBusinessAPI.Controllers;

public class SongController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}
