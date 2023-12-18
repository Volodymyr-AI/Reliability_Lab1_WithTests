# Reliability Lab1 WPF Project

This WPF project is designed to calculate and visualize a reliability model using the Runge-Kutta numerical method. The application allows users to input parameters and run simulations to obtain a table of results, which can be displayed in a separate window.

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The project is a Windows Presentation Foundation (WPF) application written in C#. It utilizes the Runge-Kutta numerical method to solve a system of differential equations representing a reliability model. The results are then displayed in a table for easy analysis.

## Features

- **User Interface:** The project includes a WPF-based graphical user interface that allows users to input various parameters for the reliability model.

- **Runge-Kutta Solver:** The `MyClass` class contains a Runge-Kutta solver (`RungeKutta` method) to numerically solve the system of differential equations.

- **Table Display:** The application generates a table of results, displaying the values of different variables over time. The table includes the individual probabilities (`P1` to `P10`), the sum of probabilities (`SUM`), and other derived values (`Pt`, `Qt`).

## Requirements

- Windows operating system
- .NET Framework

## Installation

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Build the solution to restore dependencies.

## Usage

1. Launch the application.
2. Input the required parameters in the main window.
3. Click the "Run" button to calculate and display the results in a separate table window.

## Contributing

If you would like to contribute to the project, feel free to fork the repository and submit a pull request. Bug reports, feature requests, and feedback are also welcome.

## License

This project is licensed under the [MIT License](LICENSE.md).
