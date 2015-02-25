'use strict';

angular.module("statesController", ['ngRoute', 'statesRepository', 'alertRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/States', {
            templateUrl: '/States/Index',
            controller: 'statesCtrl'
        });
}]
)
.controller('statesCtrl', ['$scope', 'statesRepo', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', function ($scope, statesRepo, $rootScope, $location, $filter, filterFilter, alertService) {

    $scope.statesList = [];
    $scope.actionState = "";
    $scope.load = true;

    if (!$rootScope.alerts)
        $rootScope.alerts = [];



    //Sorting
    $scope.sort = "Name";
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
        // Create $scope.filtered and then calculat $scope.noOfPages, no racing!
        $scope.filtered = filterFilter($scope.statesList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });

    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];

    $scope.setStateData = function () {
        statesRepo.getStatesList().success(function (data) {
            $scope.statesList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size

            $scope.noOfPages = ($scope.statesList) ? Math.ceil($scope.statesList.length / $scope.entryLimit) : 1;

            $scope.itemsInPage = ($scope.statesList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.statesList.length) ?
                                        $scope.statesList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;

            $scope.load = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };

    $scope.setActionState = function (action, state) {
        $scope.actionState = action;
        if (action == "add") {
            $scope.state_modalTitle = "Agregar Estado";
            $scope.state_buttonName = "Agregar";
        }
        else {
            $scope.state_modalTitle = "Editar Estado";
            $scope.state_buttonName = "Editar";
            $scope.editState(state);
        }
    }

    $scope.editState = function (stateToEdit) {
        $scope.State_StateId = stateToEdit.StateId
        $scope.State_Name = stateToEdit.Name;

    };



    $scope.stateClearData = function () {
        $scope.actionState = "";
        $scope.State_StateId = "";
        $scope.State_Name = "";
    }


    $scope.saveState = function () {
        $scope.load = true;
        var state;

        if ($filter('filter')($scope.statesList, { Name: $scope.State_Name }).length == 0) {
            if ($scope.actionState == "add") {
                state = {
                    Name: $scope.State_Name
                };

                statesRepo.insertState(function () {
                }, state).success(function () {
                    alertService.add('success', 'Mensaje', 'El Estado se ha insertado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.setStateData();
                    $scope.load = false;
                }).error(function () {
                    alertService.add('danger', 'Error', 'No se ha podido insertar el registro.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.load = false;
                });

            }
            else {
                state = {
                    StateId: $scope.State_StateId,
                    Name: $scope.State_Name
                };

                statesRepo.updateState(function () {
                }, state).success(function () {
                    alertService.add('success', 'Mensaje', 'El Estado se ha editado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.setStateData();
                    $scope.load = false;
                }).error(function () {
                    alertService.add('danger', 'Error', 'No se ha podido editar el registro.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.load = false;
                });

            }


        }
        else {
            alertService.add('danger', 'Error', 'Ya existe un registro con ese nombre');
        }
        $scope.stateClearData();
        $scope.load = false;

    };

    $scope.setStateToDelete = function (state) {
        $scope.stateToDeleteId = state.StateId;
    };

    $scope.cancelStatetDelete = function () {
        $scope.stateToDeleteId = "";
    };

    $scope.deleteState = function () {
        $scope.load = true;
        statesRepo.deleteState(function () {
        }, $scope.stateToDeleteId).success(function () {
            alertService.add('success', 'Mensaje', 'El Estado se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.cancelStatetDelete();
            $scope.setStateData();
            $scope.load = false;
        }).error(function () {
            alertService.add('danger', 'Error', 'No se ha podido eliminar el registro.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });



    }

    $scope.setStateData();

}]);