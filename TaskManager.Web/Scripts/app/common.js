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

    function File(name, source, mimeType) {
        this.name = name;
        this.mimeType = mimeType;
        this.source = source;
    }

    function filesTooLarge(target) {
        let filesSize = 0;

        let result = false;

        let files = target.files;

        for (var i = 0; i < files.length; i++) {
            filesSize += files[i].size;
        }

        filesSize /= 1024;

        if (filesSize > 4096) {
            alert("Maksymalny rozmiar plików to 4MB");

            target.value = '';

            result = true;
        }

        return result;
    }

    common.selectFiles = function (target, output) {
        let result = true;

        if (filesTooLarge(target)) {
            result = false;
        }
        else {
            var cFiles = target.files; // FileList object

            output.length = 0;

            // Loop through the FileList and render image files as thumbnails.
            for (var i = 0; i < cFiles.length; i++) {
                let ff = cFiles[i]

                var read = new FileReader();

                // Closure to capture the file information.
                read.onload = (function (theReference) {
                    return function (e) {
                        output.push(new File(theReference.name, e.target.result, theReference.type));
                    };
                })(ff);

                // Read in the image file as a data URL.
                read.readAsDataURL(ff);
            }
        }

        return result;
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

        self.files = [];

        self.fileSelect = function (_element, event) {
            common.selectFiles(event.target, self.files);
        }
    }

    return common;
})();