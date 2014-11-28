/// <reference path="../../Views/Clientes/_RegisterClient.html" />

var acLabApp = angular.module('AccionLaboralApp', [
  'ngRoute',
  'ngGrid',
  'clientsController',
  'clientsRepository'
  
]);

acLabApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/RegisterClient', {
            templateUrl: '/Clients/Create',
            controller: 'CustomerController'
        }).
        when('/AllClients', {
            templateUrl: '/Clients/Index',
            controller: 'CustomerController'
        }).
        otherwise({
            redirectTo: '/'
        });
  }]);
/*
acLabApp.controller('mainController', function ($scope) {
    $scope.title = "Pagina principal";
});

acLabApp.controller('registerController', function ($scope) {
          $scope.title = "Registro de Cliente";
  });*/