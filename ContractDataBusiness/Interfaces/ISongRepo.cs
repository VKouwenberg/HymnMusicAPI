using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HymnModels.Models;

namespace ContractDataBusiness.Interfaces;

public interface ISongRepo
{
	Task<byte[]> GetSongByName(string filename);
	Task<List<String>> ListAllSongNames();
	Task<List<String>> SearchSongsByName(string searchString);
}