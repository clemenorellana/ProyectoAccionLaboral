'use strict';

angular.module("contractTemplatesController", ['ngRoute', 'contractTemplatesRepository'])
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
.controller('contractTemplatesCtrl', ['$scope', 'contractTemplatesRepo', '$routeParams', function ($scope, contractTemplatesRepo, $routeParams) {
    var actionContractTemplate = "";
    $scope.contractTemplateList = [];
    $scope.contractId = $routeParams.id;
   
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
        $scope.loading = false;
    }).error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.loading = false;
        });
    
    $scope.contract_addNewRedirect = function () {
        window.location = "#/Contracts/Create";
    }
        
    $scope.contract_editRedirect = function (index) {
        var id = $scope.contractTemplateList[index].ContractTemplateId;
        window.location = "#/Contracts/Edit/" + id;
    }

    $scope.contract_viewRedirect = function (index) {
        var id = $scope.contractTemplateList[index].ContractTemplateId;
        window.location = "#/Contracts/Preview/" + id;
    }

    $scope.contract_cancelRedirect = function () {
        window.location = "#/Contracts";
    }

    $scope.contract_refresh = function () {
        contractTemplatesRepo.getContractTemplateList().success(function (data) {
            $scope.contractTemplateList = data;
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
            }, contract);

            $scope.contractTemplateList.push(contract);
        }
        else {
            contract = {
                ContractTemplateId: $scope.Contract_ContractTemplateId,
                Name: $scope.Contract_Name,
                Description: $scope.Contract_Description,
                Active: $scope.Contract_Active
            };

            contractTemplatesRepo.updateContractTemplate(function () {
            }, contract);
        }

        $scope.actionContractTemplate = "";
        $scope.Contract_ContractTemplateId = "";
        $scope.Contract_Name = "",
        $scope.Contract_Description = "";
        $scope.Contract_Active = "";
        
        $scope.contract_cancelRedirect();
        $scope.contract_refresh();
    };
    
    $scope.setContractToDelete = function (index) {
        var id = $scope.contractTemplateList[index].ContractTemplateId;
        $scope.contractToDeleteId = id;
        $scope.contractToDeleteIndex = index;
    };

    $scope.cancelContractDelete = function () {
        $scope.contractToDeleteId = "";
        $scope.contractToDeleteIndex = "";
    };

    $scope.removeContractTemplate = function (index) {
        $scope.contractTemplateList.splice(index, 1);
    };

    $scope.deleteContractTemplate = function () {
        var id = $scope.contractToDeleteId;
        $scope.removeContractTemplate($scope.contractToDeleteIndex);
        contractTemplatesRepo.deleteContractTemplate(function () {
            alert('Contract Template deleted');
            //removeContract(index);
        }, id);
        $scope.cancelContractDelete();
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

}]);