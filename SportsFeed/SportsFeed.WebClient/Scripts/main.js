var previousGroup = "Soccer";
var bettingHub;

function changeRoom(group) {
    bettingHub.server.leaveGroup(previousGroup);
    bettingHub.server.joinGroup(group);

    previousGroup = group;
    window.scrollTo(0, 0);
}

$(document).ready(function () {
    var oddTemplateHtml = $("#odd-template").html();
    var betTemplateHtml = $("#bet-template").html();
    var matchTemplateHtml = $("#match-template").html();

    var templates = {
        event: Handlebars.compile($("#event-template").html()),
        match: Handlebars.compile(matchTemplateHtml),
        bet: Handlebars.compile(betTemplateHtml),
        odd: Handlebars.compile(oddTemplateHtml)
    };

    Handlebars.registerPartial("odd", oddTemplateHtml);
    Handlebars.registerPartial("bet", betTemplateHtml);
    Handlebars.registerPartial("match", matchTemplateHtml);
    var odd = {
        Id: 999,
        Name: "Odd1",
        SpecialBetValue: "9999",
        Value: 5000.3
    };

    var bet = {
        Id: 1111,
        Name: "SuperBet",
        Odds: [odd]
    };

    var match = {
        Id: 2222,
        Name: "Pesho Vs Tutrakan",
        Bets: [bet]
    };

    var event = {
        Id: 3333,
        Name: "Super Tournament",
        Matches: [match]
    }

    var a = templates.event(event);
    console.log(a);

    bettingHub = $.connection.bettingHub;

    bettingHub.client.sendUpdateData = function (changes) {
        console.log("MODIFIED");
        console.log(changes);

        ensureNoNulls(changes);
    };

    bettingHub.client.sendDeleteData = function (changes) {
        console.log("DELETED");
        console.log(changes);

        ensureNoNulls(changes);

        deleteEntities(changes);
    };

    $.connection.hub.start().done(function () {
        bettingHub.server.joinGroup(previousGroup);
    });

    function ensureNoNulls(data) {
        data.Events = data.Events || [];
        data.Matches = data.Matches || [];
        data.Bets = data.Bets || [];
        data.Odds = data.Odds || [];
    }

    function deleteEntities(data) {
        data.events.forEach(function (ev) {
            $("#event-" + ev.Id).remove();
        });

        data.Matches.forEach(function (ev) {
            $("#match-" + ev.Id).remove();
        });

        data.Bets.forEach(function (ev) {
            $("#bet-" + ev.Id).remove();
        });

        data.Odds.forEach(function (ev) {
            $("#odd-" + ev.Id).remove();
        });
    }
});