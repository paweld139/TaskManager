function AddViewModel(app, dataModel) {
    var self = this;

    Sammy(function () {
        this.get('#add', function () {
            app.view(self);

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