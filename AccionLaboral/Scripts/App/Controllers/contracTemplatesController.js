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
        }).
        when('/ContractReport', {
            templateUrl: '/ContractTemplates/ContractReport',
            controller: 'contractTemplateReportCtrl'
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

    $scope.saveContract = function () {
        var contract;

        if (actionContractTemplate == "add") {
            contract = {
                Name: $scope.Contract_Name,
                Description: $scope.Contract_Description,
                Active: 0
            };

            if ($filter('filter')($scope.contractTemplateList, { Name: $scope.Contract_Name }).length == 0) {
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

                $scope.actionContractTemplate = "";
                $scope.Contract_ContractTemplateId = "";
                $scope.Contract_Name = "",
                $scope.Contract_Description = "";
                $scope.Contract_Active = "";

                $scope.contract_cancelRedirect();
                 

                $scope.setContractData();
            }
            else {
                alertService.add('danger', 'Error', 'Ya existe una Plantilla de Contrato con ese nombe.');
            }
                

        }
        else {
            contract = {
                ContractTemplateId: $scope.Contract_ContractTemplateId,
                Name: $scope.Contract_Name,
                Description: $scope.Contract_Description,
                Active: $scope.Contract_Active
            };
            //if ($filter('filter')($scope.contractTemplateList, { Name: $scope.Contract_Name }).length == 0) {
                contractTemplatesRepo.updateContractTemplate(function () {
                }, contract).success(function () {
                    alertService.add('success', 'Mensaje', 'La Plantilla de Contrato se ha editado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                    $location.path("/Contracts");
                    //$scope.load = false;
                }).error(function () {
                    alertService.add('danger', 'Error', 'No se ha podido editar el registro.');
                    $scope.alertsTags = $rootScope.alerts;
                    $scope.load = false;
                });

                $scope.actionContractTemplate = "";
                $scope.Contract_ContractTemplateId = "";
                $scope.Contract_Name = "",
                $scope.Contract_Description = "";
                $scope.Contract_Active = "";

                $scope.contract_cancelRedirect();
                 

                $scope.setContractData();
            //}
            //else {
            //    alertService.add('danger', 'Error', 'Ya existe una Plantilla de Contrato con ese nombe.');
            //}

        }

        
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


    $scope.contract_changeActiveContract = function (contract) {
        //This code works when Accion Laboral used only a contract template
        //var currentActiveContract;
        //for (var i = 0; i < $scope.contractTemplateList.length; i++) {
        //    var contract = $scope.contractTemplateList[i];
        //    if (contract.Active)
        //        currentActiveContract = contract;
        //}
        
        //if (currentActiveContract != null) {
        //    currentActiveContract.Active = false;
        //    contractTemplatesRepo.updateContractTemplate(function () {
        //    }, currentActiveContract);
        //}

        //var newActiveContract = $scope.contractTemplateList[index];
        //newActiveContract.Active = true;
        //contractTemplatesRepo.updateContractTemplate(function () {
        //}, newActiveContract);


        //Now Accion Laboral can use any active contract template
        var succMsj, erroMsj;
        if (contract.Active == true) {
            contract.Active = false;
            succMsj = 'La Plantilla de Contrato se ha desactivado correctamente.'
            erroMsj = 'No se ha podido desactivar el contrato.'
        }
        else {
            contract.Active = true;
            succMsj = 'La Plantilla de Contrato se ha activado correctamente.'
            erroMsj = 'No se ha podido activar el contrato.'
        }

        contractTemplatesRepo.updateContractTemplate(function () {
        }, contract).success(function () {
            alertService.add('success', 'Mensaje', succMsj);
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        }).error(function () {
            alertService.add('danger', 'Error', erroMsj);
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });
        
    }

    $scope.setContractData();
}])
.controller('contractTemplateReportCtrl', ['$scope', 'contractTemplatesRepo', '$routeParams', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', 'customerRepository','$window', function ($scope, contractTemplatesRepo, $routeParams, $rootScope, $location, $filter, filterFilter, alertService, customerRepository, $window) {
    var actionContractTemplate = "";
    $scope.contractTemplateList = [];
    $scope.contractId = $routeParams.id;
    $scope.load = true;
    $scope.loadData = false;

    if (!$rootScope.alerts)
        $rootScope.alerts = [];


    $scope.setContractData = function () {
        contractTemplatesRepo.getActiveContractTemplateList().success(function (data) {
            $scope.contractTemplateList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos.";
            $scope.load = false;
        });

    };

   

    $scope.setContractData();

    $scope.generateContrat = function () {
        $scope.loadData = true;
        $scope.client = null;

        var dateNow = new Date();
        var day = dateNow.getDate();
        var monthNumber = dateNow.getDay();
        var month;
        switch (monthNumber) {
            case 1:
                month = "Enero";
                break;
            case 2:
                month = "Febrero";
                break;
            case 3:
                month = "Marzo";
                break;
            case 4:
                month = "Abril";
                break;
            case 5:
                month = "Mayo";
                break;
            case 6:
                month = "Junio";
                break;
            case 7:
                month = "Julio";
                break;
            case 8:
                month = "Agosto";
                break;
            case 9:
                month = "Septiembre";
                break;
            case 10:
                month = "Octubre";
                break;
            case 11:
                month = "Noviembre";
                break;
            default:
                month = "Diciembre";
        }
        var year = dateNow.getFullYear();


        customerRepository.getCustomers($rootScope.userLoggedIn).success(function (data) {
            for (var i = 0; i < data.length; i++)
            {
                if (data[i].IdentityNumber == $scope.identityNumber) {
                    $scope.client = data[i];
                    break;
                }
            }
            if ($scope.client == null)
                alertService.add('danger', 'Error', 'No existe ningun cliente con esa de identidad.');
            else {
                //replace(/abc/g, '');
                var contractContent = $scope.contractTemplate.Description;
                var finalContractContent = contractContent.replace(/{NombreCompleto}/g, $scope.client.FirstName + " " + $scope.client.LastName);
                finalContractContent = finalContractContent.replace(/{Identidad}/g, $scope.client.IdentityNumber);
                //finalContractContent = finalContractContent.replace(/{Inciso}/g, "Inciso A");
                //finalContractContent = finalContractContent.replace(/{EmpresaColocado}/g, "Inciso A");
                finalContractContent = finalContractContent.replace(/{Dia}/g, day);
                finalContractContent = finalContractContent.replace(/{Mes}/g, month);
                finalContractContent = finalContractContent.replace(/{Anio}/g, year);
                $scope.contractContent = finalContractContent;

                $scope.placeHolders = [];
                //obtener los placeholders vacios
                var cadena1 = finalContractContent.split("{");
                for (var i = 0; i < cadena1.length; i++)
                {
                    var cadena2 = cadena1[i].split("}");
                    if (cadena2.length > 1) {
                        var item = {
                            Field: "{"+cadena2[0]+"}",
                            Value: ""                            
                        }
                        var existe = false;
                        for (var x = 0; x < $scope.placeHolders.length; x++)
                        {
                            if ($scope.placeHolders[x].Field == item.Field) {
                                existe = true;
                                break;
                            }
                        }
                        if (!existe)
                            $scope.placeHolders.push(item);
                    }
                }
            }
            $scope.alertsTags = $rootScope.alerts;
            $scope.loadData = false;
        })
        .error(function () {
            alertService.add('danger', 'Error', 'No se han podido obtener el listado de contratos.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.loadData = false;
        });
        
    };

    

    $scope.replacePlaceHolders = function () {
        var contractContent = $scope.contractContent;
        for (var i = 0; i < $scope.placeHolders.length; i++) {
            var item = $scope.placeHolders[i];
            contractContent = contractContent.replace(item.Field, item.Value);
        }
        $scope.contractContent = contractContent;
    };
    

    $scope.exportContratReport = function () {
        var id = $scope.contractContent;

        var contract = {
            ClientData: $scope.client,
            ContractTemplateData: $scope.contractTemplate,
            ContractContent: $scope.contractContent
        };
        
        contractTemplatesRepo.exportContractReport(contract)
                 .success(function (data) {
                     var id = data.FileName;
                     $window.open("ContractTemplates/Download/" + id, '_blank');
                 }).error(function (data, status, headers, config) {
                     alertService.add('danger', 'Error', 'No se ha podido generar el reporte.');
                     $scope.alertsTags = $rootScope.alerts;
                 });
    };


}])
;