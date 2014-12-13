
//following is our application module.ngGrid is the angular grid that we need to use to display data.
var accionLabControllers = angular.module('clientsRepository', []);
var urlCustomer = 'api/clients/';

//the factory object for the webAPI call.
accionLabControllers.factory('customerRepository',['$http', function ($http) {
    return {
        getCustomers: function () {
            return $http.get(urlCustomer);
        },
        getCities: function (callback) {
            return $http.get('api/Cities');
        },
        getCustomer: function (id) {
            return $http.get(urlCustomer + id);
        },
        getCountries: function (callback) {
            return $http.get('api/Countries');
        },
        getAcademicLevels: function (callback) {
            return $http.get('api/AcademicLevels');
        },
        getCareers: function (callback) {
            return $http.get('api/Careers');
        },
        getEducationTypes: function (callback) {
            return $http.get('api/EducationTypes');
        },
        getLanguages: function (callback) {
            return $http.get('api/Languages');
        },
        getLanguageLevels: function (callback) {
            return $http.get('api/LanguageLevels');
        },
        //method for insert
        InsertCustomer: function (callback, client) {
            
            var newClient = {
                 "Correlative": client.Correlative, "FirstName": client.FirstName,
                "LastName": client.LastName, "Birthday": client.Birthday, "Age": client.Age,
                "Gender": client.Gender, "Email": client.Email, "Neighborhood": client.Neighborhood,
                "CompleteAddress": client.CompleteAddress, "Cellphone": client.Cellphone, "HomePhone": client.HomePhone,
                "Hobby": client.Hobby, "Photo": client.Photo, "CurrentStudies": client.CurrentStudies,
                "WageAspiration": client.WageAspiration, "FacebookEmail": client.FacebookEmail, "BBPin": client.BBPin,
                "Twitter": client.Twitter, "DesiredEmployment": client.DesiredEmployment, "CompaniesWithPreviouslyRequested": client.CompaniesWithPreviouslyRequested,
                "CityId": client.CityId.CityId, "AdvisorId": client.AdvisorId, "CareerId": client.CareerId,
                "AcademicEducations": client.AcademicEducations, "Languages": client.Languages, "KnownPrograms": client.KnownPrograms,
                "WorkExperiences": client.workExperiences, "References": client.workReferences
            };
            return $http.post(urlCustomer, newClient);
        }
            ,
        //method for update
        UpdateCustomer: function (client) {
            var newClient = {
                "ClientId": client.ClientId, "Correlative": client.Correlative, "FirstName": client.FirstName,
                "LastName": client.LastName, "Birthday": client.Birthday, "Age": client.Age,
                "Gender": client.Gender, "Email": client.Email, "Neighborhood": client.Neighborhood,
                "CompleteAddress": client.CompleteAddress, "Cellphone": client.Cellphone, "HomePhone": client.HomePhone,
                "Hobby": client.Hobby, "Photo": client.Photo, "CurrentStudies": client.CurrentStudies,
                "WageAspiration": client.WageAspiration, "FacebookEmail": client.FacebookEmail, "BBPin": client.BBPin,
                "Twitter": client.Twitter, "DesiredEmployment": client.DesiredEmployment, "CompaniesWithPreviouslyRequested": client.CompaniesWithPreviouslyRequested,
                "CityId": client.CityId, "AdvisorId": client.AdvisorId,
                "AcademicEducations": client.AcademicEducations, "Languages": client.Languages, "KnownPrograms": client.KnownPrograms,
                "WorkExperiences": client.WorkExperiences, "References": client.References
            };
            return $http.put(urlCustomer + newClient.ClientId, newClient);
        }
        ,
        //method for delete
        DeleteCustomer: function (callback, id) {
            return $http.delete(urlCustomer + '/' + id);
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

