'use strict';

angular.module("contractTemplatesController", ['ngRoute', 'contractTemplatesRepository', 'alertRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Contracts', {
            templateUrl: '/ContractTemplates/Index',
            controller: 'contractTemplatesCtrl'
        }).
        when('/Contracts/Create', {
            templateUrl: '/ContractTemplates/Create',
            controller: 'contractTemplatesCtrl'
        }).
        when('/Contracts/Edit/:id', {
            templateUrl: function (params) {
                return '/ContractTemplates/Edit/' + params.id;
            },
            controller: 'contractTemplatesCtrl'
        }).
        when('/Contracts/Preview/:id', {
            templateUrl: function (params) {
                return '/ContractTemplates/Details/' + params.id;
            },
            controller: 'contractTemplatesCtrl'
        })
    ;
}]
)
.controller('contractTemplatesCtrl', ['$scope', 'contractTemplatesRepo', '$routeParams', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', function ($scope, contractTemplatesRepo, $routeParams, $rootScope, $location, $filter, filterFilter, alertService) {
    var actionContractTemplate = "";
    $scope.contractTemplateList = [];
    $scope.contractId = $routeParams.id;
    $scope.load = true;

    if (!$rootScope.alerts)
        $rootScope.alerts = [];

    //$scope.$watch('$routeChangeSuccess', function () {
    //    contractTemplatesRepo.getContractTemplateList().success(function (data) {
    //        $scope.contractTemplateList = data;
    //        $scope.totalServerItems = data.totalItems;
    //        $scope.items = data.items;
    //        $scope.load = false;
    //    })
    //    .error(function (data) {
    //        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
    //        $scope.load = false;
    //    });
    //});

   if ($scope.contractId == null) {
        actionContractTemplate = "add";
        $scope.contract_modalTitle = "Agregar Plantilla de Contrato";
        $scope.contract_buttonName = "Agregar";
    }
    else {
        actionContractTemplate = "edit";
        $scope.contract_modalTitle = "Editar Plantilla de Contrato";
        $scope.contract_buttonName = "Editar";        
        var id = $scope.contractId;
    
        contractTemplatesRepo.getContractTemplate(id).success(function (data) {
            var contractToEdit = data;
            $scope.Contract_ContractTemplateId = contractToEdit.ContractTemplateId;
            $scope.Contract_Name = contractToEdit.Name;
            $scope.Contract_Description = contractToEdit.Description;
            $scope.Contract_Active = contractToEdit.Active;
        });
    }    

    contractTemplatesRepo.getContractTemplateList().success(function (data) {
        $scope.contractTemplateList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    }).error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
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
        $scope.filtered = filterFilter($scope.contractTemplateList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });


    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];

    $scope.setContractData = function () {
        contractTemplatesRepo.getContractTemplateList().success(function (data) {
            $scope.contractTemplateList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size

            $scope.noOfPages = ($scope.contractTemplateList) ? Math.ceil($scope.contractTemplateList.length / $scope.entryLimit) : 1;

            $scope.itemsInPage = ($scope.contractTemplateList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.contractTemplateList.length) ?
                                        $scope.contractTemplateList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };

    $scope.contract_addNewRedirect = function () {
        window.location = "#/Contracts/Create";
    }

    $scope.contract_editRedirect = function (contract) {
        window.location = "#/Contracts/Edit/" + contract.ContractTemplateId;
    }

    $scope.contract_viewRedirect = function (contract) {
        window.location = "#/Contracts/Preview/" + contract.ContractTemplateId;
    }

    $scope.contract_cancelRedirect = function () {
        window.location = "#/Contracts";
    }

    $scope.contract_refresh = function () {
        contractTemplatesRepo.getContractTemplateList().success(function (data) {
            $scope.contractTemplateList = data;
            $scope.load = false;
        });
    };
        
    $scope.saveContract = function () {
        var contract;

        if (actionContractTemplate == "add") {
            contract = {
                Name: $scope.Contract_Name,
                Description: $scope.Contract_Description,
                Active: 0
            };

            contractTemplatesRepo.insertContractTemplate(function () {
            }, contract).success(function () {
                alertService.add('success', 'Mensaje', 'La Plantilla de Contrato se ha insertado correctamente.');
                $scope.alertsTags = $rootScope.alerts;
                $location.path("/Contracts");
                //$scope.load = false;
            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido insertar el registro.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });

        }
        else {
            contract = {
                ContractTemplateId: $scope.Contract_ContractTemplateId,
                Name: $scope.Contract_Name,
                Description: $scope.Contract_Description,
                Active: $scope.Contract_Active
            };

            contractTemplatesRepo.updateContractTemplate(function () {
            }, contract).success(function () {
                alertService.add('success', 'Mensaje', 'La Plantilla de Contrato se ha modificado correctamente.');
                $scope.alertsTags = $rootScope.alerts;
                $location.path("/Contracts");
                //$scope.load = false;
            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido modificar el registro.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });
        }

        $scope.actionContractTemplate = "";
        $scope.Contract_ContractTemplateId = "";
        $scope.Contract_Name = "",
        $scope.Contract_Description = "";
        $scope.Contract_Active = "";

        $scope.contract_cancelRedirect();
        $scope.contract_refresh();

        $scope.setContractData();
    };

    $scope.setContractToDelete = function (contract) {
        $scope.contractToDeleteId = contract.ContractTemplateId;
    };

    $scope.cancelContractDelete = function () {
        $scope.contractToDeleteId = "";
    };

    $scope.deleteContractTemplate = function () {
        $scope.load = true;
        contractTemplatesRepo.deleteContractTemplate(function () {
            alert('Contract Template deleted');
        }, $scope.contractToDeleteId).success(function () {
            alertService.add('success', 'Mensaje', 'La Plantilla de Contrato se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.cancelContractDelete();
            $scope.setContractData();
            $scope.load = false;
        }).error(function () {
            alertService.add('danger', 'Error', 'No se ha podido eliminar el registro.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });
    }


    $scope.contract_changeActiveContract = function (index) {
        var currentActiveContract;
        for (var i = 0; i < $scope.contractTemplateList.length; i++) {
            var contract = $scope.contractTemplateList[i];
            if (contract.Active)
                currentActiveContract = contract;
        }
        
        if (currentActiveContract != null) {
            currentActiveContract.Active = false;
            contractTemplatesRepo.updateContractTemplate(function () {
            }, currentActiveContract);
        }

        var newActiveContract = $scope.contractTemplateList[index];
        newActiveContract.Active = true;
        contractTemplatesRepo.updateContractTemplate(function () {
        }, newActiveContract);
    }

    $scope.setContractData();
}]);