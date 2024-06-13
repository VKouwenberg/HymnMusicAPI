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
	private readonly ISongRepository _songRepository;

	public SongComponent(ISongRepository songRepository)
	{
		_songRepository = songRepository;
	}
	public async Task<byte[]> GetSongByName(string fileName)
	{
		return await _songRepository.GetSongByName(fileName);
	}

	public async Task<List<string>> ListAllSongNames()
	{
		return await _songRepository.ListAllSongNames();
	}

	public async Task<List<string>> SearchSongsByName(string searchString)
	{
		return await _songRepository.SearchSongsByName(searchString);
	}
}
