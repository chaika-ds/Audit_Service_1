

var ModalSvc = ModalSvc || (new function () {

    var modals = [];

    var svc = this;

    var loaders = {

    }

    var openCounter = 0;
    var body = null;
    var modalContainer = null;

    var interval = setInterval(function () {

        if (document.querySelector("body")) {
            clearInterval(interval);

            body = document.querySelector("body");
            modalContainer = document.createElement("div");

            body.appendChild(modalContainer);
        }

    }, 300);

    var toggleBackdrop = function () {
        if (openCounter == 1) {
            var backdrop = document.createElement("div");
            backdrop.classList.add("modal-backdrop");
            backdrop.classList.add("fade");
            backdrop.classList.add("show");
            body.appendChild(backdrop);
        }
        else if (openCounter == 0) {
            body = document.querySelector("body");
            var backdrop = document.querySelector(".modal-backdrop");
            if (backdrop)
                body.removeChild(backdrop);
        }
    }

    var createModalContainer = function (guid) {
        var container = document.createElement("div");

        container.classList.add("modal");
        container.classList.add("fade");
        container.setAttribute("id", "modal_" + guid);
        container.setAttribute("tabindex", "-1");
        container.setAttribute("role", "dialog");
        container.setAttribute("aria-labelledby", "modalLabel_" + guid);
        container.setAttribute("aria-hidden", "true");
        container.style.paddingRight = "17px";
        container.style.display = "block";
        container.style.maxHeight = "93%";

        return container;
    }

    var createDialog = function (guid) {
        var dialog = document.createElement("div");

        dialog.classList.add("modal-dialog");
        dialog.classList.add("modal-lg");
        dialog.classList.add("modal-dialog-scrollable");
        dialog.setAttribute("role", "document");

        return dialog;
    }

    var createContent = function (guid) {
        var content = document.createElement("div");
        content.classList.add("modal-content");
        return content;
    }

    var createHeader = function (guid, title) {
        var header = document.createElement("div");
        header.classList.add("modal-header");
        header.classList.add("alert");
        header.classList.add("alert-success");
        header.style.margin = "5px";

        var h6 = document.createElement("h6");
        h6.classList.add("modal-title");
        h6.setAttribute("id", "modalLabel_" + guid);
        h6.innerHTML = title;

        var button = document.createElement("button");
        button.setAttribute("type", "button");
        button.classList.add("close");
        button.setAttribute("data-dismiss", "modal");
        button.setAttribute("aria-label", "Close");

        button.setAttribute("onclick", ("ModalSvc.close('" + guid + "')"));

        var span = document.createElement("span");
        span.setAttribute("aria-hidden", "true");
        span.innerHTML = "&times;";

        button.appendChild(span);

        header.appendChild(h6);
        header.appendChild(button);

        return header;
    }

    var toggleModal = function (guid, isSecondary=false) {

        var modal = getModal(guid);
        if (modal.container.classList.contains("show")) {
            modal.container.classList.remove("show");
            modal.open = false;
            openCounter--;
        }
        else {
            modal.container.classList.add("show");
            modal.open = true;
            openCounter++;

            var timeout = setTimeout(function () {
                for (var i = 0; i < modal.scripts.length; ++i) {
                    try {
                        eval(modal.scripts[i]);
                    }
                    catch (e) {
                        console.error(e.message);
                    }
                }
                clearTimeout(timeout);
            }, 300);

        }
        if(!isSecondary){
            toggleBackdrop();
        }
        return outer(modal);
    }

    var getModal = function (guid) {
        var modal = null;
        for (var i = 0; i < modals.length; ++i) {
            if (modals[i].guid == guid) {
                modal = modals[i];
                break;
            }
        }
        return modal;
    }

    var getPrev = function (guid) {
        var modal = getModal(guid);
        var ind = modals.indexOf(modal);
        if (ind >= 1) {
            return outer(modals[ind - 1]);
        }
        else {
            return null;
        }
    }

    var getNext = function (guid) {
        var modal = getModal(guid);
        var ind = modals.indexOf(modal);
        if (ind < (modals.length - 1)) {
            return outer(modals[ind + 1]);
        }
        else {
            return null;
        }
    }

    var outer = function (modal) {
        return {
            guid: modal.guid,
            toggle: modal.toggle,
            close: modal.close,
            next: modal.next,
            prev: modal.prev,
            open: modal.open,
            load: modal.load
        }
    }

    this.close = function (guid) {
        var modal = getModal(guid);
        var ind = modals.indexOf(modal);
        modalContainer.removeChild(modal.container);

        if (modal.loaderGuid) {
            delete loaders[modal.loaderGuid];
        }

        modals.splice(ind, 1);

        if (modals.length <= 0) {
            var backdrop = document.querySelector(".modal-backdrop");
            if (backdrop)
                body.removeChild(backdrop);
        }
    }

    this.create = function (htmlNode) {

        var guid = QuickGuid.newGuid();

        var fake = document.createElement("div");
        fake.innerHTML = htmlNode;

        var container = createModalContainer(guid);
        var dialog = createDialog(guid);
        var content = createContent(guid);

        var headHtml = fake.querySelector(".modal-title")
        var title = "Title";
        if (headHtml) {
            title = headHtml.innerHTML;
        }
        var header = createHeader(guid, title);


        var bodyHtml = fake.querySelector(".modal-body")
        var body = "<div></div>";
        if (bodyHtml) {
            body = bodyHtml.innerHTML;
        }
        var contentHtml = document.createElement("div");
        contentHtml.innerHTML = body;
        contentHtml.style.overflow = "auto";

        content.appendChild(header);
        content.appendChild(contentHtml);

        dialog.appendChild(content);
        container.appendChild(dialog);

        modalContainer.appendChild(container);

        var scr = fake.querySelectorAll("script");
        var scripts = [];
        for (var i = 0; i < scr.length; ++i) {
            scripts.push(scr[i].innerHTML);
        }

        var modal = {
            guid: guid,
            container: container,
            dialog: dialog,
            content: content,
            scripts: scripts,

            open: false,

            toggle: function (isSecondary) {
                return toggleModal(guid, isSecondary);
            },
            close: function () {
                svc.close(guid);
            },
            next: function () {
                return getNext(guid);
            },
            prev: function () {
                return getPrev(guid);
            }
        };

        modals.push(modal);

        return outer(modal);
    }
}());