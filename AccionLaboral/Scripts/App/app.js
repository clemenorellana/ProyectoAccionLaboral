/// <reference path="../../Views/Clientes/_RegisterClient.html" />
'use strict';

angular.module('AccionLaboralApp', [
  'ngRoute',
  'ngGrid',
  'clientsController',
  'careersController',
  'contractTemplatesController',
  'employeesController',
  'usersController',
  'countriesController',
  'citiesController'
])
.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        otherwise({
            redirectTo: '/Login'
        });
  }]);
/*
acLabApp.controller('mainController', function ($scope) {
    $scope.title = "Pagina principal";
});

acLabApp.controller('registerController', function ($scope) {
          $scope.title = "Registro de Cliente";
  });*/