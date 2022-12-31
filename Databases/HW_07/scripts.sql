SELECT (EXTRACT(YEAR FROM birthdate)) as birthyear, COUNT(player.player_id) as players, COUNT(medal in ('GOLD')) as golds
FROM olympics
JOIN events event on event.olympic_id = olympics.olympic_id
JOIN results result on result.event_id = event.event_id
JOIN players player on player.player_id = result.player_id
WHERE medal in ('GOLD') AND year in (2004)
GROUP BY (EXTRACT(YEAR FROM birthdate));


SELECT event.event_id
FROM olympics
JOIN events event on event.olympic_id = olympics.olympic_id
JOIN results result on result.event_id = event.event_id
JOIN players player on player.player_id = result.player_id
WHERE is_team_event is FALSE and medal in ('GOLD')
GROUP BY event.event_id
HAVING COUNT(player.player_id) > 1;


SELECT DISTINCT player_id, event.olympic_id
from results
JOIN events event on event.event_id = results.event_id
JOIN olympics olympic on olympic.olympic_id = event.olympic_id;


SELECT countries.name, COUNT(player.name)::float / countries.population as percent
FROM countries
JOIN players player on player.country_id = countries.country_id
WHERE LEFT(player.name , 1) IN ('A','E','I','O','U')
GROUP BY countries.name, countries.population
ORDER BY percent DESC
LIMIT 1;


SELECT player.country_id, COUNT(result.medal)::float / country.population as percent
FROM olympics
JOIN events event on olympics.olympic_id = event.olympic_id
JOIN results result on event.event_id = result.event_id
JOIN players player on result.player_id = player.player_id
JOIN countries country on player.country_id = country.country_id
WHERE event.is_team_event is TRUE AND olympics.year in (2000)
GROUP BY player.country_id, country.population
ORDER BY percent
LIMIT 5;
