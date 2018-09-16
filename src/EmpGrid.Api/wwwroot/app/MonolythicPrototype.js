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

    function delay(duration) {
        return (...args) =>
            new Promise((resolve, _) => setTimeout(() => resolve(...args), duration));
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
            this.presences((this._resetDto.presences || []).map(p => new PresenceVm(p, this._mediums)));
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
            this._mediums = data.mediums;
            this._emps = ko.observableArray(data.emps.map(e => new EmpVm(e, data.mediums)));

            this.emps = ko.computed(() => this._emps().sort((a,b) => a.name().localeCompare(b.name())));

            var nrOfCols = Math.min(8, Math.round(Math.sqrt(this.emps().length)));
            this.colCss = `repeat(${nrOfCols}, 1fr)`;
        }

        addEmp(emp) {
            this._emps.push(emp);
        }

        removeEmp(emp) {
            this._emps.remove(emp);
        }
    }

    class RootVm {
        constructor(oauth2, dal) {
            this._dal = dal;
            this._oauth2 = oauth2;

            this.grid = ko.observable();
            this.isInEditMode = ko.observable(false);
            this.empInEditMode = ko.observable(null);
            this.isBusy = ko.observable(true);

            this.reLoadGrid();
        }

        reLoadGrid() {
            this.isBusy(true);

            return this._dal.getGrid().then(data => {
                this.grid(new GridVm(data));

                this._mediums = data.mediums;

                this.mediumOptions = Object.keys(data.mediums).map(k => {
                    return {
                        txt: data.mediums[k].name,
                        val: data.mediums[k].id
                    };
                });

                this.isBusy(false);
            });
        }

        toggleEditMode() {
            if (this.isInEditMode()) {
                this.logout().then(_ => this.isInEditMode(false));
            } else if (this._oauth2.isAuthorized()) {
                this.isInEditMode(true);
            } else {
                this.login().then(_ => this.isInEditMode(true));
            }
        }

        login() {
            this.isBusy(true);

            return this._oauth2
                .authorize("admin", "changemequickly")
                .then(delay(350)) // HACK: To prevent UI flickering
                .then(_ => this.isBusy(false));
        }

        logout() {
            return this._oauth2.clear();
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

        startAdding() {
            this.empInEditMode(new EmpVm({ id: uuidv4() }, this._mediums));
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
                    if (!this.grid().emps().some(e => e.id() === dto.id)) {
                        this.grid().addEmp(this.empInEditMode());
                    }

                    this.empInEditMode().saveResetState(dto);
                    this.empInEditMode(null);

                    this.isBusy(false);
                });
        }
    }

    class Dal {
        constructor(baseUrl, oauth2) {
            this._baseUrl = baseUrl;
            this._oauth2 = oauth2;
        }

        getGrid() {
            return fetch(`${this._baseUrl}/grid`, {
                method: "GET",
            }).then(response => response.json());
        }

        saveEmp(dto) {
            return fetch(`${this._baseUrl}/emp/${dto.id}`, {
                method: "PUT",
                headers: this._oauth2.setBearerToken({ "Content-Type": "application/json" }),
                body: JSON.stringify(dto),
            }).then(response => null);
        }

        deleteEmp(id) {
            return fetch(`${this._baseUrl}/emp/${id}`, {
                headers: this._oauth2.setBearerToken({}),
                method: "DELETE",
            }).then(response => null);
        }
    }

    class OAuth2 {
        constructor(baseUrl) {
            this._baseUrl = baseUrl;
        }

        authorize(username, password) {
            if (!username || !password) {
                throw new Error("Cannot authorize, missing username and/or password");
            }

            return this._authorize({
                "username": username,
                "password": password,
                "grant_type": "password",
            });
        }

        refresh() {
            if (!this._refreshToken) {
                throw new Error("Cannot refresh, we don't have a refresh token");
            }

            return this._authorize({
                "refresh_token": this._refreshToken,
                "grant_type": "refresh_token",
            });
        }

        setBearerToken(headers = {}) {
            if (!this._accessToken) {
                throw new Error("Can't set access token, have you authorized yet?");
            }

            headers["Authorization"] = `Bearer ${this._accessToken}`;

            return headers;
        }

        clear() {
            this._clearRefreshTimer();
            this._accessToken = null;
            this._refreshToken = null;
            this._tokenRefreshAfterTime = null;

            // For consistent API and future compatability with any fetch
            // calls we return a promise chain.
            return Promise.resolve();
        }

        isAuthorized() {
            return !!this._accessToken;
        }

        _authorize(formDataValues) {
            const formData = new FormData();

            formData.append("scope", "offline_access empgridv1");
            formData.append("client_id", "empgridv1-js");

            for (const key in formDataValues) {
                formData.append(key, formDataValues[key]);
            }

            return fetch(`${this._baseUrl}/connect/token`, {
                method: "POST",
                body: formData
            }).then(response => response.json())
                .then(json => this._handleTokenResponse(json));
        }

        _handleTokenResponse(json) {
            this._accessToken = json["access_token"];
            this._refreshToken = json["refresh_token"];

            const expiresIn = parseInt(json["expires_in"], 10);

            this._clearRefreshTimer();

            const factor = 0.5;
            const now = new Date();

            this._tokenRefreshAfterTime = now.setSeconds(now.getSeconds() + (expiresIn * factor));

            this._refreshTimer = setInterval(() => {
                if (new Date().getTime() > this._tokenRefreshAfterTime) {
                    this.refresh();
                }
            }, 5000);
        }

        _clearRefreshTimer() {
            if (this._refreshTimer) {
                clearInterval(this._refreshTimer);
            }
        }
    }

    function bootstrap() {
        const oauth2 = new OAuth2("");
        const dal = new Dal("/api/v1", oauth2);
        const vm = new RootVm(oauth2, dal);
        const viewPort = document.getElementById("ko-viewport");
        ko.applyBindings(vm, viewPort);
    }

    document.addEventListener("DOMContentLoaded", evt => bootstrap());
}());
