'use strict';

angular.module("clientsController", ['ngRoute', 'clientsRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/RegisterClient', {
            templateUrl: '/Clients/Create',
            controller: 'CustomerController'
        }).
        when('/AllClients', {
            templateUrl: '/Clients/Index',
            controller: 'CustomerController'
        }).when("/editClient/:id", {
            title: 'Editar usuario',
            templateUrl: "/Clients/Edit",
            controller: "editCustomerController"
        });
}])
.controller('CustomerController', ['$scope', '$location', 'customerRepository', function($scope, $location, customerRepository) {

        $scope.handleFileSelectAdd = function(evt) {
            var f = evt.target.files[0];
            var reader = new FileReader();
            reader.onload = (function(theFile) {
                return function(e) {
                    var filePayload = e.target.result;
                    $scope.episodeImgData = e.target.result;
                    document.getElementById('imagen').src = $scope.episodeImgData;
                };
            })(f);
            reader.readAsDataURL(f);
        };

        $scope.academicEducations = [], $scope.knownLanguages = [], $scope.knownPrograms = [], $scope.personalReferences = [], $scope.workReferences = [], $scope.workExperiences = [], $scope.Trannings = [];

        var imageElement = document.getElementById('exampleInputFile');
        if (imageElement)
            imageElement.addEventListener('change', $scope.handleFileSelectAdd, false);
        $scope.index = -1;
        $scope.action = '';
        $scope.load = true;
        $scope.selectedClient = -1;
        
        $scope.setClientToDelete = function(id) {
            $scope.selectedClient = id;
        }
        $scope.addAcademicEducation = function () {
            if ($scope.action == 'edit') {
                $scope.academicEducations[$scope.index].TrainingName = $scope.TrainingName,
                    $scope.academicEducations[$scope.index].InstitutionName = $scope.InstitutionName,
                    $scope.academicEducations[$scope.index].AcademicLevel = $scope.AcademicLevel,
                    $scope.academicEducations[$scope.index].CareerId = $scope.CareerId,
                    $scope.academicEducations[$scope.index].EducationTypeId = $scope.EducationTypeId,
                    $scope.academicEducations[$scope.index].City = $scope.City,
                    $scope.academicEducations[$scope.index].Year = $scope.Year,
                    $scope.academicEducations[$scope.index].Country = $scope.Country;
            } else {
                var education = {
                    TrainingName: $scope.TrainingName,
                    InstitutionName: $scope.InstitutionName,
                    AcademicLevel: $scope.AcademicLevel,
                    CareerId: $scope.CareerId,
                    EducationTypeId: $scope.EducationTypeId,
                    City: $scope.City,
                    Year: $scope.Year,
                    Country: $scope.Country
                };
                $scope.academicEducations.push(education);
            }
            $scope.action = '';
            $scope.index = -1;
        };
        $scope.fillAcademicEducation = function (index) {
            $scope.textButton = 'Editar';
            $scope.TrainingName = $scope.academicEducations[index].TrainingName,
                $scope.InstitutionName = $scope.academicEducations[index].InstitutionName,
                $scope.AcademicLevel = $scope.academicEducations[index].AcademicLevel,
                $scope.CareerId = $scope.academicEducations[index].CareerId,
                $scope.EducationTypeId = $scope.academicEducations[index].EducationTypeId,
                $scope.City = $scope.academicEducations[index].City,
                $scope.Year = $scope.academicEducations[index].Year,
                $scope.Country = $scope.academicEducations[index].Country;
            $scope.action = 'edit';
            $scope.index = index;
        }
        $scope.clearAcademicEducation = function () {
            $scope.textButton = 'Agregar';
            $scope.TrainingName = "",
            $scope.InstitutionName = "",
            $scope.AcademicLevel = "",
            $scope.EducationTypeId = "";
            $scope.City = "",
            $scope.Year = "",
            $scope.Country = "";
        };
        $scope.removeAcademicEducation = function(index) {
            $scope.academicEducations.splice(index, 1);
        };

        /*Languages*/
        $scope.clearKnownLanguage = function () {
            $scope.textButton = 'Agregar';
            $scope.Percentage = "",
            $scope.Language = "",
            $scope.LanguageLevel = "";
            $scope.action = '';
            $scope.index = -1;
        };
        $scope.fillKnownLanguage = function (index) {
            $scope.textButton = 'Editar';
            $scope.Percentage = $scope.knownLanguages[index].Percentage,
            $scope.Language = $scope.knownLanguages[index].Language,
            $scope.LanguageLevel = $scope.knownLanguages[index].LanguageLevel;
            $scope.action = 'edit';
            $scope.index = index;
        };
        $scope.addKnownLanguage = function () {
            if ($scope.action=='edit') {
                $scope.knownLanguages[$scope.index].Percentage = $scope.Percentage,
                    $scope.knownLanguages[$scope.index].Language = $scope.Language,
                    $scope.knownLanguages[$scope.index].LanguageLevel = $scope.LanguageLevel;
            } else {
                var lang = {
                    Percentage: $scope.Percentage,
                    Language: $scope.Language,
                    LanguageLevel: $scope.LanguageLevel
                };
                $scope.knownLanguages.push(lang);
                $scope.clearKnownLanguage();
            }
        };
        $scope.removeKnownLanguage = function (index) {
            $scope.knownLanguages.splice(index, 1);
        };
        $scope.removeWorkExperience = function(index) {
            $scope.workExperiences.splice(index, 1);
        };
        $scope.removeWorkReference = function(index) {
            $scope.workReferences.splice(index, 1);
        };
        $scope.removePersonalReference = function(index) {
            $scope.personalReferences.splice(index, 1);
        };
        

        /*Programs*/
        $scope.clearProgram = function () {
            $scope.textButton = 'Agregar';
            $scope.NameProgram = "";
            $scope.action = '';
            $scope.index = -1;
        };
        $scope.fillKnownProgram = function (index) {
            $scope.textButton = 'Editar';
        $scope.NameProgram = $scope.knownPrograms[index].Name;
        $scope.action = 'edit';
        $scope.index = index;
    };
    $scope.addKnownProgram = function () {
        if ($scope.action == 'edit') {
            $scope.knownPrograms[$scope.index].Name = $scope.NameProgram;
        } else {
            var program = {
                Name: $scope.NameProgram
            };
            $scope.knownPrograms.push(program);
            $scope.clearProgram();
        }
    };
    $scope.removeKnownProgram = function (index) {
        $scope.knownPrograms.splice(index, 1);
    };

        /*WorkExperience*/
    $scope.clearWorkExperience = function () {
        $scope.textButton = 'Agregar';
            $scope.CompanyName = "";
            $scope.CompanyArea = "";
            $scope.Charge = "";
            $scope.StartDate = "";
            $scope.EndDate = "";
            $scope.Achievements = "";
            $scope.WorkCountry = "";
            $scope.WorkCity = "";
            $scope.action = '';
            $scope.index = -1;
        };
        $scope.fillWorkExperience = function (index) {
            $scope.textButton = 'Editar';
            $scope.CompanyName = $scope.workExperiences[index].CompanyName,
                $scope.CompanyArea = $scope.workExperiences[index].CompanyArea,
                $scope.Charge = $scope.workExperiences[index].Charge,
                $scope.StartDate = $scope.workExperiences[index].StartDate,
                $scope.EndDate = $scope.workExperiences[index].EndDate,
                $scope.Achievements = $scope.workExperiences[index].Archievements,
                $scope.WorkCity = $scope.workExperiences[index].WorkCity,
                $scope.WorkCountry = $scope.workExperiences[index].WorkCountry;
            $scope.action = 'edit';
            $scope.index = index;
        };
        $scope.addWorkExperience = function () {
            if ($scope.action == 'edit') {
                $scope.workExperiences[$scope.index].CompanyName = $scope.CompanyName,
                    $scope.workExperiences[$scope.index].CompanyArea = $scope.CompanyArea,
                    $scope.workExperiences[$scope.index].Charge = $scope.Charge,
                    $scope.workExperiences[$scope.index].StartDate = $scope.StartDate,
                    $scope.workExperiences[$scope.index].EndDate = $scope.EndDate,
                    $scope.workExperiences[$scope.index].Archievements = $scope.Achievements,
                    $scope.workExperiences[$scope.index].WorkCity = $scope.WorkCity,
                    $scope.workExperiences[$scope.index].WorkCountry = $scope.WorkCountry;
            } else {
                var experience = {
                    CompanyName: $scope.CompanyName,
                    CompanyArea: $scope.CompanyArea,
                    Charge: $scope.Charge,
                    StartDate: $scope.StartDate,
                    EndDate: $scope.EndDate,
                    Achievements: $scope.Achievements,
                    WorkCity: $scope.WorkCity,
                    WorkCountry: $scope.WorkCountry
                };
                $scope.workExperiences.push(experience);
                $scope.clearWorkExperience();
            }
        };
    $scope.removeWorkExperience = function (index) {
        $scope.workExperiences.splice(index, 1);
    };
        /*WorkReference*/
        $scope.clearWorkReference = function() {
                $scope.textButton = "Agregar";
                $scope.FirstNameWRef = "";
                $scope.LastNameWRef = "";
                $scope.ChargeWRef = "";
                $scope.CellphoneWRef = "";
                $scope.EmailWRef = "";
                $scope.CompanyNameWRef = "";
                $scope.RelationshipWRef = "";
                $scope.CityWRef = "";
                $scope.CountryWRef = "";
                $scope.action = '';
                $scope.index = -1;
            };
        $scope.fillWorkReference = function (index) {
            $scope.textButton = 'Editar';
            $scope.FirstNameWRef = $scope.workReferences[index].FirstName,
                $scope.LastNameWRef = $scope.workReferences[index].LastName,
                $scope.ChargeWRef = $scope.workReferences[index].Charge,
                $scope.CellphoneWRef = $scope.workReferences[index].Cellphone,
                $scope.EmailWRef = $scope.workReferences[index].Email,
                $scope.CompanyNameWRef = $scope.workReferences[index].CompanyName,
                $scope.RelationshipWRef = $scope.workReferences[index].Relationship,
                $scope.CityWRef = $scope.workReferences[index].City,
                $scope.CountryWRef = $scope.workReferences[index].Country;
            $scope.action = 'edit';
            $scope.index = index;
        };
        $scope.addWorkReference = function () {
            if ($scope.action == 'edit') {
                $scope.workReferences[$scope.index].FirstName = $scope.FirstNameWRef,
                    $scope.workReferences[$scope.index].LastName = $scope.LastNameWRef,
                    $scope.workReferences[$scope.index].Charge = $scope.ChargeWRef,
                    $scope.workReferences[$scope.index].Cellphone = $scope.CellphoneWRef,
                    $scope.workReferences[$scope.index].Email = $scope.EmailWRef,
                    $scope.workReferences[$scope.index].CompanyName = $scope.CompanyNameWRef,
                    $scope.workReferences[$scope.index].Relationship = $scope.RelationshipWRef,
                    $scope.workReferences[$scope.index].City = $scope.CityWRef,
                    $scope.workReferences[$scope.index].Country = $scope.CountryWRef;
            } else {
                var wReference = {
                    FirstName: $scope.FirstNameWRef,
                    LastName: $scope.LastNameWRef,
                    Charge: $scope.ChargeWRef,
                    Cellphone: $scope.CellphoneWRef,
                    Email: $scope.EmailWRef,
                    CompanyName: $scope.CompanyNameWRef,
                    Relationship: $scope.RelationshipWRef,
                    City: $scope.CityWRef,
                    Country: $scope.CountryWRef
                };
                $scope.workReferences.push(wReference);
                $scope.clearWorkReference();
            }
        };
    $scope.removeWorkReference = function (index) {
        $scope.workReferences.splice(index, 1);
    };

        /*PersonalReference*/
    $scope.clearPersonalReference = function () {
        $scope.textButton = 'Agregar';
                    $scope.FirstNamePRef = "";
                    $scope.LastNamePRef = "";
                    $scope.ChargePRef = "";
                    $scope.CellphonePRef = "";
                    $scope.EmailPRef = "";
                    $scope.CompanyNamePRef = "";
                    $scope.RelationshipPRef = "";
                    $scope.CityPRef = "";
                    $scope.CountryPRef = "";
                    $scope.action = '';
                    $scope.index = -1;
                };
    $scope.fillPersonalReference = function (index) {
        $scope.textButton = 'Editar';
            $scope.FirstNamePRef = $scope.personalReferences[index].FirstName,
                $scope.LastNamePRef = $scope.personalReferences[index].LastName,
                $scope.ChargePRef = $scope.personalReferences[index].Charge,
                $scope.CellphonePRef = $scope.personalReferences[index].Cellphone,
                $scope.EmailPRef = $scope.personalReferences[index].Email,
                $scope.CompanyNamePRef = $scope.personalReferences[index].CompanyName,
                $scope.RelationshipPRef = $scope.personalReferences[index].Relationship,
                $scope.CityPRef = $scope.personalReferences[index].City;
            $scope.action = 'edit';
            $scope.index = index;
        };
        $scope.addPersonalReference = function () {
            if ($scope.action == 'edit') {
                $scope.personalReferences[$scope.index].FirstName = $scope.FirstNamePRef,
                    $scope.personalReferences[$scope.index].LastName = $scope.LastNamePRef,
                    $scope.personalReferences[$scope.index].Charge = $scope.ChargePRef,
                    $scope.personalReferences[$scope.index].Cellphone = $scope.CellphonePRef,
                    $scope.personalReferences[$scope.index].Email = $scope.EmailPRef,
                    $scope.personalReferences[$scope.index].CompanyName = $scope.CompanyNamePRef,
                    $scope.personalReferences[$scope.index].Relationship = $scope.RelationshipPRef,
                    $scope.personalReferences[$scope.index].City = $scope.CityPRef,
                    $scope.personalReferences[$scope.index].Country = $scope.CountryPRef;
            } else {
                var pReference = {
                    FirstName: $scope.FirstNamePRef,
                    LastName: $scope.LastNamePRef,
                    Charge: $scope.ChargePRef,
                    Cellphone: $scope.CellphonePRef,
                    Email: $scope.EmailPRef,
                    CompanyName: $scope.CompanyNamePRef,
                    Relationship: $scope.RelationshipPRef,
                    City: $scope.CityPRef,
                    Country: $scope.CountryPRef
                };
                $scope.personalReferences.push(pReference);
                $scope.clearPersonalReference();
            }
        };
    $scope.removePersonalReference = function (index) {
        $scope.personalReferences.splice(index, 1);
    };

        /*Tranning*/
    $scope.clearTranning = function () {
        $scope.textButton = 'Agregar';
                    $scope._TrainingName = "",
                        $scope._InstitutionName = "",
                        $scope._AcademicLevel = "",
                        $scope._EducationTypeId = "",
                        $scope._City = "",
                        $scope._Year = "",
                        $scope._Country = "";
                    $scope.action = '';
                    $scope.index = -1;
                };
        $scope.fillTranning = function(index) {
                    $scope.textButton = "Editar";
                    $scope._TrainingName = $scope.Trannings[index].TrainingName,
                        $scope._InstitutionName = $scope.Trannings[index].InstitutionName,
                        $scope._CareerId = $scope.Trannings[index].CareerId,
                        $scope._EducationTypeId = $scope.Trannings[index].EducationTypeId,
                        $scope._City = $scope.Trannings[index].City,
                        $scope._Year = $scope.Trannings[index].Year,
                        $scope._Country = $scope.Trannings[index].Country;
                    $scope.action = 'edit';
                    $scope.index = index;
                };
        $scope.addTranning = function() {
                    if ($scope.action == 'edit') {
                        $scope.Trannings[$scope.index].TrainingName = $scope._TrainingName,
                            $scope.Trannings[$scope.index].InstitutionName = $scope._InstitutionName,
                            $scope.Trannings[$scope.index].CareerId = $scope._CareerId,
                            $scope.Trannings[$scope.index].EducationTypeId = $scope._EducationTypeId,
                            $scope.Trannings[$scope.index].City = $scope._City,
                            $scope.Trannings[$scope.index].Year = $scope._Year,
                            $scope.Trannings[$scope.index].Country = $scope._Country;
                    } else {
                        var tranning = {
                            TrainingName: $scope._TrainingName,
                            InstitutionName: $scope._InstitutionName,
                            CareerId: $scope._CareerId,
                            EducationTypeId: $scope._EducationTypeId,
                            City: $scope._City,
                            Year: $scope._Year,
                            Country: $scope._Country
                        };
                        $scope.Trannings.push(tranning);
                        $scope.clearTranning();
                    }
                };
        $scope.removeTranning = function(index) {
                    $scope.Trannings.splice(index, 1);
                };

                customerRepository.getCities().success(function(data) {
                    $scope.Cities = data;
                });
                customerRepository.getCountries().success(function(data) {
                    $scope.Countries = data;
                });
                customerRepository.getAcademicLevels().success(function(data) {

                    $scope.AcademicLevels = data;
                });
                customerRepository.getEducationTypes().success(function(data) {

                    $scope.EducationTypes = data;
                });
                customerRepository.getLanguages().success(function(data) {

                    $scope.Languages = data;
                });
                customerRepository.getLanguageLevels().success(function(data) {

                    $scope.LanguageLevels = data;
                });
                $scope.assignCareers = function() {
                    if ($scope.AcademicLevel)
                        $scope.Careers = $scope.AcademicLevel.Careers;
                }

                customerRepository.getCustomers().success(function(data) {
                        $scope.customerData = data;
                        $scope.totalServerItems = data.totalItems;
                        $scope.items = data.items;
                        $scope.load = false;
                    })
                    .error(function(data) {
                        $scope.error = "An Error has occured while loading posts! " + data.ExceptionMessage;
                        $scope.load = false;
                    });

                $scope.setScope = function(obj, action) {

                    $scope.action = action;
                    $scope.New = obj;
                    $location.url('editClient/' + obj.ClientId);
                };

                // filter
                $scope.filterOptions = {
                    filterText: "",
                    useExternalFilter: true
                };

                // paging
                $scope.totalServerItems = 0;
                $scope.pagingOptions = {
                    pageSizes: [25, 50, 100],
                    pageSize: 25,
                    currentPage: 1
                };

                // sort
                $scope.sortOptions = {
                    fields: ["name"],
                    directions: ["ASC"]
                };

                $scope.gridOptions = {
                    data: 'customerData',
                    columnDefs: [
                        { field: 'clientId', displayName: 'ID', width: '5%' },
                        { field: 'FirstName', displayName: 'Nombre', width: '15%' },
                        { field: 'LastName', displayName: 'Apellido', width: '15%' },
                        { field: 'Age', displayName: 'Edad', width: '5%' },
                        { field: 'Email', displayName: 'Email', width: '15%' },
                        { field: 'CompleteAddress', displayName: 'CompleteAddress', width: '15%' },
                        { field: 'Cellphone', displayName: 'Celular', width: '15%' },
                        { displayName: 'Options', cellTemplate: '<input type="button" ng-click="setScope(row.entity,\'edit\')" name="edit"  value="Edit">&nbsp;<input type="button" ng-click="DeleteUser(row.entity.id)"  name="delete"  value="Delete">', width: '25%' }
                    ],
                    enablePaging: true,
                    enablePinning: true,
                    pagingOptions: $scope.pagingOptions,
                    filterOptions: $scope.filterOptions,
                    keepLastSelected: true,
                    multiSelect: false,
                    showColumnMenu: true,
                    showFilter: true,
                    showGroupPanel: true,
                    showFooter: true,
                    sortInfo: $scope.sortOptions,
                    totalServerItems: "totalServerItems",
                    useExternalSorting: true,
                    i18n: "en"
                };

                $scope.refresh = function() {
                    setTimeout(function() {
                        var sb = [];
                        for (var i = 0; i < $scope.sortOptions.fields.length; i++) {
                            sb.push($scope.sortOptions.directions[i] === "DESC" ? "-" : "+");
                            sb.push($scope.sortOptions.fields[i]);
                        }

                        var p = {
                            name: $scope.filterOptions.filterText,
                            pageNumber: $scope.pagingOptions.currentPage,
                            pageSize: $scope.pagingOptions.pageSize,
                            sortInfo: sb.join("")
                        };
                    }, 100);
                };

                // watches
                $scope.$watch('pagingOptions', function(newVal, oldVal) {
                    if (newVal !== oldVal) {
                        $scope.refresh();
                    }
                }, true);

                $scope.$watch('filterOptions', function(newVal, oldVal) {
                    if (newVal !== oldVal) {
                        $scope.refresh();
                    }
                }, true);

                $scope.$watch('sortOptions', function(newVal, oldVal) {
                    if (newVal !== oldVal) {
                        $scope.refresh();
                    }
                }, true);

                $scope.refresh();


                $scope.update = function() {

                    if ($scope.episodeImgData)
                        $scope.New.Photo = $scope.episodeImgData.replace(/data:image\/jpeg;base64,/g, '');

                    $scope.New.AcademicEducations = [];
                    if ($scope.academicEducations.length > 0) {

                        for (var i = 0; i < $scope.academicEducations.length; i++) {
                            var academic = {
                                Year: $scope.academicEducations[i].Year,
                                CountryId: $scope.academicEducations[i].Country.CountryId,
                                CityId: $scope.academicEducations[i].City.CityId,
                                AcademicLevelId: $scope.academicEducations[i].AcademicLevel.AcademicLevelId,
                                TrainingName: $scope.academicEducations[i].TrainingName,
                                InstitutionName: $scope.academicEducations[i].InstitutionName,
                                CareerId: $scope.academicEducations[i].CareerId,
                                EducationTypeId: 1
                            };
                            $scope.New.AcademicEducations.push(academic);
                        }
                    }

                    if ($scope.knownLaguages.length > 0) {
                        $scope.New.Languages = [];
                        for (var j = 0; j < $scope.knownLaguages.length; j++) {
                            var language = {
                                LanguageId: $scope.knownLaguages[j].Language.LanguageId,
                                LanguageLevelId: $scope.knownLaguages[j].LanguageLevel.LanguageLevelId,
                                Percentage: $scope.knownLaguages[j].Percentage
                            };
                            $scope.New.Languages.push(language);
                        }
                    }
                    if ($scope.knownPrograms.length > 0) {
                        $scope.New.KnownPrograms = [];
                        for (var j = 0; j < $scope.knownPrograms.length; j++) {
                            var program = {
                                Name: $scope.knownPrograms[j].Name
                            };
                            $scope.New.KnownPrograms.push(program);
                        }
                    }

                    if ($scope.workExperiences.length > 0) {
                        $scope.New.workExperiences = [];
                        for (var k = 0; k < $scope.workExperiences.length; k++) {
                            var experience = {
                                CompanyName: $scope.workExperiences[k].CompanyName,
                                CompanyArea: $scope.workExperiences[k].CompanyArea,
                                Charge: $scope.workExperiences[k].Charge,
                                StartDate: $scope.workExperiences[k].StartDate,
                                EndDate: $scope.workExperiences[k].EndDate,
                                Achievements: $scope.workExperiences[k].Achievements,
                                CityId: $scope.workExperiences[k].WorkCity.CityId
                            };
                            $scope.New.workExperiences.push(experience);
                        }
                    }

                    if ($scope.workReferences.length > 0) {
                        $scope.New.workReferences = [];
                        for (var l = 0; l < $scope.workReferences.length; l++) {
                            var wExperience = {
                                FirstName: $scope.workReferences[l].FirstName,
                                LastName: $scope.workReferences[l].LastName,
                                Charge: $scope.workReferences[l].Charge,
                                Cellphone: $scope.workReferences[l].Cellphone,
                                Email: $scope.workReferences[l].Email,
                                CompanyName: $scope.workReferences[l].CompanyName,
                                Relationship: $scope.workReferences[l].Relationship,
                                ReferenceTypeId: 1,
                                CityId: $scope.workReferences[l].City.CityId
                            }

                            $scope.New.workReferences.push(wExperience);
                        }
                    }

                    if ($scope.personalReferences.length > 0) {
                        $scope.New.personalReferences = [];
                        for (var l = 0; l < $scope.personalReferences.length; l++) {
                            var pExperience = {
                                FirstName: $scope.personalReferences[l].FirstName,
                                LastName: $scope.personalReferences[l].LastName,
                                Charge: $scope.personalReferences[l].Charge,
                                Cellphone: $scope.personalReferences[l].Cellphone,
                                Email: $scope.personalReferences[l].Email,
                                CompanyName: $scope.personalReferences[l].CompanyName,
                                Relationship: $scope.personalReferences[l].Relationship,
                                ReferenceTypeId: 2,
                                CityId: $scope.personalReferences[l].City.CityId
                            }

                            $scope.New.workReferences.push(pExperience);
                        }
                    }

                    if ($scope.Trannings.length > 0) {
                        for (var i = 0; i < $scope.Trannings.length; i++) {
                            var tranning = {
                                Year: $scope.Trannings[i].Year,
                                CountryId: $scope.Trannings[i].Country.CountryId,
                                CityId: $scope.Trannings[i].City.CityId,
                                TrainingName: $scope.Trannings[i].TrainingName,
                                InstitutionName: $scope.Trannings[i].InstitutionName,
                                CareerId: $scope.Trannings[i].CareerId,
                                EducationTypeId: 2
                            };
                            $scope.New.AcademicEducations.push(tranning);
                        }
                    }

                    if ($scope.action == 'edit') {
                        customerRepository.UpdateCustomer(function() {
                        }, $scope.New);
                        $scope.action = '';
                    } else {
                        customerRepository.InsertCustomer(function() {

                        }, $scope.New);

                    }


                }

                $scope.DeleteCustomer = function(id) {
                    customerRepository.DeleteCustomer(function() {
                        alert('Customer deleted');
                        getCustomers();
                    }, id);

                };
            }]).
    directive('loading', function() {
        return {
            restrict: 'E',
            replace: true,
            template: '<div class="loading"><img src="../../Images/loading.gif" width="50" height="50" />CARGANDO...</div>',
            link: function(scope, element, attr) {
                scope.$watch('loading', function(val) {
                    if (val)
                        $(element).show();
                    else
                        $(element).hide();
                });
            }
        }
    }).controller("editCustomerController", [
        '$scope', '$routeParams', '$filter', 'customerRepository', function ($scope, $routeParams, $filter, customerRepository) {

            $scope.handleFileSelectAdd = function (evt) {
                var f = evt.target.files[0];
                var reader = new FileReader();
                reader.onload = (function (theFile) {
                    return function (e) {
                        var filePayload = e.target.result;
                        $scope.episodeImgData = e.target.result;
                        document.getElementById('imagen').src = $scope.episodeImgData;
                        $scope.New.Photo = $scope.episodeImgData.replace(/data:image\/jpeg;base64,/g, '')
                    };
                })(f);
                reader.readAsDataURL(f);
            };

            var imageElement = document.getElementById('exampleInputFile');
            if (imageElement)
                imageElement.addEventListener('change', $scope.handleFileSelectAdd, false);

            customerRepository.getCustomer($routeParams.id).success(function (data) {
            $scope.academicEducations = [],
            $scope.knownLaguages = [],
            $scope.knownPrograms = [],
            $scope.personalReferences = [],
            $scope.workReferences = [],
            $scope.workExperiences = [];
            $scope.Trannings =[];   
                if (data.Birthday)
                    data.Birthday = new Date(data.Birthday);
                $scope.academicEducations = [];
                var educations = data.AcademicEducations;
                $scope.knownLanguages = data.Languages;
                $scope.knownPrograms = data.KnownPrograms;
                $scope.workExperiences = data.WorkExperiences;
                $scope.Trannings = [];
                $scope.workReferences = [],
                    $scope.personalReferences = [];
                for (var j = 0; j < data.AcademicEducations.length; j++) {
                    if (data.AcademicEducations[j].EducationType.Name == 'ACADEMICA') {
                        $scope.academicEducations.push(data.AcademicEducations[j]);
                    } else {
                        $scope.Trannings.push(data.AcademicEducations[j]);
                    }
                }
                for (var i = 0; i < data.References.length; i++) {
                    if (data.References[i].ReferenceType.Name == 'L')
                        $scope.workReferences.push(data.References[i]);
                    else
                        $scope.personalReferences.push(data.References[i]);
                }

                $scope.New = data;
       
            });
            
            $scope.index = -1;
            $scope.action = '';
            $scope.load = true;

            $scope.addAcademicEducation = function () {
                var index = $scope.countryIndex;
                if ($scope.action == 'edit') {
                    $scope.academicEducations[$scope.index].TrainingName = $scope.TrainingName,
                        $scope.academicEducations[$scope.index].InstitutionName = $scope.InstitutionName,
                        $scope.academicEducations[$scope.index].AcademicLevel = $filter('filter')($scope.AcademicLevels, { AcademicLevelId: $scope.AcademicLevelId })[0],
                        $scope.academicEducations[$scope.index].AcademicLevelId = $scope.AcademicLevelId,
                        $scope.academicEducations[$scope.index].CareerId = $scope.CareerId,
                        $scope.academicEducations[$scope.index].EducationTypeId = $scope.EducationTypeId,
                        $scope.academicEducations[$scope.index].City = $filter('filter')($scope.Cities, { CityId: $scope.CityId })[0],
                        $scope.academicEducations[$scope.index].CityId = $scope.CityId,
                        $scope.academicEducations[$scope.index].Year = $scope.Year,
                        $scope.academicEducations[$scope.index].Country = $filter('filter')($scope.Countries, { CountryId: $scope.CountryId })[0],
                        $scope.academicEducations[$scope.index].CountryId = $scope.CountryId;
                } else {
                    var education = {
                        TrainingName: $scope.TrainingName,
                        InstitutionName: $scope.InstitutionName,
                        AcademicLevelId: $scope.AcademicLevelId,
                        AcademicLevel: $filter('filter')($scope.AcademicLevels, { AcademicLevelId: $scope.AcademicLevelId })[0],
                        CareerId: $scope.CareerId,
                        EducationTypeId: $scope.EducationTypeId,
                        CityId: $scope.CityId,
                        City: $filter('filter')($scope.Cities, { CityId: $scope.CityId })[0],
                        Year: $scope.Year,
                        CountryId: $scope.CountryId,
                        Country: $filter('filter')($scope.Countries, { CountryId: $scope.CountryId })[0]
                    };
                    $scope.academicEducations.push(education);
                }
                $scope.action = '';
                $scope.index = -1;
            };
            $scope.fillAcademicEducation = function (index) {
                $scope.textButton = 'Editar';
                $scope.TrainingName = $scope.academicEducations[index].TrainingName,
                    $scope.InstitutionName = $scope.academicEducations[index].InstitutionName,
                    $scope.AcademicLevelId = $scope.academicEducations[index].AcademicLevel.AcademicLevelId,
                    $scope.Careers = $scope.academicEducations[index].AcademicLevel.Careers,
                    $scope.CareerId = $scope.academicEducations[index].CareerId,
                    $scope.EducationTypeId = $scope.academicEducations[index].EducationTypeId,
                    $scope.CityId = $scope.academicEducations[index].CityId,
                    $scope.Year = $scope.academicEducations[index].Year,
                    $scope.CountryId = $scope.academicEducations[index].City.CountryId;
                $scope.action = 'edit';
                $scope.index = index;
            }
            $scope.clearAcademicEducation = function () {
                $scope.textButton = 'Agregar';
                $scope.TrainingName = "",
                $scope.InstitutionName = "",
                $scope.AcademicLevelId = "",
                $scope.EducationTypeId = "";
                $scope.CityId = "",
                $scope.Year = "",
                $scope.CountryId = "";
                $scope.action = "";
            };
            $scope.removeAcademicEducation = function (index) {
                $scope.academicEducations.splice(index, 1);
            };

            /*Languages*/
            $scope.clearKnownLanguage = function () {
                $scope.textButton = 'Agregar';
                $scope.Percentage = "",
                $scope.LanguageId = "",
                $scope.LanguageLevelId = "";
                $scope.action = '';
                $scope.index = -1;
            };
            $scope.fillKnownLanguage = function (index) {
                $scope.textButton = 'Editar';
                $scope.Percentage = $scope.knownLanguages[index].Percentage,
                $scope.LanguageId = $scope.knownLanguages[index].LanguageId,
                $scope.LanguageLevelId = $scope.knownLanguages[index].LanguageLevelId;
                $scope.action = 'edit';
                $scope.index = index;
            };
            $scope.addKnownLanguage = function () {
                if ($scope.action == 'edit') {
                    $scope.knownLanguages[$scope.index].Percentage = $scope.Percentage,
                        $scope.knownLanguages[$scope.index].Language = $filter('filter')($scope.Languages, { LanguageId: $scope.LanguageId })[0],
                        $scope.knownLanguages[$scope.index].LanguageId = $scope.LanguageId,
                        $scope.knownLanguages[$scope.index].LanguageLevel = $filter('filter')($scope.LanguageLevels, {LanguageLevelId: $scope.LanguageLevelId})[0],
                        $scope.knownLanguages[$scope.index].LanguageLevelId = $scope.LanguageLevelId;
                
                } else {
                    var lang = {
                        Percentage: $scope.Percentage,
                        Language: $filter('filter')($scope.Languages, { LanguageId: $scope.LanguageId })[0],
                        LanguageId: $scope.LanguageId,
                        LanguageLevel: $filter('filter')($scope.LanguageLevels, {LanguageLevelId: $scope.LanguageLevelId})[0],
                        LanguageLevelId: $scope.LanguageLevelId
                    };
                    $scope.knownLanguages.push(lang);
                    $scope.clearKnownLanguage();
                }
            };
            $scope.removeKnownLanguage = function (index) {
                $scope.knownLanguages.splice(index, 1);
            };

            /*Programs*/
            $scope.clearProgram = function () {
                $scope.textButton = 'Agregar';
                $scope.NameProgram = "";
                $scope.action = '';
                $scope.index = -1;
            };
            $scope.fillKnownProgram = function (index) {
                $scope.textButton = 'Editar';
                $scope.NameProgram = $scope.knownPrograms[index].Name;
                $scope.action = 'edit';
                $scope.index = index;
            };
            $scope.addKnownProgram = function () {
                if ($scope.action == 'edit') {
                    $scope.knownPrograms[$scope.index].Name = $scope.NameProgram;
                } else {
                    var program = {
                        Name: $scope.NameProgram
                    };
                    $scope.knownPrograms.push(program);
                    $scope.clearProgram();
                }
            };
            $scope.removeKnownProgram = function (index) {
                $scope.knownPrograms.splice(index, 1);
            };

            /*WorkExperience*/
            $scope.clearWorkExperience = function () {
                $scope.textButton = 'Agregar';
                $scope.CompanyName = "";
                $scope.CompanyArea = "";
                $scope.Charge = "";
                $scope.StartDate = "";
                $scope.EndDate = "";
                $scope.Achievements = "";
                $scope.WorkCountryId = "";
                $scope.WorkCityId = "";
                $scope.action = '';
                $scope.index = -1;
            };
            $scope.fillWorkExperience = function (index) {
                $scope.textButton = 'Editar';
                $scope.CompanyName = $scope.workExperiences[index].CompanyName,
                    $scope.CompanyArea = $scope.workExperiences[index].CompanyArea,
                    $scope.Charge = $scope.workExperiences[index].Charge,
                    $scope.StartDate = new Date($scope.workExperiences[index].StartDate),
                    $scope.EndDate = new Date($scope.workExperiences[index].EndDate),
                    $scope.Achievements = $scope.workExperiences[index].Archievements,
                    $scope.WorkCityId = $scope.workExperiences[index].CityId,
                    $scope.WorkCountryId = $scope.workExperiences[index].City.CountryId;
                $scope.action = 'edit';
                $scope.index = index;
            };
            $scope.addWorkExperience = function () {
                if ($scope.action == 'edit') {
                    $scope.workExperiences[$scope.index].CompanyName = $scope.CompanyName,
                        $scope.workExperiences[$scope.index].CompanyArea = $scope.CompanyArea,
                        $scope.workExperiences[$scope.index].Charge = $scope.Charge,
                        $scope.workExperiences[$scope.index].StartDate = $scope.StartDate,
                        $scope.workExperiences[$scope.index].EndDate = $scope.EndDate,
                        $scope.workExperiences[$scope.index].Archievements = $scope.Achievements,
                        $scope.workExperiences[$scope.index].City = $filter('filter')($scope.Cities, { CityId: $scope.WorkCityId })[0],
                        $scope.workExperiences[$scope.index].CityId = $scope.WorkCityId, 
                        $scope.workExperiences[$scope.index].Country = $filter('filter')($scope.Countries, { CountryId: $scope.WorkCountryId })[0],
                        $scope.workExperiences[$scope.index].CountryId = $scope.WorkCountryId;
                } else {
                    var experience = {
                        CompanyName: $scope.CompanyName,
                        CompanyArea: $scope.CompanyArea,
                        Charge: $scope.Charge,
                        StartDate: $scope.StartDate,
                        EndDate: $scope.EndDate,
                        Achievements: $scope.Achievements,
                        City: $filter('filter')($scope.Cities, { CityId: $scope.WorkCityId })[0],
                        CityId: $scope.WorkCityId,
                        Country: $filter('filter')($scope.Countries, { CountryId: $scope.WorkCountryId })[0],
                        CountryId: $scope.WorkCountryId
                    };
                    $scope.workExperiences.push(experience);
                    $scope.clearWorkExperience();
                }
            };
            $scope.removeWorkExperience = function (index) {
                $scope.workExperiences.splice(index, 1);
            };

            /*WorkReference*/
            $scope.clearWorkReference = function () {
                $scope.textButton = "Agregar";
                $scope.FirstNameWRef = "";
                $scope.LastNameWRef = "";
                $scope.ChargeWRef = "";
                $scope.CellphoneWRef = "";
                $scope.EmailWRef = "";
                $scope.CompanyNameWRef = "";
                $scope.RelationshipWRef = "";
                $scope.CityWRef = "";
                $scope.CountryWRef = "";
                $scope.action = '';
                $scope.index = -1;
            };
            $scope.fillWorkReference = function (index) {
                $scope.textButton = 'Editar';
                $scope.FirstNameWRef = $scope.workReferences[index].FirstName,
                    $scope.LastNameWRef = $scope.workReferences[index].LastName,
                    $scope.ChargeWRef = $scope.workReferences[index].Charge,
                    $scope.CellphoneWRef = $scope.workReferences[index].Cellphone,
                    $scope.EmailWRef = $scope.workReferences[index].Email,
                    $scope.CompanyNameWRef = $scope.workReferences[index].CompanyName,
                    $scope.RelationshipWRef = $scope.workReferences[index].Relationship,
                    $scope.CityIdWRef = $scope.workReferences[index].City.CityId,
                    $scope.CountryIdWRef = $scope.workReferences[index].City.Country.CountryId;
                $scope.action = 'edit';
                $scope.index = index;
            };
            $scope.addWorkReference = function () {
                if ($scope.action == 'edit') {
                    $scope.workReferences[$scope.index].FirstName = $scope.FirstNameWRef,
                        $scope.workReferences[$scope.index].LastName = $scope.LastNameWRef,
                        $scope.workReferences[$scope.index].Charge = $scope.ChargeWRef,
                        $scope.workReferences[$scope.index].Cellphone = $scope.CellphoneWRef,
                        $scope.workReferences[$scope.index].Email = $scope.EmailWRef,
                        $scope.workReferences[$scope.index].CompanyName = $scope.CompanyNameWRef,
                        $scope.workReferences[$scope.index].Relationship = $scope.RelationshipWRef,
                        $scope.workReferences[$scope.index].City = $filter('filter')($scope.Cities, { CityId: $scope.CityIdWRef })[0],
                        $scope.workReferences[$scope.index].CityId = $scope.CityIdWRef,
                        $scope.workReferences[$scope.index].Country = $filter('filter')($scope.Cities, { CountryId: $scope.CountryIdWRef })[0],
                        $scope.workReferences[$scope.index].CountryId = $scope.CountryIdWRef;
                } else {
                    var wReference = {
                        FirstName: $scope.FirstNameWRef,
                        LastName: $scope.LastNameWRef,
                        Charge: $scope.ChargeWRef,
                        Cellphone: $scope.CellphoneWRef,
                        Email: $scope.EmailWRef,
                        CompanyName: $scope.CompanyNameWRef,
                        Relationship: $scope.RelationshipWRef,
                        City: $filter('filter')($scope.Cities, { CityId: $scope.CityIdWRef })[0],
                        CityId: $scope.CityIdWRef,
                        Country: $filter('filter')($scope.Cities, { CountryId: $scope.CountryIdWRef })[0],
                        CountryId: $scope.CountryIdWRef
                    };
                    $scope.workReferences.push(wReference);
                    $scope.clearWorkReference();
                }
            };
            $scope.removeWorkReference = function (index) {
                $scope.workReferences.splice(index, 1);
            };

            /*PersonalReference*/
            $scope.clearPersonalReference = function () {
                $scope.textButton = 'Agregar';
                $scope.FirstNamePRef = "";
                $scope.LastNamePRef = "";
                $scope.ChargePRef = "";
                $scope.CellphonePRef = "";
                $scope.EmailPRef = "";
                $scope.CompanyNamePRef = "";
                $scope.RelationshipPRef = "";
                $scope.CityIdPRef = "";
                $scope.CountryIdPRef = "";
                $scope.action = '';
                $scope.index = -1;
            };
            $scope.fillPersonalReference = function (index) {
                $scope.textButton = 'Editar';
                $scope.FirstNamePRef = $scope.personalReferences[index].FirstName,
                    $scope.LastNamePRef = $scope.personalReferences[index].LastName,
                    $scope.ChargePRef = $scope.personalReferences[index].Charge,
                    $scope.CellphonePRef = $scope.personalReferences[index].Cellphone,
                    $scope.EmailPRef = $scope.personalReferences[index].Email,
                    $scope.CompanyNamePRef = $scope.personalReferences[index].CompanyName,
                    $scope.RelationshipPRef = $scope.personalReferences[index].Relationship,
                    $scope.CityIdPRef = $scope.personalReferences[index].CityId,
                    $scope.CountryIdPRef = $scope.personalReferences[index].City.CountryId;
                $scope.action = 'edit';
                $scope.index = index;
            };
            $scope.addPersonalReference = function () {
                if ($scope.action == 'edit') {
                    $scope.personalReferences[$scope.index].FirstName = $scope.FirstNamePRef,
                        $scope.personalReferences[$scope.index].LastName = $scope.LastNamePRef,
                        $scope.personalReferences[$scope.index].Charge = $scope.ChargePRef,
                        $scope.personalReferences[$scope.index].Cellphone = $scope.CellphonePRef,
                        $scope.personalReferences[$scope.index].Email = $scope.EmailPRef,
                        $scope.personalReferences[$scope.index].CompanyName = $scope.CompanyNamePRef,
                        $scope.personalReferences[$scope.index].Relationship = $scope.RelationshipPRef,
                        $scope.personalReferences[$scope.index].CityId = $scope.CityIdPRef,
                        $scope.personalReferences[$scope.index].City = $filter('filter')($scope.Cities, { CityId: $scope.CityPRef })[0],
                        $scope.personalReferences[$scope.index].CountryId = $scope.CountryIdPRef,
                        $scope.personalReferences[$scope.index].Country = $filter('filter')($scope.Countries, { CountryId: $scope.CountryIdPRef })[0];
                } else {
                    var pReference = {
                        FirstName: $scope.FirstNamePRef,
                        LastName: $scope.LastNamePRef,
                        Charge: $scope.ChargePRef,
                        Cellphone: $scope.CellphonePRef,
                        Email: $scope.EmailPRef,
                        CompanyName: $scope.CompanyNamePRef,
                        Relationship: $scope.RelationshipPRef,
                        CityId: $scope.CityIdPRef,
                        City: $filter('filter')($scope.Cities, { CityId: $scope.CityPRef })[0],
                        CountryId: $scope.CountryIdPRef,
                        Country: $filter('filter')($scope.Countries, { CountryId: $scope.CountryIdPRef })[0]
                    };
                    $scope.personalReferences.push(pReference);
                    $scope.clearPersonalReference();
                }
            };
            $scope.removePersonalReference = function (index) {
                $scope.personalReferences.splice(index, 1);
            };
           
            /*Tranning*/
            $scope.clearTranning = function () {
                $scope.textButton = 'Agregar';
                $scope._TrainingName = "",
                    $scope._InstitutionName = "",
                    $scope._AcademicLevel = "",
                    $scope._EducationTypeId = "",
                    $scope._CityId = "",
                    $scope._Year = "",
                    $scope._CountryId = "";
                $scope.action = '';
                $scope.index = -1;
            };
            $scope.fillTranning = function (index) {
                $scope.textButton = "Editar";
                $scope._TrainingName = $scope.Trannings[index].TrainingName,
                    $scope._InstitutionName = $scope.Trannings[index].InstitutionName,
                    $scope._CareerId = $scope.Trannings[index].CareerId,
                    $scope._EducationTypeId = $scope.Trannings[index].EducationTypeId,
                    $scope._CityId = $scope.Trannings[index].CityId,
                    $scope._Year = $scope.Trannings[index].Year,
                    $scope._CountryId = $scope.Trannings[index].City.CountryId;
                $scope.action = 'edit';
                $scope.index = index;
            };
            $scope.addTranning = function () {
                if ($scope.action == 'edit') {
                    $scope.Trannings[$scope.index].TrainingName = $scope._TrainingName;
                    $scope.Trannings[$scope.index].InstitutionName = $scope._InstitutionName;
                    $scope.Trannings[$scope.index].Career = $filter('filter')($scope.AllCarees, { CareerId: $scope._CareerId })[0];
                    $scope.Trannings[$scope.index].CareerId = $scope._CareerId;
                    $scope.Trannings[$scope.index].EducationTypeId = $scope._EducationTypeId;
                    $scope.Trannings[$scope.index].City = $filter('filter')($scope.Cities, { CityId: $scope._CityId })[0];
                    $scope.Trannings[$scope.index].CityId = $scope._CityId;
                    $scope.Trannings[$scope.index].Year = $scope._Year;
                    $scope.Trannings[$scope.index].Country = $filter('filter')($scope.Countries, { CountryId: $scope._CountryId })[0];
                    $scope.Trannings[$scope.index].CountryId = $scope._CountryId;
                } else {
                    var tranning = {
                        TrainingName: $scope._TrainingName,
                        InstitutionName: $scope._InstitutionName,
                        Career: $filter('filter')($scope.AllCarees, { CareerId: $scope._CareerId })[0],
                        CareerId: $scope._CareerId,
                        EducationTypeId: $scope._EducationTypeId,
                        City: $filter('filter')($scope.Cities, { CityId: $scope._CityId })[0],
                        CityId: $scope._CityId,
                        Year: $scope._Year,
                        Country: $filter('filter')($scope.Countries, { CountryId: $scope._CountryId })[0],
                        CountryId: $scope._CountryId
                    };
                    if(!$scope.Trannings)
                    $scope.Trannings = [];

                    $scope.Trannings.push(tranning);
                    $scope.clearTranning();
                }
            };
            $scope.removeTranning = function (index) {
                $scope.Trannings.splice(index, 1);
            };

            customerRepository.getCities().success(function (data) {
                $scope.Cities = data;
            });
            customerRepository.getCareers().success(function (data) {
                $scope.AllCarees = data;
            });
            customerRepository.getCountries().success(function (data) {
                $scope.Countries = data;
            });
            customerRepository.getAcademicLevels().success(function (data) {

                $scope.AcademicLevels = data;
            });
            customerRepository.getEducationTypes().success(function (data) {

                $scope.EducationTypes = data;
            });
            customerRepository.getLanguages().success(function (data) {

                $scope.Languages = data;
            });
            customerRepository.getLanguageLevels().success(function (data) {

                $scope.LanguageLevels = data;
            });


        $scope.getCareersByAcademicLevel = function(academicLevelId) {
            for (var i = 0; i < $scope.AcademicLevels.length; i++) {
                var id = $scope.AcademicLevels[i].AcademicLevelId
                if (academicLevelId.AcademicLevelId == id)
                    return $scope.AcademicLevels[i].Careers;
            }
        };
            $scope.assignCareers = function () {
                if ($scope.AcademicLevelId)
                    $scope.Careers = $filter('filter')($scope.AcademicLevels, { AcademicLevelId: $scope.AcademicLevelId })[0].Careers;
            }

            $scope.update = function() {
                $scope.New.References = [];
                $scope.New.AcademicEducations = [];

                if ($scope.academicEducations.length > 0) {

                    for (var i = 0; i < $scope.academicEducations.length; i++) {
                        var academic = {
                            AcademicEducationId: ($scope.academicEducations[i].AcademicEducationId) ? $scope.academicEducations[i].AcademicEducationId : 0,
                            Year: $scope.academicEducations[i].Year,
                            CountryId: $scope.academicEducations[i].City.Country.CountryId,
                            CityId: $scope.academicEducations[i].City.CityId,
                            AcademicLevelId: $scope.academicEducations[i].AcademicLevel.AcademicLevelId,
                            TrainingName: $scope.academicEducations[i].TrainingName,
                            InstitutionName: $scope.academicEducations[i].InstitutionName,
                            CareerId: $scope.academicEducations[i].CareerId,
                            EducationTypeId: 1
                        };
                        $scope.New.AcademicEducations.push(academic);
                    }
                }
                if ($scope.Trannings.length > 0) {
                    for (var i = 0; i < $scope.Trannings.length; i++) {
                        var tranning = {
                            AcademicEducationId: ($scope.Trannings[i].AcademicEducationId) ? $scope.Trannings[i].AcademicEducationId : 0,
                            Year: $scope.Trannings[i].Year,
                            CountryId: $scope.Trannings[i].City.Country.CountryId,
                            CityId: $scope.Trannings[i].City.CityId,
                            TrainingName: $scope.Trannings[i].TrainingName,
                            InstitutionName: $scope.Trannings[i].InstitutionName,
                            CareerId: $scope.Trannings[i].CareerId,
                            EducationTypeId: 2
                        };
                        $scope.New.AcademicEducations.push(tranning);
                    }
                }
                if ($scope.workReferences.length > 0) {
                        for (var l = 0; l < $scope.workReferences.length; l++) {
                            var wExperience = {
                                ReferenceId: ($scope.workReferences[l].ReferenceId) ? $scope.workReferences[l].ReferenceId : null,
                                FirstName: $scope.workReferences[l].FirstName,
                                LastName: $scope.workReferences[l].LastName,
                                Charge: $scope.workReferences[l].Charge,
                                Cellphone: $scope.workReferences[l].Cellphone,
                                Email: $scope.workReferences[l].Email,
                                CompanyName: $scope.workReferences[l].CompanyName,
                                Relationship: $scope.workReferences[l].Relationship,
                                ReferenceTypeId: 1,
                                CityId: $scope.workReferences[l].City.CityId
                            }

                            $scope.New.References.push(wExperience);
                        }
                    }

                    if ($scope.personalReferences.length > 0) {
                        for (var l = 0; l < $scope.personalReferences.length; l++) {
                            var pExperience = {
                                ReferenceId: ($scope.personalReferences[l].ReferenceId) ? $scope.personalReferences[l].ReferenceId : null,
                                FirstName: $scope.personalReferences[l].FirstName,
                                LastName: $scope.personalReferences[l].LastName,
                                Charge: $scope.personalReferences[l].Charge,
                                Cellphone: $scope.personalReferences[l].Cellphone,
                                Email: $scope.personalReferences[l].Email,
                                CompanyName: $scope.personalReferences[l].CompanyName,
                                Relationship: $scope.personalReferences[l].Relationship,
                                ReferenceTypeId: 2,
                                CityId: $scope.personalReferences[l].City.CityId
                            }

                            $scope.New.References.push(pExperience);
                        }
                    }
                customerRepository.UpdateCustomer($scope.New);
            }
        }
    ]);