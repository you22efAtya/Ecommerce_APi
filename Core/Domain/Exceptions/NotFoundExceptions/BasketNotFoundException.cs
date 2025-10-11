using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
    public sealed class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(string id) : base($"The basket with id : {id} is not found")
        {

        }
    }
}
