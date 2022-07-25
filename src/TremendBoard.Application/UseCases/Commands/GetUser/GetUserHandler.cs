using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TremendBoard.Application.models;
using TremendBoard.Infrastructure.Services.Interfaces;

namespace TremendBoard.Application.UseCases.Commands.GetFullName
{
    public class GetUserHandler : IRequestHandler<GetUserRequest, GetUserResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetUserHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var model = await _unitOfWork.User.GetByIdAsync(request.Id);

            if (model == null)
            {
                throw new Exception("User not found");
            }
            var user =  _mapper.Map<SomeUserModel>(model);
            var response = new GetUserResponse()
            {
                User = user
            };
            return response;
        }
    }
}
