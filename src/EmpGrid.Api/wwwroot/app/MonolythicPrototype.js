(function () {
    const apiBaseUrl = "api/v1";

    const goFetch = (path, options) => fetch(`${apiBaseUrl}${path}`, options).then(response => response.json());
    const GET = (path) => goFetch(path, { method: "GET" });

    class PresenceVm {
        constructor(data, mediums) {
            this.name = data.mediumId; // Just a default/fallback option
            this.renderHtml = data.mediumId; // Just a default/fallback option

            if (mediums.hasOwnProperty(data.mediumId)) {
                this.name = mediums[data.mediumId].name;
                this.renderHtml = `<i class="fa fa-${mediums[data.mediumId].fontAwesomeClass}"></i>`;
            } 

            this.url = data.url;
        }
    }
    class EmpVm {
        constructor(data, mediums) {
            this.name = data.name;
            this.emailAddress = data.emailAddress;
            this.mailto = `mailto:${this.emailAddress}`;
            this.showMailtoLink = !!this.emailAddress;
            this.tagLine = data.tagLine || "No tag line provided...";
            this.presences = data.presences.map(p => new PresenceVm(p, mediums));
        }
    }

    class GridVm {
        constructor(data) {
            this.emps = data.emps.map(e => new EmpVm(e, data.mediums));

            var nrOfCols = Math.min(8, Math.round(Math.sqrt(this.emps.length)));
            this.colCss = `repeat(${nrOfCols}, 1fr)`;
        }
    }

    class RootVm {
        constructor() {
            this.grid = ko.observable();
            this.reLoadGrid();
        }

        reLoadGrid() {
            GET("/grid").then(data => {
                this.grid(new GridVm(data));
            });
        }
    }

    function bootstrap() {
        var vm = new RootVm();
        var viewPort = document.getElementById("ko-viewport");
        ko.applyBindings(vm, viewPort);
    }

    document.addEventListener("DOMContentLoaded", evt => bootstrap());
}());