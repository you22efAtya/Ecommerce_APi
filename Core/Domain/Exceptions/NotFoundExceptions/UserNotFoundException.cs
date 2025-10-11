using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFoundExceptions
{
    public class UserNotFoundException(string email) : NotFoundException($"The user with email {email} is not found")
    {
    }
}
