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
    $scope.employeeId = $routeParams.id;

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
            var employeeToEdit = data;
            $scope.Employee_EmployeeId = employeeToEdit.EmployeeId;
            $scope.Employee_Name = employeeToEdit.Name;
            $scope.Employee_Description = employeeToEdit.Description;
            $scope.Employee_Active = employeeToEdit.Active;
        });
    }

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

    employeesRepo.getRoles().success(function(data){
        $scope.rolesList = data;
    });

    $scope.employee_addNewRedirect = function () {
        window.location = "#/Employees/Create";
    }

    $scope.employee_editRedirect = function (index) {
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
        //sionDate: new Date(),
        $scope.employee_Career.CareerId = "";
        $scope.employee_Role.RoleId = "";
        $scope.employee_User.UserId = "";
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
                AdmissionDate: new Date(),
                CareerId: $scope.employee_Career.CareerId,
                RoleId: $scope.employee_Role.RoleId,
                UserId: $scope.employee_User.UserId
            };

            employeesRepo.updateEmployee(function () {
            }, employee);
        }

        $scope.clearData();
        $scope.employee_cancelRedirect();
        $scope.employee_refresh();
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