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

    function Files(name, source, mimeType, fileId, extension, refId, refGid, refSubId) {
        this.FileId = fileId;
        this.Name = name;
        this.Extension = extension;
        this.RefId = refId;
        this.RefGid = refGid;
        this.RefSubId = refSubId;
        this.MimeType = mimeType;
        this.Source = source;
    }

    common.Comment = function (ticketId) {
        var self = this;

        self.ticketId = ticketId;
        self.employeeId = UserData.employeeId();

        self.content = ko.observable("").extend({ notify: 'always' }).withPausing();

        self.isEmpty = ko.pureComputed(function () {
            return self.content().length === 0;
        });

        self.clear = function () {
            self.content.sneakyUpdate("");
        }

        self.isValid = function () {
            return self.content.isValid();
        }

        self.Files = [];

        self.fileSelect = function (_element, event) {
            var cFiles = event.target.files; // FileList object

            self.Files.length = 0;

            // Loop through the FileList and render image files as thumbnails.
            for (var i = 0, ff = cFiles[i]; i < cFiles.length; i++) {

                var read = new FileReader();

                // Closure to capture the file information.
                read.onload = (function (theReference) {
                    return function (e) {
                        self.Files.push(new Files(theReference.name, e.target.result, theReference.type));
                    };
                })(ff);

                // Read in the image file as a data URL.
                read.readAsDataURL(ff);
            }
        }
    }

    return common;
})();