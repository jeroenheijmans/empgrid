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
            this.name = ko.observable("");
            this.emailAddress = ko.observable("");
            this.tagLine = ko.observable("");
            this.presences = ko.observableArray([]);

            this.mailto = ko.pureComputed(() => `mailto:${this.emailAddress()}`);
            this.showMailtoLink = ko.pureComputed(() => !!this.emailAddress());
            this.tagLineDisplay = ko.pureComputed(() => this.tagLine() || "No tag line provided...");

            this._resetDto = data;
            this._mediums = mediums;
            this.reset();
        }

        reset() {
            this.name(this._resetDto.name);
            this.emailAddress(this._resetDto.emailAddress);
            this.tagLine(this._resetDto.tagLine);
            this.presences(this._resetDto.presences.map(p => new PresenceVm(p, this._mediums)));
        }
    }

    class GridVm {
        constructor(data) {
            this.emps = ko.observableArray(data.emps.map(e => new EmpVm(e, data.mediums)));

            var nrOfCols = Math.min(8, Math.round(Math.sqrt(this.emps().length)));
            this.colCss = `repeat(${nrOfCols}, 1fr)`;
        }
    }

    class RootVm {
        constructor() {
            this.grid = ko.observable();

            this.isInEditMode = ko.observable(false);
            this.empInEditMode = ko.observable(null);

            this.reLoadGrid();
        }

        reLoadGrid() {
            GET("/grid").then(data => {
                this.grid(new GridVm(data));
                this.empInEditMode(this.grid().emps()[0]);
            });
        }

        toggleEditMode() {
            this.isInEditMode(!this.isInEditMode())
        }

        startDeleting(emp) {
            if (confirm(`Are you certain you want to delete ${emp.name()}?\n\nThis cannot be undone!`)) {
                alert("TODO");
            }
        }

        startEditing(emp) {
            this.empInEditMode(emp);
        }

        cancelEditing() {
            this.empInEditMode().reset();
            this.empInEditMode(null);
        }

        commitEditing() {
            alert("TODO");
        }
    }

    function bootstrap() {
        var vm = new RootVm();
        var viewPort = document.getElementById("ko-viewport");
        ko.applyBindings(vm, viewPort);
    }

    document.addEventListener("DOMContentLoaded", evt => bootstrap());
}());
