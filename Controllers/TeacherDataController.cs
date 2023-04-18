using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
//using System.Web.Http;
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

        //GET : /Author/DeleteConfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {
            TeacherController controller = new TeacherController();
            Teacher NewTeacher = controller.FindTeacher(id);


            return View(NewTeacher);
        }


        //POST : /Author/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            TeacherController controller = new TeacherController();
            controller.DeleteTeacher(id);
            return RedirectToAction("Index");
        }

        //GET : /Author/New
        public ActionResult New()
        {
            return View();
        }

        //GET : /Teacher/Ajax_New
        public ActionResult Ajax_New()
        {
            return View();

        }

        //POST : /Author/Create
        [HttpPost]
        public ActionResult Create(Teacher NewTeacher)
        {
            //Identify that this method is running
            //Identify the inputs provided from the form

            Debug.WriteLine("I have accessed the Create Method!");
            Debug.WriteLine(NewTeacher.TeacherFName);
            Debug.WriteLine(NewTeacher.TeacherLName);
            Debug.WriteLine(NewTeacher.Salary);

            //Teacher NewTeacher = new Teacher();
            //NewTeacher.TeacherFName = TeacherFName;
            //NewTeacher.TeacherLName = TeacherLName;
            //NewTeacher.Salary = Salary;
            //NewTeacher.employeenumber = employeenumber;

            TeacherController controller = new TeacherController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("Index");
        }
        public ActionResult Update(int id)
        {
            TeacherController controller = new TeacherController();
            Teacher SelectedAuthor = controller.FindTeacher(id);

            return View(SelectedAuthor);
        }

        public ActionResult Ajax_Update(int id)
        {
            TeacherController controller = new TeacherController();
            Teacher SelectedAuthor = controller.FindTeacher(id);

            return View(SelectedAuthor);
        }


        /// <summary>
        /// Receives a POST request containing information about an existing author in the system, with new values. Conveys this information to the API, and redirects to the "Author Show" page of our updated author.
        /// </summary>
        /// <param name="id">Id of the Author to update</param>
        /// <param name="AuthorFname">The updated first name of the author</param>
        /// <param name="AuthorLname">The updated last name of the author</param>
        /// <param name="AuthorBio">The updated bio of the author.</param>
        /// <param name="AuthorEmail">The updated email of the author.</param>
        /// <returns>A dynamic webpage which provides the current information of the author.</returns>
        /// <example>
        /// POST : /Author/Update/10
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"AuthorFname":"Christine",
        ///	"AuthorLname":"Bittle",
        ///	"AuthorBio":"Loves Coding!",
        ///	"AuthorEmail":"christine@test.ca"
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id, Teacher Teacherinfo) { 
            
        

            TeacherController controller = new TeacherController();
            controller.UpdateTeacher(id, Teacherinfo);

            return RedirectToAction("Show/" + id);
        }
    }
}