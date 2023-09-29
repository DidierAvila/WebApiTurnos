using AutoMapper;
using Azure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiTurnos.Data.Models;
using WebApiTurnos.Data.Repositories.BranchRep;
using WebApiTurnos.Data.Repositories.IShiftRep;

namespace WebApiTurnos.Core.Services.BranchSer
{
    public class BranchService : IBranchService
    {
        private readonly IMapper _mapper;
        private readonly IBranchRepository _repository;

        public BranchService(IMapper mapper, IBranchRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Branch> Create(Branch createRequest, CancellationToken cancellationToken)
        {
            return await _repository.Create(createRequest, cancellationToken);
        }

        public async Task<Branch> Read(int id, CancellationToken cancellationtoken)
        {
            return await _repository.Read(id, cancellationtoken);
        }

        public async Task<Branch> Update(Branch updateRequest, CancellationToken cancellationToken)
        {
            Branch entity = await _repository.Update(updateRequest, cancellationToken);
            return entity;
        }

        public async Task Delete(int id, CancellationToken cancellationToken)
        {
            await _repository.Delete(id, cancellationToken);
        }

        public async Task<ICollection<Branch>> GetAll(CancellationToken cancellationToken)
        {
            return await _repository.GetAll(cancellationToken);
        }
    }
}
