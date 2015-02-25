var accionLabControllers = angular.module('statesRepository', []);


accionLabControllers.factory('statesRepo', ['$http', function ($http) {
    var url = 'api/States';
    return {
        getStatesList: function (callback) {
            return $http.get(url);
        },
        //method for insert
        insertState: function (callback, state) {
            return $http.post(url, state);
        },
        //method for update
        updateState: function (callback, state) {
            return $http.put(url + '/' + state.StateId, state);
        },
        //method for delete
        deleteState: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);