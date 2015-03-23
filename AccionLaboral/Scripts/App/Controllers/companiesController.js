'use strict';

angular.module("companiesController", ['ngRoute', 'companiesRepository', 'alertRepository'])
.config(['$routeProvider', function ($routeProvider) {

    $routeProvider.
        when('/Companies', {
            templateUrl: '/Companies/Index',
            controller: 'companiesCtrl'
        }).
        when('/AddCompany', {
            templateUrl: '/Companies/Create',
            controller: 'companiesCtrl'
        }).
        when('/EditCompany/:id', {
            templateUrl: function (params) {
                return '/Companies/Edit/' + params.id;
            },
            controller: 'companiesCtrl'
        }).
        when('/NewCompaniesReport', {
            templateUrl: '/Companies/NewCompaniesReport',
            controller: 'newCompaniesReportCtrl'
        });
}]
)
.controller('companiesCtrl', ['$scope', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', 'companiesRepo', '$routeParams', function ($scope, $rootScope, $location, $filter, filterFilter, alertService, companiesRepo, $routeParams) {

    $scope.companyList = [];
    $scope.load = true;
    $scope.contactsByCompanyList = [];
    $scope.actionContact = 'Agregar'
    $scope.contactIndex;
    var actionCompany;

    if (!$rootScope.alerts)
        $rootScope.alerts = [];


    $scope.companyId = $routeParams.id;

    if ($scope.companyId == null) {
        actionCompany = "add";
        $scope.tittleCompanyForm = "Agregar datos de una Empresa";
        $scope.buttonNameCompanyForm = "Agregar";
    }
    else {
        actionCompany = "edit";
        $scope.tittleCompanyForm = "Editar datos de una Empresa";
        $scope.buttonNameCompanyForm = "Editar";
        var id = $scope.companyId;

        companiesRepo.getCompany(id).success(function (data) {
            var companyToEdit = data;
            $scope.company_CompanyId = companyToEdit.CompanyId;
            $scope.company_Name = companyToEdit.Name;
            $scope.company_Area = companyToEdit.Area;
            $scope.company_DateCreated = companyToEdit.DateCreated;
            $scope.contactsByCompanyList = companyToEdit.ContactsByCompany;
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
        // Create $scope.filtered and then calculat $scope.noOfPages, no racing!
        $scope.filtered = filterFilter($scope.companyList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });

    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];

    $scope.setCompanyData = function () {
        companiesRepo.getCompanyList().success(function (data) {
            $scope.companyList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size
            //max rows for data table

            $scope.noOfPages = ($scope.companyList) ? Math.ceil($scope.companyList.length / $scope.entryLimit) : 1;

            $scope.itemsInPage = ($scope.companyList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.companyList.length) ?
                    $scope.companyList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;

        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };


    $scope.setActionContact = function (index, action) {

        if (action == 'edit') {
            $scope.actionContact = 'Editar'
            var contact = $scope.contactsByCompanyList[index];
            $scope.contact_ContactByCompanyId = contact.ContactByCompanyId;
            $scope.contact_Name = contact.ContactName;
            $scope.contact_Email = contact.ContactEmail;
            $scope.contact_Telephone = contact.Telephone;
            $scope.contact_Extension = contact.Extension;
            $scope.contact_Observations = contact.Observations;
            $scope.contact_CompanyId = contact.CompanyId;
        }
        else {
            $scope.actionContact = 'Agregar'

        }
        $scope.contactIndex = index;

    }

    $scope.saveContactByCompany = function () {

        var contact = {
            ContactByCompanyId: $scope.contact_ContactByCompanyId,
            ContactName: $scope.contact_Name,
            ContactEmail: $scope.contact_Email,
            Telephone: $scope.contact_Telephone,
            Extension: $scope.contact_Extension,
            Observations: $scope.contact_Observations,
            CompanyId: $scope.contact_CompanyId
        };

        if ($scope.actionContact == 'Agregar') {
            $scope.contactsByCompanyList.push(contact);
        }
        else {
            $scope.contactsByCompanyList[$scope.contactIndex] = contact;
        }

        $scope.clearContactForm();

    }

    $scope.editContactByCompany = function (index) {
        var contact = $scope.contactsByCompanyList[index];
        $scope.contact_Name = contact.ContactName;
        $scope.contact_Email = contact.ContactEmail;
        $scope.contact_Telephone = contact.Telephone;
        $scope.contact_Extension = contact.Extension;
        $scope.contact_Observations = contact.Observations;
    }

    $scope.clearContactForm = function () {
        $scope.contact_Name = "";
        $scope.contact_Email = "";
        $scope.contact_Telephone = "";
        $scope.contact_Extension = "";
        $scope.contact_Observations = "";
        $scope.actionContact = 'Agregar'
    }

    $scope.removeContactByCompany = function (index) {
        $scope.contactsByCompanyList.splice(index, 1);
    }



    $scope.saveCompany = function () {
        if (!$scope.companyForm.$valid)
            return;

        var newCompany = {
            CompanyId: $scope.company_CompanyId,
            CompanyName: $scope.company_Name,
            CompanyArea: $scope.company_Area,
            DateCreated: $scope.company_DateCreated,
            ContactsByCompany: [],
            //aagregar vacantes
        }

        if ($scope.contactsByCompanyList != null) {
            for (var j = 0; j < $scope.contactsByCompanyList.length; j++) {
                var contactByCompany = $scope.contactsByCompanyList[j];
                var contact = {
                    ContactByCompanyId: contactByCompany.ContactByCompanyId,
                    ContactName: contactByCompany.ContactName,
                    ContactEmail: contactByCompany.ContactEmail,
                    Telephone: contactByCompany.Telephone,
                    Extension: contactByCompany.Extension,
                    Observations: contactByCompany.Observations,
                    CompanyId: contactByCompany.CompanyId
                };
                newCompany.ContactsByCompany.push(contact);
            }
        }

        if (actionCompany == "add") {
            companiesRepo.insertCompany(function () { }, newCompany).success(function () {
                alertService.add('success', 'Mensaje', 'La Empresa se ha insertado correctamente.');
                $scope.alertsTags = $rootScope.alerts;
                $location.path("/Companies");
                //$scope.load = false;
            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido insertar el registro.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });
        }
        else {
            companiesRepo.updateCompany(function () { }, newCompany).success(function () {
                alertService.add('success', 'Mensaje', 'La Empresa se ha editado correctamente.');
                $scope.alertsTags = $rootScope.alerts;
                $location.path("/Companies");
                //$scope.load = false;
            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido editar el registro.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });
        }
        $scope.setCompanyData();
        window.location = "#/Companies";
    };

    $scope.company_cancelRedirect = function () {
        window.location = "#/Companies";
    }

    $scope.company_addNewRedirect = function () {
        window.location = "#/AddCompany";
    }

    $scope.setCompanyToDelete = function (id, index) {
        $scope.companyToDelete = id;
        $scope.companyIndex = index;
    }

    $scope.deleteCompany = function (id) {
        $scope.companyDeleted = 'delete';

        $scope.load = true;
        companiesRepo.deleteCompany(function () {
        }, id).success(function () {
            alertService.add('success', 'Mensaje', 'La Empresa se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.companyToDelete = 0;
            $scope.companyIndex = -1;
            $scope.setCompanyData();
            $scope.load = false;

        }).error(function () {
            alertService.add('danger', 'Error', 'No se ha podido eliminar el registro.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });

    }

    $scope.company_editRedirect = function (id) {
        window.location = "#/EditCompany/" + id;
    }

    $scope.setCompanyData();
}])
.controller('newCompaniesReportCtrl', ['$scope', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', 'companiesRepo', '$routeParams', '$window',
    function ($scope, $rootScope, $location, $filter, filterFilter, alertService, companiesRepo, $routeParams, $window) {
        $scope.companyList = [];
        $scope.tituloPrueba = false;
        debugger;
        $scope.contactsByCompanyList = [];
        
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
            $scope.filtered = filterFilter($scope.companyList, term);
            $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
        });

        $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
        $scope.entryLimit = $scope.itemsPerPageList[0];

        //$scope.setCompanyData = function () {
        //    debugger
        //    var starDate = $scope.filterStartDate;
        //    var endDate = $scope.filterEndDate;

        //    companiesRepo.getNewCompaniesDataReport(starDate, endDate).success(function (data) {
        //        $scope.companyList = data;
        //        $scope.totalServerItems = data.totalItems;
        //        $scope.items = data.items;
        //        $scope.load = false;

        //        if ($rootScope.alerts)
        //            $scope.alertsTags = $rootScope.alerts;
        //        $scope.currentPage = 1; //current page
        //        $scope.maxSize = 5; //pagination max size
        //        //max rows for data table

        //        $scope.noOfPages = ($scope.companyList) ? Math.ceil($scope.companyList.length / $scope.entryLimit) : 1;

        //        $scope.itemsInPage = ($scope.companyList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.companyList.length) ?
        //                $scope.companyList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;

        //    })
        //    .error(function (data) {
        //        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        //        $scope.load = false;
        //    });

        //};

        $scope.generateCompaniesReport = function () {
            debugger;
            $scope.load = true;
            
            var startDate = $scope.filterStartDate;
            var endDate = $scope.filterEndDate;
            var id = {
                "Companies": $scope.filtered,
                "DateFrom": startDate,
                "DateTo": endDate
            };

            companiesRepo.getCompaniesDataReport(id).success(function (data) {
                $scope.companyList = data;
                $scope.totalServerItems = data.totalItems;
                $scope.items = data.items;
                $scope.load = false;

                if ($rootScope.alerts)
                    $scope.alertsTags = $rootScope.alerts;
                $scope.currentPage = 1; //current page
                $scope.maxSize = 5; //pagination max size
                //max rows for data table

                $scope.noOfPages = ($scope.companyList) ? Math.ceil($scope.companyList.length / $scope.entryLimit) : 1;

                $scope.itemsInPage = ($scope.companyList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.companyList.length) ?
                        $scope.companyList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;

            })
            .error(function (data) {
                $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
                $scope.load = false;
            });

            $scope.load = false;
        };

        $scope.exportCompaniesReport = function () {
            var filters = { "Companies": $scope.filtered, "DateFrom": "", "DateTo": "" };
            if ($scope.dateFrom)
                filters.DateFrom = getDateFromFormat($scope.dateFrom, "dd/MM/yyyy");
            if ($scope.dateTo)
                filters.DateTo = getDateFromFormat($scope.dateTo, "dd/MM/yyyy");

            filters.Companies = angular.copy($scope.companyList);           
            if (filters.Companies.length > 0) {
                companiesRepo.exportCompaniesReport(filters)
                 .success(function (data) {
                     $window.open("Companies/Download/" + 'CompaniesReport', '_blank');
                 }).error(function (data, status, headers, config) {
                     alertService.add('danger', 'Error', 'No se ha podido generar el reporte.');
                     $scope.alertsTags = $rootScope.alerts;
                 });
            } else {
                alertService.add('danger', 'Error', 'No hay datos entre el rango de fecha seleccionado.');
                $scope.alertsTags = $rootScope.alerts;
            }
        };

    }
]);