using System;
using HymnModels.Models;

namespace ContractBusinessAPI.Interfaces;

public interface ISongComponent
{
	Task<byte[]> GetSongByName(string filename);
	Task<List<String>> ListAllSongNames();
	Task<List<String>> SearchSongsByName(string searchString);
}