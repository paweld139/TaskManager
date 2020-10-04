function DetailsViewModel(app, dataModel) {
    var self = this;


    //#region Data

    let ticketId = 0;

    self.ticket = ko.observable();

    self.lookups = ko.observable();


    self.errors = ko.observable();

    self.errors.subscribe(function (value) {
        if (value) {
            scrollTop();
        }
    });


    self.info = ko.observable();

    self.info.subscribe(function (value) {
        if (value) {
            scrollTop();
        }
    });

    //#endregion


    //#region Operations

    function sortCommentsFunction(c) {
        return -new Date(c.dateCreated); //Sortowanie po dacie utworzenia malejąco
    }

    self.refreshTicket = function (data) {
        data.comments.sortBy(sortCommentsFunction);

        fromISODateToLocaleStringConverter(data, "dateCreated", "dateModified");

        fromISODateToLocaleStringConverter(data.comments, "dateCreated");

        self.comment = new window.common.Comment(ticketId); //W tym momencie pojawia się lub rerenderuje się sekcja z edytorem tekstu
        //Sekcja dodawania komentarza dla ticketa

        //Przed ustawieniem ticketa z powodu with, kopia widoku jest przechowywana przez knockouta, ale nie ma jej w dokumencie.

        self.InitiateLookups();

        self.ticket(data);

        //Po ustawieniu ticketa widok jest w DOM, więc można zainicjować edytor tekstu, przypisać event do
        //wysyłania formularza czy odświeżyć walidację. Dlatego tak się dzieje, bo te elementy są
        //właśnie w tym widoku przechowywanym przez knockout.

        self.commentTextEditor = new InitializeTextEditor("commentTaskEditor", self.comment.content);

        SetHash("editTicketForm", null, ".editTicket",
            function () {
                $("#editTicketForm").submit();

                return false;
            });


        RefreshUnobtrusiveValidator("editTicketForm");
    }

    self.getTicket = function () {
        SendRequest(requestType.GET, app.dataModel.getTicketUrl + ticketId, null, null, null, function (data) {
            self.refreshTicket(data);
        })
    }

    self.editTicket = function (data) {
        SendRequest(requestType.PUT, app.dataModel.editTicketUrl + ticketId, data, null, null, function (response) {
            self.refreshTicket(response); //Zostaje ustawione m.in. aktualne RowVersion

            self.info("Pomyślnie zapisano");
        },
            function (errors) {
                if (errors && errors.modelState) {
                    DisplayModelStateErrors(errors.modelState, self.errors);
                }
                else if (errors) {
                    self.errors(errors.message || errors)
                }
            });
    }

    self.deleteComment = function (data) {
        SendRequest(requestType.DELETE, app.dataModel.deleteCommentUrl + ticketId + "/comments/" + data.id, null,
            function () {
                return confirm("Czy na pewno chcesz usunąć komentarz?");
            }, null,
            function () {
                self.getTicket();
            });
    }

    self.addComment = function () {
        self.commentTextEditor.updateElement(); //Aktualizuję observable content zawartością edytora tekstu.
        //Możliwe, że ckeditor jeszcze się nie zblurował i nie zaktualizował zawartości w observable
        //Jeśli nie jest prawidłowy, to wyświetla błąd.

        if (self.comment.isValid()) { //Dodaję komentarz tylko wtedy jak jest prawidłowy
            SendRequest(requestType.POST, app.dataModel.addCommentUrl + ticketId + "/comments", ko.toJSON(self.comment),
                null, null,
                function () {
                    self.getTicket();
                });
        }
    }

    self.setStatus = function (status) {
        SendRequest(requestType.PATCH, app.dataModel.setTicketStatusUrl + ticketId + "/" + status.id,
            null, null,
            function (data) {
                self.refreshTicket(data);
            }
        );
    }

    //#endregion


    //#region Initialise

    self.InitializeObject = function (id) {
        ticketId = id;
    }

    self.InitiateLookups = function () {
        SendRequest(requestType.GET, app.dataModel.lookupsUrl, { ticketId: ticketId }, null, function (data) {
            self.lookups(data);

            const ticket = self.ticket();

            if (ticket) { //Zdarzenie odświeżające pickera zaczyna się wywoływać po ustawieniu opcji. 
                //W tym przypadku wartość trafiła jako pierwsza z ticketa, a dopiero później wartości.
                //Następuje odświeżenie pickerów, by były widoczne wartości i aktualna wartość.
                $(".selectpicker").selectpicker('refresh');

                //RefreshUnobtrusiveValidator("editTicketForm");
            }
        })
    }

    self.OnLoad = new OnLoad(function () {
       
    }); //Wywołuje się tylko raz

    self.InitializeElements = function () { //Jest poza danymi ticketa, więc jest wywoływane po wejściu w widok
        $("#editTicketForm").submit(function () {
            const data = JSON.stringify($(this).serializeObject());

            self.editTicket(data);

            return false;
        });
    }

    self.InitializeUI = function () {
        self.ticket(null);
        self.errors(null);
        self.info(null);

        self.InitializeElements();
    }

    self.Initialize = function (id) { //Wywołuje się po każdym wejściu w widok
        self.InitializeUI();

        self.InitializeObject(id)

        //self.OnLoad.Execute(); //Wywołuje się tylko raz

        //self.InitiateLookups();

        self.getTicket(); //Pobranie danych dla ticketa o zadanym id
    }

    //#endregion


    Sammy(function () {
        this.get('#details/:taskId', function () {
            app.view(self); //Ustawienie widoku jako aktualnego, następuje sprawdzenie czy token dostępu do API jest aktualny.
            //Widok zostaje umieszczony w DOM, więc można zacząć ustawianie eventów, inicjalizacja elementów

            self.Initialize(this.params.taskId); //Inicjalizacja UI oraz pobranie danych

            ActivateFolder("details"); //Pojawienie widoku, o ile jest niewidoczny po jego ustawieniu, np. na początku.
            //wszystkie widoki są niewidoczne.
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