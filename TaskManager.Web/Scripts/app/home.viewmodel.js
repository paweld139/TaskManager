function HomeViewModel(app, dataModel) {
    var self = this;


    //#region Data

    self.tickets = ko.observableArray();

    self.ticketsExist = ko.pureComputed(() => self.filteredRecords().length > 0);

    self.ticketsCount = ko.pureComputed(function () {
        return self.filteredRecords().length;
    });


    self.lookups = ko.observable();

    //#endregion


    //#region Sorting

    self.columns = ko.observableArray([
        { property: "number", type: "string", state: ko.observable("") },
        { property: "subject", type: "string", state: ko.observable("") },
        { property: "executionDate", type: "string", state: ko.observable("") },
        { property: "receiptDate", type: "string", state: ko.observable("") },
        { property: "dateCreated", type: "string", state: ko.observable("") },
        { property: "type", type: "string", state: ko.observable("") },
        { property: "priority", type: "string", state: ko.observable("") },
        { property: "status", type: "string", state: ko.observable("") },
        { property: "contrahent", type: "string", state: ko.observable("") },
        { property: "representative", type: "string", state: ko.observable("") },
        { property: "operator", type: "string", state: ko.observable("") }
    ]);

    self.sortUtil = new SortUtil(self.columns, self.tickets);

    self.InitializeColumns = (function () { //Dzieje się to gdy widok jest niewidoczny, przed applyBindings
        const columns = $("#tickets-table th:not(:first)");

        const columnsArray = self.columns();

        $.each(columns, function (index) {
            columnsArray[index].header = this.innerText;
        })

        columns.remove();
    })();

    //#endregion


    //#region Filtering

    self.searchByNumber = ko.observable('');

    self.searchBySubject = ko.observable('');


    self.selectedContrahents = ko.observableArray([]);

    self.selectedRepresentatives = ko.observableArray([]);

    self.selectedOperators = ko.observableArray([]);

    self.selectedPriorities = ko.observableArray([]);

    self.selectedTypes = ko.observableArray([]);

    self.selectedStatuses = ko.observableArray([]);


    self.showToday = ko.observable(false);

    self.onlyNotBinded = ko.observable(false);


    self.dateFrom = ko.observable(new Date().addMonths(-1).toDate());

    self.dateTo = ko.observable(new Date().toDate())


    self.filteredRecords = ko.computed(function () {

        return ko.utils.arrayFilter(self.tickets(), function (rec) {
            return (
                (self.searchByNumber().length === 0 || rec.number.toLowerCase().indexOf(self.searchByNumber().toLowerCase()) > -1)
                &&
                (self.searchBySubject().length === 0 || rec.subject.toLowerCase().indexOf(self.searchBySubject().toLowerCase()) > -1)
                &&
                (self.showToday() === false || rec.dateCreated.includes(new Date().toLocaleDateString()))
                &&
                (self.onlyNotBinded() === false || rec.operatorId === null)
                &&
                (self.selectedContrahents().length === 0 || self.selectedContrahents().indexOf(rec.contrahentId) > -1)
                &&
                (self.selectedRepresentatives().length === 0 || self.selectedRepresentatives().indexOf(rec.representativeId) > -1)
                &&
                (self.selectedOperators().length === 0 || self.selectedOperators().indexOf(rec.operatorId) > -1)
                &&
                (self.selectedPriorities().length === 0 || self.selectedPriorities().indexOf(rec.priorityId) > -1)
                &&
                (self.selectedTypes().length === 0 || self.selectedTypes().indexOf(rec.typeId) > -1)
                &&
                (self.selectedStatuses().length === 0 || self.selectedStatuses().indexOf(rec.statusId) > -1)
            )
        });
    });

    //#endregion


    //#region Operations

    function sortTicketsFunction(t) {
        return -new Date(t.dateCreated);
    }

    self.getTickets = function () {
        self.sortUtil.clearColumnStates();

        SendRequest(requestType.GET, app.dataModel.ticketsInfoUrl, { dateFrom: self.dateFrom(), dateTo: self.dateTo() }, null, null, function (data) {
            data.sortBy(sortTicketsFunction);

            fromISODateToLocaleStringConverter(data, "dateCreated", "executionDate", "receiptDate");

            self.tickets(data);
        });
    }

    self.exportToCSV = function () {
        let tickets = objectsToRows(self.filteredRecords());
        exportToCsv('tickets.csv', tickets);
    }

    //#endregion


    //#region Initialise

    self.InitiateLookups = function () {
        SendRequest(requestType.GET, app.dataModel.lookupsUrl, null, null, null, function (data) {
            self.lookups(data); //Następuje utworzenie elementu z selectami w DOM i aktualizacja selectów
        })
    }

    self.OnLoad = new OnLoad(function () {
        self.InitiateLookups();
    });

    self.SetHintButtons = function () {
        SetHintButton($("#filtersTooltip"));
    }

    self.InitiateElements = function () {
        $('#filtersCard').card();
    }

    self.InitializeUI = function () {
        self.SetHintButtons();

        self.InitiateElements();
    }

    self.Initialize = function () {
        self.InitializeUI();

        self.OnLoad.Execute();

        self.getTickets();
    }

    //#endregion


    Sammy(function () {
        this.get('#home', function () {
            app.view(self);

            self.Initialize();

            ActivateFolder("home");
        });
        this.get('/', function () { this.app.runRoute('get', '#home'); });
    });

    return self;
}

app.addViewModel({
    name: "Home",
    bindingMemberName: "home",
    factory: HomeViewModel
});
