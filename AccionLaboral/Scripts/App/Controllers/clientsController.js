var customersApp = angular.module("clientsController", []);

customersApp.controller('newCustomerController', ['$scope', 'customerRepository', function ($scope, customerRepository) {
    getCustomers();
    function getCustomers() {
        customerRepository.getCustomers(function(results) {
            $scope.customerData = results;
        });
    }

    $scope.setScope = function (obj, action) {

        $scope.action = action;
        $scope.New = obj;
    }

    $scope.gridOptions = {
        data: 'customerData',
        showGroupPanel: true,
        columnDefs: [{ field: 'name', displayName: 'Name', width: '15%' },
            { field: 'city', displayName: 'City', width: '15%' },
            { field: 'address', displayName: 'Address', width: '15%' },
            { field: 'contactNo', displayName: 'Contact No', width: '15%' },
            { field: 'emailId', displayName: 'Email Id', width: '15%' },
            { displayName: 'Options', cellTemplate: '<input type="button" ng-click="setScope(row.entity,\'edit\')" name="edit"  value="Edit">&nbsp;<input type="button" ng-click="DeleteUser(row.entity.id)"  name="delete"  value="Delete">', width: '25%' }
        ]
    };


    $scope.update = function () {
        if ($scope.action == 'edit') {
            customerRepository.updateclient(function () {
                $scope.status = 'customer updated successfully';
                alert('customer updated successfully');
                getCustomers();
            }, $scope.New);
            $scope.action = '';
        }
        else {
            customerRepository.insertclient(function () {
                alert('customer inserted successfully');
                getCustomers();
            }, $scope.New);

        }


    }

    $scope.Deleteclient = function (id) {
        customerRepository.deleteUser(function () {
            alert('Customer deleted');
            getCustomers();
        }, id);

    }

}]);