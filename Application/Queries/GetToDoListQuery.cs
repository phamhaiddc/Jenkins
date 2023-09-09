using Application.Handlers;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetToDoListHandler : IRequestHandler<GetToDoListQuery, List<ToDo>>
    {
        private readonly IRepository _repository;

        public GetToDoListHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ToDo>> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
