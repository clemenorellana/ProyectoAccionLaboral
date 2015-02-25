var accionLabControllers = angular.module('interviewTypesRepository', []);


accionLabControllers.factory('interviewTypesRepo', ['$http', function ($http) {
    var url = 'api/InterviewTypes';
    return {
        getInterviewList: function (callback) {
            return $http.get(url);
        },
        //method for insert
        insertInterview: function (callback, interview) {
            return $http.post(url, interview);
        },
        //method for update
        updateInterview: function (callback, interview) {
            return $http.put(url + '/' + interview.InterviewTypeId, interview);
        },
        //method for delete
        deleteInterview: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);