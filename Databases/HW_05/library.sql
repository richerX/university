CREATE SCHEMA library;

CREATE TABLE library.reader (
    id serial primary key,
    name text,
    family text,
    address text,
    birth_year date
);

CREATE TABLE library.publisher (
    id serial primary key,
    name text,
    address text
);

CREATE TABLE library.category (
    id serial primary key,
    subcategory int references library.category (id),
    name text
);

CREATE TABLE library.book (
    id serial primary key,
    publisher_id int references library.publisher (id),
    category_id int references library.category (id),
    year date,
    name text,
    author text,
    number_of_pages int
);

CREATE TABLE library.instance (
    id serial primary key,
    book_id int references library.book (id),
    number_of_copy int,
    shelf_position int
);

CREATE TABLE library.rent (
    id serial primary key,
    reader_id int references library.reader (id),
    instance_id int references library.instance (id),
    date date
);
