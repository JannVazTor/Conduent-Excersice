using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConduentPractice
{
    public class StudentDTO
    {
        public string Names { get; set; }
        public string PaternalSurname { get; set; }
        public string MaternalSurname { get; set; }
        public float Grade { get; set; }
        public string Group { get; set; }
        public float SchoolGrade { get; set; }
    }

    public class StudentRawDTO {
        public string Nombres { get; set; }
        public string Apellido_Materno { get; set; }
        public string Apellido_Paterno { get; set; }
        public float Grado { get; set; }
        public string Grupo { get; set; }
        public float Calificacion { get; set; }
    }
}