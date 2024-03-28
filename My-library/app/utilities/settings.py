import os
import pathlib


class PathSettings:
    LOG = os.getenv("LOG_PATH", "../logs")
    pathlib.Path(LOG).mkdir(exist_ok=True)

    DB = os.getenv("DB", "../data/local.db")
    pathlib.Path(DB).parent.mkdir(exist_ok=True)
