var accionLabControllers = angular.module('contractTemplatesRepository', []);


accionLabControllers.factory('contractTemplatesRepo', ['$http', '$rootScope', function ($http, $rootScope) {
    var url = 'api/ContractTemplates';
    return {
        getContractTemplateList: function (callback) {
            return $http.get(url, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for insert
        insertContractTemplate: function (callback, contract) {
            var contract = {
                "Name": contract.Name,
                "Description": contract.Description,
                "Active": 0
            };
            return $http.post(url, contract, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getContractTemplate: function (id) {
            return $http.get(url + "/" + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for update
        updateContractTemplate: function (callback, contract) {
            var contract = {
                "ContractTemplateId": contract.ContractTemplateId,
                "Name": contract.Name,
                "Description": contract.Description,
                "Active": contract.Active
            };
            return $http.put(url + '/' + contract.ContractTemplateId, contract, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        deleteContractTemplate: function (callback, id) {
            return $http.delete(url + '/' + id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        exportContractReport: function (id) {
            return $http.post('api/ExportContractReport', id, { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getActiveContractTemplateList: function (callback) {
            return $http.get('api/ContractTemplatesActive', { headers: { 'Authorization': 'Bearer ' + $rootScope.userToken, 'Content-Type': 'application/json; charset=utf-8' } });
        }
    }
}]);