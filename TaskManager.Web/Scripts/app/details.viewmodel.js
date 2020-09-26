function DetailsViewModel(app, dataModel) {
    var self = this;


    //#region Data

    self.ticket = ko.observable();

    self.lookups = ko.observable();


    self.errors = ko.observable();

    self.errors.subscribe(scrollTop);


    self.info = ko.observable();

    self.info.subscribe(scrollTop);

    //#endregion


    //#region Operations

    self.refreshTicket = function (data) {
        data.comments.sortBy(sortCommentsFunction);

        fromISODateToLocaleStringConverter(data, "dateCreated", "executionDate", "receiptDate", "dateModified");

        fromISODateToLocaleStringConverter(data.comments, "dateCreated");

        self.ticket(data);


        self.textEditor = new InitializeTextEditor("commentTaskEditor", "en");

        SetHash("editTicketForm", null, "editTicket",
            function () {
                $("#editTicketForm").submit();

                return false;
            });


        RefreshUnobtrusiveValidator("editTicketForm");
    }

    self.getTicket = function (id) {
        SendRequest(requestType.GET, app.dataModel.getTicketUrl + id, null, null, null, function (data) {
            self.refreshTicket(data);
        })
    }

    self.editTicket = function (data) {
        SendRequest(requestType.PUT, app.dataModel.editTicketUrl + self.ticket().id, data, null, null, function (response) {
            self.refreshTicket(response);

            self.info("Pomyślnie zapisano");
        },
        function (errors) {
            if (errors.modelState) {
                DisplayModelStateErrors(errors.modelState, self.errors);
            }
            else {
                self.errors(errors.message ||  errors)
            }
        });
    }

    function sortCommentsFunction(c) {
        return -new Date(c.dateCreated);
    }

    //#endregion


    //#region Initialise

    self.SetHintButtons = function () {

    }

    self.InitiateElements = function () {
        $("#editTicketForm").submit(function () {
            const data = JSON.stringify($(this).serializeObject());

            self.editTicket(data);

            return false;
        });
    }

    self.InitiateLookups = function () {
        SendRequest(requestType.GET, app.dataModel.lookupsUrl, null, null, null, function (data) {
            self.lookups(data);

            const ticket = self.ticket();

            if (ticket) {
                $(".selectpicker").selectpicker('refresh');

                //RefreshUnobtrusiveValidator("editTicketForm");
            }
        })
    }

    self.InitiateUI = function () {
        self.ticket(null);
        self.errors(null);
        self.info(null);

        self.SetHintButtons();

        self.InitiateElements();
    }

    self.OnLoad = new OnLoad(function () {
        self.InitiateLookups();
    });

    self.Initialize = function (id) {
        self.InitiateUI();

        self.OnLoad.Execute();

        self.getTicket(id);
    }

    //#endregion


    Sammy(function () {
        this.get('#details/:taskId', function () {
            app.view(self);

            self.Initialize(this.params.taskId);

            ActivateFolder("details");
        });
    });

    return self;
}

function DetailsNavigator(app, dataModel) {
    return function (taskId) {
        window.location.hash = "details/" + taskId;
    }
}

app.addViewModel({
    name: "Details",
    bindingMemberName: "details",
    factory: DetailsViewModel,
    navigatorFactory: DetailsNavigator
});