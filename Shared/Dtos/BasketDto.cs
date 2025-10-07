using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public record BasketDto
    {
        public string Id { get; init; }
        public IEnumerable<BasketItemDto> Items { get; init; }
    }

}
