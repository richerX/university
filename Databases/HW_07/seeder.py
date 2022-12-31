import random
import string
from datetime import timedelta, datetime


PLAYERS_COUNT = 2000
EVENTS_COUNT = 500

countries = [['Algeria', 'ALG'], ['Argentina', 'ARG'], ['Australia', 'AUS'], ['Austria', 'AUT'],['The Bahamas', 'BAH'],
             ['Barbados', 'BAR'], ['Belarus', 'BLR'], ['Brazil', 'BRA'], ['Bulgaria', 'BUL'], ['Canada', 'CAN'], ['China', 'CHN']]

olympics = [['SYD2000', 'Sydney', 2000],
            ['ATH2004', 'Athens', 2004]]

names = ['Darrell', 'Johnjay', 'Mickey', 'Guy', 'Nicholas', 'Zaaine', 'Tomson', 'Titi', 'Blazey', 'Abbas',
         'Jeronimo','Torin', 'Leno', 'Fynlay', 'Dyllan', 'Marshall', 'Kiyonari', 'Rohit', 'Kiefer', 'Mathu']

surnames = ['Meland', 'Meininger', 'Deruso', 'Mccoppin', 'Fanucchi', 'Hersberger', 'Kaufmann', 'Bollaert', 'Lavi', 
            'Fedezko', 'Morgen', 'Agard', 'Fehlinger', 'Donaghue', 'Riscen', 'Overmire', 'Auler', 'Shrewsberry', 'Bristle', 'Battles']


def random_date():
    start = datetime.strptime('1/1/1950 10:00 AM', '%m/%d/%Y %I:%M %p')
    end = datetime.strptime('1/1/1980 10:00 AM', '%m/%d/%Y %I:%M %p')    
    delta = end - start
    int_delta = (delta.days * 24 * 60 * 60) + delta.seconds
    random_second = random.randrange(int_delta)
    answer = start + timedelta(seconds = random_second)
    correct_answer = answer.strftime('%d-%m-%Y')    
    return correct_answer


def produce_countries():
    ids = []
    for country in countries:
        full_name = country[0]
        short_name = country[1]
        area = random.randint(10000, 1000000)
        population = random.randint(1000000, 10000000)
        
        ids.append(short_name)
        print(f"insert into Countries values('{full_name}', '{short_name}', {area}, {population});")
    return ids


def produce_olympics(country_ids):
    ids = []
    for olympic in olympics:
        cur_id = olympic[0]
        counrty_id = random.choice(country_ids)
        city = olympic[1]
        year = olympic[2]
        season = random.choice(["TRUE", "FALSE"])
        
        ids.append(cur_id)
        print(f"insert into Olympics values('{cur_id}', '{counrty_id}', '{city}', {year}, {season});")
    return ids


def produce_players(country_ids):
    ids = []
    for _ in range(PLAYERS_COUNT):
        name = random.choice(names) + " " + random.choice(surnames)
        player_id = "".join(random.choices(string.ascii_uppercase + string.digits, k = 10))
        counrty_id = random.choice(country_ids)
        date = random_date()
        
        ids.append(player_id)
        print(f"insert into Players values('{name}', '{player_id}', '{counrty_id}', to_date('{date}', 'dd-mm-yyyy'));")
    return ids


def produce_events(olympic_ids):
    ids = []
    for _ in range(EVENTS_COUNT):
        event_id = "".join(random.choices(string.ascii_uppercase + string.digits, k = 10))
        name = "SPORT_"+ "".join(random.choices(string.ascii_uppercase + string.digits, k = 10))
        olympic_id = random.choice(olympic_ids)
        team = random.choice(["TRUE", "FALSE"])
        
        ids.append(event_id)
        print(f"insert into Events values('{event_id}', '{name}', '{olympic_id}', {team});")
    return ids


def produce_results(event_ids, player_ids):
    ids = []
    for event_id in event_ids:
        for medal in ["GOLD", "SILVER", "BRONZE"]:
            player = random.choice(player_ids)
            print(f"insert into Results values('{event_id}', '{player}', '{medal}');")
    return ids


country_ids = produce_countries()
olympic_ids = produce_olympics(country_ids)
player_ids = produce_players(country_ids)
event_ids = produce_events(olympic_ids)
result_ids = produce_results(event_ids, player_ids)
