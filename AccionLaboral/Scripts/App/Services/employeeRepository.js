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
        getEmployeesUsersFree: function (callback) {
            return $http.get('api/UsersFree');
        },
        getEmployeesCareers: function (callback) {
            return $http.get('api/Careers');
        },
        getEmployee: function (id) {
            return $http.get(url + "/" + id);
        },
        //method for insert
        insertEmployee: function (callback, employee) {
            return $http.post(url, employee);
        },
        //method for update
        updateEmployee: function (callback, employee) {
            return $http.put(url + '/' + employee.EmployeeId, employee);
        },
        //method for delete
        deleteEmployee: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);