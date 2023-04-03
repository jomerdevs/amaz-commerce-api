using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pendiente")]
        Pending,
        [EnumMember(Value = "El pago fue completado")]
        Completed,
        [EnumMember(Value = "El producto ha sido enviado al cliente")]
        Sent,
        [EnumMember(Value = "Error con el pago")]
        Error
    }
}
