using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TremendBoard.Infrastructure.Services.Interfaces
{
    public interface IProjectService<T> where T : class
    {
        void Create(T model);
        void Edit(string id);
        void Edit(T model);
    }
}
