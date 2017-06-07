(function () {
    const apiBaseUrl = "api/v1";

    function loadGrid() {
        fetch(`${apiBaseUrl}/grid`, { method: "GET" })
            .then(response => response.json().then(data => {
                renderEmps(data.emps);
            }));
    }

    function renderEmps(emps) {
        var container = document.createElement("div");

        for (var i = 0; i < emps.length; i++) {
            var empDiv = document.createElement("pre");
            empDiv.innerHTML = JSON.stringify(emps[i], null, 2);
            container.appendChild(empDiv);
        }

        document.body.appendChild(container);
    }

    document.addEventListener("DOMContentLoaded", function (event) {
        document.getElementById("test").innerHTML = "App is running...";
        loadGrid();
    });
}());