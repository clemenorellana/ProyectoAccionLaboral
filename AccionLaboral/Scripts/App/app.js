/// <reference path="../../Views/Clientes/_RegisterClient.html" />
'use strict';

angular.module('AccionLaboralApp', [
        'ngRoute',
        'ngCookies',
        'ui.bootstrap',
        'dialogs',
        'clientsController',
        'careersController',
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
        'ui.select'
])
    .config([
        '$routeProvider',
        function($routeProvider) {
            $routeProvider.
                otherwise({
                    redirectTo: '/'
                });
        }
    ]).filter('startFrom', function() {
        return function(input, start) {
            if (input) {
                start = +start; //parse to int
                return input.slice(start);
            }
            return [];
        }
    }).controller('mainController', [
        '$scope', '$location', '$cookies', '$rootScope', '$timeout', '$dialogs', 'usersRepo', 'employeesRepo',  function ($scope, $location, $cookies, $rootScope, $timeout, $dialogs, usersRepo, employeesRepo) {
            $rootScope.validPass = true;
            $scope.alerts = [];
            $rootScope.forgotPass = false;
            $rootScope.resetPass = false;
            $scope.addAlert = function (type, msg) {
                $scope.alerts[0] = {type: type, msg: msg};
            };

            $scope.closeAlert = function (index) {
                $scope.alerts.splice(index, 1);
            };
            
            var progress = 25;
            var msgs = [
              'Hey! I\'m waiting here...',
              'About half way done...',
              'Almost there?',
              'Woo Hoo! I made it!'
            ];
            var i = 0;
            var fakeProgress = function () {
                $timeout(function () {
                    if (progress < 100) {
                        progress += 25;
                        $rootScope.$broadcast('dialogs.wait.progress', { msg: msgs[i++], 'progress': progress });
                        fakeProgress();
                    } else {
                        $rootScope.$broadcast('dialogs.wait.complete');
                    }
                }, 1000);
            }; // end fakeProgress 

            $scope.launch = function (dialog) {
                $dialogs.wait(msgs[i++], progress);
                fakeProgress();
            };

            var userStored = $cookies.userName;
            //var userStored = $cookies.userLoggedIn;
            if (userStored == "null" || (!userStored)) {
                $scope.skinClass = "bg-black";
                $scope.template = 'Users/Login';
                $scope.userValid = false;
            }
            else {
                $scope.skinClass = "skin-blue";
                $scope.userValid = true;
                $scope.template = 'Home/Home';
                $rootScope.user = { UserName: userStored };
                $location.path('/');
            }

            $scope.clearCookies = function () {
                $cookies.userLoggedInCookie = null;
                $cookies.userLogged = null;

                $cookies.userAdmissionDate = null;
                $cookies.userFirstName = null;
                $cookies.userLastName = null;
                $cookies.userPhoto = null;
                $cookies.userRoleId = null;
                $cookies.userUserId = null;
                $cookies.userUserName = null;
                $cookies.EmployeeId = null;


            }

            $scope.viewProfile = function ()
            {
                var employeeId = $rootScope.userLoggedIn.EmployeeId;
                //window.location = "#/Employees/Edit/" + employeeId;
                $scope.template = '/Employees/Edit/' + employeeId;
                //window.location = "#/Employees";

            }

            $scope.logout = function () {
                $cookies.userName = null;

                $scope.clearCookies();

                $scope.skinClass = "bg-black";
                $scope.template = "Users/Login";
                $scope.userValid = false;
            }

            $scope.changePassword = function (UserName, Password1, Password2){
                if (Password1 === Password2) {
                    $rootScope.validPass = true;
                }
                else {
                    $rootScope.validPass = false;
                }
            }

            $scope.sendRequestChangePassword = function (userName) {
                $scope.launch('wait');
                debugger
                $rootScope.forgotPass = false;
                
                usersRepo.requestChangePassword(userName).success(function (data) {
                    $scope.userValid = data;
                    if ($scope.userValid == true) {
                        $scope.addAlert("success", "La solicitud se ha enviado a su correo.");
                        //$scope.skinClass = "bg-black";
                        //$scope.template = "Users/Login";
                        window.location = "#/Login";
                        $rootScope.forgotPass = false;
                    } else {
                        $scope.skinClass = "bg-black";
                        $scope.template = "Users/Login";
                        $scope.addAlert("danger", "Usuario no valido. Intente de nuevo.");
                    };
                })
                .error(function (message) {
                    $scope.addAlert("danger", "Ha ocurrido un error en el servidor.");
                });

            }
            

            $scope.getRole = function (menu) {
                debugger
                var user = $rootScope.userLoggedIn;
                if (menu === "Clientes")
                {
                    if (user.Role.Name === 'Asesor Corporativo')
                        return false;
                    return true;
                }
                else if (menu === 'Administrar el Sistema')
                {
                    if (user.Role.Name === 'Asesor Corporativo' || user.Role.Name === 'Asesor de Reclutamiento')
                        return false;
                    return true;
                }
            }

            //$scope.skinClass = "bg-black";
            //$scope.template = "Users/Login";
            $scope.validateUser = function (userName, password, isValidForm) {
                $scope.launch('wait');
                if(isValidForm){
                    usersRepo.login(userName, password).success(function (data) {
                        var employee = data;
                        //$scope.userValid = data; 
                        $scope.userValid = (employee.EmployeeId != 0) ? true : false;
                        if($scope.userValid == true){
                            $scope.template = 'Home/Home';
                            $scope.skinClass = "skin-blue";
                            $cookies.userName = userName;
                            $rootScope.user = { UserName: $cookies.userName };

                            //$cookies.userLoggedInCookie = employee.FirstName;

                            //var employeeCookie = {
                            //    "Address": $cookies.userLoggedInCookie.Address,
                            //    "AdmissionDate": $cookies.userLoggedInCookie.AdmissionDate,
                            //    "Age": $cookies.userLoggedInCookie.Age,
                            //    "Birthday": $cookies.userLoggedInCookie.Birthday,
                            //    "Career": $cookies.userLoggedInCookie.Career,
                            //    "CareerId": $cookies.userLoggedInCookie.CareerId,
                            //    "Cellphone": $cookies.userLoggedInCookie.Cellphone,
                            //    "Email": $cookies.userLoggedInCookie.Email,
                            //    "EmployeeAlias": $cookies.userLoggedInCookie.EmployeeAlias,
                            //    "EmployeeId": $cookies.userLoggedInCookie.EmployeeId,
                            //    "FirstName": $cookies.userLoggedInCookie.FirstName,
                            //    "Gender": $cookies.userLoggedInCookie.Gender,
                            //    "HomePhone": $cookies.userLoggedInCookie.HomePhone,
                            //    "LastName": $cookies.userLoggedInCookie.LastName,
                            //    "Photo": $cookies.userLoggedInCookie.Photo,
                            //    "Role": $cookies.userLoggedInCookie.Role,
                            //    "RoleId": $cookies.userLoggedInCookie.RoleId,
                            //    "User": {
                            //        "Active": $cookies.userLoggedInCookie.User.Active,
                            //        "Busy": $cookies.userLoggedInCookie.User.Busy,
                            //        ////"Password": $cookies.userLoggedInCookie.Password,
                            //        "UserId": $cookies.userLoggedInCookie.User.UserId,
                            //        "UserName": $cookies.userLoggedInCookie.User.UserName
                            //    },
                            //    "UserId": $cookies.userLoggedInCookie.UserId
                            //}


                            $cookies.userAdmissionDate = employee.AdmissionDate;
                            $cookies.userFirstName = employee.FirstName;
                            $cookies.userLastName = employee.LastName;
                            $cookies.userPhoto = employee.Photo;
                            $cookies.userRoleId = employee.Role;
                            $cookies.userUserId = employee.UserId;
                            $cookies.userUserName = employee.User.UserName;
                            $cookies.EmployeeId = employee.EmployeeId;

                            

                            var employeeCookie = {
                                "AdmissionDate": $cookies.userAdmissionDate,
                                "FirstName": $cookies.userFirstName,
                                "LastName": $cookies.userLastName,
                                "Photo": $cookies.userPhoto,
                                "Role": $cookies.userRoleId,
                                "UserName": $cookies.userUserName,
                                "UserId": $cookies.userUserId,
                                "EmployeeId": $cookies.EmployeeId
                            }

                            
                            $rootScope.userLoggedIn = employeeCookie;

                                
                            $rootScope.forgotPass =false;
                        } else {
                            $scope.skinClass = "bg-black";
                            $scope.template = "Users/Login";
                            $scope.addAlert("danger", "Usuario o clave invalido. Intente de nuevo.");
                        };
                    })
                        .error(function (message) {
                            $scope.addAlert("danger","Ha ocurrido un error en el servidor.");
                        });
                }
                else {
                    $scope.addAlert("danger", "Hay campos invalidos");
                }
            }

        }
    ])
    .run(['$rootScope', '$location', '$cookies', '$http',
    function ($rootScope, $location, $cookies, $http) {
        // keep user logged in after page refresh

        //$rootScope.userLoggedIn = { FirstName: $cookies.userLoggedInCookie };
        //$cookies.userLoggedInCookie = employee.FirstName;


        var employeeCookie = {
            "AdmissionDate": $cookies.userAdmissionDate,
            "FirstName": $cookies.userFirstName,
            "LastName": $cookies.userLastName,
            "Photo": $cookies.userPhoto,
            //"Role": $cookies.userLoggedInCookie.Role,
            "RoleId": $cookies.userRoleId,
            "UserName": $cookies.userUserName,
            "UserId": $cookies.userUserId,
            "EmployeeId": $cookies.EmployeeId,
        }


        $rootScope.userLoggedIn = employeeCookie;

    }]);
