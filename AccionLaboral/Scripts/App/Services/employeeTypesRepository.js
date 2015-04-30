var accionLabControllers = angular.module('employeeTypesRepository', []);

accionLabControllers.factory('employeeTypesRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var url = 'api/Roles';
    return {
        getEmployeeTypesList: function (callback) {
            return $http.get(url, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for insert
        insertEmployeeType: function (callback, employeeType) {
            return $http.post(url, employeeType, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for update
        updateEmployeeType: function (callback, employeeType) {
            
            return $http.put(url + '/' + employeeType.Id, employeeType, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        deleteEmployeeType: function (callback, id) {
            return $http.delete(url + '/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
    }
}]);