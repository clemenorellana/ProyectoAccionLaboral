'use strict';

angular.module("mainController", ['ngRoute', 'loadingRepository', 'alertRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/', {
            templateUrl: '/Cities/Index',
            controller: 'citiesCtrl'
        });
}]
).controller('mainController', [
        '$scope', '$location', '$cookies', '$rootScope', '$timeout', '$dialogs', 'usersRepo', 'employeesRepo', '$cookieStore', 'authService', 'Idle', '$modal', '$modalStack', '$log', 'alertService', 'modalService',
        function ($scope, $location, $cookies, $rootScope, $timeout, $dialogs, $modalInstance, usersRepo, employeesRepo, $cookieStore, authService, Idle, Keepalive, $modal, $modalStack, $log, alertService, modalService) {
            ////modal service
            var custName = 'calseto';
            var modalOptions = {
                closeButtonText: 'Cancel',
                actionButtonText: 'Delete Customer',
                headerText: 'Delete ' + custName + '?',
                bodyText: 'Are you sure you want to delete this customer?'
            };
            $scope.test = function () {
                modalService.showModal({}, modalOptions).then(function (result) {

                });
            }
            //end modal service



            $scope.showModal = false;
            $rootScope.loading = true;
            function closeModals() {
                $scope.showModal = false;
                //$scope.$apply();
            }

            $scope.$on('IdleStart', function () {
                closeModals();
                $scope.showModal = true;
            });

            $scope.$on('IdleEnd', function () {
                closeModals();
                $scope.showModal = false;
            });

            $scope.$on('IdleTimeout', function () {
                closeModals();
                $scope.showModal = false;
                $scope.logout();
                $scope.$apply();

            });

            $scope.start = function () {
                closeModals();
                Idle.watch();
                $scope.started = true;
            };

            $scope.stop = function () {
                closeModals();
                Idle.unwatch();
                $scope.started = false;

            };


            $rootScope.userLoggedIn = authService.authentication.employee;
            $rootScope.userToken = authService.authentication.token;

            $rootScope.validPass = true;
            $scope.alerts = [];
            $rootScope.forgotPass = false;
            $rootScope.resetPass = false;
            $scope.addAlert = function (type, msg) {
                $scope.alerts[0] = { type: type, msg: msg };
            };

            $scope.closeAlert = function (index) {
                $scope.alerts.splice(index, 1);
            };

            var progress = 50;
            var msgs = [
              'Hey! I\'m waiting here...',
              'About half way done...',
              'Almost there?',
              'Woo Hoo! I made it!'
            ];
            var i = 0;
            var fakeProgress = function () {
                $timeout(function () {
                    //if (progress < 100) {
                    if ($rootScope.loading) {
                        progress += 25;
                        $rootScope.$broadcast('dialogs.wait.progress', { msg: msgs[i++], 'progress': progress });
                        fakeProgress();
                    } else {
                        $rootScope.$broadcast('dialogs.wait.complete');
                    }
                }, 1000);
            }; // end fakeProgress 

            $scope.open = function (size) {
                var modalInstance = $modal.open({
                    templateUrl: 'loadingModal.html',
                    controller: 'mainController',
                    size: size,
                    resolve: {
                        items: function () {
                            return $scope.items;
                        }
                    }
                });
                modalInstance.result.then(function (selectedItem) {
                    $rootScope.enrollClient = selectedItem;
                }, function () {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            };

            $scope.launch = function (dialog) {
                $dialogs.wait(msgs[i++], progress);
                fakeProgress();
            };


            var isLoggedIn = authService.authentication.isAuth;
            if (isLoggedIn == false || isLoggedIn == null) { // si no esta conectado
                $scope.skinClass = "bg-black";
                $scope.template = 'Users/Login';
                $scope.userValid = false;
                $rootScope.loading = false;
            }
            else {
                $scope.skinClass = "skin-blue";
                $scope.userValid = true;
                $scope.template = 'Home/Home';
                $location.path('/HomePage');
                $rootScope.loading = false;
            }


            $scope.viewProfile = function (paramEmployeeId) {
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
                        $rootScope.loading = false;
                    }
                })
                .error(function (message) {
                    $scope.addAlert("danger", "Ha ocurrido un error en el servidor.");
                    $rootScope.loading = false;
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
                    $rootScope.loading = false;
                })
                .error(function (message) {
                    $scope.addAlert("danger", "Ha ocurrido un error en el servidor.");
                    $rootScope.loading = false;
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
                        $rootScope.loading = false;
                    } else {
                        $scope.skinClass = "bg-black";
                        $scope.template = "Users/Login";
                        $scope.addAlert("danger", "Usuario no valido. Intente de nuevo.");
                    };
                    $rootScope.loading = false;
                })
                .error(function (message) {
                    $scope.addAlert("danger", "Ha ocurrido un error en el servidor.");
                    $rootScope.loading = false;
                });

            }


            $scope.getRole = function (menu) {

                
                //---------------------------------------------------------
                //Listado de roles
                //    Alias - Nombre del Rol
                //    ADMIN - Administrador del Sistema	
                //    GTEGE - Gerente General	
                //    GTEAG - Gerente de Agencia	
                //    ASIGE - Asistente de Gerencia	
                //    ASREC - Asesor de Reclutamiento	
                //    ASCOR - Asesor Corporativo	 
                //---------------------------------------------------------        
               

                var user = $rootScope.userLoggedIn;
                // Opciones del menu de "Administrar el Sistema"
                if (menu == "ManageSystem" || menu == "Users" || menu == "Employees" || menu == "EmployeeTypes" || menu == "Roles" ||
                     menu == "Countries" || menu == "Cities" || menu == "Careers" || menu == "Contracts" || menu == "Interviews")
                    return (user.Role.Alias == "ASCOR" || user.Role.Alias == "ASREC") ? false : true;


                // Opciones del menu de "Clientes"
                if (menu == "Clients" || menu == "AllClients" || menu == "RegisterClient" || menu == "EnrollClient")
                    return (user.Role.Alias == "ASCOR") ? false : true;


                // Opciones del menu de "Vacantes" -- esto para que los asesores de reclutamiento las puedan ver
                if (menu == "VacantsMenu")
                    return (user.Role.Alias != "ASREC") ? false : true;


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
                if (menu == "DiscardedCustomersReport")
                    return (user.Role.Alias == "ASREC" || user.Role.Alias == "ASCOR") ? false : true;

                if (menu == "NewCompaniesReport" || menu == "CompaniesReport")
                    return (user.Role.Alias == "ASREC") ? false : true;

                if (menu == "ContractReport" || menu == "CVReport" || menu == "NewClientsReport")
                    return (user.Role.Alias == "ASCOR") ? false : true;


            }

            $scope.logout = function () {
                authService.logOut();
                $location.path('/');

                $scope.skinClass = "bg-black";
                $scope.template = "Users/Login";
                $scope.userValid = false;

                $scope.stop();
            }


            $scope.validateUser = function (userName, password, isValidForm) {
                //$scope.launch('wait');
                //$("#loadingModal").modal('show');
                if (isValidForm) {

                    $scope.loginData = {
                        userName: userName,
                        password: password
                    };

                    //var token = authService.accessToken($scope.loginData);
                    //$scope.loginData.token = token;
                    authService.login($scope.loginData).then(function (response) {
                        //$scope.launch('wait');
                        var employee = response;

                        $scope.userValid = (employee.EmployeeId != 0) ? true : false;

                        //if ($scope.userValid == true && !employee.User.Active)
                        if ($scope.userValid == true && !employee.User.Active) {
                            $scope.skinClass = "bg-black";
                            $scope.template = "Users/Login";
                            $scope.addAlert("danger", "El usuario esta inactivo.");
                            $scope.userValid = false;
                            $rootScope.userLoggedIn = null;
                            $rootScope.userToken = null;
                            authService.logOut();
                            //$("#loadingModal").modal('hide');
                        }
                        else if ($scope.userValid == true) {
                            //$("#loadingModal").modal('hide');
                            $scope.template = 'Home/Home';
                            $scope.skinClass = "skin-blue";
                            $rootScope.userLoggedIn = authService.authentication.employee;
                            $rootScope.userToken = authService.authentication.token;
                            $rootScope.forgotPass = false;
                            $modalInstance.dismiss('cancel');
                            //$modalStack.dismissAll();
                            $location.path('/HomePage');
                            $scope.start();
                        }
                        else {
                            $scope.skinClass = "bg-black";
                            $scope.template = "Users/Login";
                            $scope.addAlert("danger", "Usuario o clave invalido. Intente de nuevo.");
                            $scope.userValid = false;
                            $rootScope.userLoggedIn = null;
                            $rootScope.userToken = null;
                            authService.logOut();
                            //$("#loadingModal").modal('hide');
                            $location.path('/');
                        }
                    },
                    function (err) {
                        $scope.message = err.error_description;
                        $scope.addAlert("danger", $scope.message);
                        //$("#loadingModal").modal('hide');
                    });
                }
                else {
                    $scope.addAlert("danger", "Existen campos invalidos");
                    //$("#loadingModal").modal('hide');
                }
            }

        }
]);