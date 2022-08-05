var Inject = Inject || new function () {

    var interval = setInterval(function () {

        if (document.querySelector(".opblock-tag-section")) {
            clearInterval(interval);
            loaded();
            menuLoaded();
        }
        
    }, 300);

    function loaded() {

        var spoilerButtons = document.querySelectorAll(".arrow");

        spoilerButtons.forEach(function (item) {
            item.onclick = function (event) {

                var inter1 = setInterval(function () {
                        clearInterval(inter1);
                }, 300);
            };
        });
    }

    function menuLoaded() {

        var button = { name: "Validation rules", link: "/docs/menu/ValidationRules.html" }

        var container = document.querySelector(".auth-wrapper");

        var newNode = document.createElement("div");
        newNode.classList.add("wrapper");
        var btnGroup = document.createElement("div");
        btnGroup.classList.add("btn-group");
        btnGroup.setAttribute("role", "group");
        btnGroup.style.display = "block";
        newNode.appendChild(btnGroup);

        var newButton = document.createElement("button");
        newButton.setAttribute("type", "button");
        newButton.classList.add("btn");
        newButton.classList.add("btn-outline-warning");
        newButton.innerHTML = button.name;
        newButton.setAttribute("data-link", button.link);
        btnGroup.appendChild(newButton);

        newButton.onclick = function (ev) {
            ev.stopImmediatePropagation();

            var attrUrl = ev.target.getAttribute("data-link");
            loadContent(attrUrl, function (data) {
                var md = ModalSvc.create(data);
                md.toggle();
            }, null);

        };

        container.appendChild(newButton);

    }

    function loadContent(url, succ, err) {
        $.ajax({
            type: 'GET',
            url: url,
            data: null,
            cache: false,
            success: function (data) {
                if (succ) {
                    succ(data);
                }
            },
            error: function (msg) {
                if (err) {
                    err();
                }
            }
        });
    }
}

