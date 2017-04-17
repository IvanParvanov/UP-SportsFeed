var previousGroup = "Soccer";
var bettingHub;
$(document).ready(function () {

    bettingHub = $.connection.bettingHub;

    bettingHub.client.sendUpdateData = function (data) {
        console.log("MODIFIED");
        console.log(data);
    };

    bettingHub.client.sendDeleteData = function(data) {
        console.log("DELETED");
        console.log(data);
    };

    $.connection.hub.start().done(function () {
        bettingHub.server.joinGroup(previousGroup);
    });
});

function changeRoom(group) {
    bettingHub.server.leaveGroup(previousGroup);
    bettingHub.server.joinGroup(group);

    previousGroup = group;
}