using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using School.Models;

namespace School.Controllers
{
    public class TeacherDataController : Controller
    {
        // GET: TeacherData
        public ActionResult Index()
        {
            TeacherController controller = new TeacherController();
            IEnumerable<Teacher> TeachersName = controller.ListTeachers();
            return View(TeachersName);
        }

        public ActionResult Show(int id) 
        {
            TeacherController controller = new TeacherController();
            Teacher Teacher = controller.FindTeacher(id);
            return View(Teacher);
        }
    }
}