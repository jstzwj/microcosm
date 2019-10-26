include_directories(${PROJECT_SOURCE_DIR}/include)

set(HEADER_DIR ${PROJECT_SOURCE_DIR}/include/microcosm)
set(SOURCE_DIR ${PROJECT_SOURCE_DIR}/src)

set(
    MICROCOSM_CLIENT_LIST
    ${SOURCE_DIR}/client/client.cpp
)

# Add source to this project's executable.
add_executable (client ${MICROCOSM_CLIENT_LIST})
