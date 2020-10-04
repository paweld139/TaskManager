using PDCore.Extensions;
using PDCore.Interfaces;
using PDCoreNew.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.BLL.Enums;
using TaskManager.DAL.Contracts;
using TaskManager.DAL.Entities;

namespace TaskManager.DAL.Services
{
    public class FileService
    {
        private readonly ITaskManagerUow taskManagerUow;
        private readonly IDataAccessStrategy<File> dataAccessStrategy;

        public FileService(ITaskManagerUow taskManagerUow, IDataAccessStrategy<File> dataAccessStrategy)
        {
            this.taskManagerUow = taskManagerUow;
            this.dataAccessStrategy = dataAccessStrategy;
        }

        public Task SaveFiles(IEnumerable<FileModel> files, IEntity<int> parent, ObjType type)
        {
            var result = Task.CompletedTask;

            if (files.Any())
            {
                files.Where(f => dataAccessStrategy.CanAdd(parent, type, f).Result).ForEach(f => dataAccessStrategy.PrepareForAdd(f));

                result = taskManagerUow.Files.AddFileFromObjectsList(files);
            }

            return result;
        }
    }
}
