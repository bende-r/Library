using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MediatR;

namespace Application.UseCases.AuthorsUseCases.AddAuthor
{
    public class DeleteAuthorCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }

        public DeleteAuthorCommand(Guid id)
        {
            Id = id;
        }
    }
}
