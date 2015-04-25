
//following is our application module.ngGrid is the angular grid that we need to use to display data.
var accionLabControllers = angular.module('clientsRepository', []);
var urlCustomer = 'api/clients/';

//the factory object for the webAPI call.
accionLabControllers.factory('customerRepository', ['$http', "$rootScope", function ($http, $rootScope) {
    var token = $rootScope.userToken;
    return {
        getCustomers: function (employee) {
            var urlCustomersByEmployee;
            if (employee.Role.Alias == "ASREC")
                urlCustomersByEmployee = 'api/clientsbyemployee/' + employee.EmployeeId;
            else
                urlCustomersByEmployee = 'api/clients/';

            return $http.get(urlCustomersByEmployee, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getEnrolledCustomers: function (employee) {
            var urlCustomersByEmployee;
            if (employee.Role.Alias == "ASREC")
                urlCustomersByEmployee = 'api/enrolledclientsbyemployee/' + employee.EmployeeId;
            else
                urlCustomersByEmployee = 'api/enrolledclients/';

            return $http.get(urlCustomersByEmployee, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getTrackingCustomers: function (employee) {
            var urlCustomersByEmployee;
            if (employee.Role.Alias == "ASREC")
                urlCustomersByEmployee = 'api/trackingclientsbyemployee/' + employee.EmployeeId;
            else
                urlCustomersByEmployee = 'api/trackingclients/';

            return $http.get(urlCustomersByEmployee, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getEmployees: function () {
            return $http.get('api/Employees', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getCities: function (callback) {
            return $http.get('api/Cities', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getCustomer: function (id) {
            return $http.get(urlCustomer + id, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        exportCustomer: function (id) {
            return $http.get('Clients/ExportClient/' + id, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        exportCustomers: function (filters) {
            filters.Clients = null;
            return $http.post('api/exportclients/', filters, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        exportCustomersTracking: function (filters) {
            filters.Clients = null;
            return $http.post('api/exportclientstracking', filters, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        searchCustomers: function (searchTerm, searchField, limit) {
            var searchUrl = 'api/SearchClients/' + searchTerm + '/' + ((searchField) ? searchField : null) + '/' + ((limit) ? limit : 0);
            return $http.get(searchUrl, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getCountries: function (callback) {
            return $http.get('api/Countries', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getAcademicLevels: function (callback) {
            return $http.get('api/AcademicLevels', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getCareers: function () {
            return $http.get('api/Careers', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getEducationTypes: function (callback) {
            return $http.get('api/EducationTypes', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getCompanies: function (callback) {
            return $http.get('api/Companies', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getLanguages: function (callback) {
            return $http.get('api/Languages', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getLanguageLevels: function (callback) {
            return $http.get('api/LanguageLevels', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getTrackingTypes: function () {
            return $http.get('api/trackingtypes', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        getStates: function(){
            return $http.get('api/States', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
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
            return $http.post(urlCustomer, newClient, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        Refresh: function () {
            return $http.post('api/refreshclients', { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } })
        },
        InsertTracking: function (tracking) {
            return $http.post('api/TrackingDetails', tracking, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } })
        },
        //method for update
        UpdateCustomer: function (client) {
            
            return $http.put(urlCustomer + client.ClientId, client, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },

        ChangeValues: function (client) {
            return $http({
                method: "put",
                url: "/api/changeclientvalues/" + client.ClientId,
                data: client,
                headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' }
            });
            //return $http.put('api/changestatus/' + client.ClientId, client);
        },
        //updateTracking: function (Tracking) {
          //  return $http.put(urlCustomer + Client.ClientId, Client);
        //},
        UpdateTracking: function (client) {
            return $http.put('api/Trackings/' + client.Trackings[0].TrackingId, client, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        inscribeCustomer: function (client) {
            var newClient = angular.copy(client);
            //newClient.StateId = 2;
            newClient.Trackings[0].StateId = newClient.StateId;
            newClient.Trackings[0].TrackingTypeId = 2;
            newClient.IsStudying = (client.IsStudying == "1");
            return $http.put("api/changeclientvalues/" + newClient.ClientId, newClient, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        DeleteCustomer: function (callback, id) {
            return $http.delete(urlCustomer + '/' + id, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        },
        //method for delete
        DeleteTracking: function (id) {
            return $http.delete('api/TrackingDetails' + '/' + id, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        }
        ,
        findClientByIdentityNumber: function (id) {
            return $http.get('api/FindClientByIdentityNumber' + '/' + id, { headers: { 'Authorization': 'Bearer ' + token, 'Content-Type': 'application/json; charset=utf-8' } });
        }

    }
}]);

