DROP TABLE IF EXISTS demographics;
DROP TABLE IF EXISTS names;
DROP TABLE IF EXISTS encryption;
DROP TABLE IF EXISTS repositories;
DROP TABLE IF EXISTS products;

CREATE TABLE demographics (
    id serial primary key,
    name text,
    birthday date,
    race text
);

CREATE TABLE names (
    id serial primary key,
    prefix text,
    first text,
    last text,
    suffix text
);

CREATE TABLE encryption (
    md5 text,
    sha1 text,
    sha256 text
);

CREATE TABLE repositories (
    project text,
    commits int,
    contributors int,
    address text
);

CREATE TABLE products (
    id int,
    name text,
    price float,
    stock int,
    weight float,
    producer text,
    country text
);

SELECT (length(race) + bit_length(name)) as calculation
FROM demographics;

SELECT count(*) AS count, race
FROM demographics
GROUP BY race
ORDER BY count DESC;

SELECT id, ASCII(LEFT(name, 1)) as name, birthday, ASCII(LEFT(race, 1)) as race
FROM demographics;

SELECT concat(prefix, ' ', first, ' ', last, ' ', suffix) AS title
FROM names;

SELECT CONCAT(md5, REPEAT('1', LENGTH(sha256) - LENGTH(md5))) AS md5,
       CONCAT(REPEAT('0', LENGTH(sha256)- LENGTH(sha1)), sha1) AS sha1,
       sha256
FROM encryption;

SELECT LEFT(project, commits) AS project, RIGHT(address, contributors) AS address
FROM repositories;

SELECT project, commits, contributors,
       regexp_replace(address, '[[:digit:]]', '!', 'g') AS address
FROM repositories;

SELECT name, weight, price,
       ROUND((price * 1000 / weight)::numeric, 2)::float AS price_per_kg
FROM products
ORDER BY price_per_kg, name;
