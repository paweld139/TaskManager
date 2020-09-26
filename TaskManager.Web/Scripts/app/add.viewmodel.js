function AddViewModel(app, dataModel) {
    var self = this;


    //#region Data

    self.lookups = ko.observable();


    self.selectedContrahent = ko.observable();

    self.selectedRepresentative = ko.observable();


    self.selectedContrahent.subscribe(function () {
        self.selectedRepresentative(undefined);
        //$("#ticketRepresentative").selectpicker("refresh");
    })

    self.representatives = ko.computed(function () {
        return ko.utils.arrayFilter(self.lookups()?.representatives, function (rec) {
            return self.selectedContrahent() === rec.contrahentId;
        })
    });

    //self.representatives.subscribe(function () {
    //    $("#ticketRepresentative").selectpicker("refresh");
    //})


    self.errors = ko.observable();

    self.errors.subscribe(scrollTop);

    //#endregion


    //#region Operations


    self.saveTicket = function (data) {
        SendRequest(requestType.POST, app.dataModel.saveTicketUrl, data, null, null, function () {
            GoToFolder("home");
        },
            function (errors) {
                if (errors.modelState) {
                    DisplayModelStateErrors(errors.modelState, self.errors);
                }
                else {
                    self.errors(errors)
                }
            });
    }

    //#endregion


    //#region Initialise

    self.SetHintButtons = function () {

    }

    self.InitiateElements = function () {
        self.textEditor = new InitializeTextEditor("taskDescriptionEditor", "en");

        SetHash("addTicketForm", null, "saveTicket",
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

    self.InitiateLookups = function () {
        SendRequest(requestType.GET, app.dataModel.lookupsUrl, null, null, null, function (data) {
            self.lookups(data);

            self.selectedContrahent(UserData.contrahentId());
            self.selectedRepresentative(UserData.employeeId());

            self.RefreshDropdowns();
        })
    }

    self.RefreshDropdowns = function () {
        if (self.lookups()) {
            //$("#ticketContrahent").selectpicker("refresh");
            //$("#ticketRepresentative").selectpicker("refresh");
            //$("#tickettype").selectpicker("refresh");
            //$("#ticketpriority").selectpicker("refresh");



            //$("#ticketContrahent").selectpicker("refresh");
            //$("#ticketRepresentative").selectpicker("refresh");

            RefreshUnobtrusiveValidator("addTicketForm");
        }
    }

    self.InitiateUI = function () {
        self.RefreshDropdowns();

        self.SetHintButtons();

        self.InitiateElements();
    }

    self.OnLoad = new OnLoad(function () {
        self.InitiateLookups();
    });

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