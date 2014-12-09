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
.controller('careersCtrl', ['$scope', 'careersRepo', function ($scope, careersRepo) {

    $scope.careerList = [];
    $scope.actionCareer = "";
    
    careersRepo.getAcademicLevels().success(function (data) {
        $scope.Career_AcademicLevels = data;
    });

    careersRepo.getCarrerList().success(function (data) {
        debugger
        $scope.careerList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.loading = false;
    })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.loading = false;
        });

    $scope.setActionCareer = function (action, index) {
        debugger
        $scope.actionCareer = action;
        if (action == "add") {
            $scope.career_modalTitle = "Agregar Carrera";
            $scope.career_buttonName = "Agregar";
        }
        else {
            $scope.career_modalTitle = "Editar Carrera";
            $scope.career_buttonName = "Editar";
            $scope.editCareer(index);
        }
    }

    $scope.editCareer = function (index) {
        debugger
        var careerToEdit = $scope.careerList[index];
        $scope.Career_CareerId = careerToEdit.CareerId
        $scope.Career_Name = careerToEdit.Name;
        $scope.Career_AcademicLevel = careerToEdit.AcademicLevelId;
    };

    $scope.saveCareer = function () {
        debugger
        
        var career;

        if ($scope.actionCareer == "add") {
            career = {
                Name: $scope.Career_Name,
                AcademicLevelId: $scope.Career_AcademicLevel.AcademicLevelId,
                AcademicLevelName: $scope.Career_AcademicLevel.Name
            };

            careersRepo.insertCareer(function () {
            }, career);

            $scope.careerList.push(career);
        }
        else {
            career = {
                CareerId: $scope.Career_CareerId,
                Name: $scope.Career_Name,
                AcademicLevelId: $scope.Career_AcademicLevel.AcademicLevelId,
                AcademicLevelName: $scope.Career_AcademicLevel.Name
            };

            careersRepo.updateCareer(function () {
            }, career);
              
            //refresh table with la values edited
        }

        $scope.actionCareer = "";
        //$scope.Career_CareerId
        $scope.Name = "",
        $scope.Career_AcademicLevel = "";
        
    };

    
    $scope.removeCareer = function (index) {
        $scope.careerList.splice(index, 1);
    };

    $scope.deleteCareer = function (index) {
        var id = $scope.careerList[index].CareerId;
        $scope.removeCareer(index);
        careersRepo.deleteCareer(function () {            
            alert('Career deleted');
            //removeCareer(index);
        }, id);
        
    }
}]);