var customersApp = angular.module("clientsController", []);

customersApp.controller('CustomerController', ['$scope', 'customerRepository', function ($scope, customerRepository) {
   /* getCustomers();
    function getCustomers() {
        customerRepository.getCustomers(function(results) {
            $scope.customerData = results;
        });
    }*/

    customerRepository.getCustomers().success(function (data) {
        $scope.customerData = data;
        $scope.totalServerItems = data.totalItems;
        $scope.items = data.items;
        $scope.loading = false;
    })
            .error(function (data) {
                $scope.error = "An Error has occured while loading posts! " + data.ExceptionMessage;
                $scope.loading = false;
            });

    $scope.setScope = function (obj, action) {

        $scope.action = action;
        $scope.New = obj;
    }

    // filter
    $scope.filterOptions = {
        filterText: "",
        useExternalFilter: true
    };

    // paging
    $scope.totalServerItems = 0;
    $scope.pagingOptions = {
        pageSizes: [25, 50, 100],
        pageSize: 25,
        currentPage: 1
    };

    // sort
    $scope.sortOptions = {
        fields: ["name"],
        directions: ["ASC"]
    };

    $scope.gridOptions = {
        data: 'customerData',
        columnDefs: [{ field: 'clientId', displayName: 'ID', width: '5%' },
            { field: 'FirstName', displayName: 'Nombre', width: '15%' },
            { field: 'LastName', displayName: 'Apellido', width: '15%' },
            { field: 'Age', displayName: 'Edad', width: '5%' },
            { field: 'Email', displayName: 'Email', width: '15%' },
            { field: 'CompleteAddress', displayName: 'CompleteAddress', width: '15%' },
            { field: 'Cellphone', displayName: 'Celular', width: '15%' },
            { displayName: 'Options', cellTemplate: '<input type="button" ng-click="setScope(row.entity,\'edit\')" name="edit"  value="Edit">&nbsp;<input type="button" ng-click="DeleteUser(row.entity.id)"  name="delete"  value="Delete">', width: '25%' }],
        enablePaging: true,
        enablePinning: true,
        pagingOptions: $scope.pagingOptions,        
        filterOptions: $scope.filterOptions,
        keepLastSelected: true,
        multiSelect: false,
        showColumnMenu: true,
        showFilter: true,
        showGroupPanel: true,
        showFooter: true,
        sortInfo: $scope.sortOptions,
        totalServerItems: "totalServerItems",
        useExternalSorting: true,
        i18n: "en"
    };

    $scope.refresh = function() {
        setTimeout(function () {
            var sb = [];
            for (var i = 0; i < $scope.sortOptions.fields.length; i++) {
                sb.push($scope.sortOptions.directions[i] === "DESC" ? "-" : "+");
                sb.push($scope.sortOptions.fields[i]);
            }

            var p = {
                name: $scope.filterOptions.filterText,
                pageNumber: $scope.pagingOptions.currentPage,
                pageSize: $scope.pagingOptions.pageSize,
                sortInfo: sb.join("")
            };
        }, 100);
    };

    // watches
    $scope.$watch('pagingOptions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.refresh();
        }
    }, true);

    $scope.$watch('filterOptions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.refresh();
        }
    }, true);

    $scope.$watch('sortOptions', function (newVal, oldVal) {
        if (newVal !== oldVal) {
            $scope.refresh();
        }
    }, true);

    $scope.refresh();


    $scope.update = function () {
        debugger;
        if ($scope.action == 'edit') {
            customerRepository.UpdateCustomer(function () {
                $scope.status = 'customer updated successfully';
                alert('customer updated successfully');
                getCustomers();
            }, $scope.New);
            $scope.action = '';
        }
        else {
            customerRepository.InsertCustomer(function () {
                alert('customer inserted successfully');
                getCustomers();
            }, $scope.New);

        }


    }

    $scope.DeleteCustomer = function (id) {
        customerRepository.DeleteCustomer(function () {
            alert('Customer deleted');
            getCustomers();
        }, id);

    }

}]);