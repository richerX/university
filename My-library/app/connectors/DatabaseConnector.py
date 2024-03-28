from collections import OrderedDict
from typing import Iterable

import dataset
from dataset import Database, Table

from utilities.models import Book, BookMetadata, Group, User


class DatabaseConnector:
    PATH = "sqlite:///{path}"

    class Tech:
        DIVIDER = ";"
        ID = "id"

    class Tables:
        USERS = "users"
        GROUPS = "groups"
        BOOKS = "books"
        SUBGROUPS_IDS = "subgroups_ids"
        GROUP_BOOKS_IDS = "group_books_ids"

    class Users:
        LOGIN = "login"
        PASSWORD = "password"
        NAME = "name"
        ROOT_GROUP_ID = "root_group_id"

    class Groups:
        NAME = "name"

    class Books:
        ISBN = "isbn"
        TITLE = "title"
        AUTHORS = "authors"
        PUBLISHER = "publisher"
        YEAR = "year"
        LANGUAGE = "language"
        COMMENT = "comment"
        COLOR = "color"
        PLACE = "place"

    class SubGroups:
        PARENT_GROUP_ID = "parent_group_id"
        CHILD_GROUP_ID = "child_group_id"

    class GroupBooks:
        GROUP_ID = "group_id"
        BOOK_ID = "book_id"

    def __init__(self, path: str):
        self.database: Database = dataset.connect(self.PATH.format(path = path))
        self.users: Table = self.database[self.Tables.USERS]
        self.groups: Table = self.database[self.Tables.GROUPS]
        self.books: Table = self.database[self.Tables.BOOKS]
        self.subgroups: Table = self.database[self.Tables.SUBGROUPS_IDS]
        self.group_books: Table = self.database[self.Tables.GROUP_BOOKS_IDS]

    """
    BASIC
    """

    @staticmethod
    def _insert_values(table: Table, values: dict) -> int:
        return table.insert(values)

    def _update_values(self, table: Table, identifier: int, values: dict) -> None:
        values[self.Tech.ID] = identifier
        table.update(values, keys = [self.Tech.ID])

    def clear_all(self):
        self.users.drop()
        self.groups.drop()
        self.books.drop()
        self.subgroups.drop()
        self.group_books.drop()

    """
    USERS
    """

    def _assemble_user(self, values: OrderedDict) -> User:
        identifier: int = values.get(self.Tech.ID)
        login: str = values.get(self.Users.LOGIN)
        password: str = values.get(self.Users.PASSWORD)
        name: str = values.get(self.Users.NAME)
        group_id: int = values.get(self.Users.ROOT_GROUP_ID)
        return User(identifier = identifier, login = login, password = password, name = name, root_group_id = group_id)

    def get_user(self, user_id: int) -> User:
        values: OrderedDict = self.users.find_one(**{self.Tech.ID: user_id})
        return self._assemble_user(values = values)

    def get_user_by_login(self, login: str, password: str) -> User | None:
        values: OrderedDict | None = self.users.find_one(**{self.Users.LOGIN: login, self.Users.PASSWORD: password})
        if values is None:
            return
        return self._assemble_user(values = values)

    def insert_user(self, name: str, login: str, password: str, root_group_id: int) -> User:
        user_id: int = self._insert_values(
            table = self.users,
            values = {
                self.Users.NAME: name,
                self.Users.LOGIN: login,
                self.Users.PASSWORD: password,
                self.Users.ROOT_GROUP_ID: root_group_id,
            }
        )
        return self.get_user(user_id = user_id)

    def update_user_name(self, user_id: int, name: str) -> None:
        self._update_values(table = self.users, identifier = user_id, values = {self.Users.NAME: name})

    def update_user_login(self, user_id: int, login: str) -> None:
        self._update_values(table = self.users, identifier = user_id, values = {self.Users.LOGIN: login})

    def update_user_password(self, user_id: int, password: str) -> None:
        self._update_values(table = self.users, identifier = user_id, values = {self.Users.PASSWORD: password})

    """
    BOOKS
    """

    # ISBN = "isbn"
    # TITLE = "title"
    # AUTHORS = "authors"
    # PUBLISHER = "publisher"
    # YEAR = "year"
    # LANGUAGE = "language"
    # COMMENT = "comment"
    # COLOR = "color"
    # PLACE = "place"

    def _assemble_book(self, values: OrderedDict) -> Book:
        identifier: int = values.get(self.Tech.ID)

        isbn: int = values.get(self.Books.ISBN)
        title: str = values.get(self.Books.TITLE)
        authors: str = values.get(self.Books.AUTHORS)
        publisher: str = values.get(self.Books.PUBLISHER)
        year: int = values.get(self.Books.YEAR)
        language: str = values.get(self.Books.LANGUAGE)
        metadata = BookMetadata(
            isbn = isbn,
            title = title,
            authors = authors,
            publisher = publisher,
            year = year,
            language = language,
        )

        comment: str | None = values.get(self.Books.COMMENT)
        color: str | None = values.get(self.Books.COLOR)
        place: str | None = values.get(self.Books.PLACE)

        return Book(identifier = identifier, metadata = metadata, comment = comment, color = color, place = place)

    def get_book(self, book_id: int) -> Book:
        values: OrderedDict = self.books.find_one(**{self.Tech.ID: book_id})
        return self._assemble_book(values = values)

    def insert_book(self, metadata: BookMetadata) -> Book:
        book_id: int = self._insert_values(
            table = self.books,
            values = {
                self.Books.ISBN: metadata.isbn,
                self.Books.TITLE: metadata.title,
                self.Books.AUTHORS: metadata.authors,
                self.Books.PUBLISHER: metadata.publisher,
                self.Books.YEAR: metadata.year,
                self.Books.LANGUAGE: metadata.language,
            }
        )
        return self.get_book(book_id = book_id)

    def delete_book(self, book_id: int) -> bool:
        values = {self.Tech.ID: book_id}
        result = self.books.find_one(**values)
        if result is None:
            return False
        self.books.delete(**values)
        return True

    def update_book_comment(self, book_id: int, comment: str) -> None:
        self._update_values(table = self.books, identifier = book_id, values = {self.Books.COMMENT: comment})

    def update_book_color(self, book_id: int, color: str) -> None:
        self._update_values(table = self.books, identifier = book_id, values = {self.Books.COLOR: color})

    def update_book_place(self, book_id: int, place: str) -> None:
        self._update_values(table = self.books, identifier = book_id, values = {self.Books.PLACE: place})

    """
    GROUPS
    """

    def _assemble_group(self, values: OrderedDict) -> Group:
        identifier: int = values.get(self.Tech.ID)
        name: str = values.get(self.Groups.NAME)

        subgroups: Iterable[OrderedDict] = self.subgroups.find(**{self.SubGroups.PARENT_GROUP_ID: identifier})
        subgroups_ids: list[int] = [subgroup.get(self.SubGroups.CHILD_GROUP_ID) for subgroup in subgroups]

        books: Iterable[OrderedDict] = self.group_books.find(**{self.GroupBooks.GROUP_ID: identifier})
        books_ids: list[int] = [book.get(self.GroupBooks.BOOK_ID) for book in books]

        return Group(identifier = identifier, name = name, subgroups_ids = subgroups_ids, books_ids = books_ids)

    def get_group(self, group_id: int) -> Group:
        values: OrderedDict = self.groups.find_one(**{self.Tech.ID: group_id})
        return self._assemble_group(values = values)

    def check_group_is_root_id(self, group_id: int) -> bool:
        values: OrderedDict = self.users.find_one(**{self.Users.ROOT_GROUP_ID: group_id})
        return values is not None

    def insert_group(self, name: str) -> Group:
        group_id: int = self._insert_values(
            table = self.groups,
            values = {
                self.Groups.NAME: name,
            }
        )
        return self.get_group(group_id = group_id)

    def add_book_to_group(self, group_id: int, book_id: int) -> bool:
        values = {self.GroupBooks.GROUP_ID: group_id, self.GroupBooks.BOOK_ID: book_id}
        result = self.group_books.find_one(**values)
        if result is not None:
            return False
        self._insert_values(table = self.group_books, values = values)
        return True

    def delete_book_from_group(self, group_id: int, book_id: int) -> bool:
        values = {self.GroupBooks.GROUP_ID: group_id, self.GroupBooks.BOOK_ID: book_id}
        result = self.group_books.find_one(**values)
        if result is None:
            return False
        self.group_books.delete(**values)
        return True

    def add_group_to_group(self, parent_group_id: int, child_group_id: int) -> bool:
        values = {self.SubGroups.PARENT_GROUP_ID: parent_group_id, self.SubGroups.CHILD_GROUP_ID: child_group_id}
        result = self.subgroups.find_one(**values)
        if result is not None:
            return False
        self._insert_values(table = self.subgroups, values = values)
        return True

    def delete_group_from_group(self, parent_group_id: int, child_group_id: int) -> bool:
        values = {self.SubGroups.PARENT_GROUP_ID: parent_group_id, self.SubGroups.CHILD_GROUP_ID: child_group_id}
        result = self.subgroups.find_one(**values)
        if result is None:
            return False
        self.subgroups.delete(**values)
        return True

    def update_group_name(self, group_id: int, name: str) -> None:
        self._update_values(table = self.groups, identifier = group_id, values = {self.Groups.NAME: name})
