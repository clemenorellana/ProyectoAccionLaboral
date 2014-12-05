var accionLabControllers = angular.module('careersRepository', []);
var url = 'api/Careers';

accionLabControllers.factory('careersRepo', ['$http', function ($http) {
    return {
        getCarrerList: function (callback) {
            return $http.get(url);
        },
        getAcademicLevels: function (callback) {
            return $http.get('api/AcademicLevels');
        },
        //method for delete
        deleteCareer: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);