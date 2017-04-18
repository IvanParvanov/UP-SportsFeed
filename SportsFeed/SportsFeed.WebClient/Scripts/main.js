var previousGroup = "Soccer";
var bettingHub;

function changeRoom(group) {
    bettingHub.server.leaveGroup(previousGroup);
    bettingHub.server.joinGroup(group);

    previousGroup = group;
    window.scrollTo(0, 0);
}

$(document).ready(function () {
    var templates = (function () {
        var oddTemplateHtml = $("#odd-template").html();
        var betTemplateHtml = $("#bet-template").html();
        var matchTemplateHtml = $("#match-template").html();
        Handlebars.registerPartial("odd", oddTemplateHtml);
        Handlebars.registerPartial("bet", betTemplateHtml);
        Handlebars.registerPartial("match", matchTemplateHtml);

        return {
            event: Handlebars.compile($("#event-template").html()),
            match: Handlebars.compile(matchTemplateHtml),
            bet: Handlebars.compile(betTemplateHtml),
            odd: Handlebars.compile(oddTemplateHtml)
        }
    })();

    bettingHub = $.connection.bettingHub;

    bettingHub.client.sendUpdateData = function (changes) {
        console.log("MODIFIED");
        console.log(changes);

        ensureNoNulls(changes);

        changes.Events.forEach(function (ev) {
            var id = ev.Id;
            var $element = $("#event-" + id);
            ev.Matches = [];
            if ($element.length == 0) {
                for (var i = changes.Matches.length - 1; i >= 0; i--) {
                    var currentMatch = changes.Matches[i];
                    if (currentMatch.EventId === ev.Id) {
                        pushApply(ev.Matches, changes.Matches.splice(i, 1));
                    }

                    currentMatch.Bets = [];
                    for (var j = changes.Bets.length - 1; j >= 0; j--) {
                        var currentBet = changes.Bets[j];
                        if (currentBet.MatchId == currentMatch.Id) {
                            pushApply(currentMatch.Bets, changes.Bets.splice(j, 1));
                        }

                        currentBet.Odds = [];
                        for (var k = changes.Odds.length - 1; k >= 0; k--) {
                            var currentOdd = changes.Odds[k];
                            if (currentOdd.BetId == currentBet.Id) {
                                pushApply(currentBet.Odds, changes.Odds.splice(k, 1));
                            }
                        }
                    }
                }

                var html = templates.event(ev);
                $("#events").prepend(html);

                ev.Matches.forEach(function (m) {
                    $("#match-toggle-" + m.Id).collapse();
                });
            } else {
                $element.find(".event-name").text(ev.Name);
            }
        });

        changes.Matches.forEach(function (m) {
            var id = m.Id;
            var $element = $("#match-" + id);
            m.Bets = [];
            if ($element.length == 0) {
                m.Bets = [];
                for (var j = changes.Bets.length - 1; j >= 0; j--) {
                    var currentBet = changes.Bets[j];
                    if (currentBet.MatchId == m.Id) {
                        pushApply(m.Bets, changes.Bets.splice(j, 1));
                    }

                    currentBet.Odds = [];
                    for (var k = changes.Odds.Length - 1; k >= 0; k--) {
                        var currentOdd = changes.Odds[k];
                        if (currentOdd.BetId == currentBet.Id) {
                            pushApply(currentBet.Odds, changes.Odds.splice(k, 1));
                        }
                    }
                }

                var html = templates.match(m);
                $("#event-" + m.EventId).append(html);

            } else {
                $element.find(".match-name").text(m.Name);
            }
        });

        changes.Bets.forEach(function (b) {
            var id = b.Id;
            var $element = $("#bet-" + id);
            b.Odds = [];
            if ($element.length == 0) {
                for (var k = changes.Odds.Length - 1; k >= 0; k--) {
                    var currentOdd = changes.Odds[k];
                    if (currentOdd.BetId == b.Id) {
                        pushApply(b.Odds, changes.Odds.splice(k, 1));
                    }
                }

                var html = templates.bet(b);
                $("#match-toggle-" + b.MatchId).append(html);
            } else {
                $element.find(".bet-name").text(b.Name);
            }
        });

        changes.Odds.forEach(function (o) {
            var id = o.Id;
            var $element = $("#odd-" + id);

            if ($element.length == 0) {
                var html = templates.odd(o);
                $("#bet-" + o.BetId).append(html);
            } else {
                $element.find(".odd-name").text(o.Name);
                $element.find(".odd-specialBetValue").text(o.SpecialBetValue);
                $element.find(".odd-value").text(o.Value);
            }
        });
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

    function pushApply(destination, source) {
        Array.prototype.push.apply(destination, source);
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