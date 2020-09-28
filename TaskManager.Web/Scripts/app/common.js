
window.common = (function () {
    var common = {};

    common.getFragment = function getFragment() {
        if (window.location.hash.indexOf("#") === 0) {
            return parseQueryString(window.location.hash.substr(1));
        } else {
            return {};
        }
    };

    function parseQueryString(queryString) {
        var data = {},
            pairs, pair, separatorIndex, escapedKey, escapedValue, key, value;

        if (queryString === null) {
            return data;
        }

        pairs = queryString.split("&");

        for (var i = 0; i < pairs.length; i++) {
            pair = pairs[i];
            separatorIndex = pair.indexOf("=");

            if (separatorIndex === -1) {
                escapedKey = pair;
                escapedValue = null;
            } else {
                escapedKey = pair.substr(0, separatorIndex);
                escapedValue = pair.substr(separatorIndex + 1);
            }

            key = decodeURIComponent(escapedKey);
            value = decodeURIComponent(escapedValue);

            data[key] = value;
        }

        return data;
    }

    common.setTimezoneOffset = function () {
        setTimezoneCookie();
    }

    common.Comment = function (ticketId) {
        this.ticketId = ticketId;
        this.employeeId = UserData.employeeId();

        this.content = ko.observable("").extend({ notify: 'always' }).withPausing();

        this.isEmpty = ko.pureComputed(function () {
            return this.content().length === 0;
        }, this);

        this.clear = function () {
            this.content.sneakyUpdate("");
        }

        this.isValid = function () {
            return this.content.isValid(); 
        }
    }

    return common;
})();