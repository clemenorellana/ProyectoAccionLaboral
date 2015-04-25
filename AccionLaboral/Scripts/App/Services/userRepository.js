var accionLabControllers = angular.module('usersRepository', []);

accionLabControllers.factory('usersRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var url = 'api/Users';
    var token = $rootScope.userToken;
    return {
        getUsersList: function (callback) {
            return $http.get(url);
        },
        //method for insert
        insertUser: function (callback, user) {
            var user = {
                "UserName": user.UserName,
                "Password": "AccionLaboral_123",//user.UserName,
                "ConfirmPassword": "AccionLaboral_123",
                "Active": true,
                "Busy": false,
            };
            return $http.post("api/account/register", user, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for update
        //updateUser: function (callback, user) {
        //    var user = {
        //        "UserId": user.UserId,
        //        "UserName": user.UserName,
        //        "Password": user.Password
        //    };
        //    return $http.put(url + '/' + user.UserId, user);
        //},
        updateUser: function (callback, user) {
            return $http.put(url + '/' + user.UserId, user);
        },
        //method for delete
        deleteUser: function (callback, id) {
            return $http.delete(url + '/' + id);
        },
        //method for login
        login: function (userName, password) {
            
            var user = {
                "UserName": userName,
                "Password": password
            };
            return $http.post(url + '/Login', user);
        }
        ,
        requestChangePassword: function (userName) {
            var user = {
                "UserName": userName
            };
            return $http.post(url + '/RequestChangePassword', user);
        },
        validateUserName: function (userName) {
            var user = {
                "UserName": userName
            };
            return $http.post(url + '/ValidateUserName', user);
        },
        changePassword: function (userName, password) {
            var user = {
                "UserName": userName,
                "Password": password
            };
            return $http.put(url + '/ChangePassword', user);
        },
        getUser: function (id) {
            return $http.get(url + "/" + id);
        }
    }
}]);