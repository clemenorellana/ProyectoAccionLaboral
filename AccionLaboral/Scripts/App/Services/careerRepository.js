var accionLabControllers = angular.module('careersRepository', []);


accionLabControllers.factory('careersRepo', ['$http', function ($http) {
    var url = 'api/Careers';
    return {
        getCarrerList: function (callback) {
            return $http.get(url);
        },
        getAcademicLevels: function (callback) {
            return $http.get('api/AcademicLevels');
        },
        //method for insert
        insertCareer: function (callback, career) {
            var career = {
                "Name": career.Name,
                "AcademicLevelId": career.AcademicLevelId
            };
            return $http.post(url, career);
        },
        //method for update
        updateCareer: function (callback, career) {
            var career = {
                "CareerId": career.CareerId,
                "Name": career.Name,
                "AcademicLevelId": career.AcademicLevelId
            };
            return $http.put(url + '/' + career.CareerId, career);
        },
        //method for delete
        deleteCareer: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);