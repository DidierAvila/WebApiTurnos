using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApiTurnos.Data.Models;
using WebApiTurnos.Data.Repositories.IShiftRep;
using WebApiTurnos.Data.Repositories.UserRep;

namespace WebApiTurnos.Core.Services.UserSer
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _repository;

        public UserService(IMapper mapper, IUserRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<User> Create(User createRequest, CancellationToken cancellationToken)
        {
            return await _repository.Create(createRequest, cancellationToken);
        }

        public async Task<User> Read(int id, CancellationToken cancellationToken)
        {
            return await _repository.Read(id, cancellationToken);
        }

        public async Task<User> Update(User updateRequest, CancellationToken cancellationToken)
        {
            User entity = await _repository.Read(updateRequest.Id, cancellationToken);
            entity = await _repository.Update(entity, cancellationToken);

            return entity;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
        }

        public async Task<ICollection<User>> GetAll(CancellationToken cancellationToken)
        {
            return await _repository.GetAll(cancellationToken);
        }

        public async Task<string> Login(User userLogin, CancellationToken cancellationToken)
        {
            User currentUser = await _repository.Login(userLogin, cancellationToken);
            return "asd";
        }
    }
}
