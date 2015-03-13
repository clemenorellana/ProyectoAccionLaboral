
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
        'ui.select',
        'LocalStorageModule',
        'authService',
        'ui.select',
        'isteven-multi-select'
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
        '$scope', '$location', '$cookies', '$rootScope', '$timeout', '$dialogs', 'usersRepo', 'employeesRepo', '$cookieStore', 'authService', function ($scope, $location, $cookies, $rootScope, $timeout, $dialogs, usersRepo, employeesRepo, $cookieStore, authService) {
            $rootScope.userLoggedIn = authService.authentication.employee;

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

            
            var isLoggedIn = authService.authentication.isAuth;
            if (isLoggedIn == false || isLoggedIn == null) { // si no esta conectado
                $scope.skinClass = "bg-black";
                $scope.template = 'Users/Login';
                $scope.userValid = false;
            }
            else {
                $scope.skinClass = "skin-blue";
                $scope.userValid = true;
                $scope.template = 'Home/Home';
                $location.path('/');
            }


            $scope.viewProfile = function (paramEmployeeId)
            {
                var cureentEmployeeId = $rootScope.userLoggedIn.EmployeeId;
                if (cureentEmployeeId == paramEmployeeId)
                    window.location = "#/Employees/Profile/" + cureentEmployeeId;
                else
                    $location.path('/');

            }


            $scope.changePassword = function (UserName, Password1, Password2) {

                usersRepo.validateUserName(UserName).success(function (data) {
                    var userValid = data;
                    if (!userValid) {
                        $scope.addAlert("danger", "Usuario no valido. Intente de nuevo.");
                    }
                })
                .error(function (message) {
                    $scope.addAlert("danger", "Ha ocurrido un error en el servidor.");
                });

                if (Password1 === Password2) {
                    $rootScope.validPass = true;
                }
                else {
                    $rootScope.validPass = false;
                }
    
                usersRepo.changePassword(UserName, Password1).success(function (data) {
                    $scope.addAlert("success", "La clave se ha cambiado exitosamente.");
                    $scope.resetPass = false;
                    $location.path('/');
                    $scope.skinClass = "bg-black";
                    $scope.template = 'Users/Login';
                    $scope.userValid = false;
                })
                .error(function (message) {
                    $scope.addAlert("danger", "Ha ocurrido un error en el servidor.");
                });

            }

            $scope.sendRequestChangePassword = function (userName) {
                $scope.launch('wait');

                $rootScope.forgotPass = false;
                
                usersRepo.requestChangePassword(userName).success(function (data) {
                    $scope.userValid = data;
                    if ($scope.userValid == true) {
                        $scope.addAlert("success", "La solicitud se ha enviado a su correo.");
                        window.location = "#/Login";
                        $scope.skinClass = "bg-black";
                        $scope.userValid = false;

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
                
                /*
                ---------------------------------------------------------
                Listado de roles
                    Alias - Nombre del Rol
                    ADMIN - Administrador del Sistema	
                    GTEGE - Gerente General	
                    GTEAG - Gerente de Agencia	
                    ASIGE - Asistente de Gerencia	
                    ASREC - Asesor de Reclutamiento	
                    ASCOR - Asesor Corporativo	 
                ---------------------------------------------------------        
                */

                var user = $rootScope.userLoggedIn;
                // Opciones del menu de "Administrar el Sistema"
                if ( menu == "ManageSystem" || menu == "Users" || menu == "Employees" || menu == "EmployeeTypes" || menu == "Roles" || 
                     menu == "Countries" || menu == "Cities" || menu == "Careers" || menu == "Contracts" || menu == "Interviews" )
                    return (user.Role.Alias == "ASCOR" || user.Role.Alias == "ASREC") ? false : true;
                

                // Opciones del menu de "Clientes"
                if (menu == "Clients" || menu == "AllClients" || menu == "RegisterClient" || menu == "EnrollClient" )
                    return (user.Role.Alias == "ASCOR") ? false : true;


                // Opciones del menu de "Corportativo"
                if (menu == "Corporativo" || menu == "Companies" || menu == "AddCompany" || menu == "Vacants" || menu == "AddVacant")
                    return (user.Role.Alias == "ASREC") ? false : true;
                

                // Opciones del menu de "Documentos por aprobar"
                if (menu == "DocumentsToApprove")
                    return (user.Role.Alias == "ASREC") ? false : true;

                // Opciones del menu de "Reclutamiento"
                if (menu == "Recruitment")
                    return (user.Role.Alias == "ASCOR") ? false : true;

                // Opciones del menu de "Repotes"
                if ( menu == "DiscardedCustomersReport" || menu == "NewCompaniesReport" || menu == "CompaniesReport")
                    return (user.Role.Alias == "ASREC") ? false : true;

                if (menu == "ContractReport" || menu == "CVReport" || menu == "NewClientsReport" )
                    return (user.Role.Alias == "ASCOR") ? false : true;

                
            }

            $scope.logout = function () {
                authService.logOut();
                $location.path('/');

                $scope.skinClass = "bg-black";
                $scope.template = "Users/Login";
                $scope.userValid = false;
            }

            
            $scope.validateUser = function (userName, password, isValidForm) {
                $scope.launch('wait');
                if (isValidForm) {
                     
                    $scope.loginData = {
                        userName: userName,
                        password: password
                    };

                   
                    authService.login($scope.loginData).then(function (response) {
                            
                        var employee = response;

                        $scope.userValid = (employee.EmployeeId != 0) ? true : false;
                        if ($scope.userValid == true) {
                                $scope.template = 'Home/Home';
                                $scope.skinClass = "skin-blue";
                            $rootScope.userLoggedIn = authService.authentication.employee;
                            $rootScope.forgotPass = false;
                        }
                        else {
                                $scope.skinClass = "bg-black";
                                $scope.template = "Users/Login";
                            $scope.addAlert("danger", "Usuario o clave invalido. Intente de nuevo.");
                            };
                    },
                    function (err) {
                        $scope.message = err.error_description;
                        $scope.addAlert("danger", "Ha ocurrido un error en el servidor.");
                        });
                }
                else {
                    $scope.addAlert("danger", "Existen campos invalidos");
                }
                }

            }
    ])
    .run(['authService', '$rootScope', '$location', function (authService, $rootScope,$location) {
        authService.fillAuthData();
        $rootScope.userLoggedIn = authService.authentication.employee;

        $rootScope.$on('$routeChangeStart', function (event, next, current) {
            var isLoggedIn = authService.authentication.isAuth;
            if (isLoggedIn == false || isLoggedIn == null) { // si no esta conectado
                $scope.skinClass = "bg-black";
                $scope.template = 'Users/Login';
                $scope.userValid = false;
            }
            else {
                var paramEmployeeId = next.params.id;
                var cureentEmployeeId = $rootScope.userLoggedIn.EmployeeId;
                if (cureentEmployeeId != paramEmployeeId && next.originalPath == '/Employees/Profile/:id') {
                    $location.path('/');
                    return;
        }

/*
                ---------------------------------------------------------
                Listado de roles
                    Alias - Nombre del Rol
                    ADMIN - Administrador del Sistema	
                    GTEGE - Gerente General	
                    GTEAG - Gerente de Agencia	
                    ASIGE - Asistente de Gerencia	
                    ASREC - Asesor de Reclutamiento	
                    ASCOR - Asesor Corporativo	 
                ---------------------------------------------------------        
                */

                //Permisos para el Asesor de Reclutamiento
                var role = $rootScope.userLoggedIn.Role;
                if (role.Alias == 'ASREC') {
                    if (next.templateUrl == '/Careers/Index' ||
                        next.templateUrl == '/Cities/Index' ||
                        next.templateUrl == '/Companies/Index' ||
                        next.templateUrl == '/Companies/Create' ||
                        next.templateUrl == '/Companies/Edit/' ||
                        next.templateUrl == '/ContractTemplates/Index' ||
                        next.templateUrl == '/ContractTemplates/Create' ||
                        next.templateUrl == '/ContractTemplates/Edit/' ||
                        next.templateUrl == '/ContractTemplates/Details/' ||
                        next.templateUrl == '/Countries/Index' ||
                        next.templateUrl == '/Employees/Index' ||
                        next.templateUrl == '/Employees/Create' ||
                        next.templateUrl == '/Employees/Edit/' ||
                        next.templateUrl == '/Employees/Details/' ||
                        next.templateUrl == '/Roles/Index' ||
                        next.templateUrl == '/InterviewTypes/Index' ||
                        next.templateUrl == '/States/Index' ||
                        next.templateUrl == '/Users/Index' ||
                        next.templateUrl == '/VacantsByCompany/Index' ||
                        next.templateUrl == '/VacantsByCompany/Create' ||
                        next.templateUrl == '/VacantsByCompany/Edit/' ||
                        next.templateUrl == '/VacantsByCompany/Details/' ||
                        next.templateUrl == '/Contracts/Index' ||
                        next.templateUrl == '/Interviews/Index' ||
                        next.templateUrl == '/Companies/Index' ||
                        next.templateUrl == '/AddCompany/Index' ||
                        next.templateUrl == '/AddVacant/Index' ||
                        next.templateUrl == '/DocumentsToApprove/Index' ||
                        next.templateUrl == '/DiscardedCustomersReport/Index' ||
                        next.templateUrl == '/NewCompaniesReport/Index' ||
                        next.templateUrl == '/CompaniesReport/Index'
                        ) {
                        $location.path('/');
                    }
                }
                //Permisos para el Asesor Corporativo
                else if (role.Alias == 'ASCOR') {
                    if (next.templateUrl == '/Users/Index' ||
                        next.templateUrl == '/Employees/Index' ||
                        next.templateUrl == '/EmployeeTypes/Index' |
                        next.templateUrl == '/Careers/Index' ||
                        next.templateUrl == '/Countries/Index' ||
                        next.templateUrl == '/Cities/Index' ||
                        next.templateUrl == '/Contracts/Index' ||
                        next.templateUrl == '/Interviews/Index' ||
                        next.templateUrl == '/Clients/Index' ||
                        next.templateUrl == '/Clients/Create' ||
                        next.templateUrl == '/Clients/Edit' ||
                        next.templateUrl == '/Clients/Enroll' ||
                        next.templateUrl == '/Clients/Tracking' ||
                        next.templateUrl == '/Clients/ClientTracking' ||
                        next.templateUrl == '/Clients/ClientProfile' ||
                        next.templateUrl == '/ContractTemplates/Index' ||
                        next.templateUrl == '/ContractTemplates/Create' ||
                        next.templateUrl == '/ContractTemplates/Edit/' ||
                        next.templateUrl == '/ContractTemplates/Details/' ||
                        next.templateUrl == '/Employees/Index' ||
                        next.templateUrl == '/Employees/Create' ||
                        next.templateUrl == '/Employees/Edit/' ||
                        next.templateUrl == '/Employees/Details/' ||
                        next.templateUrl == '/Roles/Index' ||
                        next.templateUrl == '/InterviewTypes/Index' ||
                        next.templateUrl == '/States/Index' ||
                        next.templateUrl == '/RegisterClient/Index' ||
                        next.templateUrl == '/EnrollClient/Index' |
                        next.templateUrl == '/ContractReport/Index' ||
                        next.templateUrl == '/Countries/Index' ||
                        next.templateUrl == '/CVReport/Index' ||
                        next.templateUrl == '/NewClientsReport/Index' ||
                        next.templateUrl == '/Clients/SearchClients'
                        ) {
                        $location.path('/');
                    }
                }

            }
        })

    }]);
  