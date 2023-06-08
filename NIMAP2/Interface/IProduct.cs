using NIMAP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace NIMAP2.Interface
{
    public interface IProduct
    {
        Task<bool> Create(Product pro);
        Task<bool> Edit(Product pro);
        Task<bool> Delete(int id);
        Task<Product> GetProductByID(int id);

    }
}