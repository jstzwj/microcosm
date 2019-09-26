# CMakeList.txt : Top-level CMake project file, do global configuration
# and include sub-projects here.
#
cmake_minimum_required (VERSION 3.8)

project ("benzene")

set(CMAKE_CXX_STANDARD 17)


option(USE_CONAN "Use conan" ON)

if (USE_CONAN)
    include(${CMAKE_BINARY_DIR}/conanbuildinfo.cmake)
    conan_basic_setup()
else()
    FIND_PACKAGE( Boost 1.71.0 REQUIRED )
    INCLUDE_DIRECTORIES( ${Boost_INCLUDE_DIR} )
endif()

# Include sub-projects.
add_subdirectory ("src")