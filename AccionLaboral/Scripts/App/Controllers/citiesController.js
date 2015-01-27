'use strict';

angular.module("citiesController", ['ngRoute', 'citiesRepository', 'alertRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Cities', {
            templateUrl: '/Cities/Index',
            controller: 'citiesCtrl'
        });
}]
)
.controller('citiesCtrl', ['$scope', 'citiesRepo', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', function ($scope, citiesRepo, $rootScope, $location, $filter, filterFilter, alertService) {
     
    $scope.citiesList = [];
    $scope.actionCity = "";
    $scope.load = true;

    if (!$rootScope.alerts)
        $rootScope.alerts = [];

    citiesRepo.getCityCountries().success(function (data) {
        $scope.City_Countries = data;
    });

    
    $scope.$watch('search', function (term) {
        // Create $scope.filtered and then calculat $scope.noOfPages, no racing!
        $scope.filtered = filterFilter($scope.vacantList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
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
        $scope.filtered = filterFilter($scope.citiesList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });

    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];


    $scope.setCityata = function () {
        citiesRepo.getCitiesList().success(function (data) {
            $scope.citiesList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size

            $scope.noOfPages = ($scope.citiesList) ? Math.ceil($scope.citiesList.length / $scope.entryLimit) : 1;

            $scope.itemsInPage = ($scope.citiesList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.citiesList.length) ?
                                        $scope.citiesList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };

    $scope.setActionCity = function (action, city) {
        $scope.actionCity = action;
        if (action == "add") {
            $scope.city_modalTitle = "Agregar Ciudad";
            $scope.city_buttonName = "Agregar";
        }
        else {
            $scope.city_modalTitle = "Editar Ciudad";
            $scope.city_buttonName = "Editar";
            $scope.editCity(city);
        }
    }

    $scope.editCity = function (cityToEdit) {
        $scope.City_CityId = cityToEdit.CityId
        $scope.City_Name = cityToEdit.Name;
        $scope.City_Country = cityToEdit.CountryId;
    };

    $scope.cityClearData = function () {
        $scope.actionCity = "";
        $scope.City_CityId = "";
        $scope.City_Name = "";
        $scope.City_Country = "";
    }


    $scope.saveCity = function () {
        var city;
        if ($scope.actionCity == "add") {
            city = {
                Name: $scope.City_Name,
                CountryId: $scope.City_Country,
            };

            citiesRepo.insertCity(function () {
            }, city).success(function () {
                alertService.add('success', 'Mensaje', 'La Ciudad se ha insertado correctamente.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.setCityata();
                $scope.load = false;
            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido insertar el registro.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });

        }
        else {
            city = {
                CityId: $scope.City_CityId,
                Name: $scope.City_Name,
                CountryId: $scope.City_Country,
            };

            citiesRepo.updateCity(function () {
            }, city).success(function () {
                alertService.add('success', 'Mensaje', 'La Ciudad se ha editado correctamente.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.setCityata();
                $scope.load = false;
            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido editar el registro.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });

        }

        $scope.cityClearData();

    };

    $scope.setCityToDelete = function (city) {
        $scope.cityToDeleteId = city.CityId;
    };

    $scope.cancelCitytDelete = function () {
        $scope.cityToDeleteId = -1;
    };

    $scope.deleteCity = function () {
        $scope.load = true;
        citiesRepo.deleteCity(function () {
            alert('City deleted');
        }, $scope.cityToDeleteId).success(function () {
            alertService.add('success', 'Mensaje', 'La Ciudad se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.cancelCitytDelete();
            $scope.setCityata();
            $scope.load = false;
        }).error(function () {
            alertService.add('danger', 'Error', 'No se ha podido eliminar el registro.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });

        
        $scope.load = false;
    }

    $scope.setCityata();
    
}]);