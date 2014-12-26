var accionLabControllers = angular.module('citiesRepository', []);


accionLabControllers.factory('citiesRepo', ['$http', function ($http) {
    var url = 'api/Cities';
    return {
        getCitiesList: function (callback) {
            return $http.get(url);
        },
        getCityCountries: function (callback) {
            return $http.get('api/Countries');
        },
        //method for insert
        insertCity: function (callback, city) {
            var city = {
                "Name": city.Name,
                "CountryId": city.CountryId
            };
            return $http.post(url, city);
        },
        //method for update
        updateCity: function (callback, city) {
            var city = {
                "CityId": city.CityId,
                "Name": city.Name,
                "CountryId": city.CountryId
            };
            return $http.put(url + '/' + city.CityId, city);
        },
        //method for delete
        deleteCity: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);