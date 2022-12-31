CREATE TABLE Reader (
    ID serial primary key,
    LastName text,
    FirstName text,
    Address text,
    BirthDate date
);

CREATE TABLE Publisher (
    PubName text primary key,
    PubAddress text
);

CREATE TABLE Book (
    ISBN serial primary key,
    Title text,
    Author text,
    PagesNum int,
    PubYear date,
    PubName text references Publisher (PubName)
);

CREATE TABLE Category (
    CategoryName text primary key,
    ParentCat text references Category (CategoryName)
);

CREATE TABLE Copy (
    ISBN serial references Book (ISBN),
    CopyNumber int,
    ShelfPosition int,
    primary key (ISBN, CopyNumber)
);

CREATE TABLE Borrowing (
    ReaderNr serial primary key references Reader (ID),
    ISBN serial,
    CopyNumber int,
    ReturnDate date,
    foreign key (ISBN, CopyNumber) references Copy (ISBN, CopyNumber)
);

CREATE TABLE BookCat (
    ISBN serial primary key references Book (ISBN),
    CategoryName text references Category (CategoryName)
);
