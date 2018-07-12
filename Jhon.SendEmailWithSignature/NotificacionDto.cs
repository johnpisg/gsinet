using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jhon.SendEmailWithSignature
{
    public class NotificacionDto
    {
        public string NombreUsuario { get; set; }
        public int RequisicionId { get; set; }
        public string EstadoDescripcion { get; set; }
        public string NombreEmpleado { get; set; }
        public DateTime Fecha { get; set; }
    }
}
