# UP-SportFeed

[![Build status](https://ci.appveyor.com/api/projects/status/ip9ktyg4pn1mjxtk?svg=true)](https://ci.appveyor.com/project/J0hnyBG/up-sportsfeed)

- ~~Request the http://vitalbet.net/sportxml feed every 60 seconds and store the data in a MS SQL DB (Please note that due to XML feed size, browsers timeout and you can view it within the browser via Dev tools)~~
 
- ~~After the initial request store only the new entities in the database and update the existing ones if a change is present.~~
 
- ~~Use Entity Framework for all DB-related operations.~~
 
- ~~Implement a web application which will show all available matches which have odds and will start in the next 24 hours.~~
 
- ~~Data on the page should be updated once every 60 seconds.~~
 
- ~~Only new matches should be added~~
 
- ~~Matches that do not start in the next 24 hours should be removed~~
 
- ~~Data on the page should be updated once every 60 seconds - use Signal-R to avoid reloading the entire page~~
  
- ~~Only new matches should be added - use Signal-R to avoid reloading the entire page~~
 
- ~~Matches that do not start in the next 24 hours should be removed - use Signal-R to remove only the matches, do not reload the entire page~~