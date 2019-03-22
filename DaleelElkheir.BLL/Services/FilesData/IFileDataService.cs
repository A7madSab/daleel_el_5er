using DaleelElkheir.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.FilesData
{
    public interface IFileDataService
    {
        void InsertFileData(FileData file);
        void DeleteFileData(int fileID);
        FileData GetFileData(int fileID);
        void UpdateFileData(FileData file);
    }
}
