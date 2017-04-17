$(document).ready(function () {
    var bettingHub = $.connection.bettingHub;

    bettingHub.client.sendUpdateData = function (data) {
        console.log(data);
    };

    // contosoChatHubProxy.server.leaveGroup(groupName);
    $.connection.hub.start().done(function () {
        bettingHub.server.joinGroup("Soccer");
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