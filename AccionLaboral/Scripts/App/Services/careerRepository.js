var accionLabControllers = angular.module('careersRepository', []);
var url = 'api/Careers';

accionLabControllers.factory('careersRepo', ['$http', function ($http) {
    return {
        getCarrerList: function (callback) {
            return $http.get(url);
        }
    }
}]);