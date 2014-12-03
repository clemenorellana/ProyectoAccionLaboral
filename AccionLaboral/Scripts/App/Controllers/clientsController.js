var customersApp = angular.module("clientsController", []);

customersApp.controller('CustomerController', ['$scope', 'customerRepository', function ($scope, customerRepository) {
    
    $scope.handleFileSelectAdd = function (evt) {
        debugger;
        var f = evt.target.files[0];
        var reader = new FileReader();
        reader.onload = (function (theFile) {
            return function (e) {
                var filePayload = e.target.result;
                $scope.episodeImgData = e.target.result;
                document.getElementById('imagen').src = $scope.episodeImgData;
            };
        })(f);
        reader.readAsDataURL(f);
    };
    
    $scope.academicEducations = [], $scope.knownLaguages = [], $scope.knownPrograms = [], $scope.personalReferences = [], $scope.workReferences = [], $scope.workExperiences=[];

    var imageElement = document.getElementById('exampleInputFile');
    if(imageElement)
    imageElement.addEventListener('change', $scope.handleFileSelectAdd, false);

    $scope.addAcademicEducation = function() {
        var education = {
            TrainingName: $scope.TrainingName,
            InstitutionName: $scope.InstitutionName,
            AcademicLevel: $scope.AcademicLevel,
            CareerId: $scope.CareerId,
            EducationTypeId: $scope.EducationTypeId,
            City: $scope.City,
            Year: $scope.Year,
            Country: $scope.Country
        };
        $scope.academicEducations.push(education);
        $scope.TrainingName = "",
            $scope.InstitutionName = "",
            $scope.AcademicLevel = "",
            $scope.City = "",
            $scope.Year = "",
            $scope.Country = "";
    };

    $scope.removeAcademicEducation = function (index) {
        $scope.academicEducations.splice(index, 1);
    };
    
    $scope.removeKnowLanguage = function (index) {
        $scope.knownLaguages.splice(index, 1);
    };

    $scope.removeKnowProgram = function (index) {
        $scope.knownPrograms.splice(index, 1);
    }; 

    $scope.removeWorkExperience = function (index) {
        $scope.workExperiences.splice(index, 1);
    };

    $scope.removeWorkReference = function (index) {
        $scope.workReferences.splice(index, 1);
    };

    $scope.removePersonalReference = function (index) {
        $scope.personalReferences.splice(index, 1);
    };

    $scope.addKnownLaguage = function() {
        var lang = {
            Percentage: $scope.Percentage,
            Language: $scope.Language,
            LanguageLevel: $scope.LanguageLevel
        };

        $scope.knownLaguages.push(lang);

        $scope.Percentage = "",
            $scope.Language = "",
            $scope.LanguageLevel = "";
    };

    $scope.addKnownProgram = function () {
        var program = {
            Name: $scope.NameProgram
        };
        $scope.knownPrograms.push(program);

        $scope.NameProgram = "";
    }

    $scope.addWorkExperience = function() {
        var experience = {
            CompanyName: $scope.CompanyName,
            CompanyArea: $scope.CompanyArea,
            Charge: $scope.Charge,
            StartDate: $scope.StartDate,
            EndDate: $scope.EndDate,
            Achievements: $scope.Achievements,
            WorkCity: $scope.WorkCity,
            WorkCountry: $scope.WorkCountry
        };

        $scope.workExperiences.push(experience);
        $scope.CompanyName = "";
        $scope.CompanyArea = "";
        $scope.Charge = "";
        $scope.StartDate = "";
        $scope.EndDate = "";
        $scope.Achievements = "";
        $scope.WorkCountry = "";
        $scope.WorkCity = "";
    };

    $scope.addWorkReference = function() {
        var wReference = {
            FirstName: $scope.FirstNameWRef,
            LastName: $scope.LastNameWRef,
            Charge: $scope.ChargeWRef,
            CellPhone: $scope.CellPhoneWRef,
            Email: $scope.EmailWRef,
            CompanyName: $scope.CompanyNameWRef,
            Relationship: $scope.RelationshipWRef,
            City: $scope.CityWRef,
            Country: $scope.CountryWRef
        };
        $scope.workReferences.push(wReference);

        $scope.FirstNameWRef = "";
        $scope.LastNameWRef = "";
        $scope.ChargeWRef = "";
        $scope.CellPhoneWRef = "";
        $scope.EmailWRef = "";
        $scope.CompanyNameWRef = "";
        $scope.RelationshipWRef = "";
        $scope.CityWRef = "";
        $scope.CountryWRef = "";
    };

    $scope.addPersonalReference = function () {
        var pReference = {
            FirstName: $scope.FirstNamePRef,
            LastName: $scope.LastNamePRef,
            Charge: $scope.ChargePRef,
            CellPhone: $scope.CellPhonePRef,
            Email: $scope.EmailPRef,
            CompanyName: $scope.CompanyNamePRef,
            Relationship: $scope.RelationshipPRef,
            City: $scope.CityPRef
        };
        $scope.personalReferences.push(pReference);

        $scope.FirstNamePRef = "";
        $scope.LastNamePRef = "";
        $scope.ChargePRef = "";
        $scope.CellPhonePRef = "";
        $scope.EmailPRef = "";
        $scope.CompanyNamePRef = "";
        $scope.RelationshipPRef = "";
        $scope.CityPRef = "";
        $scope.CountryPRef = "";
    };


    customerRepository.getCities().success(function (data) {
        $scope.Cities = data;
    });

    customerRepository.getCountries().success(function(data) {
        $scope.Countries = data;
    });

    customerRepository.getAcademicLevels().success(function (data) {
        
        $scope.AcademicLevels = data;
    });

    customerRepository.getEducationTypes().success(function (data) {

        $scope.EducationTypes = data;
    });

    customerRepository.getLanguages().success(function (data) {

        $scope.Languages = data;
    });

    customerRepository.getLanguageLevels().success(function (data) {

        $scope.LanguageLevels = data;
    });

    $scope.assignCareers = function () {
        if ($scope.AcademicLevel)
        $scope.Careers = $scope.AcademicLevel.Careers;
    }

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
        
        if ($scope.episodeImgData)
            $scope.New.Photo = $scope.episodeImgData.replace(/data:image\/jpeg;base64,/g, '');

        $scope.New.AcademicEducations = [];
        if ($scope.academicEducations.length>0) {
            
            for (var i = 0; i < $scope.academicEducations.length; i++) {
                var academic = {
                    Year: $scope.academicEducations[i].Year,
                    CountryId: $scope.academicEducations[i].Country.CountryId,
                    CityId: $scope.academicEducations[i].City.CityId,
                    AcademicLevelId: $scope.academicEducations[i].AcademicLevel.AcademicLevelId,
                    TrainingName: $scope.academicEducations[i].TrainingName,
                    InstitutionName: $scope.academicEducations[i].InstitutionName,
                    CareerId: $scope.academicEducations[i].CareerId,
                    EducationTypeId: $scope.academicEducations[i].EducationTypeId
                };
                $scope.New.AcademicEducations.push(academic);
            }
        }
        
        if ($scope.knownLaguages.length > 0) {
            $scope.New.Languages = [];
            for (var j = 0; j < $scope.knownLaguages.length; j++) {
                var language = {
                    LanguageId: $scope.knownLaguages[j].Language.LanguageId,
                    LanguageLevelId: $scope.knownLaguages[j].LanguageLevel.LanguageLevelId,
                    Percentage: $scope.knownLaguages[j].Percentage
                };
                $scope.New.Languages.push(language);
            }
        }
        if ($scope.knownPrograms.length > 0) {
            $scope.New.KnownPrograms = [];
            for (var j = 0; j < $scope.knownPrograms.length; j++) {
                    var program = {
                        Name: $scope.knownPrograms[j].Name
                    };
                    $scope.New.KnownPrograms.push(program);
                }
        }
        
        if ($scope.workExperiences.length > 0) {
            $scope.New.workExperiences = [];
            for (var k = 0; k < $scope.workExperiences.length; k++) {
                var experience = {
                    CompanyName: $scope.workExperiences[k].CompanyName,
                    CompanyArea: $scope.workExperiences[k].CompanyArea,
                    Charge: $scope.workExperiences[k].Charge,
                    StartDate: $scope.workExperiences[k].StartDate,
                    EndDate: $scope.workExperiences[k].EndDate,
                    Achievements: $scope.workExperiences[k].Achievements,
                    CityId: $scope.workExperiences[k].WorkCity.CityId
                };
                $scope.New.workExperiences.push(experience);
            }
        }
        
        if ($scope.workReferences.length > 0) {
            $scope.New.workReferences = [];
                for (var l = 0; l < $scope.workReferences.length; l++) {
                    var wExperience = {
                        FirstName: $scope.workReferences[l].FirstName,
                        LastName: $scope.workReferences[l].LastName,
                        Charge: $scope.workReferences[l].Charge,
                        Cellphone: $scope.workReferences[l].CellPhone,
                        Email: $scope.workReferences[l].Email,
                        CompanyName: $scope.workReferences[l].CompanyName,
                        Relationship: $scope.workReferences[l].Relationship,
                        ReferenceTypeId: 1,
                        CityId: $scope.workReferences[l].City.CityId
                    }

                    $scope.New.workReferences.push(wExperience);
                }
            }
            
        if ($scope.personalReferences.length > 0) {
            $scope.New.personalReferences = [];
                for (var l = 0; l < $scope.personalReferences.length; l++) {
                    var pExperience = {
                        FirstName: $scope.personalReferences[l].FirstName,
                        LastName: $scope.personalReferences[l].LastName,
                        Charge: $scope.personalReferences[l].Charge,
                        Cellphone: $scope.personalReferences[l].CellPhone,
                        Email: $scope.personalReferences[l].Email,
                        CompanyName: $scope.personalReferences[l].CompanyName,
                        Relationship: $scope.personalReferences[l].Relationship,
                        ReferenceTypeId: 2,
                        CityId: $scope.personalReferences[l].City.CityId
                    }

                    $scope.New.workReferences.push(pExperience);
                }
            }
            
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