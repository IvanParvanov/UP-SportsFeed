var previousGroup = "Soccer";
var bettingHub;
$(document).ready(function () {

    bettingHub = $.connection.bettingHub;

    bettingHub.client.sendUpdateData = function (data) {
        console.log(data);
    };

    // contosoChatHubProxy.server.leaveGroup(groupName);
    $.connection.hub.start().done(function () {
        bettingHub.server.joinGroup(previousGroup);
    });

    //var connection = $.connection('/betting');

    //connection.received(function (data) {
    //    console.log(data);
    //});

    //connection.start().done(function () {
    //    $("#broadcast").click(function () {
    //        connection.server.joinGroup("Soccer");

    //        //connection.send($('#msg').val());
    //    });
    //});
});

function changeRoom(group) {
    bettingHub.server.leaveGroup(previousGroup);
    bettingHub.server.joinGroup(group);

    previousGroup = group;
}