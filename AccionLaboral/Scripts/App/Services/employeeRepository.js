var accionLabControllers = angular.module('employeesRepository', []);


accionLabControllers.factory('employeesRepo', ['$http', function ($http) {
    var url = 'api/Employees';
    return {
        getEmployeesList: function (callback) {
            return $http.get(url);
        },
        getEmployeesRoles: function (callback) {
            return $http.get('api/Roles');
        },
        getEmployeesUsers: function (callback) {
            return $http.get('api/Users');
        },
        getEmployeesCareers: function (callback) {
            return $http.get('api/Careers');
        },
        getEmployee: function (id) {
            return $http.get(url + "/" + id);
        },
        //method for insert
        insertEmployee: function (callback, employee) {
            //var employee = {
            //    "FirstName": employee.FirstName,
            //    "LastName": employee.LastName,
            //    "Email": employee.Email,
            //    "Birthday": employee.Birthday,
            //    "Age": employee.Age,
            //    "Cellphone": employee.Cellphone,
            //    "HomePhone": employee.HomePhone,
            //    "Address": employee.Address,
            //    "Gender": employee.Gender,
            //    "EmployeeAlias": employee.EmployeeAlias,
            //    "AdmissionDate": new Date(),
            //    "CareerId": employee.CareerId,
            //    "RoleId": employee.RoleId,
            //    "UserId": employee.UserId,

            //};
            return $http.post(url, employee);
        },
        //method for update
        updateEmployee: function (callback, employee) {
            //var employee = {
            //    "EmployeeId": employee.EmployeeId,
            //    "FirstName": employee.FirstName,
            //    "LastName": employee.LastName,
            //    "Email": employee.Email,
            //    "Birthday": employee.Birthday,
            //    "Age": employee.Age,
            //    "Cellphone": employee.Cellphone,
            //    "HomePhone": employee.HomePhone,
            //    "Address": employee.Address,
            //    "Gender": employee.Gender,
            //    "EmployeeAlias": employee.EmployeeAlias,
            //    "AdmissionDate": employee.AdmissionDate,
            //    "CareerId": employee.CareerId,
            //    "RoleId": employee.RoleId,
            //    "UserId": employee.UserId
            //};
            return $http.put(url + '/' + employee.EmployeeId, employee);
        },
        //method for delete
        deleteEmployee: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);