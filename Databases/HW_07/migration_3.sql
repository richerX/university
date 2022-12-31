create table Players (
    name char(40),
    player_id char(10) unique,
    country_id char(3),
    birthdate date,
    foreign key (country_id) references Countries(country_id)
);
