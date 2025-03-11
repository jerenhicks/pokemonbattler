# Pokemon Battle Simulator

<div align="center">

![.NET 8.0](https://img.shields.io/badge/Version-.NET%208.0-informational?style=flat&logo=dotnet)
&nbsp;
[![CI Build](https://github.com/irongut/CodeCoverageSummary/actions/workflows/ci-build.yml/badge.svg)](https://github.com/irongut/CodeCoverageSummary/actions/workflows/ci-build.yml)

</div>

## Profiling

1. run the following command 'dotnet tool install --global dotnet-trace'
2. Run application
3. Find IP by typing 'dotnet trace ps'. Copy the ID for the service that is running
4. Run the following command 'dotnet trace collect -p XXXXX --format Speedscope' where XXXX is the ID found in previous step
5. After some time, hit enter to end trace.
6. Find file location based on the outputted message at the end.
7. Load data here: https://www.speedscope.app/
