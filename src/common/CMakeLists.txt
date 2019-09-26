include_directories(${CMAKE_SOURCE_DIR}/include)

set(HEADER_DIR ${CMAKE_SOURCE_DIR}/include/microcosm)
set(SOURCE_DIR ${CMAKE_SOURCE_DIR}/src)

set(
    MICROCOSM_COMMON_LIST
    ${SOURCE_DIR}/common.cpp
)

# Add source to this project's executable.
add_executable (server ${MICROCOSM_SERVER_LIST})
if (USE_CONAN)
    target_link_libraries(server ${CONAN_LIBS})
else()
    target_link_libraries(server ${Boost_LIBRARIES})
endif()
