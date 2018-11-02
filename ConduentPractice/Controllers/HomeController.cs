using Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConduentPractice.Helpers;
using ConduentPractice.Business.Services;
using ConduentPractice.Models.ViewModels;

namespace ConduentPractice.Controllers
{
    public class HomeController : Controller
    {
        private StudentService _studentService;

        public ActionResult Index(HttpPostedFileBase studentFile)
        {
            if (!ModelState.IsValid || studentFile == null || studentFile.ContentLength == 0) return View(new StudentViewModel());
            var stream = studentFile.InputStream;
            if (!(studentFile.FileName.EndsWith(".xls") || studentFile.FileName.EndsWith(".xlsx")))
            {
                ViewData["invalidFile"] = "Solo se aceptan archivos con terminación .xls y .xlsx";
                return View(new StudentViewModel());
            }
            IExcelDataReader reader = studentFile.FileName.EndsWith(".xls")
                    ? ExcelReaderFactory.CreateBinaryReader(stream)
                    : ExcelReaderFactory.CreateOpenXmlReader(stream);
            reader.IsFirstRowAsColumnNames = true;
            var studentList = reader.AsDataSet().Tables[0].ToTypedList<StudentRawDTO>().ToStudentDTO();
            _studentService = new StudentService();
            reader.Close();
            var studentsSummary = _studentService.GetStudentsSummary(studentList);
            return View(studentsSummary);
        }

    }
}
