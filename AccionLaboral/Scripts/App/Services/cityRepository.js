var accionLabControllers = angular.module('citiesRepository', []);


accionLabControllers.factory('citiesRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var url = 'api/Cities';
    return {
        getCitiesList: function (callback) {
            return $http.get(url, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getCityCountries: function (callback) {
            return $http.get('api/Countries', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for insert
        insertCity: function (callback, city) {
            var city = {
                "Name": city.Name,
                "CountryId": city.CountryId
            };
            return $http.post(url, city, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for update
        updateCity: function (callback, city) {
            var city = {
                "CityId": city.CityId,
                "Name": city.Name,
                "CountryId": city.CountryId
            };
            return $http.put(url + '/' + city.CityId, city, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        deleteCity: function (callback, id) {
            return $http.delete(url + '/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
    }
}]);