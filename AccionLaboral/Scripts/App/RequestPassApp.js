
'use strict';

angular.module('RequestPasswordApp', [
        'ngRoute',
        'ngResource',
        'ngCookies',
        'ui.bootstrap',
        'dialogs',
        'usersRepository'
    ])
    .config([
        '$routeProvider',
        function ($routeProvider) {
            $routeProvider.
                 when('/HomePage', {
                     templateUrl: '/Home/HomePage',
                     controller: 'homePageController'
                 }).
                otherwise({
                    redirectTo: '/',
                    templateUrl: '/Home/HomePage',
                    controller: 'homePageController'
                });
        }
    ]).controller('RequestPasswordController', [
        '$scope', '$location', '$cookies', '$rootScope', '$timeout', '$dialogs', 'usersRepo', function ($scope, $location, $cookies, $rootScope, $timeout, $dialogs, usersRepo) {
            
            $scope.getParams = function () {
                var urlParams = $location.path();
                var protocol = $location.protocol();
                var host = $location.host();
                var port = $location.port();
                var params = urlParams.split('/');
                return { "username": params[1], "id": params[2], "url": protocol + "://" + host + ":" + port };
            }

            $scope.forgotPassword = function (newPassword, confirmPassword) {
                var allParams = $scope.getParams();
                usersRepo.forgotPassword(allParams.url, allParams.id, newPassword, confirmPassword).success(function () {
                    $scope.success = "Su contrase\u00f1a ha sido cambiada exit\u00f3samente.";
                    $scope.newMessage = "";
                    $scope.confirmMessage = "";
                    $scope.newPassword = "";
                    $scope.confirmPassword = "";
                }).error(function (error) {
                    $scope.alerts = [];
                    $scope.newMessage = (error.ModelState["model.NewPassword"]) ? error.ModelState["model.NewPassword"].toString() : "";
                    $scope.confirmMessage = (error.ModelState["model.ConfirmPassword"]) ? error.ModelState["model.ConfirmPassword"].toString() : "";
                    $scope.success = "";
                })
            }
        }
    ]);
  