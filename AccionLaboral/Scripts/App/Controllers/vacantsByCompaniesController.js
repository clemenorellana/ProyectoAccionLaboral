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
    //#/EditVacant/
}]
)
.controller('vacantsByCompaniesCtrl', ['$scope', 'vacantByCompanyRepo', '$routeParams', function ($scope, vacantByCompanyRepo, $routeParams) {

    $scope.vacantList = [];
    $scope.vacant_companyList = [];
    $scope.vacant_vacantLevelList = [];
    $scope.vacant_academicLevelList = [];
    $scope.vacant_careerList = [];
    $scope.vacant_cityList = [];
    $scope.load = true;
    $scope.actionContact = 'Agregar'
    $scope.contactIndex;
    var actionVacant;

    //$scope.companyId = $routeParams.id;
    var vacantByCompanyId = $routeParams.id;
    debugger
    if (vacantByCompanyId == null) {
        actionVacant = "add";
        $scope.tittleVacantForm = "Agregar una Vacante";
        $scope.buttonNameVacantForm = "Agregar";
    }
    else {
        actionVacant = "edit";
        $scope.tittleVacantForm = "Editar una Vacante";
        $scope.buttonNameVacantForm = "Editar";
        //var id = $scope.companyId;

        vacantByCompanyRepo.getCompany(vacantByCompanyId).success(function (data) {
            var vacantToEdit = data;
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
        $scope.vacant_companyList = data;
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
        $scope.vacant_vacantLevelList = data;
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
        $scope.vacant_academicLevelList = data;
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
        $scope.vacant_careerList = data;
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
        $scope.vacant_cityList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });


    $scope.saveVacant = function () {
        debugger
        var newVacant = {
            //VacantByCompanyId : 
            VacantName : $scope.vacant_VacantName,
            Active : 'true',
            Quantity : $scope.vacant_Quantity,   
            StartAge : $scope.vacant_StartAge,   
            EndAge : $scope.vacant_EndAge,     
            Gender : $scope.vacant_Gender,    
            Requirements : $scope.vacant_Requirements,
            ChargeDescription : $scope.vacant_ChargeDescription,
            RequestDate : new Date($scope.vacant_RequestDate),
            //CoverdDate : $scope.
            //CompanyId: $scope.vacant_company.CompanyId,
            //AcademicLevelId: $scope.vacant_academicLevel.AcademicLevelId,
            //CareerId: $scope.vacant_career.CareerId,
            //CityId: $scope.vacant_city.CityId,
            //VacantLevelId: $scope.vacant_vacantLevel.VacantLevelId
        }

        
        //TODO: sacar los datos de las vacantes agregadas
        if (actionVacant == "add") {
            vacantByCompanyRepo.insertCompany(function () { }, newVacant);
        }
        else {
            vacantByCompanyRepo.updateCompany(function () { }, newVacant);
        }

        window.location = "#/Vacants";
    };

    $scope.vacant_cancelRedirect = function () {
        window.location = "#/Vacants";
    }

    $scope.vacant_addNewRedirect = function () {
        window.location = "#/AddVacant";
    }

    $scope.setVacantToDelete = function (id) {
        $scope.vacantToDelete = id;
    }

    $scope.deleteVacant = function (id) {
        vacantByCompanyRepo.deleteCompany(function () {
        }, id);
        $scope.vacantToDelete = 0;
        $scope.refreshVacants();
    }

    $scope.refreshVacants = function () {
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

    $scope.vacant_editRedirect = function (id) {
        window.location = "#/EditVacant/" + id;
    }


}]);