DROP TABLE IF EXISTS t_books;

CREATE TABLE t_books (
    book_id int,
    title text,
    category text,
    author text,
    is_active VARCHAR(1)
);

ANALYZE VERBOSE t_books;

CREATE INDEX t_books_title_idx ON t_books(title);

EXPLAIN SELECT * FROM t_books WHERE position('expert' in title) > 0;

DROP INDEX IF EXISTS t_books_title_idx;

DROP TABLE IF EXISTS t_lookup;
CREATE TABLE t_lookup (
    item_key VARCHAR(10),
    item_value VARCHAR(100),
   CONSTRAINT t_lookup_pk PRIMARY KEY(item_key)
);

INSERT INTO t_lookup SELECT book_id, title FROM t_books o WHERE book_id < 1362855;

DROP TABLE IF EXISTS t_lookup_iot;
CREATE TABLE t_lookup_iot (
    item_key VARCHAR(10),
    item_value VARCHAR(100),
   CONSTRAINT t_lookup_iot_pk PRIMARY KEY(item_key)
);
CLUSTER t_lookup_iot USING t_lookup_iot_pk;

INSERT INTO t_lookup_iot SELECT book_id, title FROM t_books o WHERE book_id < 1362855;

ANALYZE VERBOSE t_lookup;
ANALYZE VERBOSE t_lookup_iot;

EXPLAIN SELECT * FROM t_lookup WHERE item_key = '1364684';

EXPLAIN SELECT * FROM t_lookup_iot WHERE item_key = '1364684';

CREATE INDEX t_lookup_idx ON t_lookup(item_value);

CREATE INDEX t_lookup_iot_idx ON t_lookup_iot(item_value);

EXPLAIN SELECT * FROM t_lookup WHERE item_value = 'T_BOOKS';

EXPLAIN SELECT * FROM t_lookup_iot WHERE item_value = 'T_BOOKS';
