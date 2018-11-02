using ConduentPractice.Models.ViewModels;
using ConduentPractice.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace ConduentPractice.Business.Services
{
    public class StudentService
    {
        public StudentViewModel GetStudentsSummary(List<StudentDTO> students)
        {
            students = students.OrderByDescending(s => s.SchoolGrade).ToList();
            var studentSummary = new StudentViewModel
            {
                StudentNameWithBestSchoolGrade = GetStudentNamesBySchoolGradeSchoolGrades(students, students.First().SchoolGrade),
                StudentNameWithWorstSchoolGrade = GetStudentNamesBySchoolGradeSchoolGrades(students, students.Last().SchoolGrade),
                StudentSchoolGradesAverage = students.Average(s => s.SchoolGrade),
                StudentSchoolGradeChartDataSet = GetStudentSchoolGradeChartDataSet(students),
                StudentGradeAverageChartDataSet = GetStudentGradeAverageChartDataSet(students)
            };

            var javascriptSerializer = new JavaScriptSerializer();
            studentSummary.StudentSchoolGradeChartDataSetJson = javascriptSerializer.Serialize(studentSummary.StudentSchoolGradeChartDataSet);
            studentSummary.StudentGradeAverageChartDataSetJson = javascriptSerializer.Serialize(studentSummary.StudentGradeAverageChartDataSet);
            return studentSummary;
        }

        public string GetStudentNamesBySchoolGradeSchoolGrades(List<StudentDTO> students, float schoolGrade) {
            return string.Join(", ", 
                students
                    .Where(st => st.SchoolGrade == schoolGrade)
                    .Select(s => s.GetFullName())
                    .Distinct());
        }

        public StudentSchoolChartViewModel GetStudentGradeAverageChartDataSet(List<StudentDTO> students)
        {
            return new StudentSchoolChartViewModel
            {
                labels = students.Select(s => string.Format("Grado {0}", s.Grade.ToString())).Distinct().ToArray(),
                datasets = new object[] {
                   new {
                       label = "Promedio",
                       data = students.GroupBy(
                                   g => g.Grade, 
                                   g => g.SchoolGrade,
                                   (key, g) => new 
                                   { 
                                       Grade = key, 
                                       SchoolGradeAverage = g.ToList().Average() 
                                   }).Select(g => g.SchoolGradeAverage).ToArray(),
                        backgroundColor = "rgba(255,99,132,0.2)",
                        borderColor = "rgba(255,99,132,1)",
                        borderWidth = 1
                   }
               }
            };
        }

        public StudentSchoolChartViewModel GetStudentSchoolGradeChartDataSet(List<StudentDTO> students)
        {
            return new StudentSchoolChartViewModel
                {
                    labels = students.Select(s => s.Names).ToArray(),
                    datasets = new object[] { 
                        new { 
                            label = "Calificación", 
                            data = students.Select(s => s.SchoolGrade).ToArray(),
                            backgroundColor = "rgba(0,0,255,0.2)",
                            borderColor = "rgba(0,0,255,1)",
                            borderWidth = 1
                        }
                    }
                };
        }
    }
}