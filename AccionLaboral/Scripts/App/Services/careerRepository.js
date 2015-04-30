var accionLabControllers = angular.module('careersRepository', []);


accionLabControllers.factory('careersRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var url = 'api/Careers';
    return {
        getCarrerList: function (callback) {
            return $http.get(url, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getAcademicLevels: function (callback) {
            return $http.get('api/AcademicLevels', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for insert
        insertCareer: function (callback, career) {
            var career = {
                "Name": career.Name,
                "AcademicLevelId": career.AcademicLevelId
            };
            return $http.post(url, career, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for update
        updateCareer: function (callback, career) {
            var career = {
                "CareerId": career.CareerId,
                "Name": career.Name,
                "AcademicLevelId": career.AcademicLevelId
            };
            return $http.put(url + '/' + career.CareerId, career, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        deleteCareer: function (callback, id) {
            return $http.delete(url + '/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
    }
}]);