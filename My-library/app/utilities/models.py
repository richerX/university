from pydantic import BaseModel


class User(BaseModel):
    identifier: int
    login: str
    password: str
    name: str
    root_group_id: int


class Group(BaseModel):
    identifier: int
    name: str
    subgroups_ids: list[int]
    books_ids: list[int]


class BookMetadata(BaseModel):
    isbn: int
    title: str
    authors: str
    publisher: str
    year: int
    language: str


class Book(BaseModel):
    identifier: int
    metadata: BookMetadata
    comment: str | None
    color: str | None
    place: str | None

