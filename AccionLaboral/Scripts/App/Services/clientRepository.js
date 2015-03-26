
//following is our application module.ngGrid is the angular grid that we need to use to display data.
var accionLabControllers = angular.module('clientsRepository', []);
var urlCustomer = 'api/clients/';

//the factory object for the webAPI call.
accionLabControllers.factory('customerRepository',['$http', function ($http) {
    return {
        getCustomers: function (employee) {
            var urlCustomersByEmployee;
            if (employee.Role.Alias == "ASREC")
                urlCustomersByEmployee = 'api/clientsbyemployee/' + employee.EmployeeId;
            else
                urlCustomersByEmployee = 'api/clients/';

            return $http.get(urlCustomersByEmployee);
        },
        getEmployees: function () {
            return $http.get('api/Employees');
        },
        getCities: function (callback) {
            return $http.get('api/Cities');
        },
        getCustomer: function (id) {
            return $http.get(urlCustomer + id);
        },
        exportCustomer: function (id) {
            return $http.get('Clients/ExportClient/' + id);
        },
        exportCustomers: function (filters) {
            var id = new Object();
            id = filters;
            return $http.post('api/exportclients/', id);
            //return $http.get('api/exportclients/id=' + filters);
        },
        exportCustomersTracking: function (filters) {
            return $http.post('api/exportclientstracking', filters);
            //return $http.post('clients/ExportClientsTracking', filters);
        },
        searchCustomers: function (searchTerm, searchField, limit) {
            var searchUrl = 'api/SearchClients/' + searchTerm + '/' + ((searchField) ? searchField : null) + '/' + ((limit) ? limit : 0);
            return $http.get(searchUrl);
        },
        getCountries: function (callback) {
            return $http.get('api/Countries');
        },
        getAcademicLevels: function (callback) {
            return $http.get('api/AcademicLevels');
        },
        getCareers: function () {
            return $http.get('api/Careers');
        },
        getEducationTypes: function (callback) {
            return $http.get('api/EducationTypes');
        },
        getCompanies: function (callback) {
            return $http.get('api/Companies');
        },
        getLanguages: function (callback) {
            return $http.get('api/Languages');
        },
        getLanguageLevels: function (callback) {
            return $http.get('api/LanguageLevels');
        },
        getTrackingTypes: function () {
            return $http.get('api/trackingtypes');
        },
        getStates: function(){
            return $http.get('api/States');
        },

        //method for insert
        InsertCustomer: function (callback, client) {
            var Tracking = [{ TrackingTypeId: 1, StateId: 1 }];

            var newClient = {
                "IdentityNumber": client.IdentityNumber, "Correlative": client.Correlative, "FirstName": client.FirstName,
                "LastName": client.LastName, "Birthday": client.Birthday, "Age": client.Age,
                "Gender": client.Gender, "Email": client.Email, "Neighborhood": client.Neighborhood,
                "CompleteAddress": client.CompleteAddress, "Cellphone": client.Cellphone, "HomePhone": client.HomePhone,
                "Hobby": client.Hobby, "Photo": client.Photo, "CurrentStudies": client.CurrentStudies,
                "WageAspiration": client.WageAspiration, "FacebookEmail": client.FacebookEmail, "BBPin": client.BBPin,
                "Twitter": client.Twitter, "DesiredEmployment": client.DesiredEmployment, "CompaniesWithPreviouslyRequested": client.CompaniesWithPreviouslyRequested,

                "CityId": client.CityId.CityId,
                "EmployeeId": client.AdvisorId,
                "CareerId": client.CareerId,
                "StateId": 1,

                "AcademicEducations": client.AcademicEducations, "Languages": client.Languages, "KnownPrograms": client.KnownPrograms,
                "WorkExperiences": client.workExperiences, "References": client.workReferences, "Trackings": Tracking

            };
            return $http.post(urlCustomer, newClient);
        },
        InsertTracking: function (tracking) {
            return $http.post('api/TrackingDetails', tracking)
        },
        //method for update
        UpdateCustomer: function (client) {
            
            return $http.put(urlCustomer + client.ClientId, client);
        },
        //updateTracking: function (Tracking) {
          //  return $http.put(urlCustomer + Client.ClientId, Client);
        //},
        UpdateTracking: function (client) {
            return $http.put('api/Trackings/' + client.Trackings[0].TrackingId, client);
        },
        inscribeCustomer: function (client) {
            var newClient = angular.copy(client);
            newClient.StateId = 2;
            newClient.Trackings[0].StateId = 2;
            newClient.Trackings[0].TrackingTypeId = 2;
            newClient.IsStudying = (client.IsStudying == "1");
            return $http.put(urlCustomer + newClient.ClientId, newClient);
        },
        //method for delete
        DeleteCustomer: function (callback, id) {
            return $http.delete(urlCustomer + '/' + id);
        },
        //method for delete
        DeleteTracking: function (id) {
            return $http.delete('api/TrackingDetails' + '/' + id);
        }


    }
}]);
/*
//controller   
customersApp.controller('customerCtrl', function ($scope, customerRepository) {
    getCustomers();
    function getCustomers() {
        customerRepository.getCustomers(function (results) {
            $scope.customerData = results;
        })
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
            { displayName: 'Options', cellTemplate: '<input type="button" ng-click="setScope(row.entity,\'edit\')" name="edit"  value="Edit">&nbsp;<input type="button" ng-click="Deleteclient(row.entity.id)"  name="delete"  value="Delete">', width: '25%' }
        ]
    };


    $scope.update = function () {
        if ($scope.action == 'edit') {
            customerRepository.updateclient(function() {
                $scope.status = 'customer updated successfully';
                alert('customer updated successfully');
                getCustomers();
            }, $scope.New);
            $scope.action = '';
        }
        else {
            customerRepository.insertclient(function() {
                alert('customer inserted successfully');
                getCustomers();
            }, $scope.New);

        }


    }

    $scope.Deleteclient = function (id) {
        customerRepository.deleteclient(function() {
            alert('Customer deleted');
            getCustomers();
        }, id);

    }

});
*/

