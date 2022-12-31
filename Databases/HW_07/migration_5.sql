create table Results (
    event_id char(10),
    player_id char(10),
    medal char(7),
    foreign key (event_id) references Events(event_id),
    foreign key (player_id) references players(player_id)
);
