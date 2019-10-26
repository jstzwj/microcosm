include_directories(${PROJECT_SOURCE_DIR}/include)

set(HEADER_DIR ${PROJECT_SOURCE_DIR}/include/microcosm)
set(SOURCE_DIR ${PROJECT_SOURCE_DIR}/src)

set(
    MICROCOSM_COMMON_LIST
    ${SOURCE_DIR}/common.cpp
)

# Add source to this project's executable.
add_executable (common ${MICROCOSM_SERVER_LIST})

