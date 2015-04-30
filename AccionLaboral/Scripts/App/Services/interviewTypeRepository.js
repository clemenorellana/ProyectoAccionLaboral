var accionLabControllers = angular.module('interviewTypesRepository', []);


accionLabControllers.factory('interviewTypesRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var url = 'api/InterviewTypes';
    return {
        getInterviewList: function (callback) {
            return $http.get(url, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for insert
        insertInterview: function (callback, interview) {
            return $http.post(url, interview, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for update
        updateInterview: function (callback, interview) {
            return $http.put(url + '/' + interview.InterviewTypeId, interview, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        deleteInterview: function (callback, id) {
            return $http.delete(url + '/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
    }
}]);