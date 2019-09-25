IF NOT EXIST build (
    mkdir build
)
cd build
conan install ..
cmake .. -G "Visual Studio 16 2019" -A "x64"
cmake --build . --config Release
cd ..