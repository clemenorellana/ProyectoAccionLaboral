var accionLabControllers = angular.module('companiesRepository', []);


accionLabControllers.factory('companiesRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var urlCompany = 'api/Companies';
    return {
        getCompanyList: function (callback) {
            return $http.get(urlCompany, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
        ,
        //insert method
        insertCompany: function (callback, company) {
            var newCompany = {
                "Name": company.CompanyName,
                "Area": company.CompanyArea,
                "DateCreated": company.DateCreated,
                "ContactsByCompany": company.ContactsByCompany
                //"VacantsByCompany": company.VacantsByCompany
            };
            return $http.post(urlCompany, newCompany, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } })
        },
        //update method
        getCompany: function (id) {
            return $http.get(urlCompany + "/" + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        updateCompany: function (callback, company) {
            debugger
            var newCompany = {
                "CompanyId": company.CompanyId,
                "Name": company.CompanyName,
                "Area": company.CompanyArea,
                "DateCreated": company.DateCreated,
                "ContactsByCompany": company.ContactsByCompany
                //"VacantsByCompany": company.VacantsByCompany
            };
            return $http.put(urlCompany + '/' + newCompany.CompanyId, newCompany, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //delete method
        deleteCompany: function (callback, id) {
            return $http.delete(urlCompany + '/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
        ,
        //Reports
        getCompaniesDataReport: function (id) {
            return $http.post('api/companiesdatareport/', id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
        ,
        exportCompaniesReport: function (filters) {
            return $http.post('api/exportcompaniesreport/', filters, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
    }
}]);