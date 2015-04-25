'use strict';


var appServices = angular.module('authService', []);

appServices.factory('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: "",
        token:"",
        employee: {}
    };

    var _getToken = function (loginData) {
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
        $http.post('/token', data, { headers: {'Content-Type': 'application/x-www-form-urlencoded; Charset=utf-8' } }).success(function (response) {


            _authentication.token = response.access_token;

            deferred.resolve(response);

        }).error(function (err, status) {
            deferred.reject(err);
        });

        return _authentication.token;
    }

    var _login = function (loginData) {
        //_getToken(loginData);
        var dataToken = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;
        var data = "grant_type=password&username=" + loginData.userName + "&PasswordHash=" + loginData.password;

        var deferred = $q.defer();

        var url = 'api/Users';
        $http.post('/token', dataToken, { headers: { 'Content-Type': 'application/x-www-form-urlencoded; Charset=utf-8' } }).success(function (responseToken) {
            _authentication.token = responseToken.access_token;
            $http.post(url + '/Login', data, { headers: { 'Authorization': 'Bearer ' + _authentication.token, 'Content-Type': 'application/x-www-form-urlencoded' } }).success(function (response) {

                localStorageService.set('authorizationData', { token: _authentication.token, userName: loginData.userName, employee: response });
            
                _authentication.isAuth = true;
                _authentication.userName = loginData.userName;
                _authentication.token = _authentication.token;
                _authentication.employee = response;

                deferred.resolve(response);

            }).error(function (err, status) {
                _logOut();
                deferred.reject(err);
            });
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _logOut = function () {

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.token = "";
        _authentication.employee = {};

    };

    var _fillAuthData = function () {
        
        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.token = authData.token;
            _authentication.employee = authData.employee;
        }

    }


    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.accessToken = _getToken;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    authServiceFactory.updateAuthenticationData = function (employee) {
        
        _fillAuthData();
        var userName = _authentication.userName;
        var token = _authentication.token;

        localStorageService.set('authorizationData', {  userName: userName, employee: employee, token: token });
        
        _authentication.isAuth = true;
        _authentication.userName = userName;
        _authentication.token = token;
        _authentication.employee = employee;


    }

    return authServiceFactory;
}]);