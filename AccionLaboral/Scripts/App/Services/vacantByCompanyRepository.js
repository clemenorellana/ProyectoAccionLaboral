var accionLabControllers = angular.module('vacantByCompanyRepository', []);


accionLabControllers.factory('vacantByCompanyRepo', ['$http', function ($http) {
    var urlVacants = 'api/VacantsByCompany';
    return {
        getVacantList: function (callback) {
            return $http.get(urlVacants);
        },
        getCompanyList: function (callback) {
            return $http.get('api/Companies');
        },
        getVacantLevelList: function (callback) {
            return $http.get('api/VacantLevels');
        },
        getAcademicLevelList: function (callback) {
            return $http.get('api/AcademicLevels');
        },
        getCareerList: function (callback) {
            return $http.get('api/Careers');
        },
        getCityList: function (callback) {
            return $http.get('api/Cities');
        }
    }
}]);