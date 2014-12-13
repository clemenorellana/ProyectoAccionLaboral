var accionLabControllers = angular.module('countriesRepository', []);


accionLabControllers.factory('countriesRepo', ['$http', function ($http) {
    var url = 'api/Countries';
    return {
        getCountriesList: function (callback) {
            debugger
            return $http.get(url);
        },
        //method for insert
        insertCountry: function (callback, country) {
            debugger
            var country = {
                "Name": country.Name,
            };
            return $http.post(url, country);
        },
        //method for update
        updateCountry: function (callback, country) {
            var country = {
                "CountryId": country.CountryId,
                "Name": country.Name
            };
            return $http.put(url + '/' + country.CountryId, country);
        },
        //method for delete
        deleteCountry: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);