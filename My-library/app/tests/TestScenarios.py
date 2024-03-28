from classes.EventsDispatcher import EventsDispatcher
from connectors.DatabaseConnector import DatabaseConnector
from utilities.const import Core
from utilities.models import Book, BookMetadata, Group, User


class TestScenarios:
    def test_database(self):
        database = DatabaseConnector(path = "../../data/tests.db")
        database.clear_all()

        root_group: Group = database.insert_group(name = Core.DEFAULT_COLLECTION)
        user: User = database.insert_user(name = "Ilya", login = "login1", password = "pass1", root_group_id = root_group.identifier)
        assert user.identifier == 1
        assert user.name == "Ilya"
        assert user.login == "login1"
        assert user.password == "pass1"
        assert user.root_group_id == 1
        assert root_group.name == Core.DEFAULT_COLLECTION

        user: User = database.get_user_by_login(login = "login1", password = "pass1")
        assert user.identifier == 1
        assert user.name == "Ilya"
        assert user.login == "login1"
        assert user.password == "pass1"
        assert user.root_group_id == 1

        assert database.get_user_by_login(login = "login1", password = "pass2") is None

        database.update_user_name(user_id = user.identifier, name = "Anton")
        database.update_user_login(user_id = user.identifier, login = "login2")
        database.update_user_password(user_id = user.identifier, password = "pass2")
        user = database.get_user(user_id = user.identifier)
        assert user.name == "Anton"
        assert user.login == "login2"
        assert user.password == "pass2"

        group_2: Group = database.insert_group(name = "Group 2")
        group_3: Group = database.insert_group(name = "Group 3")
        group_4: Group = database.insert_group(name = "Group 4")
        database.add_group_to_group(parent_group_id = root_group.identifier, child_group_id = group_2.identifier)
        database.add_group_to_group(parent_group_id = root_group.identifier, child_group_id = group_3.identifier)
        database.add_group_to_group(parent_group_id = group_2.identifier, child_group_id = group_4.identifier)

        root_group: Group = database.get_group(group_id = root_group.identifier)
        group_2: Group = database.get_group(group_id = group_2.identifier)
        assert root_group.subgroups_ids == [group_2.identifier, group_3.identifier]
        assert group_2.subgroups_ids == [group_4.identifier]

        database.delete_group_from_group(parent_group_id = root_group.identifier, child_group_id = group_3.identifier)
        root_group: Group = database.get_group(group_id = root_group.identifier)
        assert root_group.subgroups_ids == [group_2.identifier]

        metadata = BookMetadata(isbn = 123, title = "Title", authors = "Ilya, Anton", publisher = "GoldPrint", year = 2020, language = "ru")
        book: Book = database.insert_book(metadata = metadata)
        book: Book = database.get_book(book_id = book.identifier)
        assert book.identifier == 1
        assert book.metadata.isbn == 123
        assert book.metadata.title == "Title"
        assert book.metadata.authors == "Ilya, Anton"
        assert book.metadata.publisher == "GoldPrint"
        assert book.metadata.year == 2020
        assert book.metadata.language == "ru"

        database.update_book_comment(book_id = book.identifier, comment = "Best book ever!")
        database.update_book_color(book_id = book.identifier, color = "blue")
        database.update_book_place(book_id = book.identifier, place = "3-11")
        book: Book = database.get_book(book_id = book.identifier)
        assert book.comment == "Best book ever!"
        assert book.color == "blue"
        assert book.place == "3-11"

    def test_events(self):
        database = DatabaseConnector(path = "../../data/tests.db")
        database.clear_all()
        events = EventsDispatcher(database_connector = database)

        assert events.root() == {"Response": "OK 200"}

        created_user: User = events.create_user(name = "Ilya", login = "login1", password = "pass1")
        got_user: User = events.get_user(user_id = created_user.identifier)
        assert created_user.name == got_user.name == "Ilya"
        assert created_user.login == got_user.login == "login1"
        assert created_user.password == got_user.password == "pass1"
        assert created_user.identifier == got_user.identifier == 1
        assert created_user.root_group_id == got_user.root_group_id == 1

        user: User = events.get_user_by_login(login = "login1", password = "pass1")
        assert user.identifier == 1
        assert user.name == "Ilya"
        assert user.login == "login1"
        assert user.password == "pass1"
        assert user.root_group_id == 1

        assert events.get_user_by_login(login = "login1", password = "pass2") == {"Error": "User with that login and password not found"}

        metadata: BookMetadata = events.get_book_metadata(isbn = "0-451-19114-5")
        assert metadata.isbn == 9780451191144
        assert metadata.title == "Atlas Shrugged"
        assert metadata.authors == "Ayn Rand"
        assert metadata.publisher == "Penguin"
        assert metadata.year == 1996
        assert metadata.language == "en"

        added_book: Book = events.add_book(isbn = "0-451-19114-5", group_id = 1)
        got_book: Book = events.get_book(book_id = added_book.identifier)
        assert added_book.identifier == got_book.identifier == 1
        assert added_book.metadata.isbn == got_book.metadata.isbn == 9780451191144
        assert added_book.metadata.title == got_book.metadata.title == "Atlas Shrugged"
        assert added_book.metadata.authors == got_book.metadata.authors == "Ayn Rand"
        assert added_book.metadata.publisher == got_book.metadata.publisher == "Penguin"
        assert added_book.metadata.year == got_book.metadata.year == 1996
        assert added_book.metadata.language == got_book.metadata.language == "en"
        assert added_book.comment == got_book.comment is None
        assert added_book.color == got_book.color is None
        assert added_book.place == got_book.place is None

        changed_book: Book = events.change_book(book_id = got_book.identifier, comment = "Best book ever!")
        assert changed_book.comment == "Best book ever!"
        assert changed_book.color is None
        assert changed_book.place is None

        assert events.delete_book(book_id = got_book.identifier, group_id = 1)
        got_parent_group: Group = events.get_group(group_id = 1)
        assert got_parent_group.books_ids == []

        added_group: Group = events.add_group(name = "Added group", parent_group_id = 1)
        got_parent_group: Group = events.get_group(group_id = 1)
        got_added_group: Group = events.get_group(group_id = 2)
        assert added_group.name == got_added_group.name == "Added group"
        assert added_group.identifier == got_added_group.identifier
        assert got_parent_group.subgroups_ids == [got_added_group.identifier]

        changed_group: Group = events.change_group(group_id = got_added_group.identifier, name = "New added group")
        assert changed_group.name == "New added group"

        assert events.delete_group(group_id = got_added_group.identifier, parent_group_id = got_parent_group.identifier)
        got_parent_group: Group = events.get_group(group_id = 1)
        assert got_parent_group.subgroups_ids == []

        events.add_group(name = "SubGroup ID3", parent_group_id = 1)
        events.add_group(name = "SubGroup ID4", parent_group_id = 3)
        events.add_group(name = "SubGroup ID5", parent_group_id = 4)
        events.add_book(isbn = "0-451-19114-5", group_id = 1)
        events.add_book(isbn = "0-451-19114-5", group_id = 3)
        events.add_book(isbn = "0-451-19114-5", group_id = 3)
        events.add_book(isbn = "0-451-19114-5", group_id = 4)
        assert len(events.get_user_books(user_id = 1)) == 4
        assert len(events.get_user_groups(user_id = 1)) == 4
