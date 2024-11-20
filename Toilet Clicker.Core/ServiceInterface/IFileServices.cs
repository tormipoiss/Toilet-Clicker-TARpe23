using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.Dto;

namespace Toilet_Clicker.Core.ServiceInterface
{
	public interface IFileServices
	{
		void UploadFilesToDatabase(ToiletDto dto, Toilet domain);
		void UploadFilesToDatabaseLocation(LocationDto dto, Location domain);
		Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
	}
}
  