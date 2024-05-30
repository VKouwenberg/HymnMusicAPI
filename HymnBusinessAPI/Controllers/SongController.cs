using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContractBusinessAPI.Interfaces;
using HymnModels.Models;


namespace HymnBusinessAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SongController : Controller
{
	private readonly ISongComponent _songComponent;
	private readonly ILogger<SongController> _logger;

	public SongController(ISongComponent songComponent, ILogger<SongController> logger)
	{
		_songComponent = songComponent;
		_logger = logger;
	}

	[HttpGet("{filename}")]
	public async Task<IActionResult> GetSongByName(string filename)
	{
		_logger.LogInformation("Attempting to retrieve song: {Filename}", filename);
		byte[] songBytes = await _songComponent.GetSongByName(filename);

		if (songBytes == null || songBytes.Length == 0)
		{
			_logger.LogWarning("Song not found: {Filename}", filename);
			return NotFound();
		}

		return File(songBytes, "audio/mpeg"); // Adjust the content type according to your file type
	}

	[HttpGet("list")]
	public async Task<IActionResult> ListAllSongNames()
	{
		_logger.LogInformation("Attempting to retrieve list of all song names");
		List<string> songNames = await _songComponent.ListAllSongNames();
		return Ok(songNames);
	}

	[HttpGet("search")]
	public async Task<IActionResult> SearchSongsByName([FromQuery] string searchString)
	{
		_logger.LogInformation("Searching for songs with query: {SearchString}", searchString);
		List<string> songNames = await _songComponent.SearchSongsByName(searchString);
		return Ok(songNames);
	}
}
