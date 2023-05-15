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
- [ ] Transports
  - [ ] Car plates validation (pre 1994)
  - [ ] Car plates validation (post 1994) 
- [ ] Places
  - [ ] Regions
  - [ ] Provinces
  - [ ] Municipalities and metropolitan areas
- [ ] Other? - File an issue!

# Contributing

Contributions are welcome. Feel free to file issues and pull requests on the repo and we'll address them as we can.

> **Warning**
Always use the **developizza** branch for your Pull Requests, don't target the **maindolino** branch directly.

# Blog
<!-- BLOG-POST-LIST:START -->
<!-- BLOG-POST-LIST:END -->

# Thanks

Special thanks to [Marco Minerva](https://github.com/marcominerva/) for his continuous and tireless work in favor of the community, and for [this video in particular](https://www.youtube.com/watch?v=N-MYq7HXhew) from which I practically copied & pasted the version management.

# GitHub + DEV 2023 Hackathon

This repo participates to the [GitHub + DEV 2023 Hackathon](tps://dev.to/devteam/announcing-the-github-dev-2023-hackathon-4ocn), and shows how to use the following Github Actions:
 * [CodeQL](https://github.com/Defkon1/italian-toolkit/blob/maindolino/.github/workflows/codeql.yml)
 * [Linter](https://github.com/Defkon1/italian-toolkit/blob/maindolino/.github/workflows/linter.yml)
 * [Build and push to NuGet](https://github.com/Defkon1/italian-toolkit/blob/maindolino/.github/workflows/publish-to-nuget.yml)
 * [Update README with blog posts](https://github.com/Defkon1/italian-toolkit/blob/maindolino/.github/workflows/blog-posts-workflow.yml)

# License

[MIT](https://github.com/defkon1/italian-toolkit/blob/master/LICENSE) © [Alessio Marinelli](https://www.alessiomarinelli.it/)
