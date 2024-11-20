using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Toilet_Clicker.Core.Domain;
using Toilet_Clicker.Core.Dto;
using Toilet_Clicker.Core.ServiceInterface;
using Toilet_Clicker.Data;

namespace Toilet_Clicker.ApplicationServices.Services
{
	public class FileServices : IFileServices
	{
		private readonly IHostEnvironment _webHost;
		private readonly ToiletClickerContext _context;

        public FileServices
            (
                IHostEnvironment webHost,
                ToiletClickerContext context
            )
        {
            _webHost = webHost;
            _context = context;
        }

        public void UploadFilesToDatabase(ToiletDto dto, Toilet domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var image in dto.Files) 
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            ID = Guid.NewGuid(),
                            ImageTitle = image.FileName,
                            ToiletID = domain.ID
                        };

                        image.CopyTo( target );
                        files.ImageData = target.ToArray();
                        _context.FilesToDatabase.Add( files );
                    }
                }
            }
        }

		public void UploadFilesToDatabaseLocation(LocationDto dto, Location domain)
		{
			if (dto.Files != null && dto.Files.Count > 0)
			{
				foreach (var image in dto.Files)
				{
					using (var target = new MemoryStream())
					{
						FileToDatabase files = new FileToDatabase()
						{
							ID = Guid.NewGuid(),
							ImageTitle = image.FileName,
							LocationID = domain.ID
						};

						image.CopyTo(target);
						files.ImageData = target.ToArray();
						_context.FilesToDatabase.Add( files );
					}
				}
			}
		}

		public async Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto)
        {
            var imageID = await _context.FilesToDatabase
                .FirstOrDefaultAsync(x => x.ID == dto.ID);
            var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\" + imageID.ImageData;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            _context.FilesToDatabase.Remove(imageID);
            await _context.SaveChangesAsync();

            return null;
        }
    }
}
