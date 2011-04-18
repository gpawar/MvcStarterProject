using System.Collections.Generic;
using System.Linq;
using MvcStarterProject.DataAccess;

namespace MvcStarterProject.Business
{
    public class GetProductService : GetObjectService<Product>, IGetProductService
    {
        public GetProductService(IRepository<Product> repository) : base(repository)
        {
        }

        public IList<Product> GetAvailableProducts()
        {
            return _repository.AsQueryable().Where(p => p.IsActive).ToList();
        }
    }
}