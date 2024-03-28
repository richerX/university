from fastapi import APIRouter
from isbnlib import meta

from connectors.DatabaseConnector import DatabaseConnector
from utilities.const import Char, Core
from utilities.models import Book, BookMetadata, Group, User


class EventsDispatcher:
    def __init__(self, database_connector: DatabaseConnector):
        self.database: DatabaseConnector = database_connector

        self.router = APIRouter()
        self.router.add_api_route("/", self.root, methods=["GET"])
        self.router.add_api_route("/create_user", self.create_user, methods = ["GET"])
        self.router.add_api_route("/get_user", self.get_user, methods = ["GET"])
        self.router.add_api_route("/get_user_by_login", self.get_user_by_login, methods = ["GET"])
        self.router.add_api_route("/get_user_books", self.get_user_books, methods = ["GET"])
        self.router.add_api_route("/get_user_groups", self.get_user_groups, methods = ["GET"])
        self.router.add_api_route("/book_metadata", self.get_book_metadata, methods = ["GET"])

        self.router.add_api_route("/add_book", self.add_book, methods = ["GET"])
        self.router.add_api_route("/get_book", self.get_book, methods = ["GET"])
        self.router.add_api_route("/move_book", self.move_book, methods = ["GET"])
        self.router.add_api_route("/change_book", self.change_book, methods = ["GET"])
        self.router.add_api_route("/delete_book", self.delete_book, methods = ["GET"])

        self.router.add_api_route("/add_group", self.add_group, methods = ["GET"])
        self.router.add_api_route("/get_group", self.get_group, methods = ["GET"])
        self.router.add_api_route("/move_group", self.move_group, methods = ["GET"])
        self.router.add_api_route("/change_group", self.change_group, methods = ["GET"])
        self.router.add_api_route("/delete_group", self.delete_group, methods = ["GET"])

    """
    MAIN
    """

    @staticmethod
    def root() -> dict:
        return {"Response": "OK 200"}

    def create_user(self, name: str, login: str, password: str) -> User | dict:
        # http://127.0.0.1:8000/create_user?name=Ilya&login=login1&password=pass1
        user_by_login: User | None = self.database.get_user_by_login(login = login, password = password)
        if user_by_login:
            return {"Error": "User with that login and password is already exists"}
        root_group: Group = self.database.insert_group(name = Core.DEFAULT_COLLECTION)
        user: User = self.database.insert_user(name = name, login = login, password = password, root_group_id = root_group.identifier)
        return user

    def get_user(self, user_id: int) -> User:
        # http://127.0.0.1:8000/get_user?user_id=1
        return self.database.get_user(user_id = int(user_id))

    def get_user_by_login(self, login: str, password: str) -> User | dict:
        # http://127.0.0.1:8000/get_user_by_login?login=login1&password=pass1
        user_by_login: User | None = self.database.get_user_by_login(login = login, password = password)
        if user_by_login is None:
            return {"Error": "User with that login and password not found"}
        return user_by_login

    def get_user_groups(self, user_id: int) -> list[Group]:
        # http://127.0.0.1:8000/get_user_groups?user_id=1
        user: User = self.database.get_user(user_id = int(user_id))

        groups: list[Group] = []
        visited_ids: set[int] = set()
        group_ids: list[int] = [user.root_group_id]
        while group_ids:
            group_id = group_ids.pop()
            if group_id in visited_ids:
                continue
            visited_ids.add(group_id)

            group: Group = self.database.get_group(group_id = group_id)
            group_ids.extend(group.subgroups_ids)
            groups.append(group)

        return groups

    def get_user_books(self, user_id: int) -> list[Book]:
        # http://127.0.0.1:8000/get_user_books?user_id=1
        user: User = self.database.get_user(user_id = int(user_id))

        books: list[Book] = []
        visited_ids: set[int] = set()
        group_ids: list[int] = [user.root_group_id]
        while group_ids:
            group_id = group_ids.pop()
            if group_id in visited_ids:
                continue
            visited_ids.add(group_id)

            group: Group = self.database.get_group(group_id = group_id)
            group_ids.extend(group.subgroups_ids)
            for book_id in group.books_ids:
                book: Book = self.database.get_book(book_id = book_id)
                books.append(book)

        return books

    @staticmethod
    def get_book_metadata(isbn: str) -> BookMetadata:
        # http://127.0.0.1:8000/book_metadata?isbn=978-50-42477-03-4
        # http://127.0.0.1:8000/book_metadata?isbn=9785042477034
        # http://127.0.0.1:8000/book_metadata?isbn=0-451-19114-5
        response = meta(isbn)
        return BookMetadata(
            isbn = int(response.get('ISBN-13')),
            title = response.get('Title'),
            authors = Char.COMMA.join(response.get('Authors')),
            publisher = response.get('Publisher'),
            year = int(response.get('Year')),
            language = response.get('Language'),
        )

    """
    BOOK
    """

    def add_book(self, isbn: str, group_id: int) -> Book:
        # http://127.0.0.1:8000/add_book?isbn=0-451-19114-5&group_id=1
        group: Group = self.database.get_group(group_id = group_id)
        response = meta(isbn)
        book_metadata = BookMetadata(
            isbn = int(response.get('ISBN-13')),
            title = response.get('Title'),
            authors = Char.COMMA.join(response.get('Authors')),
            publisher = response.get('Publisher'),
            year = int(response.get('Year')),
            language = response.get('Language'),
        )
        book: Book = self.database.insert_book(metadata = book_metadata)
        self.database.add_book_to_group(group_id = group.identifier, book_id = book.identifier)
        return book

    def get_book(self, book_id: int) -> Book:
        # http://127.0.0.1:8000/get_book?book_id=1
        return self.database.get_book(book_id = book_id)

    def move_book(self, book_id: int, current_group_id: int, target_group_id: int) -> bool:
        # http://127.0.0.1:8000/move_book?book_id=1&current_group_id=1&target_group_id=2
        result1: bool = self.database.delete_book_from_group(group_id = current_group_id, book_id = book_id)
        result2: bool = self.database.add_book_to_group(group_id = target_group_id, book_id = book_id)
        return result1 and result2

    def change_book(self, book_id: int, comment: str | None = None, color: str | None = None, place: str | None = None) -> Book:
        # http://127.0.0.1:8000/change_book?book_id=1&comment=Comment&place=3-11
        if comment:
            self.database.update_book_comment(book_id = book_id, comment = comment)
        if color:
            self.database.update_book_color(book_id = book_id, color = color)
        if place:
            self.database.update_book_place(book_id = book_id, place = place)
        return self.database.get_book(book_id = book_id)

    def delete_book(self, book_id: int, group_id: int) -> bool:
        # http://127.0.0.1:8000/delete_book?book_id=1&group_id=2
        result1: bool = self.database.delete_book_from_group(group_id = group_id, book_id = book_id)
        result2: bool = self.database.delete_book(book_id = book_id)
        return result1 and result2

    """
    GROUP
    """

    def add_group(self, name: str, parent_group_id: int) -> Group:
        # http://127.0.0.1:8000/add_group?name=NewGroup&parent_group_id=1
        parent_group: Group = self.database.get_group(group_id = parent_group_id)
        group: Group = self.database.insert_group(name = name)
        self.database.add_group_to_group(parent_group_id = parent_group.identifier, child_group_id = group.identifier)
        return group

    def get_group(self, group_id: int) -> Group:
        # http://127.0.0.1:8000/group_book?group_id=1
        return self.database.get_group(group_id = group_id)

    def move_group(self, group_id: int, current_group_id: int, target_group_id: int) -> bool:
        # http://127.0.0.1:8000/move_group?group_id=1&current_group_id=1&target_group_id=2
        result1: bool = self.database.delete_group_from_group(parent_group_id = current_group_id, child_group_id = group_id)
        result2: bool = self.database.add_group_to_group(parent_group_id = target_group_id, child_group_id = group_id)
        return result1 and result2

    def change_group(self, group_id: int, name: str | None = None) -> Group:
        # http://127.0.0.1:8000/change_group?group_id=1&name=NewName
        if name:
            self.database.update_group_name(group_id = group_id, name = name)
        return self.database.get_group(group_id = group_id)

    def delete_group(self, group_id: int, parent_group_id: int) -> bool | dict:
        # http://127.0.0.1:8000/delete_group?group_id=2&parent_group_id=1
        group: Group = self.database.get_group(group_id = group_id)
        if group.subgroups_ids or group.books_ids:
            return {"Error": "Group is not empty"}

        if self.database.check_group_is_root_id(group_id = group_id):
            return {"Error": "Group is root for some user"}

        result: bool = self.database.delete_group_from_group(parent_group_id = parent_group_id, child_group_id = group_id)
        return result
