var accionLabControllers = angular.module('vacantByCompanyRepository', []);


accionLabControllers.factory('vacantByCompanyRepo', ['$http', function ($http) {
    var urlVacants = 'api/VacantsByCompany';
    return {
        getVacantList: function (callback) {
            return $http.get(urlVacants);
        },
        getInterviewTypeList: function (callback) {
            return $http.get('api/InterviewTypes');
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
        },
        //method for insert
        insertVacant: function (callback, vacant) {
            debugger
            return $http.post(urlVacants, vacant);
        },
        //update method
        getVacant: function (id) {
            return $http.get(urlVacants + "/" + id);
        },
        updateVacant: function (callback, vacant) {
            debugger
            return $http.put(urlVacants + '/' + vacant.VacantByCompanyId, vacant);
        },
        //method for delete
        deleteVacant: function (callback, id) {
            return $http.delete(urlVacants + '/' + id);
        }
    }
}]);