'use strict';

angular.module("interviewTypesController", ['ngRoute', 'interviewTypesRepository', 'alertRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Interviews', {
            templateUrl: '/InterviewTypes/Index',
            controller: 'interviewTypesCtrl'
        });
}]
)
.controller('interviewTypesCtrl', ['$scope', 'interviewTypesRepo', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', function ($scope, interviewTypesRepo, $rootScope, $location, $filter, filterFilter, alertService) {

    $scope.interviewList = [];
    $scope.actionInterview = "";
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
        $scope.filtered = filterFilter($scope.interviewList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });

    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];

    $scope.setInterviewData = function () {
        interviewTypesRepo.getInterviewList().success(function (data) {
            $scope.interviewList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size

            $scope.noOfPages = ($scope.interviewList) ? Math.ceil($scope.interviewList.length / $scope.entryLimit) : 1;

            $scope.itemsInPage = ($scope.interviewList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.interviewList.length) ?
                                        $scope.interviewList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;

            //$scope.load = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            //$scope.load = false;
        });

    };

    $scope.setActionInterview = function (action, interview) {
        $scope.actionInterview = action;
        if (action == "add") {
            $scope.interview_modalTitle = "Agregar País";
            $scope.interview_buttonName = "Agregar";
        }
        else {
            $scope.interview_modalTitle = "Editar País";
            $scope.interview_buttonName = "Editar";
            $scope.editInterview(interview);
        }
    }

    $scope.editInterview = function (interviewToEdit) {
        $scope.Interview_InterviewTypeId = interviewToEdit.InterviewTypeId
        $scope.Interview_Name = interviewToEdit.Name;

    };



    $scope.interviewClearData = function () {
        $scope.actionInterview = "";
        $scope.Interview_InterviewTypeId = "";
        $scope.Interview_Name = "";
    }


    $scope.saveInterview = function () {
        $scope.load = true;
        var interview;

        if ($filter('filter')($scope.interviewList, { Name: $scope.Interview_Name }).length == 0) {
            if ($scope.actionInterview == "add") {
                interview = {
                    Name: $scope.Interview_Name
                };

                interviewTypesRepo.insertInterview(function () {
                }, interview).success(function () {
                    alertService.add('success', 'Mensaje', 'El País se ha insertado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.setInterviewData();
                    $scope.load = false;
                }).error(function () {
                    alertService.add('danger', 'Error', 'No se ha podido insertar el registro.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.load = false;
                });

            }
            else {
                interview = {
                    InterviewTypeId: $scope.Interview_InterviewTypeId,
                    Name: $scope.Interview_Name
                };

                interviewTypesRepo.updateInterview(function () {
                }, interview).success(function () {
                    alertService.add('success', 'Mensaje', 'El País se ha editado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.setInterviewData();
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
        $scope.interviewClearData();
        $scope.load = false;

    };

    $scope.setInterviewToDelete = function (interview) {
        $scope.interviewToDeleteId = interview.InterviewTypeId;
    };

    $scope.cancelInterviewtDelete = function () {
        $scope.interviewToDeleteId = "";
    };

    $scope.deleteInterview = function () {
        $scope.load = true;
        interviewTypesRepo.deleteInterview(function () {
        }, $scope.interviewToDeleteId).success(function () {
            alertService.add('success', 'Mensaje', 'El País se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.cancelInterviewtDelete();
            $scope.setInterviewData();
            $scope.load = false;
        }).error(function () {
            alertService.add('danger', 'Error', 'No se ha podido eliminar el registro.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });



    }

    $scope.setInterviewData();

}]);