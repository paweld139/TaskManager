function AddViewModel(app, dataModel) {
    var self = this;


    //#region Data

    self.lookups = ko.observable();


    self.selectedRepresentative = ko.observable();


    self.selectedContrahent = ko.observable();

    self.selectedContrahent.subscribe(function () {
        self.selectedRepresentative(undefined);
    })

    self.representatives = ko.computed(function () {
        if (!self.lookups()) {
            return [];
        }

        return ko.utils.arrayFilter(self.lookups().representatives, function (rec) {
            return self.selectedContrahent() === rec.contrahentId;
        })
    });

    self.errors = ko.observable();

    self.errors.subscribe(function (value) {
        if (value) {
            scrollTop();
        }
    });

    //#endregion


    //#region Operations


    self.saveTicket = function (data) {
        SendRequest(requestType.POST, app.dataModel.saveTicketUrl, data, null, null, function () {
            app.navigateToHome();
        },
            function (errors) {
                if (errors && errors.modelState) {
                    DisplayModelStateErrors(errors.modelState, self.errors);
                }
                else if(errors) {
                    self.errors(errors.message || errors)
                }
            });
    }

    //#endregion


    //#region Initialise

    self.InitiateLookups = function () {
        SendRequest(requestType.GET, app.dataModel.lookupsUrl, null, null, null, function (data) {
            self.lookups(data); //W tym momencie pojawia się element w DOM zawierający selecty do wybierania kontrahenta i przedstawiciela

            self.selectedContrahent(UserData.contrahentId());
            self.selectedRepresentative(UserData.employeeId());

            self.RefreshValidator();
        })
    }

    self.OnLoad = new OnLoad(function () {
        self.InitiateLookups();
    });

    self.RefreshValidator = function () {
        if (self.lookups()) { //Jeśli nie ma jeszcze lookups, to i tak zostaną pobrane później i odświeżą walidator.
            RefreshUnobtrusiveValidator("addTicketForm");
        }
    }

    self.InitiateElements = function () {
        self.textEditor = new InitializeTextEditor("taskDescriptionEditor");

        SetHash("addTicketForm", null, "#saveTicket",
            function () {
                $("#addTicketForm").submit();

                return false;
            },
            function () {
                self.textEditor.updateElement();
            });

        $("#addTicketForm").submit(function () {
            const data = JSON.stringify($(this).serializeObject());

            self.saveTicket(data);

            return false;
        });
    }

    self.InitiateUI = function () {
        self.RefreshValidator();

        self.InitiateElements();
    }

    self.Initialize = function () {
        self.InitiateUI();

        self.OnLoad.Execute();
    }

    //#endregion


    Sammy(function () {
        this.get('#add', function () {
            app.view(self);

            self.Initialize();

            ActivateFolder("add");
        });
    });

    return self;
}

app.addViewModel({
    name: "Add",
    bindingMemberName: "add",
    factory: AddViewModel
});