'use strict';

angular.module("contractTemplatesController", ['ngRoute', 'contractTemplatesRepository'])
.config(['$routeProvider', function ($routeProvider) {
    debugger
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
            templateUrl: '/ContractTemplates/Edit',
            controller: 'contractTemplatesCtrl'
        })    
    ;
}]
)
.controller('contractTemplatesCtrl', ['$scope', 'contractTemplatesRepo', '$routeParams', function ($scope, contractTemplatesRepo, $routeParams) {
    debugger
    $scope.contractTemplateList = [];

    $scope.model = {
        message: $routeParams.message
    }
    
    contractTemplatesRepo.getContractTemplateList().success(function (data) {
        $scope.contractTemplateList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.loading = false;
    })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.loading = false;
        });

    
    $scope.contract_addNewRedirect = function () {
        $scope.setActionContractTemplate("add");
        window.location = "#/Contracts/Create";
    }

    $scope.contract_editRedirect = function (index) {
        debugger
        //$scope.setActionContractTemplate("edit");
        var id = $scope.contractTemplateList[index].ContractTemplateId;
        window.location = "#/Contracts/Edit/" + id;
    }

    $scope.contract_cancelRedirect = function () {
        window.location = "#/Contracts";
    }

    $scope.setActionContractTemplate = function (action, index) {
        
        $scope.actionContractTemplate = action;
        if (action == "add") {
            $scope.contract_modalTitle = "Agregar Plantilla de Contrato";
            $scope.contract_buttonName = "Agregar";
        }
        else {
            $scope.contract_modalTitle = "Editar Plantilla de Contrato";
            $scope.contract_buttonName = "Editar";
            $scope.editContract(index);
        }
    }

    //$scope.editContract = function (index) {
    //    debugger
    //    var contractToEdit = $scope.contractTemplateList[index];
    //    $scope.Contract_ContractId = contractToEdit.ContractId
    //    $scope.Contract_Name = contractToEdit.Name;
    //    $scope.Contract_AcademicLevel = contractToEdit.AcademicLevelId;
    //};

    $scope.saveContract = function () {
        

        var contract;

        //if ($scope.actionContractTemplate == "add") {
            contract = {
                Name: $scope.contractName,
                Description: $scope.contractDescription,
                Active: 0
            };

            contractTemplatesRepo.insertContractTemplate(function () {
            }, contract);

            $scope.contractTemplateList.push(contract);
        //}
        //else {
        //    contract = {
        //        ContractTemplateId: $scope.contractTemplateId,
        //        Name: $scope.contractName,
        //        Description: $scope.contractDescription,
        //        Active: $scope.contractActive
        //    };

        //    contractTemplatesRepo.updateContractTemplate(function () {
        //    }, contract);

        //    //TODO: refresh table with la values edited
        //}

        $scope.actionContractTemplate = "";
        $scope.contractId = "";
        $scope.contractName = "",
        $scope.contractDescription = "";
        $scope.contractActive = "";

        $scope.contract_cancelRedirect();

    };


    $scope.removeContractTemplate = function (index) {
        $scope.contractTemplateList.splice(index, 1);
    };

    $scope.deleteContractTemplate = function (index) {
        var id = $scope.contractTemplateList[index].ContractTemplateId;
        $scope.removeContractTemplate(index);
        contractTemplatesRepo.deleteContractTemplate(function () {
            alert('Contract Template deleted');
            //removeContract(index);
        }, id);

    }
}]);