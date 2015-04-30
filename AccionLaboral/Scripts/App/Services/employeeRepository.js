var accionLabControllers = angular.module('employeesRepository', []);


accionLabControllers.factory('employeesRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var url = 'api/Employees';
    return {
        getEmployeesList: function (callback) {
            return $http.get(url, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getRecruitmentEmployees: function (callback) {
            return $http.get('api/RecruitmentEmployees', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getEmployeesRoles: function (callback) {
            return $http.get('api/Roles', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getEmployeesUsers: function (callback) {
            return $http.get('api/Users', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getEmployeesUsersFree: function (callback) {
            return $http.get('api/UsersFree', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getEmployeesCareers: function (callback) {
            return $http.get('api/Careers', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getEmployee: function (id) {
            return $http.get(url + "/" + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for insert
        insertEmployee: function (callback, employee) {
            return $http.post(url, employee, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for update
        updateEmployee: function (callback, employee) {
            return $http.put(url + '/' + employee.EmployeeId, employee, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        deleteEmployee: function (callback, id) {
            return $http.delete(url + '/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
    }
}]);