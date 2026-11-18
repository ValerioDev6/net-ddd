using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace CleanArquitectura.Domain.Shared
{
    public record PaginationParams
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 2;

        public int PageSize
        {
            get => _pageSize;
        }

        public string? OrderBy { get; set; }

        public bool OrderAsc { get; set; } = true;

        public string? Search { get; set; }

    }
}
