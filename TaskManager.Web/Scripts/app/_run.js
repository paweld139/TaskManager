$(function () {
    app.initialize();

    // Activate Knockout
    ko.validation.init({ grouping: { observable: false } });
    ko.applyBindings(app, document.body);

    //$(".selectpicker").selectpicker('val', getCookie("culture"))
    //$('#languages').selectpicker('refresh');

    //checkCookie();
});
