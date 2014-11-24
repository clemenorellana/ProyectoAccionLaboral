var accionLabControllers = angular.module("accionLabControllers", []);

accionLabControllers.controller('mainController', ['$scope', 
function ($scope) {
    $scope.title = "Pagina principal";
}]).controller("registerController", ['$scope'
    , function ($scope) {
        $scope.title = "Registrar Cliente";
}]
);