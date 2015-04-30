var accionLabControllers = angular.module('statesRepository', []);


accionLabControllers.factory('statesRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var url = 'api/States';
    return {
        getStatesList: function (callback) {
            return $http.get(url, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for insert
        insertState: function (callback, state) {
            return $http.post(url, state, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for update
        updateState: function (callback, state) {
            return $http.put(url + '/' + state.StateId, state, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        deleteState: function (callback, id) {
            return $http.delete(url + '/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
    }
}]);