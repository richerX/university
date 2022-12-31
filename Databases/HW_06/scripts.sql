SELECT Book.Title, Book.PubName FROM Book;

SELECT Book.ISBN
FROM Book
WHERE Book.PagesNum = (SELECT MAX(Book.PagesNum) FROM Book);

SELECT Book.Author
FROM Book
GROUP BY Book.Author
HAVING COUNT(*) > 5;

SELECT Book.ISBN
FROM Book
WHERE Book.PagesNum > 2 * (SELECT AVG(Book.PagesNum) FROM Book);

SELECT category1.CategoryName
FROM Category category1
WHERE EXISTS (SELECT * FROM Category category2 WHERE category2.ParentCat = category1.CategoryName);

SELECT Book.Author
FROM Book
GROUP BY Book.Author
HAVING COUNT(*) = (
    SELECT MAX(c) FROM (
        SELECT count(*) AS c
        FROM Book
        GROUP BY Book.Author
    ) as _
);

SELECT reader.ID
FROM Reader reader
WHERE (
    SELECT COUNT(DISTINCT B1.ISBN)
    FROM Book B1
    JOIN Borrowing B2 on B1.ISBN = B2.ISBN
    WHERE B2.ReaderNr = reader.ID
    AND B1.Author = 'Mark Twain'
) = (
    SELECT COUNT(allbooks.ISBN) as num
    FROM Book allbooks
    WHERE allbooks.Author = 'Mark Twain'
);

SELECT Book.ISBN
FROM Book
JOIN Copy ON Book.ISBN = Copy.ISBN
GROUP BY Book.ISBN
HAVING COUNT(*) > 1;

SELECT Book.ISBN
FROM Book
ORDER BY Book.PubYear
LIMIT 10;

WITH RECURSIVE Categoryunder(category) AS (
    SELECT Category.CategoryName
    FROM Category
    WHERE Category.ParentCat = 'Sport'

    UNION

    SELECT Category.CategoryName
    FROM Category
    JOIN Categoryunder current ON Category.ParentCat = current.category
)
SELECT * from Categoryunder;
 
INSERT INTO Borrowing(ReaderNr, ISBN, CopyNumber)
SELECT Reader.ID, '123456', 4
FROM Reader
WHERE Reader.FirstName = 'John'
AND Reader.LastName = 'Johnson'
LIMIT 1;

DELETE FROM Book
WHERE Book.PubYear > '2000-12-31';

UPDATE Borrowing
SET ReturnDate = ReturnDate + 30
WHERE (Borrowing.ISBN, Borrowing.ReaderNr, Borrowing.ReturnDate) IN (
    SELECT B.ISBN, B.ReaderNr, B.ReturnDate
    FROM Borrowing B
    JOIN Book B2 on B2.ISBN = B.ISBN
    JOIN BookCat BC on B2.ISBN = BC.ISBN
    WHERE B.ReturnDate >= '2016-01-01'
    AND BC.CategoryName = 'Databases'
);
