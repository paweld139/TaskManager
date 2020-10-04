function AppDataModel() {
    var self = this;
    // Routes
    self.ticketsInfoUrl = "/api/tickets/briefs/getByDate";
    self.lookupsUrl = "api/lookups/all";
    self.saveTicketUrl = "api/tickets";
    self.getTicketUrl = "api/tickets/";
    self.editTicketUrl = "api/tickets/";
    self.deleteCommentUrl = "api/tickets/";
    self.addCommentUrl = "api/tickets/";
    self.setTicketStatusUrl = "api/tickets/";
    self.deleteFileUrl = "api/files/";

    self.siteUrl = "/";

    // Route operations

    // Other private operations

    // Operations

    self.removeFile = function (onSuccess, onError) {
        return function (data) {
            SendRequest(requestType.DELETE, self.deleteFileUrl + data.id, null,
                function () {
                    return confirm("Czy na pewno chcesz usunąć plik?");
                }, null, onSuccess, onError);
        };
    };

    // Data
    self.returnUrl = self.siteUrl;

    const accessTokenKey = "accessToken";
    const tokenExpirationKey = "tokenExpiration";

    // Data access operations
    self.setAccessToken = function (accessToken, expiresIn) {
        let tokenExpiration = new Date().addSeconds(parseInt(expiresIn));

        sessionStorage.setItem(accessTokenKey, accessToken);
        sessionStorage.setItem(tokenExpirationKey, tokenExpiration);
    };

    self.getAccessToken = function () {
        let tokenExpiration = sessionStorage.getItem(tokenExpirationKey);

        return (tokenExpiration !== null && new Date(tokenExpiration) > new Date()) ? sessionStorage.getItem(accessTokenKey) : null;
    };


    self.removeAccessToken = function () {
        sessionStorage.removeItem(accessTokenKey);
        sessionStorage.removeItem(tokenExpirationKey);
    };
}
