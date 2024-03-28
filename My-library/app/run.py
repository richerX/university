from logging import Logger

import uvicorn
from fastapi import FastAPI

from classes.EventsDispatcher import EventsDispatcher
from connectors.DatabaseConnector import DatabaseConnector
from utilities.logger import LoggerFactory
from utilities.settings import PathSettings


def main() -> None:
    logger: Logger = LoggerFactory.get_logger(__name__)
    logger.info("Container initialized")

    database_connector: DatabaseConnector = DatabaseConnector(path = PathSettings.DB)
    logger.info(f"DatabaseConnector initialized")

    events_dispatcher: EventsDispatcher = EventsDispatcher(
        database_connector = database_connector,
    )
    logger.info("EventsDispatcher initialized")

    app = FastAPI()
    app.include_router(events_dispatcher.router)
    logger.info("App routers initialized")

    uvicorn.run(app, host = "127.0.0.1", port = 8000)


if __name__ == "__main__":
    main()
