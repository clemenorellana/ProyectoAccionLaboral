'use strict';


var appServices = angular.module('authService', []);

appServices.factory('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: "",
        employee: {}
    };


    var _login = function (loginData) {
    
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        var url = 'api/Users';

        $http.post(url + '/Login', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

            localStorageService.set('authorizationData', { token: response.access_token, userName: loginData.userName, employee: response });
            
            _authentication.isAuth = true;
            _authentication.userName = loginData.userName;
            _authentication.employee = response;

            deferred.resolve(response);

        }).error(function (err, status) {
            _logOut();
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.employee = {};

    };

    var _fillAuthData = function () {
        debugger
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.employee = authData.employee;
        }

    }


    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    authServiceFactory.updateAuthenticationData = function (employee) {
        debugger

        _fillAuthData();
        var userName = _authentication.userName;

        localStorageService.set('authorizationData', {  userName: userName, employee: employee });
        
        _authentication.isAuth = true;
        _authentication.userName = userName
        _authentication.employee = employee;

    }

    return authServiceFactory;
}]);