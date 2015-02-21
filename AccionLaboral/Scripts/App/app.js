/// <reference path="../../Views/Clientes/_RegisterClient.html" />
'use strict';

angular.module('AccionLaboralApp', [
        'ngRoute',
        'ngCookies',
        'ui.bootstrap',
        'dialogs',
        'clientsController',
        'careersController',
        'contractTemplatesController',
        'employeesController',
        'usersController',
        'countriesController',
        'citiesController',
        'companiesController',
        'vacantsByCompaniesController',
        'ngSanitize',
        'ui.select'
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
        '$scope', '$location', '$cookies', '$rootScope', '$timeout', '$dialogs', 'usersRepo', function ($scope, $location, $cookies, $rootScope, $timeout, $dialogs, usersRepo) {

            $scope.alerts = [];
            $scope.addAlert = function (type, msg) {
                $scope.alerts[0] = {type: type, msg: msg};
            };

            $scope.closeAlert = function (index) {
                $scope.alerts.splice(index, 1);
            };
            
            var progress = 25;
            var msgs = [
              'Hey! I\'m waiting here...',
              'About half way done...',
              'Almost there?',
              'Woo Hoo! I made it!'
            ];
            var i = 0;
            var fakeProgress = function () {
                $timeout(function () {
                    if (progress < 100) {
                        progress += 25;
                        $rootScope.$broadcast('dialogs.wait.progress', { msg: msgs[i++], 'progress': progress });
                        fakeProgress();
                    } else {
                        $rootScope.$broadcast('dialogs.wait.complete');
                    }
                }, 1000);
            }; // end fakeProgress 

            $scope.launch = function (dialog) {
                $dialogs.wait(msgs[i++], progress);
                fakeProgress();
            };

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
            $scope.validateUser = function (userName, password, isValidForm) {
                $scope.launch('wait');
                if(isValidForm){
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
                            $scope.addAlert("danger", "Usuario no valido. Intente de nuevo.");
                        };
                    })
                    .error(function (message) {
                        $scope.addAlert("danger","Ha ocurrido un error en el servidor.");
                    });
                } else {
                    $scope.addAlert("danger", "Hay campos invalidos");
                }
            }

        }
    ]);
/*
acLabApp.controller('registerController', function ($scope) {
          $scope.title = "Registro de Cliente";
  });*/