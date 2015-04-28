'use strict';

angular.module("careersController", ['ngRoute', 'careersRepository', 'alertRepository'])
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

    if (!$rootScope.alerts)
        $rootScope.alerts = [];

    
    careersRepo.getAcademicLevels().success(function (data) {
        $scope.Career_AcademicLevels = data;
    });

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
        $scope.filtered = filterFilter($scope.careerList, term);
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
            $scope.error = "Ha ocurrido un error al cargar los datos." + ' \nDetalle: ' + data.ExceptionMessage;
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

            var levelFilter = $filter('filter')($scope.careerList, { AcademicLevelId: $scope.Career_AcademicLevel });
            var careerFilter = (levelFilter.length > 0) ? $filter('filter')(levelFilter, { Name: $scope.Career_Name }) : 0;
            
            if (careerFilter == 0 ) {
                    careersRepo.insertCareer(function () {
                    }, career).success(function () {

                        alertService.add('success', 'Mensaje', 'La carrera se ha insertado correctamente.');
                        $scope.alertsTags = $rootScope.alerts;
                        $scope.setCareerData();
                        $scope.load = false;
                    }).error(function (error) {
                        var x = error.ExceptionMessage;
                        alertService.add('danger', 'Error', 'No se ha podido insertar el registro.');
                        $scope.alertsTags = $rootScope.alerts;
                        $scope.load = false;
                    });
            }
            else {
                alertService.add('danger', 'Error', 'Ya existe una Carrera con ese nombre');
            }
        }
        else {
            career = {
                CareerId: $scope.Career_CareerId,
                Name: $scope.Career_Name,
                AcademicLevelId: $scope.Career_AcademicLevel,
            };
            
            var levelFilter = $filter('filter')($scope.careerList, { AcademicLevelId: $scope.Career_AcademicLevel });
            var careerFilter = (levelFilter.length > 0) ? $filter('filter')(levelFilter, { Name: $scope.Career_Name }) : 0;

            if (careerFilter == 0) {
                    careersRepo.updateCareer(function () {
                    }, career).success(function () {
                        alertService.add('success', 'Mensaje', 'La carrera se ha editado correctamente.');
                        $scope.alertsTags = $rootScope.alerts;
                        $scope.setCareerData();
                        $scope.load = false;
                    }).error(function () {
                        alertService.add('danger', 'Error', 'No se ha podido editar el registro.');
                        $scope.alertsTags = $rootScope.alerts;
                        $scope.load = false;
                    });
            }
            else {
                alertService.add('danger', 'Error', 'Ya existe una Carrera con ese nombre');
            }
        }


        $scope.careerClearData();        
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
            alertService.add('success', 'Mensaje', 'La carrera se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.cancelCareertDelete();
            $scope.setCareerData();
            $scope.load = false;
        }).error(function () {
            alertService.add('danger', 'Error', 'No se ha podido eliminar el registro.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });

        $scope.cancelCareertDelete();
        $scope.load = false;
    }

    $scope.setCareerData();
        
}]);