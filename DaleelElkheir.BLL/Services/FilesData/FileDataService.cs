using DaleelElkheir.DAL.Domain;
using DaleelElkheir.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaleelElkheir.BLL.Services.FilesData
{
    public class FileDataService:IFileDataService
    {
        private readonly UnitOfWork unitOfWork;
        public FileDataService (UnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }
        public void InsertFileData(FileData file)
        {
            unitOfWork.Repository<FileData>().Insert(file);
            unitOfWork.Save();
        }
        public void DeleteFileData(int fileID)
        {
            unitOfWork.Repository<FileData>().Delete(fileID);
            unitOfWork.Save();
        }
        public FileData GetFileData(int fileID)
        {
           return unitOfWork.Repository<FileData>().GetById(fileID);
        }
        public void UpdateFileData(FileData file)
        {
            unitOfWork.Repository<FileData>().Update(file);
            unitOfWork.Save();
        }
    }
}
