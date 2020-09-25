﻿function AppViewModel(dataModel) {
    // Private state
    var self = this;

    // Private operations
    function cleanUpLocation() {
        window.location.hash = "";

        if (typeof history.pushState !== "undefined") {
            history.pushState("", document.title, location.pathname);
        }
    }

    // Data
    self.Views = {
        Loading: {} // Other views are added dynamically by app.addViewModel(...).
    };

    self.dataModel = dataModel;

    // UI state
    self.folders = ['Home', 'Add'];

    self.view = ko.observable(self.Views.Loading);

    self.isViewSelected = function (viewItem) {
        return self.view() === viewItem;
    }

    self.loading = ko.computed(function () {
        return self.view() === self.Views.Loading;
    });

    // UI operations

    // Other navigateToX functions are added dynamically by app.addViewModel(...).

    // Other operations
    self.accessTokenExpired = ko.observable();

    self.reauthorize = function () {
        self.dataModel.removeAccessToken();
        self.view.notifySubscribers();
    };

    self.addViewModel = function (options) {
        var viewItem = new options.factory(self, dataModel),
            navigator;

        // Add view to AppViewModel.Views enum (for example, app.Views.Home).
        self.Views[options.name] = viewItem;

        // Add binding member to AppViewModel (for example, app.home);
        self[options.bindingMemberName] = ko.computed(function () {
            //self.accessTokenExpired();

            if (!dataModel.getAccessToken()) {
                // The following code looks for a fragment in the URL to get the access token which will be
                // used to call the protected Web API resource
                var fragment = common.getFragment();

                if (fragment.access_token) {
                    // returning with access token, restore old hash, or at least hide token
                    window.location.hash = fragment.state || '';
                    dataModel.setAccessToken(fragment.access_token, fragment.expires_in);
                } else {
                    // no token - so bounce to Authorize endpoint in AccountController to sign in or register
                    window.location = "/Account/Authorize?client_id=web&response_type=token&state="
                        + encodeURIComponent(window.location.hash);
                }
            }

            if (self.view() !== viewItem) {
                return null;
            }

            return self.Views[options.name];
        });

        if (typeof options.navigatorFactory !== "undefined") {
            navigator = options.navigatorFactory(self, dataModel);
        } else {
            navigator = function () {
                window.location.hash = options.bindingMemberName;
            };
        }

        // Add navigation member to AppViewModel (for example, app.NavigateToHome());
        self["navigateTo" + options.name] = navigator;
    };

    self.initialize = function () {
        window.common.setTimezoneOffset();

        Sammy().run();
    };
}

var app = new AppViewModel(new AppDataModel());
