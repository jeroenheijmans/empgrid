(function () {
    const apiBaseUrl = "api/v1";

    function loadGrid() {
        fetch(`${apiBaseUrl}/grid`, { method: "GET" })
            .then(response => response.json().then(data => {
                var viewPort = document.getElementById("grid");
                ko.applyBindings(data, viewPort);
            }));
    }

    document.addEventListener("DOMContentLoaded", function (event) {
        loadGrid();
    });
}());