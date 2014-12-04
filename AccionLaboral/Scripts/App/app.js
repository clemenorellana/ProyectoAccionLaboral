/// <reference path="../../Views/Clientes/_RegisterClient.html" />
'use strict';

angular.module('AccionLaboralApp', [
  'ngRoute',
  'ngGrid',
  'clientsController'
])
.config(['$routeProvider',
  function ($routeProvider) {
      $routeProvider.
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