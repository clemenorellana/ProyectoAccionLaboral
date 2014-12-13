'use strict';

angular.module("countriesController", ['ngRoute', 'countriesRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Countries', {
            templateUrl: '/Countries/Index',
            controller: 'countriesCtrl'
        });
}]
)
.controller('countriesCtrl', ['$scope', 'countriesRepo', function ($scope, countriesRepo) {
    debugger
    $scope.countriesList = [];
    $scope.actionCountry = "";

    
    countriesRepo.getCountriesList().success(function (data) {
        $scope.countriesList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.loading = false;
    })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.loading = false;
        });

    $scope.setActionCountry = function (action, index) {
        $scope.actionCountry = action;
        if (action == "add") {
            $scope.country_modalTitle = "Agregar País";
            $scope.country_buttonName = "Agregar";
        }
        else {
            $scope.country_modalTitle = "Editar País";
            $scope.country_buttonName = "Editar";
            $scope.editCountry(index);
        }
    }

    $scope.editCountry = function (index) {
        var countryToEdit = $scope.countriesList[index];
        $scope.Country_CountryId = countryToEdit.CountryId
        $scope.Country_Name = countryToEdit.Name;

    };

    $scope.country_refresh = function () {

        countriesRepo.getCountriesList().success(function (data) {
            debugger
            $scope.countriesList = [];
            $scope.countriesList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.loading = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.loading = false;
        })
    };

    $scope.countryClearData = function () {
        $scope.actionCountry = "";
        $scope.Country_CountryId = "";
        $scope.Country_Name = "";
    }


    $scope.saveCountry = function () {
        debugger
        var country;

        if ($scope.actionCountry == "add") {
            country = {
                Name: $scope.Country_Name
            };

            countriesRepo.insertCountry(function () {
            }, country);

            $scope.countriesList.push(country);
        }
        else {
            country = {
                CountryId: $scope.Country_CountryId,
                Name: $scope.Country_Name
            };

            countriesRepo.updateCountry(function () {
            }, country);

        }

        $scope.countryClearData();

        $scope.country_refresh();

    };

    $scope.setCountryToDelete = function (index) {
        var id = $scope.countriesList[index].CountryId;
        $scope.countryToDeleteId = id;
        $scope.countryToDeleteIndex = index;
    };

    $scope.cancelCountrytDelete = function () {
        $scope.countryToDeleteId = "";
        $scope.countryToDeleteIndex = "";
    };

    $scope.removeCountry = function (index) {
        $scope.countriesList.splice(index, 1);
    };

    $scope.deleteCountry = function () {
        var id = $scope.countryToDeleteId;
        $scope.removeCountry($scope.countryToDeleteIndex);
        countriesRepo.deleteCountry(function () {
            alert('Country deleted');
            //removeCountry(index);
        }, id);
        $scope.cancelCountrytDelete();
    }



}]);