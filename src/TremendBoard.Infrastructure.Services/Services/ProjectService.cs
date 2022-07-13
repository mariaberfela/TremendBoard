//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TremendBoard.Infrastructure.Data.Models;
//using TremendBoard.Infrastructure.Services.Interfaces;
 

//namespace TremendBoard.Infrastructure.Services.Services
//{
//    internal class ProjectService<T> : IProjectService<T> where T : class
//    {
//        private readonly IUnitOfWork _unitOfWork;

//        public ProjectService(IUnitOfWork unitOfWork)
//        {
//            _unitOfWork = unitOfWork;
//        }

//        public async void Create(T model)
//        {
//            await _unitOfWork.Project.AddAsync(new Project
//            {
//                Name = model.Name,
//                Description = model.Description,
//                CreatedDate = DateTime.Now,
//                Deadline = model.Deadline,
//                ProjectStatus = model.ProjectStatus
//            });

//            await _unitOfWork.SaveAsync();

//        }

//        public void Edit(string id)
//        {
//            throw new NotImplementedException();
//        }

//        public void Edit(T model)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
