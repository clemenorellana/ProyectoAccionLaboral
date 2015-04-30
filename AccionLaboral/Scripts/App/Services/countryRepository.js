var accionLabControllers = angular.module('countriesRepository', []);


accionLabControllers.factory('countriesRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var url = 'api/Countries';
    return {
        getCountriesList: function (callback) {
            return $http.get(url, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for insert
        insertCountry: function (callback, country) {
            var country = {
                "Name": country.Name,
            };
            return $http.post(url, country, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for update
        updateCountry: function (callback, country) {
            var country = {
                "CountryId": country.CountryId,
                "Name": country.Name
            };
            return $http.put(url + '/' + country.CountryId, country, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        deleteCountry: function (callback, id) {
            return $http.delete(url + '/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
    }
}]);