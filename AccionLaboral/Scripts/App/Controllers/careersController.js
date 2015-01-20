'use strict';

angular.module("careersController", ['ngRoute', 'careersRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Careers', {
            templateUrl: '/Careers/Index',
            controller: 'careersCtrl'
        });
}]
)
.controller('careersCtrl', ['$scope', 'careersRepo', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', function ($scope, careersRepo, $rootScope, $location, $filter, filterFilter, alertService) {

    $scope.careerList = [];
    $scope.actionCareer = "";
    $scope.load = true;
    
    careersRepo.getAcademicLevels().success(function (data) {
        $scope.Career_AcademicLevels = data;
    });

    careersRepo.getCarrerList().success(function (data) {
        $scope.careerList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    //End Sorting//
    $scope.$watch('search', function (term) {
        // Create $scope.filtered and then calculat $scope.noOfPages, no racing!
        $scope.filtered = filterFilter($scope.vacantList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });

    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];

    $scope.setCareerData = function () {
        $scope.load = true;
        careersRepo.getCarrerList().success(function (data) {
            $scope.careerList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size

            $scope.noOfPages = ($scope.careerList) ? Math.ceil($scope.careerList.length / $scope.entryLimit) : 1;

            $scope.itemsInPage = ($scope.careerList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.careerList.length) ?
                                        $scope.careerList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;

            $scope.load = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };

    $scope.setActionCareer = function (action, career) {
        $scope.actionCareer = action;
        if (action == "add") {
            $scope.career_modalTitle = "Agregar Carrera";
            $scope.career_buttonName = "Agregar";
        }
        else {
            $scope.career_modalTitle = "Editar Carrera";
            $scope.career_buttonName = "Editar";
            $scope.editCareer(career);
        }
    }

    $scope.editCareer = function (careerToEdit) {
        $scope.Career_CareerId = careerToEdit.CareerId
        $scope.Career_Name = careerToEdit.Name;
        $scope.Career_AcademicLevel = careerToEdit.AcademicLevelId;
    };

    $scope.career_refresh = function () {
        $scope.load = true;
        careersRepo.getCarrerList().success(function (data) {
            $scope.careerList = [];
            $scope.careerList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        })
    };

    $scope.careerClearData = function () {
        $scope.actionCareer = "";
        $scope.Career_CareerId = "";
        $scope.Career_Name = "";
        $scope.Career_AcademicLevel = "";
    }

        
    $scope.saveCareer = function () {
        var career;
        $scope.load = true;
        if ($scope.actionCareer == "add") {
            career = {
                Name: $scope.Career_Name,
                AcademicLevelId: $scope.Career_AcademicLevel,
            };

            careersRepo.insertCareer(function () {
            }, career).success(function () {
                //alertService.add('success', 'Enhorabuena', 'Registro se ha insertado correctamente.');
                //$scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            }).error(function () {
                //alertService.add('danger', 'Error', 'No se ha podido insertar el registro.');
                //$scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });


        }
        else {
            career = {
                CareerId: $scope.Career_CareerId,
                Name: $scope.Career_Name,
                AcademicLevelId: $scope.Career_AcademicLevel,
            };

            careersRepo.updateCareer(function () {
            }, career).success(function () {
                //alertService.add('success', 'Enhorabuena', 'Registro se ha actualizado correctamente.');
                //$scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            }).error(function () {
                //alertService.add('danger', 'Error', 'No se ha podido actualizar el registro.');
                //$scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });
              
        }

        $scope.careerClearData();

        //$scope.career_refresh();
        $scope.setCareerData();
        $scope.load = false;
    };
        
    $scope.setCareerToDelete = function (career) {
        $scope.careerToDeleteId = career.CareerId;
    };

    $scope.cancelCareertDelete = function () {
        $scope.careerToDeleteId = "";
    };
    
    
    $scope.deleteCareer = function () {
        $scope.load = true;
        careersRepo.deleteCareer(function () {            
            alert('Career deleted');
        }, $scope.careerToDeleteId).success(function () {
            $scope.cancelCareertDelete();
            $scope.setCareerData();
            $scope.load = false;
        }).error(function (error) {
            //alertService.add('danger', 'Error', 'No se ha podido eliminar el registro.');
            //$scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });;
        $scope.cancelCareertDelete();
        $scope.load = false;
    }

    $scope.setCareerData();
        
}]);