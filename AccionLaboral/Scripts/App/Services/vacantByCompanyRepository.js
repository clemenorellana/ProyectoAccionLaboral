var accionLabControllers = angular.module('vacantByCompanyRepository', []);


accionLabControllers.factory('vacantByCompanyRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var urlVacants = 'api/VacantsByCompany';
    return {
        getVacantList: function (callback) {
            return $http.get(urlVacants, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getInterviewTypeList: function (callback) {
            return $http.get('api/InterviewTypes', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getCompanyList: function (callback) {
            return $http.get('api/Companies', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getVacantLevelList: function (callback) {
            return $http.get('api/VacantLevels', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getAcademicLevelList: function (callback) {
            return $http.get('api/AcademicLevels', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getCareerList: function (callback) {
            return $http.get('api/Careers', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getCityList: function (callback) {
            return $http.get('api/Cities', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for insert
        insertVacant: function (callback, vacant) {
            debugger
            return $http.post(urlVacants, vacant, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //update method
        getVacant: function (id) {
            return $http.get(urlVacants + "/" + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        updateVacant: function (callback, vacant) {
            debugger
            return $http.put(urlVacants + '/' + vacant.VacantByCompanyId, vacant, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        deleteVacant: function (callback, id) {
            return $http.delete(urlVacants + '/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //datos de la vacante cubierta
        insertVacantCovered: function (callback, vacant) {
            return $http.post('api/VacantCovered', vacant, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getVacantsCovered: function (id) {
            return $http.get('api/VacantsCovered/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //Reports
        getVacantsDataReport: function (id) {
            return $http.post('api/VacantDataReport/', id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
        ,
        exportVacantReport: function (filters) {
            return $http.post('api/ExportVacantReport/', filters, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
    }
}]);