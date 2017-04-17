using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

using Bytes2you.Validation;

using SportsFeed.Models;
using SportsFeed.Services.Contracts;

namespace SportsFeed.Services
{
    public class VitalBetInformationService : IBetInformationService
    {
        public const string VitalBetApiUrl = "http://vitalbet.net/sportxml";

        private readonly IWebRequestService webRequestService;

        public VitalBetInformationService(IWebRequestService webRequestService)
        {
            Guard.WhenArgument(webRequestService, nameof(webRequestService)).IsNull().Throw();

            this.webRequestService = webRequestService;
        }

        public IList<Sport> GetData()
        {
           // var text = File.ReadAllText(@"D:\SampleDataLite.xml");
            var text = this.webRequestService.DownloadString(VitalBetApiUrl);

            var serializer = new XmlSerializer(typeof(XmlSports));
            var memStream = new MemoryStream(Encoding.UTF8.GetBytes(text));
            var deserialized = (XmlSports)serializer.Deserialize(memStream);

            var sports = deserialized.Sports.ToList();

            foreach (var sport in sports)
            {
                foreach (var sportEvent in sport.Events.ToList())
                {
                    sportEvent.Matches.RemoveWhere(m => m.StartDate < DateTime.UtcNow
                                                        || m.StartDate > DateTime.UtcNow.AddDays(1));

                    foreach (var match in sportEvent.Matches)
                    {
                        match.Bets.RemoveWhere(bet => bet.Odds.Count == 0);
                    }

                    sportEvent.Matches.RemoveWhere(m => m.Bets.Count == 0);

                    if (sportEvent.Matches.Count == 0)
                    {
                        sport.Events.Remove(sportEvent);
                        continue;
                    }

                    sportEvent.SportId = sport.Id;

                    foreach (var sportEventMatch in sportEvent.Matches)
                    {
                        sportEventMatch.EventId = sportEvent.Id;

                        foreach (var bet in sportEventMatch.Bets)
                        {
                            bet.MatchId = sportEventMatch.Id;

                            foreach (var odd in bet.Odds)
                            {
                                odd.BetId = bet.Id;
                            }
                        }
                    }
                }
            }

            return sports;
        }
    }
}
