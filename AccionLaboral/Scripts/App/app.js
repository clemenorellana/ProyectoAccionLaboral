/// <reference path="../../Views/Clientes/_RegisterClient.html" />
/// <reference path="../../Views/Clientes/_RegisterClient.html" />
/// <reference path="../../Views/Clientes/_RegisterClient.html" />

var acLabApp = angular.module('AccionLaboralApp', [
  'ngRoute',
  'accionLabControllers'
]);

acLabApp.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
        when('/RegisterClient', {
            templateUrl: '/Home/RegisterClient',
            controller: 'registerController'
        }).
        when('/', {
            controller: 'mainController'
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