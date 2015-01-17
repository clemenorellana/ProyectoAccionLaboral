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
        }).
        when('/EditVacant/:id', {
            templateUrl: function (params) {
                return '/VacantsByCompany/Edit/' + params.id;
            },
            controller: 'vacantsByCompaniesCtrl'
        });
}]
)
.controller('vacantsByCompaniesCtrl', ['$scope', 'vacantByCompanyRepo', '$routeParams', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', function ($scope, vacantByCompanyRepo, $routeParams, $rootScope, $location, $filter, filterFilter, alertService) {

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


    $scope.$watch('$routeChangeSuccess', function () {
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
    });

    //Sorting
    $scope.sort = "Company.Name";
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
        $scope.filtered = filterFilter($scope.vacantList, term);
        $scope.noOfPages = ($scope.filtered) ? Math.ceil($scope.filtered.length / $scope.entryLimit) : 1;
    });


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
        debugger;
        vacantByCompanyRepo.getVacant(vacantByCompanyId).success(function (data) {
            var vacantToEdit = data;
            $scope.vacant_company = vacantToEdit.VacantByCompanyId;
            $scope.vacant_VacantName = vacantToEdit.VacantName;
            $scope.vacant_company = vacantToEdit.Active;
            $scope.vacant_ChargeDescription = vacantToEdit.ChargeDescription;
            $scope.vacant_company = vacantToEdit.Company;
            //$scope.vacant_company = vacantToEdit.CoverdDate;
            $scope.vacant_StartAge = vacantToEdit.StartAge;
            $scope.vacant_EndAge = vacantToEdit.EndAge;
            $scope.vacant_Gender = vacantToEdit.Gender;
            $scope.vacant_Quantity = vacantToEdit.Quantity;
            $scope.vacant_RequestDate = new Date(vacantToEdit.RequestDate);
            $scope.vacant_Requirements = vacantToEdit.Requirements;
            $scope.vacant_academicLevel = vacantToEdit.AcademicLevel;
            $scope.vacant_vacantLevel = vacantToEdit.VacantLevel;
            $scope.vacant_career = vacantToEdit.Career;
            $scope.vacant_city = vacantToEdit.City;
            debugger;
        });
    }

    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];
    $scope.setData = function () {
        vacantByCompanyRepo.getVacantList().success(function (data) {
            $scope.vacantList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size
            
            $scope.noOfPages = ($scope.vacantList) ? Math.ceil($scope.vacantList.length / $scope.entryLimit) : 1;

            $scope.itemsInPage = ($scope.vacantList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.vacantList.length) ?
                    $scope.vacantList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;
            $scope.itemsInPage = 10;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };

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

    $scope.refreshVacants = function () {
        $scope.load = true;
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
    }

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
            vacantByCompanyRepo.insertVacant(function () { }, newVacant);
        }
        else {
            vacantByCompanyRepo.updateVacant(function () { }, newVacant);
        }

        window.location = "#/Vacants";
    };

    $scope.vacant_cancelRedirect = function () {
        window.location = "#/Vacants";
    }

    $scope.vacant_addNewRedirect = function () {
        window.location = "#/AddVacant";
    }

    $scope.setVacantToDelete = function (id,index) {
        $scope.vacantToDelete = id;
        $scope.vacantIndex = index;
    }

    $scope.deleteVacant = function (id) {
        vacantByCompanyRepo.deleteCompany(function () {
        }, id);
        $scope.vacantToDelete = 0;
        $scope.refreshVacants();
    }

    

    $scope.vacant_editRedirect = function (id) {
        window.location = "#/EditVacant/" + id;
    }

    $scope.deleteVacant = function (id) {
        vacantByCompanyRepo.deleteVacant(function () {
        }, id);
        $scope.companyList.splice($scope.vacantIndex, 1);
        $scope.vacantToDelete = 0;
        $scope.vacantIndex = -1;
        $scope.refreshVacants();
        window.location = "#/Vacants";
    }

}]);