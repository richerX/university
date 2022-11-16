# set minimum cmake version
cmake_minimum_required(VERSION 3.5 FATAL_ERROR)

# project name and language
project(task1 LANGUAGES CXX)

set(BUILD_DIR "${CMAKE_BINARY_DIR}")
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${BUILD_DIR}/../bin")

set(BINARY_DIR "${CMAKE_BINARY_DIR}")

# Source files list
set(SOURCE_FILES
    # Headers
    animal.h
    beast.h
    bird.h
    container.h
	fish.h
	includes.h

    # Sources
    animal.cpp
    beast.cpp
    bird.cpp
    container.cpp
    fish.cpp
	includes.cpp
	main.cpp
)

add_executable(task ${SOURCE_FILES})
