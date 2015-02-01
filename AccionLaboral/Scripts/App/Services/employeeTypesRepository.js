var accionLabControllers = angular.module('employeeTypesRepository', []);

accionLabControllers.factory('employeeTypesRepo', ['$http', function ($http) {
    var url = 'api/Roles';
    return {
        getEmployeeTypesList: function (callback) {
            return $http.get(url);
        },
        //method for insert
        insertEmployeeType: function (callback, employeeType) {
            return $http.post(url, employeeType);
        },
        //method for update
        updateEmployeeType: function (callback, employeeType) {
            
            return $http.put(url + '/' + employeeType.RoleId, employeeType);
        },
        //method for delete
        deleteEmployeeType: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);