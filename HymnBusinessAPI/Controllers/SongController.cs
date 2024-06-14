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

		string contentType = "application/octet-stream";
		string fileExtension = Path.GetExtension(filename).ToLower();

		switch (fileExtension)
		{
			case ".mp3":
				contentType = "audio/mpeg";
				break;
			case ".flac":
				contentType = "audio/flac";
				break;
			case ".wav":
				contentType = "audio/wav";
				break;
		}

		return File(songBytes, contentType, filename);
	}

	[HttpGet("list")]
	public async Task<IActionResult> ListAllSongNames()
	{
		_logger.LogInformation("Attempting to retrieve list of all song names");
		List<string> songNames = await _songComponent.ListAllSongNames();
		return Ok(songNames);
	}

	[HttpGet("SearchSongsByName")]
	public async Task<IActionResult> SearchSongsByName([FromQuery] string? searchString)
	{
		_logger.LogInformation("Searching for songs with query: {SearchString}", searchString);
		List<string> songNames = await _songComponent.SearchSongsByName(searchString);
		return Ok(songNames);
	}
}