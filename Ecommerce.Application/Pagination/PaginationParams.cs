using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Pagination
{
    public abstract class PaginationParams
    {
        public string Sort { get; set; }
        public int PageIndex { get; set; } = 1; // 1 por defecto en caso de que no se envíe


        private const int MaxPageSize = 50; // limite de registros a 50 por página
        private int _pageSize = 3; // minimo 3 para el limite de registros

        public int PageSize 
        { 
            get => _pageSize;
            // si el valor es mayor al maximo de página permitido mostramos el maximo permitido, de lo contrario asigna el value
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }

        public string? Search { get; set; }
    }
}
