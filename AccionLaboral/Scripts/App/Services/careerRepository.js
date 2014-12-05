var accionLabControllers = angular.module('careersRepository', []);
var url = 'api/Careers';

accionLabControllers.factory('careersRepo', ['$http', function ($http) {
    return {
        getCarrerList: function (callback) {
            return $http.get(url);
        },
        //method for delete
        deleteCareer: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);