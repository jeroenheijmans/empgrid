(function () {
    const apiBaseUrl = "api/v1";

    function PresenceVm(data, mediums) {
        var self = this;

        self.name = data.mediumId; // Just a default/fallback option
        self.renderHtml = data.mediumId; // Just a default/fallback option

        if (mediums.hasOwnProperty(data.mediumId)) {
            self.name = mediums[data.mediumId].name;
            self.renderHtml = `<i class="fa fa-${mediums[data.mediumId].fontAwesomeClass}"></i>`;
        } 

        self.url = data.url;
    }

    function EmpVm(data, mediums) {
        var self = this;
        
        self.name = data.name;
        self.emailAddress = data.emailAddress;
        self.mailto = `mailto:${self.emailAddress}`;
        self.showMailtoLink = !!self.emailAddress;
        self.tagLine = data.tagLine || "No tag line provided...";
        self.presences = data.presences.map(p => new PresenceVm(p, mediums));
    }

    function GridVm(data) {
        var self = this;

        self.emps = data.emps.map(e => new EmpVm(e, data.mediums));

        var nrOfCols = Math.min(8, Math.round(Math.sqrt(self.emps.length)));
        self.colCss = `repeat(${nrOfCols}, 1fr)`;
    }

    function loadGrid() {
        fetch(`${apiBaseUrl}/grid`, { method: "GET" })
            .then(response => response.json().then(data => {
                var vm = new GridVm(data);
                var viewPort = document.getElementById("grid");
                ko.applyBindings(vm, viewPort);
            }));
    }

    document.addEventListener("DOMContentLoaded", function (event) {
        loadGrid();
    });
}());