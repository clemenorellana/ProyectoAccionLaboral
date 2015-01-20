'use strict';

angular.module("companiesController", ['ngRoute', 'companiesRepository'])
.config(['$routeProvider', function ($routeProvider) {
    debugger
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
        });
}]
)
.controller('companiesCtrl', ['$scope', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', 'companiesRepo', '$routeParams',  function ($scope, $rootScope, $location, $filter, filterFilter, alertService, companiesRepo, $routeParams) {
    
    $scope.companyList = [];
    $scope.load = true;
    $scope.contactsByCompanyList = [];
    $scope.actionContact = 'Agregar'
    $scope.contactIndex;
    var actionCompany;


    $scope.companyId = $routeParams.id;
    debugger
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
            //$scope.company_VacantsByCompany = companyToEdit.VacantsByCompany;
        });
    }

    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];

    $scope.setData = function () {
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


    $scope.$watch('search', function (term) {
        // Create $scope.filtered and then calculat $scope.noOfPages, no racing!
        $scope.filtered = filterFilter($scope.companyList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });

    companiesRepo.getCompanyList().success(function (data) {
        debugger
        $scope.companyList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });


    

    $scope.setActionContact = function (index, action) {
        debugger
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
        debugger
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

    $scope.refreshCompanies = function () {
        $scope.load = true;
        companiesRepo.getCompanyList().success(function (data) {
            debugger
            $scope.companyList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });
    }

    $scope.saveCompany = function () {
        debugger
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
        //TODO: sacar los datos de las vacantes agregadas
        if (actionCompany == "add") {
            companiesRepo.insertCompany(function () { }, newCompany);
        }
        else {
            companiesRepo.updateCompany(function () { }, newCompany);
        }
        $scope.setData();
        window.location = "#/Companies";
    };
    
    $scope.company_cancelRedirect = function () {
        window.location = "#/Companies";
    }

    $scope.company_addNewRedirect = function () {
        window.location = "#/AddCompany";
    }

    $scope.setCompanyToDelete = function (id,index) {
        $scope.companyToDelete = id;
        $scope.companyIndex = index;
    }

    $scope.deleteCompany = function (id) {
        $scope.companyDeleted = 'delete';
        debugger
        $scope.load = true;
        companiesRepo.deleteCompany(function () {
        }, id).success(function () {
                $scope.companyToDelete = 0;
                $scope.companyIndex = -1;
                $scope.refreshCompanies();
                $scope.load = false;
            }).error(function (error) {
            
            });
    }

    $scope.company_editRedirect = function (id) {
        window.location = "#/EditCompany/" + id;
    }

    $scope.setData();
}]);