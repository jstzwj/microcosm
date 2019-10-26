cmake_minimum_required (VERSION 3.8)

project ("asio")

set(ASIO_CPP ${PROJECT_SOURCE_DIR}/src/asio.cpp)
add_library(asio ${ASIO_CPP})