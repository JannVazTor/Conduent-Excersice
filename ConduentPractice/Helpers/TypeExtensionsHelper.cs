using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ConduentPractice.Helpers
{
    public static class TypeExtensionsHelper
    {
        public static string GetFullName(this StudentDTO s) {
            return string.Format("{0} {1} {2}",
                s.Names,
                s.PaternalSurname,
                s.MaternalSurname);
        }

        public static List<StudentDTO> ToStudentDTO(this List<StudentRawDTO> students)
        {
            return students.Select(s => new StudentDTO
            {
                Names = s.Nombres,
                PaternalSurname = s.Apellido_Paterno,
                MaternalSurname = s.Apellido_Materno,
                Grade = s.Grado,
                Group = s.Grupo,
                SchoolGrade = s.Calificacion
            }).ToList();
        }

        /// <summary>
        /// Converts a DataTable to a list with generic objects
        /// </summary>
        /// <typeparam name="T">Generic object</typeparam>
        /// <param name="table">DataTable</param>
        /// <returns>List with generic objects</returns>
        public static List<T> ToTypedList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();
                foreach (DataColumn col in table.Columns)
                {
                    if (col.ColumnName.Contains(' ')) col.ColumnName = col.ColumnName.Replace(' ', '_');
                }

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    list.Add(obj);
                }

                return list;
            }
            catch
            {
                return null;
            }
        }
    }
}