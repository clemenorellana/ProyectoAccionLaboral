var accionLabControllers = angular.module('contractTemplatesRepository', []);
var url = 'api/ContractTemplates';

accionLabControllers.factory('contractTemplatesRepo', ['$http', function ($http) {
    return {
        getContractTemplateList: function (callback) {
            return $http.get(url);
        },
        //method for insert
        insertContractTemplate: function (callback, contract) {
            var contract = {
                "Name": contract.Name,
                "Description": contract.Description,
                "Active": 0
            };
            return $http.post(url, contract);
        },
        getContractTemplate: function (callback, id) {
            return $http.get(url + "/Edit/" + id);
        },
        //method for update
        updateContractTemplate: function (callback, contract) {
            var contract = {
                "ContractTemplateId": contract.ContractTemplateId,
                "Name": contract.Name,
                "Description": contract.Description,
                "Active": contract.Active
            };
            return $http.put(url + '/' + contract.ContractTemplateId, contract);
        },
        //method for delete
        deleteContractTemplate: function (callback, id) {
            return $http.delete(url + '/' + id);
        }
    }
}]);