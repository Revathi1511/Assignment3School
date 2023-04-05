function AddTeacher() {
	//goal: send a request which looks like this:
	//POST : http://localhost:54767/api/Teacher/AddTeacher
	//with POST data of authorname, bio, email, etc.

	var URL = "http://localhost:54767/api/Teacher/AddTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFName = document.getElementById('TeacherFName').value;
	var TeacherLName = document.getElementById('TeacherLName').value;
	var Salary = document.getElementById('Salary').value;
	var employeenumber = document.getElementById('employeenumber').value;



	var TeacherData = {
		"TeacherFName": TeacherFName,
		"TeacherLName": TeacherLName,
		"Salary": Salary,
		"employeenumber": employeenumber
	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.
			window.location.href = "http://localhost:54767/TeacherData/Index";

		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));
}