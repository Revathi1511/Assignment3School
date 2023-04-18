using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using MySql.Data.MySqlClient;
using Mysqlx.Datatypes;
using School.Models;

namespace School.Controllers
{
    public class TeacherController : ApiController
    {

        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the authors table of our School database.
        /// <summary>
        /// Returns a list of Teachers in the system
        /// </summary>
        /// <example>GET api/Teacher/ListTeachers</example>
        /// <returns>
        /// A list of teacher names (first names and last names)
        /// </returns>
        [HttpGet]
        public IEnumerable<Teacher> ListTeachers()
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers";

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teacher Names
            List<Teacher> TeacherNames = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {
                TeacherNames.Add(new Teacher()
                {
                    TeacherId = Int32.Parse(ResultSet["teacherid"].ToString()),
                    TeacherFName = ResultSet["teacherfname"].ToString(),
                    TeacherLName = ResultSet["teacherlname"].ToString(),
                    EmployeeNumber = ResultSet["employeenumber"].ToString(),
                    HireDate = DateTime.Parse(ResultSet["hiredate"].ToString()),
                    Salary = Decimal.Parse(ResultSet["salary"].ToString())
                });
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of teacher names
            return TeacherNames;
        }

        [HttpGet]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid = " + id;

            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                NewTeacher.TeacherId = Int32.Parse(ResultSet["teacherid"].ToString());
                NewTeacher.TeacherFName = ResultSet["teacherfname"].ToString();
                NewTeacher.TeacherLName = ResultSet["teacherlname"].ToString();
                NewTeacher.EmployeeNumber = ResultSet["employeenumber"].ToString();
                NewTeacher.HireDate = DateTime.Parse(ResultSet["hiredate"].ToString());
                NewTeacher.Salary = Decimal.Parse(ResultSet["salary"].ToString());
            }


            return NewTeacher;
        }

        /// <summary>
        /// Deletes an Author from the connected MySQL Database if the ID of that author exists. Does NOT maintain relational integrity. Non-Deterministic.
        /// </summary>
        /// <param name="id">The ID of the author.</param>
        /// <example>POST /api/AuthorData/DeleteAuthor/3</example>
        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from teachers where teacherid=@id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

        /// <summary>
        /// Adds an Author to the MySQL Database.
        /// </summary>
        /// <param name="NewTeacher">An object with fields that map to the columns of the Teacher's table. Non-Deterministic.</param>
        /// <example>
        /// POST api/TeacherData/AddTeacher 
        /// FORM DATA / POST DATA / REQUEST BODY 
        /// {
        ///	"AuthorFname":"Christine",
        ///	"AuthorLname":"Bittle",
        ///	"AuthorBio":"Likes Coding!",
        ///	"AuthorEmail":"christine@test.ca"
        /// }
        /// </example>
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void AddTeacher([FromBody] Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            Debug.WriteLine(NewTeacher.TeacherFName);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "insert into teachers (TeacherFName, TeacherLName, EmployeeNumber, hiredate, Salary) values (@TeacherFName,@TeacherLName,@EmployeeNumber, CURRENT_DATE(), @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFName", NewTeacher.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();



        }
        [HttpPost]
        [EnableCors(origins: "*", methods: "*", headers: "*")]
        public void UpdateTeacher(int id, [FromBody] Teacher TeacherInfo)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Debug.WriteLine(AuthorInfo.AuthorFname);

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set TeacherFName=@TeacherFName, TeacherLName=@TeacherLname, EmployeeNumber=@EmployeeNumber, Salary=@Salary  where Teacherid=@Teacherid";
            cmd.Parameters.AddWithValue("@TeacherFName", TeacherInfo.TeacherFName);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLName);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@Teacherid", id);

            cmd.Prepare();

            cmd.ExecuteNonQuery();

            Conn.Close();


        }

    }
}
