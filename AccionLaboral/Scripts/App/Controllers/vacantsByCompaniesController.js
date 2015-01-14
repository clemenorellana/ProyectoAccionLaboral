'use strict';

angular.module("vacantsByCompaniesController", ['ngRoute', 'vacantByCompanyRepository'])
.config(['$routeProvider', function ($routeProvider) {
    debugger
    $routeProvider.
        when('/Vacants', {
            templateUrl: '/VacantsByCompany/Index',
            controller: 'vacantsByCompaniesCtrl'
        }).
        when('/AddVacant', {
            templateUrl: '/VacantsByCompany/Create',
            controller: 'vacantsByCompaniesCtrl'
        });
}]
)
.controller('vacantsByCompaniesCtrl', ['$scope', 'vacantByCompanyRepo', '$routeParams', function ($scope, vacantByCompanyRepo, $routeParams) {

    $scope.vacantList = [];
    $scope.companyList = [];
    $scope.vacantLevelList = [];
    $scope.academicLevelList = [];
    $scope.careerList = [];
    $scope.cityList = [];
    $scope.load = true;
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

        vacantByCompanyRepo.getCompany(id).success(function (data) {
            var companyToEdit = data;
            $scope.company_CompanyId = companyToEdit.CompanyId;
            $scope.company_Name = companyToEdit.Name;
            $scope.company_Area = companyToEdit.Area;
            $scope.company_DateCreated = companyToEdit.DateCreated;
            $scope.vacantList = companyToEdit.ContactsByCompany;
            //$scope.company_VacantsByCompany = companyToEdit.VacantsByCompany;
        });
    }

    vacantByCompanyRepo.getVacantList().success(function (data) {
        debugger
        $scope.vacantList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });

    vacantByCompanyRepo.getCompanyList().success(function (data) {
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
        
    vacantByCompanyRepo.getVacantLevelList().success(function (data) {
        debugger
        $scope.vacantLevelList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });

    vacantByCompanyRepo.getAcademicLevelList().success(function (data) {
        debugger
        $scope.academicLevelList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });

    vacantByCompanyRepo.getCareerList().success(function (data) {
        debugger
        $scope.careerList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });

    vacantByCompanyRepo.getCityList().success(function (data) {
        debugger
        $scope.cityList = data;
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
            var contact = $scope.vacantList[index];
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
            $scope.vacantList.push(contact);
        }
        else {
            $scope.vacantList[$scope.contactIndex] = contact;
        }

        $scope.clearContactForm();

    }

    $scope.editContactByCompany = function (index) {
        var contact = $scope.vacantList[index];
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
        $scope.vacantList.splice(index, 1);
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

        if ($scope.vacantList != null) {
            for (var j = 0; j < $scope.vacantList.length; j++) {
                var contactByCompany = $scope.vacantList[j];
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
            vacantByCompanyRepo.insertCompany(function () { }, newCompany);
        }
        else {
            vacantByCompanyRepo.updateCompany(function () { }, newCompany);
        }

        window.location = "#/Companies";
    };

    $scope.company_cancelRedirect = function () {
        window.location = "#/Companies";
    }

    $scope.company_addNewRedirect = function () {
        window.location = "#/AddCompany";
    }

    $scope.setCompanyToDelete = function (id) {
        $scope.companyToDelete = id;
    }

    $scope.deleteCompany = function (id) {
        vacantByCompanyRepo.deleteCompany(function () {
        }, id);
        $scope.companyToDelete = 0;
        $scope.refreshCompanies();
    }

    $scope.refreshCompanies = function () {
        vacantByCompanyRepo.getCompanyList().success(function (data) {
            debugger
            $scope.vacantList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;
        })
    .error(function (data) {
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });
    }

    $scope.company_editRedirect = function (id) {
        window.location = "#/EditCompany/" + id;
    }


}]);