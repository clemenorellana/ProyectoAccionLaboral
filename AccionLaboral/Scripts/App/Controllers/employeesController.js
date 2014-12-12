'use strict';
debugger
angular.module("employeesController", ['ngRoute', 'employeesRepository'])
.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
        when('/Employees', {
            templateUrl: '/Employees/Index',
            controller: 'employeesCtrl'
        })
        .
        when('/Employees/Create', {
            templateUrl: '/Employees/Create',
            controller: 'employeesCtrl'
        }).
        when('/Employees/Edit/:id', {
            templateUrl: function (params) {
                return '/Employees/Edit/' + params.id;
            },
            controller: 'employeesCtrl'
        }).
        when('/Employees/Preview/:id', {
            templateUrl: function (params) {
                return '/Employees/Details/' + params.id;
            },
            controller: 'employeesCtrl'
        })
    ;
}]
).filter('dateFormat', function($filter){
    return function(input){
        if (input == null) {
            return "";
        }
        var _date = $filter('date')(new Date(input), 'MMM dd yyyy');
        return _date;
    };
})
.controller('employeesCtrl', ['$scope', 'employeesRepo', '$routeParams', function ($scope, employeesRepo, $routeParams) {
    debugger

    var actionEmployee = "";
    $scope.employeesList = [];
    $scope.careersList = [];
    $scope.usersList = [];
    $scope.rolesList = [];
    $scope.employeeId = $routeParams.id;

    employeesRepo.getEmployeesList().success(function (data) {
        $scope.employeesList = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.loading = false;
    }).error(function (data) {
        $scope.error = "Ha ocurrido un error al cargar los datos." + data.ExceptionMessage;
        $scope.loading = false;
    });

    employeesRepo.getCareers().success(function (data) {
        $scope.careersList = data;
    });

    employeesRepo.getUsers().success(function (data) {
        $scope.usersList = data;
    });

    employeesRepo.getRoles().success(function (data) {
        $scope.rolesList = data;
    });

    if ($scope.employeeId == null) {
        actionEmployee = "add";
        $scope.employee_modalTitle = "Agregar Empleado";
        $scope.employee_buttonName = "Agregar";
    }
    else {
        actionEmployee = "edit";
        $scope.employee_modalTitle = "Editar Empleado";
        $scope.employee_buttonName = "Editar";
        var id = $scope.employeeId;

        employeesRepo.getEmployee(id).success(function (data) {
            debugger
            var employeeToEdit = data;

            var c = 0;
            for (c = 0; c < $scope.careersList.length; c++) {
                var career = $scope.careersList[c];
                if (career.CareerId == employeeToEdit.CareerId)
                    break;
            }

            var r = 0;
            for (r = 0; r < $scope.rolesList.length; r++) {
                var role = $scope.rolesList[r];
                if (role.RoleId == employeeToEdit.RoleId)
                    break;
            }

            var u = 0;
            for (u = 0; u < $scope.usersList.length; u++) {
                var user = $scope.usersList[u];
                if (user.UserId == employeeToEdit.UserId)
                    break;
            }


            
            $scope.employee_EmployeeId = employeeToEdit.EmployeeId;
            $scope.employee_FirstName = employeeToEdit.FirstName;
            $scope.employee_LastName = employeeToEdit.LastName;
            $scope.employee_Email = employeeToEdit.Email;
            $scope.employee_Birthday = new Date(employeeToEdit.Birthday);
            $scope.employee_Age = employeeToEdit.Age;
            $scope.employee_Cellphone = employeeToEdit.Cellphone;
            $scope.employee_HomePhone = employeeToEdit.HomePhone;
            $scope.employee_Address = employeeToEdit.Address;
            $scope.employee_Gender = employeeToEdit.Gender;
            $scope.employee_EmployeeAlias = employeeToEdit.EmployeeAlias;
            $scope.employee_AdmissionDate = employeeToEdit.AdmissionDate;
            $scope.employee_Career = $scope.careersList[c];//employeeToEdit.Career;
            $scope.employee_Role = $scope.rolesList[r];//employeeToEdit.Role;
            $scope.employee_User = $scope.usersList[u];//employeeToEdit.User;
        });
    }

    

    $scope.employee_addNewRedirect = function () {
        window.location = "#/Employees/Create";
    }

    $scope.employee_editRedirect = function (index) {
        debugger
        var id = $scope.employeesList[index].EmployeeId;
        window.location = "#/Employees/Edit/" + id;
    }

    $scope.employee_viewRedirect = function (index) {
        var id = $scope.employeesList[index].EmployeeId;
        window.location = "#/Employees/Preview/" + id;
    }

    $scope.employee_cancelRedirect = function () {
        window.location = "#/Employees";
    }

    $scope.employee_refresh = function () {
        employeesRepo.getEmployeesList().success(function (data) {
            $scope.employeesList = data;
        });
    }

    $scope.clearData = function () {
        $scope.employee_EmployeeId = "";
        $scope.employee_FirstName = "";
        $scope.employee_LastName = "";
        $scope.employee_Email = "";
        $scope.employee_Birthday = "";
        $scope.employee_Age = "";
        $scope.employee_Cellphone = "";
        $scope.employee_HomePhone = "";
        $scope.employee_Address = "";
        $scope.employee_Gender = "";
        $scope.employee_EmployeeAlias = "";
        $scope.employee_AdmissionDate = "";
        $scope.employee_Career = "";
        $scope.employee_Role = "";
        $scope.employee_User = "";
    }

    $scope.saveEmployee = function () {
        var employee;
        debugger
        if (actionEmployee == "add") {
            employee = {
                FirstName: $scope.employee_FirstName,
                LastName: $scope.employee_LastName,
                Email: $scope.employee_Email,
                Birthday: $scope.employee_Birthday,
                Age: $scope.employee_Age,
                Cellphone: $scope.employee_Cellphone,
                HomePhone: $scope.employee_HomePhone,
                Address: $scope.employee_Address,
                Gender: $scope.employee_Gender,
                EmployeeAlias: $scope.employee_EmployeeAlias,      
                AdmissionDate: new Date(),   
                CareerId: $scope.employee_Career.CareerId,
                RoleId: $scope.employee_Role.RoleId,
                UserId: $scope.employee_User.UserId
            };

            employeesRepo.insertEmployee(function () {
            }, employee);

            $scope.employeesList.push(employee);
        }
        else {
            employee = {
                EmployeeId: $scope.employee_EmployeeId,
                FirstName: $scope.employee_FirstName,
                LastName: $scope.employee_LastName,
                Email: $scope.employee_Email,
                Birthday: $scope.employee_Birthday,
                Age: $scope.employee_Age,
                Cellphone: $scope.employee_Cellphone,
                HomePhone: $scope.employee_HomePhone,
                Address: $scope.employee_Address,
                Gender: $scope.employee_Gender,
                EmployeeAlias: $scope.employee_EmployeeAlias,
                AdmissionDate: $scope.employee_AdmissionDate,
                CareerId: $scope.employee_Career.CareerId,
                RoleId: $scope.employee_Role.RoleId,
                UserId: $scope.employee_User.UserId
            };

            employeesRepo.updateEmployee(function () {
            }, employee);
        }

        $scope.clearData();
        $scope.employee_refresh();
        $scope.employee_cancelRedirect();
        
    };

    $scope.setEmployeeToDelete = function (index) {
        var id = $scope.employeesList[index].EmployeeId;
        $scope.employeeToDeleteId = id;
        $scope.employeeToDeleteIndex = index;
    };

    $scope.cancelEmployeeDelete = function () {
        $scope.employeeToDeleteId = "";
        $scope.employeeToDeleteIndex = "";
    };

    $scope.removeEmployee = function (index) {
        $scope.employeesList.splice(index, 1);
    };

    $scope.deleteEmployee = function () {
        debugger
        var id = $scope.employeeToDeleteId;
        $scope.removeEmployee($scope.employeeToDeleteIndex);
        employeesRepo.deleteEmployee(function () {
            alert('Employee deleted');
            //removeEmployee(index);
        }, id);
        $scope.cancelEmployeeDelete();
    }
    
}]);