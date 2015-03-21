var accionLabControllers = angular.module('companiesRepository', []);


accionLabControllers.factory('companiesRepo', ['$http', function ($http) {
    var urlCompany = 'api/Companies';
    return {
        getCompanyList: function (callback) {
            return $http.get(urlCompany);
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
            return $http.post(urlCompany, newCompany)
        },
        //update method
        getCompany: function (id) {
            return $http.get(urlCompany + "/" + id);
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
            return $http.put(urlCompany + '/' + newCompany.CompanyId, newCompany);
        },
        //delete method
        deleteCompany: function (callback, id) {
            return $http.delete(urlCompany + '/' + id);
        }
        ,
        //Reports
        //New Companies
        getNewCompaniesReport: function (callback, startDate, endDate) {
            var date = {
                StartDate: startDate,
                EndDate: endDate
            };

            debugger;
            return $http.get('api/NewCompaniesReport');
        }
        

    }
}]);