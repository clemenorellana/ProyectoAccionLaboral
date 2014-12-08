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
        var careerToEdit = $scope.careerList[index];
        $scope.Career_CareerId = careerToEdit.CareerId
        $scope.Career_Name = careerToEdit.Name;
        $scope.Career_AcademicLevel = careerToEdit.AcademicLevelId;
    };

    $scope.career_refresh = function () {
        careersRepo.getCarrerList().success(function (data) {
            $scope.careerList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.loading = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.loading = false;
        })
    };

    $scope.careerClearData = function () {
        $scope.actionCareer = "";
        $scope.Career_CareerId
        $scope.Career_Name = "",
        $scope.Career_AcademicLevel = "";
    }


    $scope.saveCareer = function () {
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
              
        }

        $scope.careerClearData();

        $scope.career_refresh();
        
    };

    $scope.setCareerToDelete = function (index) {
        var id = $scope.careerList[index].CareerId;
        $scope.careerToDeleteId = id;
        $scope.careerToDeleteIndex = index;
    };

    $scope.cancelCareertDelete = function () {
        $scope.careerToDeleteId = "";
        $scope.careerToDeleteIndex = "";
    };
    
    $scope.removeCareer = function (index) {
        $scope.careerList.splice(index, 1);
    };

    $scope.deleteCareer = function () {
        var id = $scope.careerToDeleteId;
        $scope.removeCareer($scope.careerToDeleteIndex);
        careersRepo.deleteCareer(function () {            
            alert('Career deleted');
            //removeCareer(index);
        }, id);
        $scope.cancelCareertDelete();
    }



}]);