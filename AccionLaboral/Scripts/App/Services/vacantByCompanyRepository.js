var accionLabControllers = angular.module('vacantByCompanyRepository', []);


accionLabControllers.factory('vacantByCompanyRepo', ['$http', function ($http) {
    var urlVacants = 'api/VacantsByCompany';
    return {
        getVacantList: function (callback) {
            return $http.get(urlVacants);
        },
        getCompanyList: function (callback) {
            return $http.get('api/Companies');
        },
        getVacantLevelList: function (callback) {
            return $http.get('api/VacantLevels');
        },
        getAcademicLevelList: function (callback) {
            return $http.get('api/AcademicLevels');
        },
        getCareerList: function (callback) {
            return $http.get('api/Careers');
        },
        getCityList: function (callback) {
            return $http.get('api/Cities');
        },
        //method for insert
        insertVacant: function (callback, vacant) {            
            var newVacant = {
                "VacantName": vacant.VacantName,
                "AcademicLevel": vacant.AcademicLevel,
                "Active": vacant.Active,
                "Career": vacant.Career,
                "ChargeDescription": vacant.ChargeDescription,
                "City": vacant.City,
                "Company": vacant.Company,
                "CoverdDate": new Date(vacant.CoverdDate),
                "EndAge": vacant.EndAge,
                "Gender": vacant.Gender,
                "Quantity": vacant.Quantity,
                "RequestDate": new Date(vacant.RequestDate),
                "Requirements": vacant.Requirements,
                "StartAge": vacant.StartAge,
                "VacantLevel": vacant.VacantLevel
            };
            return $http.post(urlVacants, newVacant);
        },
        //update method
        getVacant: function (id) {
            return $http.get(urlVacants + "/" + id);
        },
        updateVacant: function (callback, vacant) {
            debugger
            var newVacant = {
                "VacantByCompanyId": vacant.VacantByCompanyId,
                "VacantName": vacant.VacantName,
                "AcademicLevel": vacant.AcademicLevel,
                "Active": vacant.Active,
                "Career": vacant.Career,
                "ChargeDescription": vacant.ChargeDescription,
                "City": vacant.City,
                "Company": vacant.Company,
                "CoverdDate": new Date(vacant.CoverdDate),
                "EndAge": vacant.EndAge,
                "Gender": vacant.Gender,
                "Quantity": vacant.Quantity,
                "RequestDate": new Date(vacant.RequestDate),
                "Requirements": vacant.Requirements,
                "StartAge": vacant.StartAge,
                "VacantLevel": vacant.VacantLevel
            };
            return $http.put(urlVacants + '/' + newVacant.VacantByCompanyId, newVacant);
        },
        //method for delete
        deleteVacant: function (callback, id) {
            return $http.delete(urlVacants + '/' + id);
        }
    }
}]);