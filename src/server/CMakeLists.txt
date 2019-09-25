include_directories(${CMAKE_SOURCE_DIR}/include)

set(HEADER_DIR ${CMAKE_SOURCE_DIR}/include/microcosm)
set(SOURCE_DIR ${CMAKE_SOURCE_DIR}/src)

set(
    MICROCOSM_SERVER_LIST
    ${SOURCE_DIR}/server.cpp
)

# Add source to this project's executable.
add_executable (server ${MICROCOSM_SERVER_LIST})
target_link_libraries(server ${CONAN_LIBS})
