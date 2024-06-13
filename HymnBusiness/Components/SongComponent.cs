using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HymnModels.DTOs;
using HymnModels.Models;
using ContractBusinessAPI.Interfaces;
using ContractDataBusiness.Interfaces;

namespace HymnBusiness.Components;

public class SongComponent : ISongComponent
{
	private readonly ISongRepository _songRepo;

	public async Task<byte[]> GetSongByName(string filename)
	{
		if (string.IsNullOrEmpty(filename))
		{
			throw new ArgumentException("Filename cannot be null or empty", nameof(filename));
		}

		return await _songRepo.GetSongByName(filename);
	}

	public async Task<List<string>> ListAllSongNames()
	{
		return await _songRepo.ListAllSongNames();
	}

	public async Task<List<string>> SearchSongsByName(string searchString)
	{
		if (string.IsNullOrEmpty(searchString))
		{
			throw new ArgumentException("Search string cannot be null or empty", nameof(searchString));
		}

		return await _songRepo.SearchSongsByName(searchString);
	}
}
