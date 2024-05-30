using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HymnModels.DTOs;

public class SongDTO
{
	public string SongName { get; set; }
	public byte[] SongData { get; set; }
}
