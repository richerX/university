CREATE TABLE city (
    id serial primary key,
    name text,
    region text
);

CREATE TABLE station (
    id serial primary key,
    name text,
    city_name text,
    region text
);

CREATE TABLE train (
    id serial primary key,
    train_number int,
    length int,
    start_station_name text,
    end_station_name text
);

CREATE TABLE connection (
    id serial primary key,
    from_station text,
    to_station text,
    train_number int,
    departure date,
    arrival date
);
