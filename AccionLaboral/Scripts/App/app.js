/// <reference path="../../Views/Clientes/_RegisterClient.html" />
'use strict';

angular.module('AccionLaboralApp', [
  'ngRoute',
  'ui.bootstrap',
  'ngGrid',
  'clientsController',
  'careersController',
  'contractTemplatesController'
])
.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        otherwise({
            redirectTo: '/'
        });
  }]).filter('startFrom', function () {
      return function (input, start) {
          if (input) {
              start = +start; //parse to int
              return input.slice(start);
          }
          return [];
      }
  });;
/*
acLabApp.controller('mainController', function ($scope) {
    $scope.title = "Pagina principal";
});

acLabApp.controller('registerController', function ($scope) {
          $scope.title = "Registro de Cliente";
  });*/