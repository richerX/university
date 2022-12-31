SELECT family FROM library.reader WHERE strpos(address, 'Москва') > 0;

SELECT author, book.name
FROM library.book
JOIN library.instance instance_ on book.id = instance_.book_id
JOIN library.rent rent_ on instance_.id = rent_.instance_id
JOIN library.reader reader_ on rent_.reader_id = reader_.id
WHERE reader_.name = 'Иван' AND reader_.family = 'Иванов';

SELECT DISTINCT book.id
FROM library.book
JOIN library.category category_ on book.category_id = category_.id
WHERE category_.name = 'Горы'
AND book.id NOT IN (
SELECT DISTINCT book.id
FROM library.book
JOIN library.category category_ on book.category_id = category_.id
WHERE category_.name = 'Путешествия');

SELECT DISTINCT reader.family, reader.name
FROM library.reader
JOIN library.rent rent_ on reader.id = rent_.reader_id
WHERE rent_.date is not NULL;

SELECT DISTINCT reader.family, reader.name
FROM library.reader
JOIN library.rent rent_ on reader.id = rent_.reader_id
JOIN library.instance instance_ on rent_.instance_id = instance_.id
JOIN library.book book_ on instance_.book_id = book_.id
WHERE book_.id IN (
    SELECT DISTINCT book.id
    FROM library.book
    JOIN library.instance instance_ on book.id = instance_.book_id
    JOIN library.rent rent_ on instance_.id = rent_.instance_id
    JOIN library.reader reader_ on reader_.id = rent_.reader_id
    WHERE reader_.family = 'Иванов' AND reader_.name = 'Иван'
    )
AND NOT (reader.family = 'Иванов' AND reader.name = 'Иван');

SELECT DISTINCT connection_.train_number
FROM connection
JOIN connection connection_ on connection_.train_number = connection_.train_number
JOIN station station1 ON connection_.from_station = station1.name
JOIN station station2 ON connection_.to_station = station2.name
WHERE station1.city_name = 'Москва'
AND station2.city_name = 'Тверь'
AND connection_.train_number NOT IN (
    SELECT DISTINCT connection_.train_number
    FROM connection
    JOIN connection connection_ on connection_.train_number = connection_.train_number
    JOIN station station1 ON connection_.from_station = station1.name
    JOIN station station2 ON connection_.to_station = station2.name
    WHERE station1.city_name = 'Москва'
    AND station2.city_name != 'Тверь'
);

SELECT train.train_number
FROM train
JOIN connection connection_ on train.train_number = connection_.train_number
JOIN station station1 ON connection_.from_station = station1.name
JOIN station station2 ON connection_.to_station = station2.name
WHERE station1.city_name = 'Москва'
AND station2.city_name = 'Питер'
AND DATE(connection_.departure) = DATE(connection_.arrival)
AND connection_.train_number IN(
    SELECT train.train_number
    FROM train
    JOIN connection connection_ on train.train_number = connection_.train_number
    JOIN station station1 ON connection_.from_station = station1.name
    JOIN station station2 ON connection_.to_station = station2.name

    WHERE station1.city_name = 'Москва'
    AND station2.city_name != 'Питер'
);
