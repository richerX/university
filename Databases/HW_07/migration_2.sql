create table Olympics (
    olympic_id char(7) unique,
    country_id char(3),
    city char(50),
    year integer,
    is_summer boolean,
    foreign key (country_id) references Countries(country_id)
);
