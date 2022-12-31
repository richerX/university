create table Events (
    event_id char(10) unique,
    name char(40),
    olympic_id char(7),
    is_team_event boolean,
    foreign key (olympic_id) references Olympics(olympic_id)
);
