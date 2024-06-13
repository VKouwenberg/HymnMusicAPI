namespace ContractDataBusiness.Interfaces;

public interface ISongRepository
{
	Task<byte[]> GetSongByName(string filename);
	Task<List<String>> ListAllSongNames();
	Task<List<String>> SearchSongsByName(string searchString);
}