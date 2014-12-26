'use strict';

angular.module("citiesController", ['ngRoute', 'citiesRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Cities', {
            templateUrl: '/Cities/Index',
            controller: 'citiesCtrl'
        });
}]
)
.controller('citiesCtrl', ['$scope', 'citiesRepo', function ($scope, citiesRepo) {
    debugger
    $scope.citiesList = [];
    $scope.actionCity = "";
    $scope.load = true;

    citiesRepo.getCityCountries().success(function (data) {
        debugger
        $scope.City_Countries = data;
    });

    citiesRepo.getCitiesList().success(function (data) {
        $scope.citiesList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    $scope.setActionCity = function (action, index) {
        $scope.actionCity = action;
        if (action == "add") {
            $scope.city_modalTitle = "Agregar Ciudad";
            $scope.city_buttonName = "Agregar";
        }
        else {
            $scope.city_modalTitle = "Editar Ciudad";
            $scope.city_buttonName = "Editar";
            $scope.editCity(index);
        }
    }

    $scope.editCity = function (index) {
        var cityToEdit = $scope.citiesList[index];
        $scope.City_CityId = cityToEdit.CityId
        $scope.City_Name = cityToEdit.Name;

        var i = 0;
        for (i = 0; i < $scope.City_Countries.length; i++) {
            var city = $scope.City_Countries[i];
            if (city.CountryId == cityToEdit.CountryId)
                break;
        }

        $scope.City_Country = $scope.City_Countries[i];
    };

    $scope.city_refresh = function () {

        citiesRepo.getCitiesList().success(function (data) {
            debugger
            $scope.citiesList = [];
            $scope.citiesList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        })
    };

    $scope.cityClearData = function () {
        $scope.actionCity = "";
        $scope.City_CityId = "";
        $scope.City_Name = "";
        $scope.City_Country = "";
    }


    $scope.saveCity = function () {
        var city;
        debugger

        if ($scope.actionCity == "add") {
            city = {
                Name: $scope.City_Name,
                CountryId: $scope.City_Country.CountryId,
                CountryName: $scope.City_Country.Name
            };

            citiesRepo.insertCity(function () {
            }, city);

            //$scope.citiesList.push(city);
        }
        else {
            city = {
                CityId: $scope.City_CityId,
                Name: $scope.City_Name,
                CountryId: $scope.City_Country.CountryId,
                CountryName: $scope.City_Country.Name
            };

            citiesRepo.updateCity(function () {
            }, city);

        }

        $scope.cityClearData();

        $scope.city_refresh();

    };

    $scope.setCityToDelete = function (index) {
        var id = $scope.citiesList[index].CityId;
        $scope.cityToDeleteId = id;
        $scope.cityToDeleteIndex = index;
    };

    $scope.cancelCitytDelete = function () {
        $scope.cityToDeleteId = "";
        $scope.cityToDeleteIndex = "";
    };

    $scope.removeCity = function (index) {
        $scope.citiesList.splice(index, 1);
    };

    $scope.deleteCity = function () {
        var id = $scope.cityToDeleteId;
        $scope.removeCity($scope.cityToDeleteIndex);
        citiesRepo.deleteCity(function () {
            alert('City deleted');
            //removeCity(index);
        }, id);
        $scope.cancelCitytDelete();
    }



}]);