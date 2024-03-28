import os
import sys
from logging import DEBUG, Formatter, Logger, StreamHandler, getLogger, handlers
from typing import Literal

from utilities.settings import PathSettings


class LoggerFactory:
    NAME_PREFIX: str = "app."
    STRING: str = "{asctime} :: {levelname:^6s} :: {funcName:^20s} :: {message}\n"
    STYLE: Literal["%", "{", "$"] = "{"
    BASE_NAME = PathSettings.LOG + "/app.log"
    MODE = "a"
    MAX_BYTES = 1000000
    BACKUP_COUNT = 10
    ENCODING = "utf-8"

    @classmethod
    def get_logger(cls, name: str) -> Logger:
        logger = getLogger(cls.NAME_PREFIX + name)
        logger.setLevel(DEBUG)

        formatter = Formatter(cls.STRING, style = cls.STYLE)

        if not os.path.exists(PathSettings.LOG):
            os.makedirs(PathSettings.LOG)

        file_handler = handlers.RotatingFileHandler(
            filename = cls.BASE_NAME,
            mode = cls.MODE,
            maxBytes = cls.MAX_BYTES,
            backupCount = cls.BACKUP_COUNT,
            encoding = cls.ENCODING
        )
        file_handler.setLevel(DEBUG)
        file_handler.setFormatter(formatter)

        out_hdlr = StreamHandler(sys.stdout)
        out_hdlr.setLevel(DEBUG)
        out_hdlr.setFormatter(formatter)

        logger.addHandler(file_handler)
        logger.addHandler(out_hdlr)
        return logger
