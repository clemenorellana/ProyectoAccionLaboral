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
.controller('countriesCtrl', ['$scope', 'countriesRepo', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', function ($scope, countriesRepo, $rootScope, $location, $filter, filterFilter, alertService) {
     
    $scope.countriesList = [];
    $scope.actionCountry = "";
    $scope.load = true;
    
    countriesRepo.getCountriesList().success(function (data) {
        $scope.countriesList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    //Sorting
    $scope.sort = "Name";
    $scope.reverse = false;

    $scope.changeSort = function (value) {
        if ($scope.sort == value) {
            $scope.reverse = !$scope.reverse;
            return;
        }

        $scope.sort = value;
        $scope.reverse = false;
    }
    //End Sorting//
    $scope.$watch('search', function (term) {
        // Create $scope.filtered and then calculat $scope.noOfPages, no racing!
        $scope.filtered = filterFilter($scope.countriesList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });

    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];

    $scope.setCountryData = function () {
        countriesRepo.getCountriesList().success(function (data) {
            $scope.countriesList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size

            $scope.noOfPages = ($scope.countriesList) ? Math.ceil($scope.countriesList.length / $scope.entryLimit) : 1;

            $scope.itemsInPage = ($scope.countriesList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.countriesList.length) ?
                                        $scope.countriesList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;

            //$scope.load = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            //$scope.load = false;
        });

    };

    $scope.setActionCountry = function (action, country) {
        $scope.actionCountry = action;
        if (action == "add") {
            $scope.country_modalTitle = "Agregar País";
            $scope.country_buttonName = "Agregar";
        }
        else {
            $scope.country_modalTitle = "Editar País";
            $scope.country_buttonName = "Editar";
            $scope.editCountry(country);
        }
    }

    $scope.editCountry = function (countryToEdit) {
        $scope.Country_CountryId = countryToEdit.CountryId
        $scope.Country_Name = countryToEdit.Name;

    };

    $scope.country_refresh = function () {

        countriesRepo.getCountriesList().success(function (data) {
             
            $scope.countriesList = [];
            $scope.countriesList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        })
    };

    $scope.countryClearData = function () {
        $scope.actionCountry = "";
        $scope.Country_CountryId = "";
        $scope.Country_Name = "";
    }


    $scope.saveCountry = function () {
        $scope.load = true;
        var country;

        if ($scope.actionCountry == "add") {
            country = {
                Name: $scope.Country_Name
            };

            countriesRepo.insertCountry(function () {
            }, country);

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

        //$scope.country_refresh();

        $scope.setCountryData();
        $scope.load = true;
    };

    $scope.setCountryToDelete = function (country) {
        $scope.countryToDeleteId = country.CountryId;
    };

    $scope.cancelCountrytDelete = function () {
        $scope.countryToDeleteId = "";
    };

    $scope.deleteCountry = function () {
        $scope.load = true;
        countriesRepo.deleteCountry(function () {
        }, $scope.countryToDeleteId).success(function () {
            $scope.cancelCountrytDelete();
            $scope.setCountryData();
            $scope.load = false;
        }).error(function (error) {
            $scope.load = false;
        });

        
    }

    $scope.setCountryData();

}]);