#!/bin/bash

dotnet test ; coverlet ./tests/bin/Debug/netcoreapp2.2/OnixTest.dll --target "dotnet" --targetargs "test . --no-build" --format lcov
