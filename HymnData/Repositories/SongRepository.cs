using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using ContractDataBusiness.Interfaces;

namespace HymnData.Repositories;

public class SongRepository : ISongRepository
{
	private readonly string _musicDirectory = "C:/Fontys ICT/Semester 3/IPS Individueel Project/Music/";

	private readonly string[] _allowedExtensions = { ".mp3", ".flac", ".wav" };

	public async Task<byte[]> GetSongByName(string fileName)
	{
		if (string.IsNullOrEmpty(fileName))
		{
			return null;
		}

		var matchingFiles = Directory.GetFiles(_musicDirectory)
			.Where(f => _allowedExtensions.Contains(Path.GetExtension(f).ToLower()) &&
						Path.GetFileNameWithoutExtension(f).Equals(fileName, StringComparison.OrdinalIgnoreCase))
			.ToList();

		if (!matchingFiles.Any())
		{
			return null;
		}

		string filePath = matchingFiles.First();
		return await File.ReadAllBytesAsync(filePath);
	}

	public async Task<List<string>> ListAllSongNames()
	{
		return await Task.Run(() => Directory.GetFiles(_musicDirectory)
			.Where(f => _allowedExtensions.Contains(Path.GetExtension(f).ToLower()))
			.Select(f => Path.GetFileName(f))
			.ToList());
	}

	public async Task<List<string>> SearchSongsByName(string searchString)
	{
		if (string.IsNullOrEmpty(searchString))
		{
			return await ListAllSongNames();
		}

		return await Task.Run(() => Directory.GetFiles(_musicDirectory)
			.Where(f => _allowedExtensions.Contains(Path.GetExtension(f).ToLower()) &&
						Path.GetFileName(f).IndexOf(searchString, System.StringComparison.OrdinalIgnoreCase) >= 0)
			.Select(f => Path.GetFileName(f))
			.ToList());
	}


}