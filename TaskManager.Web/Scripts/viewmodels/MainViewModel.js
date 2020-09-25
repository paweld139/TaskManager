function CommonViewModel() {
    //#region Default

    var self = this;

    self.folders = null;

    //#endregion


    //#region Jquery Validator settings

    $.validator.setDefaults({ ignore: ":hidden" });

    jQuery.validator.addMethod("enforcetrue", function (value, element, param) {
        return element.checked;
    });

    jQuery.validator.unobtrusive.adapters.addBool("enforcetrue");

    //#endregion


    //#region Initialize

    self.OnLoad = new OnLoad(function () {
        self.folders = ["tickets", "addTicket"];

        self.InitiateElements();

        GoToFolder("appointment");
    });

    self.Initialize = function () {
        self.OnLoad.Execute();
    };

    //#endregion
}