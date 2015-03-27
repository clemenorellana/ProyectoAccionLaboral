'use strict';

angular.module("vacantsByCompaniesController", ['ngRoute', 'vacantByCompanyRepository', 'alertRepository'])
.config(['$routeProvider', function ($routeProvider) {
     
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
        }).
        when('/Vacants/Detail/:id', {
            templateUrl: function (params) {
                return '/VacantsByCompany/Details/' + params.id;
            },
            controller: 'vacantsByCompaniesCtrl'
        }).
        when('/VacantList', {
            templateUrl: '/VacantsByCompany/VacantList',
            controller: 'vacantsCtrl'
        }).
        when('/VacantReport', {
            templateUrl: '/VacantsByCompany/VacantReport',
            controller: 'vacantsReportCtrl'
        });
    
}]
)
.controller('vacantsByCompaniesCtrl', ['$scope', 'vacantByCompanyRepo', '$routeParams', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', 'employeesRepo', function ($scope, vacantByCompanyRepo, $routeParams, $rootScope, $location, $filter, filterFilter, alertService, employeesRepo) {
    $scope.employeesList = [];
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


    $scope.setEmployeeData = function () {
        employeesRepo.getRecruitmentEmployees().success(function (data) {
            $scope.employeesList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;
            debugger
            if ($scope.employeesList == null)
                return;

            var employees = [];
            for (var i = 0; i < $scope.employeesList.length; i++) {

                var vacantCovered = {
                    VacantByCompanyId: 0,
                    Employee: $scope.employeesList[i],
                    EmployeeId: $scope.employeesList[i].EmployeeId,
                    NumberOfProfiles: 0,
                }
                employees.push(vacantCovered);
            }
            $scope.employeesList = employees;


            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            //$scope.currentPage = 1; //current page
            //$scope.maxSize = 5; //pagination max size

            //$scope.noOfPages = ($scope.employeesList) ? Math.ceil($scope.employeesList.length / $scope.entryLimit) : 1;

            //$scope.itemsInPage = ($scope.employeesList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.employeesList.length) ?
            //                            $scope.employeesList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };

    $scope.setEmployeeVacantCoveredData = function (id) {
        vacantByCompanyRepo.getVacantsCovered(id).success(function (data) {
            $scope.employeesList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($scope.employeesList == null)
                return;

            var employees = [];
            for (var i = 0; i < $scope.employeesList.length; i++) {

                var vacantCovered = {
                    VacantByCompanyId: 0,
                    Employee: $scope.employeesList[i].Employee,
                    EmployeeId: $scope.employeesList[i].EmployeeId,
                    NumberOfProfiles: $scope.employeesList[i].NumberOfProfiles,
                }
                employees.push(vacantCovered);
            }
            $scope.employeesList = employees;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };



    if (!$rootScope.alerts)
        $rootScope.alerts = [];

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
     
    if (vacantByCompanyId == null) {
        $scope.showVacantCoveredDate = false;
        actionVacant = "add";
        $scope.tittleVacantForm = "Agregar una Vacante";
        $scope.buttonNameVacantForm = "Agregar";
    }
    else {
        actionVacant = "edit";
        $scope.tittleVacantForm = "Editar una Vacante";
        $scope.buttonNameVacantForm = "Editar";
        $scope.showVacantCoveredDate = true;
         

        vacantByCompanyRepo.getVacant(vacantByCompanyId).success(function (data) {
            var vacantToEdit = data;
            $scope.vacant_VacantByCompanyId = vacantToEdit.VacantByCompanyId;
            $scope.vacant_VacantName = vacantToEdit.VacantName;
            $scope.vacant_Active = vacantToEdit.Active;
            $scope.vacant_ChargeDescription = vacantToEdit.ChargeDescription;
            $scope.vacant_company = vacantToEdit.Company;
            $scope.vacant_CoveredDate = new Date(vacantToEdit.CoveredDate);
            $scope.vacant_StartAge = vacantToEdit.StartAge;
            $scope.vacant_EndAge = vacantToEdit.EndAge;
            $scope.vacant_Gender = vacantToEdit.Gender;
            $scope.vacant_Quantity = vacantToEdit.Quantity;
            $scope.vacant_RequestDate = new Date(vacantToEdit.RequestDate);
            $scope.vacant_Requirements = vacantToEdit.Requirements;
            $scope.vacant_academicLevel = vacantToEdit.AcademicLevel.AcademicLevelId;
            $scope.vacant_vacantLevel = vacantToEdit.VacantLevel;
            $scope.vacant_career = vacantToEdit.Career;
            $scope.vacant_city = vacantToEdit.City;
            $scope.vacant_interviewType = vacantToEdit.InterviewType;
            ;

            if ($scope.vacant_Active) {
                $scope.setEmployeeData();
            }
            else {
                $scope.setEmployeeVacantCoveredData(vacantByCompanyId);
            }

        }).error(function (data) {
            alertService.add('danger', 'Ha ocurrido un error al cargar los datos.' + data.ExceptionMessage);
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });
        
        
    }

    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];
    $scope.setVacantData = function () {
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

            $scope.vacant_itemsInPage = ($scope.vacantList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.vacantList.length) ?
                                        $scope.vacantList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;
        })
        .error(function (data) {
            alertService.add('danger', 'Ha ocurrido un error al cargar los datos.' + data.ExceptionMessage);
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };


    vacantByCompanyRepo.getInterviewTypeList().success(function (data) {

        $scope.vacant_interviewTypeList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        alertService.add('danger', 'Ha ocurrido un error al cargar los datos.' + data.ExceptionMessage);
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });

    vacantByCompanyRepo.getCompanyList().success(function (data) {
         
        $scope.vacant_companyList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        alertService.add('danger', 'Ha ocurrido un error al cargar los datos.' + data.ExceptionMessage);
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });
        
    vacantByCompanyRepo.getVacantLevelList().success(function (data) {
         
        $scope.vacant_vacantLevelList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        alertService.add('danger', 'Ha ocurrido un error al cargar los datos.' + data.ExceptionMessage);
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });

    vacantByCompanyRepo.getAcademicLevelList().success(function (data) {
         
        $scope.vacant_academicLevelList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        alertService.add('danger', 'Ha ocurrido un error al cargar los datos.' + data.ExceptionMessage);
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });
        
    vacantByCompanyRepo.getCareerList().success(function (data) {
         
        $scope.vacant_careerList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        alertService.add('danger', 'Ha ocurrido un error al cargar los datos.' + data.ExceptionMessage);
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });

    $scope.getCareersByAcademicLevel = function () {
         
        if ($scope.vacant_academicLevel)
            $scope.vacant_careerList = $filter('filter')($scope.vacant_academicLevelList, { AcademicLevelId: $scope.vacant_academicLevel })[0].Careers;
    }

    vacantByCompanyRepo.getCityList().success(function (data) {
         
        $scope.vacant_cityList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.load = false;
    })
    .error(function (data) {
        alertService.add('danger', 'Ha ocurrido un error al cargar los datos.' + data.ExceptionMessage);
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.load = false;
    });
  
    $scope.saveVacant = function () {

        if (!$scope.vacantForm.$valid)
            return;
        
        var newVacant = {
            VacantName : $scope.vacant_VacantName,
            Active : true,
            Quantity : $scope.vacant_Quantity,   
            StartAge : $scope.vacant_StartAge,   
            EndAge : $scope.vacant_EndAge,     
            Gender : $scope.vacant_Gender,    
            Requirements : $scope.vacant_Requirements,
            ChargeDescription : $scope.vacant_ChargeDescription,
            RequestDate : new Date($scope.vacant_RequestDate),
            CoveredDate: new Date($scope.vacant_RequestDate),
            CoveredTime: $scope.vacant_CoveredTime,
            CompanyId: $scope.vacant_company.CompanyId,
            AcademicLevelId: $scope.vacant_academicLevel,
            CareerId: $scope.vacant_career.CareerId,
            CityId: $scope.vacant_city.CityId,
            VacantLevelId: $scope.vacant_vacantLevel.VacantLevelId,
            InterviewTypeId: $scope.vacant_interviewType.InterviewTypeId
        }
        
        
        //TODO: sacar los datos de las vacantes agregadas
        if (actionVacant == "add") {
            vacantByCompanyRepo.insertVacant(function () { }, newVacant).success(function () {
                alertService.add('success', 'Mensaje', 'La Vacante se ha insertado correctamente.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.setVacantData();
                $location.path("/Vacants");
                $scope.load = false;
            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido insertar el registro.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });
        }
        else {
            newVacant.VacantByCompanyId = $scope.vacant_VacantByCompanyId;
            newVacant.CoveredDate = new Date($scope.vacant_CoveredDate);
            newVacant.Active = $scope.vacant_Active;


            vacantByCompanyRepo.updateVacant(function () { }, newVacant).success(function () {
                alertService.add('success', 'Mensaje', 'La Vacante se ha editado correctamente.');
                $scope.alertsTags = $rootScope.alerts;

                //insertar los datos de la vacante cubierta
                if (!$scope.vacant_Active) {
                    for (var i = 0; i < $scope.employeesList.length; i++) {
                        var vacantCovered = $scope.employeesList[i];
                        vacantCovered.EmployeeId = $scope.employeesList[i].EmployeeId;
                        vacantCovered.Employee = null;//$scope.employeesList[i];
                        vacantCovered.VacantByCompanyId = newVacant.VacantByCompanyId;

                        vacantByCompanyRepo.insertVacantCovered(function () { }, vacantCovered).success(function () {

                            //$scope.load = false;
                        }).error(function () {
                            //alertService.add('danger', 'Error', 'No se ha podido editar el registro.');
                            $scope.alertsTags = $rootScope.alerts;
                            $scope.load = false;
                            //return;
                        });
                    }
                }


                $scope.setVacantData();
                $location.path("/Vacants");
                $scope.load = false;
            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido editar el registro.');
                $scope.alertsTags = $rootScope.alerts;
                $scope.load = false;
            });
        }

        window.location = "#/Vacants";
    };

    $scope.vacant_cancelRedirect = function () {
        window.location = "#/Vacants";
    }

    
    $scope.vacant_detailCancelRedirect = function () {
        window.location = "#/VacantList";
    }


    $scope.vacant_addNewRedirect = function () {
        window.location = "#/AddVacant";
    }

    $scope.vacant_editRedirect = function (id) {
        window.location = "#/EditVacant/" + id;
    }

    $scope.setVacantToDelete = function (id, index) {
        $scope.vacantToDelete = id;
        $scope.vacantIndex = index;
    }

    $scope.deleteVacant = function (id) {
        $scope.load = true;
        vacantByCompanyRepo.deleteVacant(function () {
        }, id).success(function () {
            alertService.add('success', 'Mensaje', 'La Vacante se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.vacantToDelete = 0;
            $scope.vacantIndex = -1;
            $scope.setVacantData();
            $scope.load = false;
        }).error(function () {
            alertService.add('danger', 'Error', 'No se ha podido eliminar el registro.');
            $scope.alertsTags = $rootScope.alerts;
            $scope.load = false;
        });

    }

    $scope.vacant_viewRedirect = function (vacantId) {
        window.location = "#/Vacants/Detail/" + vacantId;
    }

    $scope.setVacantData();

    

    

}])
.controller('vacantsCtrl', ['$scope', 'vacantByCompanyRepo', '$routeParams', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', function ($scope, vacantByCompanyRepo, $routeParams, $rootScope, $location, $filter, filterFilter, alertService) {
    
    $scope.vacantList = [];
    $scope.load = true;
    
    if (!$rootScope.alerts)
        $rootScope.alerts = [];

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


    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];
    $scope.setVacantData = function () {
        vacantByCompanyRepo.getVacantList().success(function (data) {
            for (var i = 0; i < data.length; i++)
            {
                if(data[i].Active)
                    $scope.vacantList.push(data[i]);
            }
            
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size
            
            $scope.noOfPages = ($scope.vacantList) ? Math.ceil($scope.vacantList.length / $scope.entryLimit) : 1;

            $scope.vacant_itemsInPage = ($scope.vacantList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.vacantList.length) ?
                                        $scope.vacantList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;
        })
        .error(function (data) {
            alertService.add('danger', 'Ha ocurrido un error al cargar los datos.' + data.ExceptionMessage);
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };
    
    $scope.vacant_detailCancelRedirect = function () {
        window.location = "#/VacantList";
    }


    $scope.vacant_viewRedirect = function (vacantId) {
        window.location = "#/Vacants/Detail/" + vacantId;
    }

    $scope.setVacantData();
}])
.controller('vacantsReportCtrl', ['$scope', 'vacantByCompanyRepo', '$routeParams', '$rootScope', '$location', '$filter', 'filterFilter', 'alertService', 'employeesRepo', '$window', function ($scope, vacantByCompanyRepo, $routeParams, $rootScope, $location, $filter, filterFilter, alertService, employeesRepo, $window) {
    debugger
    $scope.employeeList = [];

    
    
    if (!$rootScope.alerts)
        $rootScope.alerts = [];

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


    $scope.itemsPerPageList = [5, 10, 20, 30, 40, 50];
    $scope.entryLimit = $scope.itemsPerPageList[0];

    $scope.setEmployeeListData = function (id) {
        debugger
        vacantByCompanyRepo.getVacantsCovered(id).success(function (data) {
            debugger
            $scope.employeesList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($scope.employeesList == null)
                return;

            var employees = [];
            for (var i = 0; i < $scope.employeesList.length; i++) {

                var vacantCovered = {
                    VacantByCompanyId: 0,
                    Employee: $scope.employeesList[i].Employee,
                    EmployeeId: $scope.employeesList[i].EmployeeId,
                    NumberOfProfiles: $scope.employeesList[i].NumberOfProfiles,
                }
                employees.push(vacantCovered);
            }
            $scope.employeesList = employees;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
        })
        .error(function (data) {
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };

    $scope.generateVacantReport = function () {
        $scope.load = true;

        var vacant = {
            "DateFrom": $scope.filterStartDate,
            "DateTo": $scope.filterEndDate
        };


        vacantByCompanyRepo.getVacantsDataReport(vacant).success(function (data) {
            $scope.vacantList = data;
            $scope.totalServerItems = data.totalItems;
            $scope.items = data.items;
            $scope.load = false;

            if ($rootScope.alerts)
                $scope.alertsTags = $rootScope.alerts;
            $scope.currentPage = 1; //current page
            $scope.maxSize = 5; //pagination max size

            $scope.noOfPages = ($scope.vacantList) ? Math.ceil($scope.vacantList.length / $scope.entryLimit) : 1;

            $scope.vacant_itemsInPage = ($scope.vacantList.length) ? ((($scope.currentPage * $scope.entryLimit) > $scope.vacantList.length) ?
                                        $scope.vacantList.length - (($scope.currentPage - 1) * $scope.entryLimit) : $scope.entryLimit) : 0;
        })
        .error(function (data) {
            alertService.add('danger', 'Ha ocurrido un error al cargar los datos.' + data.ExceptionMessage);
            $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
            $scope.load = false;
        });

    };
    

    $scope.exportVacantReport = function () {
        if ($scope.reportName == "" || $scope.reportName == null) {
            alertService.add('danger', 'Error', 'Aisgnar un nombre al archivo a exportar.');
            $scope.alertsTags = $rootScope.alerts;
            return
        }

        var filters = {
            "Vacants": $scope.vacantList,
            "DateFrom": $scope.filterStartDate,
            "DateTo": $scope.filterEndDate,
            "ReportName": $scope.reportName
        };
        debugger
        //filters.Companies = angular.copy($scope.companyList);
        if (filters.Vacants.length > 0) {
            vacantByCompanyRepo.exportVacantReport(filters)
             .success(function (data) {
                 var fileName = data.ReportName;
                 $window.open("VacantsByCompany/Download/" + fileName, '_blank');
                 $scope.reportName = "";
             }).error(function (data, status, headers, config) {
                 alertService.add('danger', 'Error', 'No se ha podido generar el reporte.');
                 $scope.alertsTags = $rootScope.alerts;
             });
        } else {
            alertService.add('danger', 'Error', 'No hay datos entre el rango de fecha seleccionado.');
            $scope.alertsTags = $rootScope.alerts;
        }
    };
}]);