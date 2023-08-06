using BusinessLogicLayer.Configuration;
using SmartChourey.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartChourey.BLL.Services.Interfaces.User
{
    public interface IFileUploadService
    {
        Task<bool> InsertFile(FileUpload fileUpload, string createdByEmail);
        Task<IList<FileUpload>> GetUploadedFiles(long categoryId, EnumHelpers.ECategory category, string siteId, EnumHelpers.EViewMode viewMode);
        Task<FileUpload> GetUploadedFile(long fileUploadId, long categoryId, EnumHelpers.ECategory category, string siteId);
        Task<bool> DeleteFile(long fileUploadId, string userName);
        Task<bool> DeleteFiles(List<FileUpload> fileUploads);
    }
}
