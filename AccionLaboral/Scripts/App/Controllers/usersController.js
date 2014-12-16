'use strict';

angular.module("usersController", ['ngRoute', 'usersRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Users', {
            templateUrl: '/Users/Index',
            controller: 'usersCtrl'
        });
}]
)
.controller('usersCtrl', ['$scope', 'usersRepo', function ($scope, usersRepo) {

    $scope.usersList = [];
    $scope.actionUser = "";
    $scope.load = true;
    usersRepo.getUsersList().success(function (data) {
        $scope.usersList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    $scope.setActionUser = function (action, index) {
        $scope.actionUser = action;
        if (action == "add") {
            $scope.user_modalTitle = "Agregar Usuario";
            $scope.user_buttonName = "Agregar";
        }
        else {
            $scope.user_modalTitle = "Editar Usuario";
            $scope.user_buttonName = "Editar";
            $scope.editUser(index);
        }
    }

    $scope.editUser = function (index) {
        var userToEdit = $scope.usersList[index];
        $scope.User_UserId = userToEdit.UserId;
        $scope.User_UserName = userToEdit.UserName;
        $scope.User_Password = userToEdit.Password;
    };

    $scope.user_refresh = function () {

        usersRepo.getUsersList().success(function (data) {
            debugger
            $scope.usersList = [];
            $scope.usersList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        })
    };

    $scope.userClearData = function () {
        $scope.actionUser = "";
        $scope.User_UserId = "";
        $scope.User_UserName = "";
        $scope.User_Password = "";
    }


    $scope.saveUser = function () {
        var user;

        if ($scope.actionUser == "add") {
            user = {
                UserName: $scope.User_UserName,
                Password: $scope.User_Password
            };

            usersRepo.insertUser(function () {
            }, user);

            //$scope.usersList.push(user);
        }
        else {
            user = {
                UserId: $scope.User_UserId,
                UserName: $scope.User_UserName,
                Password: $scope.User_Password
            };

            usersRepo.updateUser(function () {
            }, user);

        }

        $scope.userClearData();

        $scope.user_refresh();

    };

    $scope.setUserToDelete = function (index) {
        var id = $scope.usersList[index].UserId;
        $scope.userToDeleteId = id;
        $scope.userToDeleteIndex = index;
    };

    $scope.cancelUsertDelete = function () {
        $scope.userToDeleteId = "";
        $scope.userToDeleteIndex = "";
    };

    $scope.removeUser = function (index) {
        $scope.usersList.splice(index, 1);
    };

    $scope.deleteUser = function () {
        var id = $scope.userToDeleteId;
        $scope.removeUser($scope.userToDeleteIndex);
        usersRepo.deleteUser(function () {
            alert('User deleted');
            //removeUser(index);
        }, id);
        $scope.cancelUsertDelete();
    }



}]);