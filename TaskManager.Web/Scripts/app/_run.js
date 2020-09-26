$(function () {
    app.initialize();

    // Activate Knockout
    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app, document.body); //By pobrane później dane miały odpowiednie odwzorowanie w UI

    app.runSammy();

    //$(".selectpicker").selectpicker('val', getCookie("culture"))
    //$('#languages').selectpicker('refresh');

    //checkCookie();
});
