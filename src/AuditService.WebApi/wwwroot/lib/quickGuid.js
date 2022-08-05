var QuickGuid = QuickGuid || new function () {

    var create = function () {
        return "guid_" + Math.random().toString(36).substring(2) + (new Date()).getTime().toString();
    }

    return {
        newGuid: create
    }
};