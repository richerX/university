DROP TABLE IF EXISTS t_books;

CREATE TABLE t_books (
    book_id int,
    title text,
    category text,
    author text,
    is_active VARCHAR(1)
);

EXPLAIN SELECT * FROM t_books WHERE title = 'Oracle Core';

CREATE INDEX t_books_title_idx ON t_books(title);
CREATE INDEX t_books_active_idx ON t_books(is_active);

Select * from pg_indexes;

EXPLAIN SELECT * FROM t_books WHERE title = 'Oracle Core';

EXPLAIN SELECT * FROM t_books WHERE book_id = 18;

EXPLAIN SELECT * FROM t_books WHERE is_active = 'Y';

SELECT DISTINCT title, category, author FROM t_books;

DROP INDEX IF EXISTS t_books_title_idx;
DROP INDEX IF EXISTS t_books_active_idx;

EXPLAIN SELECT * FROM t_books
WHERE position('Relational' in title) > 0;

CREATE INDEX t_books_up_title_idx ON t_books(UPPER(title));

EXPLAIN SELECT * FROM t_books
WHERE position('Core' in title) > 0;

DROP INDEX IF EXISTS t_books_title_idx;
DROP INDEX IF EXISTS t_books_active_idx;
DROP INDEX IF EXISTS t_books_up_title_idx;

CREATE INDEX t_books_rev_title_idx ON t_books(title);

Select * from pg_indexes;

EXPLAIN SELECT * FROM t_books WHERE title = 'Oracle Core';

EXPLAIN SELECT * FROM t_books
WHERE position('Relational' in title) > 0;
 
-- Задание 2

EXPLAIN SELECT * FROM t_books_part WHERE book_id = 18;

EXPLAIN SELECT * FROM t_books_part WHERE title = 'Expert Oracle Database Architecture';

CREATE INDEX t_books_part_local_idx ON t_books_part(title);

EXPLAIN SELECT * FROM t_books_part WHERE title = 'Expert Oracle Database Architecture';

DROP INDEX IF EXISTS t_books_part_local_idx;

CREATE INDEX t_books_part_global_idx ON t_books_part(title);

EXPLAIN SELECT * FROM t_books_part WHERE title = 'Expert Oracle Database Architecture';

DROP INDEX IF EXISTS t_books_part_global_idx;

CREATE INDEX t_books_part_idx ON t_books_part(book_id);

EXPLAIN SELECT * FROM t_books_part WHERE book_id = 11011;

CREATE INDEX t_books_active_idx ON t_books(is_active);

EXPLAIN SELECT * FROM t_books WHERE is_active = 'Y';

CREATE INDEX t_books_author_title_index ON t_books(author, title);

EXPLAIN SELECT max(title) FROM t_books group by author;

EXPLAIN SELECT DISTINCT author FROM t_books LIMIT 10;

EXPLAIN SELECT author, title FROM t_books WHERE LEFT(title, 1) = 'T' ORDER BY author, title;

INSERT INTO t_books VALUES (150001, 'Cookbook', null, 'Mr. Hide', 'Y');

COMMIT;

CREATE INDEX t_books_cat_idx ON t_books(category);

EXPLAIN SELECT author, title FROM t_books WHERE category IS null;

DROP INDEX IF EXISTS t_books_cat_idx;

CREATE INDEX t_books_cat_null_idx ON t_books(category);

EXPLAIN SELECT author, title FROM t_books WHERE category IS null;
