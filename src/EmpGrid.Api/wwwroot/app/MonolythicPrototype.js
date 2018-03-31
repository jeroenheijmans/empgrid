(function () {
    "use strict";

    // By @broofa and community, see https://stackoverflow.com/a/2117523
    // Good enough for here for now...
    function uuidv4() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }

    class PresenceVm {
        constructor(data, mediums) {
            this._resetDto = data;

            this.mediumId = ko.observable();
            this.url = ko.observable();

            this.name = ko.pureComputed(() => {
                let id = this.mediumId();
                return mediums.hasOwnProperty(id)
                    ? mediums[this.mediumId()].name
                    : id;
            });

            this.renderHtml = ko.pureComputed(() => {
                let id = this.mediumId();
                return mediums.hasOwnProperty(id)
                    ? `<i class="fa fa-${mediums[this.mediumId()].fontAwesomeClass}"></i>`
                    : '';
            });

            this.reset();
        }

        reset() {
            this.mediumId(this._resetDto.mediumId);
            this.url(this._resetDto.url);
        }

        asDto() {
            return {
                mediumId: this.mediumId(),
                url: this.url()
            };
        }
    }
    class EmpVm {
        constructor(data, mediums) {
            this._id = data.id;
            this.id = ko.pureComputed(() => this._id);
            this.name = ko.observable("");
            this.emailAddress = ko.observable("");
            this.tagLine = ko.observable("");
            this.presences = ko.observableArray([]);

            this._gravatarUrl = ko.observable("");
            this.avatarUrl = ko.pureComputed(() => this._gravatarUrl() || "img/favicon.png");

            this.mailto = ko.pureComputed(() => `mailto:${this.emailAddress()}`);
            this.showMailtoLink = ko.pureComputed(() => !!this.emailAddress());
            this.tagLineDisplay = ko.pureComputed(() => this.tagLine() || "No tag line provided...");

            this._resetDto = data;
            this._mediums = mediums;
            this.reset();
        }

        addPresence() {
            this.presences.push(new PresenceVm({}, this._mediums));
        }

        reset() {
            this._gravatarUrl(this._resetDto.gravatarUrl);
            this.name(this._resetDto.name);
            this.emailAddress(this._resetDto.emailAddress);
            this.tagLine(this._resetDto.tagLine);
            this.presences(this._resetDto.presences.map(p => new PresenceVm(p, this._mediums)));
        }

        saveResetState(dto) {
            this._resetDto = dto;
        }

        asDto() {
            return {
                id: this._id,
                name: this.name(),
                emailAddress: this.emailAddress(),
                tagLine: this.tagLine(),
                presences: this.presences().map(p => p.asDto()).filter(p => !!p.mediumId)
            };
        }
    }

    class GridVm {
        constructor(data) {
            this._emps = ko.observableArray(data.emps.map(e => new EmpVm(e, data.mediums)));

            this.emps = ko.computed(() => this._emps().sort((a,b) => a.name().localeCompare(b.name())));

            var nrOfCols = Math.min(8, Math.round(Math.sqrt(this.emps().length)));
            this.colCss = `repeat(${nrOfCols}, 1fr)`;
        }

        removeEmp(emp) {
            this._emps.remove(emp);
        }
    }

    class RootVm {
        constructor(dal) {
            this._dal = dal;

            this.grid = ko.observable();
            this.isInEditMode = ko.observable(false);
            this.empInEditMode = ko.observable(null);
            this.isBusy = ko.observable(false);

            this.reLoadGrid();
        }

        reLoadGrid() {
            this._dal.getGrid().then(data => {
                this.grid(new GridVm(data));

                this.mediumOptions = Object.keys(data.mediums).map(k => {
                    return {
                        txt: data.mediums[k].name,
                        val: data.mediums[k].id
                    };
                });
            });
        }

        toggleEditMode() {
            this.isInEditMode(!this.isInEditMode());
        }

        commitDeleting(emp) {
            if (confirm(`Are you certain you want to delete ${emp.name()}?\n\nThis cannot be undone!`)) {
                this.isBusy(true);

                this._dal.deleteEmp(emp.id())
                    .then(json => {
                        this.grid().removeEmp(emp);
                        this.isBusy(false);
                    });
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
            const dto = this.empInEditMode().asDto();

            this.isBusy(true);

            this._dal.saveEmp(dto)
                .then(json => {
                    this.isBusy(false);
                    this.empInEditMode().saveResetState(dto);
                    this.empInEditMode(null);
                });
        }
    }

    class Dal {
        constructor(baseUrl) {
            this._baseUrl = baseUrl;
        }

        getGrid() {
            return fetch(`${this._baseUrl}/grid`, {
                method: "GET",
            }).then(response => response.json());
        }

        saveEmp(dto) {
            return fetch(`${this._baseUrl}/emp/${dto.id}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(dto),
            }).then(response => null);
        }

        deleteEmp(id) {
            return fetch(`${this._baseUrl}/emp/${id}`, {
                method: "DELETE",
            }).then(response => null);
        }
    }

    function bootstrap() {
        const dal = new Dal("/api/v1");
        const vm = new RootVm(dal);
        const viewPort = document.getElementById("ko-viewport");
        ko.applyBindings(vm, viewPort);
    }

    document.addEventListener("DOMContentLoaded", evt => bootstrap());
}());
