/// <reference path="../../Views/Clientes/_RegisterClient.html" />
'use strict';

angular.module('AccionLaboralApp', [
        'ngRoute',
        'ngCookies',
        'ui.bootstrap',
        'clientsController',
        'careersController',
        'contractTemplatesController',
        'employeesController',
        'usersController',
        'countriesController',
        'citiesController'
    ])
    .config([
        '$routeProvider',
        function($routeProvider) {
            $routeProvider.
                otherwise({
                    redirectTo: '/'
                });
        }
    ]).filter('startFrom', function() {
        return function(input, start) {
            if (input) {
                start = +start; //parse to int
                return input.slice(start);
            }
            return [];
        }
    }).controller('mainController', [
        '$scope', '$location', '$cookies', '$rootScope', 'usersRepo', function ($scope, $location, $cookies, $rootScope, usersRepo) {

            var userStored = $cookies.userName;
            if (userStored == "null" || (!userStored)) {
                $scope.skinClass = "bg-black";
                $scope.template = 'Users/Login';
                $scope.userValid = false;
            }
            else {
                $scope.skinClass = "skin-blue";
                $scope.userValid = true;
                $scope.template = 'Home/Home';
                $rootScope.user = { UserName: userStored };
                $location.path('/');
            }

            $scope.logout = function () {
                $cookies.userName = null;
                $scope.skinClass = "bg-black";
                $scope.template = "Users/Login";
                $scope.userValid = false;
            }

            //$scope.skinClass = "bg-black";
            //$scope.template = "Users/Login";
            $scope.validateUser = function(userName, password) {
                usersRepo.login(userName, password).success(function (data) {
                        $scope.userValid = data;
                        if($scope.userValid == true){
                            $scope.template = 'Home/Home';
                            $scope.skinClass = "skin-blue";
                            $cookies.userName = userName;
                            $rootScope.user = { UserName: $cookies.userName };
                        } else {
                            $scope.skinClass = "bg-black";
                            $scope.template = "Users/Login";
                        };
                    })
                    .error(function (message) {
                    var error = message;
                });
            }

        }
    ]);
/*
acLabApp.controller('registerController', function ($scope) {
          $scope.title = "Registro de Cliente";
  });*/