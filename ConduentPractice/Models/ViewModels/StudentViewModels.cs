using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConduentPractice.Models.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            this.StudentSchoolGradeChartDataSet = new StudentSchoolChartViewModel
            {
                labels = new string[] { },
                datasets = new object[] { }
            };
            this.StudentSchoolGradeChartDataSetJson = "{}";
            this.StudentGradeAverageChartDataSetJson = "{}";
        }

        public string StudentNameWithBestSchoolGrade { get; set; }
        public string StudentNameWithWorstSchoolGrade { get; set; }
        public float StudentSchoolGradesAverage { get; set; }
        public StudentSchoolChartViewModel StudentSchoolGradeChartDataSet { get; set; }
        public StudentSchoolChartViewModel StudentGradeAverageChartDataSet { get; set; }
        public string StudentSchoolGradeChartDataSetJson { get; set; }
        public string StudentGradeAverageChartDataSetJson { get; set; }
    }

    public class StudentSchoolChartViewModel
    {
        public string[] labels { get; set; }
        public object[] datasets { get; set; }
    }

}