function ReportsViewModel(app, dataModel) {
    var self = this;

    Sammy(function () {
        this.get('#reports', function () {
            app.view(self);

            ActivateFolder("reports");
        });
    });

    return self;
}

app.addViewModel({
    name: "Reports",
    bindingMemberName: "reports",
    factory: ReportsViewModel
});