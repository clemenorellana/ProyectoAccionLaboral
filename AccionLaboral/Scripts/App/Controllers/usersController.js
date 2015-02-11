'use strict';

angular.module("usersController", ['ngRoute', 'usersRepository', 'alertRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Users', {
            templateUrl: '/Users/Index',
            controller: 'usersCtrl'
        }).
        when('/Login', {
            templateUrl: '/Users/Login',
            controller: 'usersCtrl'
        }).
        when('/ForgotPassword', {
            templateUrl: '/Users/ForgotPassword',
            controller: 'usersCtrl'
        }).
        when('/ResetPassword', {
            templateUrl: '/Users/ResetPassword',
            controller: 'usersCtrl'
        });

}]
)
.controller('usersCtrl', ['$scope', 'usersRepo', '$routeParams', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', function ($scope, usersRepo, $rootScope, $location, $filter, filterFilter, alertService) {

    $scope.usersList = [];
    $scope.actionUser = "";
    $scope.load = true;


    if (!$rootScope.alerts)
        $rootScope.alerts = [];


    //Sorting

    $scope.sort = "UserName";
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
        $scope.filtered = filterFilter($scope.usersList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });


    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];

    $scope.setUserData = function () {
        usersRepo.getUsersList().success(function (data) {
            $scope.usersList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size

            $scope.noOfPages = ($scope.usersList) ? Math.ceil($scope.usersList.length / $scope.entryLimit) : 1;

            $scope.itemsInPage = ($scope.usersList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.usersList.length) ?
                                        $scope.usersList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };

    $scope.setUserState = function (user, state) {
        user.Active = state;

        usersRepo.updateUser(function () {
        }, user).success(function () {
            alertService.add('success', 'Mensaje', 'El Usuario se ha editado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.setUserData();
            $scope.load = false;
        }).error(function () {
            alertService.add('danger', 'Error', 'No se ha podido editar el registro.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });
    }

    $scope.setActionUser = function (action, user) {
        $scope.actionUser = action;
        if (action == "add") {
            $scope.user_modalTitle = "Agregar Usuario";
            $scope.user_buttonName = "Agregar";
        }
        else {
            $scope.user_modalTitle = "Editar Usuario";
            $scope.user_buttonName = "Editar";
            $scope.editUser(user);
        }
    }

    $scope.editUser = function (userToEdit) {
        $scope.User_UserId = userToEdit.UserId;
        $scope.User_UserName = userToEdit.UserName;
        $scope.User_Password = userToEdit.Password;
    };


    $scope.userClearData = function () {
        $scope.actionUser = "";
        $scope.User_UserId = "";
        $scope.User_UserName = "";
        $scope.User_Password = "";
    }


    $scope.saveUser = function () {
        //$scope.load = true;
        var user;
         
        if ($scope.actionUser == "add") {
            user = {
                UserName: $scope.User_UserName,
                Password: $scope.User_Password
            };

            usersRepo.insertUser(function () {
            }, user).success(function () {
                alertService.add('success', 'Mensaje', 'El Usuario se ha insertado correctamente.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.setUserData();
                $scope.load = false;
            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido insertar el registro.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });

        }
        else {
            user = {
                UserId: $scope.User_UserId,
                UserName: $scope.User_UserName,
                Password: $scope.User_Password
            };

            usersRepo.updateUser(function () {
            }, user).success(function () {
                alertService.add('success', 'Mensaje', 'El Usuario se ha editado correctamente.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.setUserData();
                $scope.load = false;
            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido editar el registro.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });
            
        }
        
        $scope.userClearData();
        $scope.load = false;
    };

    $scope.setUserToDelete = function (user) {
        $scope.userToDeleteId = user.UserId;
    };

    $scope.cancelUsertDelete = function () {
        $scope.userToDeleteId = "";
    };

    $scope.deleteUser = function () {
        $scope.load = true;
        usersRepo.deleteUser(function () {
        }, $scope.userToDeleteId).success(function () {
            alertService.add('success', 'Mensaje', 'El Usuario se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.cancelUsertDelete();
            //$scope.setUserData();
            $scope.load = false;
        }).error(function () {
            alertService.add('danger', 'Error', 'No se ha podido eliminar el registro.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });
        $scope.load = false;
    }

    $scope.login = function () {
         
        
        usersRepo.login($scope.userName, $scope.password).success(function (data) {
            var user = data;
            $scope.console = "loged in";
        }).error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
            $scope.console = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        });


        window.location = "#/";
    }

    $scope.setUserData();


    

    $scope.sendEmail = function (email) {

        $auth.submitRegistration({
            email: email
        });
    }

}]);