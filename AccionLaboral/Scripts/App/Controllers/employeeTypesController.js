'use strict';

angular.module("employeeTypesController", ['ngRoute', 'employeeTypesRepository', 'alertRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/EmployeeTypes', {
            templateUrl: '/Roles/Index',
            controller: 'employeeTypesCtrl'
        });
}]
)
.controller('employeeTypesCtrl', ['$scope', 'employeeTypesRepo', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', function ($scope, employeeTypesRepo, $rootScope, $location, $filter, filterFilter, alertService) {

    $scope.employeeTypesList = [];
    $scope.actionEmployeeType = "";
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
        $scope.filtered = filterFilter($scope.employeeTypesList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });

    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];

    $scope.setEmployeeTypeData = function () {
        employeeTypesRepo.getEmployeeTypesList().success(function (data) {
            $scope.employeeTypesList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size

            $scope.noOfPages = ($scope.employeeTypesList) ? Math.ceil($scope.employeeTypesList.length / $scope.entryLimit) : 1;

            $scope.itemsInPage = ($scope.employeeTypesList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.employeeTypesList.length) ?
                                        $scope.employeeTypesList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;

            //$scope.load = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            //$scope.load = false;
        });

    };

    $scope.setActionEmployeeType = function (action, employeeType) {
        $scope.actionEmployeeType = action;
        if (action == "add") {
            $scope.employeeType_modalTitle = "Agregar Tipo de Empleado";
            $scope.employeeType_buttonName = "Agregar";
        }
        else {
            $scope.employeeType_modalTitle = "Editar Tipo de Empleado";
            $scope.employeeType_buttonName = "Editar";
            $scope.editEmployeeType(employeeType);
        }
    }

    $scope.editEmployeeType = function (employeeTypeToEdit) {
        $scope.EmployeeType_Id = employeeTypeToEdit.Id;
        $scope.EmployeeType_Name = employeeTypeToEdit.Name;
        $scope.EmployeeType_Alias = employeeTypeToEdit.Alias;
    };



    $scope.employeeTypeClearData = function () {
        $scope.actionEmployeeType = "";
        $scope.EmployeeType_Id = "";
        $scope.EmployeeType_Name = "";
        $scope.EmployeeType_Alias = "";
    }


    $scope.saveEmployeeType = function () {
        $scope.load = true;
        var employeeType;

        if ($filter('filter')($scope.employeeTypesList, { Name: $scope.EmployeeType_Name }).length == 0) {
            if ($scope.actionEmployeeType == "add") {
                employeeType = {
                    Alias: $scope.EmployeeType_Name,
                    Name: $scope.EmployeeType_Name
                };

                employeeTypesRepo.insertEmployeeType(function () {
                }, employeeType).success(function () {
                    alertService.add('success', 'Mensaje', 'El Tipo de Empleado se ha insertado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.setEmployeeTypeData();
                    $scope.load = false;
                }).error(function () {
                    alertService.add('danger', 'Error', 'No se ha podido insertar el registro.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.load = false;
                });

            }
            else {
                employeeType = {
                    Id: $scope.EmployeeType_Id,
                    Alias: $scope.EmployeeType_Alias,
                    Name: $scope.EmployeeType_Name
                };

                employeeTypesRepo.updateEmployeeType(function () {
                }, employeeType).success(function () {
                    alertService.add('success', 'Mensaje', 'El Tipo de Empleado se ha editado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.setEmployeeTypeData();
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
        $scope.employeeTypeClearData();
        $scope.load = false;

    };

    $scope.setEmployeeTypeToDelete = function (employeeType) {
        $scope.employeeTypeToDeleteId = employeeType.Id;
    };

    $scope.cancelEmployeeTypetDelete = function () {
        $scope.employeeTypeToDeleteId = "";
    };

    $scope.deleteEmployeeType = function () {
        $scope.load = true;
        employeeTypesRepo.deleteEmployeeType(function () {
        }, $scope.employeeTypeToDeleteId).success(function () {
            alertService.add('success', 'Mensaje', 'El Tipo de Empleado se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.cancelEmployeeTypetDelete();
            $scope.setEmployeeTypeData();
            $scope.load = false;
        }).error(function () {
            alertService.add('danger', 'Error', 'No se ha podido eliminar el registro.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });



    }

    $scope.setEmployeeTypeData();

}]);