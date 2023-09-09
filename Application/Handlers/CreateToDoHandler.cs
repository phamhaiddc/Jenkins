using Application.Commands;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateToDoHandler : IRequestHandler<CreateToDoCommand, int>
    {
        private readonly IRepository _repository;

        public CreateToDoHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
        {
            var todo = new ToDo { Title = request.Title };
            await _repository.AddAsync(todo);
            return todo.Id;
        }
    }
}
