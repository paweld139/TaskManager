function DetailsViewModel(app, dataModel) {
    var self = this;

    Sammy(function () {
        this.get('#details/:taskId', function () {
            app.view(self);

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