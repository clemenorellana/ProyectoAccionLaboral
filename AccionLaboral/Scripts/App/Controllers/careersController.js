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

    $scope.editCareer = function (index) {
        
    };

    $scope.addNewCareer = function () {
        debugger
        var lang = {
            Name: $scope.Name,
            AcademicLevelId: $scope.AcademicLevelId
        };

        $scope.careerList.push(lang);

        $scope.Name = "",
        $scope.AcademicLevelId = "";
    };
    
    //careersRepo.getAcademicLevels().success(function (data) {
    //    debugger
    //    $scope.CarrerAcademicLevels = data;
    //});
    
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