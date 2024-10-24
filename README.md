# Parameter Scanner &emsp; [![Workflow](https://github.com/mdhiego/parameter-scanner/actions/workflows/Workflow.yml/badge.svg)](https://github.com/mdhiego/parameter-scanner/actions/workflows/Workflow.yml)

**Parameter Scanner is a Revit add-in to help you to verify that all required parameters in your Revit models have been correctly filled out.**

---

### Description

The solution comprises and Autodesk Revit add-in project, a installer project, and a build project to build de add-in project for each supported
Revit versions generating a single/multi-user installer at the end.
The solution was organized in a manner that makes it easy to support multiple .NET BIM software programs and versions (e.g. Navisworks).

### Technologies Used

* C#
* .NET Framework 4.8
* .NET 8

### Getting Started

Before you can build this project, you will need to install .NET, depending upon the solution file you are building. If you haven't already installed
these frameworks, you can do so by visiting the following:

* For the Revit 2020 - 2024:
  [.NET Framework 4.8](https://dotnet.microsoft.com/download/dotnet-framework/net48)

* For the build system and Revit 2025+ add-in:
  [.NET 8](https://dotnet.microsoft.com/en-us/download/dotnet)

After installing the necessary frameworks, clone this repository to your local machine and navigate to the project directory.

### Building

If you don't have Visual Studio installed, download it from [here](https://visualstudio.microsoft.com/downloads/).

1. Open Visual Studio
2. Click on `File -> Open -> Project/Solution` and locate your solution file to open.
3. In the `Solutions Configuration` drop-down menu, select `Release2023` or `Debug2023`. Suffix `2023` means compiling for the Revit 2023.
4. After the solution loads, you can build it by clicking on `Build -> Build Solution`.

### Solution structure

| Folder                          | Description                                                                |
|---------------------------------|----------------------------------------------------------------------------|
| src                             | Project source code folder. Contains all solution projects                 |
| src/ENG.Plugins.Build           | Nuke build system. Used to automate project builds                         |
| src/ENG.Plugins.Revit.Installer | Revit Add-in installer, called implicitly by the Nuke build                |
| output                          | Folder of generated files by the build system, such as bundles, installers |

### Project structure

| Folder     | Description                                                                                                                                                                                          |
|------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Commands   | External commands invoked from the Revit ribbon. Registered in the `Application` class                                                                                                               |
| Models     | Classes that encapsulate the app's data, include data transfer objects (DTOs). More [details](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm).                                      |
| ViewModels | Classes that implement properties and commands to which the view can bind data. More [details](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm).                                     |
| Views      | Classes that are responsible for defining the structure, layout and appearance of what the user sees on the screen. More [details](https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm). |
| Resources  | Images, sounds, localisation files, etc.                                                                                                                                                             |
| Helpers    | Helpers used across the application                                                                                                                                                                  |
| Extensions | Extensions used across the application                                                                                                                                                               |