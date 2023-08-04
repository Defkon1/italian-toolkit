# Italian Toolkit

[![Lint Code Base](https://github.com/defkon1/italian-toolkit/actions/workflows/linter.yml/badge.svg)](https://github.com/defkon1/italian-toolkit/actions/workflows/linter.yml)
[![CodeQL](https://github.com/defkon1/italian-toolkit/actions/workflows/codeql.yml/badge.svg)](https://github.com/defkon1/italian-toolkit/actions/workflows/codeql.yml)
[![NuGet](https://img.shields.io/nuget/v/ItalianToolkit.svg?style=flat-square)](https://www.nuget.org/packages/ItalianToolkit)
[![Nuget](https://img.shields.io/nuget/dt/ItalianToolkit)](https://www.nuget.org/packages/ItalianToolkit)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/defkon1/italian-toolkit/blob/master/LICENSE)

A collection of helper methods and classes for .NET for common Italy-related data management packed in a single library to avoid code duplication.

# Installation

The library is available on [NuGet](https://www.nuget.org/packages/ItalianToolkit). Just search for *ItalianToolkit* in the **Package Manager GUI** or run the following command in the **.NET CLI**:

    dotnet add package ItalianToolkit

# Roadmap

- [ ] Fiscal codes management
  - [x] Formal validation
  - [x] Calculation from person master data
  - [x] Homocodies calculation from base fiscal code
  - [x] Homocody validation
  - [ ] Birthplace validation
- [x] ANPR Identifier (ID ANPR)
  - [x] Formal validation (EXPERIMENTAL)
  - [x] Check character calculation (EXPERIMENTAL)
- [ ] Transports
  - [ ] Car plates validation
    - [ ] Car plates (pre 1994)
    - [x] Car plates (post 1994)
  - [ ] Motorcycles and Motorbikes plates validation
    - [x] Motorcycles (mopeds, three-wheelers, motor-tractors and light quadricycles with an engine capacity of less than 50 cm³)
    - [x] Motorbikes (motor vehicles and quadrycycles with an engine capacity of more than 50 cm³) - (Warning: old provinces codes check is missing)
  - [ ] Special vehicles plates validation
    - [ ] Carabineers
    - [ ] Civil Protection Department
      - [x] Cars
      - [ ] Trailers used as emergency shelter
      - [x] Civil Protection - Aosta Valley
      - [x] Civil Protection - Bolzano
      - [x] Civil Protection - Friuli
      - [X] Civil Protection - Trento
    - [x] Coast Guard - Port Authorities
      - [x] Department representative vehicles
      - [x] Cars
      - [x] Buses and heavy vehicles
      - [x] Motorbikes
      - [x] Trailers 
    - [ ] Diplomatic, consular corps and United Nations vehicles
      - [ ] Consolar corps
      - [ ] Diplomatic corps
      - [ ] United Nations (service vehicles)
      - [ ] United Nations (staff personal vehicles)
      - [ ] United Nations (vehicles in transit through Italy)
    - [x] Foreign Excursionists (EE) temporary plates
    - [x] Finance Guards
      - [x] Cars
      - [x] Motorbikes
      - [x] Trailers
    - [ ] Fire Fighters
      - [ ] Cars
      - [ ] Trailers
      - [ ] Fire Fighters - Bolzano
      - [ ] Fire Fighters - Trento
    - [x] Forestry Corps
      - [x] Italian Forestry Corps
      - [x] Forestry Corps - Aosta
      - [x] Forestry Corps - Bolzano
      - [x] Forestry Corps - Friuli      
      - [x] Forestry Corps - Sardinia (all provinces)
      - [x] Forestry Corps - Sicily (all provinces)
      - [x] Forestry Corps - Trento
    - [ ] Historical vehicles re-registered
    - [x] Italian Red Cross
      - [x] Italian Red Cross (cars and ambulances)
      - [x] Italian Red Cross (motorcycles, motorbikes, trailers and roulottes)
    - [ ] Italian Air Force
      - [ ] Cars
      - [ ] Motorbikes
    - [ ] Italian Army
      - [ ] Cars
      - [ ] Trailers
      - [ ] Tanks and armored vehicles
      - [ ] Hystorical vehicles
    - [ ] Italian Navy
      - [ ] Cars
      - [ ] Trailers
    - [ ] Local Police
      - [ ] Cars
      - [ ] Motorcycles
      - [ ] Motorbikes
    - [x] Penitentiary Police
    - [ ] Temporary plates for vehicles in transit
    - [ ] Temporary plates for vehicles in transit of Foreign Entities
    - [ ] Test plates
    - [x] Sovereign Military Order of Malta
      - [x] Cars
      - [x] Motorbykes
    - [x] State Police   
- [ ] Places
  - [ ] Regions
  - [ ] Provinces
  - [ ] Municipalities and metropolitan areas
- [ ] Other? - File an issue!

## ANPR Identifier support (EXPERIMENTAL)

The validation methods for ANPR Identifier are released as EXPERIMENTAL because the *official technical specification is not yet available*, and it was written reverse engineering the available technical information (and a bit of try & guess). 

# Contributing

Contributions are welcome. Feel free to file issues and pull requests on the repo and we'll address them as we can. 
This repo uses [Project Template](https://github.com/Josee9988/project-template)'s issue templates, but feel free to open a blank issue if you don't find a template that fits your needs.

> **Warning**
Always use the **developizza** branch for your Pull Requests, don't target the **maindolino** branch directly.

# Blog
<!-- BLOG-POST-LIST:START -->
- [Italian Toolkit 1.1.1 – Supporto all’Identificativo unico nazionale &lpar;ID ANPR&rpar;](https://www.alessiomarinelli.it/2023/06/italian-toolkit-1-1-1-supporto-allidentificativo-unico-nazionale-id-anpr/)
- [Italian Toolkit – developizza e maindolino](https://www.alessiomarinelli.it/2023/05/italian-toolkit-developizza-e-maindolino/)
<!-- BLOG-POST-LIST:END -->

# Changelog

You can find a detailed changelog [here](https://github.com/Defkon1/italian-toolkit/blob/maindolino/CHANGELOG.md).

# Thanks

Special thanks to [Marco Minerva](https://github.com/marcominerva/) for his continuous and tireless work in favor of the community, and for [this video in particular](https://www.youtube.com/watch?v=N-MYq7HXhew) from which I practically copied & pasted the version management.

# GitHub + DEV 2023 Hackathon

This repo participates to the [GitHub + DEV 2023 Hackathon](tps://dev.to/devteam/announcing-the-github-dev-2023-hackathon-4ocn), and shows how to use the following Github Actions:
 * [CodeQL](https://github.com/Defkon1/italian-toolkit/blob/maindolino/.github/workflows/codeql.yml)
 * [Linter](https://github.com/Defkon1/italian-toolkit/blob/maindolino/.github/workflows/linter.yml)
 * [Build and push to NuGet](https://github.com/Defkon1/italian-toolkit/blob/maindolino/.github/workflows/publish-to-nuget.yml)
 * [Update README with blog posts](https://github.com/Defkon1/italian-toolkit/blob/maindolino/.github/workflows/blog-posts-workflow.yml)
 * [Test runner with report generation](https://github.com/Defkon1/italian-toolkit/blob/maindolino/.github/workflows/test-runner.yml)

# License

[MIT](https://github.com/defkon1/italian-toolkit/blob/master/LICENSE) © [Alessio Marinelli](https://www.alessiomarinelli.it/)
