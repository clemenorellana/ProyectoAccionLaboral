
'use strict';

//var path = "http://localhost:49174";
var pathServer = "http://sdhwinserver.cloudapp.net";

angular.module('AccionLaboralApp', [
 //       'ngRoute',
   //     'ngCookies',
        /*'ui.bootstrap',
        'dialogs',*/
        //'clientsController'
        /*'careersController',
        'contractTemplatesController',
        'employeesController',
        'usersController',
        'countriesController',
        'citiesController',
        'companiesController',
        'vacantsByCompaniesController',
        'interviewTypesController',
        'employeeTypesController',
        'statesController',
        'ngSanitize',
        'ui.select',
        'LocalStorageModule',
        'authService',
        'ui.select',
        'isteven-multi-select'*/
])
    .factory('customerRepository', ['$http', function ($http) {
        return {

            getCities: function (callback) {
                return $http({
                    method: 'GET', url: pathServer + '/api/Cities',
                    headers: {
                        'Access-Control-Allow-Origin': '*',
                        'Access-Control-Allow-Methods': 'GET, POST',
                        'Access-Control-Allow-Headers': 'Content-Type, X-Requested-With'
                    }
                });

            },
            exportCustomersTracking: function (filters) {
                return $http.post(pathServer + '/api/exportclientstracking', filters);
                //return $http.post('clients/ExportClientsTracking', filters);
            },
            getCountries: function (callback) {
                return $http.get(pathServer + '/api/Countries');
            },
            getAcademicLevels: function (callback) {
                return $http.get(pathServer + '/api/AcademicLevels');
            },
            getCareers: function () {
                return $http.get(pathServer + '/api/Careers');
            },
            getEducationTypes: function (callback) {
                return $http.get(pathServer + '/api/EducationTypes');
            },
            getCompanies: function (callback) {
                return $http.get(pathServer + '/api/Companies');
            },
            getLanguages: function (callback) {
                return $http.get(pathServer + '/api/Languages');
            },
            getLanguageLevels: function (callback) {
                return $http.get(pathServer + '/api/LanguageLevels');
            },
            getTrackingTypes: function () {
                return $http.get(pathServer + '/api/trackingtypes');
            },

            getStates: function(){
                return $http.get(pathServer + '/api/States');
            },

            //method for insert
            InsertCustomer: function (callback, client) {
                var Tracking = [{ TrackingTypeId: 1, StateId: 1 }];

                var newClient = {
                    "IdentityNumber": client.IdentityNumber, "Correlative": client.Correlative, "FirstName": client.FirstName,
                    "LastName": client.LastName, "Birthday": client.Birthday, "Age": client.Age,
                    "Gender": client.Gender, "Email": client.Email, "Neighborhood": client.Neighborhood,
                    "CompleteAddress": client.CompleteAddress, "Cellphone": client.Cellphone, "HomePhone": client.HomePhone,
                    "Hobby": client.Hobby, "Photo": client.Photo, "CurrentStudies": client.CurrentStudies,
                    "WageAspiration": client.WageAspiration, "FacebookEmail": client.FacebookEmail, "BBPin": client.BBPin,
                    "Twitter": client.Twitter, "DesiredEmployment": client.DesiredEmployment, "CompaniesWithPreviouslyRequested": client.CompaniesWithPreviouslyRequested,

                    "CityId": client.CityId.CityId,
                    "EmployeeId": client.AdvisorId,
                    "CareerId": client.CareerId,
                    "StateId": 1,

                    "AcademicEducations": client.AcademicEducations, "Languages": client.Languages, "KnownPrograms": client.KnownPrograms,
                    "WorkExperiences": client.workExperiences, "References": client.workReferences, "Trackings": Tracking

                };

                return $http.post(pathServer + '/api/clients/', newClient);

            }
        }
    }])
    .factory('alertService', ['$timeout', '$rootScope',
    function ($timeout, $rootScope) {
        var alertService = {};

        alertService.add = function (type, title, msg, timeout) {
            $rootScope.alerts.push({
                type: type,
                title: title,
                msg: msg,
                close: function () {
                    return alertService.closeAlert(this);
                }
            });

            if (typeof timeout == 'undefined') {
                timeout = 4000;
            }

            if (timeout) {
                $timeout(function () {
                    alertService.closeAlert(this);
                }, timeout);
            }
        }

        alertService.closeAlert = function (alert) {
            return this.closeAlertIdx($rootScope.alerts.indexOf(alert));
        }

        alertService.closeAlertIdx = function (index) {
            return $rootScope.alerts.splice(index, 1);
        }

        return alertService;
    }
    ])
    .controller('CustomerController', ['$scope', '$rootScope', '$location', '$filter', '$window', 'customerRepository', 'filterFilter', 'alertService', function ($scope, $rootScope, $location, $filter, $window, customerRepository, filterFilter, alertService) {
        //enroll costumer
        $scope.enrollClientExist = false;
        $scope.showMsgErrorClient = false;
        if (!$rootScope.alerts)
            $rootScope.alerts = [];

        $scope.back = function () {
            $scope.New = "";
            $scope.episodeImgData = undefined;
            $scope.academicEducations = [], $scope.knownLanguages = [], $scope.knownPrograms = [], $scope.personalReferences = [], $scope.workReferences = [], $scope.workExperiences = [], $scope.Trannings = [];
        };
        $scope.itemsInReportPage = 0;
        $scope.getFormatDate = function (date) {
            var dd = date.getDate();
            var mm = date.getMonth() + 1; //January is 0!

            var yyyy = date.getFullYear();
            if (dd < 10) {
                dd = '0' + dd
            }
            if (mm < 10) {
                mm = '0' + mm
            }

            return (dd + '/' + mm + '/' + yyyy);
        }


        $scope.calculateAge = function () {
            var birthday = +new Date($scope.New.Birthday);
            var age = ~~((Date.now() - birthday) / (31557600000));
            $scope.New.Age = age;
        }
        $scope.handleFileSelectAdd = function (evt) {

            var f = evt.target.files[0];
            var reader = new FileReader();
            reader.onload = (function (theFile) {
                return function (e) {
                    var filePayload = e.target.result;
                    $scope.episodeImgData = filePayload.replace('data:' + f.type + ';base64,', '');
                    document.getElementById('imagen').src = filePayload;
                    $scope.$apply(function () {

                    });
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

        $scope.setClientToDelete = function (id) {
            $scope.selectedClient = id;
        }
        $scope.addAcademicEducation = function () {
            if ($scope.academicEducationModal.$valid) {
                if ($scope.action == 'edit') {
                    $scope.academicEducations[$scope.index].TrainingName = $scope.TrainingName,
                        $scope.academicEducations[$scope.index].InstitutionName = $scope.InstitutionName,
                        $scope.academicEducations[$scope.index].AcademicLevel = $scope.AcademicLevel,
                        $scope.academicEducations[$scope.index].CareerId = $scope.CareerId,
                        $scope.academicEducations[$scope.index].EducationTypeId = $scope.EducationTypeId,
                        $scope.academicEducations[$scope.index].City = $scope.City,
                        $scope.academicEducations[$scope.index].Year = $scope.Year,
                        $scope.academicEducations[$scope.index].Country = $scope.Country;
                    alertService.add('success', 'Actualizado', 'Registro se ha actualizado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
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
                    alertService.add('success', 'Insertado', 'Registro se ha insertado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                }
            } else {
                alertService.add('danger', 'Error', 'Complete todos los campos correctamente.');
                $scope.alertsTags = $rootScope.alerts;
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
            $scope.CareerId = "",
            $scope.EducationTypeId = "";
            $scope.City = "",
            $scope.Year = "",
            $scope.Country = "";
        };
        $scope.removeAcademicEducation = function (index) {
            $scope.academicEducations.splice(index, 1);
            alertService.add('success', 'Eliminado', 'Registro se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
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
            if ($scope.FormLanguage.$valid) {
                if ($scope.action == 'edit') {
                    $scope.knownLanguages[$scope.index].Percentage = $scope.Percentage,
                        $scope.knownLanguages[$scope.index].Language = $scope.Language,
                        $scope.knownLanguages[$scope.index].LanguageLevel = $scope.LanguageLevel;
                    alertService.add('success', 'Actualizado', 'Registro se ha actualizado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                } else {
                    var lang = {
                        Percentage: $scope.Percentage,
                        Language: $scope.Language,
                        LanguageLevel: $scope.LanguageLevel
                    };
                    $scope.knownLanguages.push(lang);
                    $scope.clearKnownLanguage();
                    alertService.add('success', 'Insertado', 'Registro se ha insertado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                }
            } else {
                alertService.add('danger', 'Error', 'Complete todos los campos correctamente.');
                $scope.alertsTags = $rootScope.alerts;
            }
        };
        $scope.removeKnownLanguage = function (index) {
            $scope.knownLanguages.splice(index, 1);
            alertService.add('success', 'Eliminado', 'Registro se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
        };
        $scope.removeWorkExperience = function (index) {
            $scope.workExperiences.splice(index, 1);
            alertService.add('success', 'Eliminado', 'Registro se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
        };
        $scope.removeWorkReference = function (index) {
            $scope.workReferences.splice(index, 1);
            alertService.add('success', 'Eliminado', 'Registro se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
        };
        $scope.removePersonalReference = function (index) {
            $scope.personalReferences.splice(index, 1);
            alertService.add('success', 'Eliminado', 'Registro se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
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
            if ($scope.FormProgram.$valid) {
                if ($scope.action == 'edit') {
                    $scope.knownPrograms[$scope.index].Name = $scope.NameProgram;
                    alertService.add('success', 'Actualizado', 'Registro se ha actualizado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                } else {
                    var program = {
                        Name: $scope.NameProgram
                    };
                    $scope.knownPrograms.push(program);
                    $scope.clearProgram();
                    alertService.add('success', 'Insertado', 'Registro se ha insertado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                }
            } else {
                alertService.add('danger', 'Error', 'Complete todos los campos correctamente.');
                $scope.alertsTags = $rootScope.alerts;
            }
        };
        $scope.removeKnownProgram = function (index) {
            $scope.knownPrograms.splice(index, 1);
            alertService.add('success', 'Eliminado', 'Registro se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
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
            if ($scope.formWorkExperience.$valid) {
                if ($scope.action == 'edit') {
                    $scope.workExperiences[$scope.index].CompanyName = $scope.CompanyName,
                        $scope.workExperiences[$scope.index].CompanyArea = $scope.CompanyArea,
                        $scope.workExperiences[$scope.index].Charge = $scope.Charge,
                        $scope.workExperiences[$scope.index].StartDate = $scope.StartDate,
                        $scope.workExperiences[$scope.index].EndDate = $scope.EndDate,
                        $scope.workExperiences[$scope.index].Archievements = $scope.Achievements,
                        $scope.workExperiences[$scope.index].WorkCity = $scope.WorkCity,
                        $scope.workExperiences[$scope.index].WorkCountry = $scope.WorkCountry;
                    alertService.add('success', 'Actualizado', 'Registro se ha actualizado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
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
                    alertService.add('success', 'Insertado', 'Registro se ha insertado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                }
            } else {
                alertService.add('danger', 'Error', 'Complete todos los campos correctamente.');
                $scope.alertsTags = $rootScope.alerts;
            }
        };
        $scope.removeWorkExperience = function (index) {
            $scope.workExperiences.splice(index, 1);
            alertService.add('success', 'Eliminado', 'Registro se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
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
            $scope.FirstNameWRef = $scope.workReferences[indupex].FirstName,
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
            if ($scope.formWorkReference.$valid) {
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
                    alertService.add('success', 'Actualizado', 'Registro se ha actualizado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
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
                    alertService.add('success', 'Insertado', 'Registro se ha insertado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                }
            } else {
                alertService.add('danger', 'Error', 'Complete todos los campos correctamente.');
                $scope.alertsTags = $rootScope.alerts;
            }
        };
        $scope.removeWorkReference = function (index) {
            $scope.workReferences.splice(index, 1);
            alertService.add('success', 'Eliminado', 'Registro se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
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
            if ($scope.formPersonalRef.$valid) {
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
                    alertService.add('success', 'Actualizado', 'Registro se ha actualizado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
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
                    alertService.add('success', 'Insertado', 'Registro se ha insertado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                }
            } else {
                alertService.add('danger', 'Error', 'Complete todos los campos correctamente.');
                $scope.alertsTags = $rootScope.alerts;
            }
        };
        $scope.removePersonalReference = function (index) {
            $scope.personalReferences.splice(index, 1);
            alertService.add('success', 'Eliminado', 'Registro se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
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
        $scope.fillTranning = function (index) {
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
        $scope.addTranning = function () {
            if ($scope.trainingForm.$valid) {
                if ($scope.action == 'edit') {
                    $scope.Trannings[$scope.index].TrainingName = $scope._TrainingName,
                        $scope.Trannings[$scope.index].InstitutionName = $scope._InstitutionName,
                        $scope.Trannings[$scope.index].CareerId = $scope._Career.CareerId,
                        $scope.Trannings[$scope.index].City = $scope._City,
                        $scope.Trannings[$scope.index].Year = $scope._Year,
                        $scope.Trannings[$scope.index].Country = $scope._Country;
                    alertService.add('success', 'Actualizado', 'Registro se ha actualizado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                } else {
                    var tranning = {
                        TrainingName: $scope._TrainingName,
                        InstitutionName: $scope._InstitutionName,
                        CareerId: $scope._Career.CareerId,
                        EducationTypeId: $scope._EducationTypeId,
                        City: $scope._City,
                        Year: $scope._Year,
                        Country: $scope._Country
                    };
                    $scope.Trannings.push(tranning);
                    $scope.clearTranning();
                    alertService.add('success', 'Insertado', 'Registro se ha insertado correctamente.');
                    $scope.alertsTags = $rootScope.alerts;
                }
            } else {
                alertService.add('danger', 'Error', 'Complete todos los campos correctamente.');
                $scope.alertsTags = $rootScope.alerts;
            }
        };
        $scope.removeTranning = function (index) {
            $scope.Trannings.splice(index, 1);
            alertService.add('success', 'Eliminado', 'Registro se ha eliminado correctamente.');
            $scope.alertsTags = $rootScope.alerts;
        };

        customerRepository.getCareers().success(function (data) {
            $scope.AllCarees = data;
        });

        customerRepository.getCities().success(function (data) {
            $scope.Cities = data;
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
        $scope.getStates = function () {
            customerRepository.getStates().success(function (data) {
                $scope.States = data;
            });
        };

        $scope.assignCareers = function () {
            if ($scope.AcademicLevel)
                $scope.Careers = $scope.AcademicLevel.Careers;
        };
        $scope.getCitiesByCountry = function (countryId) {
            if (countryId)
                return $filter('filter')($scope.Countries, { CountryId: countryId })[0].Cities;
        };

        $scope.update = function () {
            if ($scope.clientForm.$valid) {
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

                if ($scope.knownLanguages.length > 0) {
                    $scope.New.Languages = [];
                    for (var j = 0; j < $scope.knownLanguages.length; j++) {
                        var language = {
                            LanguageId: $scope.knownLanguages[j].Language.LanguageId,
                            LanguageLevelId: $scope.knownLanguages[j].LanguageLevel.LanguageLevelId,
                            Percentage: $scope.knownLanguages[j].Percentage
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

                //$scope.New.AdvisorId = $rootScope.userLoggedIn.EmployeeId;
                $scope.New.AdvisorId = 1;

                if ($scope.action != 'edit') {
                    customerRepository.InsertCustomer(function () {

                    }, $scope.New).success(function () {
                        alertService.add('success', 'Registrado', 'Usted se ha registrado correctamente.');

                        $scope.alertsTags = $rootScope.alerts;
                        //$location.path("/AllClients");
                    }).error(function () {
                        alertService.add('danger', 'Error', 'No se ha podido registrar. Espere un momento e intente de nuevo.');

                        $scope.alertsTags = $rootScope.alerts;
                    });

                }

            } else {
                alertService.add('danger', 'Error', 'Complete correctamente todos los campos.');
                $scope.alertsTags = $rootScope.alerts;
            }
        }
    }]).filter('startFrom', function () {
        return function (input, start) {
            if (input) {
                start = +start; //parse to int
                return input.slice(start);
            }
            return [];
        }
    }).directive('inputMask', function () {
        return {
            restrict: 'A',
            link: function (scope, el, attrs) {
                $(el).inputmask(scope.$eval(attrs.inputMask));
            }
        };
    })
    .factory('contactUsRepository', ['$http', function ($http) {
        return {
            sendMail: function (callback, message) {
                return $http.post('api/Contactus', message);
            }
        }
    }])
    .controller('contactUsController', ['$scope', 'contactUsRepository', 'alertService', '$rootScope', function ($scope, contactUsRepository, alertService, $rootScope) {

        if (!$rootScope.alerts)
            $rootScope.alerts = [];

        //$scope.fullName = "Nombre";
        //$scope.email = "Correo Electrónico";
        //$scope.subject = "Asunto";
        //$scope.message = "Mensaje";

        $scope.sendMail = function () {
            var message = {
                FullName: $scope.fullName,
                Email: $scope.email,
                Subject: $scope.subject,
                Message: $scope.message
            };


            contactUsRepository.sendMail(function () {
            }, message).success(function () {
                alertService.add('success', 'Mensaje', 'El mensaje se ha enviado correctamente.');
                $scope.alertsTags = $rootScope.alerts;

                $scope.fullName = "";
                $scope.email = null;
                $scope.subject = null;
                $scope.message = null;

            }).error(function () {
                alertService.add('danger', 'Error', 'No se ha podido enviar el mensaje.');
                $scope.alertsTags = $rootScope.alerts;
            });
        };

    }]);
;
