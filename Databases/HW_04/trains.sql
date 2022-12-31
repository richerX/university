CREATE SCHEMA trains;

CREATE TABLE trains.city (
    id serial primary key,
    name text,
    region text
);

CREATE TABLE trains.station (
    id serial primary key,
    name text,
    city int references trains.city (id)
);

CREATE TABLE trains.trains (
    id serial primary key,
    number int,
    length int
);

CREATE TABLE trains.stop (
    id serial primary key,
    train_id int references trains.trains (id),
    station_id int references trains.station (id),
    arrival time,
    departure time
);
